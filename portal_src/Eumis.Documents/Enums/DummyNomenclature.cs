using Eumis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class DummyNomenclature
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public static readonly DummyNomenclature El1 = new DummyNomenclature { Name = "Елемент 1", Code = "1" };
        public static readonly DummyNomenclature El2 = new DummyNomenclature { Name = "Елемент 2", Code = "2" };
        public static readonly DummyNomenclature El3 = new DummyNomenclature { Name = "Елемент 3", Code = "3" };
        public static readonly DummyNomenclature El4 = new DummyNomenclature { Name = "Елемент 4", Code = "4" };

        public IEnumerable<SerializableSelectListItem> GetItems()
        {
            return new List<DummyNomenclature>() {
                El1,
                El2,
                El3,
                El4
            }.Select(e => new SerializableSelectListItem() { Text = e.Name, Value = e.Code }).ToList();
        }
    }
}
