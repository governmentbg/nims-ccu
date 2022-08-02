namespace Eumis.Data.OperationalMap.Programmes.ViewObjects
{
    public class ProgrammeApplicationDocumentsVO
    {
        public int ProgrammeApplicationDocumentId { get; set; }

        public int ProgrammeId { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsSignatureRequired { get; set; }

        public bool IsActive { get; set; }
    }
}
