//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10009
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10009";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ProgrammeExpenseBudgetCollection : System.Collections.Generic.List<R_10008.ProgrammeExpenseBudget>
	{
	}



	[XmlType(TypeName="ProgrammeBudget",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ProgrammeBudget
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
		[XmlElement(ElementName="NameEN",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NameEN;
		
		[XmlIgnore]
		public string NameEN
		{ 
			get { return __NameEN; }
			set { __NameEN = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="OrderNum",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __OrderNum;
		
		[XmlIgnore]
		public string OrderNum
		{ 
			get { return __OrderNum; }
			set { __OrderNum = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10008.ProgrammeExpenseBudget),ElementName="ProgrammeExpenseBudget",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProgrammeExpenseBudgetCollection __ProgrammeExpenseBudgetCollection;
		
		[XmlIgnore]
		public ProgrammeExpenseBudgetCollection ProgrammeExpenseBudgetCollection
		{
			get
			{
				if (__ProgrammeExpenseBudgetCollection == null) __ProgrammeExpenseBudgetCollection = new ProgrammeExpenseBudgetCollection();
				return __ProgrammeExpenseBudgetCollection;
			}
			set {__ProgrammeExpenseBudgetCollection = value;}
		}

		public ProgrammeBudget()
		{
		}
	}
}