using SubjectPropActivityKID2008Collection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.SubjectPropActivityKID2008>;
using SubjectPropCollectiveBodyCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.SubjectPropCollectiveBody>;
using SubjectPropFundingSourceCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.SubjectPropFundingSource>;
using SubjectPropInstallmentsCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.SubjectPropInstallments>;
using SubjectPropOwnershipFormCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.SubjectPropOwnershipForm>;
using SubjectPropProfessionCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.SubjectPropProfession>;
using SubjectRelManagerCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.SubjectRelManager>;
using SubjectRelPartnerCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.SubjectRelPartner>;

namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class StateOfPlay
    {
        public Subject Subject { get; set; }

        public Event Event { get; set; }

        public SubjectPropRepresentationType RepresentationType { get; set; }

        public SubjectPropScopeOfActivity ScopeOfActivity { get; set; }

        public SubjectPropActivityKID2008 MainActivity2008 { get; set; }

        public SubjectPropActivityKID2003 MainActivity2003 { get; set; }

        public SubjectPropInstallmentsCollection InstallmentsCollection { get; set; }

        public SubjectPropLifeTime LifeTime { get; set; }

        public SubjectPropAccountingRecordForm AccountingRecordForm { get; set; }

        public SubjectPropOwnershipFormCollection OwnershipFormsCollection { get; set; }

        public SubjectPropFundingSourceCollection FundingSourcesCollection { get; set; }

        public SubjectPropState State { get; set; }

        public SubjectRelManagerCollection ManagersCollection { get; set; }

        public SubjectRelPartnerCollection PartnersCollection { get; set; }

        public SubjectRelAssignee Assignee { get; set; }

        public SubjectRelBelonging Belonging { get; set; }

        public SubjectPropCollectiveBodyCollection CollectiveBodiesCollection { get; set; }

        public SubjectPropActivityDate ActivityDate { get; set; }

        public SubjectPropActivityKID2008Collection AdditionalActivities2008Collection { get; set; }

        public SubjectPropProfessionCollection ProfessionsCollection { get; set; }
    }
}
