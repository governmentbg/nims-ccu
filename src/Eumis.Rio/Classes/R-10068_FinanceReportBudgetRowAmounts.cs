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




	[XmlType(TypeName="FinanceReportBudgetRowAmounts",Namespace="http://ereg.egov.bg/segment/R-10068"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class FinanceReportBudgetRowAmounts
	{

		[XmlElement(Type=typeof(Eumis.Rio.FinanceReportBudgetAmounts),ElementName="BFPContractAmounts",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10068")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.FinanceReportBudgetAmounts __BFPContractAmounts;
		
		[XmlIgnore]
		public Eumis.Rio.FinanceReportBudgetAmounts BFPContractAmounts
		{
			get {return __BFPContractAmounts;}
			set {__BFPContractAmounts = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.FinanceReportBudgetAmounts),ElementName="CurrentReportAmounts",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10068")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.FinanceReportBudgetAmounts __CurrentReportAmounts;
		
		[XmlIgnore]
		public Eumis.Rio.FinanceReportBudgetAmounts CurrentReportAmounts
		{
			get {return __CurrentReportAmounts;}
			set {__CurrentReportAmounts = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.FinanceReportBudgetAmounts),ElementName="CumulativeAmounts",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10068")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.FinanceReportBudgetAmounts __CumulativeAmounts;
		
		[XmlIgnore]
		public Eumis.Rio.FinanceReportBudgetAmounts CumulativeAmounts
		{
			get {return __CumulativeAmounts;}
			set {__CumulativeAmounts = value;}
		}

		[XmlElement(ElementName="DifferenceGrand",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10068")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __DifferenceGrand;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DifferenceGrandSpecified;
		
		[XmlIgnore]
		public decimal DifferenceGrand
		{ 
			get { return __DifferenceGrand; }
			set { __DifferenceGrand = value; __DifferenceGrandSpecified = true; }
		}

		[XmlElement(ElementName="DifferenceGrandPercentage",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10068")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __DifferenceGrandPercentage;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DifferenceGrandPercentageSpecified;
		
		[XmlIgnore]
		public decimal DifferenceGrandPercentage
		{ 
			get { return __DifferenceGrandPercentage; }
			set { __DifferenceGrandPercentage = value; __DifferenceGrandPercentageSpecified = true; }
		}

		[XmlElement(ElementName="DifferenceTotal",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10068")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __DifferenceTotal;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DifferenceTotalSpecified;
		
		[XmlIgnore]
		public decimal DifferenceTotal
		{ 
			get { return __DifferenceTotal; }
			set { __DifferenceTotal = value; __DifferenceTotalSpecified = true; }
		}

		[XmlElement(ElementName="DifferenceTotalPercentage",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10068")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __DifferenceTotalPercentage;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DifferenceTotalPercentageSpecified;
		
		[XmlIgnore]
		public decimal DifferenceTotalPercentage
		{ 
			get { return __DifferenceTotalPercentage; }
			set { __DifferenceTotalPercentage = value; __DifferenceTotalPercentageSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.FinanceReportBudgetAmounts),ElementName="LastReportAmounts",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10068")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.FinanceReportBudgetAmounts __LastReportAmounts;
		
		[XmlIgnore]
		public Eumis.Rio.FinanceReportBudgetAmounts LastReportAmounts
		{
			get {return __LastReportAmounts;}
			set {__LastReportAmounts = value;}
		}

		public FinanceReportBudgetRowAmounts()
		{
		}
	}
}
