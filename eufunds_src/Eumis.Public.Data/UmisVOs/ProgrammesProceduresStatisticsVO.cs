namespace Eumis.Public.Data.UmisVOs
{
    public class ProgrammesProceduresStatisticsVO
    {
        public int? ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeNameAlt { get; set; }

        public PageVO<ProjectProposalVO> PageProcedures { get; set; }
    }
}
