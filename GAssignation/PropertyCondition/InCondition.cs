// -----------------------------------------------------------------------
// <copyright file="InCondition.cs" company="Microsoft">
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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InCondition : PropertyCondition, IArrayCondition
    {
        public InCondition(IEnumerable valuesToCompare)
        {
			this.ValuesToCompare = valuesToCompare;
		}

		public override bool IsMatch(PropertyConditionContext context) {
			//var comparisonValue = GetComparisonValue(context);
            if (this.ValuesToCompare == null || !this.ValuesToCompare.Cast<object>().Any())
            {
                return false;
            }

            bool success = false;
            foreach (object comparisonValue in this.ValuesToCompare)
            {
                success = Compare(comparisonValue, context.PropertyValue);
                if (success == true)
                {
                    break;
                }
            }

            return success;
		}
	
		public IEnumerable ValuesToCompare { get; private set; }

		protected bool Compare(object comparisonValue, object propertyValue) {

			if (comparisonValue is IComparable && propertyValue is IComparable) {
				return GAssignation.Comparer.GetEqualsResult((IComparable)comparisonValue, (IComparable)propertyValue);
			}

			return comparisonValue == propertyValue;
		}
    }
}
