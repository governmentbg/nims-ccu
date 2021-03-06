//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10019
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10019";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_10018.AttachedDocument>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ContractTeamCollection : System.Collections.Generic.List<R_10015.ContractTeam>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CompanyCollection : System.Collections.Generic.List<R_10004.Company>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ProgrammeIndicatorsCollection : System.Collections.Generic.List<R_10014.ProgrammeIndicators>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ProjectSpecFieldCollection : System.Collections.Generic.List<R_10017.ProjectSpecField>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ProjectErrandCollection : System.Collections.Generic.List<R_10016.ProjectErrand>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ProgrammeContractActivitiesCollection : System.Collections.Generic.List<R_09995.ProgrammeContractActivities>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class DirectionsBudgetContractCollection : System.Collections.Generic.List<R_09998.DirectionsBudgetContract>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ElectronicDeclarationCollection : System.Collections.Generic.List<R_10098.ElectronicDeclaration>
	{
	}



	[XmlType(TypeName="Project",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Project
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="version",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __version;
		
		[XmlIgnore]
		public string version
		{ 
			get { return __version; }
			set { __version = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="createDate",DataType="dateTime")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime __createDate;
		
		[XmlIgnore]
		public DateTime createDate
		{ 
		get { return __createDate; }
		set { __createDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="modificationDate",DataType="dateTime")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime __modificationDate;
		
		[XmlIgnore]
		public DateTime modificationDate
		{ 
		get { return __modificationDate; }
		set { __modificationDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10002.ProjectBasicData),ElementName="ProjectBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_10002.ProjectBasicData __ProjectBasicData;
		
		[XmlIgnore]
		public R_10002.ProjectBasicData ProjectBasicData
		{
			get {return __ProjectBasicData;}
			set {__ProjectBasicData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10004.Company),ElementName="Candidate",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_10004.Company __Candidate;
		
		[XmlIgnore]
		public R_10004.Company Candidate
		{
			get {return __Candidate;}
			set {__Candidate = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(Partners),ElementName="Partners",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Partners __Partners;
		
		[XmlIgnore]
		public Partners Partners
		{
			get {return __Partners;}
			set {__Partners = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_09998.DirectionsBudgetContract),ElementName="DirectionsBudgetContract",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DirectionsBudgetContractCollection __DirectionsBudgetContractCollection;
		
		[XmlIgnore]
		public DirectionsBudgetContractCollection DirectionsBudgetContractCollection
		{
			get
			{
				if (__DirectionsBudgetContractCollection == null) __DirectionsBudgetContractCollection = new DirectionsBudgetContractCollection();
				return __DirectionsBudgetContractCollection;
			}
			set {__DirectionsBudgetContractCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_09995.ProgrammeContractActivities),ElementName="ProgrammeContractActivities",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProgrammeContractActivitiesCollection __ProgrammeContractActivitiesCollection;
		
		[XmlIgnore]
		public ProgrammeContractActivitiesCollection ProgrammeContractActivitiesCollection
		{
			get
			{
				if (__ProgrammeContractActivitiesCollection == null) __ProgrammeContractActivitiesCollection = new ProgrammeContractActivitiesCollection();
				return __ProgrammeContractActivitiesCollection;
			}
			set {__ProgrammeContractActivitiesCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10014.ProgrammeIndicators),ElementName="ProgrammeIndicators",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProgrammeIndicatorsCollection __ProgrammeIndicatorsCollection;
		
		[XmlIgnore]
		public ProgrammeIndicatorsCollection ProgrammeIndicatorsCollection
		{
			get
			{
				if (__ProgrammeIndicatorsCollection == null) __ProgrammeIndicatorsCollection = new ProgrammeIndicatorsCollection();
				return __ProgrammeIndicatorsCollection;
			}
			set {__ProgrammeIndicatorsCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(ContractTeams),ElementName="ContractTeams",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ContractTeams __ContractTeams;
		
		[XmlIgnore]
		public ContractTeams ContractTeams
		{
			get {return __ContractTeams;}
			set {__ContractTeams = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(ProjectErrands),ElementName="ProjectErrands",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProjectErrands __ProjectErrands;
		
		[XmlIgnore]
		public ProjectErrands ProjectErrands
		{
			get {return __ProjectErrands;}
			set {__ProjectErrands = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(ProjectSpecFields),ElementName="ProjectSpecFields",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProjectSpecFields __ProjectSpecFields;
		
		[XmlIgnore]
		public ProjectSpecFields ProjectSpecFields
		{
			get {return __ProjectSpecFields;}
			set {__ProjectSpecFields = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(ElectronicDeclarations),ElementName="ElectronicDeclarations",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ElectronicDeclarations __ElectronicDeclarations;
		
		[XmlIgnore]
		public ElectronicDeclarations ElectronicDeclarations
		{
			get {return __ElectronicDeclarations;}
			set {__ElectronicDeclarations = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(AttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AttachedDocuments __AttachedDocuments;
		
		[XmlIgnore]
		public AttachedDocuments AttachedDocuments
		{
			get {return __AttachedDocuments;}
			set {__AttachedDocuments = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(xmldsig.Signature),ElementName="Signature",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public xmldsig.Signature __Signature;
		
		[XmlIgnore]
		public xmldsig.Signature Signature
		{
			get {return __Signature;}
			set {__Signature = value;}
		}

		public Project()
		{
		}
	}


	[XmlType(TypeName="Partners",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Partners
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10004.Company),ElementName="Partner",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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

		public Partners()
		{
		}
	}


	[XmlType(TypeName="ContractTeams",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ContractTeams
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10015.ContractTeam),ElementName="ContractTeam",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ContractTeamCollection __ContractTeamCollection;
		
		[XmlIgnore]
		public ContractTeamCollection ContractTeamCollection
		{
			get
			{
				if (__ContractTeamCollection == null) __ContractTeamCollection = new ContractTeamCollection();
				return __ContractTeamCollection;
			}
			set {__ContractTeamCollection = value;}
		}

		public ContractTeams()
		{
		}
	}


	[XmlType(TypeName="ProjectErrands",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ProjectErrands
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10016.ProjectErrand),ElementName="ProjectErrand",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProjectErrandCollection __ProjectErrandCollection;
		
		[XmlIgnore]
		public ProjectErrandCollection ProjectErrandCollection
		{
			get
			{
				if (__ProjectErrandCollection == null) __ProjectErrandCollection = new ProjectErrandCollection();
				return __ProjectErrandCollection;
			}
			set {__ProjectErrandCollection = value;}
		}

		public ProjectErrands()
		{
		}
	}


	[XmlType(TypeName="ProjectSpecFields",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ProjectSpecFields
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10017.ProjectSpecField),ElementName="ProjectSpecField",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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

		public ProjectSpecFields()
		{
		}
	}


	[XmlType(TypeName="ElectronicDeclarations",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ElectronicDeclarations
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isInitialized",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isInitialized;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isInitializedSpecified;
		
		[XmlIgnore]
		public bool isInitialized
		{ 
			get { return __isInitialized; }
			set { __isInitialized = value; __isInitializedSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10098.ElectronicDeclaration),ElementName="ElectronicDeclaration",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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

		public ElectronicDeclarations()
		{
		}
	}


	[XmlType(TypeName="AttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class AttachedDocuments
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10018.AttachedDocument),ElementName="AttachedDocument",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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

		public AttachedDocuments()
		{
		}
	}
}
