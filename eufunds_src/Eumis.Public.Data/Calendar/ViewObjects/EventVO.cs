using Eumis.Public.Common;
using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Custom.Events;
using Eumis.Public.Resources;
using System;

namespace Eumis.Public.Data.Calendar.ViewObjects
{
    public class EventVO
    {
        private DateTime date;

        public DateTime Date
        {
            get
            {
                if (this.EventType == EventType.Procurement)
                {
                    return this.date.AddHours(23).AddMinutes(59).AddSeconds(59);
                }

                return this.date;
            }

            set
            {
                this.date = value;
            }
        }

        public EventType EventType { get; set; }

        public int? SourceId { get; set; }

        public Guid? Guid { get; set; }

        public string Title { get; set; }

        public string TitleEN { get; set; }

        public string TransTitle
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.TitleEN) ? this.Title : this.TitleEN;
                }
                else
                {
                    return this.Title;
                }
            }
        }

        public string Description { get; set; }

        public string DescriptionEN { get; set; }

        public string TransDescription
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.DescriptionEN) ? this.Description : this.DescriptionEN;
                }
                else
                {
                    return this.Description;
                }
            }
        }

        public string GetUrl()
        {
            var url = string.Empty;

            switch (this.EventType)
            {
                case EventType.Procedure:
                    url = string.Format("{0}/{1}/s/Procedure/Info/{2}", Configuration.PortalLocation, SystemLocalization.GetCurrentCulture(), this.Guid);
                    break;
                case EventType.ProcedureEnded:
                    url = string.Format("{0}/{1}/s/Procedure/InfoEnded/{2}", Configuration.PortalLocation, SystemLocalization.GetCurrentCulture(), this.Guid);
                    break;
                case EventType.Procurement:
                    url = string.Format("{0}/{1}/s/Offers/Details/{2}", Configuration.PortalLocation, SystemLocalization.GetCurrentCulture(), this.Guid);
                    break;
                case EventType.PublicDiscussion:
                    url = string.Format("{0}/{1}/s/Procedure/InfoPublicDiscussion/{2}", Configuration.PortalLocation, SystemLocalization.GetCurrentCulture(), this.Guid);
                    break;
                case EventType.PublicDiscussionEnded:
                    url = string.Format("{0}/{1}/s/Procedure/InfoArchivedPublicDiscussion/{2}", Configuration.PortalLocation, SystemLocalization.GetCurrentCulture(), this.Guid);
                    break;
                default:
                    break;
            }

            return url;
        }
    }
}
