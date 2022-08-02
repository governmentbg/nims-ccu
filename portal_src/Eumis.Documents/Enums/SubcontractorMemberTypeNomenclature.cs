using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class SubcontractorMemberTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.SubcontractorMemberTypeNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Id { get; set; }
        public string ResourceKey { get; set; }

        public static readonly SubcontractorMemberTypeNomenclature Subcontractor = new SubcontractorMemberTypeNomenclature { ResourceKey = "Subcontractor", Id = "subcontractor" };
        public static readonly SubcontractorMemberTypeNomenclature Member = new SubcontractorMemberTypeNomenclature { ResourceKey = "Member", Id = "member" };

        public IEnumerable<SubcontractorMemberTypeNomenclature> GetItems()
        {
            return new List<SubcontractorMemberTypeNomenclature>() {
                Subcontractor,
                Member
            };
        }
    }
}
