// -----------------------------------------------------------------------
// <copyright file="DefaultOptions.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class DefaultOptions
    {
        public static CascadeMode CascadeMode = CascadeMode.And;

        private static Func<Type, MemberInfo, LambdaExpression, string> propertyNameResolver = DefaultPropertyNameResolver;
        public static Func<Type, MemberInfo, LambdaExpression, string> PropertyNameResolver
        {
            get { return propertyNameResolver; }
            set { propertyNameResolver = value ?? DefaultPropertyNameResolver; }
        }

        static string DefaultPropertyNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
        {
            if (expression != null)
            {
                var chain = PropertyChain.FromExpression(expression);
                if (chain.Count > 0) return chain.ToString();
            }

            if (memberInfo != null)
            {
                return memberInfo.Name;
            }

            return null;
        }
    }
}
