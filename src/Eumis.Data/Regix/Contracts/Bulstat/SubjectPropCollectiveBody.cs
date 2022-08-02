using SubjectRelCollectiveBodyMemberCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.SubjectRelCollectiveBodyMember>;

namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class SubjectPropCollectiveBody : Entry
    {
        public NomenclatureEntry Type { get; set; }

        public SubjectRelCollectiveBodyMemberCollection MembersCollection { get; set; }
    }
}
