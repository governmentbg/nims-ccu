using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface ITechnicalReportCommunicator
    {
        #region Report

        ContractTechnicalReport GetTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, bool isEdit);

        ContractTechnicalReport CreateTechnicalReport(Guid contractGid, Guid packageGid, string token);

        ContractTechnicalReport PutTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string xml, byte[] version);

        void DeleteTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string version);

        ContractErrors CanCreateTechnicalReport(Guid contractGid, Guid packageGid, string token);

        ContractTechnicalReport SubmitTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, byte[] version);

        ContractTechnicalReport MakeDraftTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, byte[] version);

        ContractTechnicalReport MakeActualTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, byte[] version);

        #endregion

        #region Private

        ContractTechnicalReport PrivateGetTechnicalReport(Guid gid, string token, bool isEdit);

        ContractTechnicalReport PrivatePutTechnicalReport(Guid gid, string token, string xml, byte[] version);

        ContractTechnicalReport PrivateSubmitTechnicalReport(Guid gid, string token, byte[] version);

        #endregion
    }
}