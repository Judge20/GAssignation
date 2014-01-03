// -----------------------------------------------------------------------
// <copyright file="CollectionAssignerAdapter.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using System.Collections;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CollectionAssignerAdapter : IAssigner
    {
       // readonly IEnumerable _instances;
        public Func<object, bool> Predicate { get; set; }
        readonly IAssigner _assigner;
        public CollectionAssignerAdapter(IAssigner assigner)
        {
           // this._instances = instances;
            this._assigner = assigner;
        }


        public void Set(object instance)
        {
            IEnumerable instances = instance as IEnumerable;
            if (instances == null)
            {
                return;
            }

            var predicate = Predicate ?? (x => true);

            foreach (var obj in instances)
            {
                if (obj == null || !(predicate(obj)))
                {
                    continue;
                }
                _assigner.Set(obj);
            }
        }
    }
}
