using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;

namespace Eumis.Domain.Indicators
{
    public partial class Indicator : IAggregateRoot
    {
        public void UpdateAttributes(
            int measureId,
            string name,
            string nameAlt,
            bool hasGenderDivision)
        {
            this.MeasureId = measureId;
            this.Name = name;
            this.NameAlt = nameAlt;
            this.HasGenderDivision = hasGenderDivision;

            this.ModifyDate = DateTime.Now;

            var indicatorChangedEvent = new ProgrammeNotificationEvent(NotificationEventType.IndicatorChanged, this.IndicatorId, this.ProgrammeId);
            ((INotificationEventEmitter)this).NotificationEvents.Add(indicatorChangedEvent);
        }
    }
}
