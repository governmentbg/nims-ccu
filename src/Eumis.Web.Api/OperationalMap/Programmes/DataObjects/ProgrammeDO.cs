using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.Programmes;

namespace Eumis.Web.Api.OperationalMap.Programmes.DataObjects
{
    public class ProgrammeDO
    {
        public ProgrammeDO()
        {
        }

        public ProgrammeDO(Programme programme)
        {
            this.ProgrammeId = programme.MapNodeId;
            this.Status = programme.Status;
            this.Code = programme.Code;
            this.ShortName = programme.ShortName;
            this.Name = programme.Name;
            this.NameAlt = programme.NameAlt;
            this.Description = programme.Description;
            this.DescriptionAlt = programme.DescriptionAlt;
            this.CompanyId = programme.CompanyId;

            this.Version = programme.Version;
        }

        public int ProgrammeId { get; set; }

        public MapNodeStatus? Status { get; set; }

        public string Code { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public int? CompanyId { get; set; }

        public byte[] Version { get; set; }
    }
}
