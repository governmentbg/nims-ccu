namespace Eumis.Portal.Model.Entities
{
    public partial class Settlement
    {
        public int SettlementId { get; set; }
        public int MunicipalityId { get; set; }
        public string LauCode { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string DisplayName { get; set; }
        public string FullPathName { get; set; }
        public string FullPathNameAlt { get; set; }
        public string FullPath { get; set; }
        public decimal Order { get; set; }
        public virtual Municipality Municipality { get; set; }
    }
}
