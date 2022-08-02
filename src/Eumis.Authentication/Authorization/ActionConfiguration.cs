using Eumis.Authentication.Authorization.ClaimsContexts.ActuallyPaidAmount;
using Eumis.Authentication.Authorization.ClaimsContexts.AnnualAccountReport;
using Eumis.Authentication.Authorization.ClaimsContexts.Audit;
using Eumis.Authentication.Authorization.ClaimsContexts.AuditAuthorityCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.CertAuthorityCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.CertReport;
using Eumis.Authentication.Authorization.ClaimsContexts.CertReportCheck;
using Eumis.Authentication.Authorization.ClaimsContexts.CompensationDocument;
using Eumis.Authentication.Authorization.ClaimsContexts.Contract;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractDebt;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractOffer;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractProcurement;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReport;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertAuthorityCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertAuthorityFinancialCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCheck;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialCertCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialRevalidation;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidation;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidationCACorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidationCAFinancialCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportTechnicalCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractSpendingPlan;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractVersion;
using Eumis.Authentication.Authorization.ClaimsContexts.CorrectionDebt;
using Eumis.Authentication.Authorization.ClaimsContexts.EuReimbursedAmount;
using Eumis.Authentication.Authorization.ClaimsContexts.EvalSession;
using Eumis.Authentication.Authorization.ClaimsContexts.EvalSessionSheet;
using Eumis.Authentication.Authorization.ClaimsContexts.EvalSessionStandpoint;
using Eumis.Authentication.Authorization.ClaimsContexts.FinancialCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.FIReimbursedAmount;
using Eumis.Authentication.Authorization.ClaimsContexts.Irregularity;
using Eumis.Authentication.Authorization.ClaimsContexts.IrregularitySignal;
using Eumis.Authentication.Authorization.ClaimsContexts.MapNodeIndicator;
using Eumis.Authentication.Authorization.ClaimsContexts.Procedure;
using Eumis.Authentication.Authorization.ClaimsContexts.ProcedureMassCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.Prognosis;
using Eumis.Authentication.Authorization.ClaimsContexts.Programme;
using Eumis.Authentication.Authorization.ClaimsContexts.Project;
using Eumis.Authentication.Authorization.ClaimsContexts.ProjectCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.ProjectManagingAuthorityCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.ProjectMassManagingAuthorityCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.ProjectVersion;
using Eumis.Authentication.Authorization.ClaimsContexts.ReimbursedAmount;
using Eumis.Authentication.Authorization.ClaimsContexts.RequestPackage;
using Eumis.Authentication.Authorization.ClaimsContexts.SpotCheck;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Authentication.Authorization.ClaimsContexts.UserOrganization;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.Users.CommonPermissions;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Eumis.Authentication.Authorization
{
    internal static class ActionConfiguration
    {
        static ActionConfiguration()
        {
            var actionsMap = new Dictionary<Enum, Func<IUserClaimsContextInternal, bool>>();
            var objectActionsMap = new Dictionary<Enum, Func<Dictionary<Type, object>, IUserClaimsContextInternal, int, bool>>();

            ///////////////////////////////////////////////////////////////////
            // # Модул новини
            ///////////////////////////////////////////////////////////////////
            // ### Търсене в списък с новини => "Новини/Публикуване"
            actionsMap.Map(NewsListActions.Search, (cu) => cu.HasCommonPermission(NewsPermissions.CanPublish));

            // ### Търсене в списък с новини => "Новини/Публикуване"
            actionsMap.Map(NewsListActions.Create, (cu) => cu.HasCommonPermission(NewsPermissions.CanPublish));

            // ### Преглед на новина => "Новини/Публикуване"
            objectActionsMap.Map(NewsActions.View, (cu, nId) => cu.HasCommonPermission(NewsPermissions.CanPublish));

            // ### Редакция на новина => "Новини/Публикуване"
            objectActionsMap.Map(NewsActions.Edit, (cu, nId) => cu.HasCommonPermission(NewsPermissions.CanPublish));

            ///////////////////////////////////////////////////////////////////
            // # Модул ръководства
            ///////////////////////////////////////////////////////////////////
            // ### Търсене в списък с ръководства => "Ръководства/Създаване"
            actionsMap.Map(GuidanceListActions.Search, (cu) => cu.HasCommonPermission(GuidancePermissions.CanCreate));

            // ### Търсене в списък с ръководства => "Ръководства/Създаване"
            actionsMap.Map(GuidanceListActions.Create, (cu) => cu.HasCommonPermission(GuidancePermissions.CanCreate));

            // ### Преглед на ръководствo => "Ръководства/Създаване"
            objectActionsMap.Map(GuidanceActions.View, (cu, nId) => cu.HasCommonPermission(GuidancePermissions.CanCreate));

            // ### Редакция на ръководств => "Ръководства/Създаване"
            objectActionsMap.Map(GuidanceActions.Edit, (cu, nId) => cu.HasCommonPermission(GuidancePermissions.CanCreate));

            ///////////////////////////////////////////////////////////////////
            // # Модул потребители
            ///////////////////////////////////////////////////////////////////

            // ## Организации

            // ### Търсене в списък с организации => "Потребители/Администриране + АдминистраторЦКЗ"
            actionsMap.Map(UserOrganizationListActions.Search, (cu) => cu.IsSuperUser);

            // ### Създаване на организация => "Потребители/Администриране + АдминистраторЦКЗ"
            actionsMap.Map(UserOrganizationListActions.Create, (cu) => cu.IsSuperUser);

            // ### Импорт на данни от Мониторстат => "Връзка с външни информационни системи/АдминистраторЦКЗ"
            actionsMap.Map(MonitorstatListActions.Import, (cu) => cu.IsSuperUser);

            // ### Създаване на потребител => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Създаване на потребител  от организацията на потребителя => "Потребители/Администриране"
            objectActionsMap.Map<IUserOrganizationClaimsContext>(UserOrganizationActions.CreateUser, (cu, uo) => cu.IsSuperUser || (cu.UserOrganizationId == uo.UserOrganizationId && cu.HasCommonPermission(UserAdminPermissions.CanAdministrate)));

            // ### Създаване на пакет за актуализация => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Създаване на пакет за актуализация от организацията на потребителя => "Потребители/Администриране"
            objectActionsMap.Map<IUserOrganizationClaimsContext>(UserOrganizationActions.CreateRequestPackage, (cu, uo) => cu.IsSuperUser || (cu.UserOrganizationId == uo.UserOrganizationId && cu.HasCommonPermission(UserAdminPermissions.CanAdministrate)));

            // ### Преглед на организация => "Потребители/Администриране + АдминистраторЦКЗ"
            objectActionsMap.Map<IUserOrganizationClaimsContext>(UserOrganizationActions.View, (cu, uo) => cu.IsSuperUser);

            // ### Редактиране на организация => "Потребители/Администриране + АдминистраторЦКЗ"
            objectActionsMap.Map<IUserOrganizationClaimsContext>(UserOrganizationActions.Edit, (cu, uo) => cu.IsSuperUser);

            // ### Изтриване на организация => "Потребители/Администриране + АдминистраторЦКЗ"
            objectActionsMap.Map<IUserOrganizationClaimsContext>(UserOrganizationActions.Delete, (cu, uo) => cu.IsSuperUser);

            // ## Шаблони за групи

            // ### Търсене в списък шаблони за групи => "Потребители/Администриране + АдминистраторЦКЗ"
            actionsMap.Map(PermissionTemplateListActions.Search, (cu) => cu.IsSuperUser);

            // ### Създаване на шаблон за група => "Потребители/Администриране + АдминистраторЦКЗ"
            actionsMap.Map(PermissionTemplateListActions.Create, (cu) => cu.IsSuperUser);

            // ### Преглед на шаблон за група => "Потребители/Администриране + АдминистраторЦКЗ"
            objectActionsMap.Map(PermissionTemplateActions.View, (cu, ptId) => cu.IsSuperUser);

            // ### Редакция на шаблон за група => "Потребители/Администриране + АдминистраторЦКЗ"
            objectActionsMap.Map(PermissionTemplateActions.Edit, (cu, ptId) => cu.IsSuperUser);

            // ## Групи потребители

            // ### Търсене в списък групи потребители => "Потребители/Администриране + АдминистраторЦКЗ"
            actionsMap.Map(UserTypeListActions.Search, (cu) => cu.IsSuperUser);

            // ### Създаване на група потребител => "Потребители/Администриране + АдминистраторЦКЗ"
            actionsMap.Map(UserTypeListActions.Create, (cu) => cu.IsSuperUser);

            // ### Преглед на група потребител => "Потребители/Администриране + АдминистраторЦКЗ"
            objectActionsMap.Map(UserTypeActions.View, (cu, utId) => cu.IsSuperUser);

            // ### Редакция на група потребител => "Потребители/Администриране + АдминистраторЦКЗ"
            objectActionsMap.Map(UserTypeActions.Edit, (cu, utId) => cu.IsSuperUser);

            // ### Изтриване на група потребител => "Потребители/Администриране + АдминистраторЦКЗ"
            objectActionsMap.Map(UserTypeActions.Delete, (cu, utId) => cu.IsSuperUser);

            // ## Пакети за актуализация

            // ### Търсене в списък пакети за актуализация => "Потребители/Администриране"
            // ### Търсене в списък пакети за актуализация => "Потребители/Контролиране"
            actionsMap.Map(RequestPackageListActions.Search, (cu) => cu.HasAnyCommonPermission(UserAdminPermissions.CanAdministrate, UserAdminPermissions.CanControl));

            // ### Създаване на пакет заявки => "Потребители/Администриране"
            actionsMap.Map(RequestPackageListActions.Create, (cu) => cu.HasCommonPermission(UserAdminPermissions.CanAdministrate));

            // ### Създаване на директна промяна => "Потребители/Администриране + АдминистраторЦКЗ"
            actionsMap.Map(RequestPackageListActions.CreateDirect, (cu) => cu.IsSuperUser);

            // ### Преглед на пакет за актуализация => "Потребители/КонтролиращЦКЗ"
            // ### Преглед на пакет за актуализация => "Потребители/АдминистраторЦКЗ"
            actionsMap.Map(RequestPackageListActions.CanControl, (cu) => cu.IsSuperUser || cu.IsMonitoringUser);

            // ### Преглед на пакет за актуализация => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Преглед на пакет за актуализация от организацията на потребителя => "Потребители/Администриране"
            // ### Преглед на пакет за актуализация от организацията на потребителя => "Потребители/Контролиране"
            objectActionsMap.Map<IRequestPackageClaimsContext>(RequestPackageActions.View, (cu, rp) => cu.IsSuperUser || cu.IsMonitoringUser || (cu.UserOrganizationId == rp.UserOrganizationId && (cu.HasAnyCommonPermission(UserAdminPermissions.CanAdministrate) || cu.HasAnyCommonPermission(UserAdminPermissions.CanControl))));

            // ### Редакция на пакет за актуализация => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Редакция на пакет за актуализация от организацията на потребителя=> "Потребители/Администриране"
            objectActionsMap.Map<IRequestPackageClaimsContext>(RequestPackageActions.Edit, (cu, rp) => cu.IsSuperUser || (cu.UserOrganizationId == rp.UserOrganizationId && cu.HasCommonPermission(UserAdminPermissions.CanAdministrate)));

            // ### Връщане в чернова на пакет за актуализация => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Връщане в чернова на пакет за актуализация от организацията на потребителя => "Потребители/Контролиране"
            objectActionsMap.Map<IRequestPackageClaimsContext>(RequestPackageActions.SetDraft, (cu, rp) => cu.IsSuperUser || (cu.UserOrganizationId == rp.UserOrganizationId && cu.HasCommonPermission(UserAdminPermissions.CanControl)));

            // ### Отбелязване като въведен на пакет за актуализация => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Отбелязване като въведен на пакет за актуализация от организацията на потребителя => "Потребители/Администриране"
            objectActionsMap.Map<IRequestPackageClaimsContext>(RequestPackageActions.SetEntered, (cu, rp) => cu.IsSuperUser || (cu.UserOrganizationId == rp.UserOrganizationId && cu.HasCommonPermission(UserAdminPermissions.CanAdministrate)));

            // ### Отбелязване като проверен на пакет за актуализация => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Отбелязване като проверен на пакет за актуализация от организацията на потребителя => "Потребители/Контролиране"
            objectActionsMap.Map<IRequestPackageClaimsContext>(RequestPackageActions.SetChecked, (cu, rp) => cu.IsSuperUser || (cu.UserOrganizationId == rp.UserOrganizationId && cu.HasCommonPermission(UserAdminPermissions.CanControl)));

            // ### Отбелязване като приключен на пакет за актуализация => "Потребители/Администриране + АдминистраторЦКЗ"
            objectActionsMap.Map<IRequestPackageClaimsContext>(RequestPackageActions.SetEnded, (cu, rp) => cu.IsSuperUser);

            // ### Отбелязване като анулиран на пакет за актуализация => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Отбелязване като анулиран на пакет за актуализация от организацията на потребителя => "Потребители/Контролиране"
            objectActionsMap.Map<IRequestPackageClaimsContext>(RequestPackageActions.SetCanceled, (cu, rp) => cu.IsSuperUser || (cu.UserOrganizationId == rp.UserOrganizationId && cu.HasCommonPermission(UserAdminPermissions.CanControl)));

            // ### Определяне на статуса на заявка от пакет за актуализация => "Потребители/Администриране + АдминистраторЦКЗ"
            objectActionsMap.Map<IRequestPackageClaimsContext>(RequestPackageActions.ChangeUserStatus, (cu, rp) => cu.IsSuperUser);

            // ##Потребителски профили

            // ### Търсене в списък потребителски профили => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Търсене в списък потребителски профили => "Потребители/Контролиране + АдминистраторЦКЗ"
            // ### Търсене в списък потребителски профили от организацията на потребителя => "Потребители/Администриране"
            // ### Търсене в списък потребителски профили от организацията на потребителя=> "Потребители/Контролиране"
            actionsMap.Map(UserListActions.Search, (cu) => cu.HasAnyCommonPermission(UserAdminPermissions.CanAdministrate, UserAdminPermissions.CanControl));

            // ### Създаване на потребителски профил => "Потребители/Администриране"
            actionsMap.Map(UserListActions.ViewCreate, (cu) => cu.HasCommonPermission(UserAdminPermissions.CanAdministrate));

            // ### Преглед на потребителски профил => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Преглед на потребителски профил от организацията на потребителя => "Потребители/Администриране"
            // ### Преглед на потребителски профил от организацията на потребителя => "Потребители/Контролиране"
            objectActionsMap.Map<IUserClaimsContextInternal>(UserActions.View, (cu, u) => cu.IsSuperUser || (cu.UserOrganizationId == u.UserOrganizationId && cu.HasAnyCommonPermission(UserAdminPermissions.CanAdministrate, UserAdminPermissions.CanControl)));

            // ### Изтриване/Възстановяване на потребителски профил => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Изтриване/Възстановяване на потребителски профил от организацията на потребителя => "Потребители/Администриране"
            objectActionsMap.Map<IUserClaimsContextInternal>(UserActions.SetIsDeleted, (cu, u) => cu.IsSuperUser || (cu.UserOrganizationId == u.UserOrganizationId && cu.HasCommonPermission(UserAdminPermissions.CanAdministrate)));

            // ### Заключване/отключване на потребителски профил => "Потребители/Администриране + АдминистраторЦКЗ"
            // ### Заключване/отключване на потребителски профил от организацията на потребителя => "Потребители/Администриране"
            objectActionsMap.Map<IUserClaimsContextInternal>(UserActions.SetIsLocked, (cu, u) => cu.IsSuperUser || (cu.UserOrganizationId == u.UserOrganizationId && cu.HasCommonPermission(UserAdminPermissions.CanAdministrate)));

            ///////////////////////////////////////////////////////////////////
            // # Модул кандидати
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък кандидати => "Кандидати/Четене"
            actionsMap.Map(CompanyListActions.Search, (cu) => cu.HasCommonPermission(CompanyPermissions.CanRead));

            // ### Създаване на кандидат => "Кандидати/Писане"
            actionsMap.Map(CompanyListActions.Create, (cu) => cu.HasCommonPermission(CompanyPermissions.CanWrite));

            // ### Преглед на кандидат => "Кандидати/Четене"
            objectActionsMap.Map(CompanyActions.View, (cu, cId) => cu.HasCommonPermission(CompanyPermissions.CanRead));

            // ### Редактиране на кандидат => "Кандидати/Писане"
            objectActionsMap.Map(CompanyActions.Edit, (cu, cId) => cu.HasCommonPermission(CompanyPermissions.CanWrite));

            // ### Изтриване на кандидат => "Кандидати/Писане"
            objectActionsMap.Map(CompanyActions.Delete, (cu, cId) => cu.HasCommonPermission(CompanyPermissions.CanWrite));

            ///////////////////////////////////////////////////////////////////
            // # Оперативна карта
            ///////////////////////////////////////////////////////////////////

            // ## Индикатори

            // ### Търсене в списък с индикатори => "Индикатори/Четене"
            actionsMap.Map(IndicatorListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(IndicatorPermissions.CanRead));

            // ### Създаване на индикатор => "Индикатори/Писане"
            actionsMap.Map(IndicatorListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(IndicatorPermissions.CanWrite));

            // ### Преглед на индикатор => "Индикатори/Четене"
            objectActionsMap.Map(IndicatorActions.View, (cu, inId) => cu.HasProgrammePermissionForAnyProgramme(IndicatorPermissions.CanRead));

            // ### Редактиране на индикатор => "Индикатори/Писане"
            objectActionsMap.Map(IndicatorActions.Edit, (cu, inId) => cu.HasProgrammePermissionForAnyProgramme(IndicatorPermissions.CanWrite));

            // ### Изтриване на индикатор => "Индикатори/Писане"
            objectActionsMap.Map(IndicatorActions.Delete, (cu, inId) => cu.HasProgrammePermissionForAnyProgramme(IndicatorPermissions.CanWrite));

            // ## Индикатори на елемент на оперативна карта

            // ### Търсене в списък с индикатори => "Индикатори/Четене"
            actionsMap.Map(MapNodeIndicatorListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(IndicatorPermissions.CanRead));

            // ### Създаване на индикатор => "Индикатори/Писане"
            actionsMap.Map(MapNodeIndicatorListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(IndicatorPermissions.CanWrite));

            // ### Преглед на индикатор => "Индикатори/Четене"
            objectActionsMap.Map<IMapNodeIndicatorClaimsContext>(MapNodeIndicatorActions.View, (cu, ind) => cu.HasProgrammePermission(ind.ProgrammeId, IndicatorPermissions.CanRead));

            // ### Редактиране на индикатор => "Индикатори/Писане"
            objectActionsMap.Map<IMapNodeIndicatorClaimsContext>(MapNodeIndicatorActions.Edit, (cu, ind) => cu.HasProgrammePermission(ind.ProgrammeId, IndicatorPermissions.CanWrite));

            // ## Мерни единици

            // ### Търсене в списък  мерни единици => "Оперативна карта/Четене"
            actionsMap.Map(MeasureListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Създаване на мерна единица => "Оперативна карта/Писане"
            actionsMap.Map(MeasureListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Преглед на на мерна единица => "Оперативна карта/Четене"
            objectActionsMap.Map(MeasureActions.View, (cu, mId) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Редактиране на на мерна единица => "Оперативна карта/Писане"
            objectActionsMap.Map(MeasureActions.Edit, (cu, mId) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Изтриване на на мерна единица => "Оперативна карта/Писане"
            objectActionsMap.Map(MeasureActions.Delete, (cu, mId) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ## Теми за формуляр за провеждане на проверки на място

            // ### Търсене в списък теми за формуляр за провеждане на проверки на място => "Оперативна карта/Четене"
            actionsMap.Map(CheckBlankTopicListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Създаване на тема за формуляр за провеждане на проверки на място => "Оперативна карта/Писане"
            actionsMap.Map(CheckBlankTopicListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Преглед на тема за формуляр за провеждане на проверки на място => "Оперативна карта/Четене"
            objectActionsMap.Map(CheckBlankTopicActions.View, (cu, tId) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Редактиране на тема за формуляр за провеждане на проверки на място => "Оперативна карта/Писане"
            objectActionsMap.Map(CheckBlankTopicActions.Edit, (cu, tId) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Изтриване на тема за формуляр за провеждане на проверки на място => "Оперативна карта/Писане"
            objectActionsMap.Map(CheckBlankTopicActions.Delete, (cu, tId) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ## Типове разходи

            // ### Търсене в списък типове разходи => "Оперативна карта/Четене"
            actionsMap.Map(ExpenseTypeListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Създаване на тип разход => "Оперативна карта/Писане"
            actionsMap.Map(ExpenseTypeListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Преглед на тип разход => "Оперативна карта/Четене"
            objectActionsMap.Map(ExpenseTypeActions.View, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Редактиране на тип разход => "Оперативна карта/Писане"
            objectActionsMap.Map(ExpenseTypeActions.Edit, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Изтриване на тип разход => "Оперативна карта/Писане"
            objectActionsMap.Map(ExpenseTypeActions.Delete, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ## Междинни суми

            // ### Търсене в списък междинни суми => "Оперативна карта/Четене"
            actionsMap.Map(ProgrammeGroupListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Създаване на междинна сума => "Оперативна карта/Писане"
            actionsMap.Map(ProgrammeGroupListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Преглед на междинна сума => "Оперативна карта/Четене"
            objectActionsMap.Map(ProgrammeGroupActions.View, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Редактиране на междинна сума => "Оперативна карта/Писане"
            objectActionsMap.Map(ProgrammeGroupActions.Edit, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Изтриване на междинна сума => "Оперативна карта/Писане"
            objectActionsMap.Map(ProgrammeGroupActions.Delete, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ## Надбавки

            // ### Търсене в списък надбавки => "Оперативна карта/Четене"
            actionsMap.Map(AllowanceListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Създаване на надбавка => "Оперативна карта/Писане"
            actionsMap.Map(AllowanceListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Преглед на надбавка => "Оперативна карта/Четене"
            objectActionsMap.Map(AllowanceActions.View, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Редактиране на надбавка => "Оперативна карта/Писане"
            objectActionsMap.Map(AllowanceActions.Edit, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Изтирване на надбавка => "Оперативна карта/Писане"
            objectActionsMap.Map(AllowanceActions.Delete, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ## Осн.лихвени проценти

            // ### Търсене в списък осн.лихвени проценти => "Оперативна карта/Четене"
            actionsMap.Map(BasicInterestRateListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Създаване на осн.лихвен процент => "Оперативна карта/Писане"
            actionsMap.Map(BasicInterestRateListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Преглед на осн.лихвен процент => "Оперативна карта/Четене"
            objectActionsMap.Map(BasicInterestRateActions.View, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Редактиране на осн.лихвен процент => "Оперативна карта/Писане"
            objectActionsMap.Map(BasicInterestRateActions.Edit, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Изтриване на осн.лихвен процент => "Оперативна карта/Писане"
            objectActionsMap.Map(BasicInterestRateActions.Delete, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ## Схеми за олихвяване

            // ### Търсене в списък схеми за олихвяване => "Оперативна карта/Четене"
            actionsMap.Map(InterestSchemeListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Създаване на схема за олихвяване => "Оперативна карта/Писане"
            actionsMap.Map(InterestSchemeListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Преглед на схема за олихвяване => "Оперативна карта/Четене"
            objectActionsMap.Map(InterestSchemeActions.View, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanRead));

            // ### Редактиране на схема за олихвяване => "Оперативна карта/Писане"
            objectActionsMap.Map(InterestSchemeActions.Edit, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ### Изтирване на схема за олихвяване => "Оперативна карта/Писане"
            objectActionsMap.Map(InterestSchemeActions.Delete, (cu, et) => cu.HasProgrammePermissionForAnyProgramme(OperationalMapPermissions.CanWrite));

            // ## Оперативна карта

            // ### Създаване на ОП => "Оперативна карта/Администриране"
            actionsMap.Map(ProgrammeListActions.Create, (cu) => cu.HasCommonPermission(OperationalMapAdminPermissions.CanAdministrate));

            // ### Създаване на ПО => "Оперативна карта/Писане"
            objectActionsMap.Map<IProgrammeClaimsContext>(ProgrammeActions.CreateProgrammePriority, (cu, mn) => cu.HasProgrammePermission(mn.ProgrammeId, OperationalMapPermissions.CanWrite));

            // ### Изтриване на ОП => "Оперативна карта/Администриране"
            objectActionsMap.Map<IProgrammeClaimsContext>(ProgrammeActions.Delete, (cu, mn) => cu.HasCommonPermission(OperationalMapAdminPermissions.CanAdministrate));

            // ### Преглед на ОП/ПО/ИП/СЦ => "Оперативна карта/Четене"
            objectActionsMap.Map<IProgrammeClaimsContext>(ProgrammeActions.View, (cu, mn) => cu.HasProgrammePermission(mn.ProgrammeId, OperationalMapPermissions.CanRead) || (mn.MapNodeType == MapNodeType.Programme && cu.HasCommonPermission(OperationalMapAdminPermissions.CanAdministrate)));

            // ### Редактиране на ОП/ПО/ИП/СЦ => "Оперативна карта/Писане"
            objectActionsMap.Map<IProgrammeClaimsContext>(ProgrammeActions.Edit, (cu, mn) => cu.HasProgrammePermission(mn.ProgrammeId, OperationalMapPermissions.CanWrite) || (mn.MapNodeType == MapNodeType.Programme && cu.HasCommonPermission(OperationalMapAdminPermissions.CanAdministrate)));

            // ## Декларации

            // ### Търсене в списък с декларации => "Новини/Публикуване"
            actionsMap.Map(DeclarationListActions.Search, (cu) => cu.HasCommonPermission(NewsPermissions.CanPublish));

            // ### Създаване на декларация => "Новини/Публикуване"
            actionsMap.Map(DeclarationListActions.Create, (cu) => cu.HasCommonPermission(NewsPermissions.CanPublish));

            // ### Преглед на декларация => "Новини/Публикуване"
            objectActionsMap.Map(DeclarationActions.View, (cu, dId) => cu.HasCommonPermission(NewsPermissions.CanPublish));

            // ### Редакция на декларация => "Новини/Публикуване"
            objectActionsMap.Map(DeclarationActions.Edit, (cu, dId) => cu.HasCommonPermission(NewsPermissions.CanPublish));

            ///////////////////////////////////////////////////////////////////
            // # Профили
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък профили => "Профили/Четене"
            actionsMap.Map(RegistrationListActions.Search, (cu) => cu.HasCommonPermission(RegistrationPermissions.CanRead));

            // ### Преглед на профил => "Профили/Четене"
            objectActionsMap.Map(RegistrationActions.View, (cu, regId) => cu.HasCommonPermission(RegistrationPermissions.CanRead));

            ///////////////////////////////////////////////////////////////////
            // # Кодове за достъп към договор
            ///////////////////////////////////////////////////////////////////

            // ### Преглед на код за достъп към договор => "Договори/Четене за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractAccessCodeActions.View, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanRead) || cu.IsContractExternalUser(c.ContractId));

            // ### Търсене в списък кодове за достъп към договор => "Профили за достъп към договор/Четене"
            actionsMap.Map(ContractAccessCodeListActions.Search, (cu) => cu.HasCommonPermission(ContractRegistrationPermissions.CanRead));

            ///////////////////////////////////////////////////////////////////
            // # Профили за достъп към договор
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък профили за достъп към договор => "Профили за достъп към договор/Четене"
            actionsMap.Map(ContractRegistrationListActions.Search, (cu) => cu.HasCommonPermission(ContractRegistrationPermissions.CanRead));

            ///////////////////////////////////////////////////////////////////
            // # Процедури
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък процедури по ОП => "Процедури/Четене за ОП"
            actionsMap.Map(ProcedureListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(ProcedurePermissions.CanRead));

            // ### Създаване на процедура по ОП => "Процедури/Писане за ОП"
            objectActionsMap.Map<IProgrammeClaimsContext>(ProgrammeActions.CreateProcedure, (cu, mn) => cu.HasProgrammePermission(mn.ProgrammeId, ProcedurePermissions.CanWrite));

            // ### Преглед на процедура по ОП => "Процедури/Четене за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureActions.View, (cu, pr) => cu.HasProgrammePermission(pr.ProgrammeId, ProcedurePermissions.CanRead));

            // ### Редактиране на процедура по ОП => "Процедури/Четене за ОП" + "Процедури/Писане за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureActions.Edit, (cu, pr) => cu.HasAllProgrammePermissions(pr.ProgrammeId, ProcedurePermissions.CanRead, ProcedurePermissions.CanWrite));

            // ### Връщане в чернова на процедура по ОП => "Процедури/Четене за ОП" + "Процедури/Писане за ОП" + "Процедури/Проверяване за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureActions.SetDraft, (cu, pr) => cu.HasAllProgrammePermissions(pr.ProgrammeId, ProcedurePermissions.CanCheck, ProcedurePermissions.CanRead, ProcedurePermissions.CanWrite));

            // ### Въвеждане на процедура по ОП => "Процедури/Четене за ОП" + "Процедури/Писане за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureActions.SetEntered, (cu, pr) => cu.HasAllProgrammePermissions(pr.ProgrammeId, ProcedurePermissions.CanRead, ProcedurePermissions.CanWrite));

            // ### Проверяване на процедура по ОП => "Процедури/Четене за ОП" + "Процедури/Писане за ОП" + "Процедури/Проверяване за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureActions.SetChecked, (cu, pr) => cu.HasAllProgrammePermissions(pr.ProgrammeId, ProcedurePermissions.CanCheck, ProcedurePermissions.CanRead, ProcedurePermissions.CanWrite));

            // ### Активиране на процедура по ОП => "Процедури/Четене за ОП" + "Процедури/Писане за ОП" + "Процедури/Проверяване за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureActions.SetActive, (cu, pr) => cu.HasAllProgrammePermissions(pr.ProgrammeId, ProcedurePermissions.CanCheck, ProcedurePermissions.CanRead, ProcedurePermissions.CanWrite));

            // ### Приключване на процедура по ОП => "Процедури/Четене за ОП" + "Процедури/Писане за ОП" + "Процедури/Проверяване за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureActions.SetEnded, (cu, pr) => cu.HasAllProgrammePermissions(pr.ProgrammeId, ProcedurePermissions.CanCheck, ProcedurePermissions.CanRead, ProcedurePermissions.CanWrite));

            // ### Прекратяване на процедура по ОП => "Процедури/Четене за ОП" + "Процедури/Писане за ОП" + "Процедури/Проверяване за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureActions.SetTerminated, (cu, pr) => cu.HasAllProgrammePermissions(pr.ProgrammeId, ProcedurePermissions.CanCheck, ProcedurePermissions.CanRead, ProcedurePermissions.CanWrite));

            // ### Анулиране на процедура по ОП => "Процедури/Четене за ОП" + "Процедури/Писане за ОП" + "Процедури/Изтриване за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureActions.SetCanceled, (cu, pr) => cu.HasAllProgrammePermissions(pr.ProgrammeId, ProcedurePermissions.CanDelete, ProcedurePermissions.CanRead, ProcedurePermissions.CanWrite));

            ///////////////////////////////////////////////////////////////////
            // # Обща кореспонденция
            ///////////////////////////////////////////////////////////////////

            // ### Преглед на обща кореспонденция => "Комуникация по договор/Четене за ОП"
            objectActionsMap.Map<IProcedureMassCommunicationClaimsContext>(ProcedureMassCommunicationActions.View, (cu, cc) => cu.HasProgrammePermission(cc.ProgrammeId, ContractCommunicationPermissions.CanRead));

            // ### Създаване на обща кореспонденция => "Комуникация по договор/Писане за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureMassCommunicationActions.Create, (cu, pc) => cu.HasProgrammePermission(pc.ProgrammeId, ContractCommunicationPermissions.CanWrite));

            // ### Редакция на обща кореспонденция => "Комуникация по договор/Писане за ОП"
            objectActionsMap.Map<IProcedureMassCommunicationClaimsContext>(ProcedureMassCommunicationActions.Edit, (cu, cc) => cu.HasProgrammePermission(cc.ProgrammeId, ContractCommunicationPermissions.CanWrite));

            ///////////////////////////////////////////////////////////////////
            // # Индикативни годишни работни програми
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък индикативни годишни работни програми => "Процедури/Четене за ОП"
            actionsMap.Map(IndicativeAnnualWorkingProgrammeListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(ProcedurePermissions.CanRead));

            // ### Създаване на индикативна годишна работна програма => "Процедури/Писане за ОП"
            actionsMap.Map(IndicativeAnnualWorkingProgrammeListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(ProcedurePermissions.CanWrite));

            ///////////////////////////////////////////////////////////////////
            // # Договори
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък договори по ОП => "Договори/Четене за ОП"
            actionsMap.Map(ContractListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(ContractPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Създаване на договор по ОП => "Договори/Писане за ОП"
            actionsMap.Map(ContractListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(ContractPermissions.CanWrite));

            // ### Преглед на договор по ОП => "Договори/Четене за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractActions.View, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanRead) || cu.IsContractExternalUser(c.ContractId));

            // ### Редакция на договор по ОП => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractActions.Edit, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanWrite));

            // ### Връщане на договор по ОП в чернова => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractActions.ChangeStatusToDraft, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanWrite));

            // ### Изтриване на договор по ОП в чернова => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractActions.Delete, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanWrite));

            // ### Проверяване на договор по ОП => "Договори/Проверяване за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractActions.MarkAsChecked, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanCheck));

            // ### Преглед на версия на договор по ОП => "Договори/Четене за ОП"
            objectActionsMap.Map<IContractVersionClaimsContext>(ContractVersionActions.View, (cu, cv) => cu.HasProgrammePermission(cv.ProgrammeId, ContractPermissions.CanRead) || cu.IsContractExternalUser(cv.ContractId));

            // ### Редакция на версия на договор по ОП => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractVersionClaimsContext>(ContractVersionActions.Edit, (cu, cv) => cu.HasProgrammePermission(cv.ProgrammeId, ContractPermissions.CanWrite));

            // ### Изтриване на версия на договор по ОП => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractVersionClaimsContext>(ContractVersionActions.Delete, (cu, cv) => cu.HasProgrammePermission(cv.ProgrammeId, ContractPermissions.CanWrite));

            // ### Връщане на версия на договор по ОП в чернова => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractVersionClaimsContext>(ContractVersionActions.ChangeStatusToDraft, (cu, cv) => cu.HasProgrammePermission(cv.ProgrammeId, ContractPermissions.CanWrite));

            // ### Проверяване на версия на договор по ОП => "Договори/Проверяване за ОП"
            objectActionsMap.Map<IContractVersionClaimsContext>(ContractVersionActions.MarkAsChecked, (cu, cv) => cu.HasProgrammePermission(cv.ProgrammeId, ContractPermissions.CanCheck));

            // ### Преглед на процедурa за избор на изпълнител и сключени договори на договор по ОП => "Договори/Четене за ОП"
            objectActionsMap.Map<IContractProcurementClaimsContext>(ContractProcurementActions.View, (cu, cp) => cu.HasProgrammePermission(cp.ProgrammeId, ContractPermissions.CanRead) || cu.IsContractExternalUser(cp.ContractId));

            // ### Редакция на процедурa за избор на изпълнител и сключени договори на договор по ОП => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractProcurementClaimsContext>(ContractProcurementActions.Edit, (cu, cp) => cu.HasProgrammePermission(cp.ProgrammeId, ContractPermissions.CanWrite));

            // ### Изтриване на процедурa за избор на изпълнител и сключени договори на договор по ОП => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractProcurementClaimsContext>(ContractProcurementActions.Delete, (cu, cp) => cu.HasProgrammePermission(cp.ProgrammeId, ContractPermissions.CanWrite));

            // ### Връщане на процедурa за избор на изпълнител и сключени договори на договор по ОП в чернова => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractProcurementClaimsContext>(ContractProcurementActions.ChangeStatusToDraft, (cu, cp) => cu.HasProgrammePermission(cp.ProgrammeId, ContractPermissions.CanWrite));

            // ### Проверяване на процедурa за избор на изпълнител и сключени договори на договор по ОП => "Договори/Проверяване за ОП"
            objectActionsMap.Map<IContractProcurementClaimsContext>(ContractProcurementActions.MarkAsChecked, (cu, cp) => cu.HasProgrammePermission(cp.ProgrammeId, ContractPermissions.CanCheck));

            // ### Преглед на план за разходване на средствата на договор по ОП => "Договори/Четене за ОП"
            objectActionsMap.Map<IContractSpendingPlanClaimsContext>(ContractSpendingPlanActions.View, (cu, csp) => cu.HasProgrammePermission(csp.ProgrammeId, ContractPermissions.CanRead) || cu.IsContractExternalUser(csp.ContractId));

            // ### Редакция на план за разходване на средствата на договор по ОП => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractSpendingPlanClaimsContext>(ContractSpendingPlanActions.Edit, (cu, csp) => cu.HasProgrammePermission(csp.ProgrammeId, ContractPermissions.CanWrite));

            // ### Изтриване на план за разходване на средствата на договор по ОП => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractSpendingPlanClaimsContext>(ContractSpendingPlanActions.Delete, (cu, csp) => cu.HasProgrammePermission(csp.ProgrammeId, ContractPermissions.CanWrite));

            // ### Връщане на план за разходване на средствата на договор по ОП в чернова => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractSpendingPlanClaimsContext>(ContractSpendingPlanActions.ChangeStatusToDraft, (cu, csp) => cu.HasProgrammePermission(csp.ProgrammeId, ContractPermissions.CanWrite));

            // ### Проверяване на план за разходване на средствата на договор по ОП => "Договори/Проверяване за ОП"
            objectActionsMap.Map<IContractSpendingPlanClaimsContext>(ContractSpendingPlanActions.MarkAsChecked, (cu, csp) => cu.HasProgrammePermission(csp.ProgrammeId, ContractPermissions.CanCheck));

            // ### Преглед на oферта за избор на изпълнител към договор по ОП => "Договори/Четене за ОП"
            objectActionsMap.Map<IContractProcurementsOfferClaimsContext>(ContractProcurementsOfferActions.View, (cu, csp) => cu.HasProgrammePermission(csp.ProgrammeId, ContractPermissions.CanRead) || cu.IsContractExternalUser(csp.ContractId));

            // ### Търсене на кореспонденция към договор по ОП => "Кореспонденция към Договори/Четене за ОП"
            actionsMap.Map(ContractCommunicationListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(ContractCommunicationPermissions.CanRead));
            objectActionsMap.Map<IContractClaimsContext>(ContractActions.SearchAdminAuthorityCommunications, (cu, cc) => cu.HasProgrammePermission(cc.ProgrammeId, ContractCommunicationPermissions.CanRead) || cu.IsContractExternalUser(cc.ContractId));

            // ### Създаване на кореспонденция към договор по ОП => "Кореспонденция към Договори/Писане за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractActions.CreateAdminAuthorityCommunication, (cu, cc) => cu.HasProgrammePermission(cc.ProgrammeId, ContractCommunicationPermissions.CanWrite) || cu.IsContractExternalUser(cc.ContractId));

            // ### Преглед на кореспонденция към договор по ОП => "Кореспонденция към Договори/Четене за ОП"
            objectActionsMap.Map<IContractCommunicationClaimsContext>(ContractCommunicationActions.View, (cu, cc) => cu.HasProgrammePermission(cc.ProgrammeId, ContractCommunicationPermissions.CanRead) || cu.IsContractExternalUser(cc.ContractId));

            // ### Редакция на кореспонденция към договор по ОП => "Кореспонденция към Договори/Писане за ОП"
            objectActionsMap.Map<IContractCommunicationClaimsContext>(ContractCommunicationActions.Edit, (cu, cc) => cu.HasProgrammePermission(cc.ProgrammeId, ContractCommunicationPermissions.CanWrite) || cu.IsContractExternalUser(cc.ContractId));

            // ### Изтриване на кореспонденция към договор по ОП => "Кореспонденция към Договори/Писане за ОП"
            objectActionsMap.Map<IContractCommunicationClaimsContext>(ContractCommunicationActions.Delete, (cu, cc) => cu.HasProgrammePermission(cc.ProgrammeId, ContractCommunicationPermissions.CanWrite));

            // ### Преглед на профил за достъп => "Договори/Четене за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractRegistrationActions.View, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanRead) || cu.IsContractExternalUser(c.ContractId));

            // ### Редакция на профил за достъп => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractRegistrationActions.Edit, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanWrite));

            // ### Създаване или прикачване на профил за достъп => "Договори/Писане за ОП и Профили за достъп към договор/Четене"
            objectActionsMap.Map<IContractClaimsContext>(ContractRegistrationActions.CreateOrAttachRegistration, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanWrite) && cu.HasCommonPermission(ContractRegistrationPermissions.CanRead));

            // ### Активиране на профил за достъп => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractRegistrationActions.Activate, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanWrite));

            // ### Деактивиране на профил за достъп => "Договори/Писане за ОП"
            objectActionsMap.Map<IContractClaimsContext>(ContractRegistrationActions.Deactivate, (cu, c) => cu.HasProgrammePermission(c.ProgrammeId, ContractPermissions.CanWrite));

            // ### Търсене на пакет отчетни документи към договор по ОП => "Пакет отчетни документи/Четене за ОП"
            actionsMap.Map(ContractReportListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(ContractReportPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Създаване на пакет отчетни документи към договор по ОП => "Пакет отчетни документи/Писане за ОП"
            actionsMap.Map(ContractReportListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(ContractReportPermissions.CanWrite));

            // ### Преглед на пакет отчетни документи към договор по ОП => "Пакет отчетни документи/Четене за ОП"
            objectActionsMap.Map<IContractReportClaimsContext>(ContractReportActions.View, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, ContractReportPermissions.CanRead));

            // ### Редакция на пакет отчетни документи към договор по ОП => "Пакет отчетни документи/Писане за ОП"
            objectActionsMap.Map<IContractReportClaimsContext>(ContractReportActions.Edit, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, ContractReportPermissions.CanWrite));

            // ### Изтриване на пакет отчетни документи към договор по ОП => "Пакет отчетни документи/Писане за ОП"
            objectActionsMap.Map<IContractReportClaimsContext>(ContractReportActions.Delete, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, ContractReportPermissions.CanWrite));

            // ### Проверяване на пакет отчетни документи към договор по ОП => "Пакет отчетни документи/Писане за ОП"
            objectActionsMap.Map<IContractReportClaimsContext>(ContractReportActions.MarkAsChecked, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, ContractReportPermissions.CanCheck));

            // ### Търсене на пакет отчетни документи за мониторинг към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ContractReportCheckListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Преглед на пакет отчетни документи за мониторинг към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IContractReportCheckClaimsContext>(ContractReportCheckActions.View, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanRead) || cu.IsContractExternalUser(cr.ContractId));

            // ### Редакция на финансова част на пакет отчетни документи за мониторинг към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<IContractReportCheckClaimsContext>(ContractReportCheckActions.EditFinancial, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.IsContractExternalUser(cr.ContractId));

            // ### Редакция на техническа част на пакет отчетни документи за мониторинг към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportCheckClaimsContext>(ContractReportCheckActions.EditTechnical, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteTechnical) || cu.IsContractExternalUser(cr.ContractId));

            // ### Редакция на обща част на пакет отчетни документи за мониторинг към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП" или "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportCheckClaimsContext>(ContractReportCheckActions.Edit, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteTechnical) || cu.IsContractExternalUser(cr.ContractId));

            // ### Маркиране в проверка на пакет отчетни документи към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП" или "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportCheckClaimsContext>(ContractReportCheckActions.MarkAsUnchecked, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteTechnical) || cu.IsContractExternalUser(cr.ContractId));

            // ### Маркиране като приет на пакет отчетни документи към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП" "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportCheckClaimsContext>(ContractReportCheckActions.MarkAsAccepted, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteTechnical) || cu.IsContractExternalUser(cr.ContractId));

            // ### Маркиране като отхвърлен на пакет отчетни документи към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП" "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportCheckClaimsContext>(ContractReportCheckActions.MarkAsRefused, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteTechnical) || cu.IsContractExternalUser(cr.ContractId));

            // ### Търсене на корекции на верифицирани суми на ниво РОД към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ContractReportFinancialCorrectionListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Създаване на корекция на верифицирана сума на ниво РОД към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(ContractReportFinancialCorrectionListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.HasAnyContractExternalUserPermission());

            // ### Търсене на корекции на верифицирани индикатори към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ContractReportTechnicalCorrectionListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Създаване на корекция на верифицирани индикатори към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            actionsMap.Map(ContractReportTechnicalCorrectionListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteTechnical) || cu.HasAnyContractExternalUserPermission());

            // ### Преглед на корекция на верифицирана сума на ниво РОД към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IContractReportFinancialCorrectionClaimsContext>(ContractReportFinancialCorrectionActions.View, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanRead) || cu.IsContractExternalUser(cr.ContractId));

            // ### Редакция на корекция на верифицирана сума на ниво РОД  към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportFinancialCorrectionClaimsContext>(ContractReportFinancialCorrectionActions.Edit, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.IsContractExternalUser(cr.ContractId));

            // ### Изтриване на корекция на верифицирана сума на ниво РОД  към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportFinancialCorrectionClaimsContext>(ContractReportFinancialCorrectionActions.Delete, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.IsContractExternalUser(cr.ContractId));

            // ### Преглед на корекция на верифицирани индикатори към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IContractReportTechnicalCorrectionClaimsContext>(ContractReportTechnicalCorrectionActions.View, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanRead) || cu.IsContractExternalUser(cr.ContractId));

            // ### Редакция на корекция на верифицирани индикатори към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportTechnicalCorrectionClaimsContext>(ContractReportTechnicalCorrectionActions.Edit, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteTechnical) || cu.IsContractExternalUser(cr.ContractId));

            // ### Изтриване на корекция на верифицирани индикатори към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportTechnicalCorrectionClaimsContext>(ContractReportTechnicalCorrectionActions.Delete, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteTechnical) || cu.IsContractExternalUser(cr.ContractId));

            // ### Връщане на корекция на верифицирани индикатори към договор по ОП в чернова => "АдминистраторЦКЗ"
            objectActionsMap.Map<IContractReportTechnicalCorrectionClaimsContext>(ContractReportTechnicalCorrectionActions.ChangeStatusToDraft, (cu, cr) => cu.IsSuperUser);

            // ### Търсене на корекции на верифицирани суми на други нива към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ContractReportCorrectionListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Създаване на корекция на верифицирана сума на друго ниво към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(ContractReportCorrectionListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.HasAnyContractExternalUserPermission());

            // ### Преглед на корекция на верифицирана сума на друго ниво към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IContractReportCorrectionClaimsContext>(ContractReportCorrectionActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead) || cu.IsContractExternalUser(fc.ContractId));

            // ### Редакция на корекция на верифицирана сума на друго ниво към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportCorrectionClaimsContext>(ContractReportCorrectionActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.IsContractExternalUser(fc.ContractId));

            // ### Търсене на препотвърждавания на верифицирани суми на ниво РОД към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ContractReportFinancialRevalidationListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead));

            // ### Създаване на препотвърждаване на верифицирана сума на ниво РОД към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(ContractReportFinancialRevalidationListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на препотвърждаване на верифицирана сума на ниво РОД към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IContractReportFinancialRevalidationClaimsContext>(ContractReportFinancialRevalidationActions.View, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Редакция на препотвърждаване на верифицирана сума на ниво РОД  към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportFinancialRevalidationClaimsContext>(ContractReportFinancialRevalidationActions.Edit, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Изтриване на препотвърждаване на верифицирана сума на ниво РОД  към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportFinancialRevalidationClaimsContext>(ContractReportFinancialRevalidationActions.Delete, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Търсене на препотвърждавания на верифицирани суми на други нива към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ContractReportRevalidationListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead));

            // ### Създаване на препотвърждаване на верифицирана сума на друго ниво към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(ContractReportRevalidationListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на препотвърждаване на верифицирана сума на друго ниво към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IContractReportRevalidationClaimsContext>(ContractReportRevalidationActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Редакция на препотвърждаване на верифицирана сума на друго ниво към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportRevalidationClaimsContext>(ContractReportRevalidationActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Търсене на изравнявания на сертифицирани суми на ниво РОД към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ContractReportFinancialCertCorrectionListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead));

            // ### Създаване на изравняване на сертифицирана сума на ниво РОД към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(ContractReportFinancialCertCorrectionListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на изравняване на сертифицирана сума на ниво РОД към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IContractReportFinancialCertCorrectionClaimsContext>(ContractReportFinancialCertCorrectionActions.View, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Редакция на изравняване на сертифицирана сума на ниво РОД  към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportFinancialCertCorrectionClaimsContext>(ContractReportFinancialCertCorrectionActions.Edit, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Изтриване на изравняване на сертифицирана сума на ниво РОД  към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportFinancialCertCorrectionClaimsContext>(ContractReportFinancialCertCorrectionActions.Delete, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Търсене на изравнявания на сертифицирани суми на други нива към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ContractReportCertCorrectionListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead));

            // ### Създаване на изравняване на сертифицирана сума на друго ниво към договор по ОП => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(ContractReportCertCorrectionListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на изравняване на сертифицирана сума на друго ниво към договор по ОП => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IContractReportCertCorrectionClaimsContext>(ContractReportCertCorrectionActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Редакция на изравняване на сертифицирана сума на друго ниво към договор по ОП => "Мониторинг и финансов контрол/Писане по техническа част за ОП"
            objectActionsMap.Map<IContractReportCertCorrectionClaimsContext>(ContractReportCertCorrectionActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Финансови корекции за системни пропуски
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък финансови корекции за системни пропуски => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(FlatFinancialCorrectionListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead));

            // ### Създаване на финансова корекция за системни пропуски => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(FlatFinancialCorrectionListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на финансова корекция за системни пропуски => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IFlatFinancialCorrectionClaimsContext>(FlatFinancialCorrectionActions.View, (cu, ffc) => cu.HasProgrammePermission(ffc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Редакция на финансова корекция за системни пропуски => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<IFlatFinancialCorrectionClaimsContext>(FlatFinancialCorrectionActions.Edit, (cu, ffc) => cu.HasProgrammePermission(ffc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Финансови корекции
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък финансови корекции => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(FinancialCorrectionListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Създаване на финансова корекция => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(FinancialCorrectionListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.HasAnyContractExternalUserPermission());

            // ### Преглед на финансова корекция => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IFinancialCorrectionClaimsContext>(FinancialCorrectionActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead) || cu.IsContractExternalUser(fc.ContractId));

            // ### Редакция на финансова корекция => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<IFinancialCorrectionClaimsContext>(FinancialCorrectionActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial) || cu.IsContractExternalUser(fc.ContractId));

            ///////////////////////////////////////////////////////////////////
            // # Дългове към договор
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък дългове към договор => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ContractDebtListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Създаване на дълг към договор => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(ContractDebtListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на дълг към договор => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IContractDebtClaimsContext>(ContractDebtActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead) || cu.IsContractExternalUser(fc.ContractId));

            // ### Редакция на дълг към договор => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<IContractDebtClaimsContext>(ContractDebtActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Дългове по ФКСП
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък дългове по ФКСП => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(CorrectionDebtListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead));

            // ### Създаване на дълг по ФКСП => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(CorrectionDebtListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на дълг по ФКСП => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<ICorrectionDebtClaimsContext>(CorrectionDebtActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Редакция на дълг по ФКСП => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<ICorrectionDebtClaimsContext>(CorrectionDebtActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Доклад по сертификация
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък доклади по сертификация => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(CertReportListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead));

            // ### Създаване на доклад по сертификация => "Мониторинг и финансов контрол/Писане за ОП"
            actionsMap.Map(CertReportListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на доклад по сертификация => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<ICertReportClaimsContext>(CertReportActions.View, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Редакция на доклад по сертификация => "Мониторинг и финансов контрол/Писане за ОП"
            objectActionsMap.Map<ICertReportClaimsContext>(CertReportActions.Edit, (cu, cr) => cu.HasProgrammePermission(cr.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Реално изплатени суми
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък реално изплатени суми => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ActuallyPaidAmountListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Създаване на реално изплатена сума => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(ActuallyPaidAmountListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на реално изплатена сума => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IActuallyPaidAmountClaimsContext>(ActuallyPaidAmountActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead) || cu.IsContractExternalUser(fc.ContractId));

            // ### Редакция на реално изплатена сума => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<IActuallyPaidAmountClaimsContext>(ActuallyPaidAmountActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Прогнози
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък прогнози => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(PrognosisListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead));

            // ### Преглед на Справка „Годишни прогнози“ => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IProgrammeClaimsContext>(PrognosisListActions.ViewYearlyReport, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Преглед на Справка „Месечни прогнози“ => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IProgrammeClaimsContext>(PrognosisListActions.ViewMonthlyReport, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Преглед на Справка „ЛОТАР по приоритетна ос“ => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IProgrammeClaimsContext>(PrognosisListActions.ViewProgrammePriorityReport, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Преглед на Справка „ЛОТАР – ОП“ => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IProgrammeClaimsContext>(PrognosisListActions.ViewProgrammeReport, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Създаване на прогноза => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(PrognosisListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на прогноза => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IProgrammePrognosisClaimsContext>(ProgrammePrognosisActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));
            objectActionsMap.Map<IProgrammePriorityPrognosisClaimsContext>(ProgrammePriorityPrognosisActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));
            objectActionsMap.Map<IProcedurePrognosisClaimsContext>(ProcedurePrognosisActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Редакция на прогноза => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<IProgrammePrognosisClaimsContext>(ProgrammePrognosisActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));
            objectActionsMap.Map<IProgrammePriorityPrognosisClaimsContext>(ProgrammePriorityPrognosisActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));
            objectActionsMap.Map<IProcedurePrognosisClaimsContext>(ProcedurePrognosisActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Възстановени суми
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък възстановени суми => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(DebtReimbursedAmountListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Създаване на възстановена сума => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(DebtReimbursedAmountListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на възстановена сума => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IDebtReimbursedAmountClaimsContext>(DebtReimbursedAmountActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead) || cu.IsContractExternalUser(fc.ContractId));

            // ### Редакция на възстановена сума => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<IDebtReimbursedAmountClaimsContext>(DebtReimbursedAmountActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Възстановени суми по договор
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък възстановени суми по договор => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(ContractReimbursedAmountListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead) || cu.HasAnyContractExternalUserPermission());

            // ### Създаване на възстановена сума по договор => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(ContractReimbursedAmountListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на възстановена по договор => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IContractReimbursedAmountClaimsContext>(ContractReimbursedAmountActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead) || cu.IsContractExternalUser(fc.ContractId));

            // ### Редакция на възстановена сума => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<IContractReimbursedAmountClaimsContext>(ContractReimbursedAmountActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Възстановени суми по ФИ
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък възстановени суми по договор => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(FIReimbursedAmountListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead));

            // ### Създаване на възстановена сума по договор => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(FIReimbursedAmountListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на възстановена по договор => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<IFIReimbursedAmountClaimsContext>(FIReimbursedAmountActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Редакция на възстановена сума => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<IFIReimbursedAmountClaimsContext>(FIReimbursedAmountActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Изравнителни документи
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък изравнителни документи => "Мониторинг и финансов контрол/Четене за ОП"
            actionsMap.Map(CompensationDocumentListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanRead));

            // ### Създаване на изравнителен документ => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            actionsMap.Map(CompensationDocumentListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(MonitoringFinancialControlPermissions.CanWriteFinancial));

            // ### Преглед на изравнителен документ => "Мониторинг и финансов контрол/Четене за ОП"
            objectActionsMap.Map<ICompensationDocumentClaimsContext>(CompensationDocumentActions.View, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanRead));

            // ### Редакция на изравнителен документ => "Мониторинг и финансов контрол/Писане по финансова част за ОП"
            objectActionsMap.Map<ICompensationDocumentClaimsContext>(CompensationDocumentActions.Edit, (cu, fc) => cu.HasProgrammePermission(fc.ProgrammeId, MonitoringFinancialControlPermissions.CanWriteFinancial));

            ///////////////////////////////////////////////////////////////////
            // # Модул наблюдение
            ///////////////////////////////////////////////////////////////////

            // ### Генериране на справка => "Наблюдение/Четене"
            actionsMap.Map(MonitoringActions.View, (cu) => cu.HasCommonPermission(MonitoringPermissions.CanRead));

            ///////////////////////////////////////////////////////////////////
            // # Интерфейси към SAP
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък от файлове от САП => "Връзки с външни информационни системи/Импортиране"
            actionsMap.Map(SapFileListActions.Search, (cu) => cu.HasCommonPermission(SapInterfacePermissions.CanImport));

            // ### Създаване на файл от САП => "Връзки с външни информационни системиИмпортиране"
            actionsMap.Map(SapFileListActions.Create, (cu) => cu.HasCommonPermission(SapInterfacePermissions.CanImport));

            // ### Преглед на файл от САП => "Връзки с външни информационни системи/Импортиране"
            objectActionsMap.Map(SapFileActions.View, (cu, sc) => cu.HasCommonPermission(SapInterfacePermissions.CanImport));

            // ### Импортиране на файл от САП => "Връзки с външни информационни системи/Импортиране"
            objectActionsMap.Map(SapFileActions.Import, (cu, sc) => cu.HasCommonPermission(SapInterfacePermissions.CanImport));

            // ### Редакция на файл от САП => "Връзки с външни информационни системи/Импортиране"
            objectActionsMap.Map(SapFileActions.Edit, (cu, sc) => cu.HasCommonPermission(SapInterfacePermissions.CanImport));

            ///////////////////////////////////////////////////////////////////
            // # Връзкa с RegiX
            ///////////////////////////////////////////////////////////////////

            // ### Използаване на интеграция RegiX => "Потребители/Администриране + АдминистраторЦКЗ"
            actionsMap.Map(RegixActions.View, (cu) => cu.IsSuperUser);

            ///////////////////////////////////////////////////////////////////
            // # Интерфейси към други информационни системи
            ///////////////////////////////////////////////////////////////////

            // ### Експортиране на данни от ИСУН => "Интерфейси към други информационни системи/Експортиране"
            actionsMap.Map(InterfacesActions.Export, (cu) => cu.HasCommonPermission(InterfacesPermissions.CanExport));

            ///////////////////////////////////////////////////////////////////
            // # Проектни предложения
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък проектни предложения по ОП => "Проектни предложения/Четене за ОП"
            actionsMap.Map(ProjectListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(ProjectPermissions.CanRead));

            // ### ViewCreate ??? => "Проектни предложения/Регистриране за ОП"
            actionsMap.Map(ProjectListActions.ViewCreate, (cu) => cu.HasProgrammePermissionForAnyProgramme(ProjectPermissions.CanRegister));

            // ### Преглед на рег.данни на проектно предложение => "Проектни предложения/Четене за ОП"
            // TODO: add action case for cu.IsEvalSessionProjectAdmin(proj.ProjectId), cu.IsEvalSessionProjectAssessor(proj.ProjectId)
            objectActionsMap.Map<IProjectClaimsContext>(ProjectActions.View, (cu, proj) => cu.HasProgrammePermission(proj.ProgrammeId, ProjectPermissions.CanRead) || cu.IsEvalSessionProjectAdmin(proj.ProjectId) || cu.IsEvalSessionProjectAssessor(proj.ProjectId) || cu.IsEvalSessionProjectAssistantAssessor(proj.ProjectId) || cu.IsEvalSessionProjectObserver(proj.ProjectId) || (cu.HasProgrammePermission(proj.ProgrammeId, EvalSessionPermissions.CanRead) && proj.IsProjectInFinishedEvalSession()));

            // ### Редактиране на рег.данни на проектно предложение => "Проектни предложения/Регистриране за ОП"
            // TODO: add action case for cu.IsEvalSessionProjectAdmin(proj.ProjectId)
            objectActionsMap.Map<IProjectClaimsContext>(ProjectActions.Edit, (cu, proj) => cu.HasProgrammePermission(proj.ProgrammeId, ProjectPermissions.CanRegister) || cu.IsEvalSessionProjectAdmin(proj.ProjectId));

            // ### Оттгеляне на проектно предложение => "Проектни предложения/Оттегяне за ОП"
            objectActionsMap.Map<IProjectClaimsContext>(ProjectActions.Withdraw, (cu, proj) => cu.HasProgrammePermission(proj.ProgrammeId, ProjectPermissions.CanWithdraw));

            // TODO:
            objectActionsMap.Map<IProjectClaimsContext>(ProjectActions.SearchCommunication, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId) || cu.IsEvalSessionProjectAssessor(c.ProjectId) || cu.IsEvalSessionProjectAssistantAssessor(c.ProjectId) || cu.IsEvalSessionProjectObserver(c.ProjectId) || (cu.HasProgrammePermission(c.ProgrammeId, EvalSessionPermissions.CanRead) && c.IsProjectInFinishedEvalSession()));

            // TODO:
            objectActionsMap.Map<IProjectClaimsContext>(ProjectActions.CreateCommunication, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));

            // TODO:
            objectActionsMap.Map<IProjectClaimsContext>(ProjectActions.SearchVersions, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId) || cu.IsEvalSessionProjectAssessor(c.ProjectId) || cu.IsEvalSessionProjectAssistantAssessor(c.ProjectId) || cu.IsEvalSessionProjectObserver(c.ProjectId) || (cu.HasProgrammePermission(c.ProgrammeId, EvalSessionPermissions.CanRead) && c.IsProjectInFinishedEvalSession()));

            // TODO:
            objectActionsMap.Map<IProjectClaimsContext>(ProjectActions.CreateVersion, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));

            // ### Създаване на рег.данни на проектно предложение => "Проектни предложения/Регистриране за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProcedureActions.CreateProject, (cu, pr) => cu.HasProgrammePermission(pr.ProgrammeId, ProjectPermissions.CanRegister));

            // ### Преглед на проектно досие => "Проектни досиета/Четене"
            objectActionsMap.Map<IProjectClaimsContext>(ProjectDossierActions.View, (cu, p) => cu.HasProgrammePermission(p.ProgrammeId, ProjectDossierPermissions.CanRead));

            ///////////////////////////////////////////////////////////////////
            // # Комуникация с УО към проектно предложение
            ///////////////////////////////////////////////////////////////////

            // ### Търсене в списък комуникации с УО към проектно предложение => "Договори/Четене за ОП"
            actionsMap.Map(ProjectManagingAuthorityCommunicationListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(ContractPermissions.CanRead));

            // ### Създаване на комуникация с УО към проектно предложение => "Договори/Писане за ОП"
            actionsMap.Map(ProjectManagingAuthorityCommunicationListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(ContractPermissions.CanWrite));

            // ### Преглед на комуникация с УО към проектно предложение => "Договори/Четене за ОП"
            objectActionsMap.Map<IProjectManagingAuthorityCommunicationClaimsContext>(ProjectManagingAuthorityCommunicationActions.View, (cu, pr) => cu.HasProgrammePermission(pr.ProgrammeId, ContractPermissions.CanRead));

            // ### Редакция на комуникация с УО към проектно предложение => "Договори/Писане за ОП"
            objectActionsMap.Map<IProjectManagingAuthorityCommunicationClaimsContext>(ProjectManagingAuthorityCommunicationActions.Edit, (cu, pr) => cu.HasProgrammePermission(pr.ProgrammeId, ContractPermissions.CanWrite));

            // ### Изтриване на комуникация с УО към проектно предложение => "Договори/Писане за ОП"
            objectActionsMap.Map<IProjectManagingAuthorityCommunicationClaimsContext>(ProjectManagingAuthorityCommunicationActions.Delete, (cu, pr) => cu.HasProgrammePermission(pr.ProgrammeId, ContractPermissions.CanWrite));

            // ### Анулиране на комуникация с УО към проектно предложение => "Договори/Писане за ОП"
            objectActionsMap.Map<IProjectManagingAuthorityCommunicationClaimsContext>(ProjectManagingAuthorityCommunicationActions.Cancel, (cu, pr) => cu.HasProgrammePermission(pr.ProgrammeId, ContractPermissions.CanWrite));

            ///////////////////////////////////////////////////////////////////
            // # Обща комуникация с УО
            ///////////////////////////////////////////////////////////////////

            // ### Преглед на обща комуникация с УО => "Договори/Четене за ОП"
            objectActionsMap.Map<IProjectMassManagingAuthorityCommunicationClaimsContext>(ProjectMassManagingAuthorityCommunicationActions.View, (cu, cc) => cu.HasProgrammePermission(cc.ProgrammeId, ContractPermissions.CanRead));

            // ### Създаване на обща комуникация с УО => "Договори/Писане за ОП"
            objectActionsMap.Map<IProcedureClaimsContext>(ProjectMassManagingAuthorityCommunicationActions.Create, (cu, pc) => cu.HasProgrammePermission(pc.ProgrammeId, ContractPermissions.CanWrite));

            // ### Редакция на обща комуникация с УО => "Договори/Писане за ОП"
            objectActionsMap.Map<IProjectMassManagingAuthorityCommunicationClaimsContext>(ProjectMassManagingAuthorityCommunicationActions.Edit, (cu, cc) => cu.HasProgrammePermission(cc.ProgrammeId, ContractPermissions.CanWrite));

            ///////////////////////////////////////////////////////////////////
            // # Лог на действията
            ///////////////////////////////////////////////////////////////////

            // ### Преглед на лог на действията => "Лог на действията/Четене"
            actionsMap.Map(ActionLogActions.View, (cu) => cu.HasCommonPermission(ActionLogPermissions.CanRead));

            actionsMap.Map(EvalSessionListActions.Search, (cu) => cu.HasProgrammePermissionForAnyProgramme(EvalSessionPermissions.CanAdministrate) || cu.HasProgrammePermissionForAnyProgramme(EvalSessionPermissions.CanEvaluate) || cu.HasProgrammePermissionForAnyProgramme(EvalSessionPermissions.CanRead));
            actionsMap.Map(EvalSessionListActions.Create, (cu) => cu.HasProgrammePermissionForAnyProgramme(EvalSessionPermissions.CanAdministrate));

            objectActionsMap.Map<IProjectCommunicationClaimsContext>(ProjectCommunicationActions.View, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId) || cu.IsEvalSessionProjectAssessor(c.ProjectId) || cu.IsEvalSessionProjectAssistantAssessor(c.ProjectId) || cu.IsEvalSessionProjectObserver(c.ProjectId) || (cu.HasProgrammePermission(c.ProgrammeId, EvalSessionPermissions.CanRead) && c.IsProjectInFinishedEvalSession()));
            objectActionsMap.Map<IProjectCommunicationClaimsContext>(ProjectCommunicationActions.Edit, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));
            objectActionsMap.Map<IProjectCommunicationClaimsContext>(ProjectCommunicationActions.Register, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));
            objectActionsMap.Map<IProjectCommunicationClaimsContext>(ProjectCommunicationActions.PrintRegistration, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));
            objectActionsMap.Map<IProjectCommunicationClaimsContext>(ProjectCommunicationActions.Apply, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));
            objectActionsMap.Map<IProjectCommunicationClaimsContext>(ProjectCommunicationActions.Reject, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));
            objectActionsMap.Map<IProjectCommunicationClaimsContext>(ProjectCommunicationActions.Cancel, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));
            objectActionsMap.Map<IProjectCommunicationClaimsContext>(ProjectCommunicationActions.Delete, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));

            objectActionsMap.Map<IProjectVersionClaimsContext>(ProjectVersionActions.View, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId) || cu.IsEvalSessionProjectAssessor(c.ProjectId) || cu.IsEvalSessionProjectAssistantAssessor(c.ProjectId) || cu.IsEvalSessionProjectObserver(c.ProjectId) || (cu.HasProgrammePermission(c.ProgrammeId, EvalSessionPermissions.CanRead) && c.IsProjectInFinishedEvalSession()));
            objectActionsMap.Map<IProjectVersionClaimsContext>(ProjectVersionActions.Edit, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));
            objectActionsMap.Map<IProjectVersionClaimsContext>(ProjectVersionActions.Delete, (cu, c) => cu.IsEvalSessionProjectAdmin(c.ProjectId));

            objectActionsMap.Map<IEvalSessionClaimsContext>(EvalSessionActions.ViewSession, (cu, es) => cu.HasProgrammePermission(es.ProgrammeId, EvalSessionPermissions.CanAdministrate) || cu.IsEvalSessionAdmin(es.EvalSessionId) || cu.IsEvalSessionObserver(es.EvalSessionId) || (cu.HasProgrammePermission(es.ProgrammeId, EvalSessionPermissions.CanRead) && (es.EvalSessionStatus == EvalSessionStatus.Ended || es.EvalSessionStatus == EvalSessionStatus.EndedByLAG)));
            objectActionsMap.Map<IEvalSessionClaimsContext>(EvalSessionActions.EditSession, (cu, es) => cu.HasProgrammePermission(es.ProgrammeId, EvalSessionPermissions.CanAdministrate));
            objectActionsMap.Map<IEvalSessionClaimsContext>(EvalSessionActions.ViewSessionData, (cu, es) => cu.IsEvalSessionAdmin(es.EvalSessionId) || cu.IsEvalSessionObserver(es.EvalSessionId) || (cu.HasProgrammePermission(es.ProgrammeId, EvalSessionPermissions.CanRead) && (es.EvalSessionStatus == EvalSessionStatus.Ended || es.EvalSessionStatus == EvalSessionStatus.EndedByLAG)));
            objectActionsMap.Map<IEvalSessionClaimsContext>(EvalSessionActions.EditSessionData, (cu, es) => cu.IsEvalSessionAdmin(es.EvalSessionId));

            objectActionsMap.Map<IEvalSessionClaimsContext>(EvalSessionActions.SetDraft, (cu, es) => cu.HasProgrammePermission(es.ProgrammeId, EvalSessionPermissions.CanAdministrate));
            objectActionsMap.Map<IEvalSessionClaimsContext>(EvalSessionActions.SetActive, (cu, es) => cu.HasProgrammePermission(es.ProgrammeId, EvalSessionPermissions.CanAdministrate));
            objectActionsMap.Map<IEvalSessionClaimsContext>(EvalSessionActions.SetEnded, (cu, es) => cu.IsEvalSessionAdmin(es.EvalSessionId));
            objectActionsMap.Map<IEvalSessionClaimsContext>(EvalSessionActions.SetEndedByLAG, (cu, es) => cu.IsEvalSessionAdmin(es.EvalSessionId));
            objectActionsMap.Map<IEvalSessionClaimsContext>(EvalSessionActions.SetCanceled, (cu, es) => cu.HasProgrammePermission(es.ProgrammeId, EvalSessionPermissions.CanAdministrate));

            objectActionsMap.Map<IEvalSessionClaimsContext>(MyEvalSession.ViewSession, (cu, es) => cu.IsEvalSessionAssessor(es.EvalSessionId) || cu.IsEvalSessionAssistantAssessor(es.EvalSessionId));
            objectActionsMap.Map<IEvalSessionClaimsContext>(MyEvalSession.ViewSessionSheets, (cu, es) => cu.IsEvalSessionAssessor(es.EvalSessionId));
            objectActionsMap.Map<IEvalSessionClaimsContext>(MyEvalSession.ViewSessionStandpoints, (cu, es) => cu.IsEvalSessionAssistantAssessor(es.EvalSessionId));

            objectActionsMap.Map<IEvalSessionSheetClaimsContext>(MyEvalSessionSheetActions.Edit, (cu, ess) => cu.IsAssessorAssociatedWithEvalSessionSheet(ess.EvalSessionSheetId));
            objectActionsMap.Map<IEvalSessionStandpointClaimsContext>(MyEvalSessionStandpointActions.View, (cu, ess) => cu.IsEvalSessionProjectAssessor(ess.ProjectId) || cu.IsEvalSessionProjectAssistantAssessor(ess.ProjectId));
            objectActionsMap.Map<IEvalSessionStandpointClaimsContext>(MyEvalSessionStandpointActions.Edit, (cu, ess) => cu.IsUserAssociatedWithEvalSessionStandpoint(ess.EvalSessionStandpointId));

            Initialize(actionsMap, objectActionsMap);
        }

        public static IDictionary<Enum, Func<IUserClaimsContextInternal, bool>> ActionsMap { get; private set; }

        public static IDictionary<Enum, Func<Dictionary<Type, object>, IUserClaimsContextInternal, int, bool>> ObjectActionsMap { get; private set; }

        public static IDictionary<Enum, string> ActionToString { get; private set; }

        public static IDictionary<string, Enum> StringToAction { get; private set; }

        private static void Map(this IDictionary<Enum, Func<IUserClaimsContextInternal, bool>> d, Enum e, Func<IUserClaimsContextInternal, bool> checkFunc)
        {
            d.Add(e, checkFunc);
        }

        private static void Map(this IDictionary<Enum, Func<Dictionary<Type, object>, IUserClaimsContextInternal, int, bool>> d, Enum e, Func<IUserClaimsContextInternal, int, bool> checkFunc)
        {
            Func<Dictionary<Type, object>, IUserClaimsContextInternal, int, bool> check = (factories, currentUserClaimsContext, id) =>
            {
                return checkFunc(currentUserClaimsContext, id);
            };

            d.Add(e, check);
        }

        private static void Map<TClaimsContext>(this IDictionary<Enum, Func<Dictionary<Type, object>, IUserClaimsContextInternal, int, bool>> d, Enum e, Func<IUserClaimsContextInternal, TClaimsContext, bool> checkFunc)
        {
            Func<Dictionary<Type, object>, IUserClaimsContextInternal, int, bool> check = (factories, currentUserClaimsContext, id) =>
            {
                TClaimsContext claimsContext = ((Func<int, TClaimsContext>)factories[typeof(TClaimsContext)])(id);
                return checkFunc(currentUserClaimsContext, claimsContext);
            };

            d.Add(e, check);
        }

        private static void Initialize(
            IDictionary<Enum, Func<IUserClaimsContextInternal, bool>> actionsMap,
            IDictionary<Enum, Func<Dictionary<Type, object>, IUserClaimsContextInternal, int, bool>> objectActionsMap)
        {
            IDictionary<Enum, string> actionToString = new Dictionary<Enum, string>();
            IDictionary<string, Enum> stringToAction = new Dictionary<string, Enum>();
            foreach (var key in actionsMap.Keys.Concat(objectActionsMap.Keys))
            {
                Type enumType = key.GetType();
                string actionString = enumType.Name + "." + Enum.GetName(enumType, key);

                actionToString.Add(key, actionString);
                stringToAction.Add(actionString, key);
            }

            ActionsMap = new ReadOnlyDictionary<Enum, Func<IUserClaimsContextInternal, bool>>(actionsMap);
            ObjectActionsMap = new ReadOnlyDictionary<Enum, Func<Dictionary<Type, object>, IUserClaimsContextInternal, int, bool>>(objectActionsMap);
            ActionToString = new ReadOnlyDictionary<Enum, string>(actionToString);
            StringToAction = new ReadOnlyDictionary<string, Enum>(stringToAction);
        }
    }
}
