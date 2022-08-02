using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems;
using System;
using System.Linq;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections
{
    public partial class FlatFinancialCorrection
    {
        public void UpdateAttributes(
            string name,
            FlatFinancialCorrectionType type,
            DateTime? impositionDate,
            string impositionNumber,
            string description,
            Guid? blobKey)
        {
            this.Name = name;
            this.Type = type;
            this.ImpositionDate = impositionDate;
            this.ImpositionNumber = impositionNumber;
            this.Description = description;
            this.BlobKey = blobKey;

            this.ModifyDate = DateTime.Now;
        }

        public TEntity FindFlatFinancialCorrectionItem<TEntity>(int flatFinancialCorrectionLevelItemId)
            where TEntity : FlatFinancialCorrectionLevelItem, new()
        {
            var flatFinancialCorrectionItem = this.FlatFinancialCorrectionLevelItems.Where(e => e.FlatFinancialCorrectionLevelItemId == flatFinancialCorrectionLevelItemId).SingleOrDefault();

            if (flatFinancialCorrectionItem == null)
            {
                throw new DomainObjectNotFoundException("Cannot find FlatFinancialCorrectionLevelItem with id " + flatFinancialCorrectionLevelItemId);
            }

            return (TEntity)flatFinancialCorrectionItem;
        }
    }
}
