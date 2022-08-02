using Eumis.Domain.Core;
using Eumis.Domain.Procedures;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureAppGuidelineDO
    {
        public ProcedureAppGuidelineDO()
        {
        }

        public ProcedureAppGuidelineDO(int procedureId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.Version = version;
        }

        public ProcedureAppGuidelineDO(ProcedureApplicationGuideline procedureAppGuideline, byte[] version)
        {
            this.ProcedureApplicationGuidelineId = procedureAppGuideline.ProcedureApplicationGuidelineId;
            this.ProcedureId = procedureAppGuideline.ProcedureId;
            this.Name = procedureAppGuideline.Name;
            this.Description = procedureAppGuideline.Decription;
            this.Version = version;

            if (procedureAppGuideline.File != null)
            {
                this.File = new FileDO
                {
                    Key = procedureAppGuideline.File.Key,
                    Name = procedureAppGuideline.File.FileName,
                };
            }
        }

        public int ProcedureApplicationGuidelineId { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
