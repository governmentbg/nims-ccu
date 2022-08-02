using System;
using Eumis.Rio;

namespace Eumis.Domain.EvalSessions
{
    public partial class EvalSessionSheetXml : IAggregateRoot
    {
        #region EvalSessionSheetXml

        public override void SetXml(string xml)
        {
            base.SetXml(xml);

            this.ModifyDate = DateTime.Now;
        }

        public void Submit()
        {
            var sessionDoc = this.GetDocument();

            if (sessionDoc.type == EvalTypeNomenclature.Rejection)
            {
                this.EvalIsPassed = sessionDoc.IsSuccess;
                this.EvalNote = sessionDoc.Note;
            }
            else
            {
                this.EvalIsPassed = sessionDoc.IsSuccess;
                this.EvalPoints = sessionDoc.Total;
                this.EvalNote = sessionDoc.Note;
            }

            this.ModifyDate = DateTime.Now;
        }

        public void Pause()
        {
            this.EvalNote = this.GetDocument().Note;
        }

        #endregion // EvalSessionSheetXml
    }
}
