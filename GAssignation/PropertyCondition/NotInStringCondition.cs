﻿// -----------------------------------------------------------------------
// <copyright file="NotInStringCondition.cs" company="Microsoft">
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
    public class NotInStringCondition : PropertyCondition, IStringArrayCondition
    {

        public NotInStringCondition(IEnumerable<string> stringsToCompare, StringComparison comparison = StringComparison.CurrentCulture)
        {
            this.ValuesToCompare = stringsToCompare;
            this._comparison = comparison;
		}

		public override bool IsMatch(PropertyConditionContext context) {
			//var comparisonValue = GetComparisonValue(context);
            if (this.ValuesToCompare == null || !this.ValuesToCompare.Any())
            {
                return true;
            }

            bool success = false;
            foreach (string comparisonValue in this.ValuesToCompare)
            {
                success = string.Equals(context.PropertyValue.ToString(), comparisonValue, this._comparison);
                if (success == true)
                {
                    break;
                }
            }

            return !success;
		}
	
		public IEnumerable<string> ValuesToCompare { get; private set; }

        private StringComparison _comparison;
    }
}
