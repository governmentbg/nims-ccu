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




	[XmlType(TypeName="LegalEntity",Namespace="http://www.bulstat.bg/LegalEntity"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class LegalEntity : Eumis.RegiX.Rio.AVBULSTAT.Entry
	{

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="Country",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/LegalEntity")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __Country;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry Country
		{
			get {return __Country;}
			set {__Country = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="LegalForm",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/LegalEntity")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __LegalForm;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry LegalForm
		{
			get {return __LegalForm;}
			set {__LegalForm = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="LegalStatute",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/LegalEntity")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __LegalStatute;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry LegalStatute
		{
			get {return __LegalStatute;}
			set {__LegalStatute = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="SubjectGroup",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/LegalEntity")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __SubjectGroup;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry SubjectGroup
		{
			get {return __SubjectGroup;}
			set {__SubjectGroup = value;}
		}

		[XmlElement(ElementName="CyrillicFullName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/LegalEntity")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CyrillicFullName;
		
		[XmlIgnore]
		public string CyrillicFullName
		{ 
			get { return __CyrillicFullName; }
			set { __CyrillicFullName = value; }
		}

		[XmlElement(ElementName="CyrillicShortName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/LegalEntity")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CyrillicShortName;
		
		[XmlIgnore]
		public string CyrillicShortName
		{ 
			get { return __CyrillicShortName; }
			set { __CyrillicShortName = value; }
		}

		[XmlElement(ElementName="LatinFullName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/LegalEntity")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __LatinFullName;
		
		[XmlIgnore]
		public string LatinFullName
		{ 
			get { return __LatinFullName; }
			set { __LatinFullName = value; }
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="SubordinateLevel",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/LegalEntity")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __SubordinateLevel;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry SubordinateLevel
		{
			get {return __SubordinateLevel;}
			set {__SubordinateLevel = value;}
		}

		[XmlElement(ElementName="TRStatus",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/LegalEntity")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TRStatus;
		
		[XmlIgnore]
		public string TRStatus
		{ 
			get { return __TRStatus; }
			set { __TRStatus = value; }
		}

		public LegalEntity() : base()
		{
		}
	}
}