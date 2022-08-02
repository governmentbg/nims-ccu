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


	using AttachedDocumentCollection = System.Collections.Generic.List<Eumis.Rio.AttachedDocument>;



	[XmlType(TypeName="Standpoint",Namespace="http://ereg.egov.bg/segment/R-10027"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Standpoint
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

		[XmlAttribute(AttributeName="version",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __version;
		
		[XmlIgnore]
		public string version
		{ 
			get { return __version; }
			set { __version = value; }
		}

		[XmlAttribute(AttributeName="createDate",DataType="dateTime")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime __createDate;
		
		[XmlIgnore]
		public DateTime createDate
		{ 
		get { return __createDate; }
		set { __createDate = value; }
		}
		


		[XmlAttribute(AttributeName="modificationDate",DataType="dateTime")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime __modificationDate;
		
		[XmlIgnore]
		public DateTime modificationDate
		{ 
		get { return __modificationDate; }
		set { __modificationDate = value; }
		}
		


		[XmlElement(ElementName="Subject",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10027")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Subject;
		
		[XmlIgnore]
		public string Subject
		{ 
			get { return __Subject; }
			set { __Subject = value; }
		}

		[XmlElement(ElementName="Content",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://ereg.egov.bg/segment/R-10027")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Content;
		
		[XmlIgnore]
		public string Content
		{ 
			get { return __Content; }
			set { __Content = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.AttachedDocument),ElementName="AttachedDocument",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10027")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AttachedDocumentCollection __AttachedDocumentCollection;
		
		[XmlIgnore]
		public AttachedDocumentCollection AttachedDocumentCollection
		{
			get
			{
				if (__AttachedDocumentCollection == null) __AttachedDocumentCollection = new AttachedDocumentCollection();
				return __AttachedDocumentCollection;
			}
			set {__AttachedDocumentCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.Signature),ElementName="Signature",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.Signature __Signature;
		
		[XmlIgnore]
		public Eumis.Rio.Signature Signature
		{
			get {return __Signature;}
			set {__Signature = value;}
		}

		public Standpoint()
		{
		}
	}
}