using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Common.Extensions
{
    public class RequiredLocalizedAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        private string _displayName;

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid
        (object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            _displayName = validationContext.DisplayName;
            return base.IsValid(value, validationContext);
        }

        public override string FormatErrorMessage(string name)
        {
            var msg = LanguageService.Instance.Translate(ErrorMessageResourceName, ErrorMessageResourceType);
            return string.Format(msg, _displayName);
        }
    }

    public class RegularExpressionLocalizedAttribute : System.ComponentModel.DataAnnotations.RegularExpressionAttribute
    {
        private string _displayName;

        public RegularExpressionLocalizedAttribute(string pattern)
            : base(pattern) { }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid
        (object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            _displayName = validationContext.DisplayName;
            return base.IsValid(value, validationContext);
        }

        public override string FormatErrorMessage(string name)
        {
            var msg = LanguageService.Instance.Translate(ErrorMessageResourceName, ErrorMessageResourceType);
            return string.Format(msg, _displayName);
        }
    }

    public class StringLengthLocalizedAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute
    {
        private string _displayName;

        public StringLengthLocalizedAttribute(int maximumLength)
            : base(maximumLength) { }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid
        (object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            _displayName = validationContext.DisplayName;
            return base.IsValid(value, validationContext);
        }

        public override string FormatErrorMessage(string name)
        {
            var msg = LanguageService.Instance.Translate(ErrorMessageResourceName, ErrorMessageResourceType);
            return string.Format(msg, _displayName);
        }
    }

    public class CompareLocalizedAttribute : System.ComponentModel.DataAnnotations.CompareAttribute
    {
        private string _displayName;

        public CompareLocalizedAttribute(string otherProperty)
            : base(otherProperty) { }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid
        (object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            _displayName = validationContext.DisplayName;
            return base.IsValid(value, validationContext);
        }

        public override string FormatErrorMessage(string name)
        {
            var msg = LanguageService.Instance.Translate(ErrorMessageResourceName, ErrorMessageResourceType);
            return string.Format(msg, _displayName);
        }
    }

    public class ValidPasswordLocalizedAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute
    {
        private string _displayName;

        public ValidPasswordLocalizedAttribute(int maximumLength)
            : base(maximumLength) { }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid
        (object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            _displayName = validationContext.DisplayName;

            string strValue = (value as string) ?? string.Empty;

            if (strValue.Length < MinimumLength)
                return new System.ComponentModel.DataAnnotations.ValidationResult(FormatErrorMessage(_displayName));

            int smallLettersCount = 0, capitalLettersCount = 0, digitsCount = 0, symbolsCount = 0;

            foreach (char c in strValue)
            {
                if (Char.IsLower(c))
                    smallLettersCount++;
                else if (Char.IsUpper(c))
                    capitalLettersCount++;
                else if (Char.IsDigit(c))
                    digitsCount++;
                else if (!Char.IsWhiteSpace(c))
                    symbolsCount++;
            }

            int[] groups = new int[] { smallLettersCount, capitalLettersCount, digitsCount, symbolsCount };

            int validGroupsCount = groups.Count(g => g >= 2);

            if (validGroupsCount < 2)
                return new System.ComponentModel.DataAnnotations.ValidationResult(FormatErrorMessage(_displayName));

            return System.ComponentModel.DataAnnotations.ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            var msg = LanguageService.Instance.Translate(ErrorMessageResourceName, ErrorMessageResourceType);
            return string.Format(msg, _displayName);
        }
    }

    public class LanguageService
    {

        private static LanguageService _instance = new LanguageService();

        private List<System.Resources.ResourceManager> _resourceManagers = new List<System.Resources.ResourceManager>();

        private LanguageService()
        {
        }

        public static LanguageService Instance { get { return _instance; } }

        public string Translate(string key, Type resourceType)
        {
            var resourceManager = new System.Resources
                .ResourceManager(resourceType.FullName, resourceType.Assembly);

            if (!_resourceManagers.Contains(resourceManager))
                _resourceManagers.Add(resourceManager);

            foreach (var item in _resourceManagers)
            {
                var value = item.GetString(key, Localization.SystemLocalization.Culture);
                if (value != null)
                    return value;
            }

            return null;
        }
    }
}