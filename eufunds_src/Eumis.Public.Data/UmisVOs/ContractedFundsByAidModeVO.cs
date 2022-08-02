namespace Eumis.Public.Data.UmisVOs
{
    public class ContractedFundsByAidModeVO
    {
        public decimal DeminimisAmount { get; set; }

        public decimal StateAidAmount { get; set; }

        public decimal OtherAmount { get; set; }

        public decimal SelfAmount { get; set; }

        public decimal TotalBfpAmount
        {
            get
            {
                return this.DeminimisAmount + this.StateAidAmount + this.OtherAmount;
            }
        }

        public decimal TotalAmount
        {
            get
            {
                return this.TotalBfpAmount + this.SelfAmount;
            }
        }
    }
}
