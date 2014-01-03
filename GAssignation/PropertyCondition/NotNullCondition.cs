// -----------------------------------------------------------------------
// <copyright file="NotNullCondition.cs" company="Microsoft">
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
    public class NotNullCondition : PropertyCondition, INotNullCondition {
        public NotNullCondition() 
            : base() {
		}

		public override bool IsMatch(PropertyConditionContext context) {
			if (context.PropertyValue == null) {
				return false;
			}
			return true;
		}
	}

	public interface INotNullCondition : IPropertyCondition {
	}
}
