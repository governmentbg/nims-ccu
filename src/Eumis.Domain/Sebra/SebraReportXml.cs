//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.6.1590.0.
// 

namespace Eumis.Domain.Sebra
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class f
    {

        private h hField;

        private a[] aField;

        /// <remarks/>
        public h h
        {
            get
            {
                return this.hField;
            }
            set
            {
                this.hField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("a")]
        public a[] a
        {
            get
            {
                return this.aField;
            }
            set
            {
                this.aField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class h
    {

        private string refidField;

        private System.DateTime timestampField;

        private string senderField;

        private string sendernameField;

        private hReceiver receiverField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string refid
        {
            get
            {
                return this.refidField;
            }
            set
            {
                this.refidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime timestamp
        {
            get
            {
                return this.timestampField;
            }
            set
            {
                this.timestampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string sender
        {
            get
            {
                return this.senderField;
            }
            set
            {
                this.senderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string sendername
        {
            get
            {
                return this.sendernameField;
            }
            set
            {
                this.sendernameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public hReceiver receiver
        {
            get
            {
                return this.receiverField;
            }
            set
            {
                this.receiverField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum hReceiver
    {

        /// <remarks/>
        BNBOnline,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class a
    {

        private d[] dField;

        private string accField;

        private string bicField;

        private string curField;

        private decimal doField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("d")]
        public d[] d
        {
            get
            {
                return this.dField;
            }
            set
            {
                this.dField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string acc
        {
            get
            {
                return this.accField;
            }
            set
            {
                this.accField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string bic
        {
            get
            {
                return this.bicField;
            }
            set
            {
                this.bicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cur
        {
            get
            {
                return this.curField;
            }
            set
            {
                this.curField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal @do
        {
            get
            {
                return this.doField;
            }
            set
            {
                this.doField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class d
    {

        private bud budField;

        private dDoc docField;

        private string nokField;

        private string ipolField;

        private string ibanField;

        private string bicField;

        private string vppField;

        private string curField;

        private decimal suField;

        private string o1Field;

        private string o2Field;

        private dSys sysField;

        private bool sysFieldSpecified;

        private string taxField;

        private string kdField;

        private System.DateTime dexField;

        private bool dexFieldSpecified;

        private string vpnField;

        /// <remarks/>
        public bud bud
        {
            get
            {
                return this.budField;
            }
            set
            {
                this.budField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public dDoc doc
        {
            get
            {
                return this.docField;
            }
            set
            {
                this.docField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nok
        {
            get
            {
                return this.nokField;
            }
            set
            {
                this.nokField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ipol
        {
            get
            {
                return this.ipolField;
            }
            set
            {
                this.ipolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string iban
        {
            get
            {
                return this.ibanField;
            }
            set
            {
                this.ibanField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string bic
        {
            get
            {
                return this.bicField;
            }
            set
            {
                this.bicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string vpp
        {
            get
            {
                return this.vppField;
            }
            set
            {
                this.vppField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cur
        {
            get
            {
                return this.curField;
            }
            set
            {
                this.curField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal su
        {
            get
            {
                return this.suField;
            }
            set
            {
                this.suField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string o1
        {
            get
            {
                return this.o1Field;
            }
            set
            {
                this.o1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string o2
        {
            get
            {
                return this.o2Field;
            }
            set
            {
                this.o2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public dSys sys
        {
            get
            {
                return this.sysField;
            }
            set
            {
                this.sysField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sysSpecified
        {
            get
            {
                return this.sysFieldSpecified;
            }
            set
            {
                this.sysFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tax
        {
            get
            {
                return this.taxField;
            }
            set
            {
                this.taxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string kd
        {
            get
            {
                return this.kdField;
            }
            set
            {
                this.kdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime dex
        {
            get
            {
                return this.dexField;
            }
            set
            {
                this.dexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dexSpecified
        {
            get
            {
                return this.dexFieldSpecified;
            }
            set
            {
                this.dexFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string vpn
        {
            get
            {
                return this.vpnField;
            }
            set
            {
                this.vpnField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class bud
    {

        private string vdField;

        private string ndField;

        private System.DateTime ddField;

        private bool ddFieldSpecified;

        private System.DateTime dbField;

        private bool dbFieldSpecified;

        private System.DateTime deField;

        private bool deFieldSpecified;

        private string izlField;

        private string bulField;

        private string egnField;

        private string lncField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string vd
        {
            get
            {
                return this.vdField;
            }
            set
            {
                this.vdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nd
        {
            get
            {
                return this.ndField;
            }
            set
            {
                this.ndField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime dd
        {
            get
            {
                return this.ddField;
            }
            set
            {
                this.ddField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ddSpecified
        {
            get
            {
                return this.ddFieldSpecified;
            }
            set
            {
                this.ddFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime db
        {
            get
            {
                return this.dbField;
            }
            set
            {
                this.dbField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dbSpecified
        {
            get
            {
                return this.dbFieldSpecified;
            }
            set
            {
                this.dbFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime de
        {
            get
            {
                return this.deField;
            }
            set
            {
                this.deField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deSpecified
        {
            get
            {
                return this.deFieldSpecified;
            }
            set
            {
                this.deFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string izl
        {
            get
            {
                return this.izlField;
            }
            set
            {
                this.izlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string bul
        {
            get
            {
                return this.bulField;
            }
            set
            {
                this.bulField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string egn
        {
            get
            {
                return this.egnField;
            }
            set
            {
                this.egnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string lnc
        {
            get
            {
                return this.lncField;
            }
            set
            {
                this.lncField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum dDoc
    {

        /// <remarks/>
        ПНКП,

        /// <remarks/>
        ПНВБ,

        /// <remarks/>
        БПН,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum dSys
    {

        /// <remarks/>
        РИНГС,

        /// <remarks/>
        БИСЕРА,
    }
}
