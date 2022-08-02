using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class MicroDataCommunicator : IMicroDataCommunicator
    {
        #region Report

        public ContractErrors CanCreate(Guid contractGid, Guid packageGid, ContractReportMicroType type, string token)
        {
            return MicroDataApi.CanCreate(contractGid, packageGid, type, token).ToObject<ContractErrors>();
        }

        public void Create(Guid contractGid, Guid packageGid, ContractReportMicroType type, string token)
        {
            MicroDataApi.Create(contractGid, packageGid, type, token);
        }

        public ContractMicroPagePVO<ContractReportMicroType1ItemPVO> GetType1(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset)
        {
            return MicroDataApi.Get(contractGid, packageGid, documentGid, token, limit, offset).ToObject<ContractMicroPagePVO<ContractReportMicroType1ItemPVO>>();
        }

        public ContractMicroPagePVO<ContractReportMicroType2ItemPVO> GetType2(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset)
        {
            return MicroDataApi.Get(contractGid, packageGid, documentGid, token, limit, offset).ToObject<ContractMicroPagePVO<ContractReportMicroType2ItemPVO>>();
        }

        public ContractMicroPagePVO<ContractReportMicroType3ItemPVO> GetType3(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset)
        {
            return MicroDataApi.Get(contractGid, packageGid, documentGid, token, limit, offset).ToObject<ContractMicroPagePVO<ContractReportMicroType3ItemPVO>>();
        }

        public ContractMicroPagePVO<ContractReportMicroType4ItemPVO> GetType4(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset)
        {
            return MicroDataApi.Get(contractGid, packageGid, documentGid, token, limit, offset).ToObject<ContractMicroPagePVO<ContractReportMicroType4ItemPVO>>();
        }

        public bool CheckMicroHasFile(Guid contractGid, Guid packageGid, Guid documentGid, Guid fileKey, string token)
        {
            return MicroDataApi.HasFile(contractGid, packageGid, documentGid, fileKey, token);
        }

        public ContractErrors Put(Guid contractGid, Guid packageGid, Guid documentGid, string token, Guid blobKey, string fileName, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    excelBlobKey = blobKey,
                    excelName = fileName,
                    version = version
                });

            return MicroDataApi.Put(contractGid, packageGid, documentGid, token, body).ToObject<ContractErrors>();
        }

        public ContractErrors PutWithSimevCode(Guid contractGid, Guid packageGid, Guid documentGid, string token, string simevCode, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    simevCode = simevCode,
                    version = version
                });

            return MicroDataApi.PutWithSimevCode(contractGid, packageGid, documentGid, token, body).ToObject<ContractErrors>();
        }

        public ContractErrors CanSubmit(Guid contractGid, Guid packageGid, Guid documentGid, string token)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                });

            return MicroDataApi.CanSubmit(contractGid, packageGid, documentGid, token, body).ToObject<ContractErrors>();
        }

        public void Submit(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            MicroDataApi.Submit(contractGid, packageGid, documentGid, token, body);
        }

        public void Delete(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version)
        {

            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            MicroDataApi.Delete(contractGid, packageGid, documentGid, token, body);
        }

        public void MakeDraft(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            MicroDataApi.MakeDraft(contractGid, packageGid, documentGid, token, body);
        }

        public void MakeActual(Guid contractGid, Guid packageGid, Guid documentGid, string token, byte[] version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            MicroDataApi.MakeActual(contractGid, packageGid, documentGid, token, body);
        }

        #endregion

        #region Private

        public ContractMicroPagePVO<ContractReportMicroType1ItemPVO> PrivateGetType1(Guid gid, string token, int limit, int offset)
        {
            return MicroDataApi.PrivateGet(gid, token, limit, offset).ToObject<ContractMicroPagePVO<ContractReportMicroType1ItemPVO>>();
        }

        public ContractMicroPagePVO<ContractReportMicroType2ItemPVO> PrivateGetType2(Guid gid, string token, int limit, int offset)
        {
            return MicroDataApi.PrivateGet(gid, token, limit, offset).ToObject<ContractMicroPagePVO<ContractReportMicroType2ItemPVO>>();
        }

        public ContractMicroPagePVO<ContractReportMicroType3ItemPVO> PrivateGetType3(Guid gid, string token, int limit, int offset)
        {
            return MicroDataApi.PrivateGet(gid, token, limit, offset).ToObject<ContractMicroPagePVO<ContractReportMicroType3ItemPVO>>();
        }

        public ContractMicroPagePVO<ContractReportMicroType4ItemPVO> PrivateGetType4(Guid gid, string token, int limit, int offset)
        {
            return MicroDataApi.PrivateGet(gid, token, limit, offset).ToObject<ContractMicroPagePVO<ContractReportMicroType4ItemPVO>>();
        }

        #endregion
    }
}