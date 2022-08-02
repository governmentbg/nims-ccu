using System.Collections.Generic;

namespace Eumis.Public.Web.Models.Pies
{
    public class PieModel
    {
        public PieModel()
        {
            this.Data = new List<PieDataModel>();
        }

        public string Name { get; set; }

        public bool ColorByPoint { get; set; }

        public IEnumerable<PieDataModel> Data { get; set; }
    }
}