using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Interfaces
{
    public interface IDocumentNomenclatures
    {
        Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>> Nomenclatures { get; set; }
    }
}
