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




	[XmlType(TypeName="BFPContractPlan",Namespace="http://ereg.egov.bg/segment/R-10070"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class BFPContractPlan
	{

		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10070")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="ErrandArea",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10070")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PublicNomenclature __ErrandArea;
		
		[XmlIgnore]
		public Eumis.Rio.PublicNomenclature ErrandArea
		{
			get {return __ErrandArea;}
			set {__ErrandArea = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="ErrandLegalAct",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10070")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __ErrandLegalAct;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature ErrandLegalAct
		{
			get {return __ErrandLegalAct;}
			set {__ErrandLegalAct = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="ErrandType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10070")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PublicNomenclature __ErrandType;
		
		[XmlIgnore]
		public Eumis.Rio.PublicNomenclature ErrandType
		{
			get {return __ErrandType;}
			set {__ErrandType = value;}
		}

		[XmlElement(ElementName="Description",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10070")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Description;
		
		[XmlIgnore]
		public string Description
		{ 
			get { return __Description; }
			set { __Description = value; }
		}

		[XmlElement(ElementName="IsCentralProcurement",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace="http://ereg.egov.bg/segment/R-10070")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __IsCentralProcurement;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __IsCentralProcurementSpecified;
		
		[XmlIgnore]
		public bool IsCentralProcurement
		{ 
			get { return __IsCentralProcurement; }
			set { __IsCentralProcurement = value; __IsCentralProcurementSpecified = true; }
		}

		public BFPContractPlan()
		{
		}
	}
}
