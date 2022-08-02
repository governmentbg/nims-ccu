using System;

namespace Eumis.Data.EvalSessions.PortalViewObjects
{
    public class EvalSessionSheetXmlData
    {
        public EvalSessionSheetData SheetData { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string Xml { get; set; }

        public byte[] Version { get; set; }
    }
}
