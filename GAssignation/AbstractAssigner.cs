// -----------------------------------------------------------------------
// <copyright file="AbstractDefaultSetter.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using System.Linq.Expressions;
    using GAssignation.PropertyCondition;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class AbstractAssigner<T> : IAssigner<T>
    {
        readonly TrackingCollection<IPropertyRule> nestedValidators = new TrackingCollection<IPropertyRule>();

        // Work-around for reflection bug in .NET 4.5
        static Func<CascadeMode> s_cascadeMode = () => DefaultOptions.CascadeMode;
        Func<CascadeMode> cascadeMode = s_cascadeMode;

        /// <summary>
        /// Sets the cascade mode for all rules within this validator.
        /// </summary>
        public CascadeMode CascadeMode
        {
            get { return cascadeMode(); }
            set { cascadeMode = () => value; }
        }

        public void Set(object instance)
        {
            this.Set(new ConditionContext(instance));
        }

        public void Set(T instance)
        {
            Set(new ConditionContext(instance));
        }

        public virtual void Set(ConditionContext context)
        {
            foreach (IPropertyRule rule in nestedValidators)
            {
                rule.Set(context);
            }
        }

        public IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var rule = PropertyRule.Create(expression, () => CascadeMode);
            AddRule(rule);
            RuleBuilder<T, TProperty> builder = new RuleBuilder<T, TProperty>(rule);
            return builder;
        }

        public IRuleBuilderInitial<T, TCollectionElement> RuleForEach<TCollectionElement>(Expression<Func<T, IEnumerable<TCollectionElement>>> expression)
        {
            var rule = CollectionPropertyRule<TCollectionElement>.Create(expression, () => CascadeMode);
            AddRule(rule);
            IRuleBuilderInitial<T, TCollectionElement> builder = new RuleBuilder<T, TCollectionElement>(rule);
            return builder;
        }



        public void AddRule(IPropertyRule rule)
        {
            nestedValidators.Add(rule);
        }

        public void When(Func<T, bool> predicate, Action action)
        {
            var propertyRules = new List<IPropertyRule>();
            Action<IPropertyRule> onRuleAdded = propertyRules.Add;
            using (nestedValidators.OnItemAdded(onRuleAdded))
            {
                action();
            }
            propertyRules.ForEach(x => x.AddCondition(new PredicateCondition(predicate.CoerceToNonGeneric()), true));
        }

        public void UnLess(Func<T, bool> predicate, Action action)
        {
            When(x => !predicate(x), action);
        }



      
    }


}
