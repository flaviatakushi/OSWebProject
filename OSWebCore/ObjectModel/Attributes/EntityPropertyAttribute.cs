using System;

namespace OSWebCore
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class EntityPropertyAttribute : Attribute
    {
        private bool isEntityProperty;

        public EntityPropertyAttribute(bool isEntityProperty)
        {
            this.isEntityProperty = isEntityProperty;
        }

        public bool IsEntityProperty
        {
            get
            {
                return this.isEntityProperty;
            }
        }
    }
}