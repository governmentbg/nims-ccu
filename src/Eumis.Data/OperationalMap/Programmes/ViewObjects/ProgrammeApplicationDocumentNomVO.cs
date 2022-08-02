using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Data.OperationalMap.Programmes.ViewObjects
{
    public class ProgrammeApplicationDocumentNomVO : EntityNomVO
    {
        public bool IsSignatureRequired { get; set; }

        public string Extension { get; set; }
    }
}
