// -----------------------------------------------------------------------
// <copyright file="AbstractComparisonCondition.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation.PropertyCondition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using System.Reflection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class AbstractComparisonCondition : PropertyCondition, IComparisonCondition
    {
        readonly Func<object, object> _valueToCompareFunc;
        protected AbstractComparisonCondition(IComparable value)
        {
            value.Guard("value must not be null");
            ValueToCompare = value;
        }

        protected AbstractComparisonCondition(Func<object, object> valueToCompareFunc, MemberInfo member)
        {
            this._valueToCompareFunc = valueToCompareFunc;
            this.MemberToCompare = member;
        }

        public sealed override bool IsMatch(PropertyConditionContext context)
        {
            if (context.PropertyValue == null)
            {
                return false;
            }

            var value = GetComparisonValue(context);

            if (!IsValid((IComparable)context.PropertyValue, value))
            {
                return false;
            }

            return true;
        }

        private IComparable GetComparisonValue(PropertyConditionContext context)
        {
            if (_valueToCompareFunc != null)
            {
                return (IComparable)_valueToCompareFunc(context.Instance);
            }

            return (IComparable)ValueToCompare;
        }

        public abstract bool IsValid(IComparable value, IComparable valueToCompare);

        public abstract Comparison Comparison
        {
            get;
        }

        public MemberInfo MemberToCompare
        {
            get;
            private set;
        }

        public object ValueToCompare
        {
            get;
            private set;
        }
}

    public interface IComparisonCondition : IPropertyCondition
    {
        Comparison Comparison { get; }
        MemberInfo MemberToCompare { get; }
        object ValueToCompare { get; }
    }

    public enum Comparison { 
        Equal,
        NotEqual,
        LessThan,
        GreaterThan,
        GreaterThanOrEqual,
        LessThanOrEqual
    }
}
