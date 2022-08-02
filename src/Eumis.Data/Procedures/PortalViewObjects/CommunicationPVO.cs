using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class CommunicationPVO
    {
        public CommunicationPVO()
        {
            this.Files = new List<FilePVO>();
        }

        public CommunicationPVO(ProcedureMassCommunication communication, ContractCommunicationType type)
            : this()
        {
            this.Subject = communication.Subject;
            this.Body = communication.Body;
            this.Type = type;

            communication.Documents.ForEach(x =>
            {
                this.Files.Add(new FilePVO
                {
                    Description = x.Description,
                    FileKey = x.FileKey,
                    FileName = x.FileName,
                    Name = x.Name,
                });
            });
        }

        public ContractCommunicationType Type { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public IList<FilePVO> Files { get; set; }
    }
}
