using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using OSWebCore.Properties;

namespace OSWebCore
{
    public abstract class RulableObject<T> : IDataErrorInfo where T : RulableObject<T>
    {
        private Collection<BaseRule> rules;
        private Dictionary<PropertyInfo, string> propertyErrors;

        protected RulableObject()
        {
            var propertyInfos = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x =>
            {
                if (x.CanRead && x.CanWrite)
                {
                    object[] attributes = x.GetCustomAttributes(typeof(EntityPropertyAttribute), false);

                    if (attributes != null && attributes.Length > 0)
                        return ((EntityPropertyAttribute)attributes[0]).IsEntityProperty;
                }

                return false;
            });

            int count = propertyInfos.Count();

            this.propertyErrors = new Dictionary<PropertyInfo, string>(count);

            foreach (var property in propertyInfos)
                this.propertyErrors.Add(property, null);
        }

        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                return this.Error == null;
            }
        }

        [Browsable(false)]
        public string Error
        {
            get
            {
                string result = this[string.Empty];

                if (result != null && result.Length == 0)
                    result = null;

                return result;
            }
        }

        [Browsable(false)]
        public string this[string propertyName]
        {
            get
            {
                if (propertyName == null)
                    throw new ArgumentNullException("propertyName");

                if (propertyName.Length > 0)
                {
                    var key = this.propertyErrors.Keys.FirstOrDefault(x => x.Name == propertyName);

                    return key != null ? this.propertyErrors[key] : null;
                }
                else
                {
                    string messageError = string.Empty;

                    foreach (var value in this.propertyErrors.Values)
                    {
                        if (!string.IsNullOrEmpty(value))
                            messageError += value + Environment.NewLine;
                    }

                    return messageError.Trim();
                }
            }
        }

        public ReadOnlyCollection<BaseRule> GetBrokenRules(params string[] propertyNames)
        {
            if (propertyNames == null || propertyNames.Contains(null))
                throw new ArgumentNullException("propertyNames");

            if (this.rules == null)
            {
                this.rules = this.CreateRules();

                if (this.rules == null)
                    throw new InvalidOperationException(Resources.RuleObjectRulesNotCreated);
            }

            var brokenRules = this.rules.Where(x =>
            {
                if (propertyNames.Contains(x.PropertyName) || propertyNames.Length == 0)
                    return !x.ValidateRule();

                return false;
            });

            return brokenRules.ToList().AsReadOnly();
        }

        protected abstract Collection<BaseRule> CreateRules();
    }
}