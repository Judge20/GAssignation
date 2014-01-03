using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GAssignation;
using GAssignation.PropertyCondition;

namespace GAssignationTest
{
    /// <summary>
    /// Summary description for RangeConditionTest
    /// </summary>
    [TestClass]
    public class RangeConditionTest
    {
        public RangeConditionTest()
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
        public void TestBetween()
        {
            TestModel model = new TestModel()
            {
                IntProperty = 3,
                StrProperty = "2"
            };
            TestBetweenAssigner assigner = new TestBetweenAssigner();
            assigner.Set(model);
            Assert.AreEqual("between", model.StrProperty);
            Assert.AreEqual(8, model.IntProperty);
            Assert.IsNull(model.NullableProperty);
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void TestOutside()
        {
            TestModel model = new TestModel()
            {
                IntProperty = 3,
                StrProperty = "8"
            };
            TestOutAssigner assigner = new TestOutAssigner();
            assigner.Set(model);
            Assert.AreEqual("outside", model.StrProperty);
            Assert.AreEqual(2, model.IntProperty);
            Assert.IsNull(model.NullableProperty);
            //
            // TODO: Add test logic here
            //
        }

        public class TestBetweenAssigner : AbstractAssigner<TestModel>
        {
            public TestBetweenAssigner()
            {
                this.RuleFor(e => e.StrProperty).Assign("between").SetCondition(new ExclusiveBetweenCondition("1", "3"));
                this.RuleFor(e => e.IntProperty).Assign(8).SetCondition(new InclusiveBetweenCondition(1, 3));
                this.RuleFor(e => e.NullableProperty).Assign(10).SetCondition(new InclusiveBetweenCondition(1, 8));
            }
        }

        public class TestOutAssigner : AbstractAssigner<TestModel>
        {
            public TestOutAssigner()
            {
                this.RuleFor(e => e.StrProperty).Assign("outside").SetCondition(new ExclusiveOutsideCondition("1", "3"));
                this.RuleFor(e => e.IntProperty).Assign(2).SetCondition(new InclusiveOutsideCondition(1, 3));
                this.RuleFor(e => e.NullableProperty).Assign(8).SetCondition(new InclusiveOutsideCondition(1, 8));
            }
        }
    }
}
