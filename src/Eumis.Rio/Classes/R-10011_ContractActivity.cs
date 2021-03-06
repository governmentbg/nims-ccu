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


	using PrivateNomenclatureCollection = System.Collections.Generic.List<Eumis.Rio.PrivateNomenclature>;



	[XmlType(TypeName="ContractActivity",Namespace="http://ereg.egov.bg/segment/R-10011"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ContractActivity
	{

		[XmlElement(ElementName="Code",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10011")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Code;
		
		[XmlIgnore]
		public string Code
		{ 
			get { return __Code; }
			set { __Code = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="Company",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10011")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PrivateNomenclatureCollection __CompanyCollection;
		
		[XmlIgnore]
		public PrivateNomenclatureCollection CompanyCollection
		{
			get
			{
				if (__CompanyCollection == null) __CompanyCollection = new PrivateNomenclatureCollection();
				return __CompanyCollection;
			}
			set {__CompanyCollection = value;}
		}

		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10011")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[XmlElement(ElementName="ExecutionMethod",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10011")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ExecutionMethod;
		
		[XmlIgnore]
		public string ExecutionMethod
		{ 
			get { return __ExecutionMethod; }
			set { __ExecutionMethod = value; }
		}

		[XmlElement(ElementName="Result",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10011")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Result;
		
		[XmlIgnore]
		public string Result
		{ 
			get { return __Result; }
			set { __Result = value; }
		}

		[XmlElement(ElementName="StartMonth",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10011")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __StartMonth;
		
		[XmlIgnore]
		public string StartMonth
		{ 
			get { return __StartMonth; }
			set { __StartMonth = value; }
		}

		[XmlElement(ElementName="Duration",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10011")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Duration;
		
		[XmlIgnore]
		public string Duration
		{ 
			get { return __Duration; }
			set { __Duration = value; }
		}

		[XmlElement(ElementName="Amount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10011")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __Amount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __AmountSpecified;
		
		[XmlIgnore]
		public decimal Amount
		{ 
			get { return __Amount; }
			set { __Amount = value; __AmountSpecified = true; }
		}

		public ContractActivity()
		{
		}
	}
}
