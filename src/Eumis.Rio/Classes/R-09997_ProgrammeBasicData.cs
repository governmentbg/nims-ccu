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


	using PublicNomenclatureCollection = System.Collections.Generic.List<Eumis.Rio.PublicNomenclature>;



	[XmlType(TypeName="ProgrammeBasicData",Namespace="http://ereg.egov.bg/segment/R-09997"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ProgrammeBasicData
	{

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="Programme",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-09997")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PublicNomenclature __Programme;
		
		[XmlIgnore]
		public Eumis.Rio.PublicNomenclature Programme
		{
			get {return __Programme;}
			set {__Programme = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="ProgrammePriority",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-09997")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PublicNomenclatureCollection __ProgrammePriorityCollection;
		
		[XmlIgnore]
		public PublicNomenclatureCollection ProgrammePriorityCollection
		{
			get
			{
				if (__ProgrammePriorityCollection == null) __ProgrammePriorityCollection = new PublicNomenclatureCollection();
				return __ProgrammePriorityCollection;
			}
			set {__ProgrammePriorityCollection = value;}
		}

		public ProgrammeBasicData()
		{
		}
	}
}