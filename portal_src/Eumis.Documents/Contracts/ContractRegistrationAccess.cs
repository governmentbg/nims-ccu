using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    public class ContractRegistrationAccessCodesPVO
    {
        public IList<ContractRegistrationAccessCodePVO> results { get; set; }

        public int count { get; set; }
    }

    public class ContractRegistrationAccessCodePVO
    {
        public Guid gid { get; set; }

        public string code { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string identifier { get; set; }

        public string position { get; set; }

        public string email { get; set; }

        public bool isActive { get; set; }

        public AccessCodePermissionPVO permissions { get; set; }

        public DateTime createDate { get; set; }

        public byte[] version { get; set; }

        public bool HasAnyPermissions
        {
            get
            {
                return this.permissions != null
                    && (this.permissions.canReadContracts

                     || this.permissions.canReadCommunication

                     || this.permissions.canReadProcurements
                     || this.permissions.canWriteProcurements

                     || this.permissions.canReadSpendingPlan
                     || this.permissions.canWriteSpendingPlan

                     || this.permissions.canReadFinancialPlan
                     || this.permissions.canWriteFinancialPlan

                     || this.permissions.canReadTechnicalPlan
                     || this.permissions.canWriteTechnicalPlan

                     || this.permissions.canReadPaymentRequest
                     || this.permissions.canWritePaymentRequest

                     || this.permissions.canReadMicrodata
                     || this.permissions.canWriteMicrodata);
            }
        }

        public string GetPermissions()
        {
            StringBuilder sb = new StringBuilder();

            if (this.HasAnyPermissions)
            {
                sb.Append("<strong>Права за достъп:</strong>");

                if (this.permissions.canReadContracts)
                    sb.Append("<br/>Договори - Четене;");

                if (this.permissions.canReadCommunication)
                    sb.Append("<br/>Кореспонденция - Четене;");

                if (this.permissions.canReadProcurements || this.permissions.canWriteProcurements)
                    sb.Append("<br/>Процедури за избор на изпълнител и сключени договори - ");
                if (this.permissions.canReadProcurements)
                    sb.Append("Четене; ");
                if (this.permissions.canWriteProcurements)
                    sb.Append("Писане; ");

                if (this.permissions.canReadSpendingPlan || this.permissions.canWriteSpendingPlan)
                    sb.Append("<br/>План за разходване на средствата - ");
                if (this.permissions.canReadSpendingPlan)
                    sb.Append("Четене; ");
                if (this.permissions.canWriteSpendingPlan)
                    sb.Append("Писане; ");

                if (this.permissions.canReadFinancialPlan || this.permissions.canWriteFinancialPlan)
                    sb.Append("<br/>Финансов отчет - ");
                if (this.permissions.canReadFinancialPlan)
                    sb.Append("Четене; ");
                if (this.permissions.canWriteFinancialPlan)
                    sb.Append("Писане; ");

                if (this.permissions.canReadTechnicalPlan || this.permissions.canWriteTechnicalPlan)
                    sb.Append("<br/>Технически отчет - ");
                if (this.permissions.canReadTechnicalPlan)
                    sb.Append("Четене; ");
                if (this.permissions.canWriteTechnicalPlan)
                    sb.Append("Писане; ");

                if (this.permissions.canReadPaymentRequest || this.permissions.canWritePaymentRequest)
                    sb.Append("<br/>Искане за плащане - ");
                if (this.permissions.canReadPaymentRequest)
                    sb.Append("Четене; ");
                if (this.permissions.canWritePaymentRequest)
                    sb.Append("Писане; ");

                if (this.permissions.canReadMicrodata || this.permissions.canWriteMicrodata)
                    sb.Append("<br/>Микроданни - ");
                if (this.permissions.canReadMicrodata)
                    sb.Append("Четене; ");
                if (this.permissions.canWriteMicrodata)
                    sb.Append("Писане; ");
            }
            else
            {
                sb.Append("<strong>Няма права за достъп</strong>");
            }

            return sb.ToString();
        }
    }

    public class AccessCodePermissionPVO
    {
        public bool canReadContracts { get; set; }

        public bool canReadProcurements { get; set; }
        public bool canWriteProcurements { get; set; }

        public bool canReadSpendingPlan { get; set; }
        public bool canWriteSpendingPlan { get; set; }

        public bool canReadTechnicalPlan { get; set; }
        public bool canWriteTechnicalPlan { get; set; }

        public bool canReadFinancialPlan { get; set; }
        public bool canWriteFinancialPlan { get; set; }

        public bool canReadPaymentRequest { get; set; }
        public bool canWritePaymentRequest { get; set; }

        public bool canReadCommunication { get; set; }

        public bool canReadMicrodata { get; set; }
        public bool canWriteMicrodata { get; set; }
    }
}
