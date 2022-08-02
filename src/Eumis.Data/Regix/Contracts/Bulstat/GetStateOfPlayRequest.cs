namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class GetStateOfPlayRequest
    {
        public string UIC { get; set; }

        public GetStateOfPlayRequestCase Case { get; set; }
    }
}
