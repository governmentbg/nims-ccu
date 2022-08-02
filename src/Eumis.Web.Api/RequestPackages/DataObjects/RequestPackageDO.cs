using Eumis.Domain.Core;
using Eumis.Domain.RequestPackages;
using System;

namespace Eumis.Web.Api.RequestPackages.DataObjects
{
    public class RequestPackageDO
    {
        public RequestPackageDO()
        {
        }

        public RequestPackageDO(RequestPackage requestPackage)
        {
            this.RequestPackageId = requestPackage.RequestPackageId;
            this.Type = requestPackage.Type;
            this.Code = requestPackage.Code;
            this.Status = requestPackage.Status;
            this.UserOrganizationId = requestPackage.UserOrganizationId;
            this.PackageDescription = requestPackage.PackageDescription;

            this.Description1 = requestPackage.Description1;
            this.Description2 = requestPackage.Description2;
            this.Description3 = requestPackage.Description3;
            this.Description4 = requestPackage.Description4;
            this.Description5 = requestPackage.Description5;

            this.EndedMessage = requestPackage.EndedMessage;
            this.CreateDate = requestPackage.CreateDate;
            this.Version = requestPackage.Version;

            if (requestPackage.File1 != null)
            {
                this.File1 = new FileDO
                {
                    Key = requestPackage.File1.Key,
                    Name = requestPackage.File1.FileName,
                };
            }

            if (requestPackage.File2 != null)
            {
                this.File2 = new FileDO
                {
                    Key = requestPackage.File2.Key,
                    Name = requestPackage.File2.FileName,
                };
            }

            if (requestPackage.File3 != null)
            {
                this.File3 = new FileDO
                {
                    Key = requestPackage.File3.Key,
                    Name = requestPackage.File3.FileName,
                };
            }

            if (requestPackage.File4 != null)
            {
                this.File4 = new FileDO
                {
                    Key = requestPackage.File4.Key,
                    Name = requestPackage.File4.FileName,
                };
            }

            if (requestPackage.File5 != null)
            {
                this.File5 = new FileDO
                {
                    Key = requestPackage.File5.Key,
                    Name = requestPackage.File5.FileName,
                };
            }
        }

        public int RequestPackageId { get; set; }

        public RequestPackageType? Type { get; set; }

        public string Code { get; set; }

        public RequestPackageStatus Status { get; set; }

        public int? UserOrganizationId { get; set; }

        public string PackageDescription { get; set; }

        public FileDO File1 { get; set; }

        public string Description1 { get; set; }

        public FileDO File2 { get; set; }

        public string Description2 { get; set; }

        public FileDO File3 { get; set; }

        public string Description3 { get; set; }

        public FileDO File4 { get; set; }

        public string Description4 { get; set; }

        public FileDO File5 { get; set; }

        public string Description5 { get; set; }

        public string EndedMessage { get; set; }

        public DateTime? CreateDate { get; set; }

        public byte[] Version { get; set; }
    }
}
