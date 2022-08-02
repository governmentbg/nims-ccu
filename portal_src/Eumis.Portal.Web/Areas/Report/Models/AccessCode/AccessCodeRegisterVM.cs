using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Areas.Report.Models.AccessCode
{
    public class AccessCodeRegisterVM
    {
        public Guid? id { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [MaxLength(100, ErrorMessage = "Полето \"{0}\" не може да съдържа повече от 100 символа.")]
        [Display(Name = "Собствено име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [MaxLength(100, ErrorMessage = "Полето \"{0}\" не може да съдържа повече от 100 символа.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "ЕГН/Чуждестранно лице")]
        public string Identifier { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [MaxLength(100, ErrorMessage = "Полето \"{0}\" не може да съдържа повече от 100 символа.")]
        [Display(Name = "Позиция")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Полето \"Потребителско име\", съдържащо Вашия имейл адрес е задължително.")]
        [MaxLength(100, ErrorMessage = "Полето \"Потребителско име\" не може да съдържа повече от 100 символа.")]
        [Display(Name = "Потребителско име (имейл адрес)")]
        [RegularExpression(Constants.EMAIL_REGEX, ErrorMessage = "Полето \"Потребителско име\", съдържащо Вашия имейл адрес е невалидно.")]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public bool isEdit { get; set; }

        public byte[] Version { get; set; }

        private AccessCodePermissionPVO _permissions;
        public AccessCodePermissionPVO Permissions
        {
            get
            {
                if (_permissions == null)
                    return new AccessCodePermissionPVO();
                else return _permissions;
            }
            set
            {
                _permissions = value;
            }
        }

        public string Reading { get { return "Четене"; } }
        public string Writing { get { return "Писане"; } }

        public AccessCodeRegisterVM() { }

        public AccessCodeRegisterVM(ContractRegistrationAccessCodePVO contractUser)
        {
            this.id = contractUser.gid;
            this.FirstName = contractUser.firstName;
            this.LastName = contractUser.lastName;
            this.Identifier = contractUser.identifier;
            this.Position = contractUser.position;
            this.Email = contractUser.email;
            this.IsActive = contractUser.isActive;
            this.isEdit = true;
            this.Permissions = contractUser.permissions;
            this.Version = contractUser.version;
        }

        public void FixPermissions()
        {
            if(this.Permissions != null)
            {
                if (this.Permissions.canWriteProcurements)
                    this.Permissions.canReadProcurements = true;

                if (this.Permissions.canWriteSpendingPlan)
                    this.Permissions.canReadSpendingPlan = true;

                if (this.Permissions.canWriteTechnicalPlan)
                    this.Permissions.canReadTechnicalPlan = true;

                if (this.Permissions.canWriteFinancialPlan)
                    this.Permissions.canReadFinancialPlan = true;

                if (this.Permissions.canWritePaymentRequest)
                    this.Permissions.canReadPaymentRequest = true;

                if (this.Permissions.canWriteMicrodata)
                    this.Permissions.canReadMicrodata = true;
            }
        }
    }
}