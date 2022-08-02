//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10023
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10023";
	}

	[Serializable]
	public enum EvalTypeNomenclature
	{
		[XmlEnum(Name="Rejection")] Rejection,
		[XmlEnum(Name="Weight")] Weight
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_10018.AttachedDocument>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class EvalTableGroupCollection : System.Collections.Generic.List<R_10022.EvalTableGroup>
	{
	}



	[XmlType(TypeName="EvalTable",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class EvalTable
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
		[XmlAttribute(AttributeName="type")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_09993.EvalTypeNomenclature __type;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __typeSpecified;
		
		[XmlIgnore]
		public R_09993.EvalTypeNomenclature type
		{ 
			get { return __type; }
			set { __type = value; __typeSpecified = true; }
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
		[XmlElement(Type=typeof(R_10022.EvalTableGroup),ElementName="EvalTableGroup",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public EvalTableGroupCollection __EvalTableGroupCollection;
		
		[XmlIgnore]
		public EvalTableGroupCollection EvalTableGroupCollection
		{
			get
			{
				if (__EvalTableGroupCollection == null) __EvalTableGroupCollection = new EvalTableGroupCollection();
				return __EvalTableGroupCollection;
			}
			set {__EvalTableGroupCollection = value;}
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

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Limit",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __Limit;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __LimitSpecified;
		
		[XmlIgnore]
		public decimal Limit
		{ 
			get { return __Limit; }
			set { __Limit = value; __LimitSpecified = true; }
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

		public EvalTable()
		{
		}
	}
}