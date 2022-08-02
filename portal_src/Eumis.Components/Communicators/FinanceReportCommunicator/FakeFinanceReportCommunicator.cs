using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeFinanceReportCommunicator : IFinanceReportCommunicator
    {
        #region Report

        public ContractFinanceReport GetFinanceReport(Guid contractGid, Guid packageGid, string token) { throw new NotImplementedException(); }

        public ContractFinanceReport GetFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, bool isEdit) { throw new NotImplementedException(); }

        public ContractFinanceReport CreateFinanceReport(Guid contractGid, Guid packageGid, string token) { throw new NotImplementedException(); }

        public ContractFinanceReport PutFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string xml, byte[] version) { throw new NotImplementedException(); }

        public void DeleteFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string version) { throw new NotImplementedException(); }

        public ContractErrors CanCreateFinanceReport(Guid contractGid, Guid packageGid, string token) { throw new NotImplementedException(); }

        public ContractFinanceReport SubmitFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, byte[] version) { throw new NotImplementedException(); }

        public ContractFinanceReport MakeDraftFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, byte[] version) { throw new NotImplementedException(); }

        public ContractFinanceReport MakeActualFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, byte[] version) { throw new NotImplementedException(); }

        #endregion

        #region Private

        public ContractFinanceReport PrivateGetFinanceReport(Guid gid, string token, bool isEdit)
        {
            return new ContractFinanceReport()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractFinanceReport PrivatePutFinanceReport(Guid gid, string token, string xml, byte[] version)
        {
            return new ContractFinanceReport()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractFinanceReport PrivateSubmitFinanceReport(Guid gid, string token, byte[] version)
        {
            return new ContractFinanceReport()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        #endregion
    }
}