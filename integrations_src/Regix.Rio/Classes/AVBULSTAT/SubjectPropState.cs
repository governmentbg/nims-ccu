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




	[XmlType(TypeName="SubjectPropState",Namespace="http://www.bulstat.bg/SubjectPropState"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SubjectPropState : Eumis.RegiX.Rio.AVBULSTAT.Entry
	{

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="State",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/SubjectPropState")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __State;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry State
		{
			get {return __State;}
			set {__State = value;}
		}

		public SubjectPropState() : base()
		{
		}
	}
}
