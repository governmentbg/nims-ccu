using Eumis.Documents.Contracts;
using System;

namespace Eumis.Components.Communicators
{
    public interface ICheckListCommunicator
    {
        #region CheckList template

        ContractCheckListDocument GetCheckList(Guid gid, string token);

        ContractCheckListDocument PutCheckList(Guid gid, string token, string xml, byte[] version);

        ContractCheckListDocument SubmitCheckList(Guid gid, string token, byte[] version);

        #endregion

        #region CheckSheet
        
        ContractCheckSheetDocument GetCheckSheet(Guid gid, string token);

        ContractCheckSheetInitializer GetCheckSheetProcurementPlans(Guid gid, string token);

        ContractVerificationData GetUserVerificationData(Guid gid, string token);

        ContractCheckSheetDocument PutCheckSheet(Guid gid, string token, string xml, byte[] version);

        ContractCheckSheetDocument SubmitCheckSheet(Guid gid, string token, byte[] version, string nextUserRole);

        ContractCheckSheetDocument PauseCheckSheet(Guid gid, string token, byte[] version, string reason);

        #endregion
    }
}
