using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    public class AidModeNomenclature
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public const string DeminimisId = "deminimis";
        public const string StateAidId = "stateAid";
        public const string NotApplicableId = "notApplicable";

        public static readonly AidModeNomenclature Deminimis = new AidModeNomenclature { Id = DeminimisId, Name = "de minimis" };
        public static readonly AidModeNomenclature StateAid = new AidModeNomenclature { Id = StateAidId, Name = "Държавна помощ" };
        public static readonly AidModeNomenclature NotApplicable = new AidModeNomenclature { Id = NotApplicableId, Name = "Неприложимо" };

        public IEnumerable<AidModeNomenclature> GetItems()
        {
            return new List<AidModeNomenclature>() {
                Deminimis,
                StateAid,
                NotApplicable
            };
        }
    }
}
