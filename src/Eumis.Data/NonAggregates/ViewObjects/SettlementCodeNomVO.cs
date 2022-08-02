using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Data.NonAggregates.ViewObjects
{
    public class SettlementCodeNomVO : EntityCodeNomVO
    {
        public int ParentId { get; set; }

        public string FullPath { get; set; }

        public string FullPathName { get; set; }
    }
}
