using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace GAssignationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Expression<Func<TestClass, string>> exp = e => e.Test;
            // exp.Parameters[0];
            Expression assign = Expression.Assign(exp.Body, Expression.Constant("abc"));
            Expression<Action<TestClass>> lambada = Expression.Lambda<Action<TestClass>>(
                assign,
                new ParameterExpression[] { exp.Parameters[0] }
                );
            Action<TestClass> action = lambada.Compile();
            TestClass test = new TestClass();
            action(test);

            
        //    ParameterExpression p1 = Expression.Parameter(typeof(Type)

            Expression<Action<TestClass, string>> ass = null;
        }

        public class TestClass
        {
            public string Test { get; set; } 
        }
    }
}
