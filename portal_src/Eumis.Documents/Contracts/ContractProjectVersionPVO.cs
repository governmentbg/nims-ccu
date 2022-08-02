using Eumis.Common.Localization;
using System;

namespace Eumis.Documents.Contracts
{
    public class ContractProjectVersionPVO
    {
        public Guid gid { get; set; }

        public string note { get; set; }

        public string noteAlt { get; set; }

        public string displayNote
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.note, this.noteAlt);
            }
        }

        public DateTime createDate { get; set; }

        public DateTime modifyDate { get; set; }

        public string statusText { get; set; }

        public string statusTextAlt { get; set; }

        public string displayStatusText
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.statusText, this.statusTextAlt);
            }
        }
    }
}
