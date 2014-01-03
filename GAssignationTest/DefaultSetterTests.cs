using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GAssignation;

namespace GAssignationTest
{
    /// <summary>
    /// Summary description for DefaultSetterTests
    /// </summary>
    [TestClass]
    public class DefaultSetterTests
    {
        public DefaultSetterTests()
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
            TestDefaultSetter setter = new TestDefaultSetter();
            TestModel model = new TestModel();
            setter.Set(model);
            Assert.AreEqual(model.StrProperty, "aaa");
          //  Assert.AreEqual(model.NullableProperty, 1);
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void TestWhen()
        {
            TestDefaultSetter setter = new TestDefaultSetter();
            TestModel model = new TestModel() { StrProperty = "bcd" };
            setter.Set(model);
            Assert.AreEqual(model.IntProperty, 1);

            model = new TestModel() { StrProperty = "cde" };
            setter.Set(model);
            Assert.AreNotEqual(model.IntProperty, 1);

            // 
        }

        [TestMethod]
        public void TestUnless()
        {
            TestDefaultSetter setter = new TestDefaultSetter();
            TestModel model = new TestModel() { StrProperty = "cde" };
            setter.Set(model);
            Assert.AreEqual(model.NullableProperty, 1);

            model = new TestModel() { StrProperty = "bcd" };
            setter.Set(model);
            Assert.AreNotEqual(model.NullableProperty, 1);

            // 
        }

        

        public class TestDefaultSetter : AbstractAssigner<TestModel>
        {
            public TestDefaultSetter()
            {
                
                When(e => e.StrProperty == "bcd", () => {
                    RuleFor(e => e.IntProperty).Assign(1);
                });
                UnLess(e => e.StrProperty == "bcd", () => {
                    RuleFor(e => e.NullableProperty).Assign(1);
                    
                });
                RuleFor(e => e.StrProperty).Assign("aaa");
            }
        }
    }
}
