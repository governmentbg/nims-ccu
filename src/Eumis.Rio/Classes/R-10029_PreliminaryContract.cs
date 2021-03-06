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




	[XmlType(TypeName="PreliminaryContract",Namespace="http://ereg.egov.bg/segment/R-10029"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PreliminaryContract
	{

		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(ElementName="RequestedFundingAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10029")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __RequestedFundingAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __RequestedFundingAmountSpecified;
		
		[XmlIgnore]
		public decimal RequestedFundingAmount
		{ 
			get { return __RequestedFundingAmount; }
			set { __RequestedFundingAmount = value; __RequestedFundingAmountSpecified = true; }
		}

		[XmlElement(ElementName="CoFinancingBudgetAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10029")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __CoFinancingBudgetAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __CoFinancingBudgetAmountSpecified;
		
		[XmlIgnore]
		public decimal CoFinancingBudgetAmount
		{ 
			get { return __CoFinancingBudgetAmount; }
			set { __CoFinancingBudgetAmount = value; __CoFinancingBudgetAmountSpecified = true; }
		}

		[XmlElement(ElementName="IsIncomeExpected",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace="http://ereg.egov.bg/segment/R-10029")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __IsIncomeExpected;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __IsIncomeExpectedSpecified;
		
		[XmlIgnore]
		public bool IsIncomeExpected
		{ 
			get { return __IsIncomeExpected; }
			set { __IsIncomeExpected = value; __IsIncomeExpectedSpecified = true; }
		}

		public PreliminaryContract()
		{
		}
	}
}
