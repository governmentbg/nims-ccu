//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eumis.ApplicationServices {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ApplicationServicesTexts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApplicationServicesTexts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Eumis.ApplicationServices.ApplicationServicesTexts", typeof(ApplicationServicesTexts).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невалиден формат на файла..
        /// </summary>
        internal static string Common_InvalidFileFormat {
            get {
                return ResourceManager.GetString("Common_InvalidFileFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Избраният файл не отговаря на шаблона..
        /// </summary>
        internal static string Common_Parsers_FileNotMatchingTemplateError {
            get {
                return ResourceManager.GetString("Common_Parsers_FileNotMatchingTemplateError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Избраният файл не отговаря на шаблона..
        /// </summary>
        internal static string EvalSessionProjectParser_FileNotMatchingTemplateError {
            get {
                return ResourceManager.GetString("EvalSessionProjectParser_FileNotMatchingTemplateError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to извършена проверка за административно съответствие и допустимост.
        /// </summary>
        internal static string EvalSessionPublishedHandler_ResultType_ASD {
            get {
                return ResourceManager.GetString("EvalSessionPublishedHandler_ResultType_ASD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to предварително класиране.
        /// </summary>
        internal static string EvalSessionPublishedHandler_ResultType_Preliminary {
            get {
                return ResourceManager.GetString("EvalSessionPublishedHandler_ResultType_Preliminary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to класиране.
        /// </summary>
        internal static string EvalSessionPublishedHandler_ResultType_Standing {
            get {
                return ResourceManager.GetString("EvalSessionPublishedHandler_ResultType_Standing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Липсват данни за партньор/и в ПП с номер {0}. Задължителните полета са &apos;УИН(Булстат/ЕИК + Номер)&apos;, &apos;Пълно наименование&apos;, &apos;Тип организация&apos;, &apos;Вид организация&apos;, &apos;Имена на лицето, представляващо организацията&apos; и следните от &apos;Адрес за кореспонденция&apos; -&gt; &apos;Държава&apos;, &apos;Населено място&apos;, &apos;Пощенски код&apos;, &apos;Улица (ж.к., кв., №, бл., вх., ет., ап.)&apos; или &apos;Адрес&apos;(когато държавата е различна от България).
        /// </summary>
        internal static string EvalSessionReportService_CanCreate_MissingPartnerData {
            get {
                return ResourceManager.GetString("EvalSessionReportService_CanCreate_MissingPartnerData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде нов доклад, когато оценителната сесията не е активна..
        /// </summary>
        internal static string EvalSessionReportService_CanCreate_SessionInactive {
            get {
                return ResourceManager.GetString("EvalSessionReportService_CanCreate_SessionInactive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не можете да анулирате доклада..
        /// </summary>
        internal static string EvalSessionReportService_CanDelete_CannotDeleteReport {
            get {
                return ResourceManager.GetString("EvalSessionReportService_CanDelete_CannotDeleteReport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде автоматична версия на ПП, когато оценителната сесията не е активна.
        /// </summary>
        internal static string EvalSessionService_CanCreate_SessionActive {
            get {
                return ResourceManager.GetString("EvalSessionService_CanCreate_SessionActive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде нова версия на ПП с номер: {0}, когато съществува активна кореспонденция с кандидата..
        /// </summary>
        internal static string EvalSessionService_CanCreateAutomaticProjectVersion_CommunicationInProgress {
            get {
                return ResourceManager.GetString("EvalSessionService_CanCreateAutomaticProjectVersion_CommunicationInProgress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Вече съществува версия към ПП с номер: {0}, която е със статус чернова..
        /// </summary>
        internal static string EvalSessionService_CanCreateAutomaticProjectVersion_DuplicatedDraftStatus {
            get {
                return ResourceManager.GetString("EvalSessionService_CanCreateAutomaticProjectVersion_DuplicatedDraftStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Липсва автоматично генерирана първа версия на ПП с номер: {0}..
        /// </summary>
        internal static string EvalSessionService_CanCreateAutomaticProjectVersion_MissingLastVersionStatus {
            get {
                return ResourceManager.GetString("EvalSessionService_CanCreateAutomaticProjectVersion_MissingLastVersionStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Проектно предложение с номер: {0} не е включено в тази оценителна сесия..
        /// </summary>
        internal static string EvalSessionService_CanCreateAutomaticProjectVersion_NotIncluded {
            get {
                return ResourceManager.GetString("EvalSessionService_CanCreateAutomaticProjectVersion_NotIncluded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Проектно предложение с номер: {0} е анулирано от сесията.
        /// </summary>
        internal static string EvalSessionService_CanCreateAutomaticProjectVersion_ProjectIsCanceled {
            get {
                return ResourceManager.GetString("EvalSessionService_CanCreateAutomaticProjectVersion_ProjectIsCanceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Проектно предложение с номер: {0} е оттеглено..
        /// </summary>
        internal static string EvalSessionService_CanCreateAutomaticProjectVersion_ProjectIsWithdrawn {
            get {
                return ResourceManager.GetString("EvalSessionService_CanCreateAutomaticProjectVersion_ProjectIsWithdrawn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не беше намерено проектно предложение с номер: {0}..
        /// </summary>
        internal static string EvalSessionService_CanCreateAutomaticProjectVersion_RegNumberNotValid {
            get {
                return ResourceManager.GetString("EvalSessionService_CanCreateAutomaticProjectVersion_RegNumberNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Автоматично създадена от {0} на {1}..
        /// </summary>
        internal static string EvalSessionService_CreateAutomaticProjectVersion_Note {
            get {
                return ResourceManager.GetString("EvalSessionService_CreateAutomaticProjectVersion_Note", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Проектно предложение с номер: {0} не е включено в тази оценителна сесия..
        /// </summary>
        internal static string EvalSessionService_ParseProjectsExcelFile_NotIncluded {
            get {
                return ResourceManager.GetString("EvalSessionService_ParseProjectsExcelFile_NotIncluded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не беше намерено проектно предложение с номер: {0}..
        /// </summary>
        internal static string EvalSessionService_ParseProjectsExcelFile_ProjectNotFound {
            get {
                return ResourceManager.GetString("EvalSessionService_ParseProjectsExcelFile_ProjectNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to За следните проектни предложения бяха изпратени успешно заявки към Мониторстат:{0}{1}.
        /// </summary>
        internal static string MonitorstatService_AutomaticProjectMonitorstatRequest_MonitorstatRequestsSent {
            get {
                return ResourceManager.GetString("MonitorstatService_AutomaticProjectMonitorstatRequest_MonitorstatRequestsSent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Проектно предложение с номер: {0}, не съдържа приета декларация: {1}..
        /// </summary>
        internal static string MonitorstatService_AutomaticProjectMonitorstatRequest_ProjectDoesNotContainDeclaration {
            get {
                return ResourceManager.GetString("MonitorstatService_AutomaticProjectMonitorstatRequest_ProjectDoesNotContainDeclar" +
                        "ation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Проектно предложение с номер: {0}, не съдържа документ: {1}..
        /// </summary>
        internal static string MonitorstatService_AutomaticProjectMonitorstatRequest_ProjectDoesNotContainDocument {
            get {
                return ResourceManager.GetString("MonitorstatService_AutomaticProjectMonitorstatRequest_ProjectDoesNotContainDocume" +
                        "nt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не съществува активна версия на ПП с номер: {0}.
        /// </summary>
        internal static string MonitorstatService_AutomaticProjectMonitorstatRequest_ProjectHasLastNonActualVersion {
            get {
                return ResourceManager.GetString("MonitorstatService_AutomaticProjectMonitorstatRequest_ProjectHasLastNonActualVers" +
                        "ion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Възникна непредвидена грешка при изпращане на заявка към Мониторстат за ПП с номер: {0}..
        /// </summary>
        internal static string MonitorstatService_AutomaticProjectMonitorstatRequest_UnexpectedError {
            get {
                return ResourceManager.GetString("MonitorstatService_AutomaticProjectMonitorstatRequest_UnexpectedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде новa комуникация с кандидата, който не е асоцииран с профил в системата..
        /// </summary>
        internal static string ProjectCommunicationService_CanCreate_DoesNotHaveAssociatedRegistration {
            get {
                return ResourceManager.GetString("ProjectCommunicationService_CanCreate_DoesNotHaveAssociatedRegistration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде новa комуникация с кандидата, когато оценителната сесията не е активна..
        /// </summary>
        internal static string ProjectCommunicationService_CanCreate_EvalSessionNotActive {
            get {
                return ResourceManager.GetString("ProjectCommunicationService_CanCreate_EvalSessionNotActive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде новa комуникация с кандидата, когато вече съществува активна кореспонденция..
        /// </summary>
        internal static string ProjectCommunicationService_CanCreate_HasCommunicationInProgress {
            get {
                return ResourceManager.GetString("ProjectCommunicationService_CanCreate_HasCommunicationInProgress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде новa комуникация с кандидата, когато съществува версия към проектното предложение със статус чернова..
        /// </summary>
        internal static string ProjectCommunicationService_CanCreate_ProjectHasDraftVersion {
            get {
                return ResourceManager.GetString("ProjectCommunicationService_CanCreate_ProjectHasDraftVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде новa комуникация с кандидата, когато не съществува активна версия на ПП..
        /// </summary>
        internal static string ProjectCommunicationService_CanCreate_ProjectHasLastNonActualVersion {
            get {
                return ResourceManager.GetString("ProjectCommunicationService_CanCreate_ProjectHasLastNonActualVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не съществува отговор с този код..
        /// </summary>
        internal static string ProjectCommunicationService_CanRegisterEvalSessionProjectAnswer_NoAnswerWithThisCode {
            get {
                return ResourceManager.GetString("ProjectCommunicationService_CanRegisterEvalSessionProjectAnswer_NoAnswerWithThisC" +
                        "ode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Времето за отговор е надхвърлено..
        /// </summary>
        internal static string ProjectCommunicationService_CanRegisterEvalSessionProjectAnswer_QuestionTimedOut {
            get {
                return ResourceManager.GetString("ProjectCommunicationService_CanRegisterEvalSessionProjectAnswer_QuestionTimedOut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се регистрира отговор на въпрос в неактивна сесия..
        /// </summary>
        internal static string ProjectCommunicationService_CanRegisterEvalSessionProjectAnswer_SessionInactive {
            get {
                return ResourceManager.GetString("ProjectCommunicationService_CanRegisterEvalSessionProjectAnswer_SessionInactive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде новa комуникация с кандидата, който не е асоцииран с профил в системата..
        /// </summary>
        internal static string ProjectManagingAuthorityCommunicationService_CanCreate_DoesNotHaveAssociatedRegistration {
            get {
                return ResourceManager.GetString("ProjectManagingAuthorityCommunicationService_CanCreate_DoesNotHaveAssociatedRegis" +
                        "tration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не беше намерено проектно предложение с номер: {0}..
        /// </summary>
        internal static string ProjectManagingAuthorityCommunicationService_ParseRecipientsExcelFile_ProjectNotFound {
            get {
                return ResourceManager.GetString("ProjectManagingAuthorityCommunicationService_ParseRecipientsExcelFile_ProjectNotF" +
                        "ound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Автоматично създадено вследствие на регистрация..
        /// </summary>
        internal static string ProjectRegistrationService_CreatedByRegistration {
            get {
                return ResourceManager.GetString("ProjectRegistrationService_CreatedByRegistration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде нова версия на ПП, когато съществува активна кореспонденция с кандидата..
        /// </summary>
        internal static string ProjectVersionXmlService_CanCreate_CommunicationInProgress {
            get {
                return ResourceManager.GetString("ProjectVersionXmlService_CanCreate_CommunicationInProgress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Вече съществува версия към проектното предложение, която е със статус чернова..
        /// </summary>
        internal static string ProjectVersionXmlService_CanCreate_DuplicatedDraftStatus {
            get {
                return ResourceManager.GetString("ProjectVersionXmlService_CanCreate_DuplicatedDraftStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Първата версия към проектното предложение трябва да е автоматично генерирана от регистрационните данни..
        /// </summary>
        internal static string ProjectVersionXmlService_CanCreate_MissingLastVersionStatus {
            get {
                return ResourceManager.GetString("ProjectVersionXmlService_CanCreate_MissingLastVersionStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не може да се създаде нова версия на ПП, когато оценителната сесията не е активна..
        /// </summary>
        internal static string ProjectVersionXmlService_CanCreate_SessionActive {
            get {
                return ResourceManager.GetString("ProjectVersionXmlService_CanCreate_SessionActive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Автоматично създадена служебна версия..
        /// </summary>
        internal static string ProjectVersionXmlService_CreateProjectServiceVersion {
            get {
                return ResourceManager.GetString("ProjectVersionXmlService_CreateProjectServiceVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Автоматично създадена първоначална версия..
        /// </summary>
        internal static string ProjectVersionXmlService_CreateProjectVersion {
            get {
                return ResourceManager.GetString("ProjectVersionXmlService_CreateProjectVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Автоматично създадена от комуникация с рег. номер.
        /// </summary>
        internal static string ProjectVersionXmlService_CreateProjectVersionFromCommunication {
            get {
                return ResourceManager.GetString("ProjectVersionXmlService_CreateProjectVersionFromCommunication", resourceCulture);
            }
        }
    }
}
