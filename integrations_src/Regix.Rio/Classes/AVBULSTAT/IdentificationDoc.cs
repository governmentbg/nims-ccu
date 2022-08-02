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




	[XmlType(TypeName="IdentificationDoc",Namespace="http://www.bulstat.bg/IdentificationDoc"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class IdentificationDoc : Eumis.RegiX.Rio.AVBULSTAT.Entry
	{

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="IDType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/IdentificationDoc")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __IDType;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry IDType
		{
			get {return __IDType;}
			set {__IDType = value;}
		}

		[XmlElement(ElementName="IDNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/IdentificationDoc")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IDNumber;
		
		[XmlIgnore]
		public string IDNumber
		{ 
			get { return __IDNumber; }
			set { __IDNumber = value; }
		}

		[XmlElement(Type=typeof(Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry),ElementName="Country",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.bulstat.bg/IdentificationDoc")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry __Country;
		
		[XmlIgnore]
		public Eumis.RegiX.Rio.AVBULSTAT.NomenclatureEntry Country
		{
			get {return __Country;}
			set {__Country = value;}
		}

		[XmlElement(ElementName="IssueDate",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/IdentificationDoc")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IssueDate;
		
		[XmlIgnore]
		public string IssueDate
		{ 
			get { return __IssueDate; }
			set { __IssueDate = value; }
		}

		[XmlElement(ElementName="ExpiryDate",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.bulstat.bg/IdentificationDoc")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ExpiryDate;
		
		[XmlIgnore]
		public string ExpiryDate
		{ 
			get { return __ExpiryDate; }
			set { __ExpiryDate = value; }
		}

		public IdentificationDoc() : base()
		{
		}
	}
}
