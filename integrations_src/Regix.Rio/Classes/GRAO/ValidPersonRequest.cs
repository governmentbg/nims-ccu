//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace Eumis.RegiX.Rio.GRAO
{




	[XmlType(TypeName="ValidPersonRequestType",Namespace="http://egov.bg/RegiX/GRAO/NBD/ValidPersonRequest"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ValidPersonRequestType
	{

		[XmlElement(ElementName="EGN",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/GRAO/NBD/ValidPersonRequest")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __EGN;
		
		[XmlIgnore]
		public string EGN
		{ 
			get { return __EGN; }
			set { __EGN = value; }
		}

		public ValidPersonRequestType()
		{
		}
	}


	[XmlRoot(ElementName="ValidPersonRequest",Namespace="http://egov.bg/RegiX/GRAO/NBD/ValidPersonRequest",IsNullable=false),Serializable]
	public partial class ValidPersonRequest : Eumis.RegiX.Rio.GRAO.ValidPersonRequestType
	{

		public ValidPersonRequest() : base()
		{
		}
	}
}