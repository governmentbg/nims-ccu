using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitorstat.Common.Contracts
{
    public class FileDO
    {
        public string Name { get; set; }

        public int Size { get; set; }
        
        public byte[] Content { get; set; }
    }
}
