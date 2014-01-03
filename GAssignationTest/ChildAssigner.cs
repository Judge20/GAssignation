using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GAssignation;

namespace GAssignationTest
{
    /// <summary>
    /// Summary description for ChildAssigner
    /// </summary>
    [TestClass]
    public class ChildAssigner
    {
        public ChildAssigner()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            TestModel model = new TestModel() { Child = new ChildModel() };
            TestChildTestModelAssigner assigner = new TestChildTestModelAssigner();
            assigner.Set(model);
            Assert.AreEqual(1, model.Child.IntProperty);
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void TestCollection()
        {
            TestModel model = new TestModel() { Children = new List<ChildModel>()};
            model.Children.Add(new ChildModel());
            model.Children.Add(new ChildModel());
            TestChildTestModelAssigner assigner = new TestChildTestModelAssigner();
            assigner.Set(model);
            Assert.AreEqual(1, model.Children[0].IntProperty);
            Assert.AreEqual(1, model.Children[1].IntProperty);
            //
            // TODO: Add test logic here
            //
        }

        public class TestChildTestModelAssigner : AbstractAssigner<TestModel>
        {
            public TestChildTestModelAssigner()
            {
                this.RuleFor(e => e.Child).SetAssigner(new TestChildModelAssigner());
                this.RuleForEach(e => e.Children).SetAssigner(new TestChildModelAssigner());
            }
        }

        public class TestChildModelAssigner : AbstractAssigner<ChildModel>
        {
            public TestChildModelAssigner()
            {
                this.RuleFor(e => e.IntProperty).Assign(1);
                
            }
        }
    }
}
