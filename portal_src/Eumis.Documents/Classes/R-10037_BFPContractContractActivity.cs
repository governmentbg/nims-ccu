//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10037
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10037";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PrivateNomenclatureCollection : System.Collections.Generic.List<R_10000.PrivateNomenclature>
	{
	}



	[XmlType(TypeName="BFPContractContractActivity",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class BFPContractContractActivity
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isActivated",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActivated;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActivatedSpecified;
		
		[XmlIgnore]
		public bool isActivated
		{ 
			get { return __isActivated; }
			set { __isActivated = value; __isActivatedSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isActive",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActive;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActiveSpecified;
		
		[XmlIgnore]
		public bool isActive
		{ 
			get { return __isActive; }
			set { __isActive = value; __isActiveSpecified = true; }
		}

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
		[XmlElement(Type=typeof(R_10000.PrivateNomenclature),ElementName="Company",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PrivateNomenclatureCollection __CompanyCollection;
		
		[XmlIgnore]
		public PrivateNomenclatureCollection CompanyCollection
		{
			get
			{
				if (__CompanyCollection == null) __CompanyCollection = new PrivateNomenclatureCollection();
				return __CompanyCollection;
			}
			set {__CompanyCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="ExecutionMethod",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ExecutionMethod;
		
		[XmlIgnore]
		public string ExecutionMethod
		{ 
			get { return __ExecutionMethod; }
			set { __ExecutionMethod = value; }
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
		[XmlElement(ElementName="StartMonth",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __StartMonth;
		
		[XmlIgnore]
		public string StartMonth
		{ 
			get { return __StartMonth; }
			set { __StartMonth = value; }
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
		[XmlElement(ElementName="Amount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __Amount;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __AmountSpecified;
		
		[XmlIgnore]
		public decimal Amount
		{ 
			get { return __Amount; }
			set { __Amount = value; __AmountSpecified = true; }
		}

		public BFPContractContractActivity()
		{
		}
	}
}
