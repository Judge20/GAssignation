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
    /// Summary description for PropertyRuleTests
    /// </summary>
    [TestClass]
    public class PropertyRuleTests
    {
        public PropertyRuleTests()
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
            string newValue = "bcd";
            PropertyRule rule = PropertyRule.Create<TestModel, string>(e => e.StrProperty);
            rule.Assign(e => newValue);
            TestModel model = new TestModel();
            ConditionContext context = new ConditionContext(model);
            rule.Set(context);
            Assert.AreEqual(model.StrProperty, newValue);
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void TestPredicateCondition()
        {
            string newValue = "bcd";
            PropertyRule rule = PropertyRule.Create<TestModel, string>(e => e.StrProperty);
            Func<TestModel, bool> predicate = e => e.StrProperty != null;
            rule.AddCondition(new PredicateCondition(predicate.CoerceToNonGeneric()));
            rule.Assign(e => newValue);
            TestModel model = new TestModel();
            ConditionContext context = new ConditionContext(model);
            rule.Set(context);
            Assert.AreNotEqual(model.StrProperty, newValue);
        }
    }
}
