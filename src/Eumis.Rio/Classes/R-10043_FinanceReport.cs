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


	using CostSupportingDocumentCollection = System.Collections.Generic.List<Eumis.Rio.CostSupportingDocument>;

	using ContractContractorDataCollection = System.Collections.Generic.List<ContractContractorData>;

	using ActivityBudgetDetailDataCollection = System.Collections.Generic.List<ActivityBudgetDetailData>;

	using PrivateNomenclatureCollection = System.Collections.Generic.List<Eumis.Rio.PrivateNomenclature>;

	using ActivityBudgetDetailPairCollection = System.Collections.Generic.List<ActivityBudgetDetailPair>;



	[XmlType(TypeName="FinanceReport",Namespace="http://ereg.egov.bg/segment/R-10043"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class FinanceReport
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

		[XmlAttribute(AttributeName="contractGid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __contractGid;
		
		[XmlIgnore]
		public string contractGid
		{ 
			get { return __contractGid; }
			set { __contractGid = value; }
		}

		[XmlAttribute(AttributeName="packageGid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __packageGid;
		
		[XmlIgnore]
		public string packageGid
		{ 
			get { return __packageGid; }
			set { __packageGid = value; }
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

		[XmlAttribute(AttributeName="contractNumber",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __contractNumber;
		
		[XmlIgnore]
		public string contractNumber
		{ 
			get { return __contractNumber; }
			set { __contractNumber = value; }
		}

		[XmlAttribute(AttributeName="docNumber",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __docNumber;
		
		[XmlIgnore]
		public string docNumber
		{ 
			get { return __docNumber; }
			set { __docNumber = value; }
		}

		[XmlAttribute(AttributeName="docSubNumber",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __docSubNumber;
		
		[XmlIgnore]
		public string docSubNumber
		{ 
			get { return __docSubNumber; }
			set { __docSubNumber = value; }
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
		


		[XmlElement(Type=typeof(Eumis.Rio.FinanceReportBasicData),ElementName="BasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.FinanceReportBasicData __BasicData;
		
		[XmlIgnore]
		public Eumis.Rio.FinanceReportBasicData BasicData
		{
			get {return __BasicData;}
			set {__BasicData = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.FinanceBudget),ElementName="FinanceBudget",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.FinanceBudget __FinanceBudget;
		
		[XmlIgnore]
		public Eumis.Rio.FinanceBudget FinanceBudget
		{
			get {return __FinanceBudget;}
			set {__FinanceBudget = value;}
		}

		[XmlElement(Type=typeof(CostSupportingDocuments),ElementName="CostSupportingDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CostSupportingDocuments __CostSupportingDocuments;
		
		[XmlIgnore]
		public CostSupportingDocuments CostSupportingDocuments
		{
			get {return __CostSupportingDocuments;}
			set {__CostSupportingDocuments = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="Beneficiary",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __Beneficiary;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature Beneficiary
		{
			get {return __Beneficiary;}
			set {__Beneficiary = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="PartnerItem",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PrivateNomenclatureCollection __PartnerItemCollection;
		
		[XmlIgnore]
		public PrivateNomenclatureCollection PartnerItemCollection
		{
			get
			{
				if (__PartnerItemCollection == null) __PartnerItemCollection = new PrivateNomenclatureCollection();
				return __PartnerItemCollection;
			}
			set {__PartnerItemCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="ContractorItem",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PrivateNomenclatureCollection __ContractorItemCollection;
		
		[XmlIgnore]
		public PrivateNomenclatureCollection ContractorItemCollection
		{
			get
			{
				if (__ContractorItemCollection == null) __ContractorItemCollection = new PrivateNomenclatureCollection();
				return __ContractorItemCollection;
			}
			set {__ContractorItemCollection = value;}
		}

		[XmlElement(Type=typeof(ContractContractorData),ElementName="ContractContractorData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ContractContractorDataCollection __ContractContractorDataCollection;
		
		[XmlIgnore]
		public ContractContractorDataCollection ContractContractorDataCollection
		{
			get
			{
				if (__ContractContractorDataCollection == null) __ContractContractorDataCollection = new ContractContractorDataCollection();
				return __ContractContractorDataCollection;
			}
			set {__ContractContractorDataCollection = value;}
		}

		[XmlElement(Type=typeof(ActivityBudgetDetailData),ElementName="ActivityBudgetDetailData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ActivityBudgetDetailDataCollection __ActivityBudgetDetailDataCollection;
		
		[XmlIgnore]
		public ActivityBudgetDetailDataCollection ActivityBudgetDetailDataCollection
		{
			get
			{
				if (__ActivityBudgetDetailDataCollection == null) __ActivityBudgetDetailDataCollection = new ActivityBudgetDetailDataCollection();
				return __ActivityBudgetDetailDataCollection;
			}
			set {__ActivityBudgetDetailDataCollection = value;}
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

		public FinanceReport()
		{
		}
	}


	[XmlType(TypeName="CostSupportingDocuments",Namespace="http://ereg.egov.bg/segment/R-10043"),Serializable]
	public partial class CostSupportingDocuments
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

		[XmlElement(Type=typeof(Eumis.Rio.CostSupportingDocument),ElementName="CostSupportingDocument",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CostSupportingDocumentCollection __CostSupportingDocumentCollection;
		
		[XmlIgnore]
		public CostSupportingDocumentCollection CostSupportingDocumentCollection
		{
			get
			{
				if (__CostSupportingDocumentCollection == null) __CostSupportingDocumentCollection = new CostSupportingDocumentCollection();
				return __CostSupportingDocumentCollection;
			}
			set {__CostSupportingDocumentCollection = value;}
		}

		public CostSupportingDocuments()
		{
		}
	}


	[XmlType(TypeName="ContractContractorData",Namespace="http://ereg.egov.bg/segment/R-10043"),Serializable]
	public partial class ContractContractorData
	{

		[XmlElement(ElementName="ContractorId",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ContractorId;
		
		[XmlIgnore]
		public string ContractorId
		{ 
			get { return __ContractorId; }
			set { __ContractorId = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="ContractContractorItem",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PrivateNomenclatureCollection __ContractContractorItemCollection;
		
		[XmlIgnore]
		public PrivateNomenclatureCollection ContractContractorItemCollection
		{
			get
			{
				if (__ContractContractorItemCollection == null) __ContractContractorItemCollection = new PrivateNomenclatureCollection();
				return __ContractContractorItemCollection;
			}
			set {__ContractContractorItemCollection = value;}
		}

		public ContractContractorData()
		{
		}
	}


	[XmlType(TypeName="ActivityBudgetDetailData",Namespace="http://ereg.egov.bg/segment/R-10043"),Serializable]
	public partial class ActivityBudgetDetailData
	{

		[XmlElement(ElementName="ContractContractorId",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ContractContractorId;
		
		[XmlIgnore]
		public string ContractContractorId
		{ 
			get { return __ContractContractorId; }
			set { __ContractContractorId = value; }
		}

		[XmlElement(Type=typeof(ActivityBudgetDetailPair),ElementName="ActivityBudgetDetailPair",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ActivityBudgetDetailPairCollection __ActivityBudgetDetailPairCollection;
		
		[XmlIgnore]
		public ActivityBudgetDetailPairCollection ActivityBudgetDetailPairCollection
		{
			get
			{
				if (__ActivityBudgetDetailPairCollection == null) __ActivityBudgetDetailPairCollection = new ActivityBudgetDetailPairCollection();
				return __ActivityBudgetDetailPairCollection;
			}
			set {__ActivityBudgetDetailPairCollection = value;}
		}

		public ActivityBudgetDetailData()
		{
		}
	}


	[XmlType(TypeName="ActivityBudgetDetailPair",Namespace="http://ereg.egov.bg/segment/R-10043"),Serializable]
	public partial class ActivityBudgetDetailPair
	{

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="BudgetDetail",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __BudgetDetail;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature BudgetDetail
		{
			get {return __BudgetDetail;}
			set {__BudgetDetail = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="ContractActivity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10043")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __ContractActivity;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature ContractActivity
		{
			get {return __ContractActivity;}
			set {__ContractActivity = value;}
		}

		public ActivityBudgetDetailPair()
		{
		}
	}
}