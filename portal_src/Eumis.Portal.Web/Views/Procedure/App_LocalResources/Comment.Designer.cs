//------------------------------------------------------------------------------
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
    public class Comment {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Comment() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Eumis.Portal.Web.Views.Procedure.App_LocalResources.Comment", typeof(Comment).Assembly);
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
        ///   Looks up a localized string similar to Код за сигурност.
        /// </summary>
        public static string Captcha {
            get {
                return ResourceManager.GetString("Captcha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kоментар/предложение.
        /// </summary>
        public static string CommentPageTitle {
            get {
                return ResourceManager.GetString("CommentPageTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Вашият коментар е приет и ще бъде обработен..
        /// </summary>
        public static string CommentSuccessNotification {
            get {
                return ResourceManager.GetString("CommentSuccessNotification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Описание (до 4000 символа).
        /// </summary>
        public static string DescriptionLength {
            get {
                return ResourceManager.GetString("DescriptionLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невалидна електронна поща..
        /// </summary>
        public static string EmailRegularExpression {
            get {
                return ResourceManager.GetString("EmailRegularExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email.
        /// </summary>
        public static string Mail {
            get {
                return ResourceManager.GetString("Mail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Текстът в полето „Описание“ не може да съдържа повече от 4000 символа..
        /// </summary>
        public static string MessageLength {
            get {
                return ResourceManager.GetString("MessageLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Текстът в полето „Описание“ е задължителен..
        /// </summary>
        public static string MessageRequired {
            get {
                return ResourceManager.GetString("MessageRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Име и фамилия.
        /// </summary>
        public static string Name {
            get {
                return ResourceManager.GetString("Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изпрати.
        /// </summary>
        public static string SubmitButton {
            get {
                return ResourceManager.GetString("SubmitButton", resourceCulture);
            }
        }
    }
}
