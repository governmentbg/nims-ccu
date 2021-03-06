//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10032
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10032";
	}




	[XmlType(TypeName="BFPContractBudgetAmounts",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class BFPContractBudgetAmounts
	{

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="ContractValue",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __ContractValue;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ContractValueSpecified;
		
		[XmlIgnore]
		public decimal ContractValue
		{ 
			get { return __ContractValue; }
			set { __ContractValue = value; __ContractValueSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Modification",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __Modification;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ModificationSpecified;
		
		[XmlIgnore]
		public decimal Modification
		{ 
			get { return __Modification; }
			set { __Modification = value; __ModificationSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="NewCorrection",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __NewCorrection;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __NewCorrectionSpecified;
		
		[XmlIgnore]
		public decimal NewCorrection
		{ 
			get { return __NewCorrection; }
			set { __NewCorrection = value; __NewCorrectionSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="CurrentState",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __CurrentState;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __CurrentStateSpecified;
		
		[XmlIgnore]
		public decimal CurrentState
		{ 
			get { return __CurrentState; }
			set { __CurrentState = value; __CurrentStateSpecified = true; }
		}

		public BFPContractBudgetAmounts()
		{
		}
	}
}
