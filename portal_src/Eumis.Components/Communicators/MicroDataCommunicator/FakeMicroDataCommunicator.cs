using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeMicroDataCommunicator : IMicroDataCommunicator
    {
        #region Report

        public ContractErrors CanCreate(Guid contractGid, Guid packageGid, ContractReportMicroType type, string token) { throw new NotImplementedException(); }

        public void Create(Guid contractGid, Guid packageGid, ContractReportMicroType type, string token) { throw new NotImplementedException(); }

        public ContractMicroPagePVO<ContractReportMicroType1ItemPVO> GetType1(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset) { throw new NotImplementedException(); }

        public ContractMicroPagePVO<ContractReportMicroType2ItemPVO> GetType2(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset) { throw new NotImplementedException(); }

        public ContractMicroPagePVO<ContractReportMicroType3ItemPVO> GetType3(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset) { throw new NotImplementedException(); }

        public ContractMicroPagePVO<ContractReportMicroType4ItemPVO> GetType4(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset) { throw new NotImplementedException(); }

        public bool CheckMicroHasFile(Guid contractGid, Guid packageGid, Guid documentGid, Guid fileKey, string token) { throw new NotImplementedException(); }

        public ContractErrors Put(Guid contractGid, Guid packageGid, Guid documentGid, string token, Guid blobKey, string fileName, byte[] version) { throw new NotImplementedException(); }

        public ContractErrors PutWithSimevCode(Guid contractGid, Guid packageGid, Guid documentGid, string token, string simevCode, byte[] version) { throw new NotImplementedException(); }

        public ContractErrors CanSubmit(Guid contractGid, Guid packageGid, Guid documentGid, string token) { throw new NotImplementedException(); }

        public void Submit(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version) { throw new NotImplementedException(); }

        public void Delete(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version) { throw new NotImplementedException(); }

        public void MakeDraft(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version) { throw new NotImplementedException(); }

        public void MakeActual(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version) { throw new NotImplementedException(); }

        #endregion

        #region Private

        public ContractMicroPagePVO<ContractReportMicroType1ItemPVO> PrivateGetType1(Guid gid, string token, int limit, int offset) { throw new NotImplementedException(); }

        public ContractMicroPagePVO<ContractReportMicroType2ItemPVO> PrivateGetType2(Guid gid, string token, int limit, int offset) { throw new NotImplementedException(); }

        public ContractMicroPagePVO<ContractReportMicroType3ItemPVO> PrivateGetType3(Guid gid, string token, int limit, int offset) { throw new NotImplementedException(); }

        public ContractMicroPagePVO<ContractReportMicroType4ItemPVO> PrivateGetType4(Guid gid, string token, int limit, int offset) { throw new NotImplementedException(); }

        #endregion
    }
}