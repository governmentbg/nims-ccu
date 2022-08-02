using Eumis.Documents.Contracts;
using R_10093;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10093
{
    public partial class DirectionSection
    {
        [XmlIgnore]
        public IEnumerable<Direction> Items { get; set; }

        public static List<Direction> Load(List<ContractDirectionPair> directions)
        {
            List<Direction> result = new List<Direction>();

            if (directions != null)
            {
                result = directions.Select(d => new Direction
                {
                    DirectionItem = d.direction,
                    SubDirection = d.subDirection,

                }).ToList();
            }

            return result;
        }
    }
}
