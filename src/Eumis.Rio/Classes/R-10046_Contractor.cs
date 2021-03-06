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




	[XmlType(TypeName="Contractor",Namespace="http://ereg.egov.bg/segment/R-10046"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Contractor
	{

		[XmlAttribute(AttributeName="isActivated",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActivated;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActivatedSpecified;
		
		[XmlIgnore]
		public bool isActivated
		{ 
			get { return __isActivated; }
			set { __isActivated = value; __isActivatedSpecified = true; }
		}

		[XmlAttribute(AttributeName="isActive",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActive;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isActiveSpecified;
		
		[XmlIgnore]
		public bool isActive
		{ 
			get { return __isActive; }
			set { __isActive = value; __isActiveSpecified = true; }
		}

		[XmlAttribute(AttributeName="gid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __gid;
		
		[XmlIgnore]
		public string gid
		{ 
			get { return __gid; }
			set { __gid = value; }
		}

		[XmlElement(ElementName="Uin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10046")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Uin;
		
		[XmlIgnore]
		public string Uin
		{ 
			get { return __Uin; }
			set { __Uin = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="UinType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10046")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __UinType;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature UinType
		{
			get {return __UinType;}
			set {__UinType = value;}
		}

		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10046")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[XmlElement(ElementName="NameEN",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10046")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NameEN;
		
		[XmlIgnore]
		public string NameEN
		{ 
			get { return __NameEN; }
			set { __NameEN = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.Address),ElementName="Seat",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10046")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.Address __Seat;
		
		[XmlIgnore]
		public Eumis.Rio.Address Seat
		{
			get {return __Seat;}
			set {__Seat = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.EnumNomenclature),ElementName="VATRegistration",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10046")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.EnumNomenclature __VATRegistration;
		
		[XmlIgnore]
		public Eumis.Rio.EnumNomenclature VATRegistration
		{
			get {return __VATRegistration;}
			set {__VATRegistration = value;}
		}

		public Contractor()
		{
		}
	}
}
