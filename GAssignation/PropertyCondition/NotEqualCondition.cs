// -----------------------------------------------------------------------
// <copyright file="NotEqualCondition.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation.PropertyCondition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using System.Collections;
using System.Reflection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NotEqualCondition : PropertyCondition, IComparisonCondition {
		readonly IEqualityComparer comparer;
		readonly Func<object, object> func;

		public NotEqualCondition(Func<object, object> func, MemberInfo memberToCompare)
        {
			this.func = func;
			MemberToCompare = memberToCompare;
		}

		public NotEqualCondition(Func<object, object> func, MemberInfo memberToCompare, IEqualityComparer equalityComparer)
        {
			this.func = func;
			this.comparer = equalityComparer;
			MemberToCompare = memberToCompare;
		}

		public NotEqualCondition(object comparisonValue)	
        {
			ValueToCompare = comparisonValue;
		}

        public NotEqualCondition(object comparisonValue, IEqualityComparer equalityComparer)
        {
			ValueToCompare = comparisonValue;
			comparer = equalityComparer;
		}

		public override bool IsMatch(PropertyConditionContext context) {
			var comparisonValue = GetComparisonValue(context);
			bool success = !Compare(comparisonValue, context.PropertyValue);

			if (!success) {
				//context.MessageFormatter.AppendArgument("ComparisonValue", comparisonValue);
				return false;
			}

			return true;
		}

		private object GetComparisonValue(PropertyConditionContext context) {
			if (func != null) {
				return func(context.Instance);
			}

			return ValueToCompare;
		}

		public Comparison Comparison {
			get { return Comparison.NotEqual; }
		}

		public MemberInfo MemberToCompare { get; private set; }
		public object ValueToCompare { get; private set; }

		protected bool Compare(object comparisonValue, object propertyValue) {
			if(comparer != null) {
				return comparer.Equals(comparisonValue, propertyValue);
			}

			if (comparisonValue is IComparable && propertyValue is IComparable) {
                return GAssignation.Comparer.GetEqualsResult((IComparable)comparisonValue, (IComparable)propertyValue);
			}

			return comparisonValue == propertyValue;
		}
	}
}
