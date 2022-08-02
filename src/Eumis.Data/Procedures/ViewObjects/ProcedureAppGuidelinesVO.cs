using Eumis.Domain.Core;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureAppGuidelinesVO
    {
        public int ProcedureApplicationGuidelineId { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileVO File { get; set; }
    }
}
