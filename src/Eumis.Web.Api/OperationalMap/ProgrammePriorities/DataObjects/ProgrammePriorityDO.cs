using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.DataObjects
{
    public class ProgrammePriorityDO
    {
        public ProgrammePriorityDO()
        {
        }

        public ProgrammePriorityDO(ProgrammePriority programmePriority)
        {
            this.ProgrammePriorityId = programmePriority.MapNodeId;
            this.ProgrammeId = programmePriority.MapNodeRelation.ParentMapNodeId.Value;
            this.Status = programmePriority.Status;
            this.Code = programmePriority.Code;
            this.Name = programmePriority.Name;
            this.NameAlt = programmePriority.NameAlt;
            this.Description = programmePriority.Description;
            this.DescriptionAlt = programmePriority.DescriptionAlt;
            this.CompanyData = new ProgrammePriorityCompanyDO(programmePriority.CompanyData);

            this.Version = programmePriority.Version;
        }

        public int ProgrammePriorityId { get; set; }

        public int ProgrammeId { get; set; }

        public MapNodeStatus? Status { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public ProgrammePriorityCompanyDO CompanyData { get; set; }

        public byte[] Version { get; set; }
    }
}
