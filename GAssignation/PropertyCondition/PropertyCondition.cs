// -----------------------------------------------------------------------
// <copyright file="PropertyCondition.cs" company="Microsoft">
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
    public abstract class PropertyCondition : IPropertyCondition
    {
        public abstract bool IsMatch(PropertyConditionContext context);
    }
}
