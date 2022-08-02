using System;

namespace Eumis.Web.Api.Core
{
    public class InternalFileDO
    {
        public InternalFileDO()
        {
        }

        public InternalFileDO(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
