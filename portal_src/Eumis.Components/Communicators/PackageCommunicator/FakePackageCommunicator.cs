using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakePackageCommunicator : IPackageCommunicator
    {
        public ContractPackageInfos GetPackages(Guid contractGid, string token, int limit, int offset)
        {
            return new ContractPackageInfos();
        }

        public ContractReportPVO GetPackage(Guid contractGid, Guid packageGid, string token)
        {
            return new ContractReportPVO();
        }

        public ContractErrors CanCopyPackage(Guid contractGid, Guid packageGid, string token)
        {
            return new ContractErrors();
        }

        public ContractReportGidPVO CopyPackage(Guid contractGid, Guid packageGid, string token)
        {
            return new ContractReportGidPVO();
        }

        public ContractErrors CanUpdatePackage(Guid contractGid, Guid packageGid, string token, string body)
        {
            return new ContractErrors();
        }

        public void UpdatePackage(Guid contractGid, Guid packageGid, string token, string body)
        {
        }

        public ContractErrors CanDeletePackage(Guid contractGid, Guid packageGid, string token)
        {
            return new ContractErrors();
        }

        public void DeletePackage(Guid contractGid, Guid packageGid, string token, string version)
        {

        }

        public ContractErrors CanCreatePackage(Guid contractGid, string token)
        {
            return new ContractErrors();
        }

        public void CreatePackage(Guid contractGid, string token, string body)
        {
        }

        public ContractErrors CanMakeDraftPackage(Guid contractGid, Guid packageGid, string token)
        {
            return new ContractErrors();
        }

        public void MakeDraftPackage(Guid contractGid, Guid packageGid, string token, string version)
        {
        }

        public ContractErrors CanSubmitPackage(Guid contractGid, Guid packageGid, string token)
        {
            return new ContractErrors();
        }

        public void SubmitPackage(Guid contractGid, Guid packageGid, string token, string version)
        {
        }
    }
}