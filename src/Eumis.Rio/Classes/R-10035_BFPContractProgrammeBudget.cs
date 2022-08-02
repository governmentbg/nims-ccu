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


	using BFPContractProgrammeExpenseBudgetCollection = System.Collections.Generic.List<Eumis.Rio.BFPContractProgrammeExpenseBudget>;



	[XmlType(TypeName="BFPContractProgrammeBudget",Namespace="http://ereg.egov.bg/segment/R-10035"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class BFPContractProgrammeBudget
	{

		[XmlAttribute(AttributeName="gid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __gid;
		
		[XmlIgnore]
		public string gid
		{ 
			get { return __gid; }
			set { __gid = value; }
		}

		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10035")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[XmlElement(ElementName="OrderNum",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10035")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __OrderNum;
		
		[XmlIgnore]
		public string OrderNum
		{ 
			get { return __OrderNum; }
			set { __OrderNum = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.BFPContractProgrammeExpenseBudget),ElementName="BFPContractProgrammeExpenseBudget",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10035")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BFPContractProgrammeExpenseBudgetCollection __BFPContractProgrammeExpenseBudgetCollection;
		
		[XmlIgnore]
		public BFPContractProgrammeExpenseBudgetCollection BFPContractProgrammeExpenseBudgetCollection
		{
			get
			{
				if (__BFPContractProgrammeExpenseBudgetCollection == null) __BFPContractProgrammeExpenseBudgetCollection = new BFPContractProgrammeExpenseBudgetCollection();
				return __BFPContractProgrammeExpenseBudgetCollection;
			}
			set {__BFPContractProgrammeExpenseBudgetCollection = value;}
		}

		[XmlElement(ElementName="EUAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10035")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __EUAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __EUAmountSpecified;
		
		[XmlIgnore]
		public decimal EUAmount
		{ 
			get { return __EUAmount; }
			set { __EUAmount = value; __EUAmountSpecified = true; }
		}

		[XmlElement(ElementName="NationalAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10035")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __NationalAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __NationalAmountSpecified;
		
		[XmlIgnore]
		public decimal NationalAmount
		{ 
			get { return __NationalAmount; }
			set { __NationalAmount = value; __NationalAmountSpecified = true; }
		}

		[XmlElement(ElementName="GrandAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10035")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __GrandAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __GrandAmountSpecified;
		
		[XmlIgnore]
		public decimal GrandAmount
		{ 
			get { return __GrandAmount; }
			set { __GrandAmount = value; __GrandAmountSpecified = true; }
		}

		[XmlElement(ElementName="SelfAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10035")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __SelfAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __SelfAmountSpecified;
		
		[XmlIgnore]
		public decimal SelfAmount
		{ 
			get { return __SelfAmount; }
			set { __SelfAmount = value; __SelfAmountSpecified = true; }
		}

		[XmlElement(ElementName="TotalAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10035")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __TotalAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TotalAmountSpecified;
		
		[XmlIgnore]
		public decimal TotalAmount
		{ 
			get { return __TotalAmount; }
			set { __TotalAmount = value; __TotalAmountSpecified = true; }
		}

		public BFPContractProgrammeBudget()
		{
		}
	}
}
