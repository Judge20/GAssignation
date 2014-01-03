// -----------------------------------------------------------------------
// <copyright file="IDefault.cs" company="Microsoft">
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
    public interface IAssigner<in T> : IAssigner
    {
        void Set(T instance);
    }

    public interface IAssigner
    {
        void Set(object instance);
    }
}
