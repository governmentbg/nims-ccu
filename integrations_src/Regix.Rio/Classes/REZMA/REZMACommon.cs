//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace Eumis.RegiX.Rio.REZMA
{


	using ObligationTypeCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.REZMA.ObligationType>;



	[XmlType(TypeName="ObligationType",Namespace="http://egov.bg/RegiX/AM/REZMA"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ObligationType
	{

		[XmlElement(ElementName="CodeMU",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CodeMU;
		
		[XmlIgnore]
		public string CodeMU
		{ 
			get { return __CodeMU; }
			set { __CodeMU = value; }
		}

		[XmlElement(ElementName="MU",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MU;
		
		[XmlIgnore]
		public string MU
		{ 
			get { return __MU; }
			set { __MU = value; }
		}

		[XmlElement(ElementName="Document",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Document;
		
		[XmlIgnore]
		public string Document
		{ 
			get { return __Document; }
			set { __Document = value; }
		}

		[XmlElement(ElementName="DocumentNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DocumentNumber;
		
		[XmlIgnore]
		public string DocumentNumber
		{ 
			get { return __DocumentNumber; }
			set { __DocumentNumber = value; }
		}

		[XmlElement(ElementName="CreationDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __CreationDate;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __CreationDateSpecified { get { return __CreationDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? CreationDate
		{ 
			get { return __CreationDate; }
			set { __CreationDate = value; }
		}
		


		[XmlElement(ElementName="TypeObligation",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TypeObligation;
		
		[XmlIgnore]
		public string TypeObligation
		{ 
			get { return __TypeObligation; }
			set { __TypeObligation = value; }
		}

		[XmlElement(ElementName="ObligationAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __ObligationAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ObligationAmountSpecified;
		
		[XmlIgnore]
		public decimal ObligationAmount
		{ 
			get { return __ObligationAmount; }
			set { __ObligationAmount = value; __ObligationAmountSpecified = true; }
		}

		[XmlElement(ElementName="PayingDocument",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PayingDocument;
		
		[XmlIgnore]
		public string PayingDocument
		{ 
			get { return __PayingDocument; }
			set { __PayingDocument = value; }
		}

		[XmlElement(ElementName="PayingDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __PayingDate;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __PayingDateSpecified { get { return __PayingDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? PayingDate
		{ 
			get { return __PayingDate; }
			set { __PayingDate = value; }
		}
		


		[XmlElement(ElementName="PayingAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __PayingAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __PayingAmountSpecified;
		
		[XmlIgnore]
		public decimal PayingAmount
		{ 
			get { return __PayingAmount; }
			set { __PayingAmount = value; __PayingAmountSpecified = true; }
		}

		[XmlElement(ElementName="PayingTotal",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __PayingTotal;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __PayingTotalSpecified;
		
		[XmlIgnore]
		public decimal PayingTotal
		{ 
			get { return __PayingTotal; }
			set { __PayingTotal = value; __PayingTotalSpecified = true; }
		}

		[XmlElement(ElementName="Difference",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __Difference;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DifferenceSpecified;
		
		[XmlIgnore]
		public decimal Difference
		{ 
			get { return __Difference; }
			set { __Difference = value; __DifferenceSpecified = true; }
		}

		[XmlElement(ElementName="Status",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Status;
		
		[XmlIgnore]
		public string Status
		{ 
			get { return __Status; }
			set { __Status = value; }
		}

		public ObligationType()
		{
		}
	}


	[XmlType(TypeName="ObligationsType",Namespace="http://egov.bg/RegiX/AM/REZMA"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ObligationsType
	{


		[XmlElement(Type=typeof(Eumis.RegiX.Rio.REZMA.ObligationType),ElementName="Obligation",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://egov.bg/RegiX/AM/REZMA")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ObligationTypeCollection __ObligationCollection;
		
		[XmlIgnore]
		public ObligationTypeCollection ObligationCollection
		{
			get
			{
				if (__ObligationCollection == null) __ObligationCollection = new ObligationTypeCollection();
				return __ObligationCollection;
			}
			set {__ObligationCollection = value;}
		}

		public ObligationsType()
		{
		}
	}
}