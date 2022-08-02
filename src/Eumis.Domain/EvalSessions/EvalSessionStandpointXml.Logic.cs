using System;

namespace Eumis.Domain.EvalSessions
{
    public partial class EvalSessionStandpointXml : IAggregateRoot
    {
        #region EvalSessionStandpointXml

        public override void SetXml(string xml)
        {
            base.SetXml(xml);

            this.ModifyDate = DateTime.Now;
        }

        #endregion // EvalSessionStandpointXml
    }
}
