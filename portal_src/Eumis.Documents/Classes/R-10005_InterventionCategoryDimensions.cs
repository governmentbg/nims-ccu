//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_10005
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10005";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PublicNomenclatureCollection : System.Collections.Generic.List<R_10001.PublicNomenclature>
	{
	}



	[XmlType(TypeName="InterventionCategoryDimensions",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class InterventionCategoryDimensions
	{

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10001.PublicNomenclature),ElementName="InterventionField",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10001.PublicNomenclature),ElementName="FormOfFinance",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10001.PublicNomenclature),ElementName="TerritorialDimension",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10001.PublicNomenclature),ElementName="TerritorialDeliveryMechanism",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10001.PublicNomenclature),ElementName="ThematicObjective",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10001.PublicNomenclature),ElementName="ESFSecondaryTheme",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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

		[System.Web.Script.Serialization.ScriptIgnore]
        [Newtonsoft.Json.JsonIgnore]
		[XmlElement(Type=typeof(R_10001.PublicNomenclature),ElementName="EconomicDimension",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
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
