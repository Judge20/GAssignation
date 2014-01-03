// -----------------------------------------------------------------------
// <copyright file="LengthCondition.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation.PropertyCondition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Linq.Expressions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LengthCondition : PropertyCondition, ILengthCondition
    {
        public int Min { get; private set; }
        public int Max { get; private set; }


        public LengthCondition(int min, int max)
        {
            Max = max;
            Min = min;

            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }
        }

        public override bool IsMatch(PropertyConditionContext context)
        {
            if (context.PropertyValue == null) return false;

            int length = context.PropertyValue.ToString().Length;

            if (length < Min || (length > Max && Max != -1))
            {
                return false;
            }

            return true;
        }
    }

    public class ExactLengthCondition : LengthCondition
    {
        public ExactLengthCondition(int length)
            : base(length, length)
        {

        }
    }

    public class MaximumLengthCondition : LengthCondition
    {

        public MaximumLengthCondition(int max)
            : base(0, max)
        {

        }
    }

    public class MinimumLengthCondition : LengthCondition
    {

        public MinimumLengthCondition(int min)
            : base(min, -1)
        {

        }
    }

    public interface ILengthCondition : IPropertyCondition
    {
        int Min { get; }
        int Max { get; }
    }
}
