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




	[XmlType(TypeName="PrivateNomenclature",Namespace="http://ereg.egov.bg/segment/R-10000"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PrivateNomenclature
	{

		[XmlAttribute(AttributeName="orderNum",DataType="integer")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __orderNum;
		
		[XmlIgnore]
		public string orderNum
		{ 
			get { return __orderNum; }
			set { __orderNum = value; }
		}

		[XmlElement(ElementName="Id",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10000")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10000")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[XmlElement(ElementName="NameEN",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10000")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NameEN;
		
		[XmlIgnore]
		public string NameEN
		{ 
			get { return __NameEN; }
			set { __NameEN = value; }
		}

		public PrivateNomenclature()
		{
		}
	}
}
