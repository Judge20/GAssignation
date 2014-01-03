// -----------------------------------------------------------------------
// <copyright file="PredicateCondition.cs" company="Microsoft">
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
    public class PredicateCondition : IPropertyCondition
    {
        private readonly Func<object, bool> _predicate;

        public PredicateCondition(Func<object, bool> predicate)
        {
            this._predicate = predicate;
        }

        public bool IsMatch(PropertyConditionContext context)
        {
            return this._predicate(context.Instance);
        }
    }
}
