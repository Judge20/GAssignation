// -----------------------------------------------------------------------
// <copyright file="ConditionContext.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class ConditionContext<T> : ConditionContext
    {
        public ConditionContext(T instanceToValidate)
            : this(instanceToValidate, new PropertyChain(), new DefaultConditionSelector())
        {

        }

        public ConditionContext(T instanceToValidate, PropertyChain propertyChain, IConditionSelector validatorSelector)
            : base(instanceToValidate, propertyChain, validatorSelector)
        {

            InstanceToCondition = instanceToValidate;
        }

        public new T InstanceToCondition { get; private set; }
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ConditionContext
    {
       public ConditionContext(object instanceToValidate)
		 : this (instanceToValidate, new PropertyChain(), new DefaultConditionSelector()){
			
		}

       public ConditionContext(object instanceToValidate, PropertyChain propertyChain, IConditionSelector validatorSelector)
       {
			PropertyChain = new PropertyChain(propertyChain);
			InstanceToValidate = instanceToValidate;
			Selector = validatorSelector;
		}

		public PropertyChain PropertyChain { get; private set; }
		public object InstanceToValidate { get; private set; }
        public IConditionSelector Selector { get; private set; }
		public bool IsChildContext { get; internal set; }

        public ConditionContext Clone(PropertyChain chain = null, object instanceToValidate = null, IConditionSelector selector = null)
        {
            return new ConditionContext(instanceToValidate ?? this.InstanceToValidate, chain ?? this.PropertyChain, selector ?? this.Selector);
		}

        internal ConditionContext CloneForChildValidator(object instanceToValidate)
        {
            return new ConditionContext(instanceToValidate, PropertyChain, Selector)
            {
				IsChildContext = true
			};
		}
    }
}
