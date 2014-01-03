// -----------------------------------------------------------------------
// <copyright file="AbstractRangeCondition.cs" company="Microsoft">
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
    public abstract class AbstractRangeCondition : PropertyCondition, IRangeCondition
    {
        public AbstractRangeCondition(IComparable from, IComparable to) 
        {
			To = to;
			From = from;

			if (Comparer.GetComparisonResult(to, from) == -1) {
				throw new ArgumentOutOfRangeException("to", "To should be larger than from.");
			}
		}

		public IComparable From { get; private set; }
		public IComparable To { get; private set; }


		public override bool IsMatch(PropertyConditionContext context) {
			var propertyValue = (IComparable)context.PropertyValue;

			// If the value is null then we abort and assume success.
			// This should not be a failure condition - only a NotNull/NotEmpty should cause a null to fail.
			if (propertyValue == null) return false;

            return IsValid(propertyValue, From, To);
		}

        protected abstract bool IsValid(IComparable value, IComparable from, IComparable to);

    }


    public interface IRangeCondition : IPropertyCondition
    {
        IComparable From { get; }
        IComparable To { get; }
    }
}
