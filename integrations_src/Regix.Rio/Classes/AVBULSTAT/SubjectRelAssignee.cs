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


	using SubjectCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.Subject>;



	[XmlType(TypeName="SubjectRelAssignee",Namespace="http://www.bulstat.bg/SubjectRelAssignee"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SubjectRelAssignee : Eumis.RegiX.Rio.AVBULSTAT.SubscriptionElement
	{

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.Subject),ElementName="RelatedSubjects",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/SubjectRelAssignee")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubjectCollection __RelatedSubjectsCollection;
		
		[XmlIgnore]
		public SubjectCollection RelatedSubjectsCollection
		{
			get
			{
				if (__RelatedSubjectsCollection == null) __RelatedSubjectsCollection = new SubjectCollection();
				return __RelatedSubjectsCollection;
			}
			set {__RelatedSubjectsCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="Type",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/SubjectRelAssignee")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __Type;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry Type
		{
			get {return __Type;}
			set {__Type = value;}
		}

		public SubjectRelAssignee() : base()
		{
		}
	}
}
