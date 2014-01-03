// -----------------------------------------------------------------------
// <copyright file="PropertyRule.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Linq.Expressions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PropertyRule : IPropertyRule
    {
        readonly List<IPropertyCondition> topLevelConditions = new List<IPropertyCondition>();
        readonly List<IPropertyCondition> conditions = new List<IPropertyCondition>();
        Func<CascadeMode> cascadeModeThunk = () => DefaultOptions.CascadeMode;

        public MemberInfo Member { get; private set; }

        public Func<object, object> PropertyFunc { get; private set; }

        public Action<object, object> AssignAction { get; private set; }

        public LambdaExpression ExpressionProperty { get; private set; }

        public string PropertyName { get; private set; }
        // TODO
        public string DisplayName { get; set; }

        public IPropertyCondition CurrenctCondition { get; private set; }

        public Type TypeToAssign { get; private set; }

        public IEnumerable<IPropertyCondition> Conditions
        {
            get { return conditions; }
        }

        public CascadeMode CascadeMode
        {
            get { return cascadeModeThunk(); }
            set { cascadeModeThunk = () => value; }
        }

        public IAssigner Assigner { get; private set; }
        //public object Value { get; private set; }
        //private bool _isSetValue = false;

        public Func<object, object> AssignFunc { get; private set; }

        public PropertyRule(MemberInfo member, Func<object, object> propertyFunc, Action<object, object> assignAction, LambdaExpression expression, Func<CascadeMode> cascadeModeTunk, Type typeToAssign, Type containerType)
        {
            Member = member;
            PropertyFunc = propertyFunc;
            AssignAction = assignAction;
            ExpressionProperty = expression;

            TypeToAssign = typeToAssign;
            this.cascadeModeThunk = cascadeModeTunk;
            // TODO
            PropertyName = DefaultOptions.PropertyNameResolver(containerType, member, expression);
        }

        public static PropertyRule Create<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return Create(expression, () => DefaultOptions.CascadeMode);
        }

        /// <summary>
        /// Creates a new property rule from a lambda expression.
        /// </summary>
        public static PropertyRule Create<T, TProperty>(Expression<Func<T, TProperty>> expression, Func<CascadeMode> cascadeModeThunk)
        {
            var member = expression.GetMember();
            var compiled = expression.Compile();

            ParameterExpression param1 = expression.Parameters[0];
            ParameterExpression param2 = Expression.Parameter(typeof(TProperty), "newValue");
            Expression body = Expression.Assign(expression.Body, param2);
            Expression<Action<T, TProperty>> assignExpression = Expression.Lambda<Action<T, TProperty>>(
                body, new ParameterExpression[] { param1, param2 });

            var assignCompiled = assignExpression.Compile();

            return new PropertyRule(member, compiled.CoerceToNonGeneric(), assignCompiled.CoerceToNonGeneric(), expression, cascadeModeThunk, typeof(TProperty), typeof(T));
        }

        ///// <summary>
        ///// Creates a new property rule from a lambda expression.
        ///// </summary>
        //public static PropertyRule Create<T, TProperty>(Expression<Func<T, TProperty>> expression, Func<CascadeMode> cascadeModeThunk)
        //{
        //    var member = expression.GetMember();
        //    var compiled = expression.Compile();

        //    ParameterExpression param1 = expression.Parameters[0];
        //    ParameterExpression param2 = Expression.Parameter(typeof(TProperty), "newValue");
        //    Expression body = Expression.Assign(expression.Body, param2);
        //    Expression<Action<T, TProperty>> assignExpression = Expression.Lambda<Action<T, TProperty>>(
        //        body, new ParameterExpression[] { param1, param2 });

        //    var assignCompiled = assignExpression.Compile();

        //    return new PropertyRule(member, compiled.CoerceToNonGeneric(), assignCompiled.CoerceToNonGeneric(), expression, cascadeModeThunk, typeof(TProperty), typeof(T));
        //}

        public bool IsMatch(ConditionContext context)
        {
            CascadeMode cascadeMode = this.CascadeMode;
            bool result = true;

            foreach (var condition in topLevelConditions)
            {
                bool isMatch = condition.IsMatch(new PropertyConditionContext(context, this, this.PropertyName));
                if (isMatch == false)
                {
                    return false;
                }
            }

            foreach (var condition in Conditions)
            {
                bool isMatch = condition.IsMatch(new PropertyConditionContext(context, this, this.PropertyName));
                if (cascadeMode == GAssignation.CascadeMode.And)
                {
                    if (isMatch == false) return false;
                    if (isMatch == true) result = true;
                }

                if (cascadeMode == GAssignation.CascadeMode.Or)
                {
                    if (isMatch == true) return true;
                    if (isMatch == false) result = false;
                }
            }

            return result;
        }

        //public void Assign(object value)
        //{
        //    this._isSetValue = true;
        //    this.Value = value;
        //}

        public void Assign(Func<object, object> assignFunc)
        {
            this.AssignFunc = assignFunc;
        }

        public void SetAssigner(IAssigner assigner)
        {
            this.Assigner = assigner;
        }

        public virtual void Set(ConditionContext context)
        {
            if (this.IsMatch(context) == true && context.InstanceToValidate != null)
            {
                if (this.Assigner != null)
                {
                    this.Assigner.Set(this.PropertyFunc(context.InstanceToValidate));
                }

                if (this.AssignFunc != null)
                {
                    object value = AssignFunc(context.InstanceToValidate);
                    this.AssignAction(context.InstanceToValidate, value);
                }

                //if (this._isSetValue == true)
                //{
                //    this.AssignAction(context.InstanceToValidate, this.Value);
                //}
            }
        }

        public void AddCondition(IPropertyCondition condition)
        {
            AddCondition(condition, false);
        }


        public void AddCondition(IPropertyCondition condition, bool isTopLevel)
        {
            if (isTopLevel == true)
            {
                topLevelConditions.Add(condition);
            }
            else
            {
                CurrenctCondition = condition;
                conditions.Add(condition);
            }
        }
    }
}
