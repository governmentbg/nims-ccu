using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public partial class ContractReportMicro
    {
        private static readonly Dictionary<ContractReportMicroType, ContractReportDocumentType> StatusRelations = new Dictionary<ContractReportMicroType, ContractReportDocumentType>()
        {
            { ContractReportMicroType.Type1, ContractReportDocumentType.ContractReportMicrodataType1 },
            { ContractReportMicroType.Type2, ContractReportDocumentType.ContractReportMicrodataType2 },
            { ContractReportMicroType.Type3, ContractReportDocumentType.ContractReportMicrodataType3 },
            { ContractReportMicroType.Type4, ContractReportDocumentType.ContractReportMicrodataType4 },
        };

        private ContractReportDocumentType GetContractReportMicrodataType()
        {
            return StatusRelations[this.Type];
        }

        public void UpdateExcel(Guid excelBlobKey, string excelName, bool isFromExternalSystem)
        {
            if (this.Status != ContractReportMicroStatus.Draft)
            {
                throw new DomainException("Cannot update excel when ContractReportMicro status is different from 'Draft'");
            }

            this.ExcelBlobKey = excelBlobKey;
            this.ExcelName = excelName;
            this.IsFromExternalSystem = isFromExternalSystem;

            this.ModifyDate = DateTime.Now;
        }

        public void SetStatus(ContractReportMicroStatus status)
        {
            switch (status)
            {
                case ContractReportMicroStatus.Draft:
                    if (this.Status != ContractReportMicroStatus.Entered)
                    {
                        throw new DomainException("ContractReportMicro status transition not allowed");
                    }

                    break;
                case ContractReportMicroStatus.Entered:
                    if (this.Status != ContractReportMicroStatus.Draft && this.Status != ContractReportMicroStatus.Actual)
                    {
                        throw new DomainException("ContractReportMicro status transition not allowed");
                    }

                    break;
                case ContractReportMicroStatus.Actual:
                    if (this.Status != ContractReportMicroStatus.Draft && this.Status != ContractReportMicroStatus.Entered)
                    {
                        throw new DomainException("ContractReportFinancial status transition not allowed");
                    }

                    if (this.VersionSubNum > 1)
                    {
                        ((INotificationEventEmitter)this).NotificationEvents.Add(new ContractNotificationEvent(
                            NotificationEventType.ContractReportMicroDataToResent,
                            this.ContractReportId,
                            this.ContractId));
                    }

                    break;
            }

            this.Status = status;
            this.ModifyDate = DateTime.Now;
        }

        public void ReturnMicro(string statusNote)
        {
            if (this.Status != ContractReportMicroStatus.Actual)
            {
                throw new DomainException("ContractReportMicro status transition not allowed");
            }

            this.Status = ContractReportMicroStatus.Returned;
            this.StatusNote = statusNote;
            this.ModifyDate = DateTime.Now;

            ((INotificationEventEmitter)this).NotificationEvents.Add(new ContractNotificationEvent(
                NotificationEventType.ContractReportMicroDataToReturned,
                this.ContractReportId,
                this.ContractId));

            ((IEventEmitter)this).Events.Add(new ContractReportReturnedDocumentEvent()
            {
                ContractReportId = this.ContractReportId,
                ContractReportDocumentType = this.GetContractReportMicrodataType(),
                VersionNum = this.VersionNum,
                VersionSubNum = this.VersionSubNum,
            });
        }

        public void AssertIsDraft()
        {
            if (this.Status != ContractReportMicroStatus.Draft)
            {
                throw new DomainException("ContractReportMicro status transition not allowed");
            }
        }

        public void CopyExcelData(ContractReportMicro copyFromMicro)
        {
            this.AssertIsDraft();

            this.ExcelBlobKey = copyFromMicro.ExcelBlobKey;
            this.ExcelName = copyFromMicro.ExcelName;
            this.IsFromExternalSystem = copyFromMicro.IsFromExternalSystem;

            this.ModifyDate = DateTime.Now;
        }
    }
}
