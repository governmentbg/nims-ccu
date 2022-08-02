using System;
using Eumis.Domain.CertAuthorityChecks;

namespace Eumis.Web.Api.CertAuthorityChecks.DataObjects
{
    public class CertAuthorityCheckDO
    {
        public CertAuthorityCheckDO()
        {
        }

        public CertAuthorityCheckDO(CertAuthorityCheck certAuthorityCheck)
        {
            this.CertAuthorityCheckId = certAuthorityCheck.CertAuthorityCheckId;
            this.Status = certAuthorityCheck.Status;
            this.IsActivated = certAuthorityCheck.IsActivated;
            this.DeleteNote = certAuthorityCheck.DeleteNote;
            this.Level = certAuthorityCheck.Level;
            this.CheckNum = certAuthorityCheck.CheckNum;
            this.Kind = certAuthorityCheck.Kind;
            this.Type = certAuthorityCheck.Type;
            this.DateFrom = certAuthorityCheck.DateFrom;
            this.DateTo = certAuthorityCheck.DateTo;
            this.SubjectType = certAuthorityCheck.SubjectType;
            this.SubjectName = certAuthorityCheck.SubjectName;
            this.Team = certAuthorityCheck.Team;
            this.Version = certAuthorityCheck.Version;
        }

        public int CertAuthorityCheckId { get; set; }

        public CertAuthorityCheckStatus Status { get; set; }

        public bool IsActivated { get; set; }

        public string DeleteNote { get; set; }

        public CertAuthorityCheckLevel Level { get; set; }

        public int? CheckNum { get; set; }

        public CertAuthorityCheckKind Kind { get; set; }

        public CertAuthorityCheckType Type { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public CertAuthorityCheckSubjectType? SubjectType { get; set; }

        public string SubjectName { get; set; }

        public string Team { get; set; }

        public byte[] Version { get; set; }
    }
}
