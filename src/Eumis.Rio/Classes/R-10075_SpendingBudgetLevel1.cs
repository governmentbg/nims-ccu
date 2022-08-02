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


	using QuarterlyDistributionCollection = System.Collections.Generic.List<Eumis.Rio.QuarterlyDistribution>;



	[XmlType(TypeName="SpendingBudgetLevel1",Namespace="http://ereg.egov.bg/segment/R-10075"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SpendingBudgetLevel1
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

		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10075")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[XmlElement(ElementName="OrderNum",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10075")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __OrderNum;
		
		[XmlIgnore]
		public string OrderNum
		{ 
			get { return __OrderNum; }
			set { __OrderNum = value; }
		}

		[XmlElement(ElementName="TotalAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10075")]
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

		[XmlElement(ElementName="TotalCalculatedAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10075")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __TotalCalculatedAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TotalCalculatedAmountSpecified;
		
		[XmlIgnore]
		public decimal TotalCalculatedAmount
		{ 
			get { return __TotalCalculatedAmount; }
			set { __TotalCalculatedAmount = value; __TotalCalculatedAmountSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.QuarterlyDistribution),ElementName="QuarterlyDistribution",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10075")]
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

		public SpendingBudgetLevel1()
		{
		}
	}
}
