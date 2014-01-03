// -----------------------------------------------------------------------
// <copyright file="IPropertyCondition.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IPropertyCondition
    {
        bool IsMatch(PropertyConditionContext context);
    }
}
