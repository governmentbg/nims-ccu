//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace Eumis.Rio
{


	using BFPContractContractTeamCollection = System.Collections.Generic.List<Eumis.Rio.BFPContractContractTeam>;

	using ProjectSpecFieldCollection = System.Collections.Generic.List<Eumis.Rio.ProjectSpecField>;

	using BFPContractPlanCollection = System.Collections.Generic.List<Eumis.Rio.BFPContractPlan>;

	using ElectronicDeclarationCollection = System.Collections.Generic.List<Eumis.Rio.ElectronicDeclaration>;

	using BFPContractContractActivityCollection = System.Collections.Generic.List<Eumis.Rio.BFPContractContractActivity>;

	using AttachedDocumentCollection = System.Collections.Generic.List<Eumis.Rio.AttachedDocument>;

	using CompanyCollection = System.Collections.Generic.List<Eumis.Rio.Company>;

	using BFPContractIndicatorCollection = System.Collections.Generic.List<Eumis.Rio.BFPContractIndicator>;



	[XmlType(TypeName="BFPContract",Namespace="http://ereg.egov.bg/segment/R-10040"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class BFPContract
	{

		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="version",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __version;
		
		[XmlIgnore]
		public string version
		{ 
			get { return __version; }
			set { __version = value; }
		}

		[XmlAttribute(AttributeName="type")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.BFPContractTypeNomenclature __type;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __typeSpecified;
		
		[XmlIgnore]
		public Eumis.Rio.BFPContractTypeNomenclature type
		{ 
			get { return __type; }
			set { __type = value; __typeSpecified = true; }
		}

		[XmlAttribute(AttributeName="projectRegNumber",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __projectRegNumber;
		
		[XmlIgnore]
		public string projectRegNumber
		{ 
			get { return __projectRegNumber; }
			set { __projectRegNumber = value; }
		}

		[XmlAttribute(AttributeName="contractRegNumber",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __contractRegNumber;
		
		[XmlIgnore]
		public string contractRegNumber
		{ 
			get { return __contractRegNumber; }
			set { __contractRegNumber = value; }
		}

		[XmlAttribute(AttributeName="contractGid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __contractGid;
		
		[XmlIgnore]
		public string contractGid
		{ 
			get { return __contractGid; }
			set { __contractGid = value; }
		}

		[XmlAttribute(AttributeName="createDate",DataType="dateTime")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime __createDate;
		
		[XmlIgnore]
		public DateTime createDate
		{ 
		get { return __createDate; }
		set { __createDate = value; }
		}
		


		[XmlAttribute(AttributeName="modificationDate",DataType="dateTime")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime __modificationDate;
		
		[XmlIgnore]
		public DateTime modificationDate
		{ 
		get { return __modificationDate; }
		set { __modificationDate = value; }
		}
		


		[XmlElement(Type=typeof(Eumis.Rio.BFPContractBasicData),ElementName="BFPContractBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.BFPContractBasicData __BFPContractBasicData;
		
		[XmlIgnore]
		public Eumis.Rio.BFPContractBasicData BFPContractBasicData
		{
			get {return __BFPContractBasicData;}
			set {__BFPContractBasicData = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.Company),ElementName="Beneficiary",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.Company __Beneficiary;
		
		[XmlIgnore]
		public Eumis.Rio.Company Beneficiary
		{
			get {return __Beneficiary;}
			set {__Beneficiary = value;}
		}

		[XmlElement(Type=typeof(BFPContractPartners),ElementName="Partners",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractPartners __Partners;
		
		[XmlIgnore]
		public BFPContractPartners Partners
		{
			get {return __Partners;}
			set {__Partners = value;}
		}

		[XmlElement(Type=typeof(BFPContractDirectionsBudgetContract),ElementName="BFPContractDirectionsBudgetContract",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractDirectionsBudgetContract __BFPContractDirectionsBudgetContract;
		
		[XmlIgnore]
		public BFPContractDirectionsBudgetContract BFPContractDirectionsBudgetContract
		{
			get {return __BFPContractDirectionsBudgetContract;}
			set {__BFPContractDirectionsBudgetContract = value;}
		}

		[XmlElement(Type=typeof(BFPContractContractActivities),ElementName="BFPContractContractActivities",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractContractActivities __BFPContractContractActivities;
		
		[XmlIgnore]
		public BFPContractContractActivities BFPContractContractActivities
		{
			get {return __BFPContractContractActivities;}
			set {__BFPContractContractActivities = value;}
		}

		[XmlElement(Type=typeof(BFPContractIndicators),ElementName="BFPContractIndicators",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractIndicators __BFPContractIndicators;
		
		[XmlIgnore]
		public BFPContractIndicators BFPContractIndicators
		{
			get {return __BFPContractIndicators;}
			set {__BFPContractIndicators = value;}
		}

		[XmlElement(Type=typeof(BFPContractContractTeams),ElementName="BFPContractContractTeams",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractContractTeams __BFPContractContractTeams;
		
		[XmlIgnore]
		public BFPContractContractTeams BFPContractContractTeams
		{
			get {return __BFPContractContractTeams;}
			set {__BFPContractContractTeams = value;}
		}

		[XmlElement(Type=typeof(BFPContractPlans),ElementName="BFPContractPlans",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractPlans __BFPContractPlans;
		
		[XmlIgnore]
		public BFPContractPlans BFPContractPlans
		{
			get {return __BFPContractPlans;}
			set {__BFPContractPlans = value;}
		}

		[XmlElement(Type=typeof(BFPContractProjectSpecFields),ElementName="ProjectSpecFields",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractProjectSpecFields __ProjectSpecFields;
		
		[XmlIgnore]
		public BFPContractProjectSpecFields ProjectSpecFields
		{
			get {return __ProjectSpecFields;}
			set {__ProjectSpecFields = value;}
		}

		[XmlElement(Type=typeof(BFPContractElectronicDeclarations),ElementName="ElectronicDeclarations",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractElectronicDeclarations __ElectronicDeclarations;
		
		[XmlIgnore]
		public BFPContractElectronicDeclarations ElectronicDeclarations
		{
			get {return __ElectronicDeclarations;}
			set {__ElectronicDeclarations = value;}
		}

		[XmlElement(Type=typeof(BFPContractAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractAttachedDocuments __AttachedDocuments;
		
		[XmlIgnore]
		public BFPContractAttachedDocuments AttachedDocuments
		{
			get {return __AttachedDocuments;}
			set {__AttachedDocuments = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.Signature),ElementName="Signature",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.Signature __Signature;
		
		[XmlIgnore]
		public Eumis.Rio.Signature Signature
		{
			get {return __Signature;}
			set {__Signature = value;}
		}

		public BFPContract()
		{
		}
	}


	[XmlType(TypeName="BFPContractPartners",Namespace="http://ereg.egov.bg/segment/R-10040"),Serializable]
	public partial class BFPContractPartners
	{


		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.Company),ElementName="Partner",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CompanyCollection __PartnerCollection;
		
		[XmlIgnore]
		public CompanyCollection PartnerCollection
		{
			get
			{
				if (__PartnerCollection == null) __PartnerCollection = new CompanyCollection();
				return __PartnerCollection;
			}
			set {__PartnerCollection = value;}
		}

		public BFPContractPartners()
		{
		}
	}


	[XmlType(TypeName="BFPContractDirectionsBudgetContract",Namespace="http://ereg.egov.bg/segment/R-10040"),Serializable]
	public partial class BFPContractDirectionsBudgetContract
	{

		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.DirectionSection),ElementName="Directions",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.DirectionSection __Directions;
		
		[XmlIgnore]
		public Eumis.Rio.DirectionSection Directions
		{
			get {return __Directions;}
			set {__Directions = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.BFPContractBudget),ElementName="BFPContractBudget",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.BFPContractBudget __BFPContractBudget;
		
		[XmlIgnore]
		public Eumis.Rio.BFPContractBudget BFPContractBudget
		{
			get {return __BFPContractBudget;}
			set {__BFPContractBudget = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.Contract),ElementName="Contract",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.Contract __Contract;
		
		[XmlIgnore]
		public Eumis.Rio.Contract Contract
		{
			get {return __Contract;}
			set {__Contract = value;}
		}

		public BFPContractDirectionsBudgetContract()
		{
		}
	}


	[XmlType(TypeName="BFPContractContractActivities",Namespace="http://ereg.egov.bg/segment/R-10040"),Serializable]
	public partial class BFPContractContractActivities
	{


		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.BFPContractContractActivity),ElementName="BFPContractContractActivity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractContractActivityCollection __BFPContractContractActivityCollection;
		
		[XmlIgnore]
		public BFPContractContractActivityCollection BFPContractContractActivityCollection
		{
			get
			{
				if (__BFPContractContractActivityCollection == null) __BFPContractContractActivityCollection = new BFPContractContractActivityCollection();
				return __BFPContractContractActivityCollection;
			}
			set {__BFPContractContractActivityCollection = value;}
		}

		public BFPContractContractActivities()
		{
		}
	}


	[XmlType(TypeName="BFPContractIndicators",Namespace="http://ereg.egov.bg/segment/R-10040"),Serializable]
	public partial class BFPContractIndicators
	{


		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.BFPContractIndicator),ElementName="BFPContractIndicator",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractIndicatorCollection __BFPContractIndicatorCollection;
		
		[XmlIgnore]
		public BFPContractIndicatorCollection BFPContractIndicatorCollection
		{
			get
			{
				if (__BFPContractIndicatorCollection == null) __BFPContractIndicatorCollection = new BFPContractIndicatorCollection();
				return __BFPContractIndicatorCollection;
			}
			set {__BFPContractIndicatorCollection = value;}
		}

		public BFPContractIndicators()
		{
		}
	}


	[XmlType(TypeName="BFPContractContractTeams",Namespace="http://ereg.egov.bg/segment/R-10040"),Serializable]
	public partial class BFPContractContractTeams
	{


		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.BFPContractContractTeam),ElementName="BFPContractContractTeam",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractContractTeamCollection __BFPContractContractTeamCollection;
		
		[XmlIgnore]
		public BFPContractContractTeamCollection BFPContractContractTeamCollection
		{
			get
			{
				if (__BFPContractContractTeamCollection == null) __BFPContractContractTeamCollection = new BFPContractContractTeamCollection();
				return __BFPContractContractTeamCollection;
			}
			set {__BFPContractContractTeamCollection = value;}
		}

		public BFPContractContractTeams()
		{
		}
	}


	[XmlType(TypeName="BFPContractPlans",Namespace="http://ereg.egov.bg/segment/R-10040"),Serializable]
	public partial class BFPContractPlans
	{


		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.BFPContractPlan),ElementName="BFPContractPlan",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractPlanCollection __BFPContractPlanCollection;
		
		[XmlIgnore]
		public BFPContractPlanCollection BFPContractPlanCollection
		{
			get
			{
				if (__BFPContractPlanCollection == null) __BFPContractPlanCollection = new BFPContractPlanCollection();
				return __BFPContractPlanCollection;
			}
			set {__BFPContractPlanCollection = value;}
		}

		public BFPContractPlans()
		{
		}
	}


	[XmlType(TypeName="BFPContractProjectSpecFields",Namespace="http://ereg.egov.bg/segment/R-10040"),Serializable]
	public partial class BFPContractProjectSpecFields
	{


		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.ProjectSpecField),ElementName="ProjectSpecField",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProjectSpecFieldCollection __ProjectSpecFieldCollection;
		
		[XmlIgnore]
		public ProjectSpecFieldCollection ProjectSpecFieldCollection
		{
			get
			{
				if (__ProjectSpecFieldCollection == null) __ProjectSpecFieldCollection = new ProjectSpecFieldCollection();
				return __ProjectSpecFieldCollection;
			}
			set {__ProjectSpecFieldCollection = value;}
		}

		public BFPContractProjectSpecFields()
		{
		}
	}


	[XmlType(TypeName="BFPContractElectronicDeclarations",Namespace="http://ereg.egov.bg/segment/R-10040"),Serializable]
	public partial class BFPContractElectronicDeclarations
	{


		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.ElectronicDeclaration),ElementName="ElectronicDeclaration",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ElectronicDeclarationCollection __ElectronicDeclarationCollection;
		
		[XmlIgnore]
		public ElectronicDeclarationCollection ElectronicDeclarationCollection
		{
			get
			{
				if (__ElectronicDeclarationCollection == null) __ElectronicDeclarationCollection = new ElectronicDeclarationCollection();
				return __ElectronicDeclarationCollection;
			}
			set {__ElectronicDeclarationCollection = value;}
		}

		public BFPContractElectronicDeclarations()
		{
		}
	}


	[XmlType(TypeName="BFPContractAttachedDocuments",Namespace="http://ereg.egov.bg/segment/R-10040"),Serializable]
	public partial class BFPContractAttachedDocuments
	{


		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.AttachedDocument),ElementName="AttachedDocument",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10040")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AttachedDocumentCollection __AttachedDocumentCollection;
		
		[XmlIgnore]
		public AttachedDocumentCollection AttachedDocumentCollection
		{
			get
			{
				if (__AttachedDocumentCollection == null) __AttachedDocumentCollection = new AttachedDocumentCollection();
				return __AttachedDocumentCollection;
			}
			set {__AttachedDocumentCollection = value;}
		}

		public BFPContractAttachedDocuments()
		{
		}
	}
}