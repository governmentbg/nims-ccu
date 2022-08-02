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


	using CommunicationCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.Communication>;

	using AddressCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.AVBULSTAT.Address>;



	[XmlType(TypeName="Subject",Namespace="http://www.bulstat.bg/Subject"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Subject : Eumis.RegiX.Rio.AVBULSTAT.Entry
	{

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.UIC),ElementName="UIC",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/Subject")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.UIC __UIC;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.UIC UIC
		{
			get {return __UIC;}
			set {__UIC = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="SubjectType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/Subject")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __SubjectType;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry SubjectType
		{
			get {return __SubjectType;}
			set {__SubjectType = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.LegalEntity),ElementName="LegalEntitySubject",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/Subject")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.LegalEntity __LegalEntitySubject;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.LegalEntity LegalEntitySubject
		{
			get {return __LegalEntitySubject;}
			set {__LegalEntitySubject = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NaturalPerson),ElementName="NaturalPersonSubject",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/Subject")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NaturalPerson __NaturalPersonSubject;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NaturalPerson NaturalPersonSubject
		{
			get {return __NaturalPersonSubject;}
			set {__NaturalPersonSubject = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="CountrySubject",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/Subject")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __CountrySubject;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry CountrySubject
		{
			get {return __CountrySubject;}
			set {__CountrySubject = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.Communication),ElementName="Communications",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/Subject")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CommunicationCollection __CommunicationsCollection;
		
		[XmlIgnore]
		public CommunicationCollection CommunicationsCollection
		{
			get
			{
				if (__CommunicationsCollection == null) __CommunicationsCollection = new CommunicationCollection();
				return __CommunicationsCollection;
			}
			set {__CommunicationsCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.Address),ElementName="Addresses",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/Subject")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AddressCollection __AddressesCollection;
		
		[XmlIgnore]
		public AddressCollection AddressesCollection
		{
			get
			{
				if (__AddressesCollection == null) __AddressesCollection = new AddressCollection();
				return __AddressesCollection;
			}
			set {__AddressesCollection = value;}
		}

		[XmlElement(ElementName="Remark",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/Subject")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Remark;
		
		[XmlIgnore]
		public string Remark
		{ 
			get { return __Remark; }
			set { __Remark = value; }
		}

		public Subject() : base()
		{
		}
	}
}
