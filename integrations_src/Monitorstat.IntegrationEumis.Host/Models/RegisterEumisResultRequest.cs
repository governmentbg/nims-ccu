using Monitorstat.Common.Contracts;
using Monitorstat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Models
{
    public partial class RegisterEumisResultRequest
    {
        public RegisterEumisResultRequest()
        {
        }

        public RegisterEumisResultRequest(RegisterMonitorstatResultRequest request, BlobInfo blobInfo, Guid? subjectRequestGuid)
            : this()
        {
            this.ProcedureIdentifier = request.ProcedureIdentifier;
            this.SubjectIdentifier = request.SubjectIdentifier;
            this.SubjectIdentifierType = request.SubjectIdentifierType.ToEumisType();

            this.FileName = blobInfo.FileName;
            this.FileKey = blobInfo.FileKey;
            this.SubjectRequestGuid = subjectRequestGuid;
        }

        public string ProcedureIdentifier { get; }

        public string SubjectIdentifier { get; }

        public UinType SubjectIdentifierType { get; }

        public string FileName { get; }

        public Guid FileKey { get; }

        public Guid? SubjectRequestGuid { get; }
    }
}
