using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeTechnicalReportCommunicator : ITechnicalReportCommunicator
    {
        #region Report

        public ContractTechnicalReport GetTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, bool isEdit) { throw new NotImplementedException(); }

        public ContractTechnicalReport CreateTechnicalReport(Guid contractGid, Guid packageGid, string token) { throw new NotImplementedException(); }

        public ContractTechnicalReport PutTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string xml, byte[] version) { throw new NotImplementedException(); }

        public void DeleteTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string version) { throw new NotImplementedException(); }

        public ContractErrors CanCreateTechnicalReport(Guid contractGid, Guid packageGid, string token) { throw new NotImplementedException(); }

        public ContractTechnicalReport SubmitTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, byte[] version) { throw new NotImplementedException(); }

        public ContractTechnicalReport MakeDraftTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, byte[] version) { throw new NotImplementedException(); }

        public ContractTechnicalReport MakeActualTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, byte[] version) { throw new NotImplementedException(); }

        #endregion

        #region Private

        public ContractTechnicalReport PrivateGetTechnicalReport(Guid gid, string token, bool isEdit)
        {
            return new ContractTechnicalReport()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractTechnicalReport PrivatePutTechnicalReport(Guid gid, string token, string xml, byte[] version)
        {
            return new ContractTechnicalReport()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractTechnicalReport PrivateSubmitTechnicalReport(Guid gid, string token, byte[] version)
        {
            return new ContractTechnicalReport()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        #endregion
    }
}