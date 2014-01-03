// -----------------------------------------------------------------------
// <copyright file="EqualCondition.cs" company="Microsoft">
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
    public class EqualCondition : PropertyCondition, IComparisonCondition
    {
        readonly Func<object, object> _func;
        readonly IEqualityComparer _comparer;

        public EqualCondition(object valueToCompare)
        {
			this.ValueToCompare = valueToCompare;
		}

		public EqualCondition(object valueToCompare, IEqualityComparer comparer)
        {
			ValueToCompare = valueToCompare;
			this._comparer = comparer;
		}

		public EqualCondition(Func<object, object> comparisonProperty, MemberInfo member)
			
        {
			_func = comparisonProperty;
			MemberToCompare = member;
		}

		public EqualCondition(Func<object, object> comparisonProperty, MemberInfo member, IEqualityComparer comparer)
			
        {
			_func = comparisonProperty;
			MemberToCompare = member;
			this._comparer = comparer;
		}

		public override bool IsMatch(PropertyConditionContext context) {
			var comparisonValue = GetComparisonValue(context);
			bool success = Compare(comparisonValue, context.PropertyValue);

			if (!success) {
				//context.MessageFormatter.AppendArgument("ComparisonValue", comparisonValue);
				return false;
			}

			return true;
		}

		private object GetComparisonValue(PropertyConditionContext context) {
			if(_func != null) {
				return _func(context.Instance);
			}

			return ValueToCompare;
		}

		public Comparison Comparison {
			get { return Comparison.Equal; }
		}

		public MemberInfo MemberToCompare { get; private set; }
		public object ValueToCompare { get; private set; }

		protected bool Compare(object comparisonValue, object propertyValue) {
			if(_comparer != null) {
				return _comparer.Equals(comparisonValue, propertyValue);
			}

			if (comparisonValue is IComparable && propertyValue is IComparable) {
				return GAssignation.Comparer.GetEqualsResult((IComparable)comparisonValue, (IComparable)propertyValue);
			}

			return comparisonValue == propertyValue;
		}

     
    }
}
