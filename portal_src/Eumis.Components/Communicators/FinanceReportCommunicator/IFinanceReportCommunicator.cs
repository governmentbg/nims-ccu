using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IFinanceReportCommunicator
    {
        #region Report

        ContractFinanceReport GetFinanceReport(Guid contractGid, Guid packageGid, string token);

        ContractFinanceReport GetFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, bool isEdit);

        ContractFinanceReport CreateFinanceReport(Guid contractGid, Guid packageGid, string token);

        ContractFinanceReport PutFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string xml, byte[] version);

        void DeleteFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string version);

        ContractErrors CanCreateFinanceReport(Guid contractGid, Guid packageGid, string token);

        ContractFinanceReport SubmitFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, byte[] version);

        ContractFinanceReport MakeDraftFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, byte[] version);

        ContractFinanceReport MakeActualFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, byte[] version);

        #endregion

        #region Private

        ContractFinanceReport PrivateGetFinanceReport(Guid gid, string token, bool isEdit);

        ContractFinanceReport PrivatePutFinanceReport(Guid gid, string token, string xml, byte[] version);

        ContractFinanceReport PrivateSubmitFinanceReport(Guid gid, string token, byte[] version);

        #endregion
    }
}