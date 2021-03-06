//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10004
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10004";
	}




	[XmlType(TypeName="Company",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Company
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isActivated",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActivated;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActivatedSpecified;
		
		[XmlIgnore]
		public bool isActivated
		{ 
			get { return __isActivated; }
			set { __isActivated = value; __isActivatedSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isActive",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActive;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActiveSpecified;
		
		[XmlIgnore]
		public bool isActive
		{ 
			get { return __isActive; }
			set { __isActive = value; __isActiveSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="gid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __gid;
		
		[XmlIgnore]
		public string gid
		{ 
			get { return __gid; }
			set { __gid = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="NameEN",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NameEN;
		
		[XmlIgnore]
		public string NameEN
		{ 
			get { return __NameEN; }
			set { __NameEN = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Uin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Uin;
		
		[XmlIgnore]
		public string Uin
		{ 
			get { return __Uin; }
			set { __Uin = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10000.PrivateNomenclature),ElementName="UinType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_10000.PrivateNomenclature __UinType;
		
		[XmlIgnore]
		public R_10000.PrivateNomenclature UinType
		{
			get {return __UinType;}
			set {__UinType = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10000.PrivateNomenclature),ElementName="CompanyType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_10000.PrivateNomenclature __CompanyType;
		
		[XmlIgnore]
		public R_10000.PrivateNomenclature CompanyType
		{
			get {return __CompanyType;}
			set {__CompanyType = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10000.PrivateNomenclature),ElementName="CompanyLegalType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_10000.PrivateNomenclature __CompanyLegalType;
		
		[XmlIgnore]
		public R_10000.PrivateNomenclature CompanyLegalType
		{
			get {return __CompanyLegalType;}
			set {__CompanyLegalType = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="FinancialContribution",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __FinancialContribution;
		
		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __FinancialContributionSpecified;
		
		[XmlIgnore]
		public decimal FinancialContribution
		{ 
			get { return __FinancialContribution; }
			set { __FinancialContribution = value; __FinancialContributionSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10003.Address),ElementName="Seat",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_10003.Address __Seat;
		
		[XmlIgnore]
		public R_10003.Address Seat
		{
			get {return __Seat;}
			set {__Seat = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10003.Address),ElementName="Correspondence",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_10003.Address __Correspondence;
		
		[XmlIgnore]
		public R_10003.Address Correspondence
		{
			get {return __Correspondence;}
			set {__Correspondence = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Email",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Email;
		
		[XmlIgnore]
		public string Email
		{ 
			get { return __Email; }
			set { __Email = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Phone1",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Phone1;
		
		[XmlIgnore]
		public string Phone1
		{ 
			get { return __Phone1; }
			set { __Phone1 = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Phone2",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Phone2;
		
		[XmlIgnore]
		public string Phone2
		{ 
			get { return __Phone2; }
			set { __Phone2 = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="Fax",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Fax;
		
		[XmlIgnore]
		public string Fax
		{ 
			get { return __Fax; }
			set { __Fax = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="CompanyRepresentativePerson",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CompanyRepresentativePerson;
		
		[XmlIgnore]
		public string CompanyRepresentativePerson
		{ 
			get { return __CompanyRepresentativePerson; }
			set { __CompanyRepresentativePerson = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="CompanyContactPerson",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CompanyContactPerson;
		
		[XmlIgnore]
		public string CompanyContactPerson
		{ 
			get { return __CompanyContactPerson; }
			set { __CompanyContactPerson = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="CompanyContactPersonPhone",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CompanyContactPersonPhone;
		
		[XmlIgnore]
		public string CompanyContactPersonPhone
		{ 
			get { return __CompanyContactPersonPhone; }
			set { __CompanyContactPersonPhone = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(ElementName="CompanyContactPersonEmail",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CompanyContactPersonEmail;
		
		[XmlIgnore]
		public string CompanyContactPersonEmail
		{ 
			get { return __CompanyContactPersonEmail; }
			set { __CompanyContactPersonEmail = value; }
		}

		public Company()
		{
		}
	}
}
