namespace Eumis.Public.Data.UmisVOs
{
    public class ProjectProposalWrapperVO
    {
        public ProjectProposalVO Totals { get; set; }

        public PageVO<ProjectProposalVO> PageResults { get; set; }
    }
}
