//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10053
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10053";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PrivateNomenclatureCollection : System.Collections.Generic.List<R_10000.PrivateNomenclature>
	{
	}



	[XmlType(TypeName="TechnicalReportActivity",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class TechnicalReportActivity
	{

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(BFPContractActivity),ElementName="BFPContractActivity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractActivity __BFPContractActivity;
		
		[XmlIgnore]
		public BFPContractActivity BFPContractActivity
		{
			get {return __BFPContractActivity;}
			set {__BFPContractActivity = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="ExecutionDescription",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ExecutionDescription;
		
		[XmlIgnore]
		public string ExecutionDescription
		{ 
			get { return __ExecutionDescription; }
			set { __ExecutionDescription = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_09991.EnumNomenclature),ElementName="Status",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_09991.EnumNomenclature __Status;
		
		[XmlIgnore]
		public R_09991.EnumNomenclature Status
		{
			get {return __Status;}
			set {__Status = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="MonthsDuration",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MonthsDuration;
		
		[XmlIgnore]
		public string MonthsDuration
		{ 
			get { return __MonthsDuration; }
			set { __MonthsDuration = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="ActualStartDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __ActualStartDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ActualStartDateSpecified { get { return __ActualStartDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? ActualStartDate
		{ 
			get { return __ActualStartDate; }
			set { __ActualStartDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="ActualEndDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __ActualEndDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ActualEndDateSpecified { get { return __ActualEndDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? ActualEndDate
		{ 
			get { return __ActualEndDate; }
			set { __ActualEndDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="DelayReason",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DelayReason;
		
		[XmlIgnore]
		public string DelayReason
		{ 
			get { return __DelayReason; }
			set { __DelayReason = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="PeriodResult",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PeriodResult;
		
		[XmlIgnore]
		public string PeriodResult
		{ 
			get { return __PeriodResult; }
			set { __PeriodResult = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="CumulativeResult",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CumulativeResult;
		
		[XmlIgnore]
		public string CumulativeResult
		{ 
			get { return __CumulativeResult; }
			set { __CumulativeResult = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10000.PrivateNomenclature),ElementName="ContractContractor",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PrivateNomenclatureCollection __ContractContractorCollection;
		
		[XmlIgnore]
		public PrivateNomenclatureCollection ContractContractorCollection
		{
			get
			{
				if (__ContractContractorCollection == null) __ContractContractorCollection = new PrivateNomenclatureCollection();
				return __ContractContractorCollection;
			}
			set {__ContractContractorCollection = value;}
		}

		public TechnicalReportActivity()
		{
		}
	}


	[XmlType(TypeName="BFPContractActivity",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class BFPContractActivity
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="gid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __gid;
		
		[XmlIgnore]
		public string gid
		{ 
			get { return __gid; }
			set { __gid = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Code",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Code;
		
		[XmlIgnore]
		public string Code
		{ 
			get { return __Code; }
			set { __Code = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Result",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Result;
		
		[XmlIgnore]
		public string Result
		{ 
			get { return __Result; }
			set { __Result = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Duration",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Duration;
		
		[XmlIgnore]
		public string Duration
		{ 
			get { return __Duration; }
			set { __Duration = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="StartDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __StartDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __StartDateSpecified { get { return __StartDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? StartDate
		{ 
			get { return __StartDate; }
			set { __StartDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="EndDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __EndDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __EndDateSpecified { get { return __EndDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? EndDate
		{ 
			get { return __EndDate; }
			set { __EndDate = value; }
		}
		


		public BFPContractActivity()
		{
		}
	}
}
