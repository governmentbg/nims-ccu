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


	using ObjectCollection = System.Collections.Generic.List<Eumis.Rio.@Object>;

	using SPKIDataCollection = System.Collections.Generic.List<Eumis.Rio.SPKIData>;

	using KeyNameCollection = System.Collections.Generic.List<string>;

	using MgmtDataCollection = System.Collections.Generic.List<string>;

	using X509DataCollection = System.Collections.Generic.List<Eumis.Rio.X509Data>;

	using SPKISexpCollection = System.Collections.Generic.List<byte[]>;

	using ReferenceCollection = System.Collections.Generic.List<Eumis.Rio.Reference>;

	using TransformCollection = System.Collections.Generic.List<Eumis.Rio.Transform>;

	using XPathCollection = System.Collections.Generic.List<string>;

	using SignaturePropertyCollection = System.Collections.Generic.List<Eumis.Rio.SignatureProperty>;

	using KeyValueCollection = System.Collections.Generic.List<Eumis.Rio.KeyValue>;

	using RetrievalMethodCollection = System.Collections.Generic.List<Eumis.Rio.RetrievalMethod>;

	using PGPDataCollection = System.Collections.Generic.List<Eumis.Rio.PGPData>;



	[XmlType(TypeName="SignatureType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SignatureType
	{

		[XmlAttribute(AttributeName="Id",DataType="ID")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.SignedInfo),ElementName="SignedInfo",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.SignedInfo __SignedInfo;
		
		[XmlIgnore]
		public Eumis.Rio.SignedInfo SignedInfo
		{
			get {return __SignedInfo;}
			set {__SignedInfo = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.SignatureValue),ElementName="SignatureValue",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.SignatureValue __SignatureValue;
		
		[XmlIgnore]
		public Eumis.Rio.SignatureValue SignatureValue
		{
			get {return __SignatureValue;}
			set {__SignatureValue = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.KeyInfo),ElementName="KeyInfo",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.KeyInfo __KeyInfo;
		
		[XmlIgnore]
		public Eumis.Rio.KeyInfo KeyInfo
		{
			get {return __KeyInfo;}
			set {__KeyInfo = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.@Object),ElementName="Object",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ObjectCollection __ObjectCollection;
		
		[XmlIgnore]
		public ObjectCollection ObjectCollection
		{
			get
			{
				if (__ObjectCollection == null) __ObjectCollection = new ObjectCollection();
				return __ObjectCollection;
			}
			set {__ObjectCollection = value;}
		}

		public SignatureType()
		{
		}
	}


	[XmlType(TypeName="SignatureValueType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SignatureValueType
	{

		[XmlAttribute(AttributeName="Id",DataType="ID")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlText(DataType="base64Binary")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __MixedValue;
		
		[XmlIgnore]
		public byte[] MixedValue
		{ 
			get { return __MixedValue; }
			set { __MixedValue = value; }
		}

		public SignatureValueType()
		{
		}
	}


	[XmlType(TypeName="SignedInfoType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SignedInfoType
	{

		[XmlAttribute(AttributeName="Id",DataType="ID")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.CanonicalizationMethod),ElementName="CanonicalizationMethod",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.CanonicalizationMethod __CanonicalizationMethod;
		
		[XmlIgnore]
		public Eumis.Rio.CanonicalizationMethod CanonicalizationMethod
		{
			get {return __CanonicalizationMethod;}
			set {__CanonicalizationMethod = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.SignatureMethod),ElementName="SignatureMethod",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.SignatureMethod __SignatureMethod;
		
		[XmlIgnore]
		public Eumis.Rio.SignatureMethod SignatureMethod
		{
			get {return __SignatureMethod;}
			set {__SignatureMethod = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.Reference),ElementName="Reference",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ReferenceCollection __ReferenceCollection;
		
		[XmlIgnore]
		public ReferenceCollection ReferenceCollection
		{
			get
			{
				if (__ReferenceCollection == null) __ReferenceCollection = new ReferenceCollection();
				return __ReferenceCollection;
			}
			set {__ReferenceCollection = value;}
		}

		public SignedInfoType()
		{
		}
	}


	[XmlType(TypeName="CanonicalizationMethodType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CanonicalizationMethodType
	{

		[XmlAttribute(AttributeName="Algorithm",DataType="anyURI")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Algorithm;
		
		[XmlIgnore]
		public string Algorithm
		{ 
			get { return __Algorithm; }
			set { __Algorithm = value; }
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement[] Any;

		[XmlText(DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MixedValue;
		
		[XmlIgnore]
		public string MixedValue
		{ 
			get { return __MixedValue; }
			set { __MixedValue = value; }
		}

		public CanonicalizationMethodType()
		{
		}
	}


	[XmlType(TypeName="SignatureMethodType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SignatureMethodType
	{

		[XmlAttribute(AttributeName="Algorithm",DataType="anyURI")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Algorithm;
		
		[XmlIgnore]
		public string Algorithm
		{ 
			get { return __Algorithm; }
			set { __Algorithm = value; }
		}

		[XmlElement(ElementName="HMACOutputLength",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __HMACOutputLength;
		
		[XmlIgnore]
		public string HMACOutputLength
		{ 
			get { return __HMACOutputLength; }
			set { __HMACOutputLength = value; }
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement[] Any;

		[XmlText(DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MixedValue;
		
		[XmlIgnore]
		public string MixedValue
		{ 
			get { return __MixedValue; }
			set { __MixedValue = value; }
		}

		public SignatureMethodType()
		{
		}
	}


	[XmlType(TypeName="ReferenceType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ReferenceType
	{

		[XmlAttribute(AttributeName="Id",DataType="ID")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlAttribute(AttributeName="URI",DataType="anyURI")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __URI;
		
		[XmlIgnore]
		public string URI
		{ 
			get { return __URI; }
			set { __URI = value; }
		}

		[XmlAttribute(AttributeName="Type",DataType="anyURI")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Type;
		
		[XmlIgnore]
		public string Type
		{ 
			get { return __Type; }
			set { __Type = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.Transforms),ElementName="Transforms",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.Transforms __Transforms;
		
		[XmlIgnore]
		public Eumis.Rio.Transforms Transforms
		{
			get {return __Transforms;}
			set {__Transforms = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.DigestMethod),ElementName="DigestMethod",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.DigestMethod __DigestMethod;
		
		[XmlIgnore]
		public Eumis.Rio.DigestMethod DigestMethod
		{
			get {return __DigestMethod;}
			set {__DigestMethod = value;}
		}

		[XmlElement(ElementName="DigestValue",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __DigestValue;
		
		[XmlIgnore]
		public byte[] DigestValue
		{ 
			get { return __DigestValue; }
			set { __DigestValue = value; }
		}

		public ReferenceType()
		{
		}
	}


	[XmlType(TypeName="TransformsType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class TransformsType
	{


		[XmlElement(Type=typeof(Eumis.Rio.Transform),ElementName="Transform",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TransformCollection __TransformCollection;
		
		[XmlIgnore]
		public TransformCollection TransformCollection
		{
			get
			{
				if (__TransformCollection == null) __TransformCollection = new TransformCollection();
				return __TransformCollection;
			}
			set {__TransformCollection = value;}
		}

		public TransformsType()
		{
		}
	}


	[XmlType(TypeName="TransformType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class TransformType
	{

		[XmlAttribute(AttributeName="Algorithm",DataType="anyURI")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Algorithm;
		
		[XmlIgnore]
		public string Algorithm
		{ 
			get { return __Algorithm; }
			set { __Algorithm = value; }
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement Any;

		[XmlElement(Type=typeof(string),ElementName="XPath",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public XPathCollection __XPathCollection;
		
		[XmlIgnore]
		public XPathCollection XPathCollection
		{
			get
			{
				if (__XPathCollection == null) __XPathCollection = new XPathCollection();
				return __XPathCollection;
			}
			set {__XPathCollection = value;}
		}

		[XmlText(DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MixedValue;
		
		[XmlIgnore]
		public string MixedValue
		{ 
			get { return __MixedValue; }
			set { __MixedValue = value; }
		}

		public TransformType()
		{
		}
	}


	[XmlType(TypeName="DigestMethodType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class DigestMethodType
	{

		[XmlAttribute(AttributeName="Algorithm",DataType="anyURI")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Algorithm;
		
		[XmlIgnore]
		public string Algorithm
		{ 
			get { return __Algorithm; }
			set { __Algorithm = value; }
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement[] Any;

		[XmlText(DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MixedValue;
		
		[XmlIgnore]
		public string MixedValue
		{ 
			get { return __MixedValue; }
			set { __MixedValue = value; }
		}

		public DigestMethodType()
		{
		}
	}


	[XmlType(TypeName="KeyInfoType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class KeyInfoType
	{

		[XmlAttribute(AttributeName="Id",DataType="ID")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlElement(Type=typeof(string),ElementName="KeyName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public KeyNameCollection __KeyNameCollection;
		
		[XmlIgnore]
		public KeyNameCollection KeyNameCollection
		{
			get
			{
				if (__KeyNameCollection == null) __KeyNameCollection = new KeyNameCollection();
				return __KeyNameCollection;
			}
			set {__KeyNameCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.KeyValue),ElementName="KeyValue",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public KeyValueCollection __KeyValueCollection;
		
		[XmlIgnore]
		public KeyValueCollection KeyValueCollection
		{
			get
			{
				if (__KeyValueCollection == null) __KeyValueCollection = new KeyValueCollection();
				return __KeyValueCollection;
			}
			set {__KeyValueCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.RetrievalMethod),ElementName="RetrievalMethod",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public RetrievalMethodCollection __RetrievalMethodCollection;
		
		[XmlIgnore]
		public RetrievalMethodCollection RetrievalMethodCollection
		{
			get
			{
				if (__RetrievalMethodCollection == null) __RetrievalMethodCollection = new RetrievalMethodCollection();
				return __RetrievalMethodCollection;
			}
			set {__RetrievalMethodCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.X509Data),ElementName="X509Data",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public X509DataCollection __X509DataCollection;
		
		[XmlIgnore]
		public X509DataCollection X509DataCollection
		{
			get
			{
				if (__X509DataCollection == null) __X509DataCollection = new X509DataCollection();
				return __X509DataCollection;
			}
			set {__X509DataCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.PGPData),ElementName="PGPData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PGPDataCollection __PGPDataCollection;
		
		[XmlIgnore]
		public PGPDataCollection PGPDataCollection
		{
			get
			{
				if (__PGPDataCollection == null) __PGPDataCollection = new PGPDataCollection();
				return __PGPDataCollection;
			}
			set {__PGPDataCollection = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.SPKIData),ElementName="SPKIData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SPKIDataCollection __SPKIDataCollection;
		
		[XmlIgnore]
		public SPKIDataCollection SPKIDataCollection
		{
			get
			{
				if (__SPKIDataCollection == null) __SPKIDataCollection = new SPKIDataCollection();
				return __SPKIDataCollection;
			}
			set {__SPKIDataCollection = value;}
		}

		[XmlElement(Type=typeof(string),ElementName="MgmtData",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public MgmtDataCollection __MgmtDataCollection;
		
		[XmlIgnore]
		public MgmtDataCollection MgmtDataCollection
		{
			get
			{
				if (__MgmtDataCollection == null) __MgmtDataCollection = new MgmtDataCollection();
				return __MgmtDataCollection;
			}
			set {__MgmtDataCollection = value;}
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement Any;

		[XmlText(DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MixedValue;
		
		[XmlIgnore]
		public string MixedValue
		{ 
			get { return __MixedValue; }
			set { __MixedValue = value; }
		}

		public KeyInfoType()
		{
		}
	}


	[XmlType(TypeName="KeyValueType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class KeyValueType
	{

		[XmlElement(Type=typeof(Eumis.Rio.DSAKeyValue),ElementName="DSAKeyValue",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.DSAKeyValue __DSAKeyValue;
		
		[XmlIgnore]
		public Eumis.Rio.DSAKeyValue DSAKeyValue
		{
			get {return __DSAKeyValue;}
			set {__DSAKeyValue = value;}
		}

		[XmlElement(Type=typeof(Eumis.Rio.RSAKeyValue),ElementName="RSAKeyValue",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.RSAKeyValue __RSAKeyValue;
		
		[XmlIgnore]
		public Eumis.Rio.RSAKeyValue RSAKeyValue
		{
			get {return __RSAKeyValue;}
			set {__RSAKeyValue = value;}
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement Any;

		[XmlText(DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MixedValue;
		
		[XmlIgnore]
		public string MixedValue
		{ 
			get { return __MixedValue; }
			set { __MixedValue = value; }
		}

		public KeyValueType()
		{
		}
	}


	[XmlType(TypeName="RetrievalMethodType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class RetrievalMethodType
	{

		[XmlAttribute(AttributeName="URI",DataType="anyURI")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __URI;
		
		[XmlIgnore]
		public string URI
		{ 
			get { return __URI; }
			set { __URI = value; }
		}

		[XmlAttribute(AttributeName="Type",DataType="anyURI")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Type;
		
		[XmlIgnore]
		public string Type
		{ 
			get { return __Type; }
			set { __Type = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.Transforms),ElementName="Transforms",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.Transforms __Transforms;
		
		[XmlIgnore]
		public Eumis.Rio.Transforms Transforms
		{
			get {return __Transforms;}
			set {__Transforms = value;}
		}

		public RetrievalMethodType()
		{
		}
	}


	[XmlType(TypeName="X509DataType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class X509DataType
	{

		[XmlElement(Type=typeof(Eumis.Rio.X509IssuerSerialType),ElementName="X509IssuerSerial",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Eumis.Rio.X509IssuerSerialType __X509IssuerSerial;
		
		[XmlIgnore]
		public Eumis.Rio.X509IssuerSerialType X509IssuerSerial
		{
			get {return __X509IssuerSerial;}
			set {__X509IssuerSerial = value;}
		}

		[XmlElement(ElementName="X509SKI",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __X509SKI;
		
		[XmlIgnore]
		public byte[] X509SKI
		{ 
			get { return __X509SKI; }
			set { __X509SKI = value; }
		}

		[XmlElement(ElementName="X509SubjectName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __X509SubjectName;
		
		[XmlIgnore]
		public string X509SubjectName
		{ 
			get { return __X509SubjectName; }
			set { __X509SubjectName = value; }
		}

		[XmlElement(ElementName="X509Certificate",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __X509Certificate;
		
		[XmlIgnore]
		public byte[] X509Certificate
		{ 
			get { return __X509Certificate; }
			set { __X509Certificate = value; }
		}

		[XmlElement(ElementName="X509CRL",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __X509CRL;
		
		[XmlIgnore]
		public byte[] X509CRL
		{ 
			get { return __X509CRL; }
			set { __X509CRL = value; }
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement Any;

		public X509DataType()
		{
		}
	}


	[XmlType(TypeName="X509IssuerSerialType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class X509IssuerSerialType
	{

		[XmlElement(ElementName="X509IssuerName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __X509IssuerName;
		
		[XmlIgnore]
		public string X509IssuerName
		{ 
			get { return __X509IssuerName; }
			set { __X509IssuerName = value; }
		}

		[XmlElement(ElementName="X509SerialNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __X509SerialNumber;
		
		[XmlIgnore]
		public string X509SerialNumber
		{ 
			get { return __X509SerialNumber; }
			set { __X509SerialNumber = value; }
		}

		public X509IssuerSerialType()
		{
		}
	}


	[XmlType(TypeName="PGPDataType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PGPDataType
	{

		[XmlElement(ElementName="PGPKeyID",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __PGPKeyID;
		
		[XmlIgnore]
		public byte[] PGPKeyID
		{ 
			get { return __PGPKeyID; }
			set { __PGPKeyID = value; }
		}

		[XmlElement(ElementName="PGPKeyPacket",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __PGPKeyPacket;
		
		[XmlIgnore]
		public byte[] PGPKeyPacket
		{ 
			get { return __PGPKeyPacket; }
			set { __PGPKeyPacket = value; }
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement[] Any;

		public PGPDataType()
		{
		}
	}


	[XmlType(TypeName="SPKIDataType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SPKIDataType
	{

		[XmlElement(Type=typeof(byte[]),ElementName="SPKISexp",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SPKISexpCollection __SPKISexpCollection;
		
		[XmlIgnore]
		public SPKISexpCollection SPKISexpCollection
		{
			get
			{
				if (__SPKISexpCollection == null) __SPKISexpCollection = new SPKISexpCollection();
				return __SPKISexpCollection;
			}
			set {__SPKISexpCollection = value;}
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement Any;

		public SPKIDataType()
		{
		}
	}


	[XmlType(TypeName="ObjectType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ObjectType
	{

		[XmlAttribute(AttributeName="Id",DataType="ID")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlAttribute(AttributeName="MimeType",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MimeType;
		
		[XmlIgnore]
		public string MimeType
		{ 
			get { return __MimeType; }
			set { __MimeType = value; }
		}

		[XmlAttribute(AttributeName="Encoding",DataType="anyURI")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Encoding;
		
		[XmlIgnore]
		public string Encoding
		{ 
			get { return __Encoding; }
			set { __Encoding = value; }
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement Any;

		[XmlText(DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MixedValue;
		
		[XmlIgnore]
		public string MixedValue
		{ 
			get { return __MixedValue; }
			set { __MixedValue = value; }
		}

		public ObjectType()
		{
		}
	}


	[XmlType(TypeName="ManifestType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ManifestType
	{


		[XmlAttribute(AttributeName="Id",DataType="ID")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.Reference),ElementName="Reference",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ReferenceCollection __ReferenceCollection;
		
		[XmlIgnore]
		public ReferenceCollection ReferenceCollection
		{
			get
			{
				if (__ReferenceCollection == null) __ReferenceCollection = new ReferenceCollection();
				return __ReferenceCollection;
			}
			set {__ReferenceCollection = value;}
		}

		public ManifestType()
		{
		}
	}


	[XmlType(TypeName="SignaturePropertiesType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SignaturePropertiesType
	{


		[XmlAttribute(AttributeName="Id",DataType="ID")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlElement(Type=typeof(Eumis.Rio.SignatureProperty),ElementName="SignatureProperty",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SignaturePropertyCollection __SignaturePropertyCollection;
		
		[XmlIgnore]
		public SignaturePropertyCollection SignaturePropertyCollection
		{
			get
			{
				if (__SignaturePropertyCollection == null) __SignaturePropertyCollection = new SignaturePropertyCollection();
				return __SignaturePropertyCollection;
			}
			set {__SignaturePropertyCollection = value;}
		}

		public SignaturePropertiesType()
		{
		}
	}


	[XmlType(TypeName="SignaturePropertyType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SignaturePropertyType
	{

		[XmlAttribute(AttributeName="Target",DataType="anyURI")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Target;
		
		[XmlIgnore]
		public string Target
		{ 
			get { return __Target; }
			set { __Target = value; }
		}

		[XmlAttribute(AttributeName="Id",DataType="ID")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Id;
		
		[XmlIgnore]
		public string Id
		{ 
			get { return __Id; }
			set { __Id = value; }
		}

		[XmlAnyElement()]
		public System.Xml.XmlElement Any;

		[XmlText(DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MixedValue;
		
		[XmlIgnore]
		public string MixedValue
		{ 
			get { return __MixedValue; }
			set { __MixedValue = value; }
		}

		public SignaturePropertyType()
		{
		}
	}


	[XmlType(TypeName="DSAKeyValueType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class DSAKeyValueType
	{

		[XmlElement(ElementName="P",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __P;
		
		[XmlIgnore]
		public byte[] P
		{ 
			get { return __P; }
			set { __P = value; }
		}

		[XmlElement(ElementName="Q",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __Q;
		
		[XmlIgnore]
		public byte[] Q
		{ 
			get { return __Q; }
			set { __Q = value; }
		}

		[XmlElement(ElementName="G",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __G;
		
		[XmlIgnore]
		public byte[] G
		{ 
			get { return __G; }
			set { __G = value; }
		}

		[XmlElement(ElementName="Y",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __Y;
		
		[XmlIgnore]
		public byte[] Y
		{ 
			get { return __Y; }
			set { __Y = value; }
		}

		[XmlElement(ElementName="J",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __J;
		
		[XmlIgnore]
		public byte[] J
		{ 
			get { return __J; }
			set { __J = value; }
		}

		[XmlElement(ElementName="Seed",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __Seed;
		
		[XmlIgnore]
		public byte[] Seed
		{ 
			get { return __Seed; }
			set { __Seed = value; }
		}

		[XmlElement(ElementName="PgenCounter",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __PgenCounter;
		
		[XmlIgnore]
		public byte[] PgenCounter
		{ 
			get { return __PgenCounter; }
			set { __PgenCounter = value; }
		}

		public DSAKeyValueType()
		{
		}
	}


	[XmlType(TypeName="RSAKeyValueType",Namespace="http://www.w3.org/2000/09/xmldsig#"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class RSAKeyValueType
	{

		[XmlElement(ElementName="Modulus",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __Modulus;
		
		[XmlIgnore]
		public byte[] Modulus
		{ 
			get { return __Modulus; }
			set { __Modulus = value; }
		}

		[XmlElement(ElementName="Exponent",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="base64Binary",Namespace="http://www.w3.org/2000/09/xmldsig#")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte[] __Exponent;
		
		[XmlIgnore]
		public byte[] Exponent
		{ 
			get { return __Exponent; }
			set { __Exponent = value; }
		}

		public RSAKeyValueType()
		{
		}
	}


	[XmlRoot(ElementName="Signature",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class Signature : Eumis.Rio.SignatureType
	{

		public Signature() : base()
		{
		}
	}


	[XmlRoot(ElementName="SignatureValue",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class SignatureValue : Eumis.Rio.SignatureValueType
	{

		public SignatureValue() : base()
		{
		}
	}


	[XmlRoot(ElementName="SignedInfo",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class SignedInfo : Eumis.Rio.SignedInfoType
	{

		public SignedInfo() : base()
		{
		}
	}


	[XmlRoot(ElementName="CanonicalizationMethod",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class CanonicalizationMethod : Eumis.Rio.CanonicalizationMethodType
	{

		public CanonicalizationMethod() : base()
		{
		}
	}


	[XmlRoot(ElementName="SignatureMethod",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class SignatureMethod : Eumis.Rio.SignatureMethodType
	{

		public SignatureMethod() : base()
		{
		}
	}


	[XmlRoot(ElementName="Reference",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class Reference : Eumis.Rio.ReferenceType
	{

		public Reference() : base()
		{
		}
	}


	[XmlRoot(ElementName="Transforms",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class Transforms : Eumis.Rio.TransformsType
	{

		public Transforms() : base()
		{
		}
	}


	[XmlRoot(ElementName="Transform",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class Transform : Eumis.Rio.TransformType
	{

		public Transform() : base()
		{
		}
	}


	[XmlRoot(ElementName="DigestMethod",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class DigestMethod : Eumis.Rio.DigestMethodType
	{

		public DigestMethod() : base()
		{
		}
	}


	[XmlRoot(ElementName="KeyInfo",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class KeyInfo : Eumis.Rio.KeyInfoType
	{

		public KeyInfo() : base()
		{
		}
	}


	[XmlRoot(ElementName="KeyValue",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class KeyValue : Eumis.Rio.KeyValueType
	{

		public KeyValue() : base()
		{
		}
	}


	[XmlRoot(ElementName="RetrievalMethod",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class RetrievalMethod : Eumis.Rio.RetrievalMethodType
	{

		public RetrievalMethod() : base()
		{
		}
	}


	[XmlRoot(ElementName="X509Data",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class X509Data : Eumis.Rio.X509DataType
	{

		public X509Data() : base()
		{
		}
	}


	[XmlRoot(ElementName="PGPData",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class PGPData : Eumis.Rio.PGPDataType
	{

		public PGPData() : base()
		{
		}
	}


	[XmlRoot(ElementName="SPKIData",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class SPKIData : Eumis.Rio.SPKIDataType
	{

		public SPKIData() : base()
		{
		}
	}


	[XmlRoot(ElementName="Object",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class @Object : Eumis.Rio.ObjectType
	{

		public @Object() : base()
		{
		}
	}


	[XmlRoot(ElementName="Manifest",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class Manifest : Eumis.Rio.ManifestType
	{

		public Manifest() : base()
		{
		}
	}


	[XmlRoot(ElementName="SignatureProperties",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class SignatureProperties : Eumis.Rio.SignaturePropertiesType
	{

		public SignatureProperties() : base()
		{
		}
	}


	[XmlRoot(ElementName="SignatureProperty",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class SignatureProperty : Eumis.Rio.SignaturePropertyType
	{

		public SignatureProperty() : base()
		{
		}
	}


	[XmlRoot(ElementName="DSAKeyValue",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class DSAKeyValue : Eumis.Rio.DSAKeyValueType
	{

		public DSAKeyValue() : base()
		{
		}
	}


	[XmlRoot(ElementName="RSAKeyValue",Namespace="http://www.w3.org/2000/09/xmldsig#",IsNullable=false),Serializable]
	public partial class RSAKeyValue : Eumis.Rio.RSAKeyValueType
	{

		public RSAKeyValue() : base()
		{
		}
	}
}
