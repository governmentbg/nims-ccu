using System;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularitySignalDO
    {
        public IrregularitySignalDO()
        {
        }

        public IrregularitySignalDO(IrregularitySignal signal)
        {
            this.SignalId = signal.IrregularitySignalId;
            this.RegDate = signal.RegDate;
            this.SignalSource = signal.SignalSource;
            this.MASystemRegDate = signal.MASystemRegDate;
            this.SignalKind = signal.SignalKind;
            this.ViolationDesrc = signal.ViolationDesrc;
            this.Actions = signal.Actions;
            this.ActRegNum = signal.ActRegNum;
            this.ActRegDate = signal.ActRegDate;
            this.Comment = signal.Comment;

            this.Version = signal.Version;
        }

        public int SignalId { get; set; }

        public DateTime? RegDate { get; set; }

        public string SignalSource { get; set; }

        public DateTime? MASystemRegDate { get; set; }

        public string SignalKind { get; set; }

        public string ViolationDesrc { get; set; }

        public string Actions { get; set; }

        public string ActRegNum { get; set; }

        public DateTime? ActRegDate { get; set; }

        public string Comment { get; set; }

        public byte[] Version { get; set; }
    }
}
