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
    public class BFPContractContract {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BFPContractContract() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Eumis.Portal.Web.Views.Shared.App_LocalResources.BFPContractContract", typeof(BFPContractContract).Assembly);
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
        ///   Looks up a localized string similar to Очаквани приходи от проекта/продукта.
        /// </summary>
        public static string ExpectedRevenue {
            get {
                return ResourceManager.GetString("ExpectedRevenue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Недопустими разходи, необходими за изпълнението на проекта/продукта (когато е приложимо).
        /// </summary>
        public static string IneligibleCosts {
            get {
                return ResourceManager.GetString("IneligibleCosts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Съотношение Безвъзмездна финансова помощ/Финансов инструмент към Общо допустими разходи.
        /// </summary>
        public static string RatioRequestedFundingTotalEligibleCosts {
            get {
                return ResourceManager.GetString("RatioRequestedFundingTotalEligibleCosts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Безвъзмездна финансова помощ/Финансов инструмент.
        /// </summary>
        public static string RequestedFundingAmount {
            get {
                return ResourceManager.GetString("RequestedFundingAmount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Обща стойност на проекта/продукта.
        /// </summary>
        public static string TotalProjectCost {
            get {
                return ResourceManager.GetString("TotalProjectCost", resourceCulture);
            }
        }
    }
}
