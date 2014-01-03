// -----------------------------------------------------------------------
// <copyright file="NullCondition.cs" company="Microsoft">
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
    public class NullCondition : PropertyCondition, INullValidator {
        public NullCondition()
            : base()
        {
		}

		public override bool IsMatch(PropertyConditionContext context) {
			if (context.PropertyValue == null) {
				return true;
			}
			return false;
		}
	}

	public interface INullValidator : IPropertyCondition {
	}
}
