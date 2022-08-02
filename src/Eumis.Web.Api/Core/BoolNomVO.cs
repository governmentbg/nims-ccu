using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Web.Api.Core
{
    public class BoolNomVO : IEquatable<BoolNomVO>
    {
        private bool nomValueId;
        private string name;

        public BoolNomVO(bool nomValueId, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name cannot be null or empty.");
            }

            this.nomValueId = nomValueId;
            this.name = name;
        }

        public bool NomValueId
        {
            get { return this.nomValueId; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public override int GetHashCode()
        {
            return this.nomValueId.GetHashCode() ^ this.name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((BoolNomVO)obj);
        }

        public bool Equals(BoolNomVO other)
        {
            return other.nomValueId.Equals(this.nomValueId) && other.name.Equals(this.name);
        }
    }
}
