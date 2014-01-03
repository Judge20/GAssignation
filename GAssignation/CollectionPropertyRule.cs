// -----------------------------------------------------------------------
// <copyright file="CollectionPropertyRule.cs" company="Microsoft">
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
    public class CollectionPropertyRule<TProperty> : PropertyRule
    {
        public CollectionPropertyRule(MemberInfo member, Func<object, object> propertyFunc, LambdaExpression expression, Func<CascadeMode> cascadeModeThunk, Type typeToValidate, Type containerType) 
            : base(member, propertyFunc, null, expression, cascadeModeThunk, typeToValidate, containerType) {
		}

		/// <summary>
		/// Creates a new property rule from a lambda expression.
		/// </summary>
		public new static CollectionPropertyRule<TProperty> Create<T>(Expression<Func<T, IEnumerable<TProperty>>> expression, Func<CascadeMode> cascadeModeThunk) {
			var member = expression.GetMember();
			var compiled = expression.Compile();

			return new CollectionPropertyRule<TProperty>(member, compiled.CoerceToNonGeneric(), expression, cascadeModeThunk, typeof(TProperty), typeof(T));
		}

        public override void Set(ConditionContext context)
        {
 	         var propertyConditionContext = new PropertyConditionContext(context, this, this.PropertyName);
             var collectionPropertyValue = propertyConditionContext.PropertyValue as IEnumerable<TProperty>;
             if (collectionPropertyValue != null)
             {
                 foreach (var element in collectionPropertyValue) 
                 {
					if (this.IsMatch(new ConditionContext(element)))
                    {
                        if (this.Assigner != null)
                        {
                            this.Assigner.Set(element);
                        }
                    }
				 }
             }
        }
    }
}
