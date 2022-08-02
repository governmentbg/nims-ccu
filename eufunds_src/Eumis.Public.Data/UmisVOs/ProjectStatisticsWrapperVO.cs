namespace Eumis.Public.Data.UmisVOs
{
    public class ProjectStatisticsWrapperVO
    {
        public int ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public PageVO<ProjectStatisticsVO> PageProjects { get; set; }
    }
}
