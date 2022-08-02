//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace Eumis.RegiX.Rio.MVR
{


	using NationalityCollection = System.Collections.Generic.List<Eumis.RegiX.Rio.MVR.Nationality>;



	[XmlType(TypeName="NationalityList",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class NationalityList
	{


		[XmlElement(Type=typeof(Eumis.RegiX.Rio.MVR.Nationality),ElementName="Nationality",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public NationalityCollection __NationalityCollection;
		
		[XmlIgnore]
		public NationalityCollection NationalityCollection
		{
			get
			{
				if (__NationalityCollection == null) __NationalityCollection = new NationalityCollection();
				return __NationalityCollection;
			}
			set {__NationalityCollection = value;}
		}

		public NationalityList()
		{
		}
	}


	[XmlType(TypeName="Nationality",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Nationality
	{

		[XmlElement(ElementName="NationalityCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NationalityCode;
		
		[XmlIgnore]
		public string NationalityCode
		{ 
			get { return __NationalityCode; }
			set { __NationalityCode = value; }
		}

		[XmlElement(ElementName="NationalityName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NationalityName;
		
		[XmlIgnore]
		public string NationalityName
		{ 
			get { return __NationalityName; }
			set { __NationalityName = value; }
		}

		[XmlElement(ElementName="NationalityNameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NationalityNameLatin;
		
		[XmlIgnore]
		public string NationalityNameLatin
		{ 
			get { return __NationalityNameLatin; }
			set { __NationalityNameLatin = value; }
		}

		public Nationality()
		{
		}
	}


	[XmlType(TypeName="BirthPlace",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class BirthPlace
	{

		[XmlElement(ElementName="CountryCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CountryCode;
		
		[XmlIgnore]
		public string CountryCode
		{ 
			get { return __CountryCode; }
			set { __CountryCode = value; }
		}

		[XmlElement(ElementName="CountryName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CountryName;
		
		[XmlIgnore]
		public string CountryName
		{ 
			get { return __CountryName; }
			set { __CountryName = value; }
		}

		[XmlElement(ElementName="CountryNameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CountryNameLatin;
		
		[XmlIgnore]
		public string CountryNameLatin
		{ 
			get { return __CountryNameLatin; }
			set { __CountryNameLatin = value; }
		}

		[XmlElement(ElementName="TerritorialUnitName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TerritorialUnitName;
		
		[XmlIgnore]
		public string TerritorialUnitName
		{ 
			get { return __TerritorialUnitName; }
			set { __TerritorialUnitName = value; }
		}

		[XmlElement(ElementName="DistrictName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DistrictName;
		
		[XmlIgnore]
		public string DistrictName
		{ 
			get { return __DistrictName; }
			set { __DistrictName = value; }
		}

		[XmlElement(ElementName="MunicipalityName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MunicipalityName;
		
		[XmlIgnore]
		public string MunicipalityName
		{ 
			get { return __MunicipalityName; }
			set { __MunicipalityName = value; }
		}

		public BirthPlace()
		{
		}
	}


	[XmlType(TypeName="PersonNames",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PersonNames
	{

		[XmlElement(ElementName="FirstName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __FirstName;
		
		[XmlIgnore]
		public string FirstName
		{ 
			get { return __FirstName; }
			set { __FirstName = value; }
		}

		[XmlElement(ElementName="Surname",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Surname;
		
		[XmlIgnore]
		public string Surname
		{ 
			get { return __Surname; }
			set { __Surname = value; }
		}

		[XmlElement(ElementName="FamilyName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __FamilyName;
		
		[XmlIgnore]
		public string FamilyName
		{ 
			get { return __FamilyName; }
			set { __FamilyName = value; }
		}

		[XmlElement(ElementName="FirstNameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __FirstNameLatin;
		
		[XmlIgnore]
		public string FirstNameLatin
		{ 
			get { return __FirstNameLatin; }
			set { __FirstNameLatin = value; }
		}

		[XmlElement(ElementName="SurnameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __SurnameLatin;
		
		[XmlIgnore]
		public string SurnameLatin
		{ 
			get { return __SurnameLatin; }
			set { __SurnameLatin = value; }
		}

		[XmlElement(ElementName="LastNameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __LastNameLatin;
		
		[XmlIgnore]
		public string LastNameLatin
		{ 
			get { return __LastNameLatin; }
			set { __LastNameLatin = value; }
		}

		public PersonNames()
		{
		}
	}


	[XmlType(TypeName="PermanentAddress",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PermanentAddress
	{

		[XmlElement(ElementName="DistrictName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DistrictName;
		
		[XmlIgnore]
		public string DistrictName
		{ 
			get { return __DistrictName; }
			set { __DistrictName = value; }
		}

		[XmlElement(ElementName="DistrictNameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DistrictNameLatin;
		
		[XmlIgnore]
		public string DistrictNameLatin
		{ 
			get { return __DistrictNameLatin; }
			set { __DistrictNameLatin = value; }
		}

		[XmlElement(ElementName="MunicipalityName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MunicipalityName;
		
		[XmlIgnore]
		public string MunicipalityName
		{ 
			get { return __MunicipalityName; }
			set { __MunicipalityName = value; }
		}

		[XmlElement(ElementName="MunicipalityNameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MunicipalityNameLatin;
		
		[XmlIgnore]
		public string MunicipalityNameLatin
		{ 
			get { return __MunicipalityNameLatin; }
			set { __MunicipalityNameLatin = value; }
		}

		[XmlElement(ElementName="SettlementCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __SettlementCode;
		
		[XmlIgnore]
		public string SettlementCode
		{ 
			get { return __SettlementCode; }
			set { __SettlementCode = value; }
		}

		[XmlElement(ElementName="SettlementName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __SettlementName;
		
		[XmlIgnore]
		public string SettlementName
		{ 
			get { return __SettlementName; }
			set { __SettlementName = value; }
		}

		[XmlElement(ElementName="SettlementNameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __SettlementNameLatin;
		
		[XmlIgnore]
		public string SettlementNameLatin
		{ 
			get { return __SettlementNameLatin; }
			set { __SettlementNameLatin = value; }
		}

		[XmlElement(ElementName="LocationCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __LocationCode;
		
		[XmlIgnore]
		public string LocationCode
		{ 
			get { return __LocationCode; }
			set { __LocationCode = value; }
		}

		[XmlElement(ElementName="LocationName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __LocationName;
		
		[XmlIgnore]
		public string LocationName
		{ 
			get { return __LocationName; }
			set { __LocationName = value; }
		}

		[XmlElement(ElementName="LocationNameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __LocationNameLatin;
		
		[XmlIgnore]
		public string LocationNameLatin
		{ 
			get { return __LocationNameLatin; }
			set { __LocationNameLatin = value; }
		}

		[XmlElement(ElementName="BuildingNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __BuildingNumber;
		
		[XmlIgnore]
		public string BuildingNumber
		{ 
			get { return __BuildingNumber; }
			set { __BuildingNumber = value; }
		}

		[XmlElement(ElementName="Entrance",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Entrance;
		
		[XmlIgnore]
		public string Entrance
		{ 
			get { return __Entrance; }
			set { __Entrance = value; }
		}

		[XmlElement(ElementName="Floor",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Floor;
		
		[XmlIgnore]
		public string Floor
		{ 
			get { return __Floor; }
			set { __Floor = value; }
		}

		[XmlElement(ElementName="Apartment",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Apartment;
		
		[XmlIgnore]
		public string Apartment
		{ 
			get { return __Apartment; }
			set { __Apartment = value; }
		}

		public PermanentAddress()
		{
		}
	}


	[XmlType(TypeName="PersonalIdentityInfoResponseType",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PersonalIdentityInfoResponseType
	{

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.MVR.ReturnInformation),ElementName="ReturnInformations",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.MVR.ReturnInformation __ReturnInformations;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.MVR.ReturnInformation ReturnInformations
		{
			get {return __ReturnInformations;}
			set {__ReturnInformations = value;}
		}

		[XmlElement(ElementName="EGN",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __EGN;
		
		[XmlIgnore]
		public string EGN
		{ 
			get { return __EGN; }
			set { __EGN = value; }
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.MVR.PersonNames),ElementName="PersonNames",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.MVR.PersonNames __PersonNames;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.MVR.PersonNames PersonNames
		{
			get {return __PersonNames;}
			set {__PersonNames = value;}
		}

		[XmlElement(ElementName="DocumentType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DocumentType;
		
		[XmlIgnore]
		public string DocumentType
		{ 
			get { return __DocumentType; }
			set { __DocumentType = value; }
		}

		[XmlElement(ElementName="DocumentTypeLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DocumentTypeLatin;
		
		[XmlIgnore]
		public string DocumentTypeLatin
		{ 
			get { return __DocumentTypeLatin; }
			set { __DocumentTypeLatin = value; }
		}

		[XmlElement(ElementName="IdentityDocumentNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IdentityDocumentNumber;
		
		[XmlIgnore]
		public string IdentityDocumentNumber
		{ 
			get { return __IdentityDocumentNumber; }
			set { __IdentityDocumentNumber = value; }
		}

		[XmlElement(ElementName="IssueDate",Form=XmlSchemaForm.Qualified,DataType="dateTime",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __IssueDate;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __IssueDateSpecified { get { return __IssueDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? IssueDate
		{ 
			get { return __IssueDate; }
			set { __IssueDate = value; }
		}
		


		[XmlElement(ElementName="IssuerPlace",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IssuerPlace;
		
		[XmlIgnore]
		public string IssuerPlace
		{ 
			get { return __IssuerPlace; }
			set { __IssuerPlace = value; }
		}

		[XmlElement(ElementName="IssuerPlaceLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IssuerPlaceLatin;
		
		[XmlIgnore]
		public string IssuerPlaceLatin
		{ 
			get { return __IssuerPlaceLatin; }
			set { __IssuerPlaceLatin = value; }
		}

		[XmlElement(ElementName="IssuerName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IssuerName;
		
		[XmlIgnore]
		public string IssuerName
		{ 
			get { return __IssuerName; }
			set { __IssuerName = value; }
		}

		[XmlElement(ElementName="IssuerNameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IssuerNameLatin;
		
		[XmlIgnore]
		public string IssuerNameLatin
		{ 
			get { return __IssuerNameLatin; }
			set { __IssuerNameLatin = value; }
		}

		[XmlElement(ElementName="ValidDate",Form=XmlSchemaForm.Qualified,DataType="dateTime",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __ValidDate;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ValidDateSpecified { get { return __ValidDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? ValidDate
		{ 
			get { return __ValidDate; }
			set { __ValidDate = value; }
		}
		


		[XmlElement(ElementName="BirthDate",Form=XmlSchemaForm.Qualified,DataType="dateTime",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __BirthDate;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __BirthDateSpecified { get { return __BirthDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? BirthDate
		{ 
			get { return __BirthDate; }
			set { __BirthDate = value; }
		}
		


		[XmlElement(Type=typeof(Eumis.RegiX.Rio.MVR.BirthPlace),ElementName="BirthPlace",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.MVR.BirthPlace __BirthPlace;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.MVR.BirthPlace BirthPlace
		{
			get {return __BirthPlace;}
			set {__BirthPlace = value;}
		}

		[XmlElement(ElementName="GenderName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __GenderName;
		
		[XmlIgnore]
		public string GenderName
		{ 
			get { return __GenderName; }
			set { __GenderName = value; }
		}

		[XmlElement(ElementName="GenderNameLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __GenderNameLatin;
		
		[XmlIgnore]
		public string GenderNameLatin
		{ 
			get { return __GenderNameLatin; }
			set { __GenderNameLatin = value; }
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.MVR.NationalityList),ElementName="NationalityList",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.MVR.NationalityList __NationalityList;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.MVR.NationalityList NationalityList
		{
			get {return __NationalityList;}
			set {__NationalityList = value;}
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.MVR.PermanentAddress),ElementName="PermanentAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.MVR.PermanentAddress __PermanentAddress;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.MVR.PermanentAddress PermanentAddress
		{
			get {return __PermanentAddress;}
			set {__PermanentAddress = value;}
		}

		[XmlElement(ElementName="Height",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="double",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public double __Height;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __HeightSpecified;
		
		[XmlIgnore]
		public double Height
		{ 
			get { return __Height; }
			set { __Height = value; __HeightSpecified = true; }
		}

		[XmlElement(ElementName="EyesColor",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __EyesColor;
		
		[XmlIgnore]
		public string EyesColor
		{ 
			get { return __EyesColor; }
			set { __EyesColor = value; }
		}

		[XmlElement(ElementName="Picture",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __Picture;
		
		[XmlIgnore]
		public byte[] Picture
		{ 
			get { return __Picture; }
			set { __Picture = value; }
		}

		[XmlElement(ElementName="IdentitySignature",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __IdentitySignature;
		
		[XmlIgnore]
		public byte[] IdentitySignature
		{ 
			get { return __IdentitySignature; }
			set { __IdentitySignature = value; }
		}

		public PersonalIdentityInfoResponseType()
		{
		}
	}


	[XmlType(TypeName="ReturnInformation",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ReturnInformation
	{

		[XmlElement(ElementName="ReturnCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ReturnCode;
		
		[XmlIgnore]
		public string ReturnCode
		{ 
			get { return __ReturnCode; }
			set { __ReturnCode = value; }
		}

		[XmlElement(ElementName="Info",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Info;
		
		[XmlIgnore]
		public string Info
		{ 
			get { return __Info; }
			set { __Info = value; }
		}

		public ReturnInformation()
		{
		}
	}


	[XmlRoot(ElementName="PersonalIdentityInfoResponse",Namespace="http://egov.bg/RegiX/MVR/BDS/PersonalIdentityInfoResponse",IsNullable=false),Serializable]
	public partial class PersonalIdentityInfoResponse : Eumis.RegiX.Rio.MVR.PersonalIdentityInfoResponseType
	{

		public PersonalIdentityInfoResponse() : base()
		{
		}
	}
}
