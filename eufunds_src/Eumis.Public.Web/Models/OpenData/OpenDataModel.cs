using System.Collections.Generic;
using System.Web.Mvc;

namespace Eumis.Public.Web.Models.OpenData
{
    public class OpenDataModel
    {
        public string ProgrammeId { get; set; }

        public IEnumerable<SelectListItem> Programs { get; set; }
    }
}
