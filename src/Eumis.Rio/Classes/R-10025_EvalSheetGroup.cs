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


	using EvalSheetCriteriaCollection = System.Collections.Generic.List<Eumis.Rio.EvalSheetCriteria>;



	[XmlType(TypeName="EvalSheetGroup",Namespace="http://ereg.egov.bg/segment/R-10025"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class EvalSheetGroup
	{

		[XmlAttribute(AttributeName="gid",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __gid;
		
		[XmlIgnore]
		public string gid
		{ 
			get { return __gid; }
			set { __gid = value; }
		}

		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10025")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.EvalSheetCriteria),ElementName="EvalSheetCriteria",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10025")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public EvalSheetCriteriaCollection __EvalSheetCriteriaCollection;
		
		[XmlIgnore]
		public EvalSheetCriteriaCollection EvalSheetCriteriaCollection
		{
			get
			{
				if (__EvalSheetCriteriaCollection == null) __EvalSheetCriteriaCollection = new EvalSheetCriteriaCollection();
				return __EvalSheetCriteriaCollection;
			}
			set {__EvalSheetCriteriaCollection = value;}
		}

		[XmlElement(ElementName="Total",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10025")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __Total;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TotalSpecified;
		
		[XmlIgnore]
		public decimal Total
		{ 
			get { return __Total; }
			set { __Total = value; __TotalSpecified = true; }
		}

		[XmlElement(ElementName="Limit",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10025")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __Limit;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __LimitSpecified;
		
		[XmlIgnore]
		public decimal Limit
		{ 
			get { return __Limit; }
			set { __Limit = value; __LimitSpecified = true; }
		}

		public EvalSheetGroup()
		{
		}
	}
}