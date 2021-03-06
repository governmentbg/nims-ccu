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




	[XmlType(TypeName="OfferBasicData",Namespace="http://ereg.egov.bg/segment/R-10079"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class OfferBasicData
	{

		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __id;
		
		[XmlIgnore]
		public string id
		{ 
			get { return __id; }
			set { __id = value; }
		}

		[XmlAttribute(AttributeName="isLocked",DataType="boolean")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __isLockedSpecified;
		
		[XmlIgnore]
		public bool isLocked
		{ 
			get { return __isLocked; }
			set { __isLocked = value; __isLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="BeneficiaryUinType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10079")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __BeneficiaryUinType;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature BeneficiaryUinType
		{
			get {return __BeneficiaryUinType;}
			set {__BeneficiaryUinType = value;}
		}

		[XmlElement(ElementName="BeneficiaryUin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10079")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __BeneficiaryUin;
		
		[XmlIgnore]
		public string BeneficiaryUin
		{ 
			get { return __BeneficiaryUin; }
			set { __BeneficiaryUin = value; }
		}

		[XmlElement(ElementName="PlanName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10079")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PlanName;
		
		[XmlIgnore]
		public string PlanName
		{ 
			get { return __PlanName; }
			set { __PlanName = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="PlanErrandArea",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10079")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PublicNomenclature __PlanErrandArea;
		
		[XmlIgnore]
		public Eumis.Rio.PublicNomenclature PlanErrandArea
		{
			get {return __PlanErrandArea;}
			set {__PlanErrandArea = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="PlanErrandLegalAct",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10079")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __PlanErrandLegalAct;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature PlanErrandLegalAct
		{
			get {return __PlanErrandLegalAct;}
			set {__PlanErrandLegalAct = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="PlanErrandType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10079")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PublicNomenclature __PlanErrandType;
		
		[XmlIgnore]
		public Eumis.Rio.PublicNomenclature PlanErrandType
		{
			get {return __PlanErrandType;}
			set {__PlanErrandType = value;}
		}

		[XmlElement(ElementName="PlanExpectedAmount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10079")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __PlanExpectedAmount;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __PlanExpectedAmountSpecified;
		
		[XmlIgnore]
		public decimal PlanExpectedAmount
		{ 
			get { return __PlanExpectedAmount; }
			set { __PlanExpectedAmount = value; __PlanExpectedAmountSpecified = true; }
		}

		[XmlElement(ElementName="PlanDescription",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10079")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PlanDescription;
		
		[XmlIgnore]
		public string PlanDescription
		{ 
			get { return __PlanDescription; }
			set { __PlanDescription = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.DifferentiatedPosition),ElementName="DifferentiatedPosition",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10079")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.DifferentiatedPosition __DifferentiatedPosition;
		
		[XmlIgnore]
		public Eumis.Rio.DifferentiatedPosition DifferentiatedPosition
		{
			get {return __DifferentiatedPosition;}
			set {__DifferentiatedPosition = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.EnumNomenclature),ElementName="BeneficiaryRegistrationVAT",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10079")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.EnumNomenclature __BeneficiaryRegistrationVAT;
		
		[XmlIgnore]
		public Eumis.Rio.EnumNomenclature BeneficiaryRegistrationVAT
		{
			get {return __BeneficiaryRegistrationVAT;}
			set {__BeneficiaryRegistrationVAT = value;}
		}

		public OfferBasicData()
		{
		}
	}
}
