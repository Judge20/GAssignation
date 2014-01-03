// -----------------------------------------------------------------------
// <copyright file="IConditionRule.cs" company="Microsoft">
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
    public interface IPropertyRule
    {
       // IEnumerable<IPropertyCondition> Conditions { get; }

       // bool IsMatch(ConditionContext context);

        void Set(ConditionContext context);

        void AddCondition(IPropertyCondition condition);
        void AddCondition(IPropertyCondition condition, bool isTopLevel);
    }
}
