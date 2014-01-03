// -----------------------------------------------------------------------
// <copyright file="ConditionExtensions.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using GAssignation.PropertyCondition;
    using System.Collections;
    using System.Linq.Expressions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ConditionExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> NotNull<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetCondition(new NotNullCondition());
        }

        public static IRuleBuilderOptions<T, TProperty> Null<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetCondition(new NullCondition());
        }

        public static IRuleBuilderOptions<T, TProperty> NotEmpty<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetCondition(new NotEmptyCondition(default(TProperty)));
        }

        public static IRuleBuilderOptions<T, TProperty> Empty<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetCondition(new EmptyCondition(default(TProperty)));
        }

        public static IRuleBuilderOptions<T, string> Length<T>(this IRuleBuilderOptions<T, string> ruleBuilder, int min, int max)
        {
            return ruleBuilder.SetCondition(new LengthCondition(min, max));
        }

        public static IRuleBuilderOptions<T, string> Length<T>(this IRuleBuilderOptions<T, string> ruleBuilder, int exactLength)
        {
            return ruleBuilder.SetCondition(new ExactLengthCondition(exactLength));
        }

        public static IRuleBuilderOptions<T, string> MinimumLength<T>(this IRuleBuilderOptions<T, string> ruleBuilder, int minLength)
        {
            return ruleBuilder.SetCondition(new MinimumLengthCondition(minLength));
        }

        public static IRuleBuilderOptions<T, TProperty> NotEqual<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                               TProperty toCompare, IEqualityComparer comparer = null)
        {
            return ruleBuilder.SetCondition(new NotEqualCondition(toCompare, comparer));
        }

        public static IRuleBuilderOptions<T, TProperty> NotEqual<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                               Expression<Func<T, TProperty>> expression, IEqualityComparer comparer = null)
        {
            var func = expression.Compile();
            return ruleBuilder.SetCondition(new NotEqualCondition(func.CoerceToNonGeneric(), expression.GetMember(), comparer));
        }

        public static IRuleBuilderOptions<T, TProperty> Equal<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                              TProperty toCompare, IEqualityComparer comparer = null)
        {
            return ruleBuilder.SetCondition(new EqualCondition(toCompare, comparer));
        }

        public static IRuleBuilderOptions<T, TProperty> Equal<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                               Expression<Func<T, TProperty>> expression, IEqualityComparer comparer = null)
        {
            var func = expression.Compile();
            return ruleBuilder.SetCondition(new EqualCondition(func.CoerceToNonGeneric(), expression.GetMember(), comparer));
        }

        public static IRuleBuilderOptions<T, TProperty> Must<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                              Func<T, bool> func, IEqualityComparer comparer = null)
        {
            return ruleBuilder.SetCondition(new PredicateCondition(func.CoerceToNonGeneric()));
        }

        public static IRuleBuilderOptions<T, TProperty> LessThan<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                             TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetCondition(new LessThanCondition(valueToCompare));
        }

        public static IRuleBuilderOptions<T, TProperty> LessThan<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                               Expression<Func<T, TProperty>> expression)
           where TProperty : IComparable<TProperty>, IComparable
        {
            var func = expression.Compile();
            return ruleBuilder.SetCondition(new LessThanCondition(func.CoerceToNonGeneric(), expression.GetMember()));
        }

        //public static IRuleBuilderOptions<T, Nullable<TProperty>> LessThan<T, TProperty>(this IRuleBuilderOptions<T, Nullable<TProperty>> ruleBuilder,
        //                                                                     TProperty valueToCompare)
        //    where TProperty : struct, IComparable<TProperty>, IComparable
        //{
        //    return ruleBuilder.SetCondition(new LessThanCondition(valueToCompare));
        //}

        //public static IRuleBuilderOptions<T, Nullable<TProperty>> LessThan<T, TProperty>(this IRuleBuilderOptions<T, Nullable<TProperty>> ruleBuilder,
        //                                                                        Expression<Func<T, TProperty>> expression)
        //    where TProperty : struct, IComparable<TProperty>, IComparable
        //{
        //    var func = expression.Compile();
        //    return ruleBuilder.SetCondition(new LessThanCondition(func.CoerceToNonGeneric(), expression.GetMember()));
        //}

        public static IRuleBuilderOptions<T, TProperty> LessThanOrEqual<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                          TProperty valueToCompare)
         where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetCondition(new LessThanOrEqualCondition(valueToCompare));
        }

        public static IRuleBuilderOptions<T, TProperty> LessThanOrEqual<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                               Expression<Func<T, TProperty>> expression)
           where TProperty : IComparable<TProperty>, IComparable
        {
            var func = expression.Compile();
            return ruleBuilder.SetCondition(new LessThanOrEqualCondition(func.CoerceToNonGeneric(), expression.GetMember()));
        }

        public static IRuleBuilderOptions<T, TProperty> GreaterThan<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                          TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetCondition(new GreaterThanCondition(valueToCompare));
        }

        public static IRuleBuilderOptions<T, TProperty> GreaterThan<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                               Expression<Func<T, TProperty>> expression)
           where TProperty : IComparable<TProperty>, IComparable
        {
            var func = expression.Compile();
            return ruleBuilder.SetCondition(new GreaterThanCondition(func.CoerceToNonGeneric(), expression.GetMember()));
        }

        public static IRuleBuilderOptions<T, TProperty> GreaterThanOrEqual<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                        TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetCondition(new GreaterThanOrEqualCondition(valueToCompare));
        }

        public static IRuleBuilderOptions<T, TProperty> GreaterThanOrEqual<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                               Expression<Func<T, TProperty>> expression)
           where TProperty : IComparable<TProperty>, IComparable
        {
            var func = expression.Compile();
            return ruleBuilder.SetCondition(new GreaterThanOrEqualCondition(func.CoerceToNonGeneric(), expression.GetMember()));
        }

        public static IRuleBuilderOptions<T, TProperty> ExclusiveBetween<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                        TProperty from, TProperty to)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetCondition(new ExclusiveBetweenCondition(from, to));
        }

        public static IRuleBuilderOptions<T, TProperty> InclusiveBetween<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                        TProperty from, TProperty to)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetCondition(new InclusiveBetweenCondition(from, to));
        }

        public static IRuleBuilderOptions<T, TProperty> ExclusiveOutside<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                       TProperty from, TProperty to)
           where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetCondition(new ExclusiveOutsideCondition(from, to));
        }

        public static IRuleBuilderOptions<T, TProperty> InclusiveOutside<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                        TProperty from, TProperty to)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetCondition(new InclusiveOutsideCondition(from, to));
        }

        public static IRuleBuilderOptions<T, TProperty> In<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                        IEnumerable<TProperty> collection)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetCondition(new InCondition(collection));
        }

        public static IRuleBuilderOptions<T, TProperty> NotIn<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                        IEnumerable<TProperty> collection)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetCondition(new NotInCondition(collection));
        }

        public static IRuleBuilderOptions<T, string> In<T>(this IRuleBuilderOptions<T, string> ruleBuilder,
                    IEnumerable<string> collection, 
                     StringComparison comparison = StringComparison.CurrentCulture)
        {
            return ruleBuilder.SetCondition(new InStringCondition(collection, comparison));
        }

        public static IRuleBuilderOptions<T, string> NotIn<T>(this IRuleBuilderOptions<T, string> ruleBuilder,
                   IEnumerable<string> collection,
                    StringComparison comparison = StringComparison.CurrentCulture)
        {
            return ruleBuilder.SetCondition(new NotInStringCondition(collection, comparison));
        }
    }
}
