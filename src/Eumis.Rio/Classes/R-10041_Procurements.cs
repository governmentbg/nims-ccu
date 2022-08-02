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


	using ProcurementPlanCollection = System.Collections.Generic.List<Eumis.Rio.ProcurementPlan>;

	using BFPContractContractActivityCollection = System.Collections.Generic.List<Eumis.Rio.BFPContractContractActivity>;

	using PrivateNomenclatureCollection = System.Collections.Generic.List<Eumis.Rio.PrivateNomenclature>;

	using BFPContractProgrammeDetailsExpenseBudgetCollection = System.Collections.Generic.List<Eumis.Rio.BFPContractProgrammeDetailsExpenseBudget>;

	using ContractorCollection = System.Collections.Generic.List<Eumis.Rio.Contractor>;

	using ContractContractorCollection = System.Collections.Generic.List<Eumis.Rio.ContractContractor>;



	[XmlType(TypeName="Procurements",Namespace="http://ereg.egov.bg/segment/R-10041"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Procurements
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

		[XmlAttribute(AttributeName="contractGid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __contractGid;
		
		[XmlIgnore]
		public string contractGid
		{ 
			get { return __contractGid; }
			set { __contractGid = value; }
		}

		[XmlAttribute(AttributeName="contractVersionGid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __contractVersionGid;
		
		[XmlIgnore]
		public string contractVersionGid
		{ 
			get { return __contractVersionGid; }
			set { __contractVersionGid = value; }
		}

		[XmlAttribute(AttributeName="orderNum",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __orderNum;
		
		[XmlIgnore]
		public string orderNum
		{ 
			get { return __orderNum; }
			set { __orderNum = value; }
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
		


		[XmlElement(Type=typeof(Contractors),ElementName="Contractors",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10041")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Contractors __Contractors;
		
		[XmlIgnore]
		public Contractors Contractors
		{
			get {return __Contractors;}
			set {__Contractors = value;}
		}

		[XmlElement(Type=typeof(ContractContractors),ElementName="ContractContractors",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10041")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ContractContractors __ContractContractors;
		
		[XmlIgnore]
		public ContractContractors ContractContractors
		{
			get {return __ContractContractors;}
			set {__ContractContractors = value;}
		}

		[XmlElement(Type=typeof(ProcurementPlans),ElementName="ProcurementPlans",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10041")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProcurementPlans __ProcurementPlans;
		
		[XmlIgnore]
		public ProcurementPlans ProcurementPlans
		{
			get {return __ProcurementPlans;}
			set {__ProcurementPlans = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="BudgetLevel3Item",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10041")]
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

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="ContractActivityItem",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10041")]
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

		[XmlElement(Type=typeof(Eumis.Rio.BFPContractProgrammeDetailsExpenseBudget),ElementName="BudgetLevel3",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10041")]
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

		[XmlElement(Type=typeof(Eumis.Rio.BFPContractContractActivity),ElementName="ContractActivity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10041")]
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

		[XmlElement(Type=typeof(Eumis.Rio.Signature),ElementName="Signature",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.Signature __Signature;
		
		[XmlIgnore]
		public Eumis.Rio.Signature Signature
		{
			get {return __Signature;}
			set {__Signature = value;}
		}

		public Procurements()
		{
		}
	}


	[XmlType(TypeName="Contractors",Namespace="http://ereg.egov.bg/segment/R-10041"),Serializable]
	public partial class Contractors
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

		[XmlElement(Type=typeof(Eumis.Rio.Contractor),ElementName="Contractor",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10041")]
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


	[XmlType(TypeName="ContractContractors",Namespace="http://ereg.egov.bg/segment/R-10041"),Serializable]
	public partial class ContractContractors
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

		[XmlElement(Type=typeof(Eumis.Rio.ContractContractor),ElementName="ContractContractor",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10041")]
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


	[XmlType(TypeName="ProcurementPlans",Namespace="http://ereg.egov.bg/segment/R-10041"),Serializable]
	public partial class ProcurementPlans
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

		[XmlElement(Type=typeof(Eumis.Rio.ProcurementPlan),ElementName="ProcurementPlan",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10041")]
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
