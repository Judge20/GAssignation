// -----------------------------------------------------------------------
// <copyright file="CollectionAssignerExtensions.cs" company="Microsoft">
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
    //public static class CollectionAssignerExtensions
    //{
    //    public static ICollectionAssignerRuleBuilder<T, TCollectionElement> SetCollectionValidator<T, TCollectionElement>(this IRuleBuilderInitial<T, IEnumerable<TCollectionElement>> ruleBuilder, IAssigner<TCollectionElement> assigner)
    //    {
    //        var adaptor = new CollectionAssignerAdapter(assigner);
    //        var ruleOptions = ruleBuilder.SetAssigner(adaptor);
    //        return new CollectionAssignerRuleBuilder<T, TCollectionElement>(ruleOptions, adaptor);
    //    }
    //}

    public interface ICollectionRuleBuilderInitial<T, TCollectionElement>
    {
        ICollectionRuleBuilderOptions<T, TCollectionElement> SetCollectionAssigner(IAssigner assigner);
    }

    public interface ICollectionRuleBuilderOptions<T,TCollectionElement> : IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> {
			ICollectionRuleBuilderOptions<T,TCollectionElement> Where(Func<TCollectionElement, bool> predicate);
		}

    public class CollectionRuleBuilder<T, TCollectionElement> :  ICollectionRuleBuilderInitial<T, TCollectionElement>, ICollectionRuleBuilderOptions<T, TCollectionElement>
    {

        public PropertyRule Rule { get; private set; }
        //publi

        public CollectionRuleBuilder(PropertyRule rule)
        {
            Rule = rule;
        }

        //IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> _ruleBuilder;
        CollectionAssignerAdapter _adaptor;

        //public CollectionRuleBuilder(IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> ruleBuilder, CollectionAssignerAdapter adapter)
        //{
        //    this._ruleBuilder = ruleBuilder;
        //    this._adaptor = adapter;
        //}

        public ICollectionRuleBuilderOptions<T, TCollectionElement> Where(Func<TCollectionElement, bool> predicate)
        {
            if (_adaptor != null)
            {
                _adaptor.Predicate = x => predicate((TCollectionElement)x);
            }
            return this;
        }

        public IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> SetCondition(IPropertyCondition condition)
        {
            Rule.AddCondition(condition);
            return this;
           // return Rule.AddCondition(condition);
        }

        public ICollectionRuleBuilderOptions<T, TCollectionElement> SetCollectionAssigner(IAssigner assigner)
        {
            var adaptor = new CollectionAssignerAdapter(assigner);
            Rule.SetAssigner(adaptor);
            return this;
        }
    }

}
