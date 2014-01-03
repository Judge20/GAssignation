// -----------------------------------------------------------------------
// <copyright file="IRuleBuilder.cs" company="Microsoft">
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
    public interface IRuleBuilder<T, out TProperty>
    {
        //IRuleBuilder<T, TProperty> SetCondition(IPropertyCondition condition);
    }

    public interface IRuleBuilderOptions<T, out TProperty>
    {
        IRuleBuilderOptions<T, TProperty> SetCondition(IPropertyCondition condition);
    }

    public interface IRuleBuilderInitial<T, TProperty> //: IRuleBuilderInitialBase<T, TProperty>
    {
        IRuleBuilderInitial<T, TProperty> Cascade(CascadeMode cascade);

        IRuleBuilderOptions<T, TProperty> Assign(TProperty value);

        IRuleBuilderOptions<T, TProperty> Assign(Func<T, TProperty> func);

      //  IRuleBuilderOptions<T, TProperty> SetAssigner(IAssigner<TProperty> assigner);
        IRuleBuilderOptions<T, TProperty> SetAssigner(IAssigner assigner);
        
    }



   


}
