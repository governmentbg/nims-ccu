//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_09994
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-09994";
	}




	[XmlType(TypeName="PaperAttachedDocument",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PaperAttachedDocument
	{

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10000.PrivateNomenclature),ElementName="Type",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_10000.PrivateNomenclature __Type;
		
		[XmlIgnore]
		public R_10000.PrivateNomenclature Type
		{
			get {return __Type;}
			set {__Type = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Description",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Description;
		
		[XmlIgnore]
		public string Description
		{ 
			get { return __Description; }
			set { __Description = value; }
		}

		public PaperAttachedDocument()
		{
		}
	}
}