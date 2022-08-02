using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FinanceReportCommunicator : IFinanceReportCommunicator
    {
        #region Report

        public ContractFinanceReport GetFinanceReport(Guid contractGid, Guid packageGid, string token)
        {
            return FinanceReportApi.GetFinanceReport(contractGid, packageGid, token).ToObject<ContractFinanceReport>();
        }

        public ContractFinanceReport GetFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, bool isEdit)
        {
            if (isEdit)
                return FinanceReportApi.GetFinanceReportForEdit(contractGid, packageGid, financeReportGid, token).ToObject<ContractFinanceReport>();
            else
                return FinanceReportApi.GetFinanceReport(contractGid, packageGid, financeReportGid, token).ToObject<ContractFinanceReport>();
        }

        public ContractFinanceReport CreateFinanceReport(Guid contractGid, Guid packageGid, string token)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                });

            return FinanceReportApi.CreateFinanceReport(contractGid, packageGid, token, body).ToObject<ContractFinanceReport>();
        }

        public ContractFinanceReport PutFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return FinanceReportApi.PutFinanceReport(contractGid, packageGid, financeReportGid, token, body).ToObject<ContractFinanceReport>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public void DeleteFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            FinanceReportApi.DeleteFinanceReport(contractGid, packageGid, financeReportGid, token, body);
        }

        public ContractErrors CanCreateFinanceReport(Guid contractGid, Guid packageGid, string token)
        {
            return FinanceReportApi.CanCreateFinanceReport(contractGid, packageGid, token).ToObject<ContractErrors>();
        }

        public ContractFinanceReport SubmitFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return FinanceReportApi.SubmitFinanceReport(contractGid, packageGid, financeReportGid, token, body).ToObject<ContractFinanceReport>();
        }

        public ContractFinanceReport MakeDraftFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return FinanceReportApi.MakeDraftFinanceReport(contractGid, packageGid, financeReportGid, token, body).ToObject<ContractFinanceReport>();
        }

        public ContractFinanceReport MakeActualFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return FinanceReportApi.MakeActualFinanceReport(contractGid, packageGid, financeReportGid, token, body).ToObject<ContractFinanceReport>();
        }

        #endregion

        #region Private

        public ContractFinanceReport PrivateGetFinanceReport(Guid gid, string token, bool isEdit)
        {
            if(isEdit)
                return FinanceReportApi.PrivateGetFinanceReportForEdit(gid, token).ToObject<ContractFinanceReport>();
            else
                return FinanceReportApi.PrivateGetFinanceReport(gid, token).ToObject<ContractFinanceReport>();
        }

        public ContractFinanceReport PrivatePutFinanceReport(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return FinanceReportApi.PrivatePutFinanceReport(gid, token, body).ToObject<ContractFinanceReport>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractFinanceReport PrivateSubmitFinanceReport(Guid gid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return FinanceReportApi.PrivateSubmitFinanceReport(gid, token, body).ToObject<ContractFinanceReport>();
        }

        #endregion
    }
}