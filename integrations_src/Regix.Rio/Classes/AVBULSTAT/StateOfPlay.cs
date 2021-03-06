//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace Eumis.RegiX.Rio.AVBULSTAT
{


	using SubjectPropOwnershipFormCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.SubjectPropOwnershipForm>;

	using SubjectPropCollectiveBodyCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.SubjectPropCollectiveBody>;

	using SubjectRelPartnerCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.SubjectRelPartner>;

	using SubjectRelManagerCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.SubjectRelManager>;

	using SubjectPropActivityKID2008Collection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityKID2008>;

	using SubjectPropInstallmentsCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.SubjectPropInstallments>;

	using SubjectPropProfessionCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.SubjectPropProfession>;

	using SubjectPropFundingSourceCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.SubjectPropFundingSource>;



	[XmlRoot(ElementName="StateOfPlay",Namespace="http://www.bulstat.bg/StateOfPlay",IsNullable=false),Serializable]
	[XmlType(TypeName="StateOfPlay",Namespace="http://www.bulstat.bg/StateOfPlay")]
	public partial class StateOfPlay
	{

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.Subject),ElementName="Subject",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.Subject __Subject;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.Subject Subject
		{
			get {return __Subject;}
			set {__Subject = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.@Event),ElementName="Event",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.@Event __Event;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.@Event @Event
		{
			get {return __Event;}
			set {__Event = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropRepresentationType),ElementName="RepresentationType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropRepresentationType __RepresentationType;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropRepresentationType RepresentationType
		{
			get {return __RepresentationType;}
			set {__RepresentationType = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropScopeOfActivity),ElementName="ScopeOfActivity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropScopeOfActivity __ScopeOfActivity;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropScopeOfActivity ScopeOfActivity
		{
			get {return __ScopeOfActivity;}
			set {__ScopeOfActivity = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityKID2008),ElementName="MainActivity2008",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityKID2008 __MainActivity2008;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityKID2008 MainActivity2008
		{
			get {return __MainActivity2008;}
			set {__MainActivity2008 = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityKID2003),ElementName="MainActivity2003",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityKID2003 __MainActivity2003;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityKID2003 MainActivity2003
		{
			get {return __MainActivity2003;}
			set {__MainActivity2003 = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropInstallments),ElementName="Installments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubjectPropInstallmentsCollection __InstallmentsCollection;
		
		[XmlIgnore]
		public SubjectPropInstallmentsCollection InstallmentsCollection
		{
			get
			{
				if (__InstallmentsCollection == null) __InstallmentsCollection = new SubjectPropInstallmentsCollection();
				return __InstallmentsCollection;
			}
			set {__InstallmentsCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropLifeTime),ElementName="LifeTime",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropLifeTime __LifeTime;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropLifeTime LifeTime
		{
			get {return __LifeTime;}
			set {__LifeTime = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropAccountingRecordForm),ElementName="AccountingRecordForm",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropAccountingRecordForm __AccountingRecordForm;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropAccountingRecordForm AccountingRecordForm
		{
			get {return __AccountingRecordForm;}
			set {__AccountingRecordForm = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropOwnershipForm),ElementName="OwnershipForms",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubjectPropOwnershipFormCollection __OwnershipFormsCollection;
		
		[XmlIgnore]
		public SubjectPropOwnershipFormCollection OwnershipFormsCollection
		{
			get
			{
				if (__OwnershipFormsCollection == null) __OwnershipFormsCollection = new SubjectPropOwnershipFormCollection();
				return __OwnershipFormsCollection;
			}
			set {__OwnershipFormsCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropFundingSource),ElementName="FundingSources",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubjectPropFundingSourceCollection __FundingSourcesCollection;
		
		[XmlIgnore]
		public SubjectPropFundingSourceCollection FundingSourcesCollection
		{
			get
			{
				if (__FundingSourcesCollection == null) __FundingSourcesCollection = new SubjectPropFundingSourceCollection();
				return __FundingSourcesCollection;
			}
			set {__FundingSourcesCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropState),ElementName="State",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropState __State;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropState State
		{
			get {return __State;}
			set {__State = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectRelManager),ElementName="Managers",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubjectRelManagerCollection __ManagersCollection;
		
		[XmlIgnore]
		public SubjectRelManagerCollection ManagersCollection
		{
			get
			{
				if (__ManagersCollection == null) __ManagersCollection = new SubjectRelManagerCollection();
				return __ManagersCollection;
			}
			set {__ManagersCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectRelPartner),ElementName="Partners",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubjectRelPartnerCollection __PartnersCollection;
		
		[XmlIgnore]
		public SubjectRelPartnerCollection PartnersCollection
		{
			get
			{
				if (__PartnersCollection == null) __PartnersCollection = new SubjectRelPartnerCollection();
				return __PartnersCollection;
			}
			set {__PartnersCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectRelAssignee),ElementName="Assignee",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectRelAssignee __Assignee;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectRelAssignee Assignee
		{
			get {return __Assignee;}
			set {__Assignee = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectRelBelonging),ElementName="Belonging",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectRelBelonging __Belonging;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectRelBelonging Belonging
		{
			get {return __Belonging;}
			set {__Belonging = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropCollectiveBody),ElementName="CollectiveBodies",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubjectPropCollectiveBodyCollection __CollectiveBodiesCollection;
		
		[XmlIgnore]
		public SubjectPropCollectiveBodyCollection CollectiveBodiesCollection
		{
			get
			{
				if (__CollectiveBodiesCollection == null) __CollectiveBodiesCollection = new SubjectPropCollectiveBodyCollection();
				return __CollectiveBodiesCollection;
			}
			set {__CollectiveBodiesCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityDate),ElementName="ActivityDate",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityDate __ActivityDate;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityDate ActivityDate
		{
			get {return __ActivityDate;}
			set {__ActivityDate = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropActivityKID2008),ElementName="AdditionalActivities2008",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubjectPropActivityKID2008Collection __AdditionalActivities2008Collection;
		
		[XmlIgnore]
		public SubjectPropActivityKID2008Collection AdditionalActivities2008Collection
		{
			get
			{
				if (__AdditionalActivities2008Collection == null) __AdditionalActivities2008Collection = new SubjectPropActivityKID2008Collection();
				return __AdditionalActivities2008Collection;
			}
			set {__AdditionalActivities2008Collection = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.SubjectPropProfession),ElementName="Professions",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/StateOfPlay")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubjectPropProfessionCollection __ProfessionsCollection;
		
		[XmlIgnore]
		public SubjectPropProfessionCollection ProfessionsCollection
		{
			get
			{
				if (__ProfessionsCollection == null) __ProfessionsCollection = new SubjectPropProfessionCollection();
				return __ProfessionsCollection;
			}
			set {__ProfessionsCollection = value;}
		}

		public StateOfPlay()
		{
		}
	}
}
