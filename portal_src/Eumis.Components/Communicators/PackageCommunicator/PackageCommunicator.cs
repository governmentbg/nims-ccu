using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class PackageCommunicator : IPackageCommunicator
    {
        public ContractPackageInfos GetPackages(Guid contractGid, string token, int limit, int offset)
        {
            return PackageApi.GetPackages(contractGid, token, limit, offset).ToObject<ContractPackageInfos>();
        }

        public ContractReportPVO GetPackage(Guid contractGid, Guid packageGid, string token)
        {
            return PackageApi.GetPackage(contractGid, packageGid, token).ToObject<ContractReportPVO>();
        }

        public ContractErrors CanCopyPackage(Guid contractGid, Guid packageGid, string token)
        {
            return PackageApi.CanCopyPackage(contractGid, packageGid, token).ToObject<ContractErrors>();
        }

        public ContractReportGidPVO CopyPackage(Guid contractGid, Guid packageGid, string token)
        {
            return PackageApi.CopyPackage(contractGid, packageGid, token).ToObject<ContractReportGidPVO>();
        }

        public ContractErrors CanUpdatePackage(Guid contractGid, Guid packageGid, string token, string body)
        {
            return PackageApi.CanUpdatePackage(contractGid, packageGid, token, body).ToObject<ContractErrors>();
        }

        public void UpdatePackage(Guid contractGid, Guid packageGid, string token, string body)
        {
            PackageApi.UpdatePackage(contractGid, packageGid, token, body);
        }

        public ContractErrors CanDeletePackage(Guid contractGid, Guid packageGid, string token)
        {
            return PackageApi.CanDeletePackage(contractGid, packageGid, token).ToObject<ContractErrors>();
        }

        public void DeletePackage(Guid contractGid, Guid packageGid, string token, string version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            PackageApi.DeletePackage(contractGid, packageGid, token, body);
        }

        public ContractErrors CanCreatePackage(Guid contractGid, string token)
        {
            return PackageApi.CanCreatePackage(contractGid, token).ToObject<ContractErrors>();
        }

        public void CreatePackage(Guid contractGid, string token, string body)
        {
            PackageApi.CreatePackage(contractGid, token, body);
        }

        public ContractErrors CanMakeDraftPackage(Guid contractGid, Guid packageGid, string token)
        {
            return PackageApi.CanMakeDraftPackage(contractGid, packageGid, token).ToObject<ContractErrors>();
        }

        public void MakeDraftPackage(Guid contractGid, Guid packageGid, string token, string version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });


            PackageApi.MakeDraftPackage(contractGid, packageGid, token, body);
        }

        public ContractErrors CanSubmitPackage(Guid contractGid, Guid packageGid, string token)
        {
            return PackageApi.CanSubmitPackage(contractGid, packageGid, token).ToObject<ContractErrors>();
        }

        public void SubmitPackage(Guid contractGid, Guid packageGid, string token, string version)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    version = version
                });

            PackageApi.SubmitPackage(contractGid, packageGid, token, body);
        }
    }
}