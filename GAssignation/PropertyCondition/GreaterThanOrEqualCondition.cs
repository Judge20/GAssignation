// -----------------------------------------------------------------------
// <copyright file="GreaterThanOrEqualCondition.cs" company="Microsoft">
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
    public class GreaterThanOrEqualCondition: AbstractComparisonCondition  {
		public GreaterThanOrEqualCondition(IComparable value) 
            : base(value) {
		}

        public GreaterThanOrEqualCondition(Func<object, object> valueToCompareFunc, MemberInfo member)
			: base(valueToCompareFunc, member) {
		}

		public override bool IsValid(IComparable value, IComparable valueToCompare) {
			return Comparer.GetComparisonResult(value, valueToCompare) >= 0;
		}

		public override Comparison Comparison {
			get { return Comparison.GreaterThanOrEqual; }
		}
	}
    
}
