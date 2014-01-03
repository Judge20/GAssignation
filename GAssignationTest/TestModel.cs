// -----------------------------------------------------------------------
// <copyright file="TestModel.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignationTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestModel
    {
        public string StrProperty { get; set; }
        public int IntProperty { get; set; }
        public int? NullableProperty { get; set; }

        public ChildModel Child { get; set; }
        public List<ChildModel> Children { get; set; }
    }

    public class ChildModel
    {
        public int IntProperty { get; set; }
        public int StrProperty { get; set; }
    }

    
}
