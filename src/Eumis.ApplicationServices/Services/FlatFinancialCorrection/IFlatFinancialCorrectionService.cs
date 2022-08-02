using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.FlatFinancialCorrection
{
    public interface IFlatFinancialCorrectionService
    {
        Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrection CreateFlatFinancialCorrection(
            int programmeId,
            string name,
            FlatFinancialCorrectionLevel level,
            FlatFinancialCorrectionType type,
            DateTime? impositionDate,
            string impositionNumber,
            string description,
            Guid? blobKey,
            int? contractId,
            int userId);

        Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrection UpdateFlatFinancialCorrection(
            int flatFinancialCorrectionId,
            byte[] version,
            string name,
            FlatFinancialCorrectionType type,
            DateTime? impositionDate,
            string impositionNumber,
            string description,
            Guid? blobKey);

        IList<string> CanDeleteFlatFinancialCorrection(int flatFinancialCorrectionId);

        Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrection DeleteFlatFinancialCorrection(int flatFinancialCorrectionId, byte[] version);

        IList<string> CanChangeFlatFinancialCorrectionStatusToActual(int flatFinancialCorrectionId);

        IList<string> CanChangeFlatFinancialCorrectionStatusToDraft(int flatFinancialCorrectionId);

        Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrection ChangeFlatFinancialCorrectionStatus(int flatFinancialCorrectionId, byte[] version, FlatFinancialCorrectionStatus status);

        TEntity CreateFlatFinancialCorrectionItem<TEntity>(int flatFinancialCorrectionId, byte[] version, int itemId)
            where TEntity : FlatFinancialCorrectionLevelItem, IFlatFinancialCorrectionItem, new();

        TEntity UpdateFlatFinancialCorrectionItem<TEntity>(
            int flatFinancialCorrectionId,
            byte[] version,
            int flatFinancialCorrectionLevelItemId,
            decimal? percent,
            decimal? euAmount,
            decimal? bgAmount,
            decimal? totalAmount)
            where TEntity : FlatFinancialCorrectionLevelItem, new();

        TEntity DeleteFlatFinancialCorrectionItem<TEntity>(int flatFinancialCorrectionId, byte[] version, int flatFinancialCorrectionLevelItemId)
            where TEntity : FlatFinancialCorrectionLevelItem, new();
    }
}
