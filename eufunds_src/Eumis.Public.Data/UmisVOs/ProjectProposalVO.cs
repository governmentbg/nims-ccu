using Eumis.Public.Common.Localization;

namespace Eumis.Public.Data.UmisVOs
{
    public class ProjectProposalVO
    {
        public int ProcedureId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.NameAlt) ? this.Name : this.NameAlt;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public int ProjectCount { get; set; }

        public decimal BfpAmount { get; set; }

        public decimal SFAmount { get; set; }

        public decimal TotalAmout
        {
            get
            {
                return this.BfpAmount + this.SFAmount;
            }
        }

        public int ApprovedCount { get; set; }

        public int ReserveCount { get; set; }

        public int RejectedCount { get; set; }
    }
}
