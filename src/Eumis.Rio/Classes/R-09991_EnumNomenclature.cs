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




	[XmlType(TypeName="EnumNomenclature",Namespace="http://ereg.egov.bg/segment/R-09991"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class EnumNomenclature
	{

		[XmlElement(ElementName="Value",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-09991")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Value;
		
		[XmlIgnore]
		public string @Value
		{ 
			get { return __Value; }
			set { __Value = value; }
		}

		[XmlElement(ElementName="Description",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-09991")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Description;
		
		[XmlIgnore]
		public string Description
		{ 
			get { return __Description; }
			set { __Description = value; }
		}

		[XmlElement(ElementName="DescriptionEN",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-09991")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DescriptionEN;
		
		[XmlIgnore]
		public string DescriptionEN
		{ 
			get { return __DescriptionEN; }
			set { __DescriptionEN = value; }
		}

		public EnumNomenclature()
		{
		}
	}
}
