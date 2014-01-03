// -----------------------------------------------------------------------
// <copyright file="EmptyCondition.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation.PropertyCondition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EmptyCondition: PropertyCondition, IEmptyCondition {
		readonly object defaultValueForType;

        public EmptyCondition(object defaultValueForType)
            : base()
        {
			this.defaultValueForType = defaultValueForType;
		}

		public override bool IsMatch(PropertyConditionContext context) {
			if (context.PropertyValue == null
			    || IsInvalidString(context.PropertyValue)
			    || IsEmptyCollection(context.PropertyValue)
			    || Equals(context.PropertyValue, defaultValueForType)) {
				return true;
			}

			return false;
		}

		bool IsEmptyCollection(object propertyValue) {
			var collection = propertyValue as IEnumerable;
		    return collection != null && !collection.Cast<object>().Any();
		}

		bool IsInvalidString(object value) {
			if (value is string) {
				return IsNullOrWhiteSpace(value as string);
			}
			return false;
		}

		bool IsNullOrWhiteSpace(string value) {
			if (value != null) {
				for (int i = 0; i < value.Length; i++) {
					if (!char.IsWhiteSpace(value[i])) {
						return false;
					}
				}
			}
			return true;
		}
	}

	public interface IEmptyCondition : IPropertyCondition {
	}
   
}
