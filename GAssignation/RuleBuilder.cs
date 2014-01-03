// -----------------------------------------------------------------------
// <copyright file="RuleBuilder.cs" company="Microsoft">
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
    public class RuleBuilder<T, TProperty> : IRuleBuilderInitial<T, TProperty>, IRuleBuilderOptions<T, TProperty>
    {
        public PropertyRule Rule { get; private set; }

        public RuleBuilder(PropertyRule rule)
        {
            Rule = rule;
        }

        public IRuleBuilderOptions<T, TProperty> Assign(TProperty value)
        {
            Rule.Assign(e => value);
            return this;
        }

        public IRuleBuilderOptions<T, TProperty> SetCondition(IPropertyCondition condition)
        {
            Rule.AddCondition(condition);
            return this;
        }


        //public IRuleBuilderOptions<T, TProperty> SetAssigner(IAssigner<TProperty> assigner)
        //{
        //    Rule.SetAssigner(assigner);
        //    //Rule.
        //    return this;
        //}


        public IRuleBuilderOptions<T, TProperty> Assign(Func<T, TProperty> func)
        {
            Rule.Assign(func.CoerceToNonGeneric());
            return this;
        }


        public IRuleBuilderOptions<T, TProperty> SetAssigner(IAssigner assigner)
        {
            Rule.SetAssigner(assigner);
            return this;
        }



        public IRuleBuilderInitial<T, TProperty> Cascade(CascadeMode cascade)
        {
            Rule.CascadeMode = cascade;
            return this;
        }
    }
}
