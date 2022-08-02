using Eumis.Public.Common;
using Eumis.Public.Common.Helpers;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Eumis.Public.Data.Calendar.ViewObjects
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "JSON response")]
    public class EventSummaryVO
    {
        private DateTime? date;
        private Uri localUrl;

        public DateTime? Date
        {
            get
            {
                return this.date;
            }

            set
            {
                this.date = value;
            }
        }

        public string doc_date
        {
            get
            {
                return string.Format("{0:" + Configuration.CALENDAR_FORMAT_DAY + "}", this.date);
            }
        }

        public int count { get; set; }

        public string url
        {
            get => this.localUrl.ToAbsolute();

            set
            {
                this.localUrl = new Uri(value + string.Format("{0:" + Configuration.CALENDAR_FORMAT_DATE + "}", this.date), UriKind.Relative);
            }
        }
    }
}
