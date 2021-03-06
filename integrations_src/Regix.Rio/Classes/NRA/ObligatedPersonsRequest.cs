//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace Eumis.RegiX.Rio.NRA
{


	[Serializable]
	public enum EikTypeTypeRequest
	{
		[XmlEnum(Name="Bulstat")] Bulstat,
		[XmlEnum(Name="EGN")] EGN,
		[XmlEnum(Name="LNC")] LNC,
		[XmlEnum(Name="SystemNo")] SystemNo,
		[XmlEnum(Name="BulstatCL")] BulstatCL
	}



	[XmlType(TypeName="IdentityTypeRequest",Namespace="http://egov.bg/RegiX/NRA/Obligations/Request"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class IdentityTypeRequest
	{

		[XmlElement(ElementName="ID",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/NRA/Obligations/Request")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ID;
		
		[XmlIgnore]
		public string ID
		{ 
			get { return __ID; }
			set { __ID = value; }
		}

		[XmlElement(ElementName="TYPE",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://egov.bg/RegiX/NRA/Obligations/Request")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.NRA.EikTypeTypeRequest __TYPE;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TYPESpecified;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.NRA.EikTypeTypeRequest TYPE
		{ 
			get { return __TYPE; }
			set { __TYPE = value; __TYPESpecified = true; }
		}

		public IdentityTypeRequest()
		{
		}
	}


	[XmlType(TypeName="StatusTypeRequest",Namespace="http://egov.bg/RegiX/NRA/Obligations/Request"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class StatusTypeRequest
	{

		[XmlElement(ElementName="Code",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="int",Namespace="http://egov.bg/RegiX/NRA/Obligations/Request")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int __Code;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __CodeSpecified;
		
		[XmlIgnore]
		public int Code
		{ 
			get { return __Code; }
			set { __Code = value; __CodeSpecified = true; }
		}

		[XmlElement(ElementName="Message",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://egov.bg/RegiX/NRA/Obligations/Request")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Message;
		
		[XmlIgnore]
		public string Message
		{ 
			get { return __Message; }
			set { __Message = value; }
		}

		public StatusTypeRequest()
		{
		}
	}


	[XmlRoot(ElementName="ObligationRequest",Namespace="http://egov.bg/RegiX/NRA/Obligations/Request",IsNullable=false),Serializable]
	[XmlType(TypeName="ObligationRequest",Namespace="http://egov.bg/RegiX/NRA/Obligations/Request")]
	public partial class ObligationRequest
	{

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.NRA.IdentityTypeRequest),ElementName="Identity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://egov.bg/RegiX/NRA/Obligations/Request")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.NRA.IdentityTypeRequest __Identity;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.NRA.IdentityTypeRequest Identity
		{
			get {return __Identity;}
			set {__Identity = value;}
		}

		[XmlElement(ElementName="Threshold",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="unsignedShort",Namespace="http://egov.bg/RegiX/NRA/Obligations/Request")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ushort __Threshold;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ThresholdSpecified;
		
		[XmlIgnore]
		public ushort Threshold
		{ 
			get { return __Threshold; }
			set { __Threshold = value; __ThresholdSpecified = true; }
		}

		public ObligationRequest()
		{
		}
	}
}
