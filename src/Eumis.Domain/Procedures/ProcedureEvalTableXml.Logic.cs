using System;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureEvalTableXml
    {
        public override void SetXml(string xml)
        {
            base.SetXml(xml);

            this.ModifyDate = DateTime.Now;
        }
    }
}
