using System;
using OSWebCore.Properties;

namespace OSWebCore
{
    public abstract class BaseRule
    {
        private string propertyName;
        private string description;

        protected BaseRule(string propertyName, string description)
        {
            this.PropertyName = propertyName;
            this.Description = description;
        }

        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
            protected set
            {
                value = (value ?? string.Empty).Trim();

                if (value.Length == 0)
                    throw new ArgumentException(Resources.BaseRuleInvalidPropertyName, "value");

                this.propertyName = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            protected set
            {
                value = (value ?? string.Empty).Trim();

                if (value.Length == 0)
                    throw new ArgumentException(Resources.BaseRuleInvalidDescription, "value");

                this.description = value;
            }
        }

        public abstract bool ValidateRule();

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return "Property Name: " + this.propertyName ?? string.Empty + Environment.NewLine + "Description: " + this.description ?? string.Empty;
        }
    }
}