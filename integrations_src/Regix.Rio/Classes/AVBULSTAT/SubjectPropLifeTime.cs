//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace Eumis.RegiX.Rio.AVBULSTAT
{




	[XmlType(TypeName="SubjectPropLifeTime",Namespace="http://www.bulstat.bg/SubjectPropLifeTime"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SubjectPropLifeTime : Eumis.RegiX.Rio.AVBULSTAT.Entry
	{

		[XmlElement(ElementName="Date",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/SubjectPropLifeTime")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Date;
		
		[XmlIgnore]
		public string Date
		{ 
			get { return __Date; }
			set { __Date = value; }
		}

		[XmlElement(ElementName="Description",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/SubjectPropLifeTime")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Description;
		
		[XmlIgnore]
		public string Description
		{ 
			get { return __Description; }
			set { __Description = value; }
		}

		public SubjectPropLifeTime() : base()
		{
		}
	}
}
