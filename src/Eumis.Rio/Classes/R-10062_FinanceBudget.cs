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


	using FinanceBudgetLevel1Collection = System.Collections.Generic.List<Eumis.Rio.FinanceBudgetLevel1>;



	[XmlType(TypeName="FinanceBudget",Namespace="http://ereg.egov.bg/segment/R-10062"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class FinanceBudget
	{

		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10062")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[XmlElement(ElementName="OrderNum",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10062")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __OrderNum;
		
		[XmlIgnore]
		public string OrderNum
		{ 
			get { return __OrderNum; }
			set { __OrderNum = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.FinanceBudgetLevel1),ElementName="FinanceBudgetLevel1",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10062")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FinanceBudgetLevel1Collection __FinanceBudgetLevel1Collection;
		
		[XmlIgnore]
		public FinanceBudgetLevel1Collection FinanceBudgetLevel1Collection
		{
			get
			{
				if (__FinanceBudgetLevel1Collection == null) __FinanceBudgetLevel1Collection = new FinanceBudgetLevel1Collection();
				return __FinanceBudgetLevel1Collection;
			}
			set {__FinanceBudgetLevel1Collection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.FinanceReportBudgetRowAmounts),ElementName="Amounts",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10062")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.FinanceReportBudgetRowAmounts __Amounts;
		
		[XmlIgnore]
		public Eumis.Rio.FinanceReportBudgetRowAmounts Amounts
		{
			get {return __Amounts;}
			set {__Amounts = value;}
		}

		[XmlElement(ElementName="BFPContractCrossFinancingAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10062")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __BFPContractCrossFinancingAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __BFPContractCrossFinancingAmountSpecified;
		
		[XmlIgnore]
		public decimal BFPContractCrossFinancingAmount
		{ 
			get { return __BFPContractCrossFinancingAmount; }
			set { __BFPContractCrossFinancingAmount = value; __BFPContractCrossFinancingAmountSpecified = true; }
		}

		public FinanceBudget()
		{
		}
	}
}
