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




	[XmlType(TypeName="BFPContractIndicator",Namespace="http://ereg.egov.bg/segment/R-10038"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class BFPContractIndicator
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

		[XmlElement(Type=typeof(SelectedIndicator),ElementName="SelectedIndicator",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SelectedIndicator __SelectedIndicator;
		
		[XmlIgnore]
		public SelectedIndicator SelectedIndicator
		{
			get {return __SelectedIndicator;}
			set {__SelectedIndicator = value;}
		}

		[XmlElement(ElementName="BaseMen",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __BaseMen;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __BaseMenSpecified;
		
		[XmlIgnore]
		public decimal BaseMen
		{ 
			get { return __BaseMen; }
			set { __BaseMen = value; __BaseMenSpecified = true; }
		}

		[XmlElement(ElementName="BaseWomen",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __BaseWomen;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __BaseWomenSpecified;
		
		[XmlIgnore]
		public decimal BaseWomen
		{ 
			get { return __BaseWomen; }
			set { __BaseWomen = value; __BaseWomenSpecified = true; }
		}

		[XmlElement(ElementName="BaseTotal",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __BaseTotal;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __BaseTotalSpecified;
		
		[XmlIgnore]
		public decimal BaseTotal
		{ 
			get { return __BaseTotal; }
			set { __BaseTotal = value; __BaseTotalSpecified = true; }
		}

		[XmlElement(ElementName="TargetMen",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __TargetMen;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TargetMenSpecified;
		
		[XmlIgnore]
		public decimal TargetMen
		{ 
			get { return __TargetMen; }
			set { __TargetMen = value; __TargetMenSpecified = true; }
		}

		[XmlElement(ElementName="TargetWomen",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __TargetWomen;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TargetWomenSpecified;
		
		[XmlIgnore]
		public decimal TargetWomen
		{ 
			get { return __TargetWomen; }
			set { __TargetWomen = value; __TargetWomenSpecified = true; }
		}

		[XmlElement(ElementName="TargetTotal",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __TargetTotal;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TargetTotalSpecified;
		
		[XmlIgnore]
		public decimal TargetTotal
		{ 
			get { return __TargetTotal; }
			set { __TargetTotal = value; __TargetTotalSpecified = true; }
		}

		[XmlElement(ElementName="Description",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Description;
		
		[XmlIgnore]
		public string Description
		{ 
			get { return __Description; }
			set { __Description = value; }
		}

		[XmlElement(ElementName="IsLocked",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __IsLocked;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __IsLockedSpecified;
		
		[XmlIgnore]
		public bool IsLocked
		{ 
			get { return __IsLocked; }
			set { __IsLocked = value; __IsLockedSpecified = true; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="ProgrammePriority",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __ProgrammePriority;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature ProgrammePriority
		{
			get {return __ProgrammePriority;}
			set {__ProgrammePriority = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="InvestmentPriority",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __InvestmentPriority;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature InvestmentPriority
		{
			get {return __InvestmentPriority;}
			set {__InvestmentPriority = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.EnumNomenclature),ElementName="FinanceSource",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.EnumNomenclature __FinanceSource;
		
		[XmlIgnore]
		public Eumis.Rio.EnumNomenclature FinanceSource
		{
			get {return __FinanceSource;}
			set {__FinanceSource = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="SpecificTarget",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __SpecificTarget;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature SpecificTarget
		{
			get {return __SpecificTarget;}
			set {__SpecificTarget = value;}
		}

		public BFPContractIndicator()
		{
		}
	}


	[XmlType(TypeName="SelectedIndicator",Namespace="http://ereg.egov.bg/segment/R-10038"),Serializable]
	public partial class SelectedIndicator
	{

		[XmlElement(ElementName="Id",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[XmlElement(ElementName="TypeName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TypeName;
		
		[XmlIgnore]
		public string TypeName
		{ 
			get { return __TypeName; }
			set { __TypeName = value; }
		}

		[XmlElement(ElementName="TrendName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TrendName;
		
		[XmlIgnore]
		public string TrendName
		{ 
			get { return __TrendName; }
			set { __TrendName = value; }
		}

		[XmlElement(ElementName="KindName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __KindName;
		
		[XmlIgnore]
		public string KindName
		{ 
			get { return __KindName; }
			set { __KindName = value; }
		}

		[XmlElement(ElementName="MeasureName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MeasureName;
		
		[XmlIgnore]
		public string MeasureName
		{ 
			get { return __MeasureName; }
			set { __MeasureName = value; }
		}

		[XmlElement(ElementName="AggregatedReport",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AggregatedReport;
		
		[XmlIgnore]
		public string AggregatedReport
		{ 
			get { return __AggregatedReport; }
			set { __AggregatedReport = value; }
		}

		[XmlElement(ElementName="AggregatedTarget",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AggregatedTarget;
		
		[XmlIgnore]
		public string AggregatedTarget
		{ 
			get { return __AggregatedTarget; }
			set { __AggregatedTarget = value; }
		}

		[XmlElement(ElementName="HasGenderDivision",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace="http://ereg.egov.bg/segment/R-10038")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __HasGenderDivision;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __HasGenderDivisionSpecified;
		
		[XmlIgnore]
		public bool HasGenderDivision
		{ 
			get { return __HasGenderDivision; }
			set { __HasGenderDivision = value; __HasGenderDivisionSpecified = true; }
		}

		public SelectedIndicator()
		{
		}
	}
}
