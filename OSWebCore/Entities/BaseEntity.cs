using System;
using System.Collections.ObjectModel;

namespace OSWebCore
{
    public abstract class BaseEntity : RulableObject<BaseEntity>
    {
        protected static string CleanString(string value)
        {
            if (value == null)
                return null;

            value = value.Trim();

            return value.Length == 0 ? null : value;
        }

        protected override abstract Collection<BaseRule> CreateRules();
    }
}