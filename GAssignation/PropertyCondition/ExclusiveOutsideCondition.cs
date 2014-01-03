// -----------------------------------------------------------------------
// <copyright file="ExclusiveOutsideCondition.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation.PropertyCondition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ExclusiveOutsideCondition : AbstractRangeCondition
    {
        public ExclusiveOutsideCondition(IComparable from, IComparable to)
            : base(from, to)
        {

        }

        protected override bool IsValid(IComparable value, IComparable from, IComparable to)
        {
            if (Comparer.GetComparisonResult(value, From) >= 0 && Comparer.GetComparisonResult(value, To) <= 0)
            {
                return false;
            }
            return true;
        }

    }
}
