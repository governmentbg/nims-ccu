//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace Eumis.RegiX.Rio.AVBULSTAT
{




	[XmlType(TypeName="Document",Namespace="http://www.bulstat.bg/Document"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Document : Eumis.RegiX.Rio.AVBULSTAT.Entry
	{

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="DocumentType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/Document")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __DocumentType;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry DocumentType
		{
			get {return __DocumentType;}
			set {__DocumentType = value;}
		}

		[XmlElement(ElementName="DocumentNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/Document")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DocumentNumber;
		
		[XmlIgnore]
		public string DocumentNumber
		{ 
			get { return __DocumentNumber; }
			set { __DocumentNumber = value; }
		}

		[XmlElement(ElementName="DocumentDate",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/Document")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DocumentDate;
		
		[XmlIgnore]
		public string DocumentDate
		{ 
			get { return __DocumentDate; }
			set { __DocumentDate = value; }
		}

		[XmlElement(ElementName="ValidFromDate",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/Document")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ValidFromDate;
		
		[XmlIgnore]
		public string ValidFromDate
		{ 
			get { return __ValidFromDate; }
			set { __ValidFromDate = value; }
		}

		[XmlElement(ElementName="DocumentName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/Document")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DocumentName;
		
		[XmlIgnore]
		public string DocumentName
		{ 
			get { return __DocumentName; }
			set { __DocumentName = value; }
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.Subject),ElementName="Author",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/Document")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.Subject __Author;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.Subject Author
		{
			get {return __Author;}
			set {__Author = value;}
		}

		[XmlElement(ElementName="AuthorName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/Document")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AuthorName;
		
		[XmlIgnore]
		public string AuthorName
		{ 
			get { return __AuthorName; }
			set { __AuthorName = value; }
		}

		public Document() : base()
		{
		}
	}
}
