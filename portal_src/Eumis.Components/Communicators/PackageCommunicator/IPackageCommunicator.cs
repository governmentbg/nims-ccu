using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IPackageCommunicator
    {
        ContractPackageInfos GetPackages(Guid contractGid, string token, int limit, int offset);

        ContractReportPVO GetPackage(Guid contractGid, Guid packageGid, string token);

        ContractErrors CanCopyPackage(Guid contractGid, Guid packageGid, string token);

        ContractReportGidPVO CopyPackage(Guid contractGid, Guid packageGid, string token);

        ContractErrors CanUpdatePackage(Guid contractGid, Guid packageGid, string token, string body);

        void UpdatePackage(Guid contractGid, Guid packageGid, string token, string body);

        ContractErrors CanDeletePackage(Guid contractGid, Guid packageGid, string token);

        void DeletePackage(Guid contractGid, Guid packageGid, string token, string version);

        ContractErrors CanCreatePackage(Guid contractGid, string token);

        void CreatePackage(Guid contractGid, string token, string body);

        ContractErrors CanMakeDraftPackage(Guid contractGid, Guid packageGid, string token);

        void MakeDraftPackage(Guid contractGid, Guid packageGid, string token, string version);

        ContractErrors CanSubmitPackage(Guid contractGid, Guid packageGid, string token);

        void SubmitPackage(Guid contractGid, Guid packageGid, string token, string version);
    }
}