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




	[XmlType(TypeName="SubjectRelBelonging",Namespace="http://www.bulstat.bg/SubjectRelBelonging"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SubjectRelBelonging : Eumis.RegiX.Rio.AVBULSTAT.SubscriptionElement
	{

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.Subject),ElementName="RelatedSubject",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/SubjectRelBelonging")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.Subject __RelatedSubject;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.Subject RelatedSubject
		{
			get {return __RelatedSubject;}
			set {__RelatedSubject = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="Type",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/SubjectRelBelonging")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __Type;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry Type
		{
			get {return __Type;}
			set {__Type = value;}
		}

		public SubjectRelBelonging() : base()
		{
		}
	}
}
