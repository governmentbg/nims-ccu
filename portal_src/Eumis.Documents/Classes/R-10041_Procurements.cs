//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10041
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10041";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ProcurementPlanCollection : System.Collections.Generic.List<R_10048.ProcurementPlan>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class BFPContractContractActivityCollection : System.Collections.Generic.List<R_10037.BFPContractContractActivity>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PrivateNomenclatureCollection : System.Collections.Generic.List<R_10000.PrivateNomenclature>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class BFPContractProgrammeDetailsExpenseBudgetCollection : System.Collections.Generic.List<R_10033.BFPContractProgrammeDetailsExpenseBudget>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ContractorCollection : System.Collections.Generic.List<R_10046.Contractor>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ContractContractorCollection : System.Collections.Generic.List<R_10047.ContractContractor>
	{
	}



	[XmlType(TypeName="Procurements",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Procurements
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
		[XmlAttribute(AttributeName="contractGid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __contractGid;
		
		[XmlIgnore]
		public string contractGid
		{ 
			get { return __contractGid; }
			set { __contractGid = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="contractVersionGid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __contractVersionGid;
		
		[XmlIgnore]
		public string contractVersionGid
		{ 
			get { return __contractVersionGid; }
			set { __contractVersionGid = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="orderNum",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __orderNum;
		
		[XmlIgnore]
		public string orderNum
		{ 
			get { return __orderNum; }
			set { __orderNum = value; }
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
		[XmlElement(Type=typeof(Contractors),ElementName="Contractors",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Contractors __Contractors;
		
		[XmlIgnore]
		public Contractors Contractors
		{
			get {return __Contractors;}
			set {__Contractors = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(ContractContractors),ElementName="ContractContractors",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ContractContractors __ContractContractors;
		
		[XmlIgnore]
		public ContractContractors ContractContractors
		{
			get {return __ContractContractors;}
			set {__ContractContractors = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(ProcurementPlans),ElementName="ProcurementPlans",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProcurementPlans __ProcurementPlans;
		
		[XmlIgnore]
		public ProcurementPlans ProcurementPlans
		{
			get {return __ProcurementPlans;}
			set {__ProcurementPlans = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10000.PrivateNomenclature),ElementName="BudgetLevel3Item",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PrivateNomenclatureCollection __BudgetLevel3ItemCollection;
		
		[XmlIgnore]
		public PrivateNomenclatureCollection BudgetLevel3ItemCollection
		{
			get
			{
				if (__BudgetLevel3ItemCollection == null) __BudgetLevel3ItemCollection = new PrivateNomenclatureCollection();
				return __BudgetLevel3ItemCollection;
			}
			set {__BudgetLevel3ItemCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10000.PrivateNomenclature),ElementName="ContractActivityItem",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PrivateNomenclatureCollection __ContractActivityItemCollection;
		
		[XmlIgnore]
		public PrivateNomenclatureCollection ContractActivityItemCollection
		{
			get
			{
				if (__ContractActivityItemCollection == null) __ContractActivityItemCollection = new PrivateNomenclatureCollection();
				return __ContractActivityItemCollection;
			}
			set {__ContractActivityItemCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10033.BFPContractProgrammeDetailsExpenseBudget),ElementName="BudgetLevel3",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractProgrammeDetailsExpenseBudgetCollection __BudgetLevel3Collection;
		
		[XmlIgnore]
		public BFPContractProgrammeDetailsExpenseBudgetCollection BudgetLevel3Collection
		{
			get
			{
				if (__BudgetLevel3Collection == null) __BudgetLevel3Collection = new BFPContractProgrammeDetailsExpenseBudgetCollection();
				return __BudgetLevel3Collection;
			}
			set {__BudgetLevel3Collection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10037.BFPContractContractActivity),ElementName="ContractActivity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractContractActivityCollection __ContractActivityCollection;
		
		[XmlIgnore]
		public BFPContractContractActivityCollection ContractActivityCollection
		{
			get
			{
				if (__ContractActivityCollection == null) __ContractActivityCollection = new BFPContractContractActivityCollection();
				return __ContractActivityCollection;
			}
			set {__ContractActivityCollection = value;}
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

		public Procurements()
		{
		}
	}


	[XmlType(TypeName="Contractors",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Contractors
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
		[XmlElement(Type=typeof(R_10046.Contractor),ElementName="Contractor",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ContractorCollection __ContractorCollection;
		
		[XmlIgnore]
		public ContractorCollection ContractorCollection
		{
			get
			{
				if (__ContractorCollection == null) __ContractorCollection = new ContractorCollection();
				return __ContractorCollection;
			}
			set {__ContractorCollection = value;}
		}

		public Contractors()
		{
		}
	}


	[XmlType(TypeName="ContractContractors",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ContractContractors
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
		[XmlElement(Type=typeof(R_10047.ContractContractor),ElementName="ContractContractor",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ContractContractorCollection __ContractContractorCollection;
		
		[XmlIgnore]
		public ContractContractorCollection ContractContractorCollection
		{
			get
			{
				if (__ContractContractorCollection == null) __ContractContractorCollection = new ContractContractorCollection();
				return __ContractContractorCollection;
			}
			set {__ContractContractorCollection = value;}
		}

		public ContractContractors()
		{
		}
	}


	[XmlType(TypeName="ProcurementPlans",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ProcurementPlans
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
		[XmlElement(Type=typeof(R_10048.ProcurementPlan),ElementName="ProcurementPlan",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProcurementPlanCollection __ProcurementPlanCollection;
		
		[XmlIgnore]
		public ProcurementPlanCollection ProcurementPlanCollection
		{
			get
			{
				if (__ProcurementPlanCollection == null) __ProcurementPlanCollection = new ProcurementPlanCollection();
				return __ProcurementPlanCollection;
			}
			set {__ProcurementPlanCollection = value;}
		}

		public ProcurementPlans()
		{
		}
	}
}
