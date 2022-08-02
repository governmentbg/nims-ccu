using System;
using System.Diagnostics.CodeAnalysis;
using BoardMemberTypeCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Mp.BoardMemberType>;

namespace Eumis.Data.Regix.Contracts.Mp
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class NPODetailsResponseType
    {
        public string RegistrationNumber { get; set; }

        public string Name { get; set; }

        public string OrgForm { get; set; }

        public string Address { get; set; }

        public string ContactInfo { get; set; }

        public string CourtName { get; set; }

        public string LotNumber { get; set; }

        public string CourtCase { get; set; }

        public string NationalityCode { get; set; }

        public string Nationality { get; set; }

        public string BoardType { get; set; }

        public BoardMembersType BoardMembers { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class BoardMembersType
    {
        public BoardMemberTypeCollection BoardMemberCollection { get; set; }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Contract classes should be in the same file for simplicity")]
    public partial class BoardMemberType
    {
        public string Name { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }

    public partial class NPODetailsResponse : NPODetailsResponseType
    {
        public NPODetailsResponse()
            : base()
        {
        }
    }
}
