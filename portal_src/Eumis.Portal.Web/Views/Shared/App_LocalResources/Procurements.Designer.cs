﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eumis.Portal.Web.Views.Shared.App_LocalResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Procurements {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Procurements() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Eumis.Portal.Web.Views.Shared.App_LocalResources.Procurements", typeof(Procurements).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Договори с изпълнители.
        /// </summary>
        public static string ContractContractors {
            get {
                return ResourceManager.GetString("ContractContractors", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to В тази секция се въвежда единствено информация за действително сключените договори с външен за организацията изпълнител.
        /// </summary>
        public static string ContractContractorsLabel {
            get {
                return ResourceManager.GetString("ContractContractorsLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Юридически/Физически лица.
        /// </summary>
        public static string Contractors {
            get {
                return ResourceManager.GetString("Contractors", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Процедури за избор на изпълнител и сключени договори.
        /// </summary>
        public static string ProcurementPlans {
            get {
                return ResourceManager.GetString("ProcurementPlans", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to В тази секция се въвежда информация за проведените процедури за избор на изпълнител, като прикачените документи следва да бъдат максимално окрупнени (.zip, .rar и др.). Допустимият размер на файл е до 2 GB.
        /// </summary>
        public static string ProcurementPlansLabel {
            get {
                return ResourceManager.GetString("ProcurementPlansLabel", resourceCulture);
            }
        }
    }
}
