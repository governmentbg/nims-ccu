using System;
using Eumis.Documents.Contracts;

namespace Eumis.Components.Communicators
{
    public class CheckListCommunicator : ICheckListCommunicator
    {
        #region CheckList template

        public ContractCheckListDocument GetCheckList(Guid gid, string token)
        {
            return CheckListApi.GetCheckList(gid, token).ToObject<ContractCheckListDocument>();
        }

        public ContractCheckListDocument PutCheckList(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return CheckListApi.PutCheckList(gid, token, body).ToObject<ContractCheckListDocument>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractCheckListDocument SubmitCheckList(Guid gid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            return CheckListApi.SubmitCheckList(gid, token, body).ToObject<ContractCheckListDocument>();
        }

        #endregion

        #region CheckSheet

        public ContractCheckSheetDocument GetCheckSheet(Guid gid, string token)
        {
            return CheckListApi.GetCheckSheet(gid, token).ToObject<ContractCheckSheetDocument>();
        }

        public ContractCheckSheetInitializer GetCheckSheetProcurementPlans(Guid gid, string token)
        {
            return CheckListApi.GetCheckSheetProcurementPlans(gid, token).ToObject<ContractCheckSheetInitializer>();
        }

        public ContractVerificationData GetUserVerificationData(Guid gid, string token)
        {
            return CheckListApi.GetUserVerificationData(gid, token).ToObject<ContractVerificationData>();
        }

        public ContractCheckSheetDocument PutCheckSheet(Guid gid, string token, string xml, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    xml = xml,
                    version = version
                });

            try
            {
                return CheckListApi.PutCheckSheet(gid, token, body).ToObject<ContractCheckSheetDocument>();
            }
            catch (Exception ex)
            {
                ApiErrorHandling.HandleDraftCommunicationExceptions(ex);
                return null;
            }
        }

        public ContractCheckSheetDocument SubmitCheckSheet(Guid gid, string token, byte[] version, string nextUserRole)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version,
                    nextUserRole = nextUserRole,
                });

            return CheckListApi.SubmitCheckSheet(gid, token, body).ToObject<ContractCheckSheetDocument>();
        }

        public ContractCheckSheetDocument PauseCheckSheet(Guid gid, string token, byte[] version, string reason)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    pauseReason = reason,
                    version = version,
                });

            return CheckListApi.PauseCheckSheet(gid, token, body).ToObject<ContractCheckSheetDocument>();
        }

        #endregion
    }
}
