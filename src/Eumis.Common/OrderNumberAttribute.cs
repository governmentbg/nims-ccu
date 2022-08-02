using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common
{
    [AttributeUsage(AttributeTargets.All)]
    public class OrderNumberAttribute : Attribute
    {
        public OrderNumberAttribute()
        {
        }

        public OrderNumberAttribute(int order)
        {
            this.OrderNum = order;
        }

        public int OrderNum { get; }
    }
}
