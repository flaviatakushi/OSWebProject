using System;

namespace OSWebCore
{
    public delegate bool PerformSimpleRule();

    public sealed class SimpleRule : BaseRule
    {
        private PerformSimpleRule performer;

        public SimpleRule(string propertyName, string description, PerformSimpleRule performer) 
            : base(propertyName, description)
        {
            if (performer == null)
                throw new ArgumentNullException("performer");

            this.performer = performer;
        }

        public PerformSimpleRule Performer
        {
            get
            {
                return this.performer;
            }
        }

        public override bool ValidateRule()
        {
            return this.performer();
        }
    }
}