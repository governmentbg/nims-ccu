using System.ComponentModel.DataAnnotations;
using Eumis.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Models.Account
{
    public class LoginVM
    {
        [RequiredLocalized(ErrorMessageResourceName = "ValidationUserRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [StringLengthLocalized(100, ErrorMessageResourceName = "ValidationEmailLength", ErrorMessageResourceType = typeof(Resources.Account))]
        [RegularExpressionLocalized(Constants.EMAIL_REGEX, ErrorMessageResourceName = "ValidationUserInvalid", ErrorMessageResourceType = typeof(Resources.Account))]
        public string Email { get; set; }
        [RequiredLocalized(ErrorMessageResourceName = "ValidationActivationPasswordRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [StringLengthLocalized(100, ErrorMessageResourceName = "ValidationPasswordLength", ErrorMessageResourceType = typeof(Resources.Account))]
        public string Password { get; set; }
    }

    public class AccessCodeLoginVM
    {
        [RequiredLocalized(ErrorMessageResourceName = "ValidationUserRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [StringLengthLocalized(100, ErrorMessageResourceName = "ValidationEmailLength", ErrorMessageResourceType = typeof(Resources.Account))]
        [RegularExpressionLocalized(Constants.EMAIL_REGEX, ErrorMessageResourceName = "ValidationUserInvalid", ErrorMessageResourceType = typeof(Resources.Account))]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето \"№ на договор\" е задължително.")]
        public string ContractNumber { get; set; }

        [Required(ErrorMessage = "Полето \"Код за достъп\" е задължително.")]
        [StringLength(100, ErrorMessage = "Полето \"Код за достъп\" не може да съдържа повече от 100 символа.")]
        public string Code { get; set; }
    }

    public class RegisterVM
    {
        [RequiredLocalized(ErrorMessageResourceName = "ValidationFirstNameRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [StringLengthLocalized(100, ErrorMessageResourceName = "ValidationFirstNameLength", ErrorMessageResourceType = typeof(Resources.Account))]
        public string FirstName { get; set; }
        [RequiredLocalized(ErrorMessageResourceName = "ValidationLastNameRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [StringLengthLocalized(100, ErrorMessageResourceName = "ValidationLastNameLength", ErrorMessageResourceType = typeof(Resources.Account))]
        public string LastName { get; set; }
        [RequiredLocalized(ErrorMessageResourceName = "ValidationEmailRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [StringLengthLocalized(100, ErrorMessageResourceName = "ValidationEmailLength", ErrorMessageResourceType = typeof(Resources.Account))]
        [RegularExpressionLocalized(Constants.EMAIL_REGEX, ErrorMessageResourceName = "ValidationEmailInvalid", ErrorMessageResourceType = typeof(Resources.Account))]
        public string Email { get; set; }

        [StringLengthLocalized(50, ErrorMessageResourceName = "ValidationPhoneLengthInvalid", ErrorMessageResourceType = typeof(Resources.Account))]
        [RegularExpressionLocalized(@"(\+\s?)?\d[\d\s]*", ErrorMessageResourceName = "ValidationPhoneInvalid", ErrorMessageResourceType = typeof(Resources.Account))]
        public string Phone { get; set; }
    }

    public class ForgotPasswordVM
    {
        [RequiredLocalized(ErrorMessageResourceName = "ValidationEmailForgottenRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [RegularExpressionLocalized(Constants.EMAIL_REGEX, ErrorMessageResourceName = "ValidationEmailForgottenInvalid", ErrorMessageResourceType = typeof(Resources.Account))]
        public string Email { get; set; }
    }

    public class ActivateVM
    {
        public string ActivationCode { get; set; }
        [RequiredLocalized(ErrorMessageResourceName = "ValidationActivationPasswordRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [ValidPasswordLocalized(0, MinimumLength = 8, ErrorMessageResourceName = "ValidationPasswordSymbols", ErrorMessageResourceType = typeof(Resources.Account))]
        public string Password { get; set; }
        [RequiredLocalized(ErrorMessageResourceName = "ValidationActivationConfirmPasswordRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [CompareLocalized("Password", ErrorMessageResourceName = "ValidationActivationConfirmPasswordInvalid", ErrorMessageResourceType = typeof(Resources.Account))]
        public string ConfirmPassword { get; set; }
    }

    public class ProfileVM : RegisterVM
    {
        public byte[] Version { get; set; }
    }

    public class ChangePasswordVM
    {
        [RequiredLocalized(ErrorMessageResourceName = "ValidationOldPasswordRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        public string OldPassword { get; set; }

        [RequiredLocalized(ErrorMessageResourceName = "ValidationPasswordRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [ValidPasswordLocalized(0, MinimumLength = 8, ErrorMessageResourceName = "ValidationPasswordSymbols", ErrorMessageResourceType = typeof(Resources.Account))]
        public string Password { get; set; }

        [RequiredLocalized(ErrorMessageResourceName = "ValidationConfirmPasswordRequired", ErrorMessageResourceType = typeof(Resources.Account))]
        [CompareLocalized("Password", ErrorMessageResourceName = "ValidationConfirmPasswordInvalid", ErrorMessageResourceType = typeof(Resources.Account))]
        public string ConfirmPassword { get; set; }
    }
}