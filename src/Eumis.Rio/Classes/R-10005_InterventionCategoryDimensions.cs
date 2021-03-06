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


	using PublicNomenclatureCollection = System.Collections.Generic.List<Eumis.Rio.PublicNomenclature>;



	[XmlType(TypeName="InterventionCategoryDimensions",Namespace="http://ereg.egov.bg/segment/R-10005"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class InterventionCategoryDimensions
	{

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="InterventionField",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10005")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PublicNomenclatureCollection __InterventionFieldCollection;
		
		[XmlIgnore]
		public PublicNomenclatureCollection InterventionFieldCollection
		{
			get
			{
				if (__InterventionFieldCollection == null) __InterventionFieldCollection = new PublicNomenclatureCollection();
				return __InterventionFieldCollection;
			}
			set {__InterventionFieldCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="FormOfFinance",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10005")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PublicNomenclatureCollection __FormOfFinanceCollection;
		
		[XmlIgnore]
		public PublicNomenclatureCollection FormOfFinanceCollection
		{
			get
			{
				if (__FormOfFinanceCollection == null) __FormOfFinanceCollection = new PublicNomenclatureCollection();
				return __FormOfFinanceCollection;
			}
			set {__FormOfFinanceCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="TerritorialDimension",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10005")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PublicNomenclatureCollection __TerritorialDimensionCollection;
		
		[XmlIgnore]
		public PublicNomenclatureCollection TerritorialDimensionCollection
		{
			get
			{
				if (__TerritorialDimensionCollection == null) __TerritorialDimensionCollection = new PublicNomenclatureCollection();
				return __TerritorialDimensionCollection;
			}
			set {__TerritorialDimensionCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="TerritorialDeliveryMechanism",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10005")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PublicNomenclatureCollection __TerritorialDeliveryMechanismCollection;
		
		[XmlIgnore]
		public PublicNomenclatureCollection TerritorialDeliveryMechanismCollection
		{
			get
			{
				if (__TerritorialDeliveryMechanismCollection == null) __TerritorialDeliveryMechanismCollection = new PublicNomenclatureCollection();
				return __TerritorialDeliveryMechanismCollection;
			}
			set {__TerritorialDeliveryMechanismCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="ThematicObjective",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10005")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PublicNomenclatureCollection __ThematicObjectiveCollection;
		
		[XmlIgnore]
		public PublicNomenclatureCollection ThematicObjectiveCollection
		{
			get
			{
				if (__ThematicObjectiveCollection == null) __ThematicObjectiveCollection = new PublicNomenclatureCollection();
				return __ThematicObjectiveCollection;
			}
			set {__ThematicObjectiveCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="ESFSecondaryTheme",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10005")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PublicNomenclatureCollection __ESFSecondaryThemeCollection;
		
		[XmlIgnore]
		public PublicNomenclatureCollection ESFSecondaryThemeCollection
		{
			get
			{
				if (__ESFSecondaryThemeCollection == null) __ESFSecondaryThemeCollection = new PublicNomenclatureCollection();
				return __ESFSecondaryThemeCollection;
			}
			set {__ESFSecondaryThemeCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PublicNomenclature),ElementName="EconomicDimension",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10005")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PublicNomenclatureCollection __EconomicDimensionCollection;
		
		[XmlIgnore]
		public PublicNomenclatureCollection EconomicDimensionCollection
		{
			get
			{
				if (__EconomicDimensionCollection == null) __EconomicDimensionCollection = new PublicNomenclatureCollection();
				return __EconomicDimensionCollection;
			}
			set {__EconomicDimensionCollection = value;}
		}

		public InterventionCategoryDimensions()
		{
		}
	}
}
