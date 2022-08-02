//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10074
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10074";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class QuarterlyDistributionCollection : System.Collections.Generic.List<R_10072.QuarterlyDistribution>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SpendingBudgetLevel3Collection : System.Collections.Generic.List<R_10073.SpendingBudgetLevel3>
	{
	}



	[XmlType(TypeName="SpendingBudgetLevel2",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SpendingBudgetLevel2
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
		[XmlElement(ElementName="TotalAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __TotalAmount;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TotalAmountSpecified;
		
		[XmlIgnore]
		public decimal TotalAmount
		{ 
			get { return __TotalAmount; }
			set { __TotalAmount = value; __TotalAmountSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10072.QuarterlyDistribution),ElementName="QuarterlyDistribution",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public QuarterlyDistributionCollection __QuarterlyDistributionCollection;
		
		[XmlIgnore]
		public QuarterlyDistributionCollection QuarterlyDistributionCollection
		{
			get
			{
				if (__QuarterlyDistributionCollection == null) __QuarterlyDistributionCollection = new QuarterlyDistributionCollection();
				return __QuarterlyDistributionCollection;
			}
			set {__QuarterlyDistributionCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10073.SpendingBudgetLevel3),ElementName="SpendingBudgetLevel3",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SpendingBudgetLevel3Collection __SpendingBudgetLevel3Collection;
		
		[XmlIgnore]
		public SpendingBudgetLevel3Collection SpendingBudgetLevel3Collection
		{
			get
			{
				if (__SpendingBudgetLevel3Collection == null) __SpendingBudgetLevel3Collection = new SpendingBudgetLevel3Collection();
				return __SpendingBudgetLevel3Collection;
			}
			set {__SpendingBudgetLevel3Collection = value;}
		}

		public SpendingBudgetLevel2()
		{
		}
	}
}
