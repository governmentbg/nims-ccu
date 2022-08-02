using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class TechnicalReportCommunicator : ITechnicalReportCommunicator
    {
        #region Report

        public ContractTechnicalReport GetTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, bool isEdit)
        {
            if(isEdit)
                return TechnicalReportApi.GetTechnicalReportForEdit(contractGid, packageGid, technicalReportGid, token).ToObject<ContractTechnicalReport>();
            else
                return TechnicalReportApi.GetTechnicalReport(contractGid, packageGid, technicalReportGid, token).ToObject<ContractTechnicalReport>();
        }

        public ContractTechnicalReport CreateTechnicalReport(Guid contractGid, Guid packageGid, string token)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                });

            return TechnicalReportApi.CreateTechnicalReport(contractGid, packageGid, token, body).ToObject<ContractTechnicalReport>();
        }

        public ContractTechnicalReport PutTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return TechnicalReportApi.PutTechnicalReport(contractGid, packageGid, technicalReportGid, token, body).ToObject<ContractTechnicalReport>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void DeleteTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string version)
        {

            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            TechnicalReportApi.DeleteTechnicalReport(contractGid, packageGid, technicalReportGid, token, body);
        }

        public ContractErrors CanCreateTechnicalReport(Guid contractGid, Guid packageGid, string token)
        {
            return TechnicalReportApi.CanCreateTechnicalReport(contractGid, packageGid, token).ToObject<ContractErrors>();
        }

        public ContractTechnicalReport SubmitTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return TechnicalReportApi.SubmitTechnicalReport(contractGid, packageGid, technicalReportGid, token, body).ToObject<ContractTechnicalReport>();
        }

        public ContractTechnicalReport MakeDraftTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return TechnicalReportApi.MakeDraftTechnicalReport(contractGid, packageGid, technicalReportGid, token, body).ToObject<ContractTechnicalReport>();
        }

        public ContractTechnicalReport MakeActualTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return TechnicalReportApi.MakeActualTechnicalReport(contractGid, packageGid, technicalReportGid, token, body).ToObject<ContractTechnicalReport>();
        }

        #endregion

        #region Private

        public ContractTechnicalReport PrivateGetTechnicalReport(Guid gid, string token, bool isEdit)
        {
            if(isEdit)
                return TechnicalReportApi.PrivateGetTechnicalReportForEdit(gid, token).ToObject<ContractTechnicalReport>();
            else
                return TechnicalReportApi.PrivateGetTechnicalReport(gid, token).ToObject<ContractTechnicalReport>();
        }

        public ContractTechnicalReport PrivatePutTechnicalReport(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            return TechnicalReportApi.PrivatePutTechnicalReport(gid, token, body).ToObject<ContractTechnicalReport>();
        }

        public ContractTechnicalReport PrivateSubmitTechnicalReport(Guid gid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return TechnicalReportApi.PrivateSubmitTechnicalReport(gid, token, body).ToObject<ContractTechnicalReport>();
        }

        #endregion
    }
}