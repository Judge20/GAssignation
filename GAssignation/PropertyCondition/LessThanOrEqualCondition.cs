// -----------------------------------------------------------------------
// <copyright file="LessThanOrEqualCondition.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation.PropertyCondition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LessThanOrEqualCondition : AbstractComparisonCondition {
		public LessThanOrEqualCondition(IComparable value) 
            : base(value) 
        {
		}

        public LessThanOrEqualCondition(Func<object, object> valueToCompareFunc, MemberInfo member)
			: base(valueToCompareFunc, member) 
        {
		}

		public override bool IsValid(IComparable value, IComparable valueToCompare) {
			return Comparer.GetComparisonResult(value, valueToCompare) <= 0;
		}

		public override Comparison Comparison {
			get { return Comparison.LessThanOrEqual; }
		}
	}
}
