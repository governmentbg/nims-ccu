using Monitorstat.Common.Contracts;
using Monitorstat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitorstat.Common.MonitorstatService
{
    public partial class SubjectRequest
    {
        public SubjectRequest(SubjectRequestDO subjectRequest)
        {
            this.SubjectIdentifier = subjectRequest.Uin;
            this.SubjectIdentifierType = subjectRequest.UinType.ToMonitorstatType();
            this.Type = subjectRequest.UinType.ToMonitorstatNomenclature();

            this.ArchiveFile = new File();
            this.ArchiveFile.Name = subjectRequest.File.Name;
            this.ArchiveFile.Size = subjectRequest.File.Size;
            this.ArchiveFile.Content = subjectRequest.File.Content;   
        }
    }
}
