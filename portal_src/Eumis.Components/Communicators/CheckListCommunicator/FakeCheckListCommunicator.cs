using System;
using Eumis.Documents.Contracts;

namespace Eumis.Components.Communicators
{
    public class FakeCheckListCommunicator : ICheckListCommunicator
    {
        #region CheckList template

        public ContractCheckListDocument GetCheckList(Guid gid, string token)
        {
            return new ContractCheckListDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractCheckListDocument PutCheckList(Guid gid, string token, string xml, byte[] version)
        {
            return new ContractCheckListDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractCheckListDocument SubmitCheckList(Guid gid, string token, byte[] version)
        {
            return new ContractCheckListDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        #endregion

        #region CheckSheet

        public ContractCheckSheetDocument GetCheckSheet(Guid gid, string token)
        {
            return new ContractCheckSheetDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractCheckSheetDocument PutCheckSheet(Guid gid, string token, string xml, byte[] version)
        {
            return new ContractCheckSheetDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractCheckSheetDocument SubmitCheckSheet(Guid gid, string token, byte[] version, string nextUserRole)
        {
            return new ContractCheckSheetDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractCheckSheetDocument PauseCheckSheet(Guid gid, string token, byte[] version, string reason)
        {
            return new ContractCheckSheetDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractCheckSheetInitializer GetCheckSheetProcurementPlans(Guid gid, string token)
        {
            return new ContractCheckSheetInitializer()
            {
                xml = "xml",
            };
        }

        public ContractVerificationData GetUserVerificationData(Guid gid, string token)
        {
            return new ContractVerificationData();
        }

        #endregion
    }
}
