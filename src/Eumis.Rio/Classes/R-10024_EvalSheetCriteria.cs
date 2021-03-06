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




	[XmlType(TypeName="EvalSheetCriteria",Namespace="http://ereg.egov.bg/segment/R-10024"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class EvalSheetCriteria
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

		[XmlElement(Type=typeof(Eumis.Rio.EvalTableCriteria),ElementName="EvalTableCriteria",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10024")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.EvalTableCriteria __EvalTableCriteria;
		
		[XmlIgnore]
		public Eumis.Rio.EvalTableCriteria EvalTableCriteria
		{
			get {return __EvalTableCriteria;}
			set {__EvalTableCriteria = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PrivateNomenclature),ElementName="Accept",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10024")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.PrivateNomenclature __Accept;
		
		[XmlIgnore]
		public Eumis.Rio.PrivateNomenclature Accept
		{
			get {return __Accept;}
			set {__Accept = value;}
		}

		[XmlElement(ElementName="Evaluation",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="decimal",Namespace="http://ereg.egov.bg/segment/R-10024")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public decimal __Evaluation;
		
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __EvaluationSpecified;
		
		[XmlIgnore]
		public decimal Evaluation
		{ 
			get { return __Evaluation; }
			set { __Evaluation = value; __EvaluationSpecified = true; }
		}

		[XmlElement(ElementName="Note",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10024")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Note;
		
		[XmlIgnore]
		public string Note
		{ 
			get { return __Note; }
			set { __Note = value; }
		}

		public EvalSheetCriteria()
		{
		}
	}
}
