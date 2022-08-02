//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace Eumis.RegiX.Rio.DAEU
{


	[Serializable]
	public enum IdentifierType
	{
		[XmlEnum(Name="Bulstat")] Bulstat,
		[XmlEnum(Name="EGN")] EGN,
		[XmlEnum(Name="LNCh")] LNCh,
		[XmlEnum(Name="EIK")] EIK,
		[XmlEnum(Name="SystemNo")] SystemNo,
		[XmlEnum(Name="BulstatCL")] BulstatCL
	}



	[XmlType(TypeName="SearchByIdentifierRequestType",Namespace="http://egov.bg/RegiX/DaeuReports/SearchByIdentifierRequest"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SearchByIdentifierRequestType
	{

		[XmlElement(ElementName="Identifier",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/DaeuReports/SearchByIdentifierRequest")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Identifier;
		
		[XmlIgnore]
		public string Identifier
		{ 
			get { return __Identifier; }
			set { __Identifier = value; }
		}

		[XmlElement(ElementName="IdentifierType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://egov.bg/RegiX/DaeuReports/SearchByIdentifierRequest")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.DAEU.IdentifierType __IdentifierType;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __IdentifierTypeSpecified;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.DAEU.IdentifierType IdentifierType
		{ 
			get { return __IdentifierType; }
			set { __IdentifierType = value; __IdentifierTypeSpecified = true; }
		}

		[XmlElement(ElementName="DateFrom",Form=XmlSchemaForm.Qualified,DataType="dateTime",Namespace="http://egov.bg/RegiX/DaeuReports/SearchByIdentifierRequest")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __DateFrom;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DateFromSpecified { get { return __DateFrom.HasValue; } }
		
		[XmlIgnore]
		public DateTime? DateFrom
		{ 
			get { return __DateFrom; }
			set { __DateFrom = value; }
		}
		


		[XmlElement(ElementName="DateTo",Form=XmlSchemaForm.Qualified,DataType="dateTime",Namespace="http://egov.bg/RegiX/DaeuReports/SearchByIdentifierRequest")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __DateTo;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DateToSpecified { get { return __DateTo.HasValue; } }
		
		[XmlIgnore]
		public DateTime? DateTo
		{ 
			get { return __DateTo; }
			set { __DateTo = value; }
		}
		


		public SearchByIdentifierRequestType()
		{
		}
	}


	[XmlRoot(ElementName="SearchByIdentifierRequest",Namespace="http://egov.bg/RegiX/DaeuReports/SearchByIdentifierRequest",IsNullable=false),Serializable]
	public partial class SearchByIdentifierRequest : Eumis.RegiX.Rio.DAEU.SearchByIdentifierRequestType
	{

		public SearchByIdentifierRequest() : base()
		{
		}
	}
}
