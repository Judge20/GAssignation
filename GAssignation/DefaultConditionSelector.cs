// -----------------------------------------------------------------------
// <copyright file="DefaultValidatorSelector.cs" company="Microsoft">
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
    public class DefaultConditionSelector: IConditionSelector {
		/// <summary>
		/// Determines whether or not a rule should execute.
		/// </summary>
		/// <param name="rule">The rule</param>
		/// <param name="propertyPath">Property path (eg Customer.Address.Line1)</param>
		/// <param name="context">Contextual information</param>
		/// <returns>Whether or not the validator can execute.</returns>
		public bool CanExecute(IPropertyRule rule, string propertyPath, ConditionContext context) {
			// By default we ignore any rules part of a RuleSet.
            // TODO
			//if (!string.IsNullOrEmpty(rule.RuleSet)) return false;

			return true;
		}
    
    }
}
