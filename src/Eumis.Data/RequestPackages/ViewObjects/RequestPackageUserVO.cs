using Eumis.Common.Json;
using Eumis.Domain.RequestPackages;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.RequestPackages.ViewObjects
{
    public class RequestPackageUserVO
    {
        public int RequestPackageId { get; set; }

        public int UserId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public RequestPackageType? PackageType { get; set; }

        public string PackageNumber { get; set; }

        public DateTime PackageCreateDate { get; set; }

        public DateTime PackageModifyDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public RequestStatus StatusName { get; set; }

        public RequestStatus Status { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }

        public string UserType { get; set; }

        public string UserOrganization { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsLocked { get; set; }

        public bool HasPermissionRequest { get; set; }

        public bool HasRegDataRequest { get; set; }

        public string RejectionMessage { get; set; }

        public string EnteredByUser { get; set; }

        public string CheckedByUser { get; set; }

        public string EndedByUser { get; set; }
    }
}
