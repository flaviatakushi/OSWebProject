using System;
using System.Text.RegularExpressions;

namespace OSWebCore
{
    public delegate string PerformRegexRule();

    public sealed class RegexRule : BaseRule
    {
        private Regex regex;
        private PerformRegexRule performer;

        public RegexRule(string propertyName, string description, string pattern, PerformRegexRule performer)
            : base(propertyName, description)
        {
            this.regex = new Regex(pattern, RegexOptions.Compiled);

            if (performer == null)
                throw new ArgumentNullException("performer");

            this.performer = performer;
        }

        public string Pattern
        {
            get
            {
                return this.regex.ToString();
            }
        }

        public Regex Regex
        {
            get
            {
                return this.regex;
            }
        }

        public PerformRegexRule Performer
        {
            get
            {
                return this.performer;
            }
        }

        public override bool ValidateRule()
        {
            return this.regex.IsMatch(this.performer());
        }
    }
}