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
    public class PaymentRequest {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PaymentRequest() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Eumis.Portal.Web.Views.Shared.App_LocalResources.PaymentRequest", typeof(PaymentRequest).Assembly);
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
        ///   Looks up a localized string similar to Прикачени документи.
        /// </summary>
        public static string AttachedDocuments {
            get {
                return ResourceManager.GetString("AttachedDocuments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Общи данни.
        /// </summary>
        public static string BasicData {
            get {
                return ResourceManager.GetString("BasicData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Декларация.
        /// </summary>
        public static string Declaration {
            get {
                return ResourceManager.GetString("Declaration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изтегли файл.
        /// </summary>
        public static string DownloadFile {
            get {
                return ResourceManager.GetString("DownloadFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Текущото искане за плащане е от тип &quot;{0}&quot;,  а зареждания файл е с тип &quot;{1}&quot;..
        /// </summary>
        public static string PaymentTypeMissmatch {
            get {
                return ResourceManager.GetString("PaymentTypeMissmatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Зареди файл.
        /// </summary>
        public static string UploadFile {
            get {
                return ResourceManager.GetString("UploadFile", resourceCulture);
            }
        }
    }
}