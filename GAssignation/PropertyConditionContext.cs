// -----------------------------------------------------------------------
// <copyright file="PropertyConditionContext.cs" company="Microsoft">
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
    public class PropertyConditionContext
    {
       // private readonly MessageFormatter messageFormatter = new MessageFormatter();
		private bool propertyValueSet;
		private object propertyValue;

		public ConditionContext ParentContext { get; private set; }
		public PropertyRule Rule { get; private set; }
		public string PropertyName { get; private set; }
		
		public string PropertyDescription {
            get { return Rule.PropertyName; } 
		}

		public object Instance {
			get { return ParentContext.InstanceToValidate; }
		}

        //public MessageFormatter MessageFormatter {
        //    get { return messageFormatter; }
        //}

		//Lazily load the property value
		//to allow the delegating validator to cancel validation before value is obtained
		public object PropertyValue {
			get {
				if (!propertyValueSet) {
					propertyValue = Rule.PropertyFunc(Instance);
					propertyValueSet = true;
				}

				return propertyValue;
			}
			set {
				propertyValue = value;
				propertyValueSet = true;
			}
		}

        public PropertyConditionContext(ConditionContext parentContext, PropertyRule rule, string propertyName)
        {
			ParentContext = parentContext;
			Rule = rule;
			PropertyName = propertyName;
		}
    }
}
