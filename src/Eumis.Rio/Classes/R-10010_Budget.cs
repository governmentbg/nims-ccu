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


	using ProgrammeBudgetCollection = System.Collections.Generic.List<Eumis.Rio.ProgrammeBudget>;



	[XmlType(TypeName="Budget",Namespace="http://ereg.egov.bg/segment/R-10010"),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Budget
	{


		[XmlElement(Type=typeof(Eumis.Rio.ProgrammeBudget),ElementName="ProgrammeBudget",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace="http://ereg.egov.bg/segment/R-10010")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProgrammeBudgetCollection __ProgrammeBudgetCollection;
		
		[XmlIgnore]
		public ProgrammeBudgetCollection ProgrammeBudgetCollection
		{
			get
			{
				if (__ProgrammeBudgetCollection == null) __ProgrammeBudgetCollection = new ProgrammeBudgetCollection();
				return __ProgrammeBudgetCollection;
			}
			set {__ProgrammeBudgetCollection = value;}
		}

		public Budget()
		{
		}
	}
}
