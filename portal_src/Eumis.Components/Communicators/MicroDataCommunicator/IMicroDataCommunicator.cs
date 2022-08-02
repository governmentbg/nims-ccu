using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IMicroDataCommunicator
    {
        #region Report

        ContractErrors CanCreate(Guid contractGid, Guid packageGid, ContractReportMicroType type, string token);

        void Create(Guid contractGid, Guid packageGid, ContractReportMicroType type, string token);

        ContractMicroPagePVO<ContractReportMicroType1ItemPVO> GetType1(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset);

        ContractMicroPagePVO<ContractReportMicroType2ItemPVO> GetType2(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset);

        ContractMicroPagePVO<ContractReportMicroType3ItemPVO> GetType3(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset);

        ContractMicroPagePVO<ContractReportMicroType4ItemPVO> GetType4(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset);

        bool CheckMicroHasFile(Guid contractGid, Guid packageGid, Guid documentGid, Guid fileKey, string token);

        ContractErrors Put(Guid contractGid, Guid packageGid, Guid documentGid, string token, Guid blobKey, string fileName, byte[] version);

        ContractErrors PutWithSimevCode(Guid contractGid, Guid packageGid, Guid documentGid, string token, string simevCode, byte[] version);

        ContractErrors CanSubmit(Guid contractGid, Guid packageGid, Guid documentGid, string token);

        void Submit(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version);

        void Delete(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version);

        void MakeDraft(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version);

        void MakeActual(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version);

        #endregion

        #region Private

        ContractMicroPagePVO<ContractReportMicroType1ItemPVO> PrivateGetType1(Guid gid, string token, int limit, int offset);

        ContractMicroPagePVO<ContractReportMicroType2ItemPVO> PrivateGetType2(Guid gid, string token, int limit, int offset);

        ContractMicroPagePVO<ContractReportMicroType3ItemPVO> PrivateGetType3(Guid gid, string token, int limit, int offset);

        ContractMicroPagePVO<ContractReportMicroType4ItemPVO> PrivateGetType4(Guid gid, string token, int limit, int offset);

        #endregion
    }
}