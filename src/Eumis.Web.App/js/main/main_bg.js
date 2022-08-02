import angular from 'angular';
import AngularMomentModule from 'angular-moment';
import AngularL10NModule from 'l10n-angular/build/l10n-with-tools';

const BgModule = angular
  .module('main.bg', [AngularMomentModule, AngularL10NModule])
  .run([
    'amMoment',
    'moment',
    function(amMoment, moment) {
      moment.updateLocale('bg', {
        calendar: {
          sameDay: '[Днес]',
          nextDay: '[Утре]',
          nextWeek: 'dddd',
          lastDay: '[Вчера]',
          lastWeek: function() {
            switch (this.day()) {
              case 0:
              case 3:
              case 6:
                return '[В изминалата] dddd';
              case 1:
              case 2:
              case 4:
              case 5:
                return '[В изминалия] dddd';
            }
          },
          sameElse: 'L'
        }
      });

      amMoment.changeLocale('bg');
    }
  ])
  .config([
    'l10nProvider',
    function(l10n) {
      l10n.add('bg-bg', {
        unknownErrorMessage:
          'Възникна непредвидена грешка! Моля презаредете страницата с F5 и опитайте отново.',
        updateConcurrencyErrorMessage:
          'Обектът който се опитахте да запишете е бил променен! Моля презаредете страницата с F5 и опитайте отново.',
        objectNotFoundErrorMessage:
          'Обектът не може да бъде намерен! Моля презаредете страницата с F5 и опитайте отново.',
        forbiddenErrorMessage: 'Нямате права да извършите това действие!',
        sessionExpiredMessage:
          'Вашата сесия изтече и ще бъдете пренасочени към страницата за вход!',
        serviceUnavailableMessage:
          'Системата е в процес на обновяване. Моля, опитайте отново по-късно!',

        //common_messages
        common_messages_confirmDelete: 'Сигурни ли сте, че искате да изтриете записа?',
        common_messages_confirmDeactivate: 'Сигурни ли сте, че искате да деактивирате записа?',
        common_messages_confirmActivate: 'Сигурни ли сте, че искате да активирате записа?',
        common_messages_confirmLogout: 'Сигурни ли сте, че искате да излезете от системата?',

        //common_texts
        common_texts_yes: 'Да',
        common_texts_no: 'Не',

        //common_documentDirective
        common_documentDirective_text: 'Структуриран документ',
        common_documentDirective_portalEvalTableView: 'Преглед на оценителна таблица',
        common_documentDirective_portalEvalTableEdit: 'Редакция на оценителна таблица',
        common_documentDirective_portalProjectView: 'Преглед на проектно предложение',
        common_documentDirective_portalProjectEdit: 'Редакция на проектно предложение',
        common_documentDirective_portalEvalSessionSheetView: 'Преглед на оценителен лист',
        common_documentDirective_portalEvalSessionSheetEdit: 'Редакция на оценителен лист',
        common_documentDirective_portalEvalSessionStandpointView: 'Преглед на становище',
        common_documentDirective_portalEvalSessionStandpointEdit: 'Редакция на становище',
        common_documentDirective_portalProjectQuestionView: 'Преглед на въпрос към кандидат',
        common_documentDirective_portalProjectQuestionEdit: 'Редакция на въпрос към кандидат',
        common_documentDirective_portalProjectAnswerView: 'Преглед на отговор',
        common_documentDirective_portalProjectAnswerEdit: 'Редакция на отговор',
        common_documentDirective_portalProjectCommunicationView:
          'Преглед на комуникация с кандидата',
        common_documentDirective_portalProjectManagingAuthorityCommunicationView:
          'Преглед на комуникация',
        common_documentDirective_portalProjectManagingAuthorityCommunicationEdit:
          'Редакция на комуникация',
        common_documentDirective_portalContractView: 'Преглед на договор',
        common_documentDirective_portalContractEdit: 'Редакция на договор',
        common_documentDirective_portalContractEditPartial: 'Частична редакция на договор',
        common_documentDirective_portalContractOfferView:
          'Преглед на оферта към процедура за избор на изпълнител',
        common_documentDirective_portalContractProcurementView:
          'Преглед на процедура за избор на изпълнител и сключени договори',
        common_documentDirective_portalContractProcurementEdit:
          'Редакция на процедура за избор на изпълнител и сключени договори',
        common_documentDirective_portalContractCommunicationView:
          'Преглед на кореспонденция към договор',
        common_documentDirective_portalContractCommunicationEdit:
          'Редакция на кореспонденция към договор',
        common_documentDirective_portalContractSpendingPlanView:
          'Преглед на план за разходване на средствата',
        common_documentDirective_portalContractSpendingPlanEdit:
          'Редакция на план за разходване на средствата',
        common_documentDirective_portalContractReportTechnicalView:
          'Преглед на технически отчет към договор',
        common_documentDirective_portalContractReportTechnicalEdit:
          'Редакция на технически отчет към договор',
        common_documentDirective_portalContractReportFinancialView:
          'Преглед на финансов отчет към договор',
        common_documentDirective_portalContractReportFinancialEdit:
          'Редакция на финансов отчет към договор',
        common_documentDirective_portalContractReportPaymentView:
          'Преглед на искане за плащане към договор',
        common_documentDirective_portalContractReportPaymentEdit:
          'Редакция на искане за плащане към договор',
        common_documentDirective_portalContractReportMicroView: 'Преглед на микроданни към договор',

        //navigation
        navigation_title: 'СУНИ',
        navigation_logout: 'Изход',
        navigation_changePassword: 'Смяна на паролата',

        //navigation_modules
        navigation_modules_title: 'Модули',

        navigation_modules_systemInformation_title: 'Системна информация',
        navigation_modules_systemInformation_map: 'Основна структура',
        navigation_modules_systemInformation_measures: 'Мерни единици',
        navigation_modules_systemInformation_indicators: 'Индикатори',
        navigation_modules_systemInformation_indicatorTypes: 'Видове индикатори',
        navigation_modules_systemInformation_expenseTypes: 'Типове разходи',
        navigation_modules_systemInformation_interestSchemes: 'Схеми за олихвяване',
        navigation_modules_systemInformation_allowances: 'Надбавки',
        navigation_modules_systemInformation_basicInterestRates: 'Осн. лихвени проценти',
        navigation_modules_systemInformation_checkBlankTopics:
          'Теми за формуляр за провеждане на проверки на място',
        navigation_modules_systemInformation_declarations: 'Декларации',
        navigation_modules_systemInformation_programmeGroups: 'Номенклатура за междинни суми',
        navigation_modules_systemInformation_directions: 'Направления',
        navigation_modules_systemInformation_procurements: 'Централизирани обществени поръчки',
        navigation_modules_procedures: 'Бюджети',
        navigation_modules_procedures_map: 'Бюджети',
        navigation_modules_procedures_search: 'Търсене',
        navigation_modules_companies: 'Организации',
        navigation_modules_profiles: 'Профили',
        navigation_modules_profiles_registrations: 'Профили за кандидатстване',
        navigation_modules_profiles_contractRegistrations: 'Профили за достъп към договор',
        navigation_modules_profiles_contractAccessCodes: 'Кодове за достъп към договор',
        navigation_modules_projects: 'Проектни предложения',
        navigation_modules_projects_search: 'Проектни предложения',
        navigation_modules_projects_communications: 'Комуникация с организация',
        navigation_modules_projects_massCommunications: 'Обща комуникация с кандидати',
        navigation_modules_projectDossier: 'Проектно досие',
        navigation_modules_users_title: 'Потребители',
        navigation_modules_users_users: 'Потребителски профили',
        navigation_modules_users_pTemplates: 'Шаблони за групи',
        navigation_modules_users_userTypes: 'Групи потребители',
        navigation_modules_users_requestPackages: 'Пакети за актуализация',
        navigation_modules_users_userOrganizations: 'Организации',
        navigation_modules_actionLogs_title: 'Лог на действията',
        navigation_modules_actionLogs_internalActionLogs: 'Вътрешна система',
        navigation_modules_actionLogs_procedureActionLogs: 'Бюджети',
        navigation_modules_actionLogs_portalActionLogs: 'Портал',
        navigation_modules_actionLogs_loginActionLogs: 'Неуспешен вход в системата',
        navigation_modules_evalSessions: 'Оценителни сесии',
        navigation_modules_communications_title: 'Комуникации',
        navigation_modules_communications_news: 'Новини',
        navigation_modules_communications_guidances: 'Ръководства',
        navigation_modules_contracts: 'Договори',
        navigation_modules_contracts_search: 'Договори',
        navigation_modules_contracts_communication: 'Кореспонденция',
        navigation_modules_procedure_massCommunication: 'Обща кореспонденция',
        navigation_modules_contracts_report: 'Пакети отчетни документи',
        navigation_modules_monitoringFinancialControl: 'Мониторинг и финансов контрол',
        navigation_modules_monitoringFinancialControl_contractReportChecks:
          'Мониторинг на пакети отчетни документи',
        navigation_modules_monitoringFinancialControl_contractReportFinancialCorrections:
          'Коригиране на верифицирани суми на ниво РОД',
        navigation_modules_monitoringFinancialControl_contractReportCorrections:
          'Коригиране на верифицирани суми на други нива',
        navigation_modules_monitoringFinancialControl_contractReportFinancialRevalidations:
          'Препотвърждаване на верифицирани суми на ниво РОД',
        navigation_modules_monitoringFinancialControl_contractReportRevalidations:
          'Препотвърждаване на верифицирани суми на други нива',
        navigation_modules_monitoringFinancialControl_contractReportFinancialCertCorrections:
          'Изравняване на сертифицирани суми на ниво РОД',
        navigation_modules_monitoringFinancialControl_contractReportCertCorrections:
          'Изравняване на сертифицирани суми на други нива',
        navigation_modules_monitoringFinancialControl_contractReportTechnicalCorrections:
          'Коригиране на верифицирани индикатори',
        navigation_modules_monitoringFinancialControl_flatFinancialCorrection:
          'Финансови корекции за системни пропуски',
        navigation_modules_monitoringFinancialControl_financialCorrection: 'Финансови корекции',
        navigation_modules_monitoringFinancialControl_contractDebts: 'Дългове към договор',
        navigation_modules_monitoringFinancialControl_debtsReport: 'Книга на длъжниците',
        navigation_modules_monitoringFinancialControl_actuallyPaidAmounts: 'Реално изплатени суми',
        navigation_modules_monitoringFinancialControl_reimbursedAmounts:
          'Възстановени суми по дългове',
        navigation_modules_monitoringFinancialControl_contractReimbursedAmounts:
          'Възстановени суми по договор',
        navigation_modules_monitoringFinancialControl_fiReimbursedAmounts:
          'Възстановени суми по ФИ',
        navigation_modules_monitoringFinancialControl_compensationDocuments:
          'Изравнителни документи',
        navigation_modules_monitoringFinancialControl_correctionDebts: 'Дългове по ФКСП',
        navigation_modules_monitoringFinancialControl_correctionDebtsReport:
          'Книга на длъжниците по ФКСП',
        navigation_modules_monitoringFinancialControl_programmePrognoses:
          'Прогнози на ниво основна организация',
        navigation_modules_monitoringFinancialControl_programmePriorityPrognoses:
          'Прогнози на ниво разпоредител с бюджетни средства',
        navigation_modules_monitoringFinancialControl_procedurePrognoses: 'Прогнози на ниво бюджет',
        navigation_modules_monitoringFinancialControl_yearlyPrognosesReport:
          'Справка „Годишни прогнози“',
        navigation_modules_monitoringFinancialControl_monthlyPrognosesReport:
          'Справка „Месечни прогнози“',
        navigation_modules_monitoringFinancialControl_programmePriorityPrognosesReport:
          'Справка „ЛОТАР по разпоредител с бюджетни средства“',
        navigation_modules_monitoringFinancialControl_programmePrognosesReport:
          'Справка „ЛОТАР – ОП“',
        navigation_modules_monitoringFinancialControl_prognosesSummaryReport:
          'Справка „ЛОТАР – обобщена“',

        navigation_modules_monitoring: 'Наблюдение',
        navigation_modules_monitoring_аdvancePayments: 'Справка Аванси (чл.131)',
        navigation_modules_monitoring_anex3: 'Справка Анекс 3',
        navigation_modules_monitoring_doubleFunding: 'Справка Двойно финансиране',
        navigation_modules_monitoring_physicalExecution: 'Справки Физическо изпълнение',
        navigation_modules_monitoring_financialExecution: 'Справки Финансово изпълнение',
        navigation_modules_monitoring_projects: 'Справка Проектни предложения',
        navigation_modules_monitoring_contracts: 'Справка Договори',
        navigation_modules_monitoring_indicators: 'Справка Индикатори',
        navigation_modules_monitoring_contractReports: 'Справка Отчети',
        navigation_modules_monitoring_budgetLevels: 'Справка за бюджетни пера',
        navigation_modules_monitoring_financialCorrections: 'Справка Финансови корекции',
        navigation_modules_monitoring_concludedContracts: 'Справка Сключени договори',
        navigation_modules_monitoring_beneficiaryProjectsContracts:
          'Справка за проекти и договори на бенефициенти',
        navigation_modules_monitoring_evaluations: 'Справка Оценка',
        navigation_modules_monitoring_contractReportPayments: 'Справка по ИП',
        navigation_modules_monitoring_programmeSummary: 'Обобщена справка за ОП',
        navigation_modules_monitoring_irregularities: 'Справка Нередности',
        navigation_modules_monitoring_pin: 'Справка по ЕГН',
        navigation_modules_monitoring_microdataEsf: 'Справка Микроданни ЕСФ',
        navigation_modules_monitoring_arachne: 'Инструмент Arachne',
        navigation_modules_monitoring_v4Plus4: 'Справка V4+4',
        navigation_modules_monitoring_expenseTypes: 'Справка Типове разходи',
        navigation_modules_monitoring_sebra: 'Данни за СЕБРА',

        navigation_modules_interfaces: 'Връзка с външни информационни системи',
        navigation_modules_interfaces_SAPimprort: 'Връзка САП - Исун',
        navigation_modules_interfaces_SAPexport: 'Връзка Исун - САП',
        navigation_modules_interfaces_IACSimprort: 'Връзка Исак - Исун',
        navigation_modules_interfaces_other: 'Връзка Исун - други ИС',
        navigation_modules_interfaces_regix: 'Връзка с RegiX',
        navigation_modules_interfaces_monitorstat: 'Връзка с Мониторстат',

        //navigation_profile
        navigation_profile_title: 'Профил',
        navigation_profile_messages: 'Съобщения',
        navigation_profile_userNotifications: 'Нотификации',
        navigation_profile_changePassword: 'Смяна на парола',
        navigation_profile_notificationSettings: 'Настройка на нотификации',
        navigation_profile_userProfile: 'Преглед на потребителски профил',

        //navigation_help
        navigation_help_title: 'Помощ',
        navigation_help_support:
          'СУНИ се поддържа от дирекция „Централно координационно звено” в администрацията на Министерския съвет. Вашите ' +
          'въпроси може да изпращате на електронен адрес: suni@government.bg',

        //navigation_logout
        navigation_logout_title: 'Изход',

        //users_tabs
        users_tabs_edit: 'Регистрационни данни',
        users_tabs_permissions: 'Права',
        users_tabs_requests: 'История на промените',
        users_tabs_declarations: 'Декларации',
        users_tabs_notifications: 'Нотификации',

        //users_userForm
        users_userForm_username: 'Потребителско име',
        users_userForm_fullname: 'Три имена',
        users_userForm_userTypeId: 'Група потребители',
        users_userForm_email: 'E-mail',
        users_userForm_phone: 'Телефон',
        users_userForm_address: 'Адрес',
        users_userForm_organization: 'Организация',
        users_userForm_uin: 'ЕГН',
        users_userForm_position: 'Длъжност',
        users_userForm_notes: 'Коментар',
        users_userForm_active: 'Активен',
        users_userForm_deleted: 'Изтрит',
        users_userForm_locked: 'Заключен',
        users_userForm_usernameExists: 'Потребителското име е заето',
        users_userForm_usernameInvalid:
          'Потребителското име трябва да е поне 5 символа ' +
          'и да съдържа само букви, числа, подчертавки (_) и точки (.)',
        users_userForm_gdpr_declaration: 'Декларация - лични данни',
        users_userForm_gdpr_declaration_accepted: 'Приета',
        users_userForm_gdpr_declaration_acceptDate: 'Дата на приемане',

        //users_userPermissionForm
        users_userPermissionForm_permissions: 'Права на достъп',
        users_userPermissionForm_permissionsByModulesProgrammes:
          'Права на достъп по модули, спрямо оперативните програми',
        users_userPermissionForm_permissionsByModules: 'Права на достъп по модули',
        users_userPermissionForm_viewTemplate: 'Преглед',
        users_userPermissionForm_applyTemplate: 'Приложи',
        users_userPermissionForm_permissionTemplate: 'Шаблон за група',
        users_userPermissionForm_allRights: 'Всички',
        users_userPermissionForm_allRightsCheckBox: 'Всички програми',

        //users_search
        users_search_username: 'Потребителско име',
        users_search_name: 'Име',
        users_search_userOrganization: 'Организация',
        users_search_userType: 'Група',
        users_search_status: 'Статус',
        users_search_new: 'Нов потребителски профил',
        users_search_search: 'Търси',
        users_search_active: 'Активен',
        users_search_inactive: 'Неактивен',
        users_search_locked: 'Заключен',
        users_search_deleted: 'Изтрит',
        users_search_email: 'E-mail',
        users_search_hasAcceptedGDPRDeclaration: 'Декларация - Л.Д.',
        users_search_excelExport: 'Експорт',

        //users_new
        users_new_title: 'Нов потребител',
        users_new_save: 'Запис',
        users_new_cancel: 'Отказ',

        //users_edit
        users_edit_title: 'Данни за потребител',
        users_edit_delete: 'Изтриване',
        users_edit_recover: 'Възстановяване',
        users_edit_lock: 'Заключване',
        users_edit_unlock: 'Отключване',
        users_edit_confirmDelete: 'Сигурни ли сте, че искате да изтриете акаунта?',
        users_edit_confirmRecover: 'Сигурни ли сте, че искате да възстановите акаунта?',
        users_edit_confirmLock: 'Сигурни ли сте, че искате да заключите акаунта?',
        users_edit_confirmUnlock: 'Сигурни ли сте, че искате да отключите акаунта?',

        //users_permissions
        users_permissions_title: 'Права на достъп',
        users_permissions_noPermissions: 'Потребителят няма никакви права',
        users_permissions_permissionsByModules: 'Права на достъп по модули',
        users_permissions_permissionsByModulesProgrammes:
          'Права на достъп по модули, спрямо основните организации',

        //users_notificationSettings
        users_notificationSettings_view_title: 'Преглед на настройка за нотификация',
        users_notificationSettings_programmes: 'Отговорни организации',
        users_notificationSettings_programmePriorities: 'Разпоредители с бюджетни средства',
        users_notificationSettings_procedures: 'Бюджети',
        users_notificationSettings_contracts: 'Договори',

        //users_userInfo
        users_userInfo_text: 'Потребителско име: {{username}}, Име: {{fullname}}',

        //pTemplates_search
        pTemplates_search_name: 'Име',
        pTemplates_search_new: 'Нов шаблон',

        //pTemplates_new
        pTemplates_new_title: 'Нов шаблон за група',
        pTemplates_new_name: 'Име на шаблона',
        pTemplates_new_save: 'Запис',
        pTemplates_new_cancel: 'Отказ',

        //pTemplates_edit
        pTemplates_edit_title: 'Данни на шаблон за група',
        pTemplates_edit_name: 'Име на шаблона',
        pTemplates_edit_edit: 'Редакция',
        pTemplates_edit_save: 'Запис',
        pTemplates_edit_cancel: 'Отказ',
        pTemplates_edit_confirmUpdate: 'Сигурни ли сте, че искате да обновите шаблона?',

        //users_requests_search
        users_requests_search_permissionRequests: 'Промяна на права',
        users_requests_search_regDataRequests: 'Промяна на регистрационни данни',
        users_requests_search_packageType: 'Тип',
        users_requests_search_packageNumber: 'Номер',
        users_requests_search_packageCreateDate: 'Дата',
        users_requests_search_packageModifyDate: 'Дата и час на последна промяна',
        users_requests_search_statusName: 'Статус',
        users_requests_search_rejectionMessage: 'Съобщение за отхвърляне',
        users_requests_search_enteredByUser: 'Въвел',
        users_requests_search_checkedByUser: 'Проверил',
        users_requests_search_endedByUser: 'Приключил',

        //users_declarations_search
        users_declarations_searchForm_name: 'Наименование',
        users_declarations_searchForm_status: 'Статус',
        users_declarations_searchForm_activationDate: 'Дата на влизане в сила',
        users_declarations_searchForm_isAccepted: 'Приел',
        users_declarations_searchForm_acceptionDate: 'Дата на приемане',

        //changePasswordModal
        changePasswordModal_title: 'Смяна на парола',
        changePasswordModal_oldPassword: 'Текуща парола',
        changePasswordModal_newPassword: 'Нова парола',
        changePasswordModal_confirmNewPassword: 'Повторете новата парола',
        changePasswordModal_noPasswordMatch: 'Паролите не съвпадат',
        changePasswordModal_wrongPassword: 'Грешна парола',
        changePasswordModal_save: 'Запис',
        changePasswordModal_cancel: 'Отказ',

        //permissionTemplateModal
        procedures_permissionTemplateModal_title: 'Шаблон за група',
        procedures_permissionTemplateModal_ok: 'OK',

        //userTypes_userTypeForm
        userTypes_userTypeForm_name: 'Наименование',
        userTypes_userTypeForm_isSuperUser: 'Администратор ЦКЗ',
        userTypes_userTypeForm_userOrganizationId: 'Организация',
        userTypes_userTypeForm_permissionTemplateId: 'Шаблон за група',
        userTypes_userTypeForm_viewPermissionTemplate: 'Преглед',

        //userTypes_search
        userTypes_search_new: 'Нова група',
        userTypes_search_name: 'Наименование',

        //userTypes_new
        userTypes_new_title: 'Нова група потребители',
        userTypes_new_save: 'Запис',
        userTypes_new_cancel: 'Отказ',

        //userTypes_edit
        userTypes_edit_title: 'Редакция на група потребители',
        userTypes_edit_edit: 'Редакция',
        userTypes_edit_save: 'Запис',
        userTypes_edit_cancel: 'Отказ',
        userTypes_edit_delete: 'Изтриване',

        //requestPackages_tabs
        requestPackages_tabs_edit: 'Основни данни',
        requestPackages_tabs_users: 'Потребители',

        //requestPackages_requestPackagesForm
        requestPackages_requestPackagesForm_type: 'Тип',
        requestPackages_requestPackagesForm_status: 'Статус',
        requestPackages_requestPackagesForm_userOrganizationId: 'Организация',
        requestPackages_requestPackagesForm_userTypeId: 'Група потребители',
        requestPackages_requestPackagesForm_packageDescription: 'Описание',
        requestPackages_requestPackagesForm_code: 'Номер',
        requestPackages_requestPackagesForm_createDate: 'Дата на създаване',
        requestPackages_requestPackagesForm_documents: 'Документи',
        requestPackages_requestPackagesForm_file: 'Файл',
        requestPackages_requestPackagesForm_description: 'Описание',
        requestPackages_requestPackagesForm_noDocuments: 'Няма документи',
        requestPackages_requestPackagesForm_endedMessage: 'Съобщение за приключване',
        requestPackages_requestPackagesForm_addDocument: 'Добави',

        //requestPackages_search
        requestPackages_search_dateFrom: 'Дата от',
        requestPackages_search_dateTo: 'Дата до',
        requestPackages_search_new: 'Нов пакет заявки',
        requestPackages_search_newDirect: 'Нова директна промяна',
        requestPackages_search_code: 'Номер',
        requestPackages_search_type: 'Тип',
        requestPackages_search_createDate: 'Дата на създаване',
        requestPackages_search_organization: 'Организация',
        requestPackages_search_status: 'Статус',
        requestPackages_search_requestPackageUsersCount: 'Брой потребители в заявката',
        requestPackages_search_search: 'Търси',

        //requestPackages_new
        requestPackages_new_title: 'Нов пакет',
        requestPackages_new_save: 'Запис',
        requestPackages_new_cancel: 'Отказ',

        //requestPackages_edit
        requestPackages_edit_title: 'Данни за пакет',
        requestPackages_edit_edit: 'Редакция',
        requestPackages_edit_save: 'Запис',
        requestPackages_edit_cancel: 'Отказ',
        requestPackages_edit_canceled: 'Анулиран',
        requestPackages_edit_draft: 'Чернова',
        requestPackages_edit_entered: 'Въведен',
        requestPackages_edit_checked: 'Проверен',
        requestPackages_edit_ended: 'Приключен',
        requestPackages_edit_checkEnd: 'Провери и приключи',
        requestPackages_edit_checkEndConfirm:
          'Сигурни ли сте, че искате да проверите и приключите пакета ?',
        requestPackages_edit_changeStatusConfirm:
          "Сигурни ли сте, че искате да промените статуса на пакета на '{{status}}' ?",
        requestPackages_edit_endedMessage: 'Съобщение за приключване',

        //requestPackages_requestPackageUsers
        requestPackages_requestPackageUsers_type: 'Тип на пакета',
        requestPackages_requestPackageUsers_status: 'Статус на пакета',
        requestPackages_requestPackageUsers_userOrganization: 'Организация',
        requestPackages_requestPackageUsers_users: 'Потребители',
        requestPackages_requestPackageUsers_username: 'Потребителско име',
        requestPackages_requestPackageUsers_fullname: 'Три имена',
        requestPackages_requestPackageUsers_userType: 'Група',
        requestPackages_requestPackageUsers_userStatus: 'Статус',
        requestPackages_requestPackageUsers_regData: 'Рег.данни',
        requestPackages_requestPackageUsers_permissions: 'Права',
        requestPackages_requestPackageUsers_requestStatus: 'Статус',
        requestPackages_requestPackageUsers_active:
          "Сигурни ли сте, че искате да промените статуса на заявката/ите на 'Активна' ?",
        requestPackages_requestPackageUsers_rejected:
          "Сигурни ли сте, че искате да промените статуса на заявката/ите на 'Отхвърлена' ?",
        requestPackages_requestPackageUsers_checked:
          "Сигурни ли сте, че искате да промените статуса на заявката/ите на 'Проверена' ?",
        requestPackages_requestPackageUsers_noUsers: 'Няма избрани потребители',
        requestPackages_requestPackageUsers_requests: 'Заявки',
        requestPackages_requestPackageUsers_chooseUsers: 'Избери',
        requestPackages_requestPackageUsers_createAndChooseUser: 'Създай и избери',
        requestPackages_requestPackageUsers_rejectionMessage: 'Съобщение за отхвърляне',

        //actionLogs_internal
        actionLogs_internal_search_actionLogId: 'Лог идентификатор',
        actionLogs_internal_search_action: 'Действие',
        actionLogs_internal_search_aggregateRootId: 'Id на обект',
        actionLogs_internal_search_procedureId: 'Id на бюджет',
        actionLogs_internal_search_username: 'Потребител',
        actionLogs_internal_search_logDate: 'Дата',
        actionLogs_internal_search_rawUrl: 'URL',
        actionLogs_internal_search_requestId: 'RequestId',
        actionLogs_internal_search_remoteIpAddress: 'IP адрес',
        actionLogs_internal_search_search: 'Търсене',

        //actionLogs_portal
        actionLogs_portal_search_actionLogId: 'Лог идентификатор',
        actionLogs_portal_search_action: 'Действие',
        actionLogs_portal_search_aggregateRootId: 'Id на обект',
        actionLogs_portal_search_email: 'Потребител (ел. поща)',
        actionLogs_portal_search_logDate: 'Дата',
        actionLogs_portal_search_rawUrl: 'URL',
        actionLogs_portal_search_requestId: 'RequestId',
        actionLogs_portal_search_remoteIpAddress: 'IP адрес',
        actionLogs_portal_search_search: 'Търсене',

        //actionLogs_login
        actionLogs_login_search_actionLogId: 'Лог идентификатор',
        actionLogs_login_search_username: 'Потребител',
        actionLogs_login_search_logDate: 'Дата',
        actionLogs_login_search_rawUrl: 'URL',
        actionLogs_login_search_remoteIpAddress: 'IP адрес',
        actionLogs_login_search_search: 'Търсене',

        //actionLogs_view
        actionLogs_view_title: 'Детайли на действието',
        actionLogs_view_action: 'Действие',
        actionLogs_view_procedureId: 'Id на бюджет',
        actionLogs_view_username: 'Потребител',
        actionLogs_view_logDate: 'Дата',
        actionLogs_view_remoteIpAddress: 'IP адрес',
        actionLogs_view_postData: 'Снапшот',
        actionLogs_view_cancel: 'Отказ',

        //chooseUsersModal
        chooseUsersModal_title: 'Избор на потребители',
        chooseUsersModal_username: 'Потребителско име',
        chooseUsersModal_name: 'Име',
        chooseUsersModal_userOrganization: 'Организация',
        chooseUsersModal_userType: 'Група',
        chooseUsersModal_status: 'Статус',
        chooseUsersModal_continue: 'Продължи',
        chooseUsersModal_cancel: 'Отказ',

        //createAndChooseUserModal
        createAndChooseUserModal_title: 'Нов потребител',
        createAndChooseUserModal_createAndChoose: 'Създай и избери',
        createAndChooseUserModal_cancel: 'Отказ',

        //regDataRequestModal
        regDataRequestModal_newTitle: 'Нова заявка за промяна на регистрационни данни',
        regDataRequestModal_editTitle: 'Редакция на заявка за промяна на регистрационни данни',
        regDataRequestModal_viewTitle: 'Преглед на заявка за промяна на регистрационни данни',
        regDataRequestModal_save: 'Запис',
        regDataRequestModal_cancel: 'Отказ',
        regDataRequestModal_ok: 'ОК',
        regDataRequestModal_status: 'Статус на заявката',
        regDataRequestModal_rejectionMessage: 'Съобщение за отхвърляне',
        regDataRequestModal_username: 'Потребителско име',
        regDataRequestModal_userType: 'Група',
        regDataRequestModal_fullname: 'Три имена',
        regDataRequestModal_notes: 'Бележки',
        regDataRequestModal_email: 'E-mail',
        regDataRequestModal_phone: 'Телефон',
        regDataRequestModal_address: 'Адрес',
        regDataRequestModal_organization: 'Организация',
        regDataRequestModal_uin: 'ЕГН',
        regDataRequestModal_position: 'Длъжност',

        //permissionRequestModal
        permissionRequestModal_newTitle: 'Нова заявка за промяна на права',
        permissionRequestModal_editTitle: 'Редакция на заявка за промяна на права',
        permissionRequestModal_viewTitle: 'Преглед на заявка за промяна на права',
        permissionRequestModal_save: 'Запис',
        permissionRequestModal_cancel: 'Отказ',
        permissionRequestModal_ok: 'ОК',
        permissionRequestModal_status: 'Статус на заявката',
        permissionRequestModal_username: 'Потребителско име',
        permissionRequestModal_userOrganization: 'Организация',
        permissionRequestModal_userType: 'Група',
        permissionRequestModal_rejectionMessage: 'Съобщение за отхвърляне',
        permissionRequestModal_noPermissions: 'Потребителят няма никакви права',
        permissionRequestModal_permissionsByModules: 'Права на достъп по модули',
        permissionRequestModal_permissionsByModulesProgrammes:
          'Права на достъп по модули, спрямо оперативните програми',

        //operationalMap_common_indicatorDataForm
        operationalMap_common_indicatorDataForm_measure: 'Мерна единица',

        operationalMap_common_indicatorDataForm_name: 'Наименование',
        operationalMap_common_indicatorDataForm_nameAlt: 'Наименование на английски',
        operationalMap_common_indicatorDataForm_code: 'Код',
        operationalMap_common_indicatorDataForm_type: 'Тип',
        operationalMap_common_indicatorDataForm_kind: 'Вид',
        operationalMap_common_indicatorDataForm_trend: 'Тенденция',
        operationalMap_common_indicatorDataForm_aggregatedReport: 'Отчитане с натрупване',
        operationalMap_common_indicatorDataForm_aggregatedTarget: 'Целева ст-ст с натрупване',
        operationalMap_common_indicatorDataForm_hasGenderDivision: 'Разделение по пол',
        operationalMap_common_indicatorDataForm_hasQualitativeTarget: 'Целта е качеств. ст-ст',
        operationalMap_common_indicatorDataForm_sourceMapNode: 'Източник на прикачване',
        operationalMap_common_indicatorDataForm_reportingType: 'Начин на отчитане',
        operationalMap_common_indicatorDataForm_itemType: 'Вид индикатор',

        //operationalMap_common_mapNodeIndicatorForm
        operationalMap_common_mapNodeIndicatorForm_baseTotalValue: 'Базова ст-ст',
        operationalMap_common_mapNodeIndicatorForm_baseMenValue: 'Базова ст-ст (мъже)',
        operationalMap_common_mapNodeIndicatorForm_baseWomenValue: 'Базова ст-ст (жени)',
        operationalMap_common_mapNodeIndicatorForm_baseYear: 'Базова година',
        operationalMap_common_mapNodeIndicatorForm_targetTotalValue: 'Целева ст-ст',
        operationalMap_common_mapNodeIndicatorForm_targetMenValue: 'Целева ст-ст (мъже)',
        operationalMap_common_mapNodeIndicatorForm_targetWomenValue: 'Целева ст-ст (жени)',
        operationalMap_common_mapNodeIndicatorForm_milestoneTargetTotalValue: 'Етапна цел',
        operationalMap_common_mapNodeIndicatorForm_milestoneTargetMenValue: 'Етапна цел (мъже)',
        operationalMap_common_mapNodeIndicatorForm_milestoneTargetWomenValue: 'Етапна цел (жени)',
        operationalMap_common_mapNodeIndicatorForm_dataSource: 'Източник на данните',
        operationalMap_common_mapNodeIndicatorForm_description: 'Описание',
        operationalMap_common_mapNodeIndicatorForm_status: 'Статус',

        //operationalMap_common_operationalMapBudgetNextThreeForm
        operationalMap_common_operationalMapBudgetNextThreeForm_withAdvances: 'С аванси',
        operationalMap_common_operationalMapBudgetNextThreeForm_withoutAdvances: 'Без аванси',

        //operationalMap_common_operationalMapBudgetForm
        operationalMap_common_operationalMapBudgetForm_eu: 'ЕС',
        operationalMap_common_operationalMapBudgetForm_bg: 'НФ',
        operationalMap_common_operationalMapBudgetForm_total: 'Общо',
        operationalMap_common_operationalMapBudgetForm_percent: 'Процент',

        //operationalMap_common_documentForm
        operationalMap_common_documentForm_name: 'Наименование',
        operationalMap_common_documentForm_description: 'Описание',
        operationalMap_common_documentForm_file: 'Файл',
        operationalMap_common_documentForm_isDeleted: 'Анулиран',
        operationalMap_common_documentForm_isDeletedNote: 'Причина за анулиране',

        //operationalMap_common_indicatorReportedAmountDataForm
        operationalMap_common_indicatorReportedAmountDataForm_yearTitle: 'Година',
        operationalMap_common_indicatorReportedAmountDataForm_menTitle: 'Мъже',
        operationalMap_common_indicatorReportedAmountDataForm_womenTitle: 'Жени',
        operationalMap_common_indicatorReportedAmountDataForm_totalTitle: 'Общо',
        operationalMap_common_indicatorReportedAmountDataForm_year2014: '2014',
        operationalMap_common_indicatorReportedAmountDataForm_year2015: '2015',
        operationalMap_common_indicatorReportedAmountDataForm_year2016: '2016',
        operationalMap_common_indicatorReportedAmountDataForm_year2017: '2017',
        operationalMap_common_indicatorReportedAmountDataForm_year2018: '2018',
        operationalMap_common_indicatorReportedAmountDataForm_year2019: '2019',
        operationalMap_common_indicatorReportedAmountDataForm_year2020: '2020',
        operationalMap_common_indicatorReportedAmountDataForm_year2021: '2021',
        operationalMap_common_indicatorReportedAmountDataForm_year2022: '2022',
        operationalMap_common_indicatorReportedAmountDataForm_year2023: '2023',

        //expenseTypes_expenseSubTypeForm
        expenseTypes_expenseSubTypeForm_name: 'Наименование',
        expenseTypes_expenseSubTypeForm_nameAlt: 'Наименование на английски',

        //expenseTypes_expenseTypeForm
        expenseTypes_expenseTypeForm_name: 'Наименование',
        expenseTypes_expenseTypeForm_nameAlt: 'Наименование на английски',
        expenseTypes_expenseTypeForm_isActive: 'Активен',

        //expenseTypes_searchForm
        expenseTypes_searchForm_newBtn: 'Нов тип разход',
        expenseTypes_searchForm_name: 'Наименование',
        expenseTypes_searchForm_nameAlt: 'Наименование на английски',
        expenseTypes_searchForm_isActive: 'Активен',

        //expenseTypes_newForm
        expenseTypes_newForm_title: 'Нов тип разход',
        expenseTypes_newForm_save: 'Запис',
        expenseTypes_newForm_cancel: 'Отказ',

        //expenseTypes_editForm
        expenseTypes_editForm_title: 'Редакция на тип разход',
        expenseTypes_editForm_save: 'Запис',
        expenseTypes_editForm_cancel: 'Отказ',
        expenseTypes_editForm_edit: 'Редакция',
        expenseTypes_editForm_del: 'Изтриване',
        expenseTypes_editForm_deactivate: 'Деактивиране',
        expenseTypes_editForm_activate: 'Активиране',
        expenseTypes_editForm_subTypes: 'Подтипове разход',
        expenseTypes_editForm_subTypeName: 'Наименование',
        expenseTypes_editForm_subTypeNameAlt: 'Наименование на английски',

        //expenseTypes_newSubTypeModal
        expenseTypes_newSubTypeModal_title: 'Нов подтип разход',
        expenseTypes_newSubTypeModal_save: 'Запис',
        expenseTypes_newSubTypeModal_cancel: 'Отказ',

        //expenseTypes_editSubTypeModal
        expenseTypes_editSubTypeModal_title: 'Редакция на подтип разход',
        expenseTypes_editSubTypeModal_save: 'Запис',
        expenseTypes_editSubTypeModal_cancel: 'Отказ',

        //allowances_allowanceForm
        allowances_allowanceForm_name: 'Име',

        //allowances_allowanceRateForm
        allowances_allowanceRateForm_date: 'Дата',
        allowances_allowanceRateForm_rate: 'Процент',
        allowances_allowanceRateForm_incorrectDate:
          'Датата трябва да е по-голяма от последната дата в списъка с проценти',

        //allowances_searchForm
        allowances_searchForm_newBtn: 'Нова надбавка',
        allowances_searchForm_name: 'Име',

        //allowances_newForm
        allowances_newForm_title: 'Нова надбавка',
        allowances_newForm_save: 'Запис',
        allowances_newForm_cancel: 'Отказ',

        //allowances_editForm
        allowances_editForm_title: 'Редакция на надбавка',
        allowances_editForm_save: 'Запис',
        allowances_editForm_cancel: 'Отказ',
        allowances_editForm_edit: 'Редакция',
        allowances_editForm_del: 'Изтриване',
        allowances_editForm_rates: 'Проценти',
        allowances_editForm_rateDate: 'Дата',
        allowances_editForm_rateRate: 'Процент',

        //allowances_newRateModal
        allowances_newRateModal_title: 'Нов процент към надбавка',
        allowances_newRateModal_save: 'Запис',
        allowances_newRateModal_cancel: 'Отказ',

        //allowances_editRateModal
        allowances_editRateModal_title: 'Редакция на процент към надбавка',
        allowances_editRateModal_save: 'Запис',
        allowances_editRateModal_cancel: 'Отказ',

        //bInterestRates_bInterestRateForm
        bInterestRates_bInterestRateForm_name: 'Име',

        //bInterestRates_interestRateForm
        bInterestRates_interestRateForm_date: 'Дата',
        bInterestRates_interestRateForm_rate: 'Процент',
        bInterestRates_interestRateForm_incorrectDate:
          'Датата трябва да е по-голяма от последната дата в списъка с проценти',

        //bInterestRates_searchForm
        bInterestRates_searchForm_newBtn: 'Нов осн. лихвен процент',
        bInterestRates_searchForm_name: 'Име',

        //bInterestRates_newForm
        bInterestRates_newForm_title: 'Нов осн. лихвен процент',
        bInterestRates_newForm_save: 'Запис',
        bInterestRates_newForm_cancel: 'Отказ',

        //bInterestRates_editForm
        bInterestRates_editForm_title: 'Редакция на осн. лихвен процент',
        bInterestRates_editForm_save: 'Запис',
        bInterestRates_editForm_cancel: 'Отказ',
        bInterestRates_editForm_edit: 'Редакция',
        bInterestRates_editForm_del: 'Изтриване',
        bInterestRates_editForm_rates: 'Проценти',
        bInterestRates_editForm_rateDate: 'Дата',
        bInterestRates_editForm_rateRate: 'Процент',

        //bInterestRates_newRateModal
        bInterestRates_newRateModal_title: 'Нов процент към осн. лихвен процент',
        bInterestRates_newRateModal_save: 'Запис',
        bInterestRates_newRateModal_cancel: 'Отказ',

        //bInterestRates_editRateModal
        bInterestRates_editRateModal_title: 'Редакция на процент към осн. лихвен процент',
        bInterestRates_editRateModal_save: 'Запис',
        bInterestRates_editRateModal_cancel: 'Отказ',

        //interestSchemes_interestSchemeForm
        interestSchemes_interestSchemeForm_name: 'Име',
        interestSchemes_interestSchemeForm_basicInterestRate: 'Основен лихвен процент',
        interestSchemes_interestSchemeForm_allowance: 'Надбавка',
        interestSchemes_interestSchemeForm_annualBasis: 'Годишна база',
        interestSchemes_interestSchemeForm_isActive: 'Активна',

        //interestSchemes_searchForm
        interestSchemes_searchForm_newBtn: 'Нова схема',
        interestSchemes_searchForm_name: 'Име',
        interestSchemes_searchForm_basicInterestRate: 'Осн. лихвен процент',
        interestSchemes_searchForm_allowance: 'Надбавка',
        interestSchemes_searchForm_annualBasis: 'Годишна база',
        interestSchemes_searchForm_isActive: 'Активна',

        //interestSchemes_newForm
        interestSchemes_newForm_title: 'Нова схема за олихвяване',
        interestSchemes_newForm_save: 'Запис',
        interestSchemes_newForm_cancel: 'Отказ',

        //interestSchemes_editForm
        interestSchemes_editForm_title: 'Редакция на схема за олихвяване',
        interestSchemes_editForm_save: 'Запис',
        interestSchemes_editForm_cancel: 'Отказ',
        interestSchemes_editForm_edit: 'Редакция',
        interestSchemes_editForm_del: 'Изтриване',

        //checkBlankTopics_topicForm
        checkBlankTopics_topicForm_name: 'Име',

        //checkBlankTopics_searchForm
        checkBlankTopics_searchForm_newBtn: 'Нова тема',
        checkBlankTopics_searchForm_name: 'Име',

        //checkBlankTopics_newForm
        checkBlankTopics_newForm_title: 'Нова тема',
        checkBlankTopics_newForm_save: 'Запис',
        checkBlankTopics_newForm_cancel: 'Отказ',

        //checkBlankTopics_editForm
        checkBlankTopics_editForm_title: 'Редакция на тема',
        checkBlankTopics_editForm_save: 'Запис',
        checkBlankTopics_editForm_cancel: 'Отказ',
        checkBlankTopics_editForm_edit: 'Редакция',
        checkBlankTopics_editForm_del: 'Изтриване',

        //indicators_searchForm
        indicators_searchForm_title: 'Индикатори',
        indicators_searchForm_measure: 'Мерна единица',
        indicators_searchForm_programme: 'Основна организация',
        indicators_searchForm_name: 'Име',
        indicators_searchForm_code: 'Код',
        indicators_searchForm_type: 'Тип',
        indicators_searchForm_kind: 'Вид',
        indicators_searchForm_trend: 'Тенденция',
        indicators_searchForm_maleFemale: 'Пол',
        indicators_searchForm_aggregatedReport: 'Отчитане с натрупване',
        indicators_searchForm_aggregatedTarget: 'Целева ст-ст с натрупване',
        indicators_searchForm_hasGenderDivision: 'Разделение по пол',
        indicators_searchForm_hasQualitativeTarget: 'Целта е качеств. ст-ст',
        indicators_searchForm_hasSF: 'S/F класификация',
        indicators_searchForm_newBtn: 'Нов индикатор',
        indicators_searchForm_excelExport: 'Експорт',

        //indicators_newForm
        indicators_newForm_title: 'Нов индикатор',
        indicators_newForm_save: 'Запис',
        indicators_newForm_cancel: 'Отказ',

        //indicators_editForm
        indicators_editForm_title: 'Редакция на индикатор',
        indicators_editForm_save: 'Запис',
        indicators_editForm_cancel: 'Отказ',
        indicators_editForm_edit: 'Редакция',
        indicators_editForm_del: 'Изтриване',

        //indicatorTypes_searchForm
        indicatorTypes_searchForm_name: 'Наименование',
        indicatorTypes_searchForm_nameAlt: 'Наименование на английски език',
        indicatorTypes_searchForm_new: 'Нов вид индикатор',

        //indicatorTypes_newForm
        indicatorTypes_newForm_title: 'Нов вид индикатор',
        indicatorTypes_newForm_save: 'Запис',
        indicatorTypes_newForm_cancel: 'Отказ',

        //indicatorTypes_indicatorTypeDataForm
        indicatorTypes_indicatorTypeDataForm_name: 'Наименование',
        indicatorTypes_indicatorTypeDataForm_nameAlt: 'Наименование на английски език',

        //indicatorTypes_editForm
        indicatorTypes_editForm_title: 'Редакция на вид индикатор',
        indicatorTypes_editForm_edit: 'Редакция',
        indicatorTypes_editForm_del: 'Изтриване',
        indicatorTypes_editForm_save: 'Запис',
        indicatorTypes_editForm_cancel: 'Отказ',

        //directions_tabs
        directions_tabs_basicData: 'Основни данни',
        directions_tabs_subDirections: 'Поднаправления',

        //directions_searchSubDirectionForm
        directions_searchSubDirectionForm_title: 'Поднаправления',
        directions_searchSubDirectionForm_new: 'Ново поднаправление',
        directions_searchSubDirectionForm_name: 'Наименование',
        directions_searchSubDirectionForm_nameAlt: 'Наименование на английски език',

        //directions_newSubDirectionForm
        directions_newSubDirectionForm_title: 'Ново поднаправление',
        directions_newSubDirectionForm_save: 'Запис',
        directions_newSubDirectionForm_cancel: 'Отказ',

        //directions_editSbuDirectionForm
        directions_editSbuDirectionForm_title: 'Редакция на поднаправление',
        directions_editSbuDirectionForm_edit: 'Редакция',
        directions_editSbuDirectionForm_del: 'Изтриване',
        directions_editSbuDirectionForm_save: 'Запис',
        directions_editSbuDirectionForm_cancel: 'Отказ',

        //directions_searchDirectionForm
        directions_searchDirectionForm_new: 'Ново направление',
        directions_searchDirectionForm_name: 'Наименование',
        directions_searchDirectionForm_nameAlt: 'Наименование на английски език',

        //directions_newDirectionForm
        directions_newDirectionForm_title: 'Ново направление',
        directions_newDirectionForm_save: 'Запис',
        directions_newDirectionForm_cancel: 'Отказ',

        //directions_editDirectionForm
        directions_editDirectionForm_title: 'Редакция на направление',
        directions_editDirectionForm_edit: 'Редакция',
        directions_editDirectionForm_del: 'Изтриване',
        directions_editDirectionForm_changeStatusToEntered: 'Въведен',
        directions_editDirectionForm_changeStatusToDraft: 'Чернова',
        directions_editDirectionForm_save: 'Запис',
        directions_editDirectionForm_cancel: 'Отказ',
        directions_editDirectionForm_changeStatusQuestion:
          'Сигурни ли сте че искате да промените статуса на "{{status}}"',

        //directions_subDirectionForm
        directions_subDirectionForm_name: 'Наименование',
        directions_subDirectionForm_nameAlt: 'Наименование на английски език',

        //directions_directionForm
        directions_directionForm_name: 'Наименование',
        directions_directionForm_nameAlt: 'Наименование на английски език',

        //declarations_editForm
        declarations_editForm_title: 'Преглед на декларация',
        declarations_editForm_edit: 'Редакция',
        declarations_editForm_publish: 'Публикуване',
        declarations_editForm_delete: 'Изтриване',
        declarations_editForm_draft: 'Чернова',
        declarations_editForm_draftConfirm:
          'Сигурни ли сте че искате да върнете декларацията в чернова?',
        declarations_editForm_archive: 'Архивирана',
        declarations_editForm_archiveConfirm:
          'Сигурни ли сте че искате да архивирате декларацията?',
        declarations_editForm_save: 'Запис',
        declarations_editForm_cancel: 'Отказ',

        //declarations_publishDeclarationModal
        declarations_publishDeclarationModal_title: 'Публикуване на декларация',
        declarations_publishDeclarationModal_publish: 'Публикуване',
        declarations_publishDeclarationModal_cancel: 'Отказ',
        declarations_publishDeclarationModal_activationDate: 'Дата на влизане в сила',

        //declarations_declarationDataForm
        declarations_declarationDataForm_name: 'Наименование',
        declarations_declarationDataForm_nameAlt: 'Наименование на английски език',
        declarations_declarationDataForm_nameMaxlength:
          'Заглавието може да съдържа максимум 200 символа',
        declarations_declarationDataForm_content: 'Съдържание',
        declarations_declarationDataForm_contentAlt: 'Съдържание на английски език',
        declarations_declarationDataForm_activationDate: 'Дата на влизане в сила',
        declarations_declarationDataForm_files: 'Файлове',
        declarations_declarationDataForm_noFiles: 'Няма',
        declarations_declarationDataForm_fileDescr: 'Описание',
        declarations_declarationDataForm_fileDescrMaxlength:
          'Описанието може да съдържа максимум 200 символа',
        declarations_declarationDataForm_file: 'Файл',
        declarations_declarationDataForm_status: 'Статус',
        declarations_declarationDataForm_createDate: 'Дата на създаване',
        declarations_declarationDataForm_createdByUser: 'Създал',

        //declarations_searchForm
        declarations_searchForm_newBtn: 'Нова декларация',
        declarations_searchForm_createDate: 'Дата на създаване',
        declarations_searchForm_status: 'Статус',
        declarations_searchForm_search: 'Търси',
        declarations_searchForm_activationDate: 'Дата на влизане в сила',
        declarations_searchForm_creator: 'Създал',
        declarations_searchForm_name: 'Наименование',

        //declarations_newForm
        declarations_newForm_title: 'Нова декларация',
        declarations_newForm_save: 'Запис',
        declarations_newForm_cancel: 'Отказ',

        //programmes_tabs
        programmes_tabs_programme: 'Основна организация',
        programmes_tabs_programmeData: 'Основни данни',
        programmes_tabs_programmePriorities: 'Разпоредители с бюджетни средства',
        programmes_tabs_documents: 'Документи',
        programmes_tabs_directions: 'Направления',

        //programmes_programmeDataForm
        programmes_programmeDataForm_status: 'Статус',
        programmes_programmeDataForm_code: 'Код',
        programmes_programmeDataForm_codeMaxLength: 'Полето може да е максимум 200 символа.',
        programmes_programmeDataForm_name: 'Наименование',
        programmes_programmeDataForm_nameAlt: 'Наименование на английски',
        programmes_programmeDataForm_description: 'Описание',
        programmes_programmeDataForm_descriptionAlt: 'Описание на английски',
        programmes_programmeDataForm_companyId: 'Организация',

        //programmes_newProgramme
        programmes_newProgramme_title: 'Създаване на основна организация',
        programmes_newProgramme_save: 'Запис',
        programmes_newProgramme_cancel: 'Отказ',

        //programmes_editProgrammeData
        programmes_editProgrammeData_title: 'Данни за основна организация',
        programmes_editProgrammeData_del: 'Изтриване',
        programmes_editProgrammeData_draft: 'Чернова',
        programmes_editProgrammeData_draftConfirm:
          'Сигурни ли сте че искате да върнете основната организация в статус чернова?',
        programmes_editProgrammeData_enter: 'Въведена',
        programmes_editProgrammeData_enterConfirm:
          'Сигурни ли сте че искате да въведете основната организация?',
        programmes_editProgrammeData_edit: 'Редакция',
        programmes_editProgrammeData_save: 'Запис',
        programmes_editProgrammeData_cancel: 'Отказ',

        //programmes_programmeInstitutionForm
        programmes_programmeInstitutionForm_contactName: 'Име',
        programmes_programmeInstitutionForm_contactPosition: 'Длъжност',
        programmes_programmeInstitutionForm_contactPhone: 'Телефон',
        programmes_programmeInstitutionForm_contactFax: 'Факс',
        programmes_programmeInstitutionForm_contactEmail: 'Електронен адрес',

        //mapNode_indicatorsSearch
        mapNode_indicatorsSearch_hasNoIndicatorsForAttach: 'Няма индикатори за присъединяване',

        //programmes_newProgrammeBudget
        programmes_newProgrammeBudget_amountHeader: 'Размер на финансиране',

        //programmes_programmeBudgetsSearch
        programme_programmeBudgetsSearch_eu: 'ЕС',
        programme_programmeBudgetsSearch_bg: 'НФ',
        programme_programmeBudgetsSearch_total: 'Общо',
        programme_programmeBudgetsSearch_budgetPeriod: 'Година',
        programme_programmeBudgetsSearch_main: 'ОП',
        programme_programmeBudgetsSearch_reserved: 'РП',
        programme_programmeBudgetsSearch_noAvailableBudgets: 'Няма намерени резултати',

        //programmes_documentsSearch
        programmes_documentsSearch_newBtn: 'Нов документ',
        programmes_documentsSearch_name: 'Наименование',
        programmes_documentsSearch_description: 'Описание',
        programmes_documentsSearch_file: 'Файл',

        //programmes_newProgrammeDocument
        programmes_newProgrammeDocument_title: 'Нов документ',
        programmes_newProgrammeDocument_save: 'Запис',
        programmes_newProgrammeDocument_cancel: 'Отказ',

        //programmes_editProgrammeDocument
        programmes_editProgrammeDocument_title: 'Редакция на документ',
        programmes_editProgrammeDocument_edit: 'Редакция',
        programmes_editProgrammeDocument_deleteDocument: 'Изтриване',
        programmes_editProgrammeDocument_save: 'Запис',
        programmes_editProgrammeDocument_cancel: 'Отказ',

        //programmes_applicationDocumentsSearch
        programmes_applicationDocumentsSearch_name: 'Наименование',
        programmes_applicationDocumentsSearch_active: 'Активен',
        programmes_applicationDocumentsSearch_extension: 'Разширение',
        programmes_applicationDocumentsSearch_isSignatureRequired: 'Електронен подпис',
        programmes_applicationDocumentsSearch_newBtn: 'Документи за кандидатстване',
        programmes_applicationDocumentsSearch_load: 'Зареди от външен файл',
        programmes_applicationDocumentsSearch_template: 'Свали шаблон',
        programmes_applicationDocumentsSearch_excelExport: 'Експорт',

        //programmes_newProgrammeApplicationDocument
        programmes_newProgrammeApplicationDocument_title: 'Нов документ от кандидата',
        programmes_newProgrammeApplicationDocument_save: 'Запис',
        programmes_newProgrammeApplicationDocument_cancel: 'Отказ',

        //programmes_programmeApplicationDocumentsLoad
        programmes_programmeApplicationDocumentsLoad_title: 'Зареждане на документи от външен файл',
        programmes_programmeApplicationDocumentsLoad_continue: 'Продължи',
        programmes_programmeApplicationDocumentsLoad_cancel: 'Отказ',
        programmes_programmeApplicationDocumentsLoad_file: 'Файл',

        //programmes_editProgrammeApplicationDocument
        programmes_editProgrammeApplicationDocument_title: 'Редакция на документ от кандидата',
        programmes_editProgrammeApplicationDocument_edit: 'Редакция',
        programmes_editProgrammeApplicationDocument_delete: 'Изтриване',
        programmes_editProgrammeApplicationDocument_save: 'Запис',
        programmes_editProgrammeApplicationDocument_cancel: 'Отказ',
        programmes_editProgrammeApplicationDocument_activate: 'Активиране',
        programmes_editProgrammeApplicationDocument_deactivate: 'Деактивиране',
        programmes_editProgrammeApplicationDocument_relatedProcedures: 'Бюджети',
        programmes_editProgrammeApplicationDocument_procedureCode: 'Код',
        programmes_editProgrammeApplicationDocument_procedureName: 'Бюджет',
        programmes_editProgrammeApplicationDocument_procedureStatus: 'Статус',
        programmes_editProgrammeApplicationDocument_procedureActivationDate: 'Дата на активиране',

        //programmes_programmeApplicationDocumentForm
        programmes_programmeApplicationDocumentForm_name: 'Тип на документа',
        programmes_programmeApplicationDocumentForm_isSignatureRequired: 'Електронен подпис',
        programmes_programmeApplicationDocumentForm_extension: 'Разширение',
        programmes_programmeApplicationDocumentForm_isActive: 'Активен',

        //programmes_programmePrioritiesSearch
        programmes_programmePrioritiesSearch_newBtn: 'Нов разпоредител с бюджетни средства',
        programmes_programmePrioritiesSearch_code: 'Код',
        programmes_programmePrioritiesSearch_name: 'Наименование',

        //programmes_modals_loadProgrammeDeclarationItems
        programmes_modals_loadProgrammeDeclarationItems_title: 'Зареждане на файл',
        programmes_modals_loadProgrammeDeclarationItems_continue: 'Продължи',
        programmes_modals_loadProgrammeDeclarationItems_cancel: 'Отказ',
        programmes_modals_loadProgrammeDeclarationItems_file: 'Файл',

        //programmePriorities_directionsSearch
        programmes_directionsSearch_new: 'Ново направление',
        programmes_directionsSearch_direction: 'Направление',
        programmes_directionsSearch_subDirection: 'Поднаправление',

        //programmePriorities_newProgrammePriorityDirection
        programmes_newProgrammeDirection_title: 'Новo направление към бюджетен разпоредител',
        programmes_newProgrammeDirection_save: 'Запис',
        programmes_newProgrammeDirection_cancel: 'Отказ',

        //programmePriorities_editProgrammePriorityDirection
        programmes_editProgrammeDirection_title: 'Редакция на направление',
        programmes_editProgrammeDirection_edit: 'Редакция',
        programmes_editProgrammeDirection_delete: 'Изтриване',
        programmes_editProgrammeDirection_save: 'Запис',
        programmes_editProgrammeDirection_cancel: 'Отказ',

        //programmes_programmePriorityDirectionForm
        programmes_programmeDirectionForm_direction: 'Направление',
        programmes_programmeDirectionForm_subDirection: 'Поднаправление',

        //programmePriorities_tabs
        programmePriorities_tabs_programme: 'Основна организация',

        programmePriorities_tabs_programmePriorityData: 'Основни данни',
        programmePriorities_tabs_indicators: 'Индикатори',

        programmePriorities_tabs_documents: 'Документи',
        programmePriorities_tabs_directions: 'Направления',

        //programmePriorities_programmePriorityDataForm
        programmePriorities_programmePriorityDataForm_status: 'Статус',
        programmePriorities_programmePriorityDataForm_code: 'Код',
        programmePriorities_programmePriorityDataForm_codePattern:
          'Невалиден формат. Кодът трябва да завършва с -{номер}',
        programmePriorities_programmePriorityDataForm_codeMaxLength:
          'Полето може да е максимум 200 символа.',
        programmePriorities_programmePriorityDataForm_type: 'Тип на бюджетния разпределител',
        programmePriorities_programmePriorityDataForm_name: 'Наименование',
        programmePriorities_programmePriorityDataForm_nameAlt: 'Наименование на английски',
        programmePriorities_programmePriorityDataForm_description: 'Описание',
        programmePriorities_programmePriorityDataForm_descriptionAlt: 'Описание на английски',
        programmePriorities_programmePriorityDataForm_company: 'Организация',
        programmePriorities_programmePriorityDataForm_higherOrder:
          'Към разпоредител от по-високо ниво',

        //programmePriorities_programmePriorityBudgetForm
        programmePriorities_programmePriorityBudgetForm_amount: 'Размер на финансиране',
        programmePriorities_programmePriorityBudgetForm_reservedAmount: 'Резервен план',
        programmePriorities_programmePriorityBudgetForm_nextThreeAmount: 'n + 3',

        //programmePriorities_newProgrammePriority
        programmePriorities_newProgrammePriority_title:
          'Създаване на разпоредител с бюджетни средства',
        programmePriorities_newProgrammePriority_save: 'Запис',
        programmePriorities_newProgrammePriority_cancel: 'Отказ',

        //programmePriorities_editProgrammePriorityData
        programmePriorities_editProgrammePriorityData_title:
          'Данни за Разпоредителя с бюджетни средства',
        programmePriorities_editProgrammePriorityData_edit: 'Редакция',
        programmePriorities_editProgrammePriorityData_draft: 'Чернова',
        programmePriorities_editProgrammePriorityData_draftConfirm:
          'Сигурни ли сте че искате да върнете Разпоредителя с бюджетни средства в статус чернова?',
        programmePriorities_editProgrammePriorityData_enter: 'Въведен',
        programmePriorities_editProgrammePriorityData_enterConfirm:
          'Сигурни ли сте че искате да въведете Разпоредителя с бюджетни средства?',
        programmePriorities_editProgrammePriorityData_del: 'Изтриване',
        programmePriorities_editProgrammePriorityData_save: 'Запис',
        programmePriorities_editProgrammePriorityData_cancel: 'Отказ',

        //programmes_pPriorityDirectionsSearch
        programmes_pPriorityDirectionsSearch_new: 'Ново направление',
        programmes_pPriorityDirectionsSearch_direction: 'Направление',
        programmes_pPriorityDirectionsSearch_subDirection: 'Поднаправление',

        //programmes_newPPriorityDirection
        programmes_newPPriorityDirection_title: 'Новo направление към бюджетен разпоредител',
        programmes_newPPriorityDirection_save: 'Запис',
        programmes_newPPriorityDirection_cancel: 'Отказ',

        //programmes_editPPriorityDirection
        programmes_editPPriorityDirection_title: 'Редакция на направление',
        programmes_editPPriorityDirection_edit: 'Редакция',
        programmes_editPPriorityDirection_delete: 'Изтриване',
        programmes_editPPriorityDirection_save: 'Запис',
        programmes_editPPriorityDirection_cancel: 'Отказ',

        //programmePriorities_pPriorityBudgetsSearch
        programmePriorities_pPriorityBudgetsSearch_new: 'Добавяне',
        programmePriorities_pPriorityBudgetsSearch_eu: 'ЕС',
        programmePriorities_pPriorityBudgetsSearch_bg: 'НФ',
        programmePriorities_pPriorityBudgetsSearch_total: 'Общо',
        programmePriorities_pPriorityBudgetsSearch_budgetPeriod: 'Година',
        programmePriorities_pPriorityBudgetsSearch_main: 'ОП',
        programmePriorities_pPriorityBudgetsSearch_reserved: 'РП',
        programmePriorities_pPriorityBudgetsSearch_noAvailableBudgets: 'Няма намерени резултати',

        //programmePriorities_editPPriorityBudget
        programmePriorities_editPPriorityBudget_title: 'Редакция на финансиране',
        programmePriorities_editPPriorityBudget_edit: 'Редакция',
        programmePriorities_editPPriorityBudget_save: 'Запис',
        programmePriorities_editPPriorityBudget_cancel: 'Отказ',
        programmePriorities_editPPriorityBudget_deleteBudget: 'Изтриване',
        programmePriorities_editPPriorityBudget_budgetPeriod: 'Година',
        programmePriorities_editPPriorityBudget_missingBudgetError:
          'Трябва да бъде въведено финансиране поне за един от фондовете.',

        //programmePriorities_newPPriorityBudget
        programmePriorities_newPPriorityBudget_title: 'Ново финансиране',
        programmePriorities_newPPriorityBudget_save: 'Запис',
        programmePriorities_newPPriorityBudget_cancel: 'Отказ',
        programmePriorities_newPPriorityBudget_budgetPeriod: 'Година',

        //programmePriorities_documentsSearch
        programmePriorities_documentsSearch_newBtn: 'Нов документ',
        programmePriorities_documentsSearch_name: 'Наименование',
        programmePriorities_documentsSearch_description: 'Описание',
        programmePriorities_documentsSearch_file: 'Файл',

        //programmePriorities_newProgrammePriorityDocument
        programmePriorities_newProgrammePriorityDocument_title: 'Нов документ',
        programmePriorities_newProgrammePriorityDocument_save: 'Запис',
        programmePriorities_newProgrammePriorityDocument_cancel: 'Отказ',

        //programmePriorities_editProgrammePriorityDocument
        programmePriorities_editProgrammePriorityDocument_title: 'Редакция на документ',
        programmePriorities_editProgrammePriorityDocument_edit: 'Редакция',
        programmePriorities_editProgrammePriorityDocument_deleteDocument: 'Изтриване',
        programmePriorities_editProgrammePriorityDocument_save: 'Запис',
        programmePriorities_editProgrammePriorityDocument_cancel: 'Отказ',

        //programmes_declarationsSearch
        programmes_declarationsSearch_title: 'Е-декларации при кандидатстване',
        programmes_declarationsSearch_name: 'Наименование',
        programmes_declarationsSearch_nameAlt: 'Наименование на английски език',
        programmes_declarationsSearch_active: 'Активен',
        programmes_declarationsSearch_orderNum: 'Пореден номер',

        //programmes_newProgrammeDeclaration
        programmes_newProgrammeDeclaration_title: 'Новa декларация',
        programmes_newProgrammeDeclaration_save: 'Запис',
        programmes_newProgrammeDeclaration_cancel: 'Отказ',

        //programmes_editProgrammeDeclaration
        programmes_editProgrammeDeclaration_title: 'Редакция на декларация',
        programmes_editProgrammeDeclaration_edit: 'Редакция',
        programmes_editProgrammeDeclaration_delete: 'Изтриване',
        programmes_editProgrammeDeclaration_save: 'Запис',
        programmes_editProgrammeDeclaration_cancel: 'Отказ',
        programmes_editProgrammeDeclaration_activate: 'Активиране',
        programmes_editProgrammeDeclaration_deactivate: 'Деактивиране',
        programmes_editProgrammeDeclaration_relatedProcedures: 'Процедури',
        programmes_editProgrammeDeclaration_procedureCode: 'Код',
        programmes_editProgrammeDeclaration_procedureName: 'Процедура',
        programmes_editProgrammeDeclaration_procedureStatus: 'Статус',
        programmes_editProgrammeDeclaration_procedureActivationDate: 'Дата на активиране',
        programmes_editProgrammeDeclaration_items_new: 'Нов ред',
        programmes_editProgrammeDeclaration_items_orderNum: '№',
        programmes_editProgrammeDeclaration_items_content: 'Съдържание',
        programmes_editProgrammeDeclaration_items_isActive: 'Активен',
        programmes_editProgrammeDeclaration_items_loadItems: 'Зареди от Excel',
        programmes_editProgrammeDeclaration_items_template: 'Свали шаблон',

        //programmes_programmeDeclarationForm
        programmes_programmeDeclarationForm_name: 'Наименование',
        programmes_programmeDeclarationForm_nameAlt: 'Наименование на английски език',
        programmes_programmeDeclarationForm_content: 'Съдържание',
        programmes_programmeDeclarationForm_contentAlt: 'Съдържание на английски език',
        programmes_programmeDeclarationForm_isActive: 'Активен',
        programmes_programmeDeclarationForm_fieldType: 'Тип на полето за попълване',
        programmes_programmeDeclarationForm_isConsentForNSIDataProviding:
          'Декларацията е за съгласие данните на кандидата да бъдат предоставени от НСИ',
        programmes_programmeDeclarationForm_orderNum: 'Пореден номер',

        //programmes_newProgrammeDeclarationItem
        programmes_newProgrammeDeclarationItem_title: 'Нов ред',
        programmes_newProgrammeDeclarationItem_save: 'Запис',
        programmes_newProgrammeDeclarationItem_cancel: 'Отказ',

        //programmes_editProgrammeDeclarationItem
        programmes_editProgrammeDeclarationItem_title: 'Редакция на ред',
        programmes_editProgrammeDeclarationItem_edit: 'Редакция',
        programmes_editProgrammeDeclarationItem_delete: 'Изтриване',
        programmes_editProgrammeDeclarationItem_save: 'Запис',
        programmes_editProgrammeDeclarationItem_cancel: 'Отказ',
        programmes_editProgrammeDeclarationItem_activate: 'Активиране',
        programmes_editProgrammeDeclarationItem_deactivate: 'Деактивиране',

        //programmes_programmeDeclarationItemForm
        programmes_programmeDeclarationItemForm_orderNum: 'Ред',
        programmes_programmeDeclarationItemForm_content: 'Съдържание',
        programmes_programmeDeclarationItemForm_isActive: 'Активен',
        programmes_programmeDeclarationItemForm_contentMaxLength:
          'Полето може да съдържа максимум 100 символа',

        //measures_measureForm
        measures_measureForm_shortName: 'Кратко име',
        measures_measureForm_name: 'Име',
        measures_measureForm_nameAlt: 'Име на английски',

        //measures_searchForm
        measures_searchForm_title: 'Мерни единици',
        measures_searchForm_newBtn: 'Нова мерна единица',
        measures_searchForm_shortName: 'Кратко име',
        measures_searchForm_name: 'Име',
        measures_searchForm_nameAlt: 'Име на английски',

        //measures_newForm
        measures_newForm_title: 'Нова мерна единица',
        measures_newForm_save: 'Запис',
        measures_newForm_cancel: 'Отказ',

        //measures_editForm
        measures_editForm_title: 'Редакция на мерна единица',
        measures_editForm_save: 'Запис',
        measures_editForm_cancel: 'Отказ',
        measures_editForm_edit: 'Редакция',
        measures_editForm_del: 'Изтриване',

        //operationalMap
        operationalMap_newProgramme: 'Нова основна организация',
        operationalMap_view: 'Преглед',
        operationalMap_belongTo: 'към',

        //proceduresMap
        procedures_new: 'Нов бюджет',
        procedures_copy: 'Копирай',
        procedures_search: 'Бюджети',
        procedures_view: 'Преглед',

        //procedures_tabs
        procedures_tabs_procedureData: 'Основни данни',
        procedures_tabs_applicableSections: 'Приложими секции',
        procedures_tabs_indicators: 'Индикатори',
        procedures_tabs_shares: 'Източници на финансиране',
        procedures_tabs_timeLimits: 'Срокове',
        procedures_tabs_expenseBudgets: 'Бюджет',
        procedures_tabs_specFields: 'Допълнителни полета',
        procedures_tabs_documents: 'Документи',
        procedures_tabs_reportDocuments: 'Отчетни документи',
        procedures_tabs_directions: 'Направления',
        procedures_tabs_monitorstat: 'Мониторстат',

        //procedures_procedureDataForm
        procedures_procedureDataForm_procedureKind: 'Тип',
        procedures_procedureDataForm_procedureYear: 'Година',
        procedures_procedureDataForm_procedureStatus: 'Статус',
        procedures_procedureDataForm_аctivationDate: 'Дата на активиране',
        procedures_procedureDataForm_code: 'Код',
        procedures_procedureDataForm_name: 'Наименование',
        procedures_procedureDataForm_nameAlt: 'Наименование на английски език',
        procedures_procedureDataForm_description: 'Описание/Цели',
        procedures_procedureDataForm_descriptionAlt: 'Описание/Цели на английски език',
        procedures_procedureDataForm_project: 'Изисквания за проектно предложение',
        procedures_procedureDataForm_allowedRegistrationType: 'Вид на подаването',
        procedures_procedureDataForm_projectMinAmount: 'Мин. стойност БФП/ФИ',
        procedures_procedureDataForm_projectMinAmountInfo: 'Информация',
        procedures_procedureDataForm_projectMinAmountInfoAlt: 'Информация на английски език',
        procedures_procedureDataForm_projectMaxAmount: 'Макс. стойност БФП/ФИ',
        procedures_procedureDataForm_projectMaxAmountInfo: 'Информация',
        procedures_procedureDataForm_projectMaxAmountInfoAlt: 'Информация на английски език',
        procedures_procedureDataForm_projectDuration: 'Максимална продължителност (в месеци)',
        procedures_procedureDataForm_maxValueMustBeGraterThanMinValue:
          '"Макс. стойност БФП/ФИ" трябва да е по-голяма или равна на "Мин. стойност БФП/ФИ"',

        //procedures_procedureLocationForm
        procedures_procedureLocationForm_location: 'Местоположение',
        procedures_procedureLocationForm_nutsLevel: 'Ниво',
        procedures_procedureLocationForm_countryId: 'Държава',
        procedures_procedureLocationForm_nuts1Id: 'NUTS ниво 1',
        procedures_procedureLocationForm_nuts2Id: 'NUTS ниво 2',
        procedures_procedureLocationForm_districtId: 'Област',
        procedures_procedureLocationForm_municipalityId: 'Община',
        procedures_procedureLocationForm_settlementId: 'Населено място',
        procedures_procedureLocationForm_protectedZoneId: 'Защитена зона',

        //procedures_procedureDirectionForm
        procedures_procedureDirectionForm_direction: 'Направление',
        procedures_procedureDirectionForm_subDirection: 'Поднаправление',
        procedures_procedureDirectionForm_amount: 'Сума',

        //procedures_newProcedureLocation
        procedures_newProcedureLocation_title: 'Ново местоположение',
        procedures_newProcedureLocation_save: 'Запис',
        procedures_newProcedureLocation_cancel: 'Отказ',

        //procedures_editProcedureLocation
        procedures_editProcedureLocation_title: 'Редакция на местоположение',
        procedures_editProcedureLocation_save: 'Запис',
        procedures_editProcedureLocation_delete: 'Изтриване',
        procedures_editProcedureLocation_cancel: 'Отказ',

        //procedures_searchMonitorstatDocument
        procedures_searchMonitorstatDocument_title: 'Мониторстат документи',
        procedures_searchMonitorstatDocument_add: 'Добави документ',
        procedures_searchMonitorstatDocument_year: 'Година',
        procedures_searchMonitorstatDocument_survey: 'Отчет',
        procedures_searchMonitorstatDocument_report: 'Справка',
        procedures_searchMonitorstatDocument_status: 'Статус',
        procedures_searchMonitorstatDocument_requestTitle: 'Заявки за изследване към Мониторстат',
        procedures_searchMonitorstatDocument_addRequest: 'Добави заявка',
        procedures_searchMonitorstatDocument_requestStatus: 'Статус',
        procedures_searchMonitorstatDocument_executionStartDate: 'Начална дата',
        procedures_searchMonitorstatDocument_executionEndDate: 'Крайна дата',

        //procedures_editProcedureMonitorstatRequest
        procedures_editProcedureMonitorstatRequest_title:
          'Редакция на заявка за изследване към Мониторстат',
        procedures_editProcedureMonitorstatRequest_edit: 'Редакция',
        procedures_editProcedureMonitorstatRequest_save: 'Запис',
        procedures_editProcedureMonitorstatRequest_send: 'Изпрати',
        procedures_editProcedureMonitorstatRequest_cancel: 'Отказ',
        procedures_editProcedureMonitorstatRequest_sendMessage:
          'Изпращане на заявката за изследване към Мониторстат?',

        //procedures_procedureMonitorstatRequest
        procedures_procedureMonitorstatRequest_title: 'Заявка за изследване',
        procedures_procedureMonitorstatRequest_startDate: 'Начална дата',
        procedures_procedureMonitorstatRequest_endDate: 'Крайна дата',
        procedures_procedureMonitorstatRequest_status: 'Статус',

        //procedures_newProcedureMonitorstatRequest
        procedures_newProcedureMonitorstatRequest_title:
          'Нова за заявка за изследване към Мониторстат',
        procedures_newProcedureMonitorstatRequest_save: 'Запис',
        procedures_newProcedureMonitorstatRequest_cancel: 'Отказ',

        //procedures_searchMonitorstatEconomicActivities
        procedures_searchMonitorstatEconomicActivities_title: 'Икономически дейности',
        procedures_searchMonitorstatEconomicActivities_year: 'Година',
        procedures_searchMonitorstatEconomicActivities_type: 'Тип',
        procedures_searchMonitorstatEconomicActivities_createDate: 'Дата на създаване',
        procedures_searchMonitorstatEconomicActivities_status: 'Статус',
        procedures_searchMonitorstatEconomicActivities_new: 'Добави дейност',

        //procedures_newProcedureMonitorsatEconomicActivity
        procedures_newProcedureMonitorsatEconomicActivity_title: 'Нова икономическа дейност',
        procedures_newProcedureMonitorsatEconomicActivity_save: 'Запис',
        procedures_newProcedureMonitorsatEconomicActivity_cancel: 'Отказ',
        procedures_newProcedureMonitorsatEconomicActivity_year: 'Година',
        procedures_newProcedureMonitorsatEconomicActivity_type: 'Тип',

        //procedures_proceduresSearch
        procedures_proceduresSearch_search: 'Търси',
        procedures_proceduresSearch_code: 'Код',
        procedures_proceduresSearch_name: 'Наименование',
        procedures_proceduresSearch_programme: 'Основна организация',
        procedures_proceduresSearch_programmePriority: 'Разпоредител с бюджетни средства',
        procedures_proceduresSearch_activationDate: 'Дата на активиране',
        procedures_proceduresSearch_endingDate: 'Краен срок за кандидатстване',
        procedures_proceduresSearch_status: 'Статус',
        procedures_proceduresSearch_euAmount: 'Фин. от ЕС',
        procedures_proceduresSearch_bgAmount: 'Фин. от НФ',
        procedures_proceduresSearch_excelExport: 'Експорт',

        //procedures_editProcedureData
        procedures_editProcedureData_title: 'Данни за бюджет',
        procedures_editProcedureData_edit: 'Редакция',
        procedures_editProcedureData_save: 'Запис',
        procedures_editProcedureData_cancel: 'Отказ',
        procedures_editProcedureData_draft: 'Чернова',
        procedures_editProcedureData_entered: 'Въведен',
        procedures_editProcedureData_checked: 'Проверен',
        procedures_editProcedureData_active: 'Активен',
        procedures_editProcedureData_ended: 'Приключен',
        procedures_editProcedureData_terminated: 'Прекратен',
        procedures_editProcedureData_canceled: 'Анулиран',
        procedures_editProcedureData_changeStatusConfirm:
          'Сигурни ли сте, че искате да промените статуса на бюджета на {{status}}?',
        procedures_editProcedureData_locationTitle: 'Местоположение',
        procedures_editProcedureData_locationNutsLevel: 'Ниво',
        procedures_editProcedureData_locationFullPath: 'Местоположение',
        procedures_editProcedureData_location_new: 'Ново местоположение',

        //proceduresMap_newForm
        procedures_newForm_title: 'Нов бюджет',
        procedures_newForm_save: 'Запис',
        procedures_newForm_cancel: 'Отказ',
        procedures_newForm_primaryShare: 'Основен източник на финансиране',

        //procedures_procedureShare_search
        procedures_procedureShare_search_title: 'Източници на финансиране',
        procedures_procedureShare_search_programme: 'Основна организация',
        procedures_procedureShare_search_programmePriority: 'Разпоредител с бюджетни средства',
        procedures_procedureShare_search_bgAmount: 'Фин. от НФ',
        procedures_procedureShare_search_isPrimary: 'Основно фин.',

        //procedures_procedureShare_new
        procedures_procedureShare_new_title: 'Нов източник на финансиране',
        procedures_procedureShare_new_save: 'Запис',
        procedures_procedureShare_new_cancel: 'Отказ',
        procedures_procedureShare_new_msg_duplicate_number:
          'Вече е създадена бюджет с този код, следващият по поредност код е зареден в полето. Моля натиснете бутона запис отново.',

        //procedures_procedureShare_edit
        procedures_procedureShare_edit_title: 'Редакция на източник на финансиране',
        procedures_procedureShare_edit_edit: 'Редакция',
        procedures_procedureShare_edit_delete: 'Изтриване',
        procedures_procedureShare_edit_save: 'Запис',
        procedures_procedureShare_edit_cancel: 'Отказ',
        procedures_procedureShare_edit_msg_unableToDeletePrimaryShare:
          'Първичният източник на финансиране не може да се изтрива.',
        procedures_procedureShare_edit_msg_unableToDeleteLinedBudgedShare:
          'Източник на финансиране не може да бъде изтрит, защото към него вече има свързан бюджет.',

        //procedures_procedureShareForm
        procedures_procedureShareForm_programme: 'Основна организация',
        procedures_procedureShareForm_programmePriority: 'Разпоредител с бюджетни средства',
        procedures_procedureShareForm_financeSource: 'Финансов източник',
        procedures_procedureShareForm_amountHeader: 'Размер на финансиране',

        //procedures_procedureTimeLimitForm
        procedures_procedureTimeLimitForm_endDateTime: 'Крайна дата и час за кандидатстване',
        procedures_procedureTimeLimitForm_endDate: 'Дата',
        procedures_procedureTimeLimitForm_endTime: 'Час',
        procedures_procedureTimeLimitForm_notes: 'Информация',
        procedures_procedureTimeLimitForm_notValidEndTime:
          'Съществува/т срок/ове към бюджетта, с дата и час по-големи от въведените.',

        //procedures_procedureTimeLimit_search
        procedures_procedureTimeLimit_search_new: 'Нов срок',
        procedures_procedureTimeLimit_search_endDate: 'Крайна дата за кандидатстване',
        procedures_procedureTimeLimit_search_notes: 'Информация',

        //procedures_procedureTimeLimit_new
        procedures_procedureTimeLimit_new_title: 'Нов срок',
        procedures_procedureTimeLimit_new_save: 'Запис',
        procedures_procedureTimeLimit_new_cancel: 'Отказ',

        //procedures_procedureTimeLimit_edit
        procedures_procedureTimeLimit_edit_viewTitle: 'Преглед на срок',
        procedures_procedureTimeLimit_edit_editTitle: 'Редакция на срок',
        procedures_procedureTimeLimit_edit_edit: 'Редакция',
        procedures_procedureTimeLimit_edit_save: 'Запис',
        procedures_procedureTimeLimit_edit_cancel: 'Отказ',
        procedures_procedureTimeLimit_edit_delete: 'Изтриване',
        procedures_procedureTimeLimit_edit_ok: 'OK',

        //procedures_modals_budgetLevel1Modal
        procedures_modals_budgetLevel1Modal_title: 'Нов разход',
        procedures_modals_budgetLevel1Modal_expenseType: 'Тип разход',
        procedures_modals_budgetLevel1Modal_save: 'Запис',
        procedures_modals_budgetLevel1Modal_cancel: 'Отказ',

        //procedures_modals_budgetLevel2Modal
        procedures_modals_budgetLevel2Modal_newTitle: 'Нов подразход',
        procedures_modals_budgetLevel2Modal_editTitle: 'Редакция на подразход',
        procedures_modals_budgetLevel2Modal_previewTitle: 'Преглед на подразход',
        procedures_modals_budgetLevel2Modal_name: 'Описание на подразход',
        procedures_modals_budgetLevel2Modal_nameAlt: 'Описание на подразход на английски',
        procedures_modals_budgetLevel2Modal_aidMode: 'Режим на помощта',
        procedures_modals_budgetLevel2Modal_save: 'Запис',
        procedures_modals_budgetLevel2Modal_cancel: 'Отказ',
        procedures_modals_budgetLevel2Modal_back: 'Назад',

        //procedures_modals_budgetLevel3Modal
        procedures_modals_budgetLevel3Modal_newTitle: 'Нов детайл на подразход',
        procedures_modals_budgetLevel3Modal_editTitle: 'Редакция на детайл на подразход',
        procedures_modals_budgetLevel3Modal_note: 'Описание',
        procedures_modals_budgetLevel3Modal_save: 'Запис',
        procedures_modals_budgetLevel3Modal_cancel: 'Отказ',

        //procedures_modals_budgetValidationRuleModall
        procedures_modals_budgetValidationRuleModal_newTitle: 'Ново валидационно правило',
        procedures_modals_budgetValidationRuleModal_editTitle: 'Редакция на валидационно правило',
        procedures_modals_budgetValidationRuleModal_message: 'Съобщение',
        procedures_modals_budgetValidationRuleModal_condition: 'Условие',
        procedures_modals_budgetValidationRuleModal_rule: 'Проверка',
        procedures_modals_budgetValidationRuleModal_save: 'Запис',
        procedures_modals_budgetValidationRuleModal_cancel: 'Отказ',

        //procedures_modals_chooseProcedureModal
        procedures_modals_chooseProcedureModal_title: 'Копиране на бюджет',
        procedures_modals_chooseProcedureModal_continue: 'Продължи',
        procedures_modals_chooseProcedureModal_cancel: 'Отказ',
        procedures_modals_chooseProcedureModal_users: 'Бюджет',

        //procedures_modals_chooseMonitorstatReport
        procedures_modals_chooseMonitorstatReport_title: 'Избор на справки от Мониторстат',
        procedures_modals_chooseMonitorstatReport_continue: 'Продължи',
        procedures_modals_chooseMonitorstatReport_cancel: 'Отказ',
        procedures_modals_chooseMonitorstatReport_year: 'Година',
        procedures_modals_chooseMonitorstatReport_survey: 'Отчет',
        procedures_modals_chooseMonitorstatReport_search: 'Търсене',
        procedures_modals_chooseMonitorstatReport_all: 'Всички',
        procedures_modals_chooseMonitorstatReport_reportYear: 'Година',
        procedures_modals_chooseMonitorstatReport_reportSurvey: 'Отчет',
        procedures_modals_chooseMonitorstatReport_reportName: 'Справка',
        procedures_modals_chooseMonitorstatReport_cannotAddReport:
          'Справката е вече добавена и не може да се добави отново',

        //procedures_chooseDirectionsModal
        procedures_chooseDirectionsModal_title: 'Избор на направления',
        procedures_chooseDirectionsModal_continue: 'Продължи',
        procedures_chooseDirectionsModal_cancel: 'Отказ',
        procedures_chooseDirectionsModal_all: 'Всички',
        procedures_chooseDirectionsModal_direction: 'Направление',
        procedures_chooseDirectionsModal_subDirection: 'Поднаправление',

        //procedures_expenseBudgets_view
        procedures_expenseBudgets_view_expense: 'Разход',
        procedures_expenseBudgets_view_prrogrammePrefix: 'ОП: ',
        procedures_expenseBudgets_view_programmePriority: 'Разпоредител с бюджетни средства',
        procedures_expenseBudgets_view_aidMode: 'РП',
        procedures_expenseBudgets_view_deminimis: 'dM',
        procedures_expenseBudgets_view_stateAid: 'ДП',
        procedures_expenseBudgets_view_notApplicable: 'n/a',
        procedures_expenseBudgets_view_isEligibleCost: 'Доп.',
        procedures_expenseBudgets_view_add: 'добави',
        procedures_expenseBudgets_view_preview: 'преглед',
        procedures_expenseBudgets_view_delete: 'изтрий',
        procedures_expenseBudgets_view_edit: 'редактирай',
        procedures_expenseBudgets_view_deactivate: 'деактивирай',
        procedures_expenseBudgets_view_activate: 'активирай',
        procedures_expenseBudgets_view_message: 'Съобщение',
        procedures_expenseBudgets_view_condition: 'Условие',
        procedures_expenseBudgets_view_rule: 'Проверка',

        //procedures_indicatorsSearch
        procedures_indicatorsSearch_measureName: 'Мерна единица',
        procedures_indicatorsSearch_name: 'Индикатор',
        procedures_indicatorsSearch_programmeName: 'ОП',
        procedures_indicatorsSearch_hasGenderDivision: 'Разделение по пол',
        procedures_indicatorsSearch_baseTotalValue: 'Базова стойност',
        procedures_indicatorsSearch_baseYear: 'Базова година',
        procedures_indicatorsSearch_targetTotalValue: 'Целева стойност',
        procedures_indicatorsSearch_milestoneTargetTotalValue: 'Етапна цел',
        procedures_indicatorsSearch_dataSource: 'Източник на данните',
        procedures_indicatorsSearch_attach: 'Присъединяване',
        procedures_indicatorsSearch_new: 'Нов индивидуален индикатор',
        procedures_indicatorsSearch_status: 'Статус',

        //procedures_editProcedureIndicator
        procedures_editProcedureIndicator_title: 'Редакция на индикатор',
        procedures_editProcedureIndicator_deactivate: 'Деактивирай',
        procedures_editProcedureIndicator_activate: 'Активирай',
        procedures_editProcedureIndicator_edit: 'Редакция',
        procedures_editProcedureIndicator_save: 'Запис',
        procedures_editProcedureIndicator_cancel: 'Отказ',
        procedures_editProcedureIndicator_deleteIndicator: 'Изтриване',

        //procedures_attachProcedureIndicator
        procedures_attachProcedureIndicator_title: 'Нов индикатор',
        procedures_attachProcedureIndicator_name: 'Име на индикатора',
        procedures_attachProcedureIndicator_save: 'Запис',
        procedures_attachProcedureIndicator_cancel: 'Отказ',

        //procedures_newProcedureIndicator
        procedures_newProcedureIndicator_title: 'Нов индивидуален индикатор',
        procedures_newProcedureIndicator_save: 'Запис',
        procedures_newProcedureIndicator_cancel: 'Отказ',

        //procedures_appGuidelinesSearch
        procedures_appGuidelinesSearch_name: 'Наименование',
        procedures_appGuidelinesSearch_description: 'Описание',
        procedures_appGuidelinesSearch_file: 'Файл',
        procedures_appGuidelinesSearch_newBtn: 'Нова насока',

        //procedures_newProcedureAppGuideline
        procedures_newProcedureAppGuideline_title: 'Нова насока',
        procedures_newProcedureAppGuideline_name: 'Наименование',
        procedures_newProcedureAppGuideline_file: 'Файл',
        procedures_newProcedureAppGuideline_description: 'Описание',
        procedures_newProcedureAppGuideline_save: 'Запис',
        procedures_newProcedureAppGuideline_cancel: 'Отказ',

        //procedures_editProcedureAppGuideline
        procedures_editProcedureAppGuideline_title: 'Редакция на насока',
        procedures_editProcedureAppGuideline_edit: 'Редакция',
        procedures_editProcedureAppGuideline_delete: 'Изтриване',
        procedures_editProcedureAppGuideline_save: 'Запис',
        procedures_editProcedureAppGuideline_cancel: 'Отказ',
        procedures_editProcedureAppGuideline_name: 'Наименование',
        procedures_editProcedureAppGuideline_file: 'Файл',
        procedures_editProcedureAppGuideline_description: 'Описание',

        //procedures_appSectionsSearch
        procedures_appSectionsSearch_title: 'Допустими секции',
        procedures_appSectionsSearch_edit: 'Редакция',
        procedures_appSectionsSearch_save: 'Запис',
        procedures_appSectionsSearch_cancel: 'Отказ',
        procedures_appSectionsSearch_isSelected: 'Използване',
        procedures_appSectionsSearch_section: 'Секция',
        procedures_appSectionsSearch_additionalSettings: 'Допълнителни настройки',

        //procedures_appDocumentsSearch
        procedures_appDocsSearch_name: 'Тип документ',
        procedures_appDocsSearch_extension: 'Разрешени разширения',
        procedures_appDocsSearch_isRequired: 'Задължителен',
        procedures_appDocsSearch_isSignatureRequired: 'Електронен подпис',
        procedures_appDocsSearch_status: 'Статус',

        //procedures_procedureAppDocumentForm
        procedures_procedureAppDocumentForm_name: 'Тип документ',
        procedures_procedureAppDocumentForm_extension: 'Разрешени разширения (.pdf, .jpg, ...)',
        procedures_procedureAppDocumentForm_isRequired: 'Задължителен',
        procedures_procedureAppDocumentForm_isSignatureRequired: 'Електронен подпис',
        procedures_procedureAppDocumentForm_status: 'Статус',

        //procedures_newProcedureAppDocument
        procedures_newProcedureAppDocument_title: 'Нов документ за подаване',
        procedures_newProcedureAppDocument_save: 'Запис',
        procedures_newProcedureAppDocument_cancel: 'Отказ',

        //procedures_editProcedureAppDocument
        procedures_editProcedureAppDocument_title: 'Редакция на документ за подаване',
        procedures_editProcedureAppDocument_deactivate: 'Деактивирай',
        procedures_editProcedureAppDocument_activate: 'Активирай',
        procedures_editProcedureAppDocument_edit: 'Редакция',
        procedures_editProcedureAppDocument_delete: 'Изтриване',
        procedures_editProcedureAppDocument_save: 'Запис',
        procedures_editProcedureAppDocument_cancel: 'Отказ',

        //procedures_procedureSpecFieldForm
        procedures_procedureSpecFieldForm_title: 'Наименование',
        procedures_procedureSpecFieldForm_titleAlt: 'Наименование на английски език',
        procedures_procedureSpecFieldForm_description: 'Описание',
        procedures_procedureSpecFieldForm_descriptionAlt: 'Описание на английски език',
        procedures_procedureSpecFieldForm_isRequired: 'Задължително',
        procedures_procedureSpecFieldForm_maxLength: 'Максимална дължина',
        procedures_procedureSpecFieldForm_status: 'Статус',

        //procedures_procedureSpecField_search
        procedures_procedureSpecField_search_new: 'Ново допълнително поле',
        procedures_procedureSpecField_search_title: 'Наименование',
        procedures_procedureSpecField_search_description: 'Описание',
        procedures_procedureSpecField_search_isRequired: 'Задължително',
        procedures_procedureSpecField_search_status: 'Статус',

        //procedures_procedureSpecField_new
        procedures_procedureSpecField_new_title: 'Ново допълнително поле',
        procedures_procedureSpecField_new_save: 'Запис',
        procedures_procedureSpecField_new_cancel: 'Отказ',

        //procedures_procedureSpecField_edit
        procedures_procedureSpecField_edit_title: 'Редакция на допълнително поле',
        procedures_procedureSpecField_edit_deactivate: 'Деактивирай',
        procedures_procedureSpecField_edit_activate: 'Активирай',
        procedures_procedureSpecField_edit_edit: 'Редакция',
        procedures_procedureSpecField_edit_save: 'Запис',
        procedures_procedureSpecField_edit_cancel: 'Отказ',
        procedures_procedureSpecField_edit_delete: 'Изтриване',

        //procedures_procedureDirection
        procedures_procedureDirection_search_attach: 'Избери',
        procedures_procedureDirection_search_direction: 'Направление',
        procedures_procedureDirection_search_subDirection: 'Поднаправление',
        procedures_procedureDirection_search_amount: 'Сума',

        //procedures_editProcedureDirection
        procedures_editProcedureDirection_title: 'Редакция на сума по направление',
        procedures_editProcedureDirection_edit: 'Редакция',
        procedures_editProcedureDirection_delete: 'Изтриване',
        procedures_editProcedureDirection_save: 'Запис',
        procedures_editProcedureDirection_cancel: 'Отказ',

        //procedures_documentsSearch
        procedures_documentsSearch_name: 'Наименование',
        procedures_documentsSearch_description: 'Описание',
        procedures_documentsSearch_file: 'Файл',

        //procedures_newProcedureDocument
        procedures_newProcedureDocument_title: 'Нов вътрешен документ',
        procedures_newProcedureDocument_save: 'Запис',
        procedures_newProcedureDocument_cancel: 'Отказ',

        //procedures_editProcedureDocument
        procedures_editProcedureDocument_title: 'Редакция на вътрешен документ',
        procedures_editProcedureDocument_edit: 'Редакция',
        procedures_editProcedureDocument_deleteDocument: 'Изтриване',
        procedures_editProcedureDocument_save: 'Запис',
        procedures_editProcedureDocument_cancel: 'Отказ',

        //procedures_procedureEvalTableForm
        procedures_procedureEvalTableForm_name: 'Наименование',
        procedures_procedureEvalTableForm_type: 'Етап на оценка',
        procedures_procedureEvalTableForm_evalType: 'Тип на оценителния лист',
        procedures_procedureEvalTableForm_status: 'Статус',
        procedures_procedureEvalTableForm_evalTableStatus: 'Статус на въвеждане',
        procedures_procedureEvalTableForm_template: 'Оценителна таблица',

        //procedures_evalTablesSearch
        procedures_evalTablesSearch_name: 'Наименование',
        procedures_evalTablesSearch_type: 'Тип',
        procedures_evalTablesSearch_preview: 'Преглед',
        procedures_evalTablesSearch_status: 'Статус',

        //procedures_newEvalTable
        procedures_newEvalTable_title: 'Нова оценителна таблица',
        procedures_newEvalTable_save: 'Запис',
        procedures_newEvalTable_cancel: 'Отказ',

        //procedures_editEvalTable
        procedures_editEvalTable_title: 'Редакция на оценителна таблица',
        procedures_editEvalTable_deactivate: 'Деактивирай',
        procedures_editEvalTable_activate: 'Активирай',
        procedures_editEvalTable_edit: 'Редакция',
        procedures_editEvalTable_delete: 'Изтриване',
        procedures_editEvalTable_save: 'Запис',
        procedures_editEvalTable_cancel: 'Отказ',

        //procedures_questionsSearch
        procedures_questionsSearch_createDate: 'Дата на добавяне',
        procedures_questionsSearch_createdByUser: 'Добавил',
        procedures_questionsSearch_status: 'Статус',
        procedures_questionsSearch_file: 'Файл',

        //procedures_procedureQuestionForm
        procedures_procedureQuestionForm_createDate: 'Дата на добавяне',
        procedures_procedureQuestionForm_createdByUser: 'Добавил',
        procedures_procedureQuestionForm_file: 'Файл',
        procedures_procedureQuestionForm_status: 'Статус',

        //procedures_newProcedureQuestion
        procedures_newProcedureQuestion_title: 'Нов въпрос/разяснение',
        procedures_newProcedureQuestion_save: 'Запис',
        procedures_newProcedureQuestion_cancel: 'Отказ',

        //procedures_editProcedureQuestion
        procedures_editProcedureQuestion_title: 'Редакция на разяснения',
        procedures_editProcedureQuestion_ok: 'ОК',
        procedures_editProcedureQuestion_deleteQuestion: 'Изтриване',

        //procedures_allDocumentsSearch
        procedures_allDocumentsSearch_documents: 'Вътрешни документи',
        procedures_allDocumentsSearch_appGuidelines: 'Насоки за кандидатстване',
        procedures_allDocumentsSearch_appDocs: 'Документи за подаване',
        procedures_allDocumentsSearch_evalTables: 'Оценителни таблици',

        procedures_allDocumentsSearch_applicaiton: 'Формуляр за кандидатстване',
        procedures_allDocumentsSearch_questions: 'Обобщени въпроси и отговори',
        procedures_allDocumentsSearch_declarations: 'Е-декларации',

        //procedures_technicalReportDocumentsSearch
        procedures_technicalReportDocumentsSearch_name: 'Тип документ',
        procedures_technicalReportDocumentsSearch_extension: 'Разрешени разширения',
        procedures_technicalReportDocumentsSearch_isRequired: 'Задължителен',
        procedures_technicalReportDocumentsSearch_status: 'Статус',

        //procedures_newTechnicalReportDocument
        procedures_newTechnicalReportDocument_title: 'Нов документ към Технически отчет',
        procedures_newTechnicalReportDocument_save: 'Запис',
        procedures_newTechnicalReportDocument_cancel: 'Отказ',

        //procedures_editTechnicalReportDocument
        procedures_editTechnicalReportDocument_title: 'Редакция на документ към Технически отчет',
        procedures_editTechnicalReportDocument_deactivate: 'Деактивирай',
        procedures_editTechnicalReportDocument_activate: 'Активирай',
        procedures_editTechnicalReportDocument_edit: 'Редакция',
        procedures_editTechnicalReportDocument_delete: 'Изтриване',
        procedures_editTechnicalReportDocument_save: 'Запис',
        procedures_editTechnicalReportDocument_cancel: 'Отказ',

        //procedures_financialReportDocumentsSearch
        procedures_financialReportDocumentsSearch_name: 'Тип документ',
        procedures_financialReportDocumentsSearch_extension: 'Разрешени разширения',
        procedures_financialReportDocumentsSearch_isRequired: 'Задължителен',
        procedures_financialReportDocumentsSearch_status: 'Статус',

        //procedures_newFinancialReportDocument
        procedures_newFinancialReportDocument_title: 'Нов документ към Финансов отчет',
        procedures_newFinancialReportDocument_save: 'Запис',
        procedures_newFinancialReportDocument_cancel: 'Отказ',

        //procedures_editFinancialReportDocument
        procedures_editFinancialReportDocument_title: 'Редакция на документ към Финансов отчет',
        procedures_editFinancialReportDocument_deactivate: 'Деактивирай',
        procedures_editFinancialReportDocument_activate: 'Активирай',
        procedures_editFinancialReportDocument_edit: 'Редакция',
        procedures_editFinancialReportDocument_delete: 'Изтриване',
        procedures_editFinancialReportDocument_save: 'Запис',
        procedures_editFinancialReportDocument_cancel: 'Отказ',

        //procedures_advancePaymentDocumentsSearch
        procedures_advancePaymentDocumentsSearch_name: 'Тип документ',
        procedures_advancePaymentDocumentsSearch_extension: 'Разрешени разширения',
        procedures_advancePaymentDocumentsSearch_isRequired: 'Задължителен',
        procedures_advancePaymentDocumentsSearch_status: 'Статус',

        //procedures_newAdvancePaymentDocument
        procedures_newAdvancePaymentDocument_title: 'Нов документ към Искане за авансово плащане',
        procedures_newAdvancePaymentDocument_save: 'Запис',
        procedures_newAdvancePaymentDocument_cancel: 'Отказ',

        //procedures_editAdvancePaymentDocument
        procedures_editAdvancePaymentDocument_title:
          'Редакция на документ към Искане за авансово плащане',
        procedures_editAdvancePaymentDocument_deactivate: 'Деактивирай',
        procedures_editAdvancePaymentDocument_activate: 'Активирай',
        procedures_editAdvancePaymentDocument_edit: 'Редакция',
        procedures_editAdvancePaymentDocument_delete: 'Изтриване',
        procedures_editAdvancePaymentDocument_save: 'Запис',
        procedures_editAdvancePaymentDocument_cancel: 'Отказ',

        //procedures_intermediatePaymentDocumentsSearch
        procedures_intermediatePaymentDocumentsSearch_name: 'Тип документ',
        procedures_intermediatePaymentDocumentsSearch_extension: 'Разрешени разширения',
        procedures_intermediatePaymentDocumentsSearch_isRequired: 'Задължителен',
        procedures_intermediatePaymentDocumentsSearch_status: 'Статус',

        //procedures_newIntermediatePaymentDocument
        procedures_newIntermediatePaymentDocument_title:
          'Нов документ към Искане за междинно плащане',
        procedures_newIntermediatePaymentDocument_save: 'Запис',
        procedures_newIntermediatePaymentDocument_cancel: 'Отказ',

        //procedures_editIntermediatePaymentDocument
        procedures_editIntermediatePaymentDocument_title:
          'Редакция на документ към Искане за междинно плащане',
        procedures_editIntermediatePaymentDocument_deactivate: 'Деактивирай',
        procedures_editIntermediatePaymentDocument_activate: 'Активирай',
        procedures_editIntermediatePaymentDocument_edit: 'Редакция',
        procedures_editIntermediatePaymentDocument_delete: 'Изтриване',
        procedures_editIntermediatePaymentDocument_save: 'Запис',
        procedures_editIntermediatePaymentDocument_cancel: 'Отказ',

        //procedures_finalPaymentDocumentsSearch
        procedures_finalPaymentDocumentsSearch_name: 'Тип документ',
        procedures_finalPaymentDocumentsSearch_extension: 'Разрешени разширения',
        procedures_finalPaymentDocumentsSearch_isRequired: 'Задължителен',
        procedures_finalPaymentDocumentsSearch_status: 'Статус',

        //procedures_newFinalPaymentDocument
        procedures_newFinalPaymentDocument_title: 'Нов документ към Искане за окончателно плащане',
        procedures_newFinalPaymentDocument_save: 'Запис',
        procedures_newFinalPaymentDocument_cancel: 'Отказ',

        //procedures_editFinalPaymentDocument
        procedures_editFinalPaymentDocument_title:
          'Редакция на документ към Искане за окончателно плащане',
        procedures_editFinalPaymentDocument_deactivate: 'Деактивирай',
        procedures_editFinalPaymentDocument_activate: 'Активирай',
        procedures_editFinalPaymentDocument_edit: 'Редакция',
        procedures_editFinalPaymentDocument_delete: 'Изтриване',
        procedures_editFinalPaymentDocument_save: 'Запис',
        procedures_editFinalPaymentDocument_cancel: 'Отказ',

        //procedures_procurementDocumentsSearch
        procedures_procurementDocumentsSearch_name: 'Тип документ',
        procedures_procurementDocumentsSearch_extension: 'Разрешени разширения',
        procedures_procurementDocumentsSearch_isRequired: 'Задължителен',
        procedures_procurementDocumentsSearch_status: 'Статус',

        //procedures_newProcurementDocument
        procedures_newProcurementDocument_title:
          'Нов документ към Процедури за избор на изпълнител и сключени договори',
        procedures_newProcurementDocument_save: 'Запис',
        procedures_newProcurementDocument_cancel: 'Отказ',

        //procedures_editProcurementDocument
        procedures_editProcurementDocument_title:
          'Редакция на документ към Процедури за избор на изпълнител и сключени договори',
        procedures_editProcurementDocument_deactivate: 'Деактивирай',
        procedures_editProcurementDocument_activate: 'Активирай',
        procedures_editProcurementDocument_edit: 'Редакция',
        procedures_editProcurementDocument_delete: 'Изтриване',
        procedures_editProcurementDocument_save: 'Запис',
        procedures_editProcurementDocument_cancel: 'Отказ',

        //procedures_procedureContractReportDocumentForm
        procedures_procedureContractReportDocumentForm_name: 'Тип документ',
        procedures_procedureContractReportDocumentForm_extension:
          'Разрешени разширения (.pdf, .jpg, ...)',
        procedures_procedureContractReportDocumentForm_isRequired: 'Задължителен',
        procedures_procedureContractReportDocumentForm_status: 'Статус',

        //procedures_contractReportDocumentsSearch
        procedures_contractReportDocumentsSearch_title: 'Отчетни документи',
        procedures_contractReportDocumentsSearch_technicalReport: 'Технически отчет',
        procedures_contractReportDocumentsSearch_financialReport: 'Финансов отчет',
        procedures_contractReportDocumentsSearch_advancePayment: 'Искане за авансово плащане',
        procedures_contractReportDocumentsSearch_intermediatePayment: 'Искане за междинно плащане',
        procedures_contractReportDocumentsSearch_finalPayment: 'Искане за окончателно плащане',
        procedures_contractReportDocumentsSearch_draft: 'Чернова',
        procedures_contractReportDocumentsSearch_active: 'Активна',
        procedures_contractReportDocumentsSearch_procurement:
          'Процедури за избор на изпълнител и сключени договори',
        procedures_contractReportDocumentsSearch_changeStatusConfirm:
          "Сигурни ли сте, че искате да промените статуса на секцията на '{{status}}' ?",

        //procedureMassCommunications_view
        procedureMassCommunications_tabs_data: 'Основни данни',
        procedureMassCommunications_tabs_documents: 'Прикачени файлове',
        procedureMassCommunications_tabs_recipients: 'Получатели',

        //procedures_procedureMassCommunicationDocumentForm
        procedures_procedureMassCommunicationDocumentForm_programme: 'Основна организация',
        procedures_procedureMassCommunicationDocumentForm_procedure: 'Бюджет',
        procedures_procedureMassCommunicationDocumentForm_subject: 'Тема',
        procedures_procedureMassCommunicationDocumentForm_body: 'Съобщение',

        //procedureMassCommunications_procedureMassCommunicationsSearch
        procedureMassCommunications_procedureMassCommunicationsSearch_newBtn: 'Нова кореспонденция',
        procedureMassCommunications_procedureMassCommunicationsSearch_subject: 'Тема',
        procedureMassCommunications_procedureMassCommunicationsSearch_status: 'Статус',
        procedureMassCommunications_procedureMassCommunicationsSearch_modifyDate:
          'Дата на модифицирaне',
        procedureMassCommunications_procedureMassCommunicationsSearch_procedureCode:
          'Код на бюджет',

        //procedureMassCommunications_procedureMassCommunicationsNew
        procedureMassCommunications_procedureMassCommunicationsNew_title:
          'Нова обща кореспонденция',
        procedureMassCommunications_procedureMassCommunicationsNew_save: 'Запис',
        procedureMassCommunications_procedureMassCommunicationsNew_cancel: 'Отказ',

        //procedureMassCommunications_procedureMassCommunicationsEdit
        procedureMassCommunications_procedureMassCommunicationsEdit_title:
          'Редакция на обща кореспонденция',
        procedureMassCommunications_procedureMassCommunicationsEdit_edit: 'Редакция',
        procedureMassCommunications_procedureMassCommunicationsEdit_save: 'Запис',
        procedureMassCommunications_procedureMassCommunicationsEdit_send: 'Изпращане',
        procedureMassCommunications_procedureMassCommunicationsEdit_cancel: 'Отказ',
        procedureMassCommunications_procedureMassCommunicationsEdit_sendMessage:
          'Сигурни ли сте, че желаете да изпратите съобщението до получателите му?',
        procedureMassCommunications_procedureMassCommunicationsEdit_delete: 'Изтриване',

        //procedureMassCommunications_procedureMassCommunicationRecipientsSearch
        procedureMassCommunications_procedureMassCommunicationRecipientsSearch_newBtn:
          'Добави получател',
        procedureMassCommunications_procedureMassCommunicationRecipientsSearch_contractRegNumber:
          'Номер на договор',
        procedureMassCommunications_procedureMassCommunicationRecipientsSearch_contractName:
          'Наименование на договор',
        procedureMassCommunications_procedureMassCommunicationRecipientsSearch_contractDate:
          'Дата на договор',
        procedureMassCommunications_procedureMassCommunicationRecipientsSearch_beneficiaryName:
          'Бенефициент',

        //procedureMassCommunications_procedureMassCommunicationsDocumentsSearc
        procedureMassCommunications_procedureMassCommunicationsDocumentsSearch_newBtn:
          'Нов документ',
        procedureMassCommunications_procedureMassCommunicationsDocumentsSearch_name: 'Наименование',
        procedureMassCommunications_procedureMassCommunicationsDocumentsSearch_description:
          'Описание',
        procedureMassCommunications_procedureMassCommunicationsDocumentsSearch_file: 'Файл',

        //procedureMassCommunications_procedureMassCommunicationDocumentsNew
        procedureMassCommunications_procedureMassCommunicationDocumentsNew_title: 'Нов документ',
        procedureMassCommunications_procedureMassCommunicationDocumentsNew_save: 'Запис',
        procedureMassCommunications_procedureMassCommunicationDocumentsNew_cancel: 'Отказ',

        //procedureMassCommunications_procedureMassCommunicationDocumentsEdit
        procedureMassCommunications_procedureMassCommunicationDocumentsEdit_title:
          'Редакция на документ',
        procedureMassCommunications_procedureMassCommunicationDocumentsEdit_edit: 'Редакция',

        procedureMassCommunications_procedureMassCommunicationDocumentsEdit_save: 'Запис',
        procedureMassCommunications_procedureMassCommunicationDocumentsEdit_cancel: 'Отказ',

        //procedureMassCommunications_chooseRecipientsModal
        procedureMassCommunications_chooseRecipientsModal_title: 'Избор на получатели',
        procedureMassCommunications_chooseRecipientsModal_continue: 'Продължи',
        procedureMassCommunications_chooseRecipientsModal_cancel: 'Отказ',
        procedureMassCommunications_chooseRecipientsModal_all: 'Всички',
        procedureMassCommunications_chooseRecipientsModal_contractRegNumber: 'Номер на договор',
        procedureMassCommunications_chooseRecipientsModal_contractName: 'Наименование на договор',
        procedureMassCommunications_chooseRecipientsModal_contractDate: 'Дата на договор',
        procedureMassCommunications_chooseRecipientsModal_beneficiaryName: 'Бенефициент',

        //procedures_declarationsSearch
        procedures_declarationsSearch_name: 'Наименование',
        procedures_declarationsSearch_nameAlt: 'Наименование на английски език',
        procedures_declarationsSearch_status: 'Статус',
        procedures_declarationsSearch_orderNum: 'Пореден номер',

        //procedures_newProcedureDeclaration
        procedures_newProcedureDeclaration_title: 'Добавяне на декларация',
        procedures_newProcedureDeclaration_save: 'Запис',
        procedures_newProcedureDeclaration_cancel: 'Отказ',

        //procedures_editProcedureDeclaration
        procedures_editProcedureDeclaration_title: 'Преглед на декларация',
        procedures_editProcedureDeclaration_deactivate: 'Деактивирай',
        procedures_editProcedureDeclaration_activate: 'Активирай',
        procedures_editProcedureDeclaration_delete: 'Изтриване',
        procedures_editProcedureDeclaration_edit: 'Редакция',
        procedures_editProcedureDeclaration_save: 'Запис',
        procedures_editProcedureDeclaration_cancel: 'Отказ',

        //procedures_procedureDeclarationForm
        procedures_procedureDeclarationForm_programmeDeclaration: 'Декларация',
        procedures_procedureDeclarationForm_content: 'Съдържание',
        procedures_procedureDeclarationForm_status: 'Статус',
        procedures_procedureDeclarationForm_isRequired: 'Задължителен',
        procedures_procedureDeclarationForm_orderNum: 'Пореден номер',

        procurements_tabs_procedureData: 'Основни данни',
        procurements_tabs_differentiatedPositions: 'Обособени позиции',
        procurements_tabs_documents: 'Документи',

        //procurements_procurementsSearch
        procurements_procurementsSearch_newBtn: 'Нова обществена поръчка',
        procurements_procurementsSearch_name: 'Предмет',
        procurements_procurementsSearch_status: 'Статус',
        procurements_procurementsSearch_errandArea: 'Обект',
        procurements_procurementsSearch_errandLegalAct: 'Нормативен акт',
        procurements_procurementsSearch_pPANumber: 'АОП номер',
        procurements_procurementsSearch_prognosysAmount: 'Прогнозна стойност',

        //procurements_newProcurement
        procurements_newProcurement_title: 'Нова централизирана обществена поръчка',
        procurements_newProcurement_save: 'Запис',
        procurements_newProcurement_cancel: 'Отказ',

        //procurements_editProcurement
        procurements_editProcurement_title: 'Редакция на централизирана обществена поръчка',
        procurements_editProcurement_draft: 'Чернова',
        procurements_editProcurement_cancel: 'Отказ',
        procurements_editProcurement_edit: 'Редакция',
        procurements_editProcurement_active: 'Активна',
        procurements_editProcurement_del: 'Изтриване',
        procurements_editProcurement_save: 'Запис',
        procurements_editProcurement_canceled: 'Анулирана',
        procurements_editProcurement_confirmChangeStatus:
          "Сигурни ли сте, че искате да промените статуса на '{{status}}'",

        //procurements_searchProcurementDocuments
        procurements_searchProcurementDocuments_header: 'Документи към обществена поръчка',
        procurements_searchProcurementDocuments_name: 'Наименование',
        procurements_searchProcurementDocuments_description: 'Описание',
        procurements_searchProcurementDocuments_file: 'Файл',

        //procurements_newProcurementDocument
        procurements_newProcurementDocument_title: 'Нов документ към обществена поръчка',
        procurements_newProcurementDocument_save: 'Запис',
        procurements_newProcurementDocument_cancel: 'Отказ',

        //procurements_procurementDocumentEdit
        procurements_procurementDocumentEdit_title: 'Редакция на документ към обществена поръчка',
        procurements_procurementDocumentEdit_edit: 'Редакция',
        procurements_procurementDocumentEdit_del: 'Изтриване',
        procurements_procurementDocumentEdit_save: 'Запис',
        procurements_procurementDocumentEdit_cancel: 'Отказ',

        //procurements_searchProcurementPositions
        procurements_searchProcurementPositions_header: 'Обособени позиции',
        procurements_searchProcurementPositions_name: 'Наименование',
        procurements_searchProcurementPositions_comment: 'Коментар',
        procurements_searchProcurementPositions_companyName: 'Изпълнител',
        procurements_searchProcurementPositions_companyUinType: 'Вид идентификатор',
        procurements_searchProcurementPositions_companyUin: 'Идентификатор на изпълнител',

        //procurements_newProcurementPosition
        procurements_newProcurementPosition_title: 'Нова обособена позиция',
        procurements_newProcurementPosition_save: 'Запис',
        procurements_newProcurementPosition_cancel: 'Отказ',

        //procurements_procurementPositionEdit
        procurements_procurementPositionEdit_title: 'Редакция на обособена позиция',
        procurements_procurementPositionEdit_edit: 'Редакция',
        procurements_procurementPositionEdit_save: 'Запис',
        procurements_procurementPositionEdit_cancel: 'Отказ',

        //procurements_procurementDifferentiatedPositionForm
        procurements_procurementDifferentiatedPositionForm_name: 'Наименование',
        procurements_procurementDifferentiatedPositionForm_comment: 'Коментар',
        procurements_procurementDifferentiatedPositionForm_company: 'Изпълнител',

        //procurements_procurementDataForm
        procurements_procurementDataForm_name: 'Предмет',
        procurements_procurementDataForm_shortName: 'Кратко наименование',
        procurements_procurementDataForm_errandArea: 'Обект',
        procurements_procurementDataForm_pPANumber: 'АОП номер',
        procurements_procurementDataForm_errandLegalAct: 'Приложим нормативен акт',
        procurements_procurementDataForm_errandType: 'Тип процедура',
        procurements_procurementDataForm_description: 'Описание',
        procurements_procurementDataForm_prognosysAmount: 'Прогнозна стойност',
        procurements_procurementDataForm_expectedAmount: 'Прогнозна стойност съгласно обявление',
        procurements_procurementDataForm_planDate: 'Планирана дата на обявяване',
        procurements_procurementDataForm_internetAddress: 'Интернет адрес',
        procurements_procurementDataForm_offersDeadlineDate: 'Крайна дата за подаване на оферти',
        procurements_procurementDataForm_announcedDate:
          'Дата на обявление на процедура за външно възлагане',

        //companies_companyDataForm
        companies_companyDataForm_seat: 'Седалище',
        companies_companyDataForm_managementAddr: 'Адрес на управление',
        companies_companyDataForm_copySeat: 'Копирай в Адрес за кореспонденция',
        companies_companyDataForm_corr: 'Адрес за кореспонденция',
        companies_companyDataForm_contact: 'Лице за контакти',
        companies_companyDataForm_name: 'Пълно наименование',
        companies_companyDataForm_nameAlt: 'Пълно наименование на английски език',
        companies_companyDataForm_uinType: 'Булстат/ЕИК/ЕГН',
        companies_companyDataForm_uin: 'Номер',
        companies_companyDataForm_uinSearch: 'Търси в БУЛСТАТ / ТР / НПО',
        companies_companyDataForm_resultFound: 'Данните са попълнени!',
        companies_companyDataForm_noResultFound: 'Няма намерени резултати!',
        companies_companyDataForm_inputMissing: 'Моля, въведете Булстат/ЕИК/ЕГН.',
        companies_companyDataForm_inputNotValid: 'Въведеното Булстат/ЕИК/ЕГН не е валидно!',
        companies_companyDataForm_companyTypeId: 'Тип организация',
        companies_companyDataForm_companyLegalTypeId: 'Вид организация',

        companies_companyDataForm_seatCountryId: 'Държава',
        companies_companyDataForm_seatSettlementId: 'Населено място',
        companies_companyDataForm_seatPostCode: 'Пощенски код',
        companies_companyDataForm_seatStreet: 'Улица (ж.к., кв., №, бл., вх., ет., ап.)',
        companies_companyDataForm_seatAddress: 'Адрес',
        companies_companyDataForm_corrCountryId: 'Държава',
        companies_companyDataForm_corrSettlementId: 'Населено място',
        companies_companyDataForm_corrPostCode: 'Пощенски код',
        companies_companyDataForm_corrStreet: 'Улица (ж.к., кв., №, бл., вх., ет., ап.)',
        companies_companyDataForm_corrAddress: 'Адрес',
        companies_companyDataForm_phone1: 'Тел. номер 1',
        companies_companyDataForm_phone2: 'Тел. номер 2',
        companies_companyDataForm_fax: 'Номер на факс',
        companies_companyDataForm_representative: 'Имена на лицето, представляващо организацията',
        companies_companyDataForm_email: 'Е-mail',
        companies_companyDataForm_contactName: 'Име',
        companies_companyDataForm_contactPhone: 'Телефонен номер',
        companies_companyDataForm_contactEmail: 'E-mail',

        companies_companyDataForm_programmePriorityType: 'Тип на бюджетния разпределител',

        //companies_searchForm
        companies_searchForm_title: 'Кандидати',
        companies_searchForm_search: 'Търси',
        companies_searchForm_uin: 'Идентификатор',
        companies_searchForm_uinType: 'Булстат/ЕИК/ЕГН',
        companies_searchForm_uinTypeId: 'Тип на идентификатора',
        companies_searchForm_name: 'Име',
        companies_searchForm_newBtn: 'Нова организация',
        companies_searchForm_seat: 'Седалище и адрес на управление',
        companies_searchForm_corr: 'Адрес за кореспонденция',
        companies_searchForm_excelExport: 'Експорт',

        //companies_newForm
        companies_newForm_title: 'Нова организация',
        companies_newForm_save: 'Запис',
        companies_newForm_cancel: 'Отказ',

        //companies_editForm
        companies_editForm_title: 'Данни за кандидат',
        companies_editForm_save: 'Запис',
        companies_editForm_cancel: 'Отказ',
        companies_editForm_edit: 'Редакция',
        companies_editForm_del: 'Изтриване',
        //registrations_tabs

        //registrations_registrationDataForm
        registrations_registrationDataForm_email: 'Е-mail на профила',
        registrations_registrationDataForm_firstName: 'Собствено име',
        registrations_registrationDataForm_lastName: 'Фамилия',
        registrations_registrationDataForm_phone: 'Телефон',

        //registrations_searchForm
        registrations_searchForm_email: 'Е-mail',
        registrations_searchForm_firstName: 'Собствено име',
        registrations_searchForm_lastName: 'Фамилия',
        registrations_searchForm_phone: 'Телефон',
        registrations_searchForm_excelExport: 'Експорт',

        //registrations_editForm
        registrations_editForm_title: 'Преглед на профил',

        //projects_tabs
        projects_tabs_projectEdit: 'Основни данни',
        projects_tabs_communication: 'Кореспонденция',

        //projects_projectDataForm
        projects_projectDataForm_name: 'Наименование',
        projects_projectDataForm_nameAlt: 'Наименование на английски език',
        projects_projectDataForm_procedureId: 'Бюджет',
        projects_projectDataForm_companyName: 'Наименование на кандидат',
        projects_projectDataForm_companyNameAlt: 'Наименование на кандидат на английски език',
        projects_projectDataForm_companyUinType: 'Кандидат - Булстат/ЕИК/ЕГН',
        projects_projectDataForm_companyUin: 'Кандидат - Номер',
        projects_projectDataForm_projectTypeId: 'Тип',
        projects_projectDataForm_registrationStatus: 'Статус',
        projects_projectDataForm_regNumber: 'Номер',
        projects_projectDataForm_regDate: 'Дата на регистрация',
        projects_projectDataForm_regTime: 'Час на регистрация',
        projects_projectDataForm_recieveType: 'Начин на получаване',
        projects_projectDataForm_recieveDate: 'Дата на получаване',
        projects_projectDataForm_submitDate: 'Дата на подаване',
        projects_projectDataForm_storagePlace: 'Място на съхранение',
        projects_projectDataForm_originals: 'Брой оригинали',
        projects_projectDataForm_copies: 'Брой копия',
        projects_projectDataForm_notes: 'Бележки',

        //projects_projectRegistrationsSearch
        projects_projectRegistrationsSearch_search: 'Търси',
        projects_projectRegistrationsSearch_programmePriorityId: 'Разпоредител с бюджетни средства',
        projects_projectRegistrationsSearch_procedureId: 'Бюджет',
        projects_projectRegistrationsSearch_fromDate: 'От дата',
        projects_projectRegistrationsSearch_toDate: 'До дата',

        projects_projectRegistrationsSearch_procedureName: 'Бюджет',
        projects_projectRegistrationsSearch_company: 'Кандидат',
        projects_projectRegistrationsSearch_regNumber: 'Номер на ПП',
        projects_projectRegistrationsSearch_name: 'Наименование',
        projects_projectRegistrationsSearch_registrationStatus: 'Регистрационен статус',
        projects_projectRegistrationsSearch_projectType: 'Тип',
        projects_projectRegistrationsSearch_regDate: 'Дата на регистрация',
        projects_projectRegistrationsSearch_excelExport: 'Експорт',

        //projects_projectRegistationEdit
        projects_projectRegistationEdit_title: 'Редакция на проектно предложение',
        projects_projectRegistationEdit_edit: 'Редакция',
        projects_projectRegistationEdit_save: 'Запис',
        projects_projectRegistationEdit_cancel: 'Отказ',
        projects_projectRegistationEdit_print: 'Принтирай',
        projects_projectRegistationEdit_withdraw: 'Оттеглено',
        projects_projectRegistationEdit_confirmWithdraw:
          'Сигурни ли сте, че искате да оттеглите това проектно предложение?',

        //projects_projectRegistationNewStep1a
        projects_projectRegistationNewStep1a_title:
          'Регистриране на проектно предложение (стъпка 1/3)',
        projects_projectRegistationNewStep1a_next: 'Напред',
        projects_projectRegistationNewStep1a_cancel: 'Отказ',
        projects_projectRegistationNewStep1a_chooseCompany: 'Търси',
        projects_projectRegistationNewStep1a_procedureId: 'Бюджет',
        projects_projectRegistationNewStep1a_uinTypeId: 'Тип на идентификатора',
        projects_projectRegistationNewStep1a_uin: 'Идентификатор',

        //projects_projectRegistationNewStep1b
        projects_projectRegistationNewStep1b_title:
          'Регистриране на проектно предложение (стъпка 1/3)',
        projects_projectRegistationNewStep1b_next: 'Напред',
        projects_projectRegistationNewStep1b_cancel: 'Отказ',
        projects_projectRegistationNewStep1b_code: 'Код',
        projects_projectRegistationNewStep1b_codeTooShort:
          'Въведете поне първите 7 символа от кода',
        projects_projectRegistationNewStep1b_codeNotExists: 'Кодът не е регистриран',

        //projects_projectRegistationNewStep2
        projects_projectRegistationNewStep2_title:
          'Регистриране на проектно предложение (стъпка 2/3)',
        projects_projectRegistationNewStep2_company: 'Кандидат',
        projects_projectRegistationNewStep2_procedureId: 'Бюджет',
        projects_projectRegistationNewStep2_next: 'Напред',
        projects_projectRegistationNewStep2_createContinue: 'Създай и продължи',
        projects_projectRegistationNewStep2_cancel: 'Отказ',
        projects_projectRegistationNewStep2_companyNotFound:
          'Организацията не беше намерена. Моля въведете данни за нова организация.',

        //projects_projectRegistationNewStep3
        projects_projectRegistationNewStep3_title:
          'Регистриране на проектно предложение (стъпка 3/3)',
        projects_projectRegistationNewStep3_save: 'Запис',
        projects_projectRegistationNewStep3_cancel: 'Отказ',

        //projects_modals_chooseCompanyModal
        projects_modals_chooseCompanyModal_title: 'Избор на кандидат',
        projects_modals_chooseCompanyModal_choose: 'Избор',
        projects_modals_chooseCompanyModal_cancel: 'Отказ',
        projects_modals_chooseCompanyModal_search: 'Търси',
        projects_modals_chooseCompanyModal_uin: 'Идентификатор',
        projects_modals_chooseCompanyModal_uinTypeId: 'Тип на идентификатора',
        projects_modals_chooseCompanyModal_uinType: 'Булстат/ЕИК/ЕГН',
        projects_modals_chooseCompanyModal_name: 'Име',
        projects_modals_chooseCompanyModal_seat: 'Седалище и адрес на управление',
        projects_modals_chooseCompanyModal_corr: 'Адрес за кореспонденция',

        //projects_projectRegistrationCommunicationsEdit
        projects_projectRegistrationCommunicationsEdit_title: 'Редакция на кореспонденция',
        projects_projectRegistrationCommunicationsEdit_edit: 'Редакция',
        projects_projectRegistrationCommunicationsEdit_save: 'Запис',
        projects_projectRegistrationCommunicationsEdit_cancel: 'Отказ',
        projects_projectRegistrationCommunicationsEdit_delete: 'Изтриване',
        projects_projectRegistrationCommunicationsEdit_questionTemplate: 'Въпрос',
        projects_projectRegistrationCommunicationsEdit_existingAnswerMessage:
          'Вече сте отговорили на този въпрос. Последващо изпращане на отговор ще анулира предходния отговор.',
        projects_projectRegistrationCommunicationsEdit_cancelConfirm:
          'Сигурни ли сте че искате да анулирате комуникацията с кандидата?',
        projects_projectRegistrationCommunicationsEdit_cancelNote: 'Причина',
        projects_projectRegistrationCommunicationsEdit_cancelCommunication: 'Анулиране',

        //projects_projectRegistrationCommunicationAnswersSearch
        projects_projectRegistrationCommunicationAnswersSearch_answers: 'Отговори',
        projects_projectRegistrationCommunicationAnswersSearch_source: 'Изпратено от',
        projects_projectRegistrationCommunicationAnswersSearch_status: 'Статус',
        projects_projectRegistrationCommunicationAnswersSearch_answerDate: 'Дата на отговор',
        projects_projectRegistrationCommunicationAnswersSearch_preview: 'Преглед',

        //projects_projectRegistrationCommunicationsSearch
        projects_projectRegistrationCommunicationsSearch_new: 'Нова кореспонденция',
        projects_projectRegistrationCommunicationsSearch_orderNum: 'Пореден номер',
        projects_projectRegistrationCommunicationsSearch_status: 'Статус',
        projects_projectRegistrationCommunicationsSearch_source: 'Изпратено от',
        projects_projectRegistrationCommunicationsSearch_projectRegNumber: 'Рег. номер на ПП',
        projects_projectRegistrationCommunicationsSearch_regNumber: 'Рег. номер',
        projects_projectRegistrationCommunicationsSearch_subject: 'Тема',
        projects_projectRegistrationCommunicationsSearch_questionSendDate: 'Дата на изпращане',
        projects_projectRegistrationCommunicationsSearch_questionReadDate: 'Дата на първо отваряне',
        projects_projectRegistrationCommunicationsSearch_answerDate: 'Дата на отговор',

        //projects_projectRegistrationCommunicationDataForm
        projects_projectRegistrationCommunicationDataForm_orderNum: 'Пореден номер',
        projects_projectRegistrationCommunicationDataForm_source: 'Изпратено от',
        projects_projectRegistrationCommunicationDataForm_subject: 'Тема',
        projects_projectRegistrationCommunicationDataForm_status: 'Статус',
        projects_projectRegistrationCommunicationDataForm_regNumber: 'Рег. номер',
        projects_projectRegistrationCommunicationDataForm_projectRegNumber:
          'Рег. номер на проектно предложение',
        projects_projectRegistrationCommunicationDataForm_companyName: 'Наименование на кандидат',
        projects_projectRegistrationCommunicationDataForm_questionDate: 'Дата на изпращане',
        projects_projectRegistrationCommunicationDataForm_questionEndingDate:
          'Краен срок за отговор',
        projects_projectRegistrationCommunicationDataForm_questionReadDate: 'Дата на отваряне',
        projects_projectRegistrationCommunicationDataForm_answerDate: 'Дата на отговор',
        projects_projectRegistrationCommunicationDataForm_statusNote: 'Бележка',
        projects_projectRegistrationCommunicationDataForm_invalidEndingDate:
          'Датата на краен срок за отговор трябва да е по-голяма или равна от текущата дата',

        //projectManagingAuthorityCommunications_communicationsSearch
        projectManagingAuthorityCommunications_communicationsSearch_status: 'Статус',
        projectManagingAuthorityCommunications_communicationsSearch_source: 'Изпратено от',
        projectManagingAuthorityCommunications_communicationsSearch_projectRegNumber:
          'Рег. номер на ПП',
        projectManagingAuthorityCommunications_communicationsSearch_regNumber: 'Рег. номер',
        projectManagingAuthorityCommunications_communicationsSearch_subject: 'Тема',
        projectManagingAuthorityCommunications_communicationsSearch_questionSendDate:
          'Дата на изпращане',
        projectManagingAuthorityCommunications_communicationsSearch_questionReadDate:
          'Дата на първо отваряне',
        projectManagingAuthorityCommunications_communicationsSearch_answerDate: 'Дата на отговор',
        projectManagingAuthorityCommunications_communicationsSearch_excelExport: 'Експорт',
        projectManagingAuthorityCommunications_communicationsSearch_search: 'Търсене',
        projectManagingAuthorityCommunications_communicationsSearch_programme:
          'Основна организация',
        projectManagingAuthorityCommunications_communicationsSearch_programmePriority:
          'Разпоредител с бюджетни средства',
        projectManagingAuthorityCommunications_communicationsSearch_procedure: 'Бюджет',
        projectManagingAuthorityCommunications_communicationsSearch_fromDate: 'Начална дата',
        projectManagingAuthorityCommunications_communicationsSearch_toDate: 'Към дата',

        //projectManagingAuthorityCommunications_communicationsEdit
        projectManagingAuthorityCommunications_communicationsEdit_title:
          'Преглед на кореспонденция',
        projectManagingAuthorityCommunications_communicationsEdit_back: 'Назад',
        projectManagingAuthorityCommunications_communicationsEdit_questionTemplate: 'Въпрос',
        projectManagingAuthorityCommunications_communicationsEdit_answerTemplate: 'Отговор',
        projectManagingAuthorityCommunications_communicationsEdit_answers: 'Отговори',
        projectManagingAuthorityCommunications_communicationsEdit_source: 'Изпратено от',
        projectManagingAuthorityCommunications_communicationsEdit_status: 'Статус',
        projectManagingAuthorityCommunications_communicationsEdit_answerDate: 'Дата на отговор',
        projectManagingAuthorityCommunications_communicationsEdit_preview: 'Преглед',

        //projectMassManagingAuthorityCommunications_view
        projectMassManagingAuthorityCommunications_tabs_data: 'Основни данни',
        projectMassManagingAuthorityCommunications_tabs_documents: 'Прикачени файлове',
        projectMassManagingAuthorityCommunications_tabs_recipients: 'Получатели',

        //projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationForm
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationForm_programme:
          'Оперативна програма',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationForm_procedure:
          'Процедура',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationForm_subject:
          'Тема',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationForm_message:
          'Съобщение',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationForm_endingDate:
          'Краен срок за отговор',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationForm_invalidEndingDate:
          'Датата на краен срок за отговор трябва да е по-голяма или равна на текущата дата',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationForm_orderNum:
          'Пореден номер',

        //projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsSearch
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsSearch_newBtn:
          'Нова комуникация',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsSearch_subject:
          'Тема',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsSearch_status:
          'Статус',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsSearch_modifyDate:
          'Дата на модифицирaне',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsSearch_procedureCode:
          'Код на процедура',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsSearch_orderNum:
          'Пореден номер',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsSearch_programmeName:
          'Оперативна програма',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsSearch_endingDate:
          'Краен срок за отговор',

        //projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsNew
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsNew_title:
          'Нова обща комуникация',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsNew_save:
          'Запис',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsNew_cancel:
          'Отказ',

        //projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsEdit
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsEdit_title:
          'Редакция на обща комуникация',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsEdit_edit:
          'Редакция',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsEdit_save:
          'Запис',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsEdit_send:
          'Изпращане',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsEdit_cancel:
          'Отказ',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsEdit_sendMessage:
          'Сигурни ли сте, че желаете да изпратите съобщението до получателите му?',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationsEdit_delete:
          'Изтриване',

        //projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationRecipientsSearch
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationRecipientsSearch_newBtn:
          'Добави получател',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationRecipientsSearch_projectRegNumber:
          'Номер на ПП',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationRecipientsSearch_projectName:
          'Наименование на ПП',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationRecipientsSearch_recieveDate:
          'Дата на получаване на ПП',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationRecipientsSearch_beneficiaryName:
          'Бенефициент',

        //projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsSearch
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsSearch_newBtn:
          'Нов документ',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsSearch_name:
          'Наименование',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsSearch_description:
          'Описание',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsSearch_file:
          'Файл',

        //projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsNew
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsNew_title:
          'Нов документ',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsNew_save:
          'Запис',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsNew_cancel:
          'Отказ',

        //projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsEdit
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsEdit_title:
          'Редакция на документ',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsEdit_edit:
          'Редакция',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsEdit_deleteDocument:
          'Изтриване',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsEdit_save:
          'Запис',
        projectMassManagingAuthorityCommunications_projectMassManagingAuthorityCommunicationDocumentsEdit_cancel:
          'Отказ',

        //projectMassManagingAuthorityCommunications_chooseRecipientsModal
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_title:
          'Избор на получатели',
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_continue: 'Продължи',
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_cancel: 'Отказ',
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_all: 'Всички',
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_projectRegNumber:
          'Номер на ПП',
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_projectName:
          'Наименование на ПП',
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_recieveDate:
          'Дата на получаване на ПП',
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_beneficiaryName:
          'Бенефициент',
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_template: 'Свали шаблон',
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_chooseFile:
          'Зареди ПП от Excel',
        projectMassManagingAuthorityCommunications_chooseRecipientsModal_file: 'Файл',

        //projects_projectRegistrationCommunicationAnswersEdit
        projects_projectRegistrationCommunicationAnswersEdit_title: 'Преглед на отговор',
        projects_projectRegistrationCommunicationAnswersEdit_answerTemplate: 'Отговор',
        projects_projectRegistrationCommunicationAnswersEdit_back: 'Назад',
        projects_projectRegistrationCommunicationAnswersEdit_delete: 'Изтриване',

        //projects_projectCommunicationAnswerDataForm
        projects_projectCommunicationAnswerDataForm_orderNum: 'Пореден номер',
        projects_projectCommunicationAnswerDataForm_answerDate: 'Дата на отговор',
        projects_projectCommunicationAnswerDataForm_source: 'Източник',
        projects_projectCommunicationAnswerDataForm_status: 'Статус',

        //common_modals_validationErrorsModal
        common_modals_validationErrorsModal_cancel: 'Отказ',
        common_modals_validationErrorsModal_hasErrors: 'Възникнаха следните валидационни грешки:',

        //common_modals_messageNoteModal
        common_modals_messageNoteModal_ok: 'ОК',
        common_modals_messageNoteModal_cancel: 'Отказ',

        //common_modals_bfpCalculatorModal

        //userOrganizations_search
        userOrganizations_search_new: 'Нова организация към група потребители',
        userOrganizations_search_name: 'Наименование',

        //userOrganizations_userOrganizationForm
        userOrganizations_userOrganizationForm_name: 'Наименование',

        //userOrganizations_new
        userOrganizations_new_title: 'Нова организация към група потребители',
        userOrganizations_new_save: 'Запис',
        userOrganizations_new_cancel: 'Отказ',

        //userOrganizations_edit
        userOrganizations_edit_title: 'Редакция на организация към група потребители',
        userOrganizations_edit_edit: 'Редакция',
        userOrganizations_edit_save: 'Запис',
        userOrganizations_edit_cancel: 'Отказ',
        userOrganizations_edit_delete: 'Изтриване',

        //evalSessions_tabs
        evalSessions_tabs_evalSessionData: 'Основни данни',
        evalSessions_tabs_evalSessionUsers: 'Членове',
        evalSessions_tabs_evalSessionProjects: 'Проектни предложения',
        evalSessions_tabs_evalSessionStandpoints: 'Становища',
        evalSessions_tabs_evalSessionSheets: 'Оценителни листове',
        evalSessions_tabs_evalSessionDistributions: 'Разпределения',
        evalSessions_tabs_evalSessionStandings: 'Класиране',
        evalSessions_tabs_evalSessionCommunication: 'Комуникация',
        evalSessions_tabs_evalSessionDocuments: 'Документи',
        evalSessions_tabs_evalSessionResults: 'Резултати от оценка',

        //evalSessions_viewEvalSession
        evalSessions_viewEvalSession_info:
          'Статус: {{status}}, Номер: {{number}}, Бюджет: {{procedure}}',

        //evalSessions_evalSessionDataForm
        evalSessions_evalSessionDataForm_procedureId: 'Бюджет',
        evalSessions_evalSessionDataForm_evalSessionStatus: 'Статус',
        evalSessions_evalSessionDataForm_evalSessionType: 'Тип',
        evalSessions_evalSessionDataForm_sessionNum: 'Номер на сесия',
        evalSessions_evalSessionDataForm_sessionDate: 'Дата на сесия',
        evalSessions_evalSessionDataForm_orderNum: 'Номер на заповед',
        evalSessions_evalSessionDataForm_orderDate: 'Дата на заповед',

        //evalSessions_evalSessionsSearch
        evalSessions_evalSessionsSearch_search: 'Търси',
        evalSessions_evalSessionsSearch_new: 'Нова оценителна сесия',
        evalSessions_evalSessionsSearch_procedure: 'Бюджет',
        evalSessions_evalSessionsSearch_procedureName: 'Бюджет',
        evalSessions_evalSessionsSearch_evalSessionStatus: 'Статус',
        evalSessions_evalSessionsSearch_evalSessionType: 'Тип',
        evalSessions_evalSessionsSearch_sessionNum: 'Номер на сесия',
        evalSessions_evalSessionsSearch_sessionDate: 'Дата',
        evalSessions_evalSessionsSearch_orderNum: 'Номер на заповед',
        evalSessions_evalSessionsSearch_orderDate: 'Дата на заповед',

        //evalSessions_newEvalSessions
        evalSessions_newEvalSessions_title: 'Нова оценителна сесия',
        evalSessions_newEvalSessions_save: 'Запис',
        evalSessions_newEvalSessions_cancel: 'Отказ',

        //evalSessions_editEvalSessions
        evalSessions_editEvalSessions_title: 'Данни за оценителна сесия',
        evalSessions_editEvalSessions_edit: 'Редакция',
        evalSessions_editEvalSessions_save: 'Запис',
        evalSessions_editEvalSessions_cancel: 'Отказ',
        evalSessions_editEvalSessions_draft: 'Чернова',
        evalSessions_editEvalSessions_active: 'Активна',
        evalSessions_editEvalSessions_ended: 'Приключена',
        evalSessions_editEvalSessions_endedByLAG: 'Приключена от МИГ',
        evalSessions_editEvalSessions_canceled: 'Анулирана',
        evalSessions_editEvalSession_changeStatusConfirm:
          "Сигурни ли сте, че искате да промените статуса на оценителната сесия на '{{status}}' ?",

        //evalSessions_evalSessionUserForm
        evalSessions_evalSessionUserForm_userId: 'Потребител',
        evalSessions_evalSessionUserForm_type: 'Тип',
        evalSessions_evalSessionUserForm_position: 'Позиция',

        //evalSessions_evalSessionUsersSearch
        evalSessions_evalSessionUsersSearch_new: 'Нов член',
        evalSessions_evalSessionUsersSearch_username: 'Потребителско име',
        evalSessions_evalSessionUsersSearch_fullname: 'Име',
        evalSessions_evalSessionUsersSearch_type: 'Тип в сесията',
        evalSessions_evalSessionUsersSearch_position: 'Позиция',
        evalSessions_evalSessionUsersSearch_status: 'Статус',

        //evalSessions_newEvalSessionUser
        evalSessions_newEvalSessionUser_title: 'Нов член',
        evalSessions_newEvalSessionUser_save: 'Запис',
        evalSessions_newEvalSessionUser_cancel: 'Отказ',

        //evalSessions_editEvalSessionUser
        evalSessions_editEvalSessionUser_title: 'Редакция на член',
        evalSessions_editEvalSessionUser_edit: 'Редакция',
        evalSessions_editEvalSessionUser_deleteUser: 'Изтриване',
        evalSessions_editEvalSessionUser_save: 'Запис',
        evalSessions_editEvalSessionUser_cancel: 'Отказ',
        evalSessions_editEvalSessionUser_activate: 'Активиране',
        evalSessions_editEvalSessionUser_deactivate: 'Деактивиране',

        //evalSessions_evalSessionProjects
        evalSessions_evalSessionProjects_choose: 'Избери проектни предложения',
        evalSessions_evalSessionProjects_adminAdmiss: 'Обобщена ОАСД',
        evalSessions_evalSessionProjects_techFinance: 'Обобщена ТФО',
        evalSessions_evalSessionProjects_complex: 'Обобщена КО',
        evalSessions_evalSessionProjects_preliminary: 'Обобщена ПО',
        evalSessions_evalSessionProjects_excelExport: 'Експорт',
        evalSessions_evalSessionProjects_type: 'Тип',
        evalSessions_evalSessionProjects_isDeleted: 'Анули- рано',
        evalSessions_evalSessionProjects_eval: 'Обобщена оценка',
        evalSessions_evalSessionProjects_ASD: 'ОАСД',
        evalSessions_evalSessionProjects_TFO: 'ТФО',
        evalSessions_evalSessionProjects_Complex: 'КО',
        evalSessions_evalSessionProjects_Preliminary: 'ПО',
        evalSessions_evalSessionProjects_pass: 'Преминава',
        evalSessions_evalSessionProjects_points: 'Точки',
        evalSessions_evalSessionProjects_procedureName: 'Бюджет',
        evalSessions_evalSessionProjects_projectRegNumber: 'Номер на ПП',
        evalSessions_evalSessionProjects_projectName: 'Наименование',
        evalSessions_evalSessionProjects_projectKidCode: 'КП по КИД 2008',
        evalSessions_evalSessionProjects_company: 'Кандидат',
        evalSessions_evalSessionProjects_companyKidCode: 'КО по КИД 2008',
        evalSessions_evalSessionProjects_projectRegistrationStatus: 'Рег. статус',
        evalSessions_evalSessionProjects_projectType: 'Тип на ПП',
        evalSessions_evalSessionProjects_projectRegDate: 'Дата на рег.',
        evalSessions_evalSessionProjects_orderNum: '№',
        evalSessions_evalSessionProjects_standingStatus: 'Класиране',
        evalSessions_evalSessionProjects_projectWorkStatus: 'Работен Статус',
        evalSessions_evalSessionProjects_automaticProjectVersion: 'Автоматична версия на ПП',
        evalSessions_evalSessionProjects_automaticProjectMonitorstatRequests: 'Изпращане към НСИ',

        //evalSessions_viewEvalSessionProject
        evalSessions_viewEvalSessionProject_title: 'Преглед на проектно предложение',
        evalSessions_viewEvalSessionProject_regData: 'Регистрационни данни',
        evalSessions_viewEvalSessionProject_history: 'История на промените',
        evalSessions_viewEvalSessionProject_createFromRegData: 'Зареди от регистрационните данни',
        evalSessions_viewEvalSessionProject_projectStatus: 'Статус',
        evalSessions_viewEvalSessionProject_createDate: 'Дата на създаване',
        evalSessions_viewEvalSessionProject_createNote: 'Бележка',
        evalSessions_viewEvalSessionProject_modifyDate: 'Дата на последна промяна',
        evalSessions_viewEvalSessionProject_file: 'Файл',
        evalSessions_viewEvalSessionProject_signatures: 'Ел. подписи',
        evalSessions_viewEvalSessionProject_communication: 'Комуникация с кандидата',
        evalSessions_viewEvalSessionProject_otherCommunication: 'Друга комуникация с кандидата',
        evalSessions_viewEvalSessionProject_communicationSessionNum: 'Номер на сесия',
        evalSessions_viewEvalSessionProject_communicationStatus: 'Статус',
        evalSessions_viewEvalSessionProject_regNumber: 'Рег. номер',
        evalSessions_viewEvalSessionProject_questionDate: 'Дата на изпращане',
        evalSessions_viewEvalSessionProject_questionEndingDate: 'Краен срок за отговор',
        evalSessions_viewEvalSessionProject_answerDate: 'Дата на отговор',
        evalSessions_viewEvalSessionProject_evaluations: 'Обобщени оценки',
        evalSessions_viewEvalSessionProject_type: 'Тип на етап',
        evalSessions_viewEvalSessionProject_calculationType: 'Тип на обобщаването',
        evalSessions_viewEvalSessionProject_pass: 'Преминава',
        evalSessions_viewEvalSessionProject_result: 'Точки',
        evalSessions_viewEvalSessionProject_note: 'Коментар',
        evalSessions_viewEvalSessionProject_isDeleted: 'Анулиран',
        evalSessions_viewEvalSessionProject_isDeletedNote: 'Причина за анулиране',
        evalSessions_viewEvalSessionProject_cancelProject: 'Анулиране',
        evalSessions_viewEvalSessionProject_restoreProject: 'Възстановяване',
        evalSessions_viewEvalSessionProject_cancelMessage: 'Причина за анулиране',
        evalSessions_viewEvalSessionProject_confirmCancel:
          'Сигурни ли сте, че искате да анулирате проектното предложение?',
        evalSessions_viewEvalSessionProject_standings: 'Класиране',
        evalSessions_viewEvalSessionProject_newPreliminaryStanding: 'Ново предварително класиране',
        evalSessions_viewEvalSessionProject_newStanding: 'Ново класиране',
        evalSessions_viewEvalSessionProject_isPreliminary: 'Предварително',
        evalSessions_viewEvalSessionProject_orderNum: 'Пореден номер',
        evalSessions_viewEvalSessionProject_standingType: 'Тип',
        evalSessions_viewEvalSessionProject_status: 'Статус',
        evalSessions_viewEvalSessionProject_grandAmount: 'Одобрено БФП (лв.)',
        evalSessions_viewEvalSessionProject_notes: 'Бележки',
        evalSessions_viewEvalSessionProject_adminAdmiss: 'Обобщена ОАСД',
        evalSessions_viewEvalSessionProject_techFinance: 'Обобщена ТФО',
        evalSessions_viewEvalSessionProject_complex: 'Обобщена КО',
        evalSessions_viewEvalSessionProject_preliminary: 'Обобщена ПО',
        evalSessions_viewEvalSessionProject_confirmRestore:
          'Сигурни ли сте, че искате да възстановите проектното предложение? ' +
          'Това действие няма да възстанови оценителни листа и обобщените оценки, които са били анулирани преди да бъде анулирано ' +
          'проектното предложение.',
        evalSessions_viewEvalSessionProject_monitorstatRequests: 'Заявки към Mониторстат',
        evalSessions_viewEvalSessionProject_newMonitorstatRequest: 'Нова заявка',
        evalSessions_viewEvalSessionProject_newMonitorstatMassRequest: 'Нова обща заявка',
        evalSessions_viewEvalSessionProject_monitorstatStatus: 'Статус',
        evalSessions_viewEvalSessionProject_companyUin: 'Идентификатор',
        evalSessions_viewEvalSessionProject_monitorstatModifyDate: 'Дата на модифициране',
        evalSessions_viewEvalSessionProject_monitorstatUser: 'Потребител',
        evalSessions_viewEvalSessionProject_monitorstatDeclaration: 'Декларация',
        evalSessions_viewEvalSessionProject_receivedFiles: 'Получени файлове',

        //evalSessions_evalSessionProjectMonitorstatNew
        evalSessions_evalSessionProjectMonitorstatNew_title: 'Нова заявка към Мониторстат',
        evalSessions_evalSessionProjectMonitorstatNew_save: 'Запис',
        evalSessions_evalSessionProjectMonitorstatNew_cancel: 'Отказ',

        //evalSessions_projectMonitorstatEdit
        evalSessions_projectMonitorstatEdit_title: 'Редакция на заявка към Мониторстат',
        evalSessions_projectMonitorstatEdit_edit: 'Редакция',
        evalSessions_projectMonitorstatEdit_save: 'Запис',
        evalSessions_projectMonitorstatEdit_send: 'Изпрати',
        evalSessions_projectMonitorstatEdit_cancel: 'Отказ',
        evalSessions_projectMonitorstatEdit_sendMessage:
          'Сигурни ли сте, че искате да изпратите заявката за обработка към Мониторстат?',
        evalSessions_projectMonitorstatEdit_responses: 'Получени файлове',
        evalSessions_projectMonitorstatEdit_fileName: 'Наименование',
        evalSessions_projectMonitorstatEdit_modifyDate: 'Дата на получаване',
        evalSessions_projectMonitorstatEdit_file: 'Файл',

        //evalSessions_pojectMonitorstatRequest
        evalSessions_pojectMonitorstatRequest_title: 'Заявка към Мониторстат',
        evalSessions_pojectMonitorstatRequest_inqury: 'Изследване',
        evalSessions_pojectMonitorstatRequest_declaration: 'Декларация',
        evalSessions_pojectMonitorstatRequest_eDeclaration: 'Е-Деклрация',
        evalSessions_pojectMonitorstatRequest_status: 'Статус',
        evalSessions_pojectMonitorstatRequest_companyUin: 'Идентификатор',
        evalSessions_pojectMonitorstatRequest_companyUinType: 'Булстат/ЕИК/ЕГН',
        evalSessions_pojectMonitorstatRequest_foreignGid: 'Мониторстат идентификатор',

        //evalSessions_projectVersionForm
        evalSessions_projectVersionForm_status: 'Статус',
        evalSessions_projectVersionForm_createNote: 'Бележка',
        evalSessions_projectVersionForm_createNoteAlt: 'Бележка на английски',

        //evalSessions_projectCommunication
        evalSessions_projectCommunication_status: 'Статус',
        evalSessions_projectCommunication_regNumber: 'Рег. номер',
        evalSessions_projectCommunication_projectRegNumber: 'Рег. номер на проектно предложение',
        evalSessions_projectCommunication_companyName: 'Наименование на кандидат',
        evalSessions_projectCommunication_questionDate: 'Дата на изпращане',
        evalSessions_projectCommunication_questionEndingDate: 'Краен срок за отговор',
        evalSessions_projectCommunication_questionReadDate: 'Дата на отваряне',
        evalSessions_projectCommunication_answerDate: 'Дата на отговор',
        evalSessions_projectCommunication_statusNote: 'Бележка',
        evalSessions_projectCommunication_invalidEndingDate:
          'Датата на краен срок за отговор трябва да е по-голяма или равна на датата на изпращане',

        //evalSessions_newEvalSessionProjectVersion
        evalSessions_newEvalSessionProjectVersion_title: 'Нова версия на проектно предложение',
        evalSessions_newEvalSessionProjectVersion_save: 'Запис',
        evalSessions_newEvalSessionProjectVersion_cancel: 'Отказ',

        //evalSessions_editEvalSessionProjectVersion
        evalSessions_editEvalSessionProjectVersion_title: 'Редакция на проектно предложение',
        evalSessions_editEvalSessionProjectVersion_back: 'Назад',
        evalSessions_editEvalSessionProjectVersion_edit: 'Редакция',
        evalSessions_editEvalSessionProjectVersion_delete: 'Изтриване',
        evalSessions_editEvalSessionProjectVersion_save: 'Запис',
        evalSessions_editEvalSessionProjectVersion_cancel: 'Отказ',
        evalSessions_editEvalSessionProjectVersion_template: 'Проектно предложение',
        evalSessions_editEvalSessionProjectVersion_file: 'Файл',
        evalSessions_editEvalSessionProjectVersion_signatures: 'Ел. подписи',

        //evalSessions_editEvalSessionProjectCommunication
        evalSessions_editEvalSessionProjectCommunication_title: 'Преглед на въпрос към кандидат',
        evalSessions_editEvalSessionProjectCommunication_back: 'Назад',
        evalSessions_editEvalSessionProjectCommunication_delete: 'Изтриване',
        evalSessions_editEvalSessionProjectCommunication_edit: 'Редакция',
        evalSessions_editEvalSessionProjectCommunication_save: 'Запис',
        evalSessions_editEvalSessionProjectCommunication_cancel: 'Отказ',
        evalSessions_editEvalSessionProjectCommunication_cancelCommunication: 'Анулиране',
        evalSessions_editEvalSessionProjectCommunication_cancelConfirm:
          'Сигурни ли сте че искате да анулирате комуникацията с кандидата?',
        evalSessions_editEvalSessionProjectCommunication_cancelNote: 'Причина',
        evalSessions_editEvalSessionProjectCommunication_questionTemplate: 'Въпрос',

        //evalSessions_editEvalSessionProjectCommunication_answers
        evalSessions_editEvalSessionProjectCommunication_answers_title: 'Отговори',
        evalSessions_editEvalSessionProjectCommunication_answers_orderNum: 'Пореден номер',
        evalSessions_editEvalSessionProjectCommunication_answers_status: 'Статус',
        evalSessions_editEvalSessionProjectCommunication_answers_answerDate: 'Дата на отговор',
        evalSessions_editEvalSessionProjectCommunication_answers_preview: 'Преглед',

        //evalSessions_editEvalSessionProjectCommunicationAnswer
        evalSessions_editEvalSessionProjectCommunicationAnswer_title: 'Преглед на отговор',
        evalSessions_editEvalSessionProjectCommunicationAnswer_back: 'Назад',
        evalSessions_editEvalSessionProjectCommunicationAnswer_answerTemplate: 'Отговор',
        evalSessions_editEvalSessionProjectCommunicationAnswer_file: 'Файл',
        evalSessions_editEvalSessionProjectCommunicationAnswer_signatures: 'Ел. подписи',
        evalSessions_editEvalSessionProjectCommunicationAnswer_register:
          'Регистриране на отговор с код',
        evalSessions_editEvalSessionProjectCommunicationAnswer_print: 'Принтирай',

        //evalSessions_registerAnswerModal
        evalSessions_registerAnswerModal_title: 'Регистриране на отговор',
        evalSessions_registerAnswerModal_hash: 'Код',
        evalSessions_registerAnswerModal_regNumber: 'Номер',
        evalSessions_registerAnswerModal_regDate: 'Дата на регистрация',
        evalSessions_registerAnswerModal_regTime: 'Час на регистрация',
        evalSessions_registerAnswerModal_register: 'Регистрирай',
        evalSessions_registerAnswerModal_cancel: 'Отказ',
        evalSessions_registerAnswerModal_hasErrors: 'Възникнаха следните валидационни грешки:',

        //evalSessions_chooseProjectsModal_title
        evalSessions_chooseProjectsModal_title: 'Избор на проектни предложения',
        evalSessions_chooseProjectsModal_search: 'Търси',
        evalSessions_chooseProjectsModal_procedureId: 'Бюджет',
        evalSessions_chooseProjectsModal_fromDate: 'От дата',
        evalSessions_chooseProjectsModal_toDate: 'До дата',
        evalSessions_chooseProjectsModal_companySizeType: 'Категория на предприятието',
        evalSessions_chooseProjectsModal_companyKidCode: 'КО по КИД 2008',
        evalSessions_chooseProjectsModal_projectKidCode: 'КП по КИД 2008',
        evalSessions_chooseProjectsModal_continue: 'Продължи',
        evalSessions_chooseProjectsModal_cancel: 'Отказ',
        evalSessions_chooseProjectsModal_all: 'Всички',
        evalSessions_chooseProjectsModal_procedureName: 'Бюджет',
        evalSessions_chooseProjectsModal_company: 'Кандидат',
        evalSessions_chooseProjectsModal_name: 'Наименование',
        evalSessions_chooseProjectsModal_regNumber: 'Номер на ПП',
        evalSessions_chooseProjectsModal_registrationStatus: 'Регистрационен статус',
        evalSessions_chooseProjectsModal_projectType: 'Тип',
        evalSessions_chooseProjectsModal_regDate: 'Дата на регистрация',
        evalSessions_chooseProjectsModal_cannotAddProject:
          'Някое от проектните предложения вече е включено в друга сесия',

        //evalSessions_chooseAndEvaluateProjectModal_title
        evalSessions_chooseAndEvaluateProjectModal_adminAdmissTitle:
          'Избор на проектно предложение за обобщаване на ОАСД',
        evalSessions_chooseAndEvaluateProjectModal_techFinanceTitle:
          'Избор на проектно предложение за обобщаване на ТФО',
        evalSessions_chooseAndEvaluateProjectModal_complexTitle:
          'Избор на проектно предложение за обобщаване на КО',
        evalSessions_chooseAndEvaluateProjectModal_procedureName: 'Бюджет',
        evalSessions_chooseAndEvaluateProjectModal_projectRegNumber: 'Номер на ПП',
        evalSessions_chooseAndEvaluateProjectModal_projectName: 'Наименование',
        evalSessions_chooseAndEvaluateProjectModal_projectKidCode: 'КП по КИД 2008',
        evalSessions_chooseAndEvaluateProjectModal_company: 'Кандидат',
        evalSessions_chooseAndEvaluateProjectModal_companyKidCode: 'КО по КИД 2008',
        evalSessions_chooseAndEvaluateProjectModal_projectRegistrationStatus: 'Статус',
        evalSessions_chooseAndEvaluateProjectModal_projectType: 'Тип на ПП',
        evalSessions_chooseAndEvaluateProjectModal_projectRegDate: 'Дата на регистрация',
        evalSessions_chooseAndEvaluateProjectModal_type: 'Тип',
        evalSessions_chooseAndEvaluateProjectModal_cancel: 'Отказ',
        evalSessions_chooseAndEvaluateProjectModal_choose: 'Избери',
        evalSessions_chooseAndEvaluateProjectModal_evaluate: 'Автоматично обобщаване',
        evalSessions_chooseAndEvaluateProjectModal_proceedMessage:
          'Извършено е автоматично групово обобщаване на всички ПП със статус „Преминава“. За оставащите в списъка ПП трябва да бъде извършено индивидуално обобщаване чрез избор на всяко от тях.',

        //evalSessions_performAutomaticProjectEvaluationsModal
        evalSessions_performAutomaticProjectEvaluationsModal_title: 'Автоматична версия на ПП',
        evalSessions_performAutomaticProjectEvaluationsModal_continue: 'Продължи',
        evalSessions_performAutomaticProjectEvaluationsModal_cancel: 'Отказ',
        evalSessions_performAutomaticProjectEvaluationsModal_close: 'Затвори',
        evalSessions_performAutomaticProjectEvaluationsModal_file: 'Файл',
        evalSessions_performAutomaticProjectEvaluationsModal_confirm:
          'Сигурни ли сте че искате да продължите?',
        evalSessions_performAutomaticProjectEvaluationsModal_automaticEvaluationSuccess:
          'Автоматичното създаване на версии на ПП премина успешно.',
        evalSessions_performAutomaticProjectEvaluationsModal_template: 'Свали шаблон',

        //evalSessions_evalSessionSheetForm
        evalSessions_evalSessionSheetForm_userId: 'Член',
        evalSessions_evalSessionSheetForm_projectId: 'Проектно предложение',
        evalSessions_evalSessionSheetForm_type: 'Тип на етап',
        evalSessions_evalSessionSheetForm_status: 'Статус',
        evalSessions_evalSessionSheetForm_distributionType: 'Тип на разпределение',
        evalSessions_evalSessionSheetForm_statusNote: 'Причина за промяна на статуса',
        evalSessions_evalSessionSheetForm_statusDate: 'Дата на промяна на статуса',
        evalSessions_evalSessionSheetForm_createDate: 'Дата на създаване',
        evalSessions_evalSessionSheetForm_notes: 'Коментар',
        evalSessions_evalSessionSheetForm_distribution: 'Разпределение',
        evalSessions_evalSessionSheetForm_viewDistribution: 'Преглед',
        evalSessions_evalSessionSheetForm_pass: 'Преминава',
        evalSessions_evalSessionSheetForm_result: 'Точки',
        evalSessions_evalSessionSheetForm_evalNote: 'Забележка от оценителен лист',

        //evalSessions_evalSessionSheetsSearch
        evalSessions_evalSessionSheetsSearch_new: 'Нов оценителен лист',
        evalSessions_evalSessionSheetsSearch_excelExport: 'Експорт',
        evalSessions_evalSessionSheetsSearch_assessor: 'Член',
        evalSessions_evalSessionSheetsSearch_project: 'Проектно предложение',
        evalSessions_evalSessionSheetsSearch_type: 'Тип на етап',
        evalSessions_evalSessionSheetsSearch_distribution: 'Разпределение',
        evalSessions_evalSessionSheetsSearch_status: 'Статус',
        evalSessions_evalSessionSheetsSearch_statuses: 'Статуси',
        evalSessions_evalSessionSheetsSearch_distributionType: 'Тип на разпределение',
        evalSessions_evalSessionSheetsSearch_pass: 'Преминава',
        evalSessions_evalSessionSheetsSearch_result: 'Точки',
        evalSessions_evalSessionSheetsSearch_note: 'Коментар',

        //evalSessions_newEvalSessionSheet
        evalSessions_newEvalSessionSheet_title: 'Нов оценителен лист',
        evalSessions_newEvalSessionSheet_save: 'Запис',
        evalSessions_newEvalSessionSheet_cancel: 'Отказ',

        //evalSessions_evalSessionSheetsProject
        evalSessions_evalSessionSheetsProject_title: 'Преглед на проектно предложение',
        evalSessions_evalSessionSheetsProject_back: 'Назад',
        evalSessions_evalSessionSheetsProject_regData: 'Регистрационни данни',
        evalSessions_evalSessionSheetsProject_history: 'История на промените',
        evalSessions_evalSessionSheetsProject_projectStatus: 'Статус',
        evalSessions_evalSessionSheetsProject_createDate: 'Дата на създаване',
        evalSessions_evalSessionSheetsProject_createNote: 'Бележка',
        evalSessions_evalSessionSheetsProject_modifyDate: 'Дата на последна промяна',
        evalSessions_evalSessionSheetsProject_file: 'Файл',
        evalSessions_evalSessionSheetsProject_signatures: 'Ел. подписи',
        evalSessions_evalSessionSheetsProject_communication: 'Комуникация с кандидата',
        evalSessions_evalSessionSheetsProject_otherCommunication: 'Друга комуникация с кандидата',
        evalSessions_evalSessionSheetsProject_communicationSessionNum: 'Номер на сесия',
        evalSessions_evalSessionSheetsProject_communicationStatus: 'Статус',
        evalSessions_evalSessionSheetsProject_regNumber: 'Рег. номер',
        evalSessions_evalSessionSheetsProject_questionDate: 'Дата на изпращане',
        evalSessions_evalSessionSheetsProject_questionEndingDate: 'Краен срок за отговор',
        evalSessions_evalSessionSheetsProject_answerDate: 'Дата на отговор',
        evalSessions_evalSessionSheetsProject_standpoints: 'Становища',
        evalSessions_evalSessionSheetsProject_standpointNote: 'Бележка',
        evalSessions_evalSessionSheetsProject_standpointUser: 'Член',
        evalSessions_evalSessionSheetsProject_standpointStatus: 'Статус',

        //evalSessions_editEvalSessionSheet
        evalSessions_editEvalSessionSheet_title: 'Преглед на оценителен лист',
        evalSessions_editEvalSessionSheet_canceled: 'Анулиран',
        evalSessions_editEvalSessionSheet_cancelSheetConfirm:
          'Сигурни ли сте, че искате да анулирате оценителния лист?',
        evalSessions_editEvalSessionSheet_cancelMessage: 'Причина за анулиране',
        evalSessions_editEvalSessionSheet_continueSheet: 'Продължи',
        evalSessions_editEvalSessionSheet_viewContinueSheet: 'Виж продължения лист',
        evalSessions_editEvalSessionSheet_continueSheetConfirm:
          'Сигурни ли сте, че искате да продължите оценителния лист?',
        evalSessions_editEvalSessionSheet_template: 'Оценителен лист',

        //evalSessions_evalSessionStandpointsSearch
        evalSessions_evalSessionStandpointsSearch_search: 'Търси',
        evalSessions_evalSessionStandpointsSearch_new: 'Ново становище',
        evalSessions_evalSessionStandpointsSearch_user: 'Член',
        evalSessions_evalSessionStandpointsSearch_note: 'Бележка',
        evalSessions_evalSessionStandpointsSearch_project: 'Проектно предложение',
        evalSessions_evalSessionStandpointsSearch_status: 'Статус',
        evalSessions_evalSessionStandpointsSearch_statuses: 'Статуси',

        //evalSessions_evalSessionStandpointForm
        evalSessions_evalSessionStandpointForm_userId: 'Член',
        evalSessions_evalSessionStandpointForm_projectId: 'Проектно предложение',
        evalSessions_evalSessionStandpointForm_note: 'Бележка',
        evalSessions_evalSessionStandpointForm_createDate: 'Дата на създаване',
        evalSessions_evalSessionStandpointForm_status: 'Статус',
        evalSessions_evalSessionStandpointForm_statusDate: 'Дата на промяна на статуса',
        evalSessions_evalSessionStandpointForm_deleteNote: 'Причина за анулиране',

        //evalSessions_newEvalSessionStandpoint
        evalSessions_newEvalSessionStandpoint_title: 'Ново становище',
        evalSessions_newEvalSessionStandpoint_save: 'Запис',
        evalSessions_newEvalSessionStandpoint_cancel: 'Отказ',

        //evalSessions_editEvalSessionStandpoint
        evalSessions_editEvalSessionStandpoint_title: 'Преглед на становище',
        evalSessions_editEvalSessionStandpoint_canceled: 'Анулиран',
        evalSessions_editEvalSessionStandpoint_cancelConfirm:
          'Сигурни ли сте, че искате да анулирате становището?',
        evalSessions_editEvalSessionStandpoint_cancelMessage: 'Причина за анулиране',
        evalSessions_editEvalSessionStandpoint_template: 'Становище',

        //evalSessions_evalSessionDistributionsSearch
        evalSessions_evalSessionDistributionsSearch_newASD: 'Ново разпр. за ОАСД',
        evalSessions_evalSessionDistributionsSearch_newTFO: 'Ново разпр. за ТФО',
        evalSessions_evalSessionDistributionsSearch_newComplex: 'Ново разпр. за КО',
        evalSessions_evalSessionDistributionsSearch_newPreliminary: 'Ново разпр. за ПО',
        evalSessions_evalSessionDistributionsSearch_code: 'Номер',
        evalSessions_evalSessionDistributionsSearch_createDate: 'Дата на създаване',
        evalSessions_evalSessionDistributionsSearch_type: 'Тип',
        evalSessions_evalSessionDistributionsSearch_status: 'Статус',
        evalSessions_evalSessionDistributionsSearch_assessorsPerProject: 'Брой оценители',

        //evalSessions_evalSessionDistributionForm
        evalSessions_evalSessionDistributionForm_type: 'Тип',
        evalSessions_evalSessionDistributionForm_status: 'Статус',
        evalSessions_evalSessionDistributionForm_assessorsPerProject: 'Брой оценители',
        evalSessions_evalSessionDistributionForm_code: 'Номер',
        evalSessions_evalSessionDistributionForm_createDate: 'Дата на създаване',
        evalSessions_evalSessionDistributionForm_statusNote: 'Причина за отказване',
        evalSessions_evalSessionDistributionForm_assessors: 'Оценители',
        evalSessions_evalSessionDistributionForm_assessor: 'Име',
        evalSessions_evalSessionDistributionForm_isDeleted: 'Включен',
        evalSessions_evalSessionDistributionForm_isDeletedNote: 'Причина за изключване',
        evalSessions_evalSessionDistributionForm_invalidAssessorCount:
          'Броят оценители не може да бъде по-голям от броя на включените оценители',

        //evalSessions_newEvalSessionDistribution
        evalSessions_newEvalSessionDistribution_title: 'Ново разпределение',
        evalSessions_newEvalSessionDistribution_save: 'Запис',
        evalSessions_newEvalSessionDistribution_cancel: 'Отказ',
        evalSessions_newEvalSessionDistribution_projects: 'Проектни предложения',
        evalSessions_newEvalSessionDistribution_projects_all: 'Всички',
        evalSessions_newEvalSessionDistribution_projects_none: 'Никои',
        evalSessions_newEvalSessionDistribution_isDeleted: 'Включено',
        evalSessions_newEvalSessionDistribution_isDeletedNote: 'Причина за изключване',
        evalSessions_newEvalSessionDistribution_type: 'Тип',
        evalSessions_newEvalSessionDistribution_procedureName: 'Бюджет',
        evalSessions_newEvalSessionDistribution_projectRegNumber: 'Номер на ПП',
        evalSessions_newEvalSessionDistribution_projectName: 'Наименование',
        evalSessions_newEvalSessionDistribution_companyName: 'Кандидат',
        evalSessions_newEvalSessionDistribution_projectRegistrationStatus: 'Статус',
        evalSessions_newEvalSessionDistribution_projectType: 'Тип на ПП',
        evalSessions_newEvalSessionDistribution_projectRegDate: 'Дата на регистрация',
        evalSessions_newEvalSessionDistribution_noProjects:
          'Не можете да запишете разпределението, ' + 'защото няма включени проектни предложения.',
        evalSessions_newEvalSessionDistribution_noIsDeletedNote:
          'Не можете да запишете разпределението, ' +
          'защото има изключени Проектни предложения без попълнена Причина за изключване.',

        //evalSessions_editEvalSessionDistribution
        evalSessions_editEvalSessionDistribution_title: 'Преглед на разпределение',
        evalSessions_editEvalSessionDistribution_applied: 'Приложено',
        evalSessions_editEvalSessionDistribution_refused: 'Отказано',
        evalSessions_editEvalSessionDistribution_edit: 'Редакция',
        evalSessions_editEvalSessionDistribution_save: 'Запис',
        evalSessions_editEvalSessionDistribution_cancel: 'Отказ',
        evalSessions_editEvalSessionDistribution_refuseConfirm:
          'Сигурни ли сте, че искате да откажете разпределението? ' +
          'Това ще анулира всички генерирани оценителни листа от това разпределение!',
        evalSessions_editEvalSessionDistribution_refuseMessage:
          'Причина за отказване на разпределението',
        evalSessions_editEvalSessionDistribution_projects: 'Проектни предложения',
        evalSessions_editEvalSessionDistribution_isDeleted: 'Включено',
        evalSessions_editEvalSessionDistribution_isDeletedNote: 'Причина за изключване',
        evalSessions_editEvalSessionDistribution_type: 'Тип',
        evalSessions_editEvalSessionDistribution_procedureName: 'Бюджет',
        evalSessions_editEvalSessionDistribution_projectRegNumber: 'Номер на ПП',
        evalSessions_editEvalSessionDistribution_projectName: 'Наименование',
        evalSessions_editEvalSessionDistribution_companyName: 'Кандидат',
        evalSessions_editEvalSessionDistribution_projectRegistrationStatus: 'Статус',
        evalSessions_editEvalSessionDistribution_projectType: 'Тип на ПП',
        evalSessions_editEvalSessionDistribution_projectRegDate: 'Дата на регистрация',

        //myEvalSessions_tabs
        myEvalSessions_tabs_evalSessionData: 'Основни данни',
        myEvalSessions_tabs_evalSessionSheets: 'Моите оценителни листове',
        myEvalSessions_tabs_evalSessionStandpoints: 'Моите становища',

        //myEvalSessions_viewEvalSession
        myEvalSessions_viewEvalSession_info:
          'Статус: {{status}}, Номер: {{number}}, Бюджет: {{procedure}}',

        //evalSessions_editEvalSessions
        myEvalSessions_editEvalSessions_title: 'Данни за оценителна сесия',

        //myEvalSessions_evalSessionSheetsSearch
        myEvalSessions_evalSessionSheetsSearch_new: 'Нов оценителен лист',
        myEvalSessions_evalSessionSheetsSearch_fullname: 'Член',
        myEvalSessions_evalSessionSheetsSearch_projectName: 'Проектно предложение',
        myEvalSessions_evalSessionSheetsSearch_type: 'Тип на етап',
        myEvalSessions_evalSessionSheetsSearch_status: 'Статус',
        myEvalSessions_evalSessionSheetsSearch_distributionType: 'Тип на разпределение',
        myEvalSessions_evalSessionSheetsSearch_pass: 'Преминава',
        myEvalSessions_evalSessionSheetsSearch_result: 'Точки',
        myEvalSessions_evalSessionSheetsSearch_note: 'Коментар',

        //myEvalSessions_evalSessionStandpointsProject
        myEvalSessions_evalSessionStandpointsProject_title: 'Преглед на проектно предложение',
        myEvalSessions_evalSessionStandpointsProject_back: 'Назад',
        myEvalSessions_evalSessionStandpointsProject_regData: 'Регистрационни данни',
        myEvalSessions_evalSessionStandpointsProject_history: 'История на промените',
        myEvalSessions_evalSessionStandpointsProject_projectStatus: 'Статус',
        myEvalSessions_evalSessionStandpointsProject_createDate: 'Дата на създаване',
        myEvalSessions_evalSessionStandpointsProject_createNote: 'Бележка',
        myEvalSessions_evalSessionStandpointsProject_modifyDate: 'Дата на последна промяна',
        myEvalSessions_evalSessionStandpointsProject_file: 'Файл',
        myEvalSessions_evalSessionStandpointsProject_signatures: 'Ел. подписи',
        myEvalSessions_evalSessionStandpointsProject_communication: 'Комуникация с кандидата',
        myEvalSessions_evalSessionStandpointsProject_otherCommunication:
          'Друга комуникация с кандидата',
        myEvalSessions_evalSessionStandpointsProject_communicationSessionNum: 'Номер на сесия',
        myEvalSessions_evalSessionStandpointsProject_communicationStatus: 'Статус',
        myEvalSessions_evalSessionStandpointsProject_regNumber: 'Рег. номер',
        myEvalSessions_evalSessionStandpointsProject_questionDate: 'Дата на изпращане',
        myEvalSessions_evalSessionStandpointsProject_questionEndingDate: 'Краен срок за отговор',
        myEvalSessions_evalSessionStandpointsProject_answerDate: 'Дата на отговор',
        myEvalSessions_evalSessionStandpointsProject_standpoints: 'Становища',
        myEvalSessions_evalSessionStandpointsProject_standpointNote: 'Бележка',
        myEvalSessions_evalSessionStandpointsProject_standpointUser: 'Член',
        myEvalSessions_evalSessionStandpointsProject_standpointStatus: 'Статус',

        //myEvalSessions_editEvalSessionSheet
        myEvalSessions_editEvalSessionSheet_title: 'Преглед на оценителен лист',

        //evalSessions_evalSessionEvaluationForm
        evalSessions_evalSessionEvaluationForm_projectId: 'Проектно предложение',
        evalSessions_evalSessionEvaluationForm_type: 'Тип на оценка',
        evalSessions_evalSessionEvaluationForm_createDate: 'Дата на създаване',
        evalSessions_evalSessionEvaluationForm_isDeleted: 'Анулиран',
        evalSessions_evalSessionEvaluationForm_isDeletedNote: 'Причина за анулиране',
        evalSessions_evalSessionEvaluationForm_hasntEvaluation:
          'Проектното предложение няма оценки!',
        evalSessions_evalSessionEvaluationForm_hasAdminAdmiss:
          'Проектното предложение има оценка на административното съответствие и допустимостта.',
        evalSessions_evalSessionEvaluationForm_hasTechFinance:
          'Проектното предложение има техническа и финансова оценка.',
        evalSessions_evalSessionEvaluationForm_sheets: 'Оценителни листа',
        evalSessions_evalSessionEvaluationForm_endedSheetsCount: 'Брой приключени',
        evalSessions_evalSessionEvaluationForm_canceledSheetsCount: 'Брой анулирани',
        evalSessions_evalSessionEvaluationForm_pausedSheetsCount: 'Брой прекъснати',
        evalSessions_evalSessionEvaluationForm_assessor: 'Член',
        evalSessions_evalSessionEvaluationForm_project: 'Проектно предложение',
        evalSessions_evalSessionEvaluationForm_sheetType: 'Тип на етап',
        evalSessions_evalSessionEvaluationForm_distribution: 'Разпределение',
        evalSessions_evalSessionEvaluationForm_status: 'Статус',
        evalSessions_evalSessionEvaluationForm_distributionType: 'Тип на разпределение',
        evalSessions_evalSessionEvaluationForm_pass: 'Преминава',
        evalSessions_evalSessionEvaluationForm_result: 'Точки',
        evalSessions_evalSessionEvaluationForm_note: 'Основания',
        evalSessions_evalSessionEvaluationForm_sheet: 'Лист',
        evalSessions_evalSessionEvaluationForm_document: 'Структуриран документ',
        evalSessions_evalSessionEvaluationForm_total: 'Обобщена оценка',
        evalSessions_evalSessionEvaluationForm_calculationType: 'Тип на обобщаването',
        evalSessions_evalSessionEvaluationForm_isPassed: 'Преминава',
        evalSessions_evalSessionEvaluationForm_points: 'Точки',
        evalSessions_evalSessionEvaluationForm_cannotEvaluate:
          'Обобщената оценка не може да бъде автоматично пресметната!',
        evalSessions_evalSessionEvaluationForm_cannotSave:
          'Обобщената оценка не може да бъде запазена, ' +
          "защото има оценителен/и лист/а със статус 'Чернова'",

        //evalSessions_evalSessionEvaluationNew
        evalSessions_evalSessionEvaluationNew_title: 'Нова обобщена оценка',
        evalSessions_evalSessionEvaluationNew_save: 'Запис',
        evalSessions_evalSessionEvaluationNew_cancel: 'Отказ',

        //evalSessions_evalSessionEvaluationEdit
        evalSessions_evalSessionEvaluationEdit_title: 'Преглед на обобщена оценка',
        evalSessions_evalSessionEvaluationEdit_back: 'Назад',
        evalSessions_evalSessionEvaluationEdit_deleteEvaluation: 'Анулиране',
        evalSessions_evalSessionEvaluationEdit_deleteConfirm:
          'Сигурни ли сте, че искате да анулирате тази обобщена оценка ?',
        evalSessions_evalSessionEvaluationEdit_deleteMessage: 'Причина за анулиране',

        //evalSessions_evalSessionCommunicationsSearch
        evalSessions_evalSessionCommunicationsSearch_project: 'Проектно предложение',
        evalSessions_evalSessionCommunicationsSearch_status: 'Статус',
        evalSessions_evalSessionCommunicationsSearch_questionDateFrom: 'Дата от',
        evalSessions_evalSessionCommunicationsSearch_questionDateTo: 'Дата до',
        evalSessions_evalSessionCommunicationsSearch_search: 'Търси',
        evalSessions_evalSessionCommunicationsSearch_excelExport: 'Експорт',
        evalSessions_evalSessionCommunicationsSearch_createDate: 'Дата на създаване',
        evalSessions_evalSessionCommunicationsSearch_regNumber: 'Номер',
        evalSessions_evalSessionCommunicationsSearch_questionDate: 'Дата на въпрос',
        evalSessions_evalSessionCommunicationsSearch_questionEndingDate: 'Срок на отговор',
        evalSessions_evalSessionCommunicationsSearch_answerDate: 'Дата на отговор',
        evalSessions_evalSessionCommunicationsSearch_questionReadDate: 'Дата на отваряне',
        evalSessions_evalSessionCommunicationsSearch_companyName: 'Кандидат',
        evalSessions_evalSessionCommunicationsSearch_projectName: 'ПП',
        evalSessions_evalSessionCommunicationsSearch_projectRegNumber: '№ ПП',

        //evalSessions_evalSessionDocumentForm
        evalSessions_evalSessionDocumentForm_userId: 'Потребител',
        evalSessions_evalSessionDocumentForm_type: 'Тип',
        evalSessions_evalSessionDocumentForm_position: 'Позиция',

        //evalSessions_evalSessionAllDocsSearch
        evalSessions_evalSessionAllDocsSearch_reports: 'Протоколи/Доклади/Решения',
        evalSessions_evalSessionAllDocsSearch_reportCreateDate: 'Дата на създаване',
        evalSessions_evalSessionAllDocsSearch_reportType: 'Тип',
        evalSessions_evalSessionAllDocsSearch_regNumber: 'Номер',
        evalSessions_evalSessionAllDocsSearch_reportDescription: 'Описание',
        evalSessions_evalSessionAllDocsSearch_reportIsDeleted: 'Анулиран',
        evalSessions_evalSessionAllDocsSearch_reportIsDeletedNote: 'Причина за анулиране',
        evalSessions_evalSessionAllDocsSearch_docs: 'Документи',
        evalSessions_evalSessionAllDocsSearch_docName: 'Наименование',
        evalSessions_evalSessionAllDocsSearch_docDescription: 'Описание',
        evalSessions_evalSessionAllDocsSearch_docFile: 'Файл',
        evalSessions_evalSessionAllDocsSearch_docIsDeleted: 'Анулиран',
        evalSessions_evalSessionAllDocsSearch_docIsDeletedNote: 'Причина за анулиране',

        //evalSessions_newEvalSessionDocument
        evalSessions_newEvalSessionDocument_title: 'Нов документ',
        evalSessions_newEvalSessionDocument_save: 'Запис',
        evalSessions_newEvalSessionDocument_cancel: 'Отказ',

        //evalSessions_editEvalSessionDocument
        evalSessions_editEvalSessionDocument_title: 'Редакция на документ',
        evalSessions_editEvalSessionDocument_edit: 'Редакция',
        evalSessions_editEvalSessionDocument_deleteDocument: 'Анулиране',
        evalSessions_editEvalSessionDocument_save: 'Запис',
        evalSessions_editEvalSessionDocument_cancel: 'Отказ',

        //evalSessions_evalSessionReportForm
        evalSessions_evalSessionReportForm_type: 'Тип',
        evalSessions_evalSessionReportForm_regNumber: 'Номер',
        evalSessions_evalSessionReportForm_isDeleted: 'Анулиран',
        evalSessions_evalSessionReportForm_description: 'Описание',
        evalSessions_evalSessionReportForm_isDeletedNote: 'Причина за анулиране',
        evalSessions_evalSessionReportForm_createDate: 'Дата на създаване',
        evalSessions_evalSessionReportForm_modifyDate: 'Дата на промяна',

        //evalSessions_newEvalSessionReport
        evalSessions_newEvalSessionReport_title: 'Нов документ',
        evalSessions_newEvalSessionReport_save: 'Запис',
        evalSessions_newEvalSessionReport_cancel: 'Отказ',

        //evalSessions_editEvalSessionReport
        evalSessions_editEvalSessionReport_title: 'Преглед на документ',
        evalSessions_editEvalSessionReport_delete: 'Анулиране',
        evalSessions_editEvalSessionReport_deleteConfirm:
          'Сигурни ли сте, че искате да анулирате този документ?',
        evalSessions_editEvalSessionReport_deleteMessage: 'Причина за анулиране',
        evalSessions_editEvalSessionReport_projects: 'Проектни предложения',
        evalSessions_editEvalSessionReport_excelExport: 'Експорт',
        evalSessions_editEvalSessionReport_regNumber: 'ПП рег. номер',
        evalSessions_editEvalSessionReport_regDate: 'Дата и час на рег.',
        evalSessions_editEvalSessionReport_recieveDate: 'Дата и час на получаване',
        evalSessions_editEvalSessionReport_recieveType: 'Начин на получаване',
        evalSessions_editEvalSessionReport_company: 'Кандидат',
        evalSessions_editEvalSessionReport_companyUin: 'УИН',
        evalSessions_editEvalSessionReport_companyName: 'Наименование',
        evalSessions_editEvalSessionReport_companyKidCode: 'КО по КИД 2008',
        evalSessions_editEvalSessionReport_regEmail: 'Ел.поща за контакт',
        evalSessions_editEvalSessionReport_correspondence: 'Адрес за кореспонденция',
        evalSessions_editEvalSessionReport_project: 'Проектно предложение',
        evalSessions_editEvalSessionReport_name: 'Наименование',
        evalSessions_editEvalSessionReport_projectKidCode: 'КП по КИД 2008',
        evalSessions_editEvalSessionReport_companySizeType: 'Категория на предприятието',
        evalSessions_editEvalSessionReport_duration: 'Продължителност в месеци',
        evalSessions_editEvalSessionReport_projectPlace: 'Място на изпълнение',
        evalSessions_editEvalSessionReport_grandAmount: 'Общ размер на БФП (лв.)',
        evalSessions_editEvalSessionReport_coFinancingAmount: 'Общ размер на съфинансиране (лв.)',
        evalSessions_editEvalSessionReport_partners: 'Партньори',
        evalSessions_editEvalSessionReport_partnerUin: 'УИН',
        evalSessions_editEvalSessionReport_partnerName: 'Наименование',
        evalSessions_editEvalSessionReport_partnerLegalType: 'Вид на организацията',
        evalSessions_editEvalSessionReport_partnerAddress: 'Адрес',
        evalSessions_editEvalSessionReport_partnerFinancialContribution: 'Финансово участие',
        evalSessions_editEvalSessionReport_partnerRepresentative: 'Представител',
        evalSessions_editEvalSessionReport_adminAdmissResult: 'Резултат от ОАСД',
        evalSessions_editEvalSessionReport_techFinanceResult: 'Резултат от ТФО',
        evalSessions_editEvalSessionReport_complexResult: 'Резултат от КО',
        evalSessions_editEvalSessionReport_status: 'Статус',
        evalSessions_editEvalSessionReport_points: 'Точки',
        evalSessions_editEvalSessionReport_standing: 'Класиране',
        evalSessions_editEvalSessionReport_standingNum: 'Пореден номер',
        evalSessions_editEvalSessionReport_standingStatus: 'Статус',
        evalSessions_editEvalSessionReport_standingGrandAmount: 'БФП след корекции (лв.)',

        //evalSessions_evalSessionProjectStandingForm
        evalSessions_evalSessionProjectStandingForm_projectId: 'Проектно предложение',
        evalSessions_evalSessionProjectStandingForm_orderNum: 'Пореден номер',
        evalSessions_evalSessionProjectStandingForm_isPreliminary: 'Предварително',
        evalSessions_evalSessionProjectStandingForm_status: 'Статус',
        evalSessions_evalSessionProjectStandingForm_standingType: 'Тип',
        evalSessions_evalSessionProjectStandingForm_grandAmount: 'Одобрено БФП (лв.)',
        evalSessions_evalSessionProjectStandingForm_notes: 'Бележки',
        evalSessions_evalSessionProjectStandingForm_isDeleted: 'Анулирано',
        evalSessions_evalSessionProjectStandingForm_isDeletedNote: 'Причина за анулиране',
        evalSessions_evalSessionProjectStandingForm_orderNumExists:
          'Съществува неанулирано класиране със същия пореден номер',
        evalSessions_evalSessionProjectStandingForm_evaluations: 'Обобщени оценки',
        evalSessions_evalSessionProjectStandingForm_type: 'Тип на етап',
        evalSessions_evalSessionProjectStandingForm_calculationType: 'Тип на обобщаването',
        evalSessions_evalSessionProjectStandingForm_pass: 'Преминава',
        evalSessions_evalSessionProjectStandingForm_result: 'Точки',
        evalSessions_evalSessionProjectStandingForm_note: 'Коментар',
        evalSessions_evalSessionProjectStandingForm_createDate: 'Дата на създаване',
        evalSessions_evalSessionProjectStandingForm_projectVersion:
          'Версия на проектното предложение',
        evalSessions_evalSessionProjectStandingForm_template: 'Проектно предложение',
        evalSessions_evalSessionProjectStandingForm_projectStanding: 'Класиране',
        evalSessions_evalSessionProjectStandingForm_rejectionReason:
          'Причина за отхвърляне/отпадане/ниска оценка на проекта',
        evalSessions_evalSessionProjectStandingForm_procedureBudgetComponent: 'Бюджетен компонент',

        //evalSessions_evalSessionProjectStandingNew
        evalSessions_evalSessionProjectStandingNew_title: 'Ново класиране',
        evalSessions_evalSessionProjectStandingNew_save: 'Запис',
        evalSessions_evalSessionProjectStandingNew_cancel: 'Отказ',

        //evalSessions_evalSessionProjectStandingEdit
        evalSessions_evalSessionProjectStandingEdit_title: 'Преглед на класиране',
        evalSessions_evalSessionProjectStandingEdit_back: 'Назад',
        evalSessions_evalSessionProjectStandingEdit_deleteStanding: 'Анулиране',
        evalSessions_evalSessionProjectStandingEdit_deleteConfirm:
          'Сигурни ли сте, че искате да анулирате това класиране?',
        evalSessions_evalSessionProjectStandingEdit_deleteMessage: 'Причина за анулиране',

        //evalSessions_evalSessionStandingsSearch
        evalSessions_evalSessionStandingsSearch_newASD: 'Ново двуетапно класиране',
        evalSessions_evalSessionStandingsSearch_newComplex: 'Ново комплексно класиране',
        evalSessions_evalSessionStandingsSearch_newPreliminary: 'Ново предварително класиране',
        evalSessions_evalSessionStandingsSearch_code: 'Номер',
        evalSessions_evalSessionStandingsSearch_isPreliminary: 'Предварително',
        evalSessions_evalSessionStandingsSearch_status: 'Статус',
        evalSessions_evalSessionStandingsSearch_statusDate: 'Дата на създаване/отказване',
        evalSessions_evalSessionStandingsSearch_statusNote: 'Причина за отказване',

        //evalSessions_evalSessionStandingForm
        evalSessions_evalSessionStandingForm_code: 'Номер',
        evalSessions_evalSessionStandingForm_isPreliminary: 'Предварително',
        evalSessions_evalSessionStandingForm_preliminaryBudgetPercentage: 'Размер на бюджета в %',
        evalSessions_evalSessionStandingForm_standingStatus: 'Статус',
        evalSessions_evalSessionStandingForm_statusDate: 'Дата на създаване/отказване',
        evalSessions_evalSessionStandingForm_statusNote: 'Причина за отказване',
        evalSessions_evalSessionStandingForm_projects: 'Проектни предложения',
        evalSessions_evalSessionStandingForm_orderNum: 'Пореден номер',
        evalSessions_evalSessionStandingForm_status: 'Статус',
        evalSessions_evalSessionStandingForm_grandAmount: 'Одобрено БФП (лв.)',
        evalSessions_evalSessionStandingForm_procedureName: 'Бюджет',
        evalSessions_evalSessionStandingForm_projectRegNumber: 'Номер на ПП',
        evalSessions_evalSessionStandingForm_projectName: 'Наименование',
        evalSessions_evalSessionStandingForm_companyName: 'Кандидат',
        evalSessions_evalSessionStandingForm_projectRegDate: 'Дата на регистрация',
        evalSessions_evalSessionStandingForm_projectRegistrationStatus: 'Статус',
        evalSessions_evalSessionStandingForm_isStandingDeleted: 'Анулирано класиране',
        evalSessions_evalSessionStandingForm_type: 'Тип',
        evalSessions_evalSessionStandingForm_eval: 'Обобщена оценка',
        evalSessions_evalSessionStandingForm_ASD: 'ОАСД',
        evalSessions_evalSessionStandingForm_TFO: 'ТФО',
        evalSessions_evalSessionStandingForm_Complex: 'КО',
        evalSessions_evalSessionStandingForm_Preliminary: 'ПО',
        evalSessions_evalSessionStandingForm_pass: 'Преминава',
        evalSessions_evalSessionStandingForm_points: 'Точки',
        evalSessions_evalSessionStandingForm_procedureBudgetComponentName: 'Бюджетен компонент',
        evalSessions_evalSessionStandingForm_show: 'Покажи',

        //evalSessions_newEvalSessionStanding
        evalSessions_newEvalSessionStanding_title: 'Ново класиране',
        evalSessions_newEvalSessionStanding_save: 'Запис',
        evalSessions_newEvalSessionStanding_cancel: 'Отказ',

        //evalSessions_editEvalSessionStanding
        evalSessions_editEvalSessionStanding_title: 'Преглед на класиране',
        evalSessions_editEvalSessionStanding_refused: 'Отказано',
        evalSessions_editEvalSessionStanding_refuseConfirm:
          'Сигурни ли сте, че искате да откажете класирането?',
        evalSessions_editEvalSessionStanding_refuseMessage: 'Причина за отказване на класирането',
        evalSessions_editEvalSessionStanding_rearrange: 'Разместване',

        //evalSessions_rearrangeEvalSessionStanding
        evalSessions_rearrangeEvalSessionStanding_save: 'Приложи',
        evalSessions_rearrangeEvalSessionStanding_cancel: 'Отказ',
        evalSessions_rearrangeEvalSessionStanding_title: 'Разместване на класиране',
        evalSessions_rearrangeEvalSessionStanding_applyConfirm:
          'Прилагането на резултата от разместване ще замени актуалното класиране. Желаете ли да продължите?',

        //evalSessions_newEvalSessionResult
        evalSessions_newEvalSessionResult_title: 'Нов резултат от оценка',
        evalSessions_newEvalSessionResult_save: 'Запис',
        evalSessions_newEvalSessionResult_cancel: 'Отказ',
        evalSessions_newEvalSessionResult_orderNum: 'Пореден номер',
        evalSessions_newEvalSessionResult_status: 'Статус',
        evalSessions_newEvalSessionResult_type: 'Вид на резултата',
        evalSessions_newEvalSessionResult_createDate: 'Дата на създаване',

        //evalSessions_editEvalSessionResult
        evalSessions_editEvalSessionResult_title: 'Преглед на резултат от оценка',
        evalSessions_editEvalSessionResult_publish: 'Публикуване',
        evalSessions_editEvalSessionResult_publishConfirm:
          'Сигурни ли сте, че искате да публикувате резултата?',
        evalSessions_editEvalSessionResult_cancel: 'Анулиране',
        evalSessions_editEvalSessionResult_cancelConfirm:
          'Сигурни ли сте, че искате да анулирате публикуването на резултата?',
        evalSessions_editEvalSessionResult_cancelMessage: 'Причина за анулиране на публикуването',
        evalSessions_editEvalSessionResult_orderNum: 'Пореден номер',
        evalSessions_editEvalSessionResult_status: 'Статус',
        evalSessions_editEvalSessionResult_createDate: 'Дата на създаване',
        evalSessions_editEvalSessionResult_projects: 'Проектни предложения',
        evalSessions_editEvalSessionResult_loadProjects: 'Зареди резулати',
        evalSessions_editEvalSessionResult_projectNumber: 'Номер',
        evalSessions_editEvalSessionResult_projectName: 'Наименование',
        evalSessions_editEvalSessionResult_projectRegDate: 'Дата на регистрация',
        evalSessions_editEvalSessionResult_companyName: 'Кандидат',
        evalSessions_editEvalSessionResult_companyUin: 'Идентификатор на кандидат',
        evalSessions_editEvalSessionResult_adminAdmissResult: 'Резултат от ОАСД',
        evalSessions_editEvalSessionResult_nonAdmissionReason: 'Основание за недопускане',
        evalSessions_editEvalSessionResult_clearProjects: 'Изчисти резултати',
        evalSessions_editEvalSessionResult_publishedUser: 'Публикуван от потребител',
        evalSessions_editEvalSessionResult_publishedDate: 'Дата на публикуване',
        evalSessions_editEvalSessionResult_type: 'Вид резултат',
        evalSessions_editEvalSessionResult_grantAmountPreliminary: 'Размер на БФП (лв.)',
        evalSessions_editEvalSessionResult_selfAmountPreliminary: 'Размер на съфинансиране (лв.)',
        evalSessions_editEvalSessionResult_grantAmount: 'Размер на заявеното БФП (лв.)',
        evalSessions_editEvalSessionResult_selfAmount: 'Размер на заявеното съфинансиране (лв.)',
        evalSessions_editEvalSessionResult_preliminaryResult: 'Резултат от ПО',
        evalSessions_editEvalSessionResult_preliminaryPoints: 'Точки от ПО',
        evalSessions_editEvalSessionResult_preliminaryStandingNumber: 'ПO пореден номер',
        evalSessions_editEvalSessionResult_preliminaryStandingStatus: 'ПO статус',
        evalSessions_editEvalSessionResult_standingNumber: 'Пореден номер',
        evalSessions_editEvalSessionResult_standingStatus: 'Статус',
        evalSessions_editEvalSessionResult_techFinResult: 'Резултат от ТФО',
        evalSessions_editEvalSessionResult_techFinPoints: 'Точки от ТФО',
        evalSessions_editEvalSessionResult_complexResult: 'Резултат от КО',
        evalSessions_editEvalSessionResult_complexPoints: 'Точки от КО',
        evalSessions_editEvalSessionResult_correctedGrantAmount:
          'Размер на коригираното БФП от оценителна комисия (лв.)',
        evalSessions_editEvalSessionResult_correctedSelfAmount:
          'Размер на коригираното съфинансиране от оценителна комисия (лв.)',
        evalSessions_editEvalSessionResult_evalType: 'Тип',
        evalSessions_editEvalSessionResult_pass: 'Преминавва',
        evalSessions_editEvalSessionResult_points: 'Точки',
        evalSessions_editEvalSessionResult_ASD: 'ОАСД',
        evalSessions_editEvalSessionResult_TFO: 'ТФО',
        evalSessions_editEvalSessionResult_Complex: 'КО',
        evalSessions_editEvalSessionResult_Preliminary: 'ПО',
        evalSessions_editEvalSessionResult_eval: 'Обобщена оценка',
        evalSessions_editEvalSessionResult_preliminaryStandingNote: 'Доп. информация',

        //evalSessions_evalSessionResultSearch
        evalSessions_evalSessionResultSearch_newAdminAdmiss: 'Нов резултат от ОАСД',
        evalSessions_evalSessionResultSearch_newStanding: 'Нов резултат от класиране',
        evalSessions_evalSessionResultSearch_newPreliminary: 'Нов резултат от ПО',
        evalSessions_evalSessionResultSearch_orderNum: 'Пореден номер',
        evalSessions_evalSessionResultSearch_status: 'Статус',
        evalSessions_evalSessionResultSearch_createDate: 'Дата на създаване',
        evalSessions_evalSessionResultSearch_statusNote: 'Причина за отказване',
        evalSessions_evalSessionResultSearch_type: 'Вид резултат',

        //evalSessions_chooseMonitorstatRequestCompaniesModal
        evalSessions_chooseMonitorstatRequestCompaniesModal_title:
          'Изпращане на заявки към Мониторстат',
        evalSessions_chooseMonitorstatRequestCompaniesModal_send: 'Изпращане',
        evalSessions_chooseMonitorstatRequestCompaniesModal_cancel: 'Отказ',
        evalSessions_chooseMonitorstatRequestCompaniesModal_all: 'Всички',
        evalSessions_chooseMonitorstatRequestCompaniesModal_companyName: 'Бенефициент/Партньор',
        evalSessions_chooseMonitorstatRequestCompaniesModal_companyUin: 'Идентификатор',
        evalSessions_chooseMonitorstatRequestCompaniesModal_procedureMonitorstatRequest:
          'Изследване',
        evalSessions_chooseMonitorstatRequestCompaniesModal_declaration: 'Декларация',
        evalSessions_chooseMonitorstatRequestCompaniesModal_eDeclaration: 'Е-Деклрация',

        //evalSessions_chooseProjectsForMonitorstatRequestModal
        evalSessions_chooseProjectsForMonitorstatRequestModal_title: 'Изпращане към НСИ',
        evalSessions_chooseProjectsForMonitorstatRequestModal_continue: 'Продължи',
        evalSessions_chooseProjectsForMonitorstatRequestModal_cancel: 'Отказ',
        evalSessions_chooseProjectsForMonitorstatRequestModal_all: 'Всички',
        evalSessions_chooseProjectsForMonitorstatRequestModal_file: 'Файл',
        evalSessions_chooseProjectsForMonitorstatRequestModal_chooseFile: 'Зареди ПП от Excel',
        evalSessions_chooseProjectsForMonitorstatRequestModal_template: 'Свали шаблон',
        evalSessions_chooseProjectsForMonitorstatRequestModal_regNumber: 'Номер на ПП',
        evalSessions_chooseProjectsForMonitorstatRequestModal_name: 'Наименование',
        evalSessions_chooseProjectsForMonitorstatRequestModal_projectKidCode: 'КП по КИД 2008',
        evalSessions_chooseProjectsForMonitorstatRequestModal_company: 'Кандидат',
        evalSessions_chooseProjectsForMonitorstatRequestModal_registrationStatus:
          'Регистрационен статус',
        evalSessions_chooseProjectsForMonitorstatRequestModal_projectType: 'Тип',
        evalSessions_chooseProjectsForMonitorstatRequestModal_inquiry: 'Изследване',
        evalSessions_chooseProjectsForMonitorstatRequestModal_declaration: 'Декларация за НСИ',
        evalSessions_chooseProjectsForMonitorstatRequestModal_eDeclaration: 'Е-декларация за НСИ',
        evalSessions_chooseProjectsForMonitorstatRequestModal_confirm:
          'Сигурни ли сте, че искате да изпратите заявки към НСИ за избраните проектни предложения?',

        //news_searchForm
        news_searchForm_dateFrom: 'Дата на публикуване',
        news_searchForm_dateTo: 'Дата на валидност',
        news_searchForm_type: 'Тип',
        news_searchForm_status: 'Статус',
        news_searchForm_search: 'Търси',
        news_searchForm_new: 'Нова новина',
        news_searchForm_createDate: 'Дата на създаване',
        news_searchForm_title: 'Заглавие',
        news_searchForm_creator: 'Създал',

        //news_newsDataForm
        news_newsDataForm_type: 'Тип',
        news_newsDataForm_dateFrom: 'Дата на публикуване',
        news_newsDataForm_dateTo: 'Дата на валидност',
        news_newsDataForm_title: 'Заглавие',
        news_newsDataForm_titleAlt: 'Заглавие на английски',
        news_newsDataForm_titleMaxlength: 'Заглавието може да съдържа максимум 200 символа',
        news_newsDataForm_content: 'Съдържание',
        news_newsDataForm_contentAlt: 'Съдържание на английски',
        news_newsDataForm_author: 'Автор',
        news_newsDataForm_authorAlt: 'Автор на английски',
        news_newsDataForm_authorMaxlength: 'Авторът може да съдържа максимум 200 символа',
        news_newsDataForm_emailNotification: 'Нотификация по email',
        news_newsDataForm_files: 'Файлове',
        news_newsDataForm_noFiles: 'Няма',
        news_newsDataForm_fileDescr: 'Описание',
        news_newsDataForm_fileDescrMaxlength: 'Описанието може да съдържа максимум 200 символа',
        news_newsDataForm_file: 'Файл',
        news_newsDataForm_status: 'Статус',
        news_newsDataForm_createDate: 'Дата на създаване',
        news_newsDataForm_createdByUser: 'Създал',

        //news_newForm
        news_newForm_title: 'Нова новина',
        news_newForm_save: 'Запис',
        news_newForm_cancel: 'Отказ',

        //news_editForm
        news_editForm_title: 'Преглед на новина',
        news_editForm_edit: 'Редакция',
        news_editForm_publish: 'Публикуване',
        news_editForm_delete: 'Изтриване',
        news_editForm_draft: 'Чернова',
        news_editForm_draftConfirm: 'Сигурни ли сте че искате да върнете новината в чернова?',
        news_editForm_archive: 'Архивирана',
        news_editForm_archiveConfirm: 'Сигурни ли сте че искате да архивирате новината?',
        news_editForm_save: 'Запис',
        news_editForm_cancel: 'Отказ',

        //news_publishNewsModal
        news_publishNewsModal_title: 'Публикуване на новина',
        news_publishNewsModal_publish: 'Публикуване',
        news_publishNewsModal_cancel: 'Отказ',
        news_publishNewsModal_dateFrom: 'Дата на публикуване',
        news_publishNewsModal_dateTo: 'Дата на валидност',
        news_publishNewsModal_dateToValid:
          'Датата не може да бъде по-малка от датата на публикуване',

        //news_newsFeed
        news_newsFeed_title: 'Актуални новини',
        news_newsFeed_allNews: 'Всички новини',
        news_newsFeed_by: 'от',
        news_newsFeed_noNews: 'Няма новини',

        //news_allNews
        news_allNews_title: 'Всички новини',
        news_allNews_by: 'от',
        news_allNews_noNews: 'Няма новини',

        //guidances_searchForm
        guidances_searchForm_new: 'Ново ръководство',
        guidances_searchForm_module: 'Модул',
        guidances_searchForm_createDate: 'Дата на създаване',
        guidances_searchForm_description: 'Описание',
        guidances_searchForm_creator: 'Създал',
        guidances_searchForm_file: 'Файл',

        //guidances_guidanceDataForm
        guidances_guidanceDataForm_description: 'Описание',
        guidances_guidanceDataForm_descriptionMaxlength:
          'Описанието може да съдържа максимум 200 символа',
        guidances_guidanceDataForm_module: 'Модул',
        guidances_guidanceDataForm_file: 'Файл',
        guidances_guidanceDataForm_createDate: 'Дата на създаване',
        guidances_guidanceDataForm_createdByUser: 'Създал',

        //guidances_newForm
        guidances_newForm_title: 'Ново ръководство',
        guidances_newForm_save: 'Запис',
        guidances_newForm_cancel: 'Отказ',

        //guidances_editForm
        guidances_editForm_title: 'Преглед на ръководство',
        guidances_editForm_edit: 'Редакция',
        guidances_editForm_delete: 'Изтриване',
        guidances_editForm_save: 'Запис',
        guidances_editForm_cancel: 'Отказ',

        //messages_view
        messages_view_inbox: 'Входящи',
        messages_view_sent: 'Изпратени',
        messages_view_draft: 'Чернова',
        messages_view_archive: 'Архивирани',

        //messages_search
        messages_search_newMessage: 'Ново съобщение',

        //messages_searchInbox
        messages_searchInbox_by: 'от',
        messages_searchInbox_noMessages: 'Няма входящи съобщения',

        //messages_searchArchive
        messages_searchArchive_by: 'от',
        messages_searchArchive_noMessages: 'Няма архивирани съобщения',

        //messages_viewIngoing
        messages_viewIngoing_archive: 'Архивирай',
        messages_viewIngoing_confirmArchive: 'Сигурни ли сте че искате да архивирате съобщението?',
        messages_viewIngoing_by: 'от',
        messages_viewIngoing_to: 'до',

        //messages_searchSent
        messages_searchSent_to: 'до',
        messages_searchSent_noMessages: 'Няма изпратени съобщения',

        //messages_viewSent
        messages_viewSent_to: 'до',

        //messages_searchDraft
        messages_searchDraft_to: 'до',
        messages_searchDraft_noMessages: 'Няма чернови',

        //messages_messageDataForm
        messages_messageDataForm_sender: 'От',
        messages_messageDataForm_recipients: 'До',
        messages_messageDataForm_createDate: 'Дата на създаване',
        messages_messageDataForm_title: 'Заглавие',
        messages_messageDataForm_titleMaxlength: 'Заглавието може да съдържа максимум 200 символа',
        messages_messageDataForm_content: 'Съдържание',
        messages_messageDataForm_files: 'Файлове',
        messages_messageDataForm_noFiles: 'Няма',
        messages_messageDataForm_fileDescr: 'Описание',
        messages_messageDataForm_fileDescrMaxlength:
          'Описанието може да съдържа максимум 200 символа',
        messages_messageDataForm_file: 'Файл',

        //messages_newForm
        messages_newForm_title: 'Ново съобщение',
        messages_newForm_save: 'Запис',
        messages_newForm_cancel: 'Отказ',

        //messages_editForm
        messages_editForm_title: 'Редакция на съобщение',
        messages_editForm_edit: 'Редакция',
        messages_editForm_delete: 'Изтриване',
        messages_editForm_send: 'Изпрати',
        messages_editForm_save: 'Запис',
        messages_editForm_cancel: 'Отказ',

        //notificationSetting_notificationSettingsSearch
        notificationSetting_notificationSettingsSearch_new: 'Нова настройка за нотификация',
        notificationSetting_notificationSettingsSearch_copy: 'Копирай настройките от потребител',
        notificationSetting_notificationSettingsSearch_event: 'Събитие',
        notificationSetting_notificationSettingsSearch_scope: 'Обхват',
        notificationSetting_notificationSettingsSearch_status: 'Статус',
        notificationSetting_notificationSettingsSearch_createDate: 'Дата на създаване',

        //notificationSetting_notificationSettingForm
        notificationSetting_notificationSettingForm_event: 'Събитие',
        notificationSetting_notificationSettingForm_scope: 'Обхват',
        notificationSetting_notificationSettingForm_programme: 'Основна организация',
        notificationSetting_notificationSettingForm_createDate: 'Дата на създаване',
        notificationSetting_notificationSettingForm_modifyDate: 'Дата на промяна',

        //notificationSettings_newNotificationSetting
        notificationSetting_notificationSettingsNew_title: 'Настройка за нотификация',
        notificationSetting_notificationSettingsNew_save: 'Съхрани',
        notificationSetting_notificationSettingsNew_cancel: 'Откажи',

        //notificationSetting_notificationSettingsEdit
        notificationSetting_notificationSettingsEdit_title: 'Редакция на настройка за нотификация',
        notificationSetting_notificationSettingsEdit_edit: 'Редакция',
        notificationSetting_notificationSettingsEdit_del: 'Изтриване',
        notificationSetting_notificationSettingsEdit_save: 'Съхрани',
        notificationSetting_notificationSettingsEdit_cancel: 'Отказ',
        notificationSetting_notificationSettingsEdit_confirmChangeStatus:
          "Сигурни ли сте, че искате да промените статуса на '{{status}}'",
        notificationSetting_notificationSettingsEdit_actual: 'Актуален',
        notificationSetting_notificationSettingsEdit_draft: 'Чернова',

        //notificationSetting_chooseNSContractsModal
        notificationSetting_chooseNSContractsModal_title: 'Избор на договор',
        notificationSetting_chooseNSContractsModal_continue: 'Продължи',
        notificationSetting_chooseNSContractsModal_cancel: 'Отказ',
        notificationSetting_chooseNSContractsModal_all: 'Всички',
        notificationSetting_chooseNSContractsModal_procedure: 'Бюджет',
        notificationSetting_chooseNSContractsModal_regNumber: 'Номер',
        notificationSetting_chooseNSContractsModal_contractDate: 'Дата',
        notificationSetting_chooseNSContractsModal_name: 'Наименование',
        notificationSetting_chooseNSContractsModal_executionStatus: 'Статус на изпълнение',
        notificationSetting_chooseNSContractsModal_company: 'Бенефициент',
        notificationSetting_chooseNSContractsModal_totalBfpAmount: 'БФП-Общо',
        notificationSetting_chooseNSContractsModal_totalSelfAmount: 'Собств. съфинансиране',

        //notificationSetting_chooseUserModal
        notificationSetting_chooseUserModal_title: 'Копиране на настройки от потребител',
        notificationSetting_chooseUserModal_continue: 'Продължи',
        notificationSetting_chooseUserModal_cancel: 'Отказ',
        notificationSetting_chooseUserModal_users: 'Потребител',

        //notificationSetting_chooseNSContractsModal
        notificationSetting_chooseNSProceduresModal_title: 'Избор на бюджет',
        notificationSetting_chooseNSProceduresModal_continue: 'Продължи',
        notificationSetting_chooseNSProceduresModal_cancel: 'Отказ',
        notificationSetting_chooseNSProceduresModal_all: 'Всички',
        notificationSetting_chooseNSProceduresModal_search: 'Търси',
        notificationSetting_chooseNSProceduresModal_code: 'Код',
        notificationSetting_chooseNSProceduresModal_name: 'Наименование',
        notificationSetting_chooseNSProceduresModal_programme: 'Основна организация',
        notificationSetting_chooseNSProceduresModal_programmePriority:
          'Разпоредител с бюджетни средства',
        notificationSetting_chooseNSProceduresModal_listingDate: 'Дата на обявяване ',
        notificationSetting_chooseNSProceduresModal_status: 'Статус',
        notificationSetting_chooseNSProceduresModal_euAmount: 'Фин. от ЕС',
        notificationSetting_chooseNSProceduresModal_bgAmount: 'Фин. от НФ',

        //notificationSetting_chooseNSPPrioritiesModal
        notificationSetting_chooseNSPPrioritiesModal_title:
          'Избор на разпоредител с бюджетни средства',
        notificationSetting_chooseNSPPrioritiesModal_continue: 'Продължи',
        notificationSetting_chooseNSPPrioritiesModal_cancel: 'Отказ',
        notificationSetting_chooseNSPPrioritiesModal_all: 'Всички',
        notificationSetting_chooseNSPPrioritiesModal_name: 'Наименование',
        notificationSetting_chooseNSPPrioritiesModal_code: 'Номер',
        notificationSetting_chooseNSPPrioritiesModal_programmeName: 'Основна организация',

        //notificationSetting_notificationSettingView
        notificationSetting_notificationSettingView_info: 'Статус: {{status}}',

        //notificationSetting_tabs
        notificationSetting_tabs_edit: 'Основни данни',
        notificationSetting_tabs_contract: 'Договори',
        notificationSetting_tabs_procedure: 'Бюджети',
        notificationSetting_tabs_programmePriorities: 'Разпоредители',
        notificationSetting_tabs_programmes: 'Отговорни организации',

        //notificationSetting_notificationSettingAttachedContracts
        notificationSetting_notificationSettingAttachedContracts_choose: 'Избор',
        notificationSetting_notificationSettingAttachedContracts_procedure: 'Бюджет',
        notificationSetting_notificationSettingAttachedContracts_programmePriority:
          'Разпоредител с бюджетни средства',
        notificationSetting_notificationSettingAttachedContracts_regNumber: 'Номер',
        notificationSetting_notificationSettingAttachedContracts_contractDate: 'Дата на сключване',
        notificationSetting_notificationSettingAttachedContracts_name: 'Наименование',
        notificationSetting_notificationSettingAttachedContracts_executionStatus:
          'Статус на изпълнение',
        notificationSetting_notificationSettingAttachedContracts_company: 'Бенефициент',
        notificationSetting_notificationSettingAttachedContracts_totalBfpAmount: 'БФП - Общо',
        notificationSetting_notificationSettingAttachedContracts_totalSelfAmount:
          'Собств. съфинансиране',

        //notificationSetting_notificationSettingAttachedProcedures
        notificationSetting_notificationSettingAttachedProcedures_choose: 'Избор',
        notificationSetting_notificationSettingAttachedProcedures_code: 'Код',
        notificationSetting_notificationSettingAttachedProcedures_name: 'Наименование',
        notificationSetting_notificationSettingAttachedProcedures_programme: 'Основна организация',
        notificationSetting_notificationSettingAttachedProcedures_programmePriority:
          'Разпоредител с бюджетни средства',
        notificationSetting_notificationSettingAttachedProcedures_activationDate:
          'Дата на активиране',
        notificationSetting_notificationSettingAttachedProcedures_status: 'Статус',
        notificationSetting_notificationSettingAttachedProcedures_euAmount: 'Фин. от ЕС',
        notificationSetting_notificationSettingAttachedProcedures_bgAmount: 'Фин. от НФ',

        //notificationSetting_notificationSettingAttachedpPriorities
        notificationSetting_notificationSettingAttachedpPriorities_choose: 'Избор',
        notificationSetting_notificationSettingAttachedpPriorities_name: 'Наименование',
        notificationSetting_notificationSettingAttachedpPriorities_code: 'Код',
        notificationSetting_notificationSettingAttachedpPriorities_programmeName:
          'Основна организация',

        //notificationSetting_notificationSettingAttachedProgrammes
        notificationSetting_notificationSettingAttachedProgrammes_choose: 'Избор',
        notificationSetting_notificationSettingAttachedProgrammes_name: 'Наименование',
        notificationSetting_notificationSettingAttachedProgrammes_code: 'Номер',

        //notificationSetting_chooseNSProgrammesModal
        notificationSetting_chooseNSProgrammesModal_title: 'Избор на основна организация',
        notificationSetting_chooseNSProgrammesModal_continue: 'Продължи',
        notificationSetting_chooseNSProgrammesModal_cancel: 'Отказ',
        notificationSetting_chooseNSProgrammesModal_all: 'Всички',
        notificationSetting_chooseNSProgrammesModal_name: 'Наименование',
        notificationSetting_chooseNSProgrammesModal_regNumber: 'Номер',

        //userNotifications_userNotificationsSearch
        userNotifications_userNotificationsSearch_title: 'Нотификации за потребител',
        userNotifications_userNotificationsSearch_eventName: 'Събитие',
        userNotifications_userNotificationsSearch_isRead: 'Прочетена',
        userNotifications_userNotificationsSearch_createDate: 'Дата на възникване',
        userNotifications_userNotificationsSearch_programmeCode: 'Основна организация',
        userNotifications_userNotificationsSearch_programmePriorityCode:
          'Разпоредител с бюджетни средства',
        userNotifications_userNotificationsSearch_procedureCode: 'Бюджет',
        userNotifications_userNotificationsSearch_contractCode: 'Договор',

        //userNotifications_viewNotificationModal
        userNotifications_viewNotificationModal_title: 'Нотификация за настъпило събитие',
        userNotifications_viewNotificationModal_subject:
          'Настъпило е събитие "{{eventName}}", за което сте абонирани',
        userNotifications_viewNotificationModal_createDate: 'Дата на възникванe',
        userNotifications_viewNotificationModal_reason: 'Връзка към променения обект',
        userNotifications_viewNotificationModal_programmeCode: 'Основна организация',
        userNotifications_viewNotificationModal_programmePriorityCode:
          'Разпоредител с бюджетни средства',
        userNotifications_viewNotificationModal_procedureCode: 'Бюджет',
        userNotifications_viewNotificationModal_contractCode: 'Договор',
        userNotifications_viewNotificationModal_remove: 'Изтрий',
        userNotifications_viewNotificationModal_cancel: 'Отказ',

        //contracts_contractsSearch
        contracts_contractsSearch_search: 'Търси',
        contracts_contractsSearch_new: 'Нов договор СК',
        contracts_contractsSearch_newServiceContract: 'Нов договор БЛ',
        contracts_contractsSearch_procedure: 'Бюджет',
        contracts_contractsSearch_programmePriority: 'Разпоредител с бюджетни средства',
        contracts_contractsSearch_regNumber: 'Номер',
        contracts_contractsSearch_contractDate: 'Дата на сключване',
        contracts_contractsSearch_name: 'Наименование',
        contracts_contractsSearch_executionStatus: 'Статус на изпълнение',
        contracts_contractsSearch_company: 'Бенефициент',
        contracts_contractsSearch_totalBfpAmount: 'Обща стойност',
        contracts_contractsSearch_excelExport: 'Експорт',

        //contracts_viewContract
        contracts_viewContract_number:
          'Статус: {{status}}, Номер: {{number}}, Бюджет: {{procedure}}',

        //contracts_tabs
        contracts_tabs_edit: 'Договор',
        contracts_tabs_amendments: 'Промени и изменения',
        contracts_tabs_registrations: 'Профили за достъп',
        contracts_tabs_accesscodes: 'Кодове за достъп',
        contracts_tabs_communications: 'Кореспонденция',
        contracts_tabs_additionalDocuments: 'Допълнителна информация',
        contracts_tabs_documents: 'Документи',
        contracts_tabs_users: 'Външни верификатори',

        //contracts_contractDataForm
        contracts_contractDataForm_programme: 'Основна организация',
        contracts_contractDataForm_procedure: 'Бюджет',
        contracts_contractDataForm_type: 'Начин на финансиране',
        contracts_contractDataForm_registrationType: 'Тип договор',
        contracts_contractDataForm_executionStatus: 'Статус на изпълнение',
        contracts_contractDataForm_contractDate: 'Дата на сключване',
        contracts_contractDataForm_regNumber: 'Рег. номер',
        contracts_contractDataForm_name: 'Наименование',
        contracts_contractDataForm_startDate: 'Дата на стартиране',
        contracts_contractDataForm_startConditions: 'Условие за стартиране',
        contracts_contractDataForm_attachedContractId: 'Обвързване с договор ',

        ///contracts_contractProjectForm
        contracts_contractProjectForm_name: 'Наименование',
        contracts_contractProjectForm_procedureId: 'Бюджет',
        contracts_contractProjectForm_registrationStatus: 'Статус',
        contracts_contractProjectForm_companyName: 'Бенефициент',
        contracts_contractProjectForm_companyUinType: 'Бенефициент - Булстат/ЕИК/ЕГН',
        contracts_contractProjectForm_companyUin: 'Бенефициент - Номер',
        contracts_contractProjectForm_regNumber: 'Номер',
        contracts_contractProjectForm_regDate: 'Дата на регистрация',
        contracts_contractProjectForm_regTime: 'Час на регистрация',

        //contracts_contractBeneficiaryForm
        contracts_contractBeneficiaryForm_uinType: 'Булстат/ЕИК/ЕГН',
        contracts_contractBeneficiaryForm_uin: 'Номер',
        contracts_contractBeneficiaryForm_name: 'Наименование',

        //contracts_modals_chooseProjectModal
        contracts_modals_chooseProjectModal_title: 'Избор на ПП',
        contracts_modals_chooseProjectModal_cancel: 'Отказ',
        contracts_modals_chooseProjectModal_procedure: 'Бюджет',
        contracts_modals_chooseProjectModal_regNumber: 'Номер на ПП',
        contracts_modals_chooseProjectModal_search: 'Търси',
        contracts_modals_chooseProjectModal_choose: 'Избери',
        contracts_modals_chooseProjectModal_company: 'Кандидат',
        contracts_modals_chooseProjectModal_name: 'Наименование',
        contracts_modals_chooseProjectModal_registrationStatus: 'Регистрационен статус',
        contracts_modals_chooseProjectModal_projectType: 'Тип',
        contracts_modals_chooseProjectModal_regDate: 'Дата на регистрация',

        //contracts_modals_invalidUinModal
        contracts_modals_invalidForeignNumberWarningModal_title:
          'Въведения идентификатор ЛНЧ е невалиден. Сигурни ли сте, че искате да продължите?',
        contracts_modals_invalidForeignNumberWarningModal_accept: 'Да',
        contracts_modals_invalidForeignNumberWarningModal_cancel: 'Отказ',

        //contracts_modals_chooseContractRegistration
        contracts_modals_chooseContractRegistration_title: 'Избор на профил за достъп',
        contracts_modals_chooseContractRegistration_search: 'Търси',
        contracts_modals_chooseContractRegistration_email: 'Ел. поща',
        contracts_modals_chooseContractRegistration_uinType: 'Идентификатор',
        contracts_modals_chooseContractRegistration_uin: 'ЕГН/ЛНЧ',
        contracts_modals_chooseContractRegistration_firstName: 'Собствено име',
        contracts_modals_chooseContractRegistration_lastName: 'Фамилия',
        contracts_modals_chooseContractRegistration_phone: 'Телефон',
        contracts_modals_chooseContractRegistration_choose: 'Избери',
        contracts_modals_chooseContractRegistration_cancel: 'Отказ',
        contracts_modals_chooseContractRegistration_contractRegNums: 'Договори',

        //contracts_contractsNewStep1
        contracts_contractsNewStep1_title: 'Създаване на договор (стъпка 1/2)',
        contracts_contractsNewStep1_next: 'Напред',
        contracts_contractsNewStep1_cancel: 'Отказ',
        contracts_contractsNewStep1_chooseProject: 'Търси',
        contracts_contractsNewStep1_procedureId: 'Бюджет',
        contracts_contractsNewStep1_projectNumber: 'ПП номер',
        contracts_contractsNewStep1_projectNumberInvaid: 'Невалиден номер на ПП',

        //contracts_contractsNewStep2
        contracts_contractsNewStep2_title: 'Създаване на договор (стъпка 2/2)',
        contracts_contractsNewStep2_save: 'Запис',
        contracts_contractsNewStep2_cancel: 'Отказ',
        contracts_contractsNewStep2_programme: 'Основна организация',
        contracts_contractsNewStep2_registrationType: 'Тип договор',
        contracts_contractsNewStep2_contractType: 'Тип договор',
        contracts_contractsNewStep2_project: 'Проектно предложение',
        contracts_contractsNewStep2_attachedContractId: 'Обвързване с договор',

        //contracts_editContract
        contracts_editContract_title: 'Редакция на договор',
        contracts_editContract_draft: 'Чернова',
        contracts_editContract_delete: 'Изтрий',
        contracts_editContract_confirmDraft:
          'Сигурни ли сте, че искате да върнете договора в чернова?',
        contracts_editContract_check: 'Проверен',
        contracts_editContract_confirmCheck:
          'Сигурни ли сте, че искате да маркирате договора като проверен?',
        contracts_editContract_contractData: 'Общи данни',
        contracts_editContract_beneficiary: 'Бенефициент',
        contracts_editContract_contractTemplate: 'Договор',
        contracts_editContract_contractStatus: 'Статус на договора',

        //contracts_contractData
        contracts_contractData_title: 'Преглед на договор',
        contracts_contractData_contractData: 'Общи данни',
        contracts_contractData_beneficiary: 'Бенефициент',
        contracts_contractData_projectDossier: 'Проектно досие',

        //contracts_amendmentsSearch
        contracts_amendmentsSearch_versions: 'Договор',
        contracts_amendmentsSearch_newAnnex: 'Изменение',
        contracts_amendmentsSearch_newChange: 'Промяна',
        contracts_amendmentsSearch_newPartialAnnex: 'Частично изменение',
        contracts_amendmentsSearch_newPartialChange: 'Частична промяна',
        contracts_amendmentsSearch_versionNum: '№ на версия',
        contracts_amendmentsSearch_versionType: 'Тип',
        contracts_amendmentsSearch_versionRegNumber: 'Рег. номер',
        contracts_amendmentsSearch_versionCreateNote: 'Бележки',
        contracts_amendmentsSearch_versionContractDate: 'Дата на сключване',
        contracts_amendmentsSearch_versionStatus: 'Статус',
        contracts_amendmentsSearch_procurements:
          'Процедури за избор на изпълнител и сключени договори',
        contracts_amendmentsSearch_procurementSource: 'Източник',
        contracts_amendmentsSearch_procurementStatus: 'Статус',
        contracts_amendmentsSearch_procurementCreateDate: 'Дата на създаване',
        contracts_amendmentsSearch_procurementModifyDate: 'Дата на промяна',
        contracts_amendmentsSearch_procurementCreateTitle:
          'Новa процедурa за избор на изпълнител и сключени договори',
        contracts_amendmentsSearch_procurementCreateNote: 'Бележка',
        contracts_amendmentsSearch_spendingPlans: 'Планове за разходване на средствата',
        contracts_amendmentsSearch_spendingPlanSource: 'Източник',
        contracts_amendmentsSearch_spendingPlanStatus: 'Статус',
        contracts_amendmentsSearch_spendingPlanCreateDate: 'Дата на създаване',
        contracts_amendmentsSearch_spendingPlanModifyDate: 'Дата на промяна',
        contracts_amendmentsSearch_spendingPlanCreateTitle: 'Нов план за разходване на средствата',
        contracts_amendmentsSearch_spendingPlanCreateNote: 'Бележка',
        contracts_amendmentsSearch_offers: 'Оферти',
        contracts_amendmentsSearch_offersSubmitDate: 'Дата на подаване',
        contracts_amendmentsSearch_offersExpectedAmount: 'Прогнозна стойност съгласно обявление',
        contracts_amendmentsSearch_offersName: 'Предмет на бюджетта',

        //contracts_additionalDocumentsSearch
        contracts_additionalDocumentsSearch_actuallyPaidAmounts: 'Реално изплатени суми',
        contracts_additionalDocumentsSearch_contractDebts: 'Дългове',
        contracts_additionalDocumentsSearch_reimbursedAmounts: 'Възстановени суми',
        contracts_additionalDocumentsSearch_financialCorrections: 'Финансови корекции',
        contracts_additionalDocumentsSearch_flatFinancialCorrections:
          'Финансови корекции за системни пропуски',

        //contracts_versionForm
        contracts_versionForm_versionType: 'Тип',
        contracts_versionForm_versionNumber: '№ на версия',
        contracts_versionForm_regNumber: 'Рег. номер',
        contracts_versionForm_contractDate: 'Дата на сключване',
        contracts_versionForm_status: 'Статус',
        contracts_versionForm_createdByUser: 'Създал',
        contracts_versionForm_createDate: 'Дата на създаване',
        contracts_versionForm_createNote: 'Бележки',

        //contracts_versionsEdit
        contracts_versionsEdit_newContractTitle: 'Преглед на договор',
        contracts_versionsEdit_annexTitle: 'Преглед на изменение',
        contracts_versionsEdit_partialAnnexTitle: 'Преглед на частично изменение',
        contracts_versionsEdit_changeTitle: 'Преглед на промяна',
        contracts_versionsEdit_partialChangeTitle: 'Преглед на частична промяна',
        contracts_versionsEdit_draft: 'Чернова',
        contracts_versionsEdit_confirmDraft:
          'Сигурни ли сте, че искате да върнете записа в чернова?',
        contracts_versionsEdit_check: 'Проверен',
        contracts_versionsEdit_confirmCheck:
          'Сигурни ли сте, че искате да маркирате записа като проверен?',
        contracts_versionsEdit_edit: 'Редакция',
        contracts_versionsEdit_technicalEdit: 'Техническа редакция',
        contracts_versionsEdit_delete: 'Изтриване',
        contracts_versionsEdit_sapData: 'Данни за САП',
        contracts_versionsEdit_save: 'Запис',
        contracts_versionsEdit_cancel: 'Отказ',
        contracts_versionsEdit_template: 'Договор',

        //contracts_versionsEdit_sapData
        contracts_versionsEdit_sapData_header: 'Данни за САП',
        contracts_versionsEdit_sapData_programmeCode: 'Код на основна организация',
        contracts_versionsEdit_sapData_programmePriorityCodes: 'Кодове на приоритетни оси',
        contracts_versionsEdit_sapData_programmePriorityCode:
          'Код на разпоредител с бюджетни средства',
        contracts_versionsEdit_sapData_procedureCode: 'Код на бюджет',
        contracts_versionsEdit_sapData_projectRegNumber: 'Код на проектно предложение',
        contracts_versionsEdit_sapData_contractRegNumber: 'Код на договор/анекс',
        contracts_versionsEdit_sapData_companyUin: 'Идентификатор на бенефициент',
        contracts_versionsEdit_sapData_companyName: 'Наименование на бенефициент',
        contracts_versionsEdit_sapData_startDate: 'Дата на стартиране',
        contracts_versionsEdit_sapData_completionDate: 'Дата на приключване',
        contracts_versionsEdit_sapData_budgetValue: 'Стойност на бюджета:',
        contracts_versionsEdit_sapData_totalAmount: 'Общо',
        contracts_versionsEdit_sapData_bfpTotalAmount: 'Общо БФП/ФИ',
        contracts_versionsEdit_sapData_totalSelfAmount: 'Собствено финансиране',
        contracts_versionsEdit_sapData_euAmounts: 'Финансиране от ЕС',
        contracts_versionsEdit_sapData_bgAmount: 'Национално финансиране',
        contracts_versionsEdit_sapData_currency: 'Валута',
        contracts_versionsEdit_sapData_currencyBGN: 'Лева(BGN)',
        contracts_versionsEdit_sapData_currencyEUR: 'Евро(EUR)',
        contracts_versionsEdit_sapData_programmePriorityBudget: 'Стойност по приоритетни оси:',
        contracts_versionsEdit_sapData_print: 'Принтирай',

        //contracts_newVersion
        contracts_newVersion_annexTitle: 'Новo изменение',
        contracts_newVersion_changeTitle: 'Нова промяна',
        contracts_newVersion_partialAnnexTitle: 'Новo частично изменение',
        contracts_newVersion_partialChangeTitle: 'Нова частична промяна',
        contracts_newVersion_save: 'Запис',
        contracts_newVersion_cancel: 'Отказ',

        //contracts_offerForm
        contracts_offerForm_companyName: 'Наименование на бенефициент от договор за БФП',
        contracts_offerForm_companyUinType: 'ЕИК (ЕИК на бенефициент от договор за БФП)',
        contracts_offerForm_companyUin: 'Номер',
        contracts_offerForm_description: 'Описание',
        contracts_offerForm_procurementSubject: 'Предмет на предвидената бюджет',
        contracts_offerForm_procurementObject: 'Обект на бюджетта',
        contracts_offerForm_expectedAmount: 'Прогнозна стойност',

        //contracts_offersEdit
        contracts_offersEdit_title: 'Преглед на оферта към процедура за избор на изпълнител',
        contracts_offersEdit_template: 'Оферта',

        //contracts_editContractGrantDocument
        contracts_editContractGrantDocument_title: 'Редакция на документ за БФП',
        contracts_editContractGrantDocument_edit: 'Редакция',
        contracts_editContractGrantDocument_deleteDocument: 'Изтриване',
        contracts_editContractGrantDocument_save: 'Запис',
        contracts_editContractGrantDocument_cancel: 'Отказ',

        //contracts_newContractGrantDocument
        contracts_newContractGrantDocument_title: 'Нов документ',
        contracts_newContractGrantDocument_save: 'Запис',
        contracts_newContractGrantDocument_cancel: 'Отказ',

        //contracts_searchContractGrantDocuments
        contracts_searchContractGrantDocuments_header: 'Документи към договор',
        contracts_searchContractGrantDocuments_name: 'Наименование',
        contracts_searchContractGrantDocuments_description: 'Описание',
        contracts_searchContractGrantDocuments_file: 'Файл',

        //contracts_searchContractProcurementDocuments
        contracts_searchContractProcurementDocuments_header:
          'Документи към процедури за избор на изпълнител',
        contracts_searchContractProcurementDocuments_name: 'Наименование',
        contracts_searchContractProcurementDocuments_description: 'Описание',
        contracts_searchContractProcurementDocuments_file: 'Файл',

        //contracts_editContractProcurementDocument
        contracts_editContractProcurementDocument_title:
          'Редакция на документ към процедури за избор на изпълнител',
        contracts_editContractProcurementDocument_edit: 'Редакция',
        contracts_editContractProcurementDocument_deleteDocument: 'Изтриване',
        contracts_editContractProcurementDocument_save: 'Запис',
        contracts_editContractProcurementDocument_cancel: 'Отказ',

        //contracts_newContractProcurementDocument
        contracts_newContractProcurementDocument_title: 'Нов документ',
        contracts_newContractProcurementDocument_save: 'Запис',
        contracts_newContractProcurementDocument_cancel: 'Отказ',

        //contracts_procurementForm
        contracts_procurementForm_source: 'Източник',
        contracts_procurementForm_status: 'Статус',
        contracts_procurementForm_createdByUser: 'Създал',
        contracts_procurementForm_createDate: 'Дата на създаване',
        contracts_procurementForm_createNote: 'Бележки',

        //contracts_procurementsEdit
        contracts_procurementsEdit_title:
          'Преглед на процедура за избор на изпълнител и сключени договори',
        contracts_procurementsEdit_edit: 'Редакция',
        contracts_procurementsEdit_save: 'Запис',
        contracts_procurementsEdit_cancel: 'Отказ',
        contracts_procurementsEdit_delete: 'Изтриване',
        contracts_procurementsEdit_draft: 'Чернова',
        contracts_procurementsEdit_confirmDraft:
          'Сигурни ли сте, че искате да върнете записа в чернова?',
        contracts_procurementsEdit_check: 'Проверен',
        contracts_procurementsEdit_confirmCheck:
          'Сигурни ли сте, че искате да маркирате записа като проверен?',
        contracts_procurementsEdit_template: 'Процедура за избор',

        //contracts_newProcurement
        contracts_newProcurement_title: 'Нова процедура за избор на изпълнител и сключени договори',
        contracts_newProcurement_save: 'Запис',
        contracts_newProcurement_cancel: 'Отказ',

        //contracts_spendingPlanForm
        contracts_spendingPlanForm_source: 'Източник',
        contracts_spendingPlanForm_status: 'Статус',
        contracts_spendingPlanForm_createdByUser: 'Създал',
        contracts_spendingPlanForm_createDate: 'Дата на създаване',
        contracts_spendingPlanForm_createNote: 'Бележки',

        //contracts_spendingPlansEdit
        contracts_spendingPlansEdit_title: 'Преглед на план за разходване на средствата',
        contracts_spendingPlansEdit_edit: 'Редакция',
        contracts_spendingPlansEdit_save: 'Запис',
        contracts_spendingPlansEdit_cancel: 'Отказ',
        contracts_spendingPlansEdit_delete: 'Изтриване',
        contracts_spendingPlansEdit_draft: 'Чернова',
        contracts_spendingPlansEdit_confirmDraft:
          'Сигурни ли сте, че искате да върнете записа в чернова?',
        contracts_spendingPlansEdit_check: 'Проверен',
        contracts_spendingPlansEdit_confirmCheck:
          'Сигурни ли сте, че искате да маркирате записа като проверен?',
        contracts_spendingPlansEdit_template: 'План за разходване на средствата',

        //contracts_newSpendingPlan
        contracts_newSpendingPlan_title: 'Нов план за разходване на средствата',
        contracts_newSpendingPlan_save: 'Запис',
        contracts_newSpendingPlan_cancel: 'Отказ',

        //contracts_registrations_contractRegistrationDataForm
        contracts_registrations_contractRegistrationDataForm_email: 'Ел. поща',
        contracts_registrations_contractRegistrationDataForm_uin: 'ЕГН/ЛНЧ',
        contracts_registrations_contractRegistrationDataForm_uinType: 'Идентификатор',
        contracts_registrations_contractRegistrationDataForm_firstName: 'Собствено име',
        contracts_registrations_contractRegistrationDataForm_lastName: 'Фамилия',
        contracts_registrations_contractRegistrationDataForm_phone: 'Телефон',
        contracts_registrations_contractRegistrationDataForm_file: 'Декларация',
        contracts_registrations_contractRegistrationDataForm_uinNotValid: 'Невалидно ЕГН/ЛНЧ',
        contracts_registrations_contractRegistrationDataForm_emailNotUnique:
          'Вече съществува профил с такава ел. поща',

        //contracts_registrations_search
        contracts_registrations_search_attach: 'Присъединяване',
        contracts_registrations_search_new: 'Нов',
        contracts_registrations_search_email: 'Ел. поща',
        contracts_registrations_search_firstName: 'Собствено име',
        contracts_registrations_search_lastName: 'Фамилия',
        contracts_registrations_search_phone: 'Телефон',
        contracts_registrations_search_file: 'Декларация',
        contracts_registrations_search_isActive: 'Активен',

        //contracts_communications_communicationForm
        contracts_communications_communicationForm_status: 'Статус',
        contracts_communications_communicationForm_regNumber: 'Регистрационен номер',
        contracts_communications_communicationForm_subject: 'Тема',
        contracts_communications_communicationForm_readDate: 'Дата на първо отваряне',
        contracts_communications_communicationForm_sendDate: 'Дата на изпращане',
        contracts_communications_communicationForm_orderNum: 'Пореден номер',
        contracts_communications_communicationForm_source: 'Изпратено от',

        //contracts_communications_editCommunication
        contracts_communications_editCommunication_title: 'Преглед на кореспонденция',
        contracts_communications_editCommunication_delete: 'Изтриване',
        contracts_communications_editCommunication_communication: 'Съобщение',
        contracts_communications_editCommunication_back: 'Назад',

        //contracts_communications_search
        contracts_communications_search_new: 'Нова кореспонденция',
        contracts_communications_search_orderNum: 'Пореден номер',
        contracts_communications_search_status: 'Статус',
        contracts_communications_search_source: 'Изпратено от',
        contracts_communications_search_regNumber: 'Рег. номер',
        contracts_communications_search_subject: 'Тема',
        contracts_communications_search_sendDate: 'Дата на изпращане',
        contracts_communications_search_readDate: 'Дата на първо отваряне',
        contracts_communications_search_excelExport: 'Експорт',

        //contracts_contractUser_new
        contracts_contractUser_new_title: 'Нов въшнен верификатор',
        contracts_contractUser_new_save: 'Съхрани',
        contracts_contractUser_new_cancel: 'Отказ',
        contracts_contractUser_new_user: 'Потребител',

        //contracts_contractUserSearch
        contracts_contractUserSearch_new: 'Нов външен верификатор',
        contracts_contractUserSearch_fullname: 'Име',
        contracts_contractUserSearch_username: 'Потребителско име',

        //contracts_editContractUser
        contracts_editContractUser_title: 'Външен верификатор',
        contracts_editContractUser_deleteUser: 'Изтриване',
        contracts_editContractUser_cancel: 'Отказ',
        contracts_editContractUser_user: 'Потребител',

        //contractAccessCodes_contractAccessCodesForm
        contractAccessCodes_contractAccessCodesForm_contract: 'Договор',
        contractAccessCodes_contractAccessCodesForm_code: 'Код',
        contractAccessCodes_contractAccessCodesForm_firstName: 'Име',
        contractAccessCodes_contractAccessCodesForm_lastName: 'Фамилия',
        contractAccessCodes_contractAccessCodesForm_position: 'Позиция',
        contractAccessCodes_contractAccessCodesForm_identifier:
          'Идентификатор (ЕГН/Чуждестранно лице)',
        contractAccessCodes_contractAccessCodesForm_email: 'E-mail',
        contractAccessCodes_contractAccessCodesForm_isActive: 'Активен',
        contractAccessCodes_contractAccessCodesForm_createDate: 'Дата на създаване',
        contractAccessCodes_contractAccessCodesForm_modifyDate: 'Дата на посл. промяна',
        contractAccessCodes_contractAccessCodesForm_permissions: 'Права',
        contractAccessCodes_contractAccessCodesForm_read: 'Четене',
        contractAccessCodes_contractAccessCodesForm_write: 'Писане',
        contractAccessCodes_contractAccessCodesForm_contracts: 'Договор',
        contractAccessCodes_contractAccessCodesForm_communication: 'Кореспонденция',
        contractAccessCodes_contractAccessCodesForm_procurements:
          'Процедурa за избор на изпълнител и сключени договори',
        contractAccessCodes_contractAccessCodesForm_technicalPlan: 'Технически отчет',
        contractAccessCodes_contractAccessCodesForm_financialPlan: 'Финансов отчет',
        contractAccessCodes_contractAccessCodesForm_paymentRequest: 'Искане за плащане',
        contractAccessCodes_contractAccessCodesForm_microdata: 'Микроданни',
        contractAccessCodes_contractAccessCodesForm_spendingPlans:
          'План за разходване на средствата',

        //contractAccessCodes_editContractAccessCodes
        contractAccessCodes_editContractAccessCodes_title: 'Преглед на код за достъп',
        contractAccessCodes_editContractAccessCodes_back: 'Назад',

        //contractAccessCodes_contractAccessCodesSearch
        contractAccessCodes_contractAccessCodesSearch_code: 'Код',
        contractAccessCodes_contractAccessCodesSearch_contractRegNumber: 'Договор',
        contractAccessCodes_contractAccessCodesSearch_firstName: 'Име',
        contractAccessCodes_contractAccessCodesSearch_lastName: 'Фамилия',
        contractAccessCodes_contractAccessCodesSearch_email: 'E-mail',
        contractAccessCodes_contractAccessCodesSearch_isActive: 'Активен',
        contractAccessCodes_contractAccessCodesSearch_createDate: 'Дата на създаване',
        contractAccessCodes_contractAccessCodesSearch_modifyDate: 'Дата на последна промяна',
        contractAccessCodes_contractAccessCodesSearch_excelExport: 'Експорт',

        //contractRegistrations_contractRegistrationDataForm
        contractRegistrations_contractRegistrationDataForm_email: 'Ел. поща',
        contractRegistrations_contractRegistrationDataForm_uin: 'ЕГН/ЛНЧ',
        contractRegistrations_contractRegistrationDataForm_uinType: 'Идентификатор',
        contractRegistrations_contractRegistrationDataForm_firstName: 'Собствено име',
        contractRegistrations_contractRegistrationDataForm_lastName: 'Фамилия',
        contractRegistrations_contractRegistrationDataForm_phone: 'Телефон',

        //contractRegistrations_searchForm
        contractRegistrations_searchForm_newBtn: 'Нов профил за достъп',
        contractRegistrations_searchForm_email: 'Ел. поща',
        contractRegistrations_searchForm_firstName: 'Собствено име',
        contractRegistrations_searchForm_lastName: 'Фамилия',
        contractRegistrations_searchForm_phone: 'Телефон',
        contractRegistrations_searchForm_contractRegNums: 'Договори',
        contractRegistrations_searchForm_excelExport: 'Експорт',
        contractRegistrations_searchForm_contractRegNumsCompanies: 'Договори',

        //contractRegistrations_newForm
        contractRegistrations_newForm_title: 'Нов профил за достъп към договор',
        contractRegistrations_newForm_file: 'Декларация',
        contractRegistrations_newForm_save: 'Запис',
        contractRegistrations_newForm_cancel: 'Отказ',

        //contractRegistrations_attachForm
        contractRegistrations_attachForm_title: 'Присъединяване на профил за достъп към договор',
        contractRegistrations_attachForm_file: 'Декларация',
        contractRegistrations_attachForm_chooseRegistration: 'Избери профил',
        contractRegistrations_attachForm_save: 'Запис',
        contractRegistrations_attachForm_cancel: 'Отказ',
        contractRegistrations_attachForm_searchRegistration: 'Търсене',

        //contractRegistrations_editForm
        contractRegistrations_editForm_title: 'Преглед на профил за достъп към договор',
        contractRegistrations_editForm_back: 'Назад',
        contractRegistrations_editForm_activate: 'Активирай',
        contractRegistrations_editForm_deactivate: 'Деактивирай',
        contractRegistrations_editForm_edit: 'Редакция',
        contractRegistrations_editForm_save: 'Запис',
        contractRegistrations_editForm_cancel: 'Отказ',

        //contractCommunications_tabs
        contractCommunications_tabs_contract: 'Договор',
        contractCommunications_tabs_communication: 'Кореспонденция',

        //contractCommunications_contractsSearch
        contractCommunications_contractsSearch_search: 'Търси',
        contractCommunications_contractsSearch_programmePriority:
          'Разпоредител с бюджетни средства',
        contractCommunications_contractsSearch_procedure: 'Бюджет',
        contractCommunications_contractsSearch_regNumber: 'Номер',
        contractCommunications_contractsSearch_contractDate: 'Дата на сключване',
        contractCommunications_contractsSearch_name: 'Наименование',
        contractCommunications_contractsSearch_executionStatus: 'Статус на изпълнение',
        contractCommunications_contractsSearch_company: 'Бенефициент',
        contractCommunications_contractsSearch_companyKidCode: 'КО по КИД 2008',

        //contractCommunications_viewContract
        contractCommunications_viewContract_title: 'Преглед на договор',
        contractCommunications_viewContract_contractData: 'Общи данни',
        contractCommunications_viewContract_beneficiary: 'Бенефициент',

        //contractCommunications_communicationForm
        contractCommunications_communicationForm_status: 'Статус',
        contractCommunications_communicationForm_regNumber: 'Регистрационен номер',
        contractCommunications_communicationForm_subject: 'Тема',
        contractCommunications_communicationForm_readDate: 'Дата на първо отваряне',
        contractCommunications_communicationForm_sendDate: 'Дата на изпращане',
        contractCommunications_communicationForm_orderNum: 'Пореден номер',
        contractCommunications_communicationForm_source: 'Изпратено от',

        //contractCommunications_editCommunication
        contractCommunications_editCommunication_title: 'Преглед на кореспонденция',
        contractCommunications_editCommunication_delete: 'Изтриване',
        contractCommunications_editCommunication_communication: 'Съобщение',

        //contractCommunications_searchCommunication
        contractCommunications_searchCommunication_programme: 'Основна организация',
        contractCommunications_searchCommunication_programmePriority:
          'Разпоредител с бюджетни средства',
        contractCommunications_searchCommunication_procedure: 'Бюджет',
        contractCommunications_searchCommunication_fromDate: 'Начална дата',
        contractCommunications_searchCommunication_toDate: 'Към дата',
        contractCommunications_searchCommunication_search: 'Търси',
        contractCommunications_searchCommunication_new: 'Нова кореспонденция',
        contractCommunications_searchCommunication_orderNum: 'Пореден номер',
        contractCommunications_searchCommunication_contractRegNumber: 'Рег. номер на договор',
        contractCommunications_searchCommunication_status: 'Статус',
        contractCommunications_searchCommunication_source: 'Изпратено от',
        contractCommunications_searchCommunication_regNumber: 'Рег. номер',
        contractCommunications_searchCommunication_subject: 'Тема',
        contractCommunications_searchCommunication_sendDate: 'Дата на изпращане',
        contractCommunications_searchCommunication_readDate: 'Дата на първо отваряне',
        contractCommunications_searchCommunication_excelExport: 'Експорт',

        //contracts_contractAccessCodesForm
        contracts_contractAccessCodesForm_code: 'Код',
        contracts_contractAccessCodesForm_firstName: 'Име',
        contracts_contractAccessCodesForm_lastName: 'Фамилия',
        contracts_contractAccessCodesForm_position: 'Позиция',
        contracts_contractAccessCodesForm_identifier: 'Идентификатор (ЕГН/Чуждестранно лице)',
        contracts_contractAccessCodesForm_email: 'E-mail',
        contracts_contractAccessCodesForm_isActive: 'Активен',
        contracts_contractAccessCodesForm_createDate: 'Дата на създаване',
        contracts_contractAccessCodesForm_modifyDate: 'Дата на посл. промяна',
        contracts_contractAccessCodesForm_permissions: 'Права',
        contracts_contractAccessCodesForm_read: 'Четене',
        contracts_contractAccessCodesForm_write: 'Писане',
        contracts_contractAccessCodesForm_contracts: 'Договор',
        contracts_contractAccessCodesForm_communication: 'Кореспонденция',
        contracts_contractAccessCodesForm_procurements:
          'Процедурa за избор на изпълнител и сключени договори',
        contracts_contractAccessCodesForm_technicalPlan: 'Технически отчет',
        contracts_contractAccessCodesForm_financialPlan: 'Финансов отчет',
        contracts_contractAccessCodesForm_paymentRequest: 'Искане за плащане',
        contracts_contractAccessCodesForm_microdata: 'Микроданни',
        contracts_contractAccessCodesForm_spendingPlans: 'План за разходване на средствата',

        //contracts_editContractAccessCodes
        contracts_editContractAccessCodes_title: 'Преглед на код за достъп',
        contracts_editContractAccessCodes_back: 'Назад',
        contracts_editContractAccessCodes_activate: 'Активирай',
        contracts_editContractAccessCodes_deactivate: 'Деактивирай',

        //contracts_contractAccessCodesSearch
        contracts_contractAccessCodesSearch_code: 'Код',
        contracts_contractAccessCodesSearch_firstName: 'Име',
        contracts_contractAccessCodesSearch_lastName: 'Фамилия',
        contracts_contractAccessCodesSearch_email: 'E-mail',
        contracts_contractAccessCodesSearch_isActive: 'Активен',
        contracts_contractAccessCodesSearch_createDate: 'Дата на създаване',
        contracts_contractAccessCodesSearch_modifyDate: 'Дата на последна промяна',

        //contracts_serviceContractsNewStep3
        contracts_serviceContractsNewStep3_title:
          'Създаване на договор по бюджетна линия - стъпка 3/3',
        contracts_serviceContractsNewStep3_save: 'Запис',
        contracts_serviceContractsNewStep3_cancel: 'Отказ',

        //contracts_serviceContractsNewStep2
        contracts_serviceContractsNewStep2_title:
          'Създаване на договор по бюджетна линия - стъпка 2/3',
        contracts_serviceContractsNewStep2_next: 'Напред',
        contracts_serviceContractsNewStep2_cancel: 'Отказ',
        contracts_serviceContractsNewStep2_procedureId: 'Бюджет',
        contracts_serviceContractsNewStep2_companyNotFound:
          'Бенефициентът не беше намерен. Моля въведете данни за нов бенефициент.',
        contracts_serviceContractsNewStep2_company: 'Бенефициент',
        contracts_serviceContractsNewStep2_createContinue: 'Съхрани и продължи',

        //contracts_serviceContractsNewStep1
        contracts_serviceContractsNewStep1_title:
          'Създаване на договор по бюджетна линия - стъпка 1/3',
        contracts_serviceContractsNewStep1_next: 'Напред',
        contracts_serviceContractsNewStep1_cancel: 'Отказ',
        contracts_serviceContractsNewStep1_procedureId: 'Бюджет',
        contracts_serviceContractsNewStep1_uinTypeId: 'Тип на идентификатора',
        contracts_serviceContractsNewStep1_uin: 'Идентификатор',
        contracts_serviceContractsNewStep1_chooseCompany: 'Търси',

        //contracts_contractServiceDataForm
        contracts_contractServiceDataForm_programme: 'Основна организация',
        contracts_contractServiceDataForm_contractType: 'Начин на финансиране',
        contracts_contractServiceDataForm_registrationType: 'Тип договор',
        contracts_contractServiceDataForm_name: 'Наименование',
        contracts_contractServiceDataForm_nameAlt: 'Наименование на английски',
        contracts_contractServiceDataForm_procedureId: 'Бюджет',
        contracts_contractServiceDataForm_companyName: 'Бенефициент',
        contracts_contractServiceDataForm_companyUinType: 'Вид на идентификатора',
        contracts_contractServiceDataForm_companyUin: 'Идентификатор на бенефициент',
        contracts_contractServiceDataForm_notes: 'Забележки',

        //contractReports_tabs
        contractReports_tabs_contract: 'Договор',
        contractReports_tabs_report: 'Основни данни',
        contractReports_tabs_documents: 'Документи',

        //contractReports_contractsSearch
        contractReports_contractsSearch_search: 'Търси',
        contractReports_contractsSearch_procedure: 'Бюджет',
        contractReports_contractsSearch_regNumber: 'Номер',
        contractReports_contractsSearch_contractDate: 'Дата на сключване',
        contractReports_contractsSearch_name: 'Наименование',
        contractReports_contractsSearch_executionStatus: 'Статус на изпълнение',
        contractReports_contractsSearch_companyName: 'Наименование на бенефициента',
        contractReports_contractsSearch_companyUinType: 'Булстат/ЕИК/ЕГН на бенефициента',
        contractReports_contractsSearch_companyUin: 'Номер на бенефициента',
        contractReports_contractsSearch_companyKidCode: 'КО по КИД 2008',

        //contractReports_viewContract
        contractReports_viewContract_title: 'Преглед на договор',
        contractReports_viewContract_contractData: 'Общи данни',
        contractReports_viewContract_beneficiary: 'Бенефициент',

        //contractReports_communicationForm
        contractReports_contractReportForm_status: 'Статус',
        contractReports_contractReportForm_reportType: 'Тип',
        contractReports_contractReportForm_source: 'Въведен от',
        contractReports_contractReportForm_statusNote: 'Бележка към промяната на статуса',
        contractReports_contractReportForm_orderNum: 'Пореден номер',

        contractReports_contractReportForm_submitDate: 'Дата на представяне',
        contractReports_contractReportForm_submitDeadline: 'Срок на представяне',
        contractReports_contractReportForm_checkedDate: 'Дата на одобрение',
        contractReports_contractReportForm_incorrectDateTo:
          "Стойността на полето 'Крайна дата' трябва да е по-голяма или равна на 'Начална дата'",

        //contractReports_search
        contractReports_search_new: 'Нов пакет отчетни документи',
        contractReports_search_contractRegNum: 'Номер на договор',
        contractReports_search_search: 'Търси',
        contractReports_search_contractName: 'Договор',
        contractReports_search_procedureName: 'Бюджет',
        contractReports_search_orderNum: 'Пореден номер',
        contractReports_search_status: 'Статус',
        contractReports_search_source: 'Въведен от',
        contractReports_search_reportType: 'Тип',
        contractReports_search_excelExport: 'Експорт',

        //contractReports_viewContractReport
        contractReports_viewContractReport_title:
          'Договор: {{contractName}}, Статус на пакета: {{reportStatus}}',

        //contractReports_chooseContractModal
        contractReports_chooseContractModal_title: 'Избор на договор',
        contractReports_chooseContractModal_cancel: 'Отказ',
        contractReports_chooseContractModal_search: 'Търси',
        contractReports_chooseContractModal_choose: 'Избери',
        contractReports_chooseContractModal_procedure: 'Бюджет',
        contractReports_chooseContractModal_programmePriority: 'Разпоредител с бюджетни средства',
        contractReports_chooseContractModal_regNumber: 'Номер',
        contractReports_chooseContractModal_contractDate: 'Дата на сключване',
        contractReports_chooseContractModal_name: 'Наименование',
        contractReports_chooseContractModal_executionStatus: 'Статус на изпълнение',
        contractReports_chooseContractModal_company: 'Бенефициент',
        contractReports_chooseContractModal_companyKidCode: 'КО по КИД 2008',

        //contractReports_newContractReportStep1
        contractReports_newContractReportStep1_title:
          'Създаване на пакет отчетни документи (стъпка 1/2)',
        contractReports_newContractReportStep1_next: 'Напред',
        contractReports_newContractReportStep1_cancel: 'Отказ',
        contractReports_newContractReportStep1_procedureId: 'Бюджет',
        contractReports_newContractReportStep1_projectId: 'Проектно предложение',
        contractReports_newContractReportStep1_contractNumber: 'Номер на договор',
        contractReports_newContractReportStep1_contractNumberInvalid: 'Невалиден номер на договор',
        contractReports_newContractReportStep1_chooseContract: 'Търси',

        //contractReports_newContractReportStep2
        contractReports_newContractReportStep2_title:
          'Създаване на пакет отчетни документи (стъпка 2/2)',
        contractReports_newContractReportStep2_save: 'Запис',
        contractReports_newContractReportStep2_cancel: 'Отказ',

        //contractReports_editContractReport
        contractReports_editContractReport_title: 'Пакет отчетни документи',
        contractReports_editContractReport_edit: 'Редакция',
        contractReports_editContractReport_save: 'Запис',
        contractReports_editContractReport_cancel: 'Отказ',
        contractReports_editContractReport_delete: 'Изтриване',
        contractReports_editContractReport_draft: 'Чернова',
        contractReports_editContractReport_entered: 'Въведен',
        contractReports_editContractReport_sentChecked: 'Приключен',
        contractReports_editContractReport_accepted: 'Приет',
        contractReports_editContractReport_refused: 'Отхвърлен',
        contractReports_editContractReport_document: 'Документ',
        contractReports_editContractReport_communication: 'Съобщение',
        contractReports_editContractReport_status: 'Статус',
        contractReports_editContractReport_statusChangeMessage: 'Бележка към промяната на статуса',
        contractReports_editContractReport_confirmChangeStatus:
          "Сигурни ли сте, че искате да промените статуса на пакета на '{{status}}'",

        //contractReports_contractReportDocuments
        contractReports_contractReportDocuments_add: 'Добави',
        contractReports_contractReportDocuments_advance: 'Авансово',
        contractReports_contractReportDocuments_standard: 'Стандартно',
        contractReports_contractReportDocuments_reportTechnical: 'Технически отчет',
        contractReports_contractReportDocuments_reportFinancial: 'Финансов отчет',
        contractReports_contractReportDocuments_reportPayment: 'Искане за плащане',

        //contractReports_contractReportFinancialForm
        contractReports_contractReportFinancialForm_versionNum: 'Номер',
        contractReports_contractReportFinancialForm_versionSubNum: 'Версия',
        contractReports_contractReportFinancialForm_status: 'Статус',
        contractReports_contractReportFinancialForm_statusNote: 'Причина за промяната на статуса',
        contractReports_contractReportFinancialForm_startDate: 'Начална дата',
        contractReports_contractReportFinancialForm_endDate: 'Крайна дата',

        //contractReports_contractReportPaymentForm
        contractReports_contractReportPaymentForm_versionNum: 'Номер',
        contractReports_contractReportPaymentForm_versionSubNum: 'Версия',
        contractReports_contractReportPaymentForm_paymentType: 'Тип',
        contractReports_contractReportPaymentForm_regDate: 'Дата на регистрация',
        contractReports_contractReportPaymentForm_dateFrom: 'Начална дата',
        contractReports_contractReportPaymentForm_dateTo: 'Крайна дата',
        contractReports_contractReportPaymentForm_submitDate: 'Дата на представяне',
        contractReports_contractReportPaymentForm_submitDeadline: 'Срок на представяне',
        contractReports_contractReportPaymentForm_otherRegistration: 'Друга регистрация',
        contractReports_contractReportPaymentForm_requestedAmount:
          'Ст-ст на исканите средства за плащане',
        contractReports_contractReportPaymentForm_createDate: 'Дата на създаване',
        contractReports_contractReportPaymentForm_status: 'Статус',
        contractReports_contractReportPaymentForm_statusNote: 'Причина за промяната на статуса',

        //contractReports_contractReportTechnicalForm
        contractReports_contractReportTechnicalForm_versionNum: 'Номер',
        contractReports_contractReportTechnicalForm_versionSubNum: 'Версия',
        contractReports_contractReportTechnicalForm_type: 'Тип',
        contractReports_contractReportTechnicalForm_createDate: 'Дата на създаване',
        contractReports_contractReportTechnicalForm_regDate: 'Дата на регистрация',
        contractReports_contractReportTechnicalForm_submitDate: 'Дата на представяне',
        contractReports_contractReportTechnicalForm_dateFrom: 'Начална дата',
        contractReports_contractReportTechnicalForm_dateTo: 'Крайна дата',
        contractReports_contractReportTechnicalForm_status: 'Статус',
        contractReports_contractReportTechnicalForm_statusNote: 'Причина за промяната на статуса',

        //contractReports_contractReportMicroForm
        contractReports_contractReportMicroForm_versionNum: 'Номер',
        contractReports_contractReportMicroForm_versionSubNum: 'Версия',
        contractReports_contractReportMicroForm_status: 'Статус',
        contractReports_contractReportMicroForm_statusNote: 'Причина за промяната на статуса',
        contractReports_contractReportMicroForm_excelFile: 'Excel',
        contractReports_contractReportMicroForm_isFromExternalSystem: 'Изтеглен от СИМЕВ',

        //contractReports_contractReportDocuments_reportFinancial
        contractReports_contractReportDocuments_reportFinancial_orderNum: 'Пореден номер',
        contractReports_contractReportDocuments_reportFinancial_status: 'Статус',
        contractReports_contractReportDocuments_reportFinancial_createDate: 'Дата на създаване',
        contractReports_contractReportDocuments_reportFinancial_startDate: 'Начална дата',
        contractReports_contractReportDocuments_reportFinancial_endDate: 'Крайна дата',
        contractReports_contractReportDocuments_reportFinancial_sentEmail: 'Изпратил',

        //contractReports_contractReportDocuments_reportTechnical
        contractReports_contractReportDocuments_reportTechnical_orderNum: 'Пореден номер',
        contractReports_contractReportDocuments_reportTechnical_status: 'Статус',
        contractReports_contractReportDocuments_reportTechnical_createDate: 'Дата на създаване',
        contractReports_contractReportDocuments_reportTechnical_type: 'Тип',
        contractReports_contractReportDocuments_reportTechnical_regDate: 'Дата на регистрация',
        contractReports_contractReportDocuments_reportTechnical_dateFrom: 'Начална дата',
        contractReports_contractReportDocuments_reportTechnical_dateTo: 'Крайна дата',

        //contractReports_contractReportDocuments_reportPayment
        contractReports_contractReportDocuments_reportPayment_orderNum: 'Пореден номер',
        contractReports_contractReportDocuments_reportPayment_status: 'Статус',
        contractReports_contractReportDocuments_reportPayment_createDate: 'Дата на създаване',
        contractReports_contractReportDocuments_reportPayment_paymentType: 'Тип',
        contractReports_contractReportDocuments_reportPayment_regDate: 'Дата на регистрация',
        contractReports_contractReportDocuments_reportPayment_dateFrom: 'Начална дата',
        contractReports_contractReportDocuments_reportPayment_dateTo: 'Крайна дата',
        contractReports_contractReportDocuments_reportPayment_requestedAmount:
          'Стойност на исканите средства',

        //contractReports_contractReportDocuments_reportMicro
        contractReports_contractReportDocuments_reportMicro_orderNum: 'Пореден номер',
        contractReports_contractReportDocuments_reportMicro_status: 'Статус',
        contractReports_contractReportDocuments_reportMicro_createDate: 'Дата на създаване',
        contractReports_contractReportDocuments_reportMicro_statusNote: 'Бележка',
        contractReports_contractReportDocuments_reportMicro_source: 'Въведен от',

        //contractReports_editContractReportDocumentsTechnical
        contractReports_editContractReportDocumentsTechnical_title: 'Преглед на технически отчет',
        contractReports_editContractReportDocumentsTechnical_draft: 'Чернова',
        contractReports_editContractReportDocumentsTechnical_delete: 'Изтриване',
        contractReports_editContractReportDocumentsTechnical_back: 'Назад',
        contractReports_editContractReportDocumentsTechnical_document: 'Документ',
        contractReports_editContractReportDocumentsTechnical_confirmTechnicalChangeStatus:
          "Сигурни ли сте, че искате да промените статуса на техническия отчет на 'Чернова'",

        //contractReports_editContractReportDocumentsFinancial
        contractReports_editContractReportDocumentsFinancial_title: 'Преглед на финансов отчет',
        contractReports_editContractReportDocumentsFinancial_draft: 'Чернова',
        contractReports_editContractReportDocumentsFinancial_delete: 'Изтриване',
        contractReports_editContractReportDocumentsFinancial_back: 'Назад',
        contractReports_editContractReportDocumentsFinancial_document: 'Документ',
        contractReports_editContractReportDocumentsFinancial_confirmFinancialChangeStatus:
          "Сигурни ли сте, че искате да промените статуса на финансовия отчет на 'Чернова'",

        //contractReports_editContractReportDocumentsPayment
        contractReports_editContractReportDocumentsPayment_title: 'Преглед на искане за плащане',
        contractReports_editContractReportDocumentsPayment_draft: 'Чернова',
        contractReports_editContractReportDocumentsPayment_delete: 'Изтриване',
        contractReports_editContractReportDocumentsPayment_back: 'Назад',
        contractReports_editContractReportDocumentsPayment_document: 'Документ',
        contractReports_editContractReportDocumentsPayment_confirmPaymentChangeStatus:
          "Сигурни ли сте, че искате да промените статуса на искането за плащане на 'Чернова'",

        //contractReports_editContractReportMicro
        contractReports_editContractReportMicro_type1Title:
          'Преглед на микроданни участници (ФЕПНЛ)',
        contractReports_editContractReportMicro_type2Title: 'Преглед на микроданни участници (ЕСФ)',
        contractReports_editContractReportMicro_type3Title:
          'Преглед на микроданни хранителни продукти',
        contractReports_editContractReportMicro_type4Title: 'Преглед на микроданни на АСП',
        contractReports_editContractReportMicro_edit: 'Редакция',
        contractReports_editContractReportMicro_enter: 'Въведен',
        contractReports_editContractReportMicro_changeToEntered:
          "Сигурни ли сте, че искате да промените статуса на микроданните на 'Въведен'",
        contractReports_editContractReportMicro_save: 'Запис',
        contractReports_editContractReportMicro_cancel: 'Отказ',
        contractReports_editContractReportMicro_draft: 'Чернова',
        contractReports_editContractReportMicro_changeToDraft:
          "Сигурни ли сте, че искате да промените статуса на микроданните на 'Чернова'",
        contractReports_editContractReportMicro_delete: 'Изтриване',
        contractReports_editContractReportMicro_back: 'Назад',
        contractReports_editContractReportMicro_micro: 'Микроданни',
        contractReports_editContractReportMicro_newMicroVersion: 'Нова версия',
        contractReports_editContractReportMicro_editNewMicroVersion: 'Редакция на версия',

        //contractReports_choosePaymentTypeModal
        contractReports_choosePaymentTypeModal_reportType: 'Тип на искането за плащане',

        //contractReportChecks_tabs
        contractReportChecks_tabs_contract: 'Договор',
        contractReportChecks_tabs_report: 'Основни данни',
        contractReportChecks_tabs_documents: 'Документи',
        contractReportChecks_tabs_checks: 'Проверки',
        contractReportChecks_tabs_csds: 'Верифицирани РОД',
        contractReportChecks_tabs_advPaymentAmounts: 'Верифицирано АП',
        contractReportChecks_tabs_advNVPaymentAmounts: 'АП',
        contractReportChecks_tabs_paymentChecks: 'Верифицирано ИП',
        contractReportChecks_tabs_attachedDocs: 'Свързани документи',
        contractReportChecks_tabs_paymentRequests: 'ИП до момента',
        contractReportChecks_tabs_indicators: 'Верифицирани индикатори',

        //contractReportChecks_sapData
        contractReportChecks_sapData_programmeCode: 'Код на основна организация',
        contractReportChecks_sapData_programmePriorityCode:
          'Код от СУНИ на разпоредител с бюджетни средства',
        contractReportChecks_sapData_financeSource: 'Фонд',
        contractReportChecks_sapData_procedureCode: 'Код на бюджет от СУНИ',
        contractReportChecks_sapData_contractCode: 'Код на договор от СУНИ',
        contractReportChecks_sapData_orderNum: 'Номер на пакет',
        contractReportChecks_sapData_paymentOrderNum: 'Номер на искане за плащане',
        contractReportChecks_sapData_companyUin: 'Идентификатор',
        contractReportChecks_sapData_companyName: 'Име на бенефициент',
        contractReportChecks_sapData_paidBfpTotalAmount:
          'Одобрена сума за плащане от крайната проверка на искането за плащане, в т.ч.:',
        contractReportChecks_sapData_bgAmount: '-Национално финансиране',
        contractReportChecks_sapData_euAmount: '-Европейско финансиране',
        contractReportChecks_sapData_crossAmount: '-Кръстосано финансиране',
        contractReportChecks_sapData_approvedBfpTotalAmount: 'Верифицирана сума, в т.ч.:',
        contractReportChecks_sapData_currency: 'Валута',
        contractReportChecks_sapData_submitDate: 'Дата на искането',
        contractReportChecks_sapData_checkedDate: 'Дата на верификация',
        contractReportChecks_sapData_reportType: 'Тип на пакет',
        contractReportChecks_sapData_paymentType: 'Тип на искане за плащане',
        contractReportChecks_sapData_print: 'Принтирай',
        contractReportChecks_sapData_printAll: 'Принтирай всички',

        //contractReportChecks_viewContract
        contractReportChecks_viewContract_title: 'Преглед на договор',
        contractReportChecks_viewContract_contractData: 'Общи данни',
        contractReportChecks_viewContract_beneficiary: 'Бенефициент',

        //contractReportChecks_search
        contractReportChecks_search_contractRegNum: 'Номер на договор',
        contractReportChecks_search_search: 'Търси',
        contractReportChecks_search_contractName: 'Договор',
        contractReportChecks_search_procedureName: 'Бюджет',
        contractReportChecks_search_orderNum: 'Пореден номер',
        contractReportChecks_search_status: 'Статус',
        contractReportChecks_search_source: 'Въведен от',
        contractReportChecks_search_reportType: 'Тип',
        contractReportChecks_search_regDate: 'Дата на регистрация',
        contractReportChecks_search_checkedDate: 'Дата на одобрение',
        contractReportChecks_search_submitDate: 'Дата на представяне',

        contractReportChecks_search_excelExport: 'Експорт',

        //contractReportChecks_viewContractReport
        contractReportChecks_viewContractReport_title:
          'Договор: {{contractName}}, Статус на пакета: {{reportStatus}}',

        //contractReportChecks_editContractReport
        contractReportChecks_editContractReport_title: 'Преглед на пакет отчетни документи',
        contractReportChecks_editContractReport_edit: 'Редакция',
        contractReportChecks_editContractReport_save: 'Запис',
        contractReportChecks_editContractReport_cancel: 'Отказ',
        contractReportChecks_editContractReport_unchecked: 'В проверка',
        contractReportChecks_editContractReport_accepted: 'Приет',
        contractReportChecks_editContractReport_refused: 'Отхвърлен',
        contractReportChecks_editContractReport_document: 'Документ',
        contractReportChecks_editContractReport_communication: 'Съобщение',
        contractReportChecks_editContractReport_status: 'Статус',
        contractReportChecks_editContractReport_statusChangeMessage:
          'Бележка към промяната на статуса',
        contractReportChecks_editContractReport_confirmChangeStatus:
          "Сигурни ли сте, че искате да промените статуса на пакета на '{{status}}'",
        contractReportChecks_editContractReport_returnToUnchecked: 'Върни в проверка',
        contractReportChecks_editContractReport_returnToUncheckedConfirm:
          "Сигурни ли сте, че искате да върнете пакета в статус 'В проверка'?",

        //contractReportChecks_contractReportDocuments
        contractReportChecks_contractReportDocuments_add: 'Добави',
        contractReportChecks_contractReportDocuments_advance: 'Авансово',
        contractReportChecks_contractReportDocuments_standard: 'Стандартно',
        contractReportChecks_contractReportDocuments_reportTechnical: 'Технически отчет',
        contractReportChecks_contractReportDocuments_reportFinancial: 'Финансов отчет',
        contractReportChecks_contractReportDocuments_reportPayment: 'Искане за плащане',

        //contractReportChecks_contractReportDocuments_reportFinancial
        contractReportChecks_contractReportDocuments_reportFinancial_versionNum: 'Номер',
        contractReportChecks_contractReportDocuments_reportFinancial_versionSubNum: 'Версия',
        contractReportChecks_contractReportDocuments_reportFinancial_status: 'Статус',
        contractReportChecks_contractReportDocuments_reportFinancial_submitDate:
          'Дата на представяне',
        contractReportChecks_contractReportDocuments_reportFinancial_returnDate: 'Дата на връщане',
        contractReportChecks_contractReportDocuments_reportFinancial_startDate: 'Начална дата',
        contractReportChecks_contractReportDocuments_reportFinancial_endDate: 'Крайна дата',

        //contractReportChecks_contractReportDocuments_reportTechnical
        contractReportChecks_contractReportDocuments_reportTechnical_versionNum: 'Номер',
        contractReportChecks_contractReportDocuments_reportTechnical_versionSubNum: 'Версия',
        contractReportChecks_contractReportDocuments_reportTechnical_status: 'Статус',
        contractReportChecks_contractReportDocuments_reportTechnical_submitDate:
          'Дата на представяне',
        contractReportChecks_contractReportDocuments_reportTechnical_returnDate: 'Дата на връщане',
        contractReportChecks_contractReportDocuments_reportTechnical_type: 'Тип',
        contractReportChecks_contractReportDocuments_reportTechnical_dateFrom: 'Начална дата',
        contractReportChecks_contractReportDocuments_reportTechnical_dateTo: 'Крайна дата',
        contractReportChecks_contractReportDocuments_reportTechnical_sentEmail: 'Изпратил',

        //contractReportChecks_contractReportDocuments_reportPayment
        contractReportChecks_contractReportDocuments_reportPayment_versionNum: 'Номер',
        contractReportChecks_contractReportDocuments_reportPayment_versionSubNum: 'Версия',
        contractReportChecks_contractReportDocuments_reportPayment_status: 'Статус',
        contractReportChecks_contractReportDocuments_reportPayment_submitDate:
          'Дата на представяне',
        contractReportChecks_contractReportDocuments_reportPayment_returnDate: 'Дата на връщане',
        contractReportChecks_contractReportDocuments_reportPayment_paymentType: 'Тип',
        contractReportChecks_contractReportDocuments_reportPayment_dateFrom: 'Начална дата',
        contractReportChecks_contractReportDocuments_reportPayment_dateTo: 'Крайна дата',
        contractReportChecks_contractReportDocuments_reportPayment_requestedAmount:
          'Стойност на исканите средства',
        contractReportChecks_contractReportDocuments_reportPayment_sentEmail: 'Изпратил',

        //contractReportChecks_contractReportDocuments_reportMicro
        contractReportChecks_contractReportDocuments_reportMicro_reportMicro_orderNum:
          'Пореден номер',
        contractReportChecks_contractReportDocuments_reportMicro_reportMicro_status: 'Статус',
        contractReportChecks_contractReportDocuments_reportMicro_reportMicro_createDate:
          'Дата на създаване',
        contractReportChecks_contractReportDocuments_reportMicro_reportMicro_statusNote: 'Бележка',
        contractReportChecks_contractReportDocuments_reportMicro_reportMicro_sentEmail: 'Изпратил',

        //contractReportChecks_editContractReportDocumentsTechnical
        contractReportChecks_editContractReportDocumentsTechnical_title:
          'Преглед на технически отчет',
        contractReportChecks_editContractReportDocumentsTechnical_draft: 'Чернова',
        contractReportChecks_editContractReportDocumentsTechnical_actual: 'Актуален',
        contractReportChecks_editContractReportDocumentsTechnical_returned: 'Върнат',
        contractReportChecks_editContractReportDocumentsTechnical_delete: 'Изтриване',
        contractReportChecks_editContractReportDocumentsTechnical_back: 'Назад',
        contractReportChecks_editContractReportDocumentsTechnical_document: 'Документ',
        contractReportChecks_editContractReportDocumentsTechnical_confirmDraftTechnical:
          "Сигурни ли сте, че искате да промените статуса на техническия отчет на 'Чернова'",
        contractReportChecks_editContractReportDocumentsTechnical_returnedReason:
          'Причина за промяната на статуса',
        contractReportChecks_editContractReportDocumentsTechnical_confirmReturnTechnical:
          "Сигурни ли сте, че искате да промените статуса на техническия отчет на 'Върнат'?",
        contractReportChecks_editContractReportDocumentsTechnical_confirmActualTechnical:
          "Сигурни ли сте, че искате да промените статуса на техническия отчет на 'Актуален'",

        //contractReportChecks_editContractReportDocumentsFinancial
        contractReportChecks_editContractReportDocumentsFinancial_title:
          'Преглед на финансов отчет',
        contractReportChecks_editContractReportDocumentsFinancial_draft: 'Чернова',
        contractReportChecks_editContractReportDocumentsFinancial_actual: 'Актуален',
        contractReportChecks_editContractReportDocumentsFinancial_returned: 'Върнат',
        contractReportChecks_editContractReportDocumentsFinancial_delete: 'Изтриване',
        contractReportChecks_editContractReportDocumentsFinancial_back: 'Назад',
        contractReportChecks_editContractReportDocumentsFinancial_document: 'Документ',
        contractReportChecks_editContractReportDocumentsFinancial_confirmDraftFinancial:
          "Сигурни ли сте, че искате да промените статуса на финансовия отчет на 'Чернова'",
        contractReportChecks_editContractReportDocumentsFinancial_returnedReason:
          'Причина за промяната на статуса',
        contractReportChecks_editContractReportDocumentsFinancial_confirmReturnFinancial:
          "Сигурни ли сте, че искате да промените статуса на финансовия отчет на 'Върнат'?",
        contractReportChecks_editContractReportDocumentsFinancial_confirmActualFinancial:
          "Сигурни ли сте, че искате да промените статуса на финансовия отчет на 'Актуален'",

        //contractReportChecks_editContractReportDocumentsPayment
        contractReportChecks_editContractReportDocumentsPayment_title:
          'Преглед на искане за плащане',
        contractReportChecks_editContractReportDocumentsPayment_draft: 'Чернова',
        contractReportChecks_editContractReportDocumentsPayment_actual: 'Актуално',
        contractReportChecks_editContractReportDocumentsPayment_returned: 'Върнато',
        contractReportChecks_editContractReportDocumentsPayment_delete: 'Изтриване',
        contractReportChecks_editContractReportDocumentsPayment_back: 'Назад',
        contractReportChecks_editContractReportDocumentsPayment_document: 'Документ',
        contractReportChecks_editContractReportDocumentsPayment_confirmDraftPayment:
          "Сигурни ли сте, че искате да промените статуса на искането за плащане отчет на 'Чернова'",
        contractReportChecks_editContractReportDocumentsPayment_returnedReason:
          'Причина за промяната на статуса',
        contractReportChecks_editContractReportDocumentsPayment_confirmReturnPayment:
          "Сигурни ли сте, че искате да промените статуса на искането за плащане на 'Върнато'? " +
          'Това ще изтрие всички верифицирани верифицирани АП!',
        contractReportChecks_editContractReportDocumentsPayment_confirmActualPayment:
          "Сигурни ли сте, че искате да промените статуса на искането за плащане на 'Актуално'",

        //contractReportChecks_editContractReportMicro

        contractReportChecks_editContractReportMicro_edit: 'Редакция',
        contractReportChecks_editContractReportMicro_enter: 'Въведен',
        contractReportChecks_editContractReportMicro_changeToEntered:
          "Сигурни ли сте, че искате да промените статуса на микроданните на 'Въведен'",
        contractReportChecks_editContractReportMicro_save: 'Запис',
        contractReportChecks_editContractReportMicro_cancel: 'Отказ',
        contractReportChecks_editContractReportMicro_draft: 'Чернова',
        contractReportChecks_editContractReportMicro_changeToDraft:
          "Сигурни ли сте, че искате да промените статуса на микроданните на 'Чернова'",
        contractReportChecks_editContractReportMicro_actual: 'Актуален',
        contractReportChecks_editContractReportMicro_confirmActual:
          "Сигурни ли сте, че искате да промените статуса на микроданните на 'Актуален'",
        contractReportChecks_editContractReportMicro_returned: 'Върнат',
        contractReportChecks_editContractReportMicro_returnedReason:
          'Причина за промяната на статуса',
        contractReportChecks_editContractReportMicro_confirmReturn:
          "Сигурни ли сте, че искате да промените статуса на микроданните на 'Върнат'?",
        contractReportChecks_editContractReportMicro_back: 'Назад',
        contractReportChecks_editContractReportMicro_micro: 'Микроданни',
        contractReportChecks_editContractReportMicro_confirmNewVersion:
          "Промяната на статуса на текущата версия ще промени статуса на предходната актуалната версия на 'Върнат'" +
          ' с посочената причина. Сигурни ли сте, че искате да продължите?',
        contractReportChecks_editContractReportMicro_delete: 'Изтриване',
        contractReportChecks_editContractReportMicro_confirmDelete:
          'Сигурни ли сте, че искате да изтриете тази версия на микроданните',
        //contractReportChecks_contractReportChecks
        contractReportChecks_contractReportChecks_add: 'Добави',
        contractReportChecks_contractReportChecks_reportTechnical: 'Проверка на технически отчет',
        contractReportChecks_contractReportChecks_reportFinancial: 'Проверка на финансов отчет',
        contractReportChecks_contractReportChecks_reportFinancial_financialVersionNum:
          'Номер на финансов отчет',
        contractReportChecks_contractReportChecks_reportFinancial_financialVersionSubNum:
          'Версия на финансов отчет',
        contractReportChecks_contractReportChecks_reportFinancial_orderNum: 'Номер',
        contractReportChecks_contractReportChecks_reportFinancial_status: 'Статус',
        contractReportChecks_contractReportChecks_reportFinancial_checkedByUser: 'Проверено от',
        contractReportChecks_contractReportChecks_reportFinancial_createDate: 'Дата на създаване',
        contractReportChecks_contractReportChecks_reportTechnical_technicalVersionNum:
          'Номер на технически отчет',
        contractReportChecks_contractReportChecks_reportTechnical_technicalVersionSubNum:
          'Версия на технически отчет',
        contractReportChecks_contractReportChecks_reportTechnical_orderNum: 'Номер',
        contractReportChecks_contractReportChecks_reportTechnical_status: 'Статус',
        contractReportChecks_contractReportChecks_reportTechnical_checkedByUser: 'Проверено от',
        contractReportChecks_contractReportChecks_reportTechnical_createDate: 'Дата на създаване',

        //contractReportChecks_contractReportPaymentChecks
        contractReportChecks_contractReportPaymentChecks_add: 'Добави',
        contractReportChecks_contractReportPaymentChecks_paymentVersionNum:
          'Номер на искане за плащане',
        contractReportChecks_contractReportPaymentChecks_paymentVersionSubNum:
          'Версия на искане за плащане',
        contractReportChecks_contractReportPaymentChecks_orderNum: 'Номер',
        contractReportChecks_contractReportPaymentChecks_status: 'Статус',
        contractReportChecks_contractReportPaymentChecks_checkedByUser: 'Проверено от',
        contractReportChecks_contractReportPaymentChecks_createDate: 'Дата на създаване',

        //contractReportChecks_contractReportChecksFinancialForm
        contractReportChecks_contractReportChecksFinancialForm_orderNum: 'Номер',
        contractReportChecks_contractReportChecksFinancialForm_status: 'Статус',
        contractReportChecks_contractReportChecksFinancialForm_approval: 'Одобрение',
        contractReportChecks_contractReportChecksFinancialForm_file: 'Файл',
        contractReportChecks_contractReportChecksFinancialForm_checkedDate: 'Дата на проверка',
        contractReportChecks_contractReportChecksFinancialForm_checkedByUser: 'Проверено от',

        //contractReportChecks_editContractReportChecksFinancial
        contractReportChecks_editContractReportChecksFinancial_activeReason:
          "Сигурни ли сте, че искате да промените статуса на проверката на 'Актуална'",
        contractReportChecks_editContractReportChecksFinancial_title:
          'Редакция на проверка на финансов отчет',
        contractReportChecks_editContractReportChecksFinancial_active: 'Актуална',
        contractReportChecks_editContractReportChecksFinancial_delete: 'Изтриване',
        contractReportChecks_editContractReportChecksFinancial_edit: 'Редакция',
        contractReportChecks_editContractReportChecksFinancial_save: 'Запис',
        contractReportChecks_editContractReportChecksFinancial_cancel: 'Отказ',
        contractReportChecks_editContractReportChecksFinancial_back: 'Назад',
        contractReportChecks_editContractReportChecksFinancial_reportFinancial: 'Финансов отчет',
        contractReportChecks_editContractReportChecksFinancial_reportFinancialCheck: 'Проверка',
        contractReportChecks_editContractReportChecksFinancial_document: 'Документ',

        //contractReportChecks_contractReportChecksPaymentForm
        contractReportChecks_contractReportChecksPaymentForm_orderNum: 'Номер',
        contractReportChecks_contractReportChecksPaymentForm_status: 'Статус',
        contractReportChecks_contractReportChecksPaymentForm_approval: 'Одобрение',
        contractReportChecks_contractReportChecksPaymentForm_file: 'Файл',
        contractReportChecks_contractReportChecksPaymentForm_checkedDate: 'Дата на верифициране',
        contractReportChecks_contractReportChecksPaymentForm_checkedByUser: 'Проверено от',
        contractReportChecks_contractReportChecksPaymentForm_approvedAmounts:
          'Стойност на верифицирани средства',
        contractReportChecks_contractReportChecksPaymentForm_approvedEuAmount: 'БФП - ЕС',
        contractReportChecks_contractReportChecksPaymentForm_approvedBgAmount: 'Общо',
        contractReportChecks_contractReportChecksPaymentForm_approvedBfpTotalAmount: 'Общо БФП',
        contractReportChecks_contractReportChecksPaymentForm_approvedCrossAmount:
          'Кръстосано съфинансиране',
        contractReportChecks_contractReportChecksPaymentForm_approvedSelfAmount:
          'Собств. съфинансиране',
        contractReportChecks_contractReportChecksPaymentForm_approvedTotalAmount: 'Общo',
        contractReportChecks_contractReportChecksPaymentForm_paidAmounts:
          'Стойност на сумите за плащане',
        contractReportChecks_contractReportChecksPaymentForm_paidEuAmount: 'БФП - ЕС',
        contractReportChecks_contractReportChecksPaymentForm_paidBgAmount: 'Общо',
        contractReportChecks_contractReportChecksPaymentForm_paidBfpTotalAmount: 'Общо БФП',
        contractReportChecks_contractReportChecksPaymentForm_paidCrossAmount:
          'Кръстосано съфинансиране',

        //contractReportChecks_editContractReportChecksPayment
        contractReportChecks_editContractReportChecksPayment_activeReason:
          "Сигурни ли сте, че искате да промените статуса на верифицираното искане за плащане на 'Актуално'? " +
          'Датата на верификация на пакета ще приеме стойността на датата на верификация на това верифицираното искане за плащане!',
        contractReportChecks_editContractReportChecksPayment_title:
          'Редакция на верифицирано искане за плащане',
        contractReportChecks_editContractReportChecksPayment_active: 'Актуално',
        contractReportChecks_editContractReportChecksPayment_archived: 'Архивирано',
        contractReportChecks_editContractReportChecksPayment_archivedReason:
          "Сигурни ли сте, че искате да промените статуса на верифицираното искане за плащане на 'Архивирано'",
        contractReportChecks_editContractReportChecksPayment_edit: 'Редакция',
        contractReportChecks_editContractReportChecksPayment_save: 'Запис',
        contractReportChecks_editContractReportChecksPayment_cancel: 'Отказ',
        contractReportChecks_editContractReportChecksPayment_delete: 'Изтриване',
        contractReportChecks_editContractReportChecksPayment_back: 'Назад',
        contractReportChecks_editContractReportChecksPayment_reportPayment: 'Искане за плащане',
        contractReportChecks_editContractReportChecksPayment_reportPaymentCheck:
          'Верифицирано искане за плащане',
        contractReportChecks_editContractReportChecksPayment_document: 'Документ',

        //contractReportChecks_contractReportChecksTechnicalForm
        contractReportChecks_contractReportChecksTechnicalForm_orderNum: 'Номер',
        contractReportChecks_contractReportChecksTechnicalForm_status: 'Статус',
        contractReportChecks_contractReportChecksTechnicalForm_approval: 'Одобрение',
        contractReportChecks_contractReportChecksTechnicalForm_file: 'Файл',
        contractReportChecks_contractReportChecksTechnicalForm_checkedDate: 'Дата на проверка',
        contractReportChecks_contractReportChecksTechnicalForm_checkedByUser: 'Проверено от',

        //contractReportChecks_editContractReportChecksTechnical
        contractReportChecks_editContractReportChecksTechnical_activeReason:
          "Сигурни ли сте, че искате да промените статуса на проверката на 'Актуална'",
        contractReportChecks_editContractReportChecksTechnical_title:
          'Редакция на проверка на технически отчет',
        contractReportChecks_editContractReportChecksTechnical_active: 'Актуална',
        contractReportChecks_editContractReportChecksTechnical_delete: 'Изтриване',
        contractReportChecks_editContractReportChecksTechnical_edit: 'Редакция',
        contractReportChecks_editContractReportChecksTechnical_save: 'Запис',
        contractReportChecks_editContractReportChecksTechnical_cancel: 'Отказ',
        contractReportChecks_editContractReportChecksTechnical_back: 'Назад',
        contractReportChecks_editContractReportChecksTechnical_reportTechnical: 'Технически отчет',
        contractReportChecks_editContractReportChecksTechnical_reportTechnicalCheck: 'Проверка',
        contractReportChecks_editContractReportChecksTechnical_document: 'Документ',

        //contractReportChecks_contractReportChecksMicroForm

        //contractReportChecks_editContractReportChecksMicro

        //contractReportFinancialCorrections_tabs
        contractReportFinancialCorrections_tabs_contract: 'Договор',
        contractReportFinancialCorrections_tabs_report: 'Пакет',
        contractReportFinancialCorrections_tabs_data: 'Основни данни',
        contractReportFinancialCorrections_tabs_csds: 'Верифицирани РОД',
        contractReportFinancialCorrections_tabs_correctedCsds: 'Коригирани верифицирани РОД',

        //contractReportFinancialCorrections_viewContractReportFinancialCorrection
        contractReportFinancialCorrections_viewContractReportFinancialCorrection_title:
          'Договор: {{contractName}}',

        //contractReportFinancialCorrections_search
        contractReportFinancialCorrections_search_new: 'Нова корекция',
        contractReportFinancialCorrections_search_search: 'Търси',
        contractReportFinancialCorrections_search_contractRegNum: 'Номер на договор',
        contractReportFinancialCorrections_search_fromDate: 'От дата',
        contractReportFinancialCorrections_search_toDate: 'До дата',
        contractReportFinancialCorrections_search_contractName: 'Договор',
        contractReportFinancialCorrections_search_procedureName: 'Бюджет',
        contractReportFinancialCorrections_search_reportOrderNum: 'Номер на пакет',

        contractReportFinancialCorrections_search_status: 'Статус',
        contractReportFinancialCorrections_search_orderNum: 'Пореден номер',
        contractReportFinancialCorrections_search_correctedApprovedBfpTotalAmount:
          'Коригирана одобрена сума-БФП',
        contractReportFinancialCorrections_search_correctedApprovedSelfAmount:
          'Коригирана одобрена сума-СФ',
        contractReportFinancialCorrections_search_correctionDate: 'Дата на корекцията',
        contractReportFinancialCorrections_search_createDate: 'Дата на създаване',
        contractReportFinancialCorrections_search_notes: 'Бележки',

        //contractReportFinancialCorrections_newContractReportFinancialCorrection
        contractReportFinancialCorrections_newContractReportFinancialCorrection_title:
          'Нова корекция',
        contractReportFinancialCorrections_newContractReportFinancialCorrection_save: 'Запис',
        contractReportFinancialCorrections_newContractReportFinancialCorrection_cancel: 'Отказ',
        contractReportFinancialCorrections_newContractReportFinancialCorrection_procedureId:
          'Бюджет',
        contractReportFinancialCorrections_newContractReportFinancialCorrection_contractNumber:
          'Номер на договор',
        contractReportFinancialCorrections_newContractReportFinancialCorrection_contractReportNumber:
          'Номер на пакет',
        contractReportFinancialCorrections_newContractReportFinancialCorrection_chooseContractReport:
          'Търси',
        contractReportFinancialCorrections_newContractReportFinancialCorrection_contractNumberInvalid:
          'Невалиден номер на договор',
        contractReportFinancialCorrections_newContractReportFinancialCorrection_contractReportNumberInvalid:
          'Невалиден номер на пакет',

        //contractReportFinancialCorrections_chooseContractReportModal
        contractReportFinancialCorrections_chooseContractReportModal_title:
          'Избор на пакет отчетни документи',
        contractReportFinancialCorrections_chooseContractReportModal_cancel: 'Отказ',
        contractReportFinancialCorrections_chooseContractReportModal_search: 'Търси',
        contractReportFinancialCorrections_chooseContractReportModal_choose: 'Избери',
        contractReportFinancialCorrections_chooseContractReportModal_contractRegNum:
          'Номер на договор',

        contractReportFinancialCorrections_chooseContractReportModal_contractName: 'Договор',
        contractReportFinancialCorrections_chooseContractReportModal_procedureName: 'Бюджет',
        contractReportFinancialCorrections_chooseContractReportModal_orderNum: 'Пореден номер',
        contractReportFinancialCorrections_chooseContractReportModal_status: 'Статус',
        contractReportFinancialCorrections_chooseContractReportModal_source: 'Въведен от',
        contractReportFinancialCorrections_chooseContractReportModal_reportType: 'Тип',
        contractReportFinancialCorrections_chooseContractReportModal_regDate: 'Дата на регистрация',
        contractReportFinancialCorrections_chooseContractReportModal_procedure: 'Бюджет',
        contractReportFinancialCorrections_chooseContractReportModal_contractNumber:
          'Номер на договор',
        contractReportFinancialCorrections_chooseContractReportModal_contractReportNum:
          'Номер на пакет',

        //contractReportFinancialCorrections_viewContract
        contractReportFinancialCorrections_viewContract_title: 'Преглед на договор',
        contractReportFinancialCorrections_viewContract_contractData: 'Общи данни',
        contractReportFinancialCorrections_viewContract_beneficiary: 'Бенефициент',

        //contractReportFinancialCorrections_viewContractReport

        //contractReportFinancialCorrections_editContractReportFinancialCorrection_title
        contractReportFinancialCorrections_editContractReportFinancialCorrection_title:
          'Редакция на корекция',
        contractReportFinancialCorrections_editContractReportFinancialCorrection_ended: 'Приключен',
        contractReportFinancialCorrections_editContractReportFinancialCorrection_endedReason:
          "Сигурни ли сте, че искате да промените статуса на корекцията на 'Приключен'",
        contractReportFinancialCorrections_editContractReportFinancialCorrection_draft: 'Чернова',
        contractReportFinancialCorrections_editContractReportFinancialCorrection_draftReason:
          "Сигурни ли сте, че искате да промените статуса на корекцията на 'Чернова'",
        contractReportFinancialCorrections_editContractReportFinancialCorrection_edit: 'Редакция',
        contractReportFinancialCorrections_editContractReportFinancialCorrection_save: 'Запис',
        contractReportFinancialCorrections_editContractReportFinancialCorrection_cancel: 'Отказ',
        contractReportFinancialCorrections_editContractReportFinancialCorrection_delete:
          'Изтриване',

        //contractReportFinancialCorrections_contractReportFinancialCorrectionForm
        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_reportPayment:
          'Искане за плащане',
        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_reportFinancial:
          'Финансов отчет',
        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_reportFinancialCorrection:
          'Корекция',
        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_orderNum: 'Номер',
        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_status: 'Статус',
        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_document:
          'Документ',

        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_notes: 'Бележки',
        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_file: 'Файл',
        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_correctionDate:
          'Дата на корекцията',
        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_checkedDate:
          'Дата на проверка',
        contractReportFinancialCorrections_contractReportFinancialCorrectionForm_checkedByUser:
          'Проверено от',

        //contractReportFinancialCorrections_viewContractReportFinancialCorrectionReport_title
        contractReportFinancialCorrections_viewContractReportFinancialCorrectionReport_title:
          'Преглед на пакет отчетни документи',

        //contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_csd:
          'Разходооправдателен документ',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_budgetDetail:
          'Ред от бюджета',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_contractActivity:
          'Дейност',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_csdAmount:
          'Сума на РОД',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_approvedAmount:
          'Одобрена сума',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_totalAmount:
          'Обща сума',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_status:
          'Статус',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_approval:
          'Съгласие',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_company:
          'Бенефициент/Партньор/Изпълнител',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_search:
          'Търси',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsSearch_confirmCorrect:
          'Сигурни ли сте, че искате да коригирате този верифициран РОД ? Това ще премести записа в ' +
          "таб 'Коригирани верифицирани РОД'",

        //contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsView
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCSDsView_title:
          'Преглед на разходооправдателен документ',

        //contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_csd:
          'Разходооправдателен документ',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_budgetDetail:
          'Ред от бюджета',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_contractActivity:
          'Дейност',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_approvedAmount:
          'Одобрена сума',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_correctedApprovedAmount:
          'Коригирана одобрена сума',

        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_totalAmount:
          'Обща сума',

        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_status:
          'Статус',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_approval:
          'Съгласие',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_sign:
          'Знак',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_company:
          'Бенефициент/Партньор/Изпълнител',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsSearch_search:
          'Търси',

        //contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch
        contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch_csd:
          'Разходооправдателен документ',
        contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch_budgetDetail:
          'Ред от бюджета',
        contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch_contractActivity:
          'Дейност',
        contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch_csdAmount: 'Сума на РОД',
        contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch_approvedAmount:
          'Одобрена сума',

        contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch_status: 'Статус',
        contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch_approval: 'Съгласие',
        contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch_company:
          'Бенефициент/Партньор/Изпълнител',
        contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch_search: 'Търси',
        contractReportChecks_contractReportsFinancialCSDBudgetItemsSearch_excelExport: 'Експорт',

        //contractReportFinancialCorrections_modals_correctionCSDBudgetItemModal
        contractReportFinancialCorrections_modals_correctionCSDBudgetItemModal_editTitle:
          'Редакция на коригиран разходооправдателен документ',
        contractReportFinancialCorrections_modals_correctionCSDBudgetItemModal_viewTitle:
          'Преглед на коригиран разходооправдателен документ',
        contractReportFinancialCorrections_modals_correctionCSDBudgetItemModal_save: 'Запис',
        contractReportFinancialCorrections_modals_correctionCSDBudgetItemModal_edit: 'Редакция',
        contractReportFinancialCorrections_modals_correctionCSDBudgetItemModal_cancel: 'Отказ',
        contractReportFinancialCorrections_modals_correctionCSDBudgetItemModal_ended: 'Приключен',

        //contractReportFinancialCorrections_correctionCSDBudgetItemForm
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_correction:
          'Коригиране на разходооправдателен документ',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_sign: 'Знак',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_signPlusNote:
          'Изборът на знак "+" ще доведе до намаляване на верифицираните суми',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_signMinusNote:
          'Изборът на знак "-" ще доведе до увеличаване на верифицираните суми',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_status: 'Статус',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_notes: 'Бележки',

        contractReportFinancialCorrections_correctionCSDBudgetItemForm_totalAmount: 'Обща сума',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_checkedByUser:
          'Проверено от',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_checkedDate:
          'Дата на проверка',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_correctedUnapprovedAmount:
          'Коригирана неверифицирана сума на разходооправдателния документ за конкретния бюджетен ред и дейност',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_correctedUnapprovedByCorrectionAmount:
          'Коригирана неверифицирана сума на разходооправдателен документ по наложена финансова корекция за конкретния бюджетен ред и дейност',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_correctedApprovedAmount:
          'Коригирана oдобрена сума на разходооправдателния документ за конкретния бюджетен ред и дейност',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_correctionType:
          'Тип на корекцията',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_irregularityId: 'Нередност',
        contractReportFinancialCorrections_correctionCSDBudgetItemForm_financialCorrectionId:
          'Финансова корекция',

        //contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsEdit
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsEdit_editTitle:
          'Редакция на коригиран разходооправдателен документ',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsEdit_viewTitle:
          'Преглед на коригиран разходооправдателен документ',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsEdit_save:
          'Запис',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsEdit_edit:
          'Редакция',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsEdit_cancel:
          'Отказ',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsEdit_ended:
          'Приключен',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsEdit_draft:
          'Чернова',
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsEdit_endedConfirm:
          "Сигурни ли сте, че искате да смените статуса на коригирането на разходооправдателния документ на 'Приключен'?",
        contractReportFinancialCorrections_contractReportFinancialCorrectionsCorrectedCSDsEdit_draftConfirm:
          "Сигурни ли сте, че искате да смените статуса на коригирането на разходооправдателния документ на 'Чернова'?",

        //contractReportTechnicalCorrections_tabs
        contractReportTechnicalCorrections_tabs_contract: 'Договор',
        contractReportTechnicalCorrections_tabs_report: 'Пакет',
        contractReportTechnicalCorrections_tabs_data: 'Основни данни',
        contractReportTechnicalCorrections_tabs_indicators: 'Верифицирани индикатори',
        contractReportTechnicalCorrections_tabs_correctedIndicators:
          'Коригирани верифицирани индикатори',

        //contractReportTechnicalCorrections_viewContractReportTechnicalCorrection

        //contractReportTechnicalCorrections_search
        contractReportTechnicalCorrections_search_new: 'Нова корекция',
        contractReportTechnicalCorrections_search_search: 'Търси',
        contractReportTechnicalCorrections_search_contractRegNum: 'Номер на договор',
        contractReportTechnicalCorrections_search_fromDate: 'От дата',
        contractReportTechnicalCorrections_search_toDate: 'До дата',
        contractReportTechnicalCorrections_search_contractName: 'Договор',
        contractReportTechnicalCorrections_search_procedureName: 'Бюджет',
        contractReportTechnicalCorrections_search_reportOrderNum: 'Номер на пакет',
        contractReportTechnicalCorrections_search_status: 'Статус',
        contractReportTechnicalCorrections_search_orderNum: 'Пореден номер',
        contractReportTechnicalCorrections_search_correctionDate: 'Дата на корекцията',
        contractReportTechnicalCorrections_search_createDate: 'Дата на създаване',
        contractReportTechnicalCorrections_search_notes: 'Бележки',

        //contractReportTechnicalCorrections_newContractReportTechnicalCorrection
        contractReportTechnicalCorrections_newContractReportTechnicalCorrection_title:
          'Нова корекция',
        contractReportTechnicalCorrections_newContractReportTechnicalCorrection_save: 'Запис',
        contractReportTechnicalCorrections_newContractReportTechnicalCorrection_cancel: 'Отказ',
        contractReportTechnicalCorrections_newContractReportTechnicalCorrection_procedureId:
          'Бюджет',
        contractReportTechnicalCorrections_newContractReportTechnicalCorrection_contractNumber:
          'Номер на договор',
        contractReportTechnicalCorrections_newContractReportTechnicalCorrection_contractReportNumber:
          'Номер на пакет',
        contractReportTechnicalCorrections_newContractReportTechnicalCorrection_chooseContractReport:
          'Търси',
        contractReportTechnicalCorrections_newContractReportTechnicalCorrection_contractNumberInvalid:
          'Невалиден номер на договор',
        contractReportTechnicalCorrections_newContractReportTechnicalCorrection_contractReportNumberInvalid:
          'Невалиден номер на пакет',
        contractReportTechnicalCorrections_newContractReportTechnicalCorrection_confirmCreate:
          "Вече съществува корекция на верифицирани индикатори със статус 'Приключен' към същия пакет отчетни документи. " +
          'Верифицираните индикатори от последната такава корекция ще бъдат заредени.',

        //contractReportTechnicalCorrections_chooseContractReportModal
        contractReportTechnicalCorrections_chooseContractReportModal_title:
          'Избор на пакет отчетни документи',
        contractReportTechnicalCorrections_chooseContractReportModal_cancel: 'Отказ',
        contractReportTechnicalCorrections_chooseContractReportModal_search: 'Търси',
        contractReportTechnicalCorrections_chooseContractReportModal_choose: 'Избери',
        contractReportTechnicalCorrections_chooseContractReportModal_contractRegNum:
          'Номер на договор',
        contractReportTechnicalCorrections_chooseContractReportModal_contractName: 'Договор',
        contractReportTechnicalCorrections_chooseContractReportModal_procedureName: 'Бюджет',
        contractReportTechnicalCorrections_chooseContractReportModal_orderNum: 'Пореден номер',
        contractReportTechnicalCorrections_chooseContractReportModal_status: 'Статус',
        contractReportTechnicalCorrections_chooseContractReportModal_source: 'Въведен от',
        contractReportTechnicalCorrections_chooseContractReportModal_reportType: 'Тип',
        contractReportTechnicalCorrections_chooseContractReportModal_regDate: 'Дата на регистрация',
        contractReportTechnicalCorrections_chooseContractReportModal_procedure: 'Бюджет',
        contractReportTechnicalCorrections_chooseContractReportModal_contractNumber:
          'Номер на договор',
        contractReportTechnicalCorrections_chooseContractReportModal_contractReportNum:
          'Номер на пакет',

        //contractReportTechnicalCorrections_viewContract
        contractReportTechnicalCorrections_viewContract_title: 'Преглед на договор',
        contractReportTechnicalCorrections_viewContract_contractData: 'Общи данни',
        contractReportTechnicalCorrections_viewContract_beneficiary: 'Бенефициент',

        //contractReportTechnicalCorrections_viewContractReport

        //contractReportTechnicalCorrections_editContractReportTechnicalCorrection_title
        contractReportTechnicalCorrections_editContractReportTechnicalCorrection_title:
          'Редакция на корекция',
        contractReportTechnicalCorrections_editContractReportTechnicalCorrection_ended: 'Приключен',
        contractReportTechnicalCorrections_editContractReportTechnicalCorrection_endedReason:
          "Сигурни ли сте, че искате да промените статуса на корекцията на 'Приключен'",
        contractReportTechnicalCorrections_editContractReportTechnicalCorrection_draft: 'Чернова',
        contractReportTechnicalCorrections_editContractReportTechnicalCorrection_draftReason:
          "Сигурни ли сте, че искате да промените статуса на корекцията на 'Чернова'",
        contractReportTechnicalCorrections_editContractReportTechnicalCorrection_edit: 'Редакция',
        contractReportTechnicalCorrections_editContractReportTechnicalCorrection_save: 'Запис',
        contractReportTechnicalCorrections_editContractReportTechnicalCorrection_cancel: 'Отказ',
        contractReportTechnicalCorrections_editContractReportTechnicalCorrection_delete:
          'Изтриване',

        //contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm_reportTechnical:
          'Технически отчет',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm_reportTechnicalCorrection:
          'Корекция',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm_orderNum: 'Номер',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm_status: 'Статус',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm_document:
          'Документ',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm_notes: 'Бележки',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm_file: 'Файл',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm_correctionDate:
          'Дата на корекцията',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm_checkedDate:
          'Дата на проверка',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionForm_checkedByUser:
          'Проверено от',

        //contractReportTechnicalCorrections_viewContractReportTechnicalCorrectionReport_title
        contractReportTechnicalCorrections_viewContractReportTechnicalCorrectionReport_title:
          'Преглед на пакет отчетни документи',

        //contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch_indicator:
          'Индикатор',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch_status:
          'Статус',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch_approval:
          'Одобрение',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch_indicatorAmount:
          'Отчетени стойности',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch_approvedAmount:
          'Одобрени стойности',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch_periodAmount:
          'Ст-ст за периода',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch_cumulativeAmount:
          'Ст-ст с натрупване',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch_residueAmount:
          'Остатък спрямо договора',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch_confirmCorrect:
          "Сигурни ли сте, че искате да коригирате този верифициран индикатор? Това ще премести записа в таб 'Коригирани верифицирани индикатори'",

        //contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsView
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsView_title:
          'Преглед на верифициран индикатор',

        //contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsSearch
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsSearch_indicator:
          'Индикатор',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsSearch_status:
          'Статус',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsSearch_approvedAmount:
          'Одобрени стойности',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsSearch_correctedApprovedAmount:
          'Коригирани одобрени стойности',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsSearch_periodAmount:
          'Ст-ст за периода',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsSearch_cumulativeAmount:
          'Ст-ст с натрупване',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsSearch_residueAmount:
          'Остатък спрямо договора',

        //contractReportTechnicalCorrections_correctionIndicatorForm
        contractReportTechnicalCorrections_correctionIndicatorForm_correction:
          'Коригиране на верифициран индикатор',
        contractReportTechnicalCorrections_correctionIndicatorForm_status: 'Статус',
        contractReportTechnicalCorrections_correctionIndicatorForm_notes: 'Бележки',
        contractReportTechnicalCorrections_correctionIndicatorForm_checkedByUser: 'Проверено от',
        contractReportTechnicalCorrections_correctionIndicatorForm_checkedDate: 'Дата на проверка',
        contractReportTechnicalCorrections_correctionIndicatorForm_correctedApprovedPeriodAmount:
          'Коригирана одобрена стойност за периода',
        contractReportTechnicalCorrections_correctionIndicatorForm_correctedApprovedPeriodAmountMen:
          'Коригирана одобрена стойност за периода (мъже)',
        contractReportTechnicalCorrections_correctionIndicatorForm_correctedApprovedPeriodAmountWomen:
          'Коригирана одобрена стойност за периода (жени)',
        contractReportTechnicalCorrections_correctionIndicatorForm_correctedApprovedCumulativeAmount:
          'Коригирана одобрена стойност с натрупване',
        contractReportTechnicalCorrections_correctionIndicatorForm_correctedApprovedCumulativeAmountMen:
          'Коригирана одобрена стойност с натрупване (мъже)',
        contractReportTechnicalCorrections_correctionIndicatorForm_correctedApprovedCumulativeAmountWomen:
          'Коригирана одобрена стойност с натрупване (жени)',
        contractReportTechnicalCorrections_correctionIndicatorForm_lastReportCorrectedCumulativeAmount:
          'Коригирана одобрена стойност от предходен отчет',
        contractReportTechnicalCorrections_correctionIndicatorForm_lastReportCorrectedCumulativeAmountMen:
          'Коригирана одобрена стойност от предходен отчет (мъже)',
        contractReportTechnicalCorrections_correctionIndicatorForm_lastReportCorrectedCumulativeAmountWomen:
          'Коригирана одобрена стойност от предходен отчет (жени)',
        contractReportTechnicalCorrections_correctionIndicatorForm_correctedApprovedResidueAmount:
          'Коригиран одобрен остатък спрямо договора',
        contractReportTechnicalCorrections_correctionIndicatorForm_correctedApprovedResidueAmountMen:
          'Коригиран одобрен остатък спрямо договора (мъже)',
        contractReportTechnicalCorrections_correctionIndicatorForm_correctedApprovedResidueAmountWomen:
          'Коригиран одобрен остатък спрямо договора (жени)',

        //contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_editTitle:
          'Редакция на коригиран верифициран индикатор',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_viewTitle:
          'Преглед на коригиран верифициран индикатор',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_save:
          'Запис',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_edit:
          'Редакция',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_delete:
          'Изтриване',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_cancel:
          'Отказ',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_ended:
          'Приключен',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_endedConfirm:
          "Сигурни ли сте, че искате да смените статуса на корекцията на верифициран индикатор на 'Приключен'?",
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_draft:
          'Чернова',
        contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_draftConfirm:
          "Сигурни ли сте, че искате да смените статуса на корекцията на верифициран индикатор на 'Чернова'?",

        //contractReportChecks_financialCSDBudgetItemForm
        contractReportChecks_financialCSDBudgetItemForm_csd:
          'Данни за разходооправдателен документ',
        contractReportChecks_financialCSDBudgetItemForm_amount:
          'Сума на разходооправдателния документ за конкретния бюджетен ред и дейност',
        contractReportChecks_financialCSDBudgetItemForm_unapprovedAmount:
          'Неверифицирана сума на разходооправдателния документ за конкретния бюджетен ред и дейност',
        contractReportChecks_financialCSDBudgetItemForm_unapprovedByCorrectionAmount:
          'Неверифицирана сума на разходооправдателен документ по наложена финансова корекция за конкретния бюджетен ред и дейност',
        contractReportChecks_financialCSDBudgetItemForm_approvedAmount:
          'Одобрена сума на разходооправдателния документ за конкретния бюджетен ред и дейност',
        contractReportChecks_financialCSDBudgetItemForm_type: 'Вид',
        contractReportChecks_financialCSDBudgetItemForm_description: 'Описание',
        contractReportChecks_financialCSDBudgetItemForm_number: 'Уникален номер',
        contractReportChecks_financialCSDBudgetItemForm_date: 'Дата',
        contractReportChecks_financialCSDBudgetItemForm_paymentDate:
          'Дата на извършване на плащането',
        contractReportChecks_financialCSDBudgetItemForm_companyType: 'Тип',
        contractReportChecks_financialCSDBudgetItemForm_company: 'Бенефициент/Партньор/Изпълнител',
        contractReportChecks_financialCSDBudgetItemForm_contractContractor:
          'Договор за изпълнение/доставчик',
        contractReportChecks_financialCSDBudgetItemForm_file: 'Файл',

        contractReportChecks_financialCSDBudgetItemForm_noDocuments: 'Няма документи',
        contractReportChecks_financialCSDBudgetItemForm_budgetDetailName: 'Ред от бюджета',
        contractReportChecks_financialCSDBudgetItemForm_contractActivityName: 'Дейност',

        contractReportChecks_financialCSDBudgetItemForm_bfpTotalAmount: 'Общо БФП',

        contractReportChecks_financialCSDBudgetItemForm_isVatAmount: 'Сумата е ДДС',
        contractReportChecks_financialCSDBudgetItemForm_totalAmount: 'Обща сума',

        contractReportChecks_financialCSDBudgetItemForm_costSupportingDocumentApproved:
          'Съгласие по разходооправдателен документ',
        contractReportChecks_financialCSDBudgetItemForm_approve: 'Съгласие',
        contractReportChecks_financialCSDBudgetItemForm_notes: 'Бележки',
        contractReportChecks_financialCSDBudgetItemForm_status: 'Статус',
        contractReportChecks_financialCSDBudgetItemForm_checkedByUser: 'Проверено от',
        contractReportChecks_financialCSDBudgetItemForm_checkedDate: 'Дата на проверка',
        contractReportChecks_financialCSDBudgetItemForm_correctionType: 'Тип на корекцията',
        contractReportChecks_financialCSDBudgetItemForm_irregularityId: 'Нередност',
        contractReportChecks_financialCSDBudgetItemForm_financialCorrectionId: 'Финансова корекция',
        contractReportChecks_financialCSDBudgetItemForm_techCheckedByUser:
          'Технически проверено от',
        contractReportChecks_financialCSDBudgetItemForm_techCheckedDate:
          'Дата на техническа проверка',

        //contractReportChecks_modals_CSDBudgetItemModal
        contractReportChecks_modals_CSDBudgetItemModal_editTitle:
          'Редакция на разходооправдателен документ',
        contractReportChecks_modals_CSDBudgetItemModal_viewTitle:
          'Преглед на разходооправдателен документ',
        contractReportChecks_modals_CSDBudgetItemModal_save: 'Запис',
        contractReportChecks_modals_CSDBudgetItemModal_edit: 'Редакция',
        contractReportChecks_modals_CSDBudgetItemModal_cancel: 'Отказ',
        contractReportChecks_modals_CSDBudgetItemModal_ended: 'Приключен',
        contractReportChecks_modals_CSDBudgetItemModal_endedConfirm:
          "Сигурни ли сте, че искате да смените статуса на разходооправдателния документ на 'Приключен'?",

        //contractReportChecks_editCSDBudgetItem
        contractReportChecks_editCSDBudgetItem_editTitle:
          'Редакция на разходооправдателен документ',
        contractReportChecks_editCSDBudgetItem_viewTitle: 'Преглед на разходооправдателен документ',
        contractReportChecks_editCSDBudgetItem_save: 'Запис',
        contractReportChecks_editCSDBudgetItem_edit: 'Редакция',
        contractReportChecks_editCSDBudgetItem_cancel: 'Отказ',
        contractReportChecks_editCSDBudgetItem_ended: 'Приключен',
        contractReportChecks_editCSDBudgetItem_draft: 'Чернова',
        contractReportChecks_editCSDBudgetItem_endedConfirm:
          "Сигурни ли сте, че искате да смените статуса на разходооправдателния документ на 'Приключен'?",
        contractReportChecks_editCSDBudgetItem_draftConfirm:
          "Сигурни ли сте, че искате да смените статуса на разходооправдателния документ на 'Чернова'?",
        contractReportChecks_editCSDBudgetItem_techCheck: 'Технически проверен',
        contractReportChecks_editCSDBudgetItem_techCheckConfirm:
          'Сигурни ли сте, че искате да проверите разходооправдателния документ технически?',

        //contractReportChecks_attachedDocuments
        contractReportChecks_attachedDocuments_choose: 'Избери',
        contractReportChecks_attachedDocuments_financialCorrections:
          'Свързани документи за коригиране на верифицирани суми на ниво РОД',
        contractReportChecks_attachedDocuments_contractRegNum: 'Рег. номер',
        contractReportChecks_attachedDocuments_contractName: 'Договор',
        contractReportChecks_attachedDocuments_procedureName: 'Бюджет',
        contractReportChecks_attachedDocuments_reportOrderNum: 'Номер на пакет',
        contractReportChecks_attachedDocuments_status: 'Статус',
        contractReportChecks_attachedDocuments_orderNum: 'Пореден номер',
        contractReportChecks_attachedDocuments_createDate: 'Дата на създаване',
        contractReportChecks_attachedDocuments_notes: 'Бележки',

        //contractReportChecks_modals_chooseFinancialCorrectionModal
        contractReportChecks_modals_chooseFinancialCorrectionModal_title:
          'Избор на документ за коригиране на верифицирани суми на ниво РОД',
        contractReportChecks_modals_chooseFinancialCorrectionModal_cancel: 'Отказ',
        contractReportChecks_modals_chooseFinancialCorrectionModal_choose: 'Избери',
        contractReportChecks_modals_chooseFinancialCorrectionModal_contractRegNum: 'Рег. номер',
        contractReportChecks_modals_chooseFinancialCorrectionModal_contractName: 'Договор',
        contractReportChecks_modals_chooseFinancialCorrectionModal_procedureName: 'Бюджет',
        contractReportChecks_modals_chooseFinancialCorrectionModal_reportOrderNum: 'Номер на пакет',
        contractReportChecks_modals_chooseFinancialCorrectionModal_status: 'Статус',
        contractReportChecks_modals_chooseFinancialCorrectionModal_orderNum: 'Пореден номер',
        contractReportChecks_modals_chooseFinancialCorrectionModal_createDate: 'Дата на създаване',
        contractReportChecks_modals_chooseFinancialCorrectionModal_notes: 'Бележки',

        //contractReportChecks_attachedFinancialCorrections
        contractReportChecks_attachedFinancialCorrections_title:
          'Преглед на документ за коригиране на верифицирани суми на ниво РОД',

        contractReportChecks_attachedFinancialCorrections_status: 'Статус',

        contractReportChecks_attachedFinancialCorrections_csd: 'Разходооправдателен документ',
        contractReportChecks_attachedFinancialCorrections_budgetDetail: 'Ред от бюджета',
        contractReportChecks_attachedFinancialCorrections_contractActivity: 'Дейност',
        contractReportChecks_attachedFinancialCorrections_approvedAmount: 'Одобрена сума',
        contractReportChecks_attachedFinancialCorrections_correctedApprovedAmount:
          'Коригирана одобрена сума',
        contractReportChecks_attachedFinancialCorrections_euAmount: 'БФП - ЕС',
        contractReportChecks_attachedFinancialCorrections_bgAmount: 'БФП - НФ',
        contractReportChecks_attachedFinancialCorrections_selfAmount: 'Собствено съфинансиране',
        contractReportChecks_attachedFinancialCorrections_totalAmount: 'Обща сума',

        contractReportChecks_attachedFinancialCorrections_approval: 'Съгласие',
        contractReportChecks_attachedFinancialCorrections_sign: 'Знак',
        contractReportChecks_attachedFinancialCorrections_correctedCsds:
          'Коригирани верифицирани РОД',

        //contractReportChecks_contractReportIndicatorsSearch
        contractReportChecks_contractReportIndicatorsSearch_indicatorName: 'Индикатор',
        contractReportChecks_contractReportIndicatorsSearch_indicatorAmount: 'Отчетени стойности',
        contractReportChecks_contractReportIndicatorsSearch_approvedAmount: 'Одобрени стойности',
        contractReportChecks_contractReportIndicatorsSearch_status: 'Статус',
        contractReportChecks_contractReportIndicatorsSearch_approval: 'Одобрение',
        contractReportChecks_contractReportIndicatorsSearch_periodAmount: 'Ст-ст за периода',
        contractReportChecks_contractReportIndicatorsSearch_cumulativeAmount: 'Ст-ст с натрупване',
        contractReportChecks_contractReportIndicatorsSearch_residueAmount:
          'Остатък спрямо договора',
        contractReportChecks_contractReportIndicatorsSearch_approvedPeriodAmount:
          'Ст-ст за периода',
        contractReportChecks_contractReportIndicatorsSearch_approvedCumulativeAmount:
          'Ст-ст с натрупване',
        contractReportChecks_contractReportIndicatorsSearch_approvedResidueAmount:
          'Остатък спрямо договора',
        contractReportChecks_contractReportIndicatorsSearch_excelExport: 'Експорт',

        //contractReportChecks_editContractReportIndicators
        contractReportChecks_editContractReportIndicators_endedReason:
          "Сигурни ли сте, че искате да промените статуса на верифицирания индикатор на 'Приключен'",
        contractReportChecks_editContractReportIndicators_draftReason:
          "Сигурни ли сте, че искате да промените статуса на верифицирания индикатор на 'Чернова'",
        contractReportChecks_editContractReportIndicators_title:
          'Редакция на верифициран индикатор',
        contractReportChecks_editContractReportIndicators_ended: 'Приключен',
        contractReportChecks_editContractReportIndicators_draft: 'Чернова',
        contractReportChecks_editContractReportIndicators_edit: 'Редакция',
        contractReportChecks_editContractReportIndicators_save: 'Запис',
        contractReportChecks_editContractReportIndicators_cancel: 'Отказ',

        //contractReportChecks_contractReportIndicatorForm
        contractReportChecks_contractReportIndicatorForm_indicator: 'Индикатор',
        contractReportChecks_contractReportIndicatorForm_contractIndicator: 'Индикатор към договор',
        contractReportChecks_contractReportIndicatorForm_contractReportIndicator:
          'Верифициран индикатор',
        contractReportChecks_contractReportIndicatorForm_baseTotalValue: 'Базова ст-ст',
        contractReportChecks_contractReportIndicatorForm_baseMenValue: 'Базова ст-ст (мъже)',
        contractReportChecks_contractReportIndicatorForm_baseWomenValue: 'Базова ст-ст (жени)',
        contractReportChecks_contractReportIndicatorForm_targetTotalValue: 'Целева ст-ст',
        contractReportChecks_contractReportIndicatorForm_targetMenValue: 'Целева ст-ст (мъже)',
        contractReportChecks_contractReportIndicatorForm_targetWomenValue: 'Целева ст-ст (жени)',
        contractReportChecks_contractReportIndicatorForm_description: 'Източник на информация',
        contractReportChecks_contractReportIndicatorForm_status: 'Статус',
        contractReportChecks_contractReportIndicatorForm_approval: 'Одобрение',
        contractReportChecks_contractReportIndicatorForm_notes: 'Бележки',
        contractReportChecks_contractReportIndicatorForm_checkedDate: 'Дата на верифициране',
        contractReportChecks_contractReportIndicatorForm_checkedByUser: 'Проверено от',
        contractReportChecks_contractReportIndicatorForm_periodAmount:
          'Отчетена стойност за периода',
        contractReportChecks_contractReportIndicatorForm_periodAmountMen:
          'Отчетена стойност за периода (мъже)',
        contractReportChecks_contractReportIndicatorForm_periodAmountWomen:
          'Отчетена стойност за периода (жени)',
        contractReportChecks_contractReportIndicatorForm_cumulativeAmount:
          'Отчетена стойност с натрупване',
        contractReportChecks_contractReportIndicatorForm_cumulativeAmountMen:
          'Отчетена стойност с натрупване (мъже)',
        contractReportChecks_contractReportIndicatorForm_cumulativeAmountWomen:
          'Отчетена стойност с натрупване (жени)',
        contractReportChecks_contractReportIndicatorForm_residueAmount:
          'Остатък/отклонение спрямо договора',
        contractReportChecks_contractReportIndicatorForm_residueAmountMen:
          'Остатък/отклонение спрямо договора (мъже)',
        contractReportChecks_contractReportIndicatorForm_residueAmountWomen:
          'Остатък/отклонение спрямо договора (жени)',
        contractReportChecks_contractReportIndicatorForm_comment: 'Коментар',
        contractReportChecks_contractReportIndicatorForm_approvedPeriodAmount:
          'Одобрена стойност за периода',
        contractReportChecks_contractReportIndicatorForm_approvedPeriodAmountMen:
          'Одобрена стойност за периода (мъже)',
        contractReportChecks_contractReportIndicatorForm_approvedPeriodAmountWomen:
          'Одобрена стойност за периода (жени)',
        contractReportChecks_contractReportIndicatorForm_approvedCumulativeAmount:
          'Одобрена стойност с натрупване',
        contractReportChecks_contractReportIndicatorForm_approvedCumulativeAmountMen:
          'Одобрена стойност с натрупване (мъже)',
        contractReportChecks_contractReportIndicatorForm_approvedCumulativeAmountWomen:
          'Одобрена стойност с натрупване (жени)',
        contractReportChecks_contractReportIndicatorForm_approvedResidueAmount:
          'Одобрен остатък спрямо договора',
        contractReportChecks_contractReportIndicatorForm_approvedResidueAmountMen:
          'Одобрен остатък спрямо договора (мъже)',
        contractReportChecks_contractReportIndicatorForm_approvedResidueAmountWomen:
          'Одобрен остатък спрямо договора (жени)',
        contractReportChecks_contractReportIndicatorForm_correction:
          'Коригиране на верифициран индикатор',
        contractReportChecks_contractReportIndicatorForm_correctedApprovedPeriodAmount:
          'Коригирана одобрена стойност за периода',
        contractReportChecks_contractReportIndicatorForm_correctedApprovedPeriodAmountMen:
          'Коригирана одобрена стойност за периода (мъже)',
        contractReportChecks_contractReportIndicatorForm_correctedApprovedPeriodAmountWomen:
          'Коригирана одобрена стойност за периода (жени)',
        contractReportChecks_contractReportIndicatorForm_correctedApprovedCumulativeAmount:
          'Коригирана одобрена стойност с натрупване',
        contractReportChecks_contractReportIndicatorForm_correctedApprovedCumulativeAmountMen:
          'Коригирана одобрена стойност с натрупване (мъже)',
        contractReportChecks_contractReportIndicatorForm_correctedApprovedCumulativeAmountWomen:
          'Коригирана одобрена стойност с натрупване (жени)',
        contractReportChecks_contractReportIndicatorForm_correctedApprovedResidueAmount:
          'Коригиран одобрен остатък спрямо договора',
        contractReportChecks_contractReportIndicatorForm_correctedApprovedResidueAmountMen:
          'Коригиран одобрен остатък спрямо договора (мъже)',
        contractReportChecks_contractReportIndicatorForm_correctedApprovedResidueAmountWomen:
          'Коригиран одобрен остатък спрямо договора (жени)',

        //contractReportChecks_contractReportAdvancePaymentAmountsSearch
        contractReportChecks_contractReportAdvancePaymentAmountsSearch_programmePriorityName:
          'Разпоредител с бюджетни средства',

        contractReportChecks_contractReportAdvancePaymentAmountsSearch_status: 'Статус',
        contractReportChecks_contractReportAdvancePaymentAmountsSearch_approval: 'Одобрение',
        contractReportChecks_contractReportAdvancePaymentAmountsSearch_approvedAmount:
          'Одобрена сума',

        contractReportChecks_contractReportAdvancePaymentAmountsSearch_bfpTotalAmount: 'Общо',

        //contractReportChecks_contractReportAdvancePaymentAmountForm
        contractReportChecks_contractReportAdvancePaymentAmountForm_status: 'Статус',
        contractReportChecks_contractReportAdvancePaymentAmountForm_approval: 'Одобрение',

        contractReportChecks_contractReportAdvancePaymentAmountForm_notes: 'Бележки',

        contractReportChecks_contractReportAdvancePaymentAmountForm_bfpTotalAmount: 'Общо БФП',
        contractReportChecks_contractReportAdvancePaymentAmountForm_checkedDate:
          'Дата на верифициране',
        contractReportChecks_contractReportAdvancePaymentAmountForm_checkedByUser: 'Проверено от',
        contractReportChecks_contractReportAdvancePaymentAmountForm_uncertifiedAmount:
          'Несертифицирана сума',
        contractReportChecks_contractReportAdvancePaymentAmountForm_certifiedAmount:
          'Сертифицирана сума',
        contractReportChecks_contractReportAdvancePaymentAmountForm_certAmount:
          'Сертифициране на авансово плащане',
        contractReportChecks_contractReportAdvancePaymentAmountForm_certStatus:
          'Статус на сертифициране',
        contractReportChecks_contractReportAdvancePaymentAmountForm_certCheckedDate:
          'Дата на проверка',
        contractReportChecks_contractReportAdvancePaymentAmountForm_certCheckedByUser:
          'Проверено от',
        contractReportChecks_contractReportAdvancePaymentAmountForm_programmePriority:
          'Разпоредител с бюджетни средства',

        //contractReportChecks_contractReportAdvancePaymentAmountEdit
        contractReportChecks_contractReportAdvancePaymentAmountEdit_title:
          'Редакция на верифицирано АП',
        contractReportChecks_contractReportAdvancePaymentAmountEdit_save: 'Запис',

        contractReportChecks_contractReportAdvancePaymentAmountEdit_cancel: 'Отказ',
        contractReportChecks_contractReportAdvancePaymentAmountEdit_ended: 'Приключен',
        contractReportChecks_contractReportAdvancePaymentAmountEdit_draft: 'Чернова',
        contractReportChecks_contractReportAdvancePaymentAmountEdit_endedConfirm:
          "Сигурни ли сте, че искате да смените статуса на верифицираното АП на 'Приключен'?",
        contractReportChecks_contractReportAdvancePaymentAmountEdit_draftConfirm:
          "Сигурни ли сте, че искате да смените статуса на верифицираното АП документ на 'Чернова'?",
        contractReportChecks_contractReportAdvancePaymentAmountEdit_reportPayment:
          'Искане за плащане',
        contractReportChecks_contractReportAdvancePaymentAmountEdit_document: 'Документ',
        contractReportChecks_contractReportAdvancePaymentAmountEdit_advancePaymentAmount:
          'Верифицирано АП',

        //contractReportChecks_contractReportAdvancePaymentAmountsSearch
        contractReportChecks_contractReportAdvanceNVPaymentAmountsSearch_programmePriorityName:
          'Разпоредител с бюджетни средства',

        contractReportChecks_contractReportAdvanceNVPaymentAmountsSearch_status: 'Статус',
        contractReportChecks_contractReportAdvanceNVPaymentAmountsSearch_approval: 'Одобрение',

        contractReportChecks_contractReportAdvanceNVPaymentAmountsSearch_bfpTotalAmount: 'Общо',
        contractReportChecks_contractReportAdvanceNVPaymentAmountsSearch_amount: 'Сума',

        //contractReportChecks_contractReportAdvanceNVPaymentAmountForm
        contractReportChecks_contractReportAdvanceNVPaymentAmountForm_status: 'Статус',
        contractReportChecks_contractReportAdvanceNVPaymentAmountForm_approval: 'Одобрение',
        contractReportChecks_contractReportAdvanceNVPaymentAmountForm_notes: 'Бележки',
        contractReportChecks_contractReportAdvanceNVPaymentAmountForm_bfpTotalAmount: 'Общо',
        contractReportChecks_contractReportAdvanceNVPaymentAmountForm_checkedDate: 'Дата',
        contractReportChecks_contractReportAdvanceNVPaymentAmountForm_checkedByUser: 'Проверено от',
        contractReportChecks_contractReportAdvanceNVPaymentAmountForm_programmePriority:
          'Разпоредител с бюджетни средства',

        //contractReportChecks_contractReportAdvanceNVPaymentAmountEdit
        contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_title: 'Редакция на АП',
        contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_save: 'Запис',
        contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_cancel: 'Отказ',
        contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_ended: 'Приключен',
        contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_draft: 'Чернова',
        contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_endedConfirm:
          "Сигурни ли сте, че искате да смените статуса на АП на 'Приключен'?",
        contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_draftConfirm:
          "Сигурни ли сте, че искате да смените статуса на АП документ на 'Чернова'?",
        contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_reportPayment:
          'Искане за плащане',
        contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_document: 'Документ',
        contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_advanceNVPaymentAmount: 'АП',

        //flatFinancialCorrections_flatFinancialCorrectionForm
        flatFinancialCorrections_flatFinancialCorrectionForm_programmeId: 'Основна организация',
        flatFinancialCorrections_flatFinancialCorrectionForm_name: 'Наименование',
        flatFinancialCorrections_flatFinancialCorrectionForm_orderNum: 'Пореден номер',
        flatFinancialCorrections_flatFinancialCorrectionForm_status: 'Статус',
        flatFinancialCorrections_flatFinancialCorrectionForm_type: 'Тип',
        flatFinancialCorrections_flatFinancialCorrectionForm_file: 'Файл',
        flatFinancialCorrections_flatFinancialCorrectionForm_level: 'Ниво',
        flatFinancialCorrections_flatFinancialCorrectionForm_impositionDate: 'Дата на налагане',
        flatFinancialCorrections_flatFinancialCorrectionForm_impositionNumber:
          'Номер на решението за налагане',
        flatFinancialCorrections_flatFinancialCorrectionForm_description: 'Описание',
        flatFinancialCorrections_flatFinancialCorrectionForm_contract: 'Договор за БФП',

        //flatFinancialCorrections_flatFinancialCorrectionInfoForm
        flatFinancialCorrections_flatFinancialCorrectionInfoForm_name: 'Наименование',
        flatFinancialCorrections_flatFinancialCorrectionInfoForm_orderNum: 'Пореден номер',
        flatFinancialCorrections_flatFinancialCorrectionInfoForm_type: 'Тип',
        flatFinancialCorrections_flatFinancialCorrectionInfoForm_level: 'Ниво',
        flatFinancialCorrections_flatFinancialCorrectionInfoForm_impositionDate: 'Дата на налагане',
        flatFinancialCorrections_flatFinancialCorrectionInfoForm_impositionNumber:
          'Номер на решението за налагане',
        flatFinancialCorrections_flatFinancialCorrectionInfoForm_contract: 'Договор за БФП',

        //flatFinancialCorrections_tabs
        flatFinancialCorrections_tabs_edit: 'Основни данни',
        flatFinancialCorrections_tabs_items: 'Обхват',
        flatFinancialCorrections_tabs_programmeItem: 'Обхват',

        //flatFinancialCorrections_searchFlatFinancialCorrection
        flatFinancialCorrections_searchFlatFinancialCorrection_newBtn:
          'Нова финансова корекция за системни пропуски',
        flatFinancialCorrections_searchFlatFinancialCorrection_name: 'Наименование',
        flatFinancialCorrections_searchFlatFinancialCorrection_orderNum: 'Пореден номер',
        flatFinancialCorrections_searchFlatFinancialCorrection_status: 'Статус',
        flatFinancialCorrections_searchFlatFinancialCorrection_type: 'Тип',
        flatFinancialCorrections_searchFlatFinancialCorrection_level: 'Ниво',
        flatFinancialCorrections_searchFlatFinancialCorrection_impositionDate: 'Дата на налагане',
        flatFinancialCorrections_searchFlatFinancialCorrection_impositionNumber:
          'Номер на решението за налагане',

        //flatFinancialCorrections_newFlatFinancialCorrection
        flatFinancialCorrections_newFlatFinancialCorrection_title:
          'Нова финансова корекция за системни пропуски',
        flatFinancialCorrections_newFlatFinancialCorrection_save: 'Запис',
        flatFinancialCorrections_newFlatFinancialCorrection_cancel: 'Отказ',

        //flatFinancialCorrections_editFlatFinancialCorrection
        flatFinancialCorrections_editFlatFinancialCorrection_title:
          'Редакция на финансова корекция за системни пропуски',
        flatFinancialCorrections_editFlatFinancialCorrection_edit: 'Редакция',
        flatFinancialCorrections_editFlatFinancialCorrection_del: 'Изтриване',
        flatFinancialCorrections_editFlatFinancialCorrection_save: 'Запис',
        flatFinancialCorrections_editFlatFinancialCorrection_cancel: 'Отказ',
        flatFinancialCorrections_editFlatFinancialCorrection_actual: 'Актуална',
        flatFinancialCorrections_editFlatFinancialCorrection_draft: 'Чернова',
        flatFinancialCorrections_editFlatFinancialCorrection_actualReason:
          "Сигурни ли сте, че искате да промените статуса на корекцията на 'Актуална'",
        flatFinancialCorrections_editFlatFinancialCorrection_draftReason:
          "Сигурни ли сте, че искате да промените статуса на корекцията на 'Чернова'",

        //flatFinancialCorrections_searchFlatFinancialCorrectionItems
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_choose: 'Избор на елементи',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_percent: 'Процент',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_euAmount: 'БФП - ЕС',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_bgAmount: 'БФП - НФ',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_totalAmount: 'Обща сума',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_code: 'Код',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_name: 'Наименование',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_programmeCompany: 'Организация',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_regulationNumber: 'Решение',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_regulationDate:
          'Дата на решение',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_programme:
          'Основна организация',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_programmePriority:
          'Разпоредител с бюджетни средства',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_listingDate:
          'Дата на обявяване ',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_endingDate:
          'Краен срок за кандидатстване',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_status: 'Статус',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_procedure: 'Бюджет',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_regNumber: 'Номер',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_contractDate:
          'Дата на сключване',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_executionStatus:
          'Статус на изпълнение',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_company: 'Бенефициент',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_companyKidCode:
          'КО по КИД 2008',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_contractContractor:
          'Изпълнител',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_data: 'Данни',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_seat: 'Седалище',
        flatFinancialCorrections_searchFlatFinancialCorrectionItems_contract: 'Договор',

        //flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_title:
          'Редакция на обхват',
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_edit: 'Редация',
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_save: 'Запис',
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_cancel: 'Отказ',
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_calculate: 'Изчисли',
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_itemName: 'Наименование',
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_itemCode: 'Код',
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_percent:
          'Процент на корекцията',
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_euAmount: 'БФП - ЕС',
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_bgAmount: 'БФП - НФ',
        flatFinancialCorrections_editFlatFinancialCorrectionProgrammeItem_totalAmount: 'Обща сума',

        //flatFinancialCorrections_chooseProgrammePriorityItemsModal
        flatFinancialCorrections_chooseProgrammePriorityItemsModal_title:
          'Избор на разпоредител с бюджетни средства',
        flatFinancialCorrections_chooseProgrammePriorityItemsModal_continue: 'Продължи',
        flatFinancialCorrections_chooseProgrammePriorityItemsModal_cancel: 'Отказ',
        flatFinancialCorrections_chooseProgrammePriorityItemsModal_name: 'Наименование',
        flatFinancialCorrections_chooseProgrammePriorityItemsModal_code: 'Код',
        flatFinancialCorrections_chooseProgrammePriorityItemsModal_programme: 'Програма',

        //flatFinancialCorrections_chooseProcedureItemsModal
        flatFinancialCorrections_chooseProcedureItemsModal_title: 'Избор на бюджет',
        flatFinancialCorrections_chooseProcedureItemsModal_continue: 'Продължи',
        flatFinancialCorrections_chooseProcedureItemsModal_cancel: 'Отказ',
        flatFinancialCorrections_chooseProcedureItemsModal_name: 'Наименование',
        flatFinancialCorrections_chooseProcedureItemsModal_code: 'Код',
        flatFinancialCorrections_chooseProcedureItemsModal_programme: 'Основна организация',
        flatFinancialCorrections_chooseProcedureItemsModal_programmePriority:
          'Разпоредител с бюджетни средства',
        flatFinancialCorrections_chooseProcedureItemsModal_listingDate: 'Дата на обявяване ',
        flatFinancialCorrections_chooseProcedureItemsModal_endingDate:
          'Краен срок за кандидатстване',
        flatFinancialCorrections_chooseProcedureItemsModal_status: 'Статус',

        //flatFinancialCorrections_chooseContractItemsModal
        flatFinancialCorrections_chooseContractItemsModal_title: 'Избор на договор',
        flatFinancialCorrections_chooseContractItemsModal_continue: 'Продължи',
        flatFinancialCorrections_chooseContractItemsModal_cancel: 'Отказ',
        flatFinancialCorrections_chooseContractItemsModal_name: 'Наименование',
        flatFinancialCorrections_chooseContractItemsModal_procedure: 'Бюджет',
        flatFinancialCorrections_chooseContractItemsModal_regNumber: 'Номер',
        flatFinancialCorrections_chooseContractItemsModal_contractDate: 'Дата на сключване',
        flatFinancialCorrections_chooseContractItemsModal_executionStatus: 'Статус на изпълнение',
        flatFinancialCorrections_chooseContractItemsModal_company: 'Бенефициент',
        flatFinancialCorrections_chooseContractItemsModal_companyKidCode: 'КО по КИД 2008',

        //flatFinancialCorrections_chooseContractContractItemsModal
        flatFinancialCorrections_chooseContractContractItemsModal_title:
          'Избор на договор с изпълнител',
        flatFinancialCorrections_chooseContractContractItemsModal_continue: 'Продължи',
        flatFinancialCorrections_chooseContractContractItemsModal_cancel: 'Отказ',
        flatFinancialCorrections_chooseContractContractItemsModal_contractContractor: 'Изпълнител',
        flatFinancialCorrections_chooseContractContractItemsModal_data: 'Данни',
        flatFinancialCorrections_chooseContractContractItemsModal_seat: 'Седалище',
        flatFinancialCorrections_chooseContractContractItemsModal_contract: 'Договор',
        flatFinancialCorrections_chooseContractContractItemsModal_name: 'Наименование',
        flatFinancialCorrections_chooseContractContractItemsModal_procedure: 'Бюджет',
        flatFinancialCorrections_chooseContractContractItemsModal_regNumber: 'Номер',
        flatFinancialCorrections_chooseContractContractItemsModal_contractDate: 'Дата на сключване',
        flatFinancialCorrections_chooseContractContractItemsModal_executionStatus:
          'Статус на изпълнение',
        flatFinancialCorrections_chooseContractContractItemsModal_company: 'Бенефициент',
        flatFinancialCorrections_chooseContractContractItemsModal_companyKidCode: 'КО по КИД 2008',

        //flatFinancialCorrections_editFlatFinancialCorrectionItemModal
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_title:
          'Редакция на елемент от обхвата',
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_edit: 'Редация',
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_save: 'Запис',
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_cancel: 'Отказ',
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_calculate: 'Изчисли',
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_itemName: 'Наименование',
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_itemCode:
          'Код/Рег.номер на елемента',
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_percent:
          'Процент на корекцията',
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_euAmount: 'БФП - ЕС',
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_bgAmount: 'БФП - НФ',
        flatFinancialCorrections_editFlatFinancialCorrectionItemModal_totalAmount: 'Обща сума',

        //contractReportChecks_contractReportChecks_paymentRequests
        contractReportChecks_contractReportChecks_paymentRequests_title: 'Искания за плащания',
        contractReportChecks_contractReportChecks_paymentRequests_expense: 'Разход',
        contractReportChecks_contractReportChecks_paymentRequests_total: 'Общо',

        contractReportChecks_contractReportChecks_paymentRequests_bgAmount: 'Обща сума',

        //financialCorrections_tabs
        financialCorrections_tabs_edit: 'Основни данни',
        financialCorrections_tabs_versions: 'Финансова корекция',
        financialCorrections_tabs_attachedDocs: 'Свързани документи',

        //financialCorrections_financialCorrectionForm
        financialCorrections_financialCorrectionForm_orderNum: 'Пореден номер',
        financialCorrections_financialCorrectionForm_impositionDate: 'Дата на налагане',
        financialCorrections_financialCorrectionForm_isDeleted: 'Анулирана',
        financialCorrections_financialCorrectionForm_isDeletedNote: 'Причина за анулиране',
        financialCorrections_financialCorrectionForm_contractContractId: 'Договор с изпълнител',
        financialCorrections_financialCorrectionForm_contractBudgetLevel3AmountId: 'Ред от бюджет',

        //financialCorrections_searchFinancialCorrection
        financialCorrections_searchFinancialCorrection_newBtn: 'Нова финансова корекция',
        financialCorrections_searchFinancialCorrection_orderNum: 'Пореден номер',
        financialCorrections_searchFinancialCorrection_impositionDate: 'Дата на налагане',
        financialCorrections_searchFinancialCorrection_contractRegNumber: 'Рег. номер',
        financialCorrections_searchFinancialCorrection_contractName: 'Наименование',
        financialCorrections_searchFinancialCorrection_contractCompany: 'Бенефициент',
        financialCorrections_searchFinancialCorrection_contractContractNumber: 'Номер',
        financialCorrections_searchFinancialCorrection_contractContractorCompany: 'Изпълнител',
        financialCorrections_searchFinancialCorrection_contractBudgetLevel3Name: 'Ред от бюджет',
        financialCorrections_searchFinancialCorrection_contract: 'Договор',
        financialCorrections_searchFinancialCorrection_contractContract: 'Договор с изпълнител',

        //financialCorrections_financialCorrectionVersionForm
        financialCorrections_financialCorrectionVersionForm_orderNum: 'Пореден номер',
        financialCorrections_financialCorrectionVersionForm_status: 'Статус',

        financialCorrections_financialCorrectionVersionForm_percent: 'Процент',
        financialCorrections_financialCorrectionVersionForm_calculate: 'Изчисли',

        financialCorrections_financialCorrectionVersionForm_totalAmount: 'Обща сума',
        financialCorrections_financialCorrectionVersionForm_violationFoundBy:
          'Орган/институция, установила нарушението',
        financialCorrections_financialCorrectionVersionForm_financialCorrectionImposingReasonId:
          'Основание за налагане',
        financialCorrections_financialCorrectionVersionForm_amendmentReason:
          'Причина за изменението',
        financialCorrections_financialCorrectionVersionForm_correctionBearer:
          'Следва да се понесе от',
        financialCorrections_financialCorrectionVersionForm_file: 'Файл',
        financialCorrections_financialCorrectionVersionForm_description:
          'Описание на фактическата обстановка на посоченото нарушение',
        financialCorrections_financialCorrectionVersionForm_violationIds:
          'Други констатирани нарушения',

        //financialCorrections_editFinancialCorrection
        financialCorrections_editFinancialCorrection_title:
          'Преглед на основни данни на финансова корекция',
        financialCorrections_editFinancialCorrection_edit: 'Редакция',
        financialCorrections_editFinancialCorrection_deactivate: 'Анулирана',
        financialCorrections_editFinancialCorrection_activate: 'Актуална',
        financialCorrections_editFinancialCorrection_del: 'Изтриване',
        financialCorrections_editFinancialCorrection_save: 'Запис',
        financialCorrections_editFinancialCorrection_cancel: 'Отказ',
        financialCorrections_editFinancialCorrection_financialCorrection:
          'Основни данни на финансова корекция',
        financialCorrections_editFinancialCorrection_contractData: 'Договор',
        financialCorrections_editFinancialCorrection_beneficiary: 'Бенефициент',
        financialCorrections_editFinancialCorrection_cancelConfirm:
          'Сигурни ли сте, че искате да анулирате финансовата корекция?',
        financialCorrections_editFinancialCorrection_actualReason:
          "Сигурни ли сте, че искате да промените статуса на корекцията на 'Актуален'",
        financialCorrections_editFinancialCorrection_cancelMessage: 'Причина за анулиране',

        //financialCorrections_modals_chooseContractModal
        financialCorrections_modals_chooseContractModal_title: 'Избор на договор',
        financialCorrections_modals_chooseContractModal_cancel: 'Отказ',
        financialCorrections_modals_chooseContractModal_programme: 'Програма',
        financialCorrections_modals_chooseContractModal_contractNumber: '№ договор',
        financialCorrections_modals_chooseContractModal_search: 'Търси',
        financialCorrections_modals_chooseContractModal_choose: 'Избери',
        financialCorrections_modals_chooseContractModal_procedure: 'Бюджет',
        financialCorrections_modals_chooseContractModal_regNumber: 'Номер',
        financialCorrections_modals_chooseContractModal_contractDate: 'Дата на сключване',
        financialCorrections_modals_chooseContractModal_name: 'Наименование',
        financialCorrections_modals_chooseContractModal_executionStatus: 'Статус на изпълнение',
        financialCorrections_modals_chooseContractModal_company: 'Бенефициент',
        financialCorrections_modals_chooseContractModal_companyKidCode: 'КО по КИД 2008',

        //financialCorrections_newFinancialCorrectionStep1
        financialCorrections_newFinancialCorrectionStep1_title:
          'Създаване на финансова корекция (стъпка 1/2)',
        financialCorrections_newFinancialCorrectionStep1_next: 'Напред',
        financialCorrections_newFinancialCorrectionStep1_cancel: 'Отказ',
        financialCorrections_newFinancialCorrectionStep1_programme: 'Основна организация',
        financialCorrections_newFinancialCorrectionStep1_contractRegNumber: '№ договор',
        financialCorrections_newFinancialCorrectionStep1_contractNumberInvaid:
          'Невалиден номер на договор',
        financialCorrections_newFinancialCorrectionStep1_chooseContract: 'Търси',

        //financialCorrections_newFinancialCorrectionStep2
        financialCorrections_newFinancialCorrectionStep2_title:
          'Създаване на финансова корекция (стъпка 2/2)',
        financialCorrections_newFinancialCorrectionStep2_save: 'Запис',
        financialCorrections_newFinancialCorrectionStep2_cancel: 'Отказ',
        financialCorrections_newFinancialCorrectionStep2_impositionDate: 'Дата на налагане',
        financialCorrections_newFinancialCorrectionStep2_contractContract: 'Договор с изпълнител',

        financialCorrections_newFinancialCorrectionStep2_contractData: 'Договор',
        financialCorrections_newFinancialCorrectionStep2_beneficiary: 'Бенефициент',

        //financialCorrections_searchFlatFinancialCorrectionVersions
        financialCorrections_searchFlatFinancialCorrectionVersions_newAmendment: 'Ново изменение',
        financialCorrections_searchFlatFinancialCorrectionVersions_orderNum: 'Пореден номер',
        financialCorrections_searchFlatFinancialCorrectionVersions_status: 'Статус',
        financialCorrections_searchFlatFinancialCorrectionVersions_amendmentReason:
          'Причина за изменението',
        financialCorrections_searchFlatFinancialCorrectionVersions_correctionBearer:
          'Следва да се понесе от',
        financialCorrections_searchFlatFinancialCorrectionVersions_percent: 'Процент',

        financialCorrections_searchFlatFinancialCorrectionVersions_totalAmount: 'Обща сума',

        //financialCorrections_editFinancialCorrectionVersion
        financialCorrections_editFinancialCorrectionVersion_title: 'Редакция на финансова корекция',
        financialCorrections_editFinancialCorrectionVersion_actual: 'Актуален',
        financialCorrections_editFinancialCorrectionVersion_del: 'Изтриване',
        financialCorrections_editFinancialCorrectionVersion_edit: 'Редакция',
        financialCorrections_editFinancialCorrectionVersion_save: 'Запис',
        financialCorrections_editFinancialCorrectionVersion_cancel: 'Отказ',
        financialCorrections_editFinancialCorrectionVersion_actualReason:
          "Сигурни ли сте, че искате да промените статуса на корекцията на 'Актуален'",

        //financialCorrections_searchFinancialCorrectionAttachedDocs
        financialCorrections_searchFinancialCorrectionAttachedDocs_contractReports:
          'Искания за плащане',
        financialCorrections_searchFinancialCorrectionAttachedDocs_contractRegNum:
          'Номер на договор',

        financialCorrections_searchFinancialCorrectionAttachedDocs_contractName: 'Договор',
        financialCorrections_searchFinancialCorrectionAttachedDocs_procedureName: 'Бюджет',
        financialCorrections_searchFinancialCorrectionAttachedDocs_orderNum: 'Пореден номер',
        financialCorrections_searchFinancialCorrectionAttachedDocs_status: 'Статус',
        financialCorrections_searchFinancialCorrectionAttachedDocs_source: 'Въведен от',
        financialCorrections_searchFinancialCorrectionAttachedDocs_reportType: 'Тип',
        financialCorrections_searchFinancialCorrectionAttachedDocs_regDate: 'Дата на регистрация',
        financialCorrections_searchFinancialCorrectionAttachedDocs_contractReportCorrections:
          'Корекции без искане за плащане',
        financialCorrections_searchFinancialCorrectionAttachedDocs_createDate: 'Дата на създаване',
        financialCorrections_searchFinancialCorrectionAttachedDocs_notes: 'Бележки',
        financialCorrections_searchFinancialCorrectionAttachedDocs_reportOrderNum: 'Номер на пакет',
        financialCorrections_searchFinancialCorrectionAttachedDocs_contractDebts: 'Дългове',
        financialCorrections_searchFinancialCorrectionAttachedDocs_contractRegNumber:
          'Номер на договор',
        financialCorrections_searchFinancialCorrectionAttachedDocs_companyName: 'Бенефициент',
        financialCorrections_searchFinancialCorrectionAttachedDocs_regNumber: 'Номер на дълга',
        financialCorrections_searchFinancialCorrectionAttachedDocs_modifyDate:
          'Дата на последна актуализация',
        financialCorrections_searchFinancialCorrectionAttachedDocs_euAmount: 'Главница ЕС',
        financialCorrections_searchFinancialCorrectionAttachedDocs_bgAmount: 'Главница НФ',
        financialCorrections_searchFinancialCorrectionAttachedDocs_totalAmount: 'Главница Общо',
        financialCorrections_searchFinancialCorrectionAttachedDocs_certReports:
          'Доклади по сертификация',
        financialCorrections_searchFinancialCorrectionAttachedDocs_dateFrom: 'Период от',
        financialCorrections_searchFinancialCorrectionAttachedDocs_dateTo: 'Период до',
        financialCorrections_searchFinancialCorrectionAttachedDocs_programmeName:
          'Основна организация',
        financialCorrections_searchFinancialCorrectionAttachedDocs_type: 'Тип',
        financialCorrections_searchFinancialCorrectionAttachedDocs_irregularities: 'Нередности',
        financialCorrections_searchFinancialCorrectionAttachedDocs_signalNum: '№ сигнал',
        financialCorrections_searchFinancialCorrectionAttachedDocs_programme: 'Програма',
        financialCorrections_searchFinancialCorrectionAttachedDocs_natNumber: 'Национален номер',
        financialCorrections_searchFinancialCorrectionAttachedDocs_company: 'Бенефициент',

        //contractDebts_tabs
        contractDebts_tabs_edit: 'Основни данни',
        contractDebts_tabs_versions: 'Дълг',
        contractDebts_tabs_interests: 'Лихви',

        //contractDebts_contractDebtForm
        contractDebts_contractDebtForm_regNumber: 'Номер на дълга',
        contractDebts_contractDebtForm_regDate: 'Дата на регистрация',
        contractDebts_contractDebtForm_debtStartDate: 'Дата, от която дългът е дължим',
        contractDebts_contractDebtForm_interestStartDate: 'Дата, от която се начислява лихва',
        contractDebts_contractDebtForm_status: 'Актуален статус',
        contractDebts_contractDebtForm_irregularity: 'Нередност',
        contractDebts_contractDebtForm_financialCorrection: 'Финансова корекция',
        contractDebts_contractDebtForm_certReportNum: 'Номер на доклад по сертификация',
        contractDebts_contractDebtForm_comment: 'Коментар',
        contractDebts_contractDebtForm_isDeleted: 'Анулиран',
        contractDebts_contractDebtForm_isDeletedNote: 'Причина за анулиране',
        contractDebts_contractDebtForm_programmePriority: 'Разпоредител с бюджетни средства',
        contractDebts_contractDebtForm_financeSource: 'Фонд',
        contractDebts_contractDebtForm_paymentIds: 'Искане за плащане',

        //contractDebts_report
        contractDebts_report_month: 'Месец',
        contractDebts_report_year: 'Година',
        contractDebts_report_programme: 'Основна организация',
        contractDebts_report_search: 'Търси',
        contractDebts_report_excelExport: 'Експорт',
        contractDebts_report_status: 'Статус',
        contractDebts_report_regNumber: 'Дълг №',
        contractDebts_report_company: 'Бенефициент',
        contractDebts_report_contractNumber: '№ на Договор',
        contractDebts_report_regDate: 'Дата на регистрация',
        contractDebts_report_modifyDate: 'Дата на последна актуализация',
        contractDebts_report_irregularity: 'Нередност №',
        contractDebts_report_financialCorrection: 'Финансова корекция №',
        contractDebts_report_newDebt: 'Новорегистрирани дългове през месеца (главница)',
        contractDebts_report_debt: 'Дължима сума',
        contractDebts_report_reimbursedAmount: 'Възстановена сума през месеца',
        contractDebts_report_reimbursementDate: 'Дата на възстановяване',
        contractDebts_report_deductedAmount: 'Прихваната сума през месеца',
        contractDebts_report_deductionDate: 'Дата на прихващане',
        contractDebts_report_interestAmounts: 'Лихва, натрупана през месеца',
        contractDebts_report_remainingDebt: 'Дължимо към края на месеца',
        contractDebts_report_num1: 'ДС и ДДР №, в който сумата е включена първоначално',
        contractDebts_report_num2: 'ДС и ДДР №, от който сумата е приспадната',
        contractDebts_report_principal: 'Главница',
        contractDebts_report_interest: 'Лихва',
        contractDebts_report_eu: 'EС',
        contractDebts_report_bg: 'НФ',
        contractDebts_report_total: 'Общо',
        contractDebts_report_noResults: 'Няма намерени резултати за избрания месец',

        //contractDebts_searchContractDebt
        contractDebts_searchContractDebt_newBtn: 'Нов дълг към договор',
        contractDebts_searchContractDebt_contractRegNumber: 'Номер на договор',
        contractDebts_searchContractDebt_companyName: 'Бенефициент',
        contractDebts_searchContractDebt_regNumber: 'Номер на дълга',
        contractDebts_searchContractDebt_status: 'Актуален статус',
        contractDebts_searchContractDebt_regDate: 'Дата на регистрация',
        contractDebts_searchContractDebt_modifyDate: 'Дата на последна актуализация',
        contractDebts_searchContractDebt_euAmount: 'Главница ЕС',
        contractDebts_searchContractDebt_bgAmount: 'Главница НФ',
        contractDebts_searchContractDebt_totalAmount: 'Главница Общо',

        //contractDebts_contractDebtVersionForm
        contractDebts_contractDebtVersionForm_orderNum: 'Пореден номер',
        contractDebts_contractDebtVersionForm_status: 'Статус',
        contractDebts_contractDebtVersionForm_executionStatus: 'Статус на изпълнение',
        contractDebts_contractDebtVersionForm_euAmount: 'Главница ЕС',
        contractDebts_contractDebtVersionForm_bgAmount: 'Главница НФ',
        contractDebts_contractDebtVersionForm_totalAmount: 'Главница Общо',
        contractDebts_contractDebtVersionForm_certStatus: 'Сертифициран',
        contractDebts_contractDebtVersionForm_certEuAmount: 'Сертифицирана част ЕС',
        contractDebts_contractDebtVersionForm_certBgAmount: 'Сертифицирана част НФ',
        contractDebts_contractDebtVersionForm_certTotalAmount: 'Сертифицирана част Общо',
        contractDebts_contractDebtVersionForm_createdByUser: 'Създаден от',
        contractDebts_contractDebtVersionForm_createNotes: 'Бележки',
        contractDebts_contractDebtVersionForm_createDate: 'Дата на създаване',
        contractDebts_contractDebtVersionForm_modifyDate: 'Дата на последна промяна',

        //contractDebts_editContractDebt
        contractDebts_editContractDebt_title: 'Преглед на основни данни на дълг към договор',
        contractDebts_editContractDebt_edit: 'Редакция',
        contractDebts_editContractDebt_deactivate: 'Анулиран',
        contractDebts_editContractDebt_del: 'Изтриване',
        contractDebts_editContractDebt_save: 'Запис',
        contractDebts_editContractDebt_cancel: 'Отказ',
        contractDebts_editContractDebt_contractDebt: 'Основни данни на дълг към договор',
        contractDebts_editContractDebt_contractData: 'Договор',
        contractDebts_editContractDebt_beneficiary: 'Бенефициент',
        contractDebts_editContractDebt_cancelConfirm:
          'Сигурни ли сте, че искате да анулирате дълга?',
        contractDebts_editContractDebt_cancelMessage: 'Причина за анулиране',

        //contractDebts_modals_chooseContractModal
        contractDebts_modals_chooseContractModal_title: 'Избор на договор',
        contractDebts_modals_chooseContractModal_cancel: 'Отказ',
        contractDebts_modals_chooseContractModal_programme: 'Програма',
        contractDebts_modals_chooseContractModal_contractNumber: '№ договор',
        contractDebts_modals_chooseContractModal_search: 'Търси',
        contractDebts_modals_chooseContractModal_choose: 'Избери',
        contractDebts_modals_chooseContractModal_procedure: 'Бюджет',
        contractDebts_modals_chooseContractModal_regNumber: 'Номер',
        contractDebts_modals_chooseContractModal_contractDate: 'Дата на сключване',
        contractDebts_modals_chooseContractModal_name: 'Наименование',
        contractDebts_modals_chooseContractModal_executionStatus: 'Статус на изпълнение',
        contractDebts_modals_chooseContractModal_company: 'Бенефициент',
        contractDebts_modals_chooseContractModal_companyKidCode: 'КО по КИД 2008',

        //contractDebts_newContractDebtStep1
        contractDebts_newContractDebtStep1_title: 'Създаване на дълг към договор (стъпка 1/2)',
        contractDebts_newContractDebtStep1_next: 'Напред',
        contractDebts_newContractDebtStep1_cancel: 'Отказ',
        contractDebts_newContractDebtStep1_programme: 'Програма',
        contractDebts_newContractDebtStep1_contractRegNumber: '№ договор',
        contractDebts_newContractDebtStep1_contractNumberInvaid: 'Невалиден номер на договор',
        contractDebts_newContractDebtStep1_chooseContract: 'Търси',

        //contractDebts_newContractDebtStep2
        contractDebts_newContractDebtStep2_title: 'Създаване на дълг към договор (стъпка 2/2)',
        contractDebts_newContractDebtStep2_save: 'Запис',
        contractDebts_newContractDebtStep2_cancel: 'Отказ',
        contractDebts_newContractDebtStep2_regDate: 'Дата на регистрация',
        contractDebts_newContractDebtStep2_debtStartDate: 'Дата, от която дългът е дължим',
        contractDebts_newContractDebtStep2_interestStartDate: 'Дата, от която се начислява лихва',
        contractDebts_newContractDebtStep2_status: 'Статус',
        contractDebts_newContractDebtStep2_contractData: 'Договор',
        contractDebts_newContractDebtStep2_beneficiary: 'Бенефициент',
        contractDebts_newContractDebtStep2_programmePriority: 'Разпоредител с бюджетни средства',

        contractDebts_newContractDebtStep2_paymentIds: 'Искане за плащане',

        //contractDebts_searchContractDebtVersions
        contractDebts_searchContractDebtVersions_newAmendment: 'Ново изменение',
        contractDebts_searchContractDebtVersions_orderNum: 'Пореден номер',
        contractDebts_searchContractDebtVersions_status: 'Статус',
        contractDebts_searchContractDebtVersions_executionStatus: 'Статус на изпълнение',
        contractDebts_searchContractDebtVersions_euAmount: 'Главница ЕС',
        contractDebts_searchContractDebtVersions_bgAmount: 'Главница НФ',
        contractDebts_searchContractDebtVersions_totalAmount: 'Главница Общо',
        contractDebts_searchContractDebtVersions_modifyDate: 'Дата на последна промяна',
        contractDebts_searchContractDebtVersions_createdByUser: 'Направена от',

        //contractDebts_editContractDebtVersion
        contractDebts_editContractDebtVersion_title: 'Редакция на дълг към договор',
        contractDebts_editContractDebtVersion_actual: 'Актуален',
        contractDebts_editContractDebtVersion_del: 'Изтриване',
        contractDebts_editContractDebtVersion_edit: 'Редакция',
        contractDebts_editContractDebtVersion_save: 'Запис',
        contractDebts_editContractDebtVersion_cancel: 'Отказ',
        contractDebts_editContractDebtVersion_actualReason:
          "Сигурни ли сте, че искате да промените статуса на дългa на 'Актуален'",

        //contractDebts_contractDebtInterestForm
        contractDebts_contractDebtInterestForm_orderNum: 'Пореден номер',
        contractDebts_contractDebtInterestForm_interestSchemeId: 'Схема на олихвяване',
        contractDebts_contractDebtInterestForm_dateFrom: 'Дата от',
        contractDebts_contractDebtInterestForm_dateTo: 'Дата до',
        contractDebts_contractDebtInterestForm_euAmount: 'Главница ЕС',
        contractDebts_contractDebtInterestForm_bgAmount: 'Главница НФ',
        contractDebts_contractDebtInterestForm_totalAmount: 'Главница Общо',
        contractDebts_contractDebtInterestForm_euInterestAmount: 'Лихва ЕС',
        contractDebts_contractDebtInterestForm_bgInterestAmount: 'Лихва НФ',
        contractDebts_contractDebtInterestForm_totalInterestAmount: 'Лихва общо',
        contractDebts_contractDebtInterestForm_calcualte: 'Изчисли',

        //contractDebts_searchContractDebtInterests
        contractDebts_searchContractDebtInterests_newInterest: 'Нова лихва',
        contractDebts_searchContractDebtInterests_orderNum: 'Пореден номер',
        contractDebts_searchContractDebtInterests_interestScheme: 'Схема на олихвяване',
        contractDebts_searchContractDebtInterests_dateFrom: 'Дата от',
        contractDebts_searchContractDebtInterests_dateTo: 'Дата до',
        contractDebts_searchContractDebtInterests_euInterestAmount: 'Лихва ЕС',
        contractDebts_searchContractDebtInterests_bgInterestAmount: 'Лихва НФ',
        contractDebts_searchContractDebtInterests_totalInterestAmount: 'Лихва общо',

        //contractDebts_newContractDebtInterests
        contractDebts_newContractDebtInterests_title: 'Нова лихва',
        contractDebts_newContractDebtInterests_save: 'Запис',
        contractDebts_newContractDebtInterests_cancel: 'Отказ',

        //contractDebts_editContractDebtInterests
        contractDebts_editContractDebtInterests_title: 'Редакция на лихва',
        contractDebts_editContractDebtInterests_edit: 'Редакция',
        contractDebts_editContractDebtInterests_del: 'Изтриване',
        contractDebts_editContractDebtInterests_save: 'Запис',
        contractDebts_editContractDebtInterests_cancel: 'Отказ',
        contractDebts_editContractDebtInterests_contractDebt: 'Основни данни на дълг към договор',
        contractDebts_editContractDebtInterests_contractData: 'Договор',
        contractDebts_editContractDebtInterests_beneficiary: 'Бенефициент',
        contractDebts_editContractDebtInterests_cancelConfirm:
          'Сигурни ли сте, че искате да анулирате дълга?',
        contractDebts_editContractDebtInterests_cancelMessage: 'Причина за анулиране',

        //correctionDebts_tabs
        correctionDebts_tabs_edit: 'Основни данни',
        correctionDebts_tabs_versions: 'Дълг',

        //correctionDebts_correctionDebtForm
        correctionDebts_correctionDebtForm_regNumber: 'Номер на дълга по ФКСП',
        correctionDebts_correctionDebtForm_regDate: 'Дата на регистрация',
        correctionDebts_correctionDebtForm_comment: 'Коментар',
        correctionDebts_correctionDebtForm_isDeleted: 'Анулиран',
        correctionDebts_correctionDebtForm_deleteNote: 'Причина за анулиране',

        //correctionDebts_report
        correctionDebts_report_correctionOrderNum: 'Пореден № на ФКСП',
        correctionDebts_report_correctionImpositionDate:
          'Дата на Решение на организация за налагане на ФКСП',
        correctionDebts_report_correctionImpositionNumber: 'Номер на решение за налагане',
        correctionDebts_report_correctionLevel: 'Ниво',
        correctionDebts_report_debt: 'Сума за възстановяване',
        correctionDebts_report_reimbursed: 'Възстановена сума ',
        correctionDebts_report_remaining: 'Остатък за възстановяване',
        correctionDebts_report_deducted: 'Прихваната сума',
        correctionDebts_report_bgAmount: 'НФ',
        correctionDebts_report_euAmount: 'ЕС',
        correctionDebts_report_totalAmount: 'Общо',
        correctionDebts_report_noResults: 'Няма намерени резултати',

        //correctionDebts_searchCorrectionDebt
        correctionDebts_searchCorrectionDebt_newBtn: 'Нов дълг по ФКСП',
        correctionDebts_searchCorrectionDebt_correctionRegNumber: 'Номер на ФКСП',
        correctionDebts_searchCorrectionDebt_companyName: 'Бенефициент',
        correctionDebts_searchCorrectionDebt_regNumber: 'Номер на дълга по ФКСП',

        correctionDebts_searchCorrectionDebt_regDate: 'Дата на регистрация',
        correctionDebts_searchCorrectionDebt_modifyDate: 'Дата на последна актуализация',

        correctionDebts_searchCorrectionDebt_debtTotalAmount: 'Обща дължима сума',

        correctionDebts_searchCorrectionDebt_certTotalAmount: 'Обща сертифицирана сума',

        correctionDebts_searchCorrectionDebt_reimbursedTotalAmount: 'Обща възстановена сума',

        //correctionDebts_correctionDebtVersionForm
        correctionDebts_correctionDebtVersionForm_orderNum: 'Пореден номер',
        correctionDebts_correctionDebtVersionForm_status: 'Статус',
        correctionDebts_correctionDebtVersionForm_debt: 'Дължима сума',
        correctionDebts_correctionDebtVersionForm_cert: 'Сертифицирана сума',
        correctionDebts_correctionDebtVersionForm_reimbursed: 'Възстановена сума',
        correctionDebts_correctionDebtVersionForm_euAmount: 'ЕС',
        correctionDebts_correctionDebtVersionForm_bgAmount: 'НФ',
        correctionDebts_correctionDebtVersionForm_bfpAmount: 'Общо БФП',
        correctionDebts_correctionDebtVersionForm_createNotes: 'Бележки',
        correctionDebts_correctionDebtVersionForm_createdByUser: 'Създаден от',
        correctionDebts_correctionDebtVersionForm_createDate: 'Дата на създаване',
        correctionDebts_correctionDebtVersionForm_modifyDate: 'Дата на последна промяна',

        //correctionDebts_editCorrectionDebt
        correctionDebts_editCorrectionDebt_title: 'Преглед на основни данни на дълг по ФКСП',
        correctionDebts_editCorrectionDebt_edit: 'Редакция',
        correctionDebts_editCorrectionDebt_deactivate: 'Анулиран',
        correctionDebts_editCorrectionDebt_deactivateMsg: 'Причина за анулиране',
        correctionDebts_editCorrectionDebt_deactivateConfirm:
          'Сигурни ли сте, че искате да анулирате дълга?',
        correctionDebts_editCorrectionDebt_del: 'Изтриване',
        correctionDebts_editCorrectionDebt_save: 'Запис',
        correctionDebts_editCorrectionDebt_cancel: 'Отказ',
        correctionDebts_editCorrectionDebt_correctionDebt: 'Основни данни на дълг по ФКСП',
        correctionDebts_editCorrectionDebt_correctionData:
          'Финансова корекция за системни пропуски',

        //correctionDebts_modals_chooseContractModal
        correctionDebts_modals_chooseContractModal_title: 'Избор на договор',
        correctionDebts_modals_chooseContractModal_cancel: 'Отказ',
        correctionDebts_modals_chooseContractModal_programme: 'Програма',
        correctionDebts_modals_chooseContractModal_contractNumber: '№ договор',
        correctionDebts_modals_chooseContractModal_search: 'Търси',
        correctionDebts_modals_chooseContractModal_choose: 'Избери',
        correctionDebts_modals_chooseContractModal_procedure: 'Бюджет',
        correctionDebts_modals_chooseContractModal_regNumber: 'Номер',
        correctionDebts_modals_chooseContractModal_contractDate: 'Дата на сключване',
        correctionDebts_modals_chooseContractModal_name: 'Наименование',
        correctionDebts_modals_chooseContractModal_executionStatus: 'Статус на изпълнение',
        correctionDebts_modals_chooseContractModal_company: 'Бенефициент',
        correctionDebts_modals_chooseContractModal_companyKidCode: 'КО по КИД 2008',

        //correctionDebts_newCorrectionDebtStep1
        correctionDebts_newCorrectionDebtStep1_title: 'Създаване на дълг по ФКСП (стъпка 1/2)',
        correctionDebts_newCorrectionDebtStep1_next: 'Напред',
        correctionDebts_newCorrectionDebtStep1_cancel: 'Отказ',
        correctionDebts_newCorrectionDebtStep1_correction:
          'Финансова корекция за системни пропуски',

        //correctionDebts_newCorrectionDebtStep2
        correctionDebts_newCorrectionDebtStep2_title: 'Създаване на дълг по ФКСП (стъпка 2/2)',
        correctionDebts_newCorrectionDebtStep2_save: 'Запис',
        correctionDebts_newCorrectionDebtStep2_cancel: 'Отказ',
        correctionDebts_newCorrectionDebtStep2_regDate: 'Дата на регистрация',
        correctionDebts_newCorrectionDebtStep2_correctionData:
          'Финансова корекция за системни пропуски',

        //correctionDebts_searchCorrectionDebtVersions
        correctionDebts_searchCorrectionDebtVersions_newAmendment: 'Ново изменение',
        correctionDebts_searchCorrectionDebtVersions_orderNum: 'Пореден номер',
        correctionDebts_searchCorrectionDebtVersions_status: 'Статус',
        correctionDebts_searchCorrectionDebtVersions_debtEuAmount: 'Дължима сума ЕС',
        correctionDebts_searchCorrectionDebtVersions_debtBgAmount: 'Дължима сума НФ',
        correctionDebts_searchCorrectionDebtVersions_certEuAmount: 'Сертифицирана сума ЕС',
        correctionDebts_searchCorrectionDebtVersions_certBgAmount: 'Сертифицирана сума НФ',
        correctionDebts_searchCorrectionDebtVersions_reimbursedEuAmount: 'Възстановена сума ЕС',
        correctionDebts_searchCorrectionDebtVersions_reimbursedBgAmount: 'Възстановена сума НФ',
        correctionDebts_searchCorrectionDebtVersions_modifyDate: 'Дата на последна промяна',
        correctionDebts_searchCorrectionDebtVersions_createdByUser: 'Направена от',

        //correctionDebts_editCorrectionDebtVersion
        correctionDebts_editCorrectionDebtVersion_title: 'Редакция на дълг по ФКСП',
        correctionDebts_editCorrectionDebtVersion_actual: 'Актуален',
        correctionDebts_editCorrectionDebtVersion_del: 'Изтриване',
        correctionDebts_editCorrectionDebtVersion_edit: 'Редакция',
        correctionDebts_editCorrectionDebtVersion_save: 'Запис',
        correctionDebts_editCorrectionDebtVersion_cancel: 'Отказ',
        correctionDebts_editCorrectionDebtVersion_actualReason:
          "Сигурни ли сте, че искате да промените статуса на дългa на 'Актуален'",

        //actuallyPaidAmounts_modals_chooseContractModal
        actuallyPaidAmounts_modals_chooseContractModal_title: 'Избор на договор',
        actuallyPaidAmounts_modals_chooseContractModal_cancel: 'Отказ',
        actuallyPaidAmounts_modals_chooseContractModal_programme: 'Програма',
        actuallyPaidAmounts_modals_chooseContractModal_contractNumber: '№ договор',
        actuallyPaidAmounts_modals_chooseContractModal_search: 'Търси',
        actuallyPaidAmounts_modals_chooseContractModal_choose: 'Избери',
        actuallyPaidAmounts_modals_chooseContractModal_procedure: 'Бюджет',
        actuallyPaidAmounts_modals_chooseContractModal_regNumber: 'Номер',
        actuallyPaidAmounts_modals_chooseContractModal_contractDate: 'Дата на сключване',
        actuallyPaidAmounts_modals_chooseContractModal_name: 'Наименование',
        actuallyPaidAmounts_modals_chooseContractModal_executionStatus: 'Статус на изпълнение',
        actuallyPaidAmounts_modals_chooseContractModal_company: 'Бенефициент',
        actuallyPaidAmounts_modals_chooseContractModal_companyKidCode: 'КО по КИД 2008',

        //actuallyPaidAmounts_modals_chooseContractReportPaymentModal
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_title:
          'Избор на искане за плащане',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_cancel: 'Отказ',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_choose: 'Избери',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_versionNum: 'Номер',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_versionSubNum: 'Версия',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_statusName: 'Статус',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_paymentTypeName: 'Тип',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_createDate: 'Дата на създаване',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_regDate: 'Дата на регистрация',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_dateFrom: 'Начална дата',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_dateTo: 'Крайна дата',
        actuallyPaidAmounts_modals_chooseContractReportPaymentModal_requestedAmount:
          'Стойност на исканите средства',

        //actuallyPaidAmounts_paidAmountsSearch
        actuallyPaidAmounts_paidAmountsSearch_contract: 'Договор',
        actuallyPaidAmounts_paidAmountsSearch_paymentReason: 'Основание за плащане',
        actuallyPaidAmounts_paidAmountsSearch_search: 'Търси',
        actuallyPaidAmounts_paidAmountsSearch_new: 'Нова сума',
        actuallyPaidAmounts_paidAmountsSearch_programme: 'Основна организация',
        actuallyPaidAmounts_paidAmountsSearch_contractRegNumber: '№ договор',
        actuallyPaidAmounts_paidAmountsSearch_regNumber: 'Номер',
        actuallyPaidAmounts_paidAmountsSearch_status: 'Статус',
        actuallyPaidAmounts_paidAmountsSearch_paymentDate: 'Дата на плащане',
        actuallyPaidAmounts_paidAmountsSearch_paidBfpTotalAmount: 'Сума',
        actuallyPaidAmounts_paidAmountsSearch_contractReportPaymentNum: 'Номер на ИП',
        actuallyPaidAmounts_paidAmountsSearch_financeSource: 'Фонд',
        actuallyPaidAmounts_paidAmountsSearch_excelExport: 'Експорт',
        actuallyPaidAmounts_paidAmountsSearch_paymentType: 'Тип на ИП',

        //actuallyPaidAmounts_newStep1
        actuallyPaidAmounts_newStep1_title: 'Създаване на реално изплатени суми (стъпка 1/2)',
        actuallyPaidAmounts_newStep1_next: 'Напред',
        actuallyPaidAmounts_newStep1_cancel: 'Отказ',
        actuallyPaidAmounts_newStep1_programme: 'Основна организация',
        actuallyPaidAmounts_newStep1_contractRegNumber: '№ договор',
        actuallyPaidAmounts_newStep1_contractNumberInvaid: 'Невалиден номер на договор',
        actuallyPaidAmounts_newStep1_chooseContract: 'Търси',

        //actuallyPaidAmounts_newStep2
        actuallyPaidAmounts_newStep2_title: 'Създаване на реално изплатени суми (стъпка 2/2)',
        actuallyPaidAmounts_newStep2_save: 'Запис',
        actuallyPaidAmounts_newStep2_cancel: 'Отказ',
        actuallyPaidAmounts_newStep2_programmePriority: 'Разпоредител с бюджетни средства',
        actuallyPaidAmounts_newStep2_financeSource: 'Фонд',
        actuallyPaidAmounts_newStep2_contractReportPayment: 'Искане за плащане',
        actuallyPaidAmounts_newStep2_paymentReason: 'Основание за плащане',
        actuallyPaidAmounts_newStep2_contractData: 'Договор',
        actuallyPaidAmounts_newStep2_beneficiary: 'Бенефициент',

        //actuallyPaidAmounts_viewPaidAmount
        actuallyPaidAmounts_viewPaidAmount_statusInfo: 'Статус: {{status}}',
        actuallyPaidAmounts_viewPaidAmount_contractInfo: 'Договор: {{contractNum}}',

        //actuallyPaidAmounts_tabs
        actuallyPaidAmounts_tabs_view: 'Основни данни',
        actuallyPaidAmounts_tabs_paidAmount: 'Изплатена сума',
        actuallyPaidAmounts_tabs_documents: 'Документи',

        //actuallyPaidAmounts_basicDataForm
        actuallyPaidAmounts_basicDataForm_status: 'Статус',
        actuallyPaidAmounts_basicDataForm_regNumber: 'Номер',
        actuallyPaidAmounts_basicDataForm_creationType: 'Начин на въвеждане',
        actuallyPaidAmounts_basicDataForm_isDeletedNote: 'Причина за анулиране',
        actuallyPaidAmounts_basicDataForm_programme: 'Основна организация',
        actuallyPaidAmounts_basicDataForm_contract: 'Договор',
        actuallyPaidAmounts_basicDataForm_contractName: 'Наименование',
        actuallyPaidAmounts_basicDataForm_contractRegNumber: 'Рег. номер',
        actuallyPaidAmounts_basicDataForm_beneficiary: 'Бенефициент',
        actuallyPaidAmounts_basicDataForm_uinType: 'Булстат/ЕИК/ЕГН',
        actuallyPaidAmounts_basicDataForm_uin: 'Номер',
        actuallyPaidAmounts_basicDataForm_name: 'Наименование',
        actuallyPaidAmounts_basicDataForm_contractReportPayment: 'Искане за плащане',
        actuallyPaidAmounts_basicDataForm_paymentVersionNum: 'Номер',
        actuallyPaidAmounts_basicDataForm_requestedAmount: 'Поискана сума',
        actuallyPaidAmounts_basicDataForm_paidBfpTotalAmount: 'Сума за плащане',
        actuallyPaidAmounts_basicDataForm_checkedDate: 'Дата на верификация',

        //actuallyPaidAmounts_basicData
        actuallyPaidAmounts_basicData_title: 'Преглед на основни данни',
        actuallyPaidAmounts_basicData_del: 'Изтриване',
        actuallyPaidAmounts_basicData_draft: 'Чернова',
        actuallyPaidAmounts_basicData_draftConfirm:
          'Сигурни ли сте че искате да върнете записа в чернова?',
        actuallyPaidAmounts_basicData_enter: 'Въведена',
        actuallyPaidAmounts_basicData_enterConfirm: 'Сигурни ли сте че искате да въведете записа?',
        actuallyPaidAmounts_basicData_remove: 'Анулиране',
        actuallyPaidAmounts_basicData_removeNote: 'Причина за анулиране',
        actuallyPaidAmounts_basicData_removeConfirm:
          'Сигурни ли сте че искате да анулирате записа?',
        actuallyPaidAmounts_basicData_assignContractReportPayment: 'Асоцииране на ИП',
        actuallyPaidAmounts_basicData_dissociateContractReportPayment: 'Премахване на ИП',
        actuallyPaidAmounts_basicData_changeContractReportPayment: 'Смяна на ИП',
        actuallyPaidAmounts_basicData_dissociateContractReportPaymentConfirm:
          'Сигурни ли сте че искате да премахнете връзката с ИП?',

        //actuallyPaidAmounts_paidAmountForm
        actuallyPaidAmounts_paidAmountForm_programmePriority: 'Разпоредител с бюджетни средства',
        actuallyPaidAmounts_paidAmountForm_financeSource: 'Фонд',
        actuallyPaidAmounts_paidAmountForm_paymentReason: 'Основание за плащане',
        actuallyPaidAmounts_paidAmountForm_paymentDate: 'Дата на плащане',
        actuallyPaidAmounts_paidAmountForm_paidBfp: 'Изплатена сума',
        actuallyPaidAmounts_paidAmountForm_paidBfpEuAmount: 'Финансиране от ЕС',
        actuallyPaidAmounts_paidAmountForm_paidBfpBgAmount: 'Финансиране от НФ',
        actuallyPaidAmounts_paidAmountForm_paidBfpCrossAmount: 'Кръстосано съфинансиране',
        actuallyPaidAmounts_paidAmountForm_paidBfpTotalAmount: 'Общо БФП',
        actuallyPaidAmounts_paidAmountForm_paidSelfAmount: 'Собствено финансиране',
        actuallyPaidAmounts_paidAmountForm_paidTotalAmount: 'Общо',
        actuallyPaidAmounts_paidAmountForm_comment: 'Коментар',
        actuallyPaidAmounts_paidAmountForm_contractReportPayment: 'Искане за плащане',
        actuallyPaidAmounts_paidAmountForm_paymentVersionNum: 'Номер',
        actuallyPaidAmounts_paidAmountForm_requestedAmount: 'Поискана сума',
        actuallyPaidAmounts_paidAmountForm_paymentPaidBfpTotalAmount: 'Сума за плащане',
        actuallyPaidAmounts_paidAmountForm_checkedDate: 'Дата на верификация',

        //actuallyPaidAmounts_editPaidAmount
        actuallyPaidAmounts_editPaidAmount_title: 'Преглед на реално изплатена сума',
        actuallyPaidAmounts_editPaidAmount_edit: 'Редакция',
        actuallyPaidAmounts_editPaidAmount_save: 'Запис',
        actuallyPaidAmounts_editPaidAmount_cancel: 'Отказ',

        //actuallyPaidAmounts_documentsSearch
        actuallyPaidAmounts_documentsSearch_newBtn: 'Нов документ',
        actuallyPaidAmounts_documentsSearch_name: 'Наименование',
        actuallyPaidAmounts_documentsSearch_description: 'Описание',
        actuallyPaidAmounts_documentsSearch_file: 'Файл',

        //actuallyPaidAmounts_newPaidAmountDocument
        actuallyPaidAmounts_newPaidAmountDocument_title: 'Нов документ',
        actuallyPaidAmounts_newPaidAmountDocument_save: 'Запис',
        actuallyPaidAmounts_newPaidAmountDocument_cancel: 'Отказ',

        //actuallyPaidAmounts_editPaidAmountDocument
        actuallyPaidAmounts_editPaidAmountDocument_title: 'Редакция на документ',
        actuallyPaidAmounts_editPaidAmountDocument_edit: 'Редакция',
        actuallyPaidAmounts_editPaidAmountDocument_deleteDocument: 'Изтриване',
        actuallyPaidAmounts_editPaidAmountDocument_save: 'Запис',
        actuallyPaidAmounts_editPaidAmountDocument_cancel: 'Отказ',

        //annualAccountReports_annualAccountForm

        //annualAccountReport_tabs

        //annualAccountReports_viewAnnualAccountReport

        //annualAccountReports_editAnnualAccountReport

        //annualAccountReports_newAnnualAccountReport

        //annualAccountReports_searchAnnualAccountReports

        //annualAccountReports_searchAnnualAccountReportCertificationDocuments

        //annualAccountReports_searchAnnualAccountReportsAuditDocuments

        //annualAccountReports_newAnnualAccountReportCertifiedDocument

        //annualAccountReports_newAnnualAccountReportAuditDocument

        //annualAccountReports_editAnnualAccountReportCertificationDocument

        //annualAccountReports_editAnnualAccountReportAuditDocument

        //annualAccountReports_annualAccountReportAttachedCertReports

        //annualAccountReports_chooseAARCertReportsModal

        //annualAccountReports_searchAnnualAccountReportFinancialCorrections

        //annualAccountReports_chooseAARFCContractReportFinancialCorrectionsModal

        //annualAccountReports_annualAccountReportFinancialCorrectionsEdit

        //annualAccountReports_chooseAARFCContractReportFinancialCorrectionCSDsModal

        //annualAccountReports_annualAccountReportFinancialCorrectionsEditCsdEdit

        //annualAccountReports_searchAnnualAccountReportCertFinancialCorrections

        //annualAccountReports_chooseAARFCContractReportCertFinancialCorrectionsModal

        //annualAccountReports_annualAccountReportCertFinancialCorrectionsEdit

        //annualAccountReports_chooseAARFCContractReportCertFinancialCorrectionCSDsModal

        //annualAccountReports_annualAccountReportCertFinancialCorrectionsEditCsdEdit

        //annualAccountReports_searchAnnualAccountReportCorrections

        //annualAccountReports_chooseAARCContractReportCorrectionsModal

        //annualAccountReports_annualAccountReportCorrectionsEdit

        //annualAccountReports_searchAnnualAccountReportCertCorrections

        //annualAccountReports_annualAccountReportCertRevalidationCorrections

        //annualAccountReports_annualAccountReportCertRevalidationCorrectionsEdit

        //annualAccountReports_annualAccountReportCertRevalidationFinancialCorrections

        //annualAccountReports_chooseAARContractReportCertRevalidationFinancialCorrectionsModal

        //annualAccountReports_annualAccountReportCertRevalidationFinancialCorrectionsEdit

        //annualAccountReports_chooseAARContractReportCertRevalidationFinancialCorrectionCSDsModal

        //annualAccountReports_annualAccountReportCertFinancialCorrectionsEditCsdEdit

        //annualAccountReports_chooseAARCContractReportCertCorrectionsModal

        //annualAccountReports_annualAccountReportCertCorrectionsEdit

        //annualAccountReports_annualAccountReportAppendixForm

        //annualAccountReports_searchAnnualAccountReportsAppendices5

        //annualAccountReports_editAnnualAccountReportAppendix5

        //annualAccountReports_searchAnnualAccountReportsAppendices8

        //annualAccountReports_editAnnualAccountReportAppendix8

        //annualAccountReports_annualAccountReportAttachedCertReportEdit

        //certReports_certReportForm

        //certReports_tabs

        //certReports_viewCertReport

        //certReports_editCertReport

        //certReports_newCertReport

        //certReports_searchCertReports

        //certReports_searchCertReportPayments

        //certReports_chooseCRPContractReportsModal

        //certReports_chooseCRPContractReportCSDsModal

        //certReports_certReportAttachedCertReports

        //certReports_certReportContractDebts

        //certReports_certReportDebtReimbursedAmount

        //certReports_editCertReportDebtReimbursedAmount

        //certReports_searchCertReportAdvancePayments

        //certReports_contractReportAdvancePaymentAmountsSearch

        //certReports_chooseCRAPContractReportsModal

        //certReports_chooseCRAPContractReportAmountsModal

        //certReports_chooseCRCertReportsModal

        //certReports_chooseCRContractDebtsModal

        //certReports_chooseCRDebtReimbursedAmountsModal

        //certReports_searchCertReportFinancialCorrections

        //certReports_searchCertReportCorrections

        //certReports_searchCertReportFinancialRevalidations
        certReports_certReportFinancialRevalidationsEdit_from: 'от',

        //certReports_searchCertReportRevalidations

        //certReports_searchCertReportFinancialCertCorrections

        //certReports_searchCertReportCertCorrections

        //certReports_certReportAdvancePaymentsEdit

        //certReports_certReportFinancialCorrectionsEdit

        //certReports_certReportFinancialRevalidationsEdit

        //certReports_certReportFinancialCertCorrectionsEdit

        //certReports_certReportCorrectionsEdit

        //certReports_certReportRevalidationsEdit

        //certReports_certReportCertCorrectionsEdit

        //certReports_certReportPaymentsEdit

        //certReports_certReportPaymentsEditCsdEdit

        //certReports_certReportAdvancePaymentsEditAmountEdit

        //certReports_certReportFinancialCorrectionsEditCsdEdit

        //certReports_certReportFinancialRevalidationsEditCsdEdit

        //certReports_certReportFinancialCertCorrectionsEditCsdEdit

        //certReports_chooseCRFCContractReportFinancialCorrectionsModal

        //certReports_chooseCRFCContractReportFinancialCorrectionCSDsModal

        //certReports_chooseCRFRContractReportFinancialRevalidationsModal

        //certReports_chooseCRFRContractReportFinancialRevalidationCSDsModal

        //certReports_chooseCRFCCContractReportFinancialCertCorrectionsModal

        //certReports_chooseCRFCCContractReportFinancialCertCorrectionCSDsModal

        //certReports_chooseCRCContractReportCorrectionsModal

        //certReports_chooseCRRContractReportRevalidationsModal

        //certReports_chooseCRCCContractReportCertCorrectionsModal

        //certReports_searchCertReportDocuments

        //certReports_newCertReportDocument

        //certReports_editCertReportDocument

        //certReports_searchCertReportCertificationDocuments

        //certReports_editCertReportCertificationDocument

        //certReports_certReportSummary

        //certReports_certReportSummary_nonYearlyEligibleProgrammePriorityExpenses

        //certReports_certReportSummary_approvedAmountsCorrection

        //certReports_certReportSummary_reaffirmedCostsByAdministrativeAuthority

        //certReports_certReportSummary_programmePaidContributionInfoForFinancialInstruments

        //certReports_certReportSummary_nonYearlyStateAidPaidAdvancePayments

        //certReports_certReportSummary_yearlyEligibleProgrammePriorityExpenses

        //certReports_certReportSummary_offAndRecoveredAmounts

        //certReports_certReportSummary_recoverableAmounts

        //certReports_certReportSummary_recoveredAmounts

        //certReports_certReportSummary_unrecoveredAmounts

        //certReports_certReportSummary_programmePaidContributionAmountForFinancialInstruments

        //certReports_certReportSummary_yearlyStateAidPaidAdvancePayments

        //certReports_certReportSummary_reconciliation

        //certReports_certReportSummary_appendix4A

        //compensationDocuments_searchCompDocs
        compensationDocuments_searchCompDocs_search: 'Търси',
        compensationDocuments_searchCompDocs_new: 'Нов изравнителен документ',
        compensationDocuments_searchCompDocs_programme: 'Програма',
        compensationDocuments_searchCompDocs_regNumber: 'Номер',
        compensationDocuments_searchCompDocs_status: 'Статус',
        compensationDocuments_searchCompDocs_type: 'Вид',
        compensationDocuments_searchCompDocs_compensationDocDate: 'Дата',

        //compensationDocuments_new
        compensationDocuments_new_title: 'Създаване на изравнителен документ',
        compensationDocuments_new_save: 'Запис',
        compensationDocuments_new_cancel: 'Отказ',
        compensationDocuments_new_type: 'Вид',
        compensationDocuments_new_sign: 'Знак',
        compensationDocuments_new_compensationDocDate: 'Дата',
        compensationDocuments_new_contract: 'Договор',
        compensationDocuments_new_programmePriority: 'Разпоредител с бюджетни средства',

        compensationDocuments_new_contractReportPayment: 'Искане за плащане',

        //compensationDocuments_viewCompDoc
        compensationDocuments_viewCompDoc_statusInfo: 'Програма: {{programme}}, Статус: {{status}}',
        compensationDocuments_viewCompDoc_viewTab: 'Основни данни',
        compensationDocuments_viewCompDoc_compDocTab: 'Изравнителен документ',
        compensationDocuments_viewCompDoc_docsTab: 'Документи',

        //compensationDocuments_basicDataForm
        compensationDocuments_basicDataForm_type: 'Вид',
        compensationDocuments_basicDataForm_status: 'Статус',
        compensationDocuments_basicDataForm_regNumber: 'Номер',
        compensationDocuments_basicDataForm_deleteNote: 'Причина за анулиране',
        compensationDocuments_basicDataForm_programme: 'Програма',
        compensationDocuments_basicDataForm_procedure: 'Бюджет',
        compensationDocuments_basicDataForm_programmePriority: 'Разпоредител с бюджетни средства',

        compensationDocuments_basicDataForm_contract: 'Договор',
        compensationDocuments_basicDataForm_contractName: 'Наименование',
        compensationDocuments_basicDataForm_contractRegNumber: 'Рег. номер',
        compensationDocuments_basicDataForm_beneficiary: 'Бенефициент',
        compensationDocuments_basicDataForm_uinType: 'Булстат/ЕИК/ЕГН',
        compensationDocuments_basicDataForm_uin: 'Номер',
        compensationDocuments_basicDataForm_name: 'Наименование',
        compensationDocuments_basicDataForm_contractReportPayment: 'Искане за плащане',
        compensationDocuments_basicDataForm_paymentVersionNum: 'Номер',
        compensationDocuments_basicDataForm_requestedAmount: 'Поискана сума БФП',
        compensationDocuments_basicDataForm_paidBfpTotalAmount: 'Сума за плащане БФП',
        compensationDocuments_basicDataForm_checkedDate: 'Дата на верификация',

        //compensationDocuments_editCompDocument
        compensationDocuments_editCompDocument_title: 'Преглед на изравнителен документ',
        compensationDocuments_editCompDocument_edit: 'Редакция',
        compensationDocuments_editCompDocument_save: 'Запис',
        compensationDocuments_editCompDocument_cancel: 'Отказ',

        //compensationDocuments_compDocumentForm
        compensationDocuments_compDocumentForm_sign: 'Знак',
        compensationDocuments_compDocumentForm_date: 'Дата',
        compensationDocuments_compDocumentForm_description: 'Описание',
        compensationDocuments_compDocumentForm_compensationReason: 'Основание',

        compensationDocuments_compDocumentForm_actuallyPaidBfp: 'Изплатена сума БФП',
        compensationDocuments_compDocumentForm_bfpEuAmount: 'Финансиране от ЕС',
        compensationDocuments_compDocumentForm_bfpBgAmount: 'Финансиране от НФ',
        compensationDocuments_compDocumentForm_bfpCrossAmount: 'Кръстосано съфинансиране',
        compensationDocuments_compDocumentForm_bfpTotalAmount: 'Общо БФП',
        compensationDocuments_compDocumentForm_selfAmount: 'Собствено съфинансиране',
        compensationDocuments_compDocumentForm_totalAmount: 'Общо',

        //compensationDocuments_basicData
        compensationDocuments_basicData_title: 'Преглед на основни данни',
        compensationDocuments_basicData_del: 'Изтриване',
        compensationDocuments_basicData_draft: 'Чернова',
        compensationDocuments_basicData_draftConfirm:
          'Сигурни ли сте че искате да върнете записа в чернова?',
        compensationDocuments_basicData_enter: 'Въведена',
        compensationDocuments_basicData_enterConfirm:
          'Сигурни ли сте че искате да въведете записа?',
        compensationDocuments_basicData_remove: 'Анулиране',
        compensationDocuments_basicData_removeNote: 'Причина за анулиране',
        compensationDocuments_basicData_removeConfirm:
          'Сигурни ли сте че искате да анулирате записа?',

        //compensationDocuments_documentForm
        compensationDocuments_documentForm_description: 'Описание',
        compensationDocuments_documentForm_file: 'Файл',

        //compensationDocuments_docsSearch
        compensationDocuments_docsSearch_docs: 'Документи към изравнителен документ',
        compensationDocuments_docsSearch_description: 'Описание',
        compensationDocuments_docsSearch_file: 'Файл',

        //compensationDocuments_newDoc
        compensationDocuments_newDoc_title: 'Нов документ към изравнителен документ',
        compensationDocuments_newDoc_save: 'Запис',
        compensationDocuments_newDoc_cancel: 'Отказ',

        //compensationDocuments_docEdit
        compensationDocuments_docEdit_title: 'Преглед на документ към изравнителен документ',
        compensationDocuments_docEdit_edit: 'Редакция',
        compensationDocuments_docEdit_del: 'Изтриване',
        compensationDocuments_docEdit_save: 'Запис',
        compensationDocuments_docEdit_cancel: 'Отказ',

        //certReportChecks_searchCertReports

        //certReportChecks_viewCertReport

        //certReportChecks_tabs

        //certReportChecks_editCertReport

        //certReportChecks_certReportAttachedCertReports

        //certReportChecks_certReportContractDebts

        //certReportChecks_editCertReportContractDebts

        //certReportChecks_searchCertReportPayments

        //certReportChecks_searchCertReportAdvancePayments

        //certReportChecks_searchCertReportFinancialCorrections

        //certReportChecks_certReportFinancialCorrectionsEdit

        //certReportChecks_searchCertReportCorrections

        //certReportChecks_searchCertReportFinancialRevalidations

        //certReportChecks_searchCertReportRevalidations

        //certReportChecks_searchCertReportFinancialCertCorrections

        //certReportChecks_searchCertReportCertCorrections

        //certReportChecks_certReportAdvancePaymentsEdit

        //certReportChecks_certReportPaymentsEdit

        //certReportChecks_certReportPaymentsEditCsdEdit

        //certReportChecks_certReportAdvancePaymentsEditAmountEdit

        //certReportChecks_certReportFinancialCorrectionsEditCsdEdit

        //certReportChecks_searchCertReportDocuments

        //certReportChecks_editCertReportDocument

        //certReportChecks_searchCertReportCertificationDocuments

        //certReportChecks_newCertReportCertificationDocument

        //certReportChecks_editCertReportCertificationDocument

        //certReportChecks_certReportFinancialRevalidationsEdit

        //certReportChecks_certReportFinancialCertCorrectionsEdit

        //certReportChecks_certReportCorrectionsEdit

        //certReportChecks_certReportRevalidationsEdit

        //certReportChecks_certReportCertCorrectionsEdit

        //certReportChecks_certReportFinancialRevalidationsEditCsdEdit

        //certReportChecks_certReportFinancialCertCorrectionsEditCsdEdit

        //reimbursedAmounts_amountsSearch
        reimbursedAmounts_amountsSearch_contract: 'Договор',
        reimbursedAmounts_amountsSearch_type: 'Вид',
        reimbursedAmounts_amountsSearch_search: 'Търси',
        reimbursedAmounts_amountsSearch_new: 'Нова сума',
        reimbursedAmounts_amountsSearch_excelExport: 'Експорт',
        reimbursedAmounts_amountsSearch_programme: 'Програма',
        reimbursedAmounts_amountsSearch_debtRegNumber: '№ дълг',
        reimbursedAmounts_amountsSearch_regNumber: 'Номер',
        reimbursedAmounts_amountsSearch_status: 'Статус',
        reimbursedAmounts_amountsSearch_reimbursement: 'Начин на възстановяване',
        reimbursedAmounts_amountsSearch_reimbursementDate: 'Дата на плащане',
        reimbursedAmounts_amountsSearch_principalEuAmount: 'Главница-Финансиране от ЕС',
        reimbursedAmounts_amountsSearch_principalBgAmount: 'Главница-Финансиране от НФ',
        reimbursedAmounts_amountsSearch_principalTotalAmount: 'Главница-Общо',
        reimbursedAmounts_amountsSearch_interestEuAmount: 'Лихва-Финансиране от ЕС',
        reimbursedAmounts_amountsSearch_interestBgAmount: 'Лихва-Финансиране от НФ',
        reimbursedAmounts_amountsSearch_interestTotalAmount: 'Лихва-Общо',

        //reimbursedAmounts_new
        reimbursedAmounts_new_title: 'Създаване на възстановена сума',
        reimbursedAmounts_new_save: 'Запис',
        reimbursedAmounts_new_cancel: 'Отказ',
        reimbursedAmounts_new_contract: 'Договор',
        reimbursedAmounts_new_contractDebt: 'Дълг',
        reimbursedAmounts_new_reimbursementDate: 'Дата на възстановяване',
        reimbursedAmounts_new_type: 'Вид',
        reimbursedAmounts_new_reimbursement: 'Начин на възстановяване',

        //reimbursedAmounts_viewReimbursedAmount
        reimbursedAmounts_viewReimbursedAmount_info: 'Статус: {{status}}, Дълг: {{debtNum}}',
        reimbursedAmounts_viewReimbursedAmount_view: 'Основни данни',
        reimbursedAmounts_viewReimbursedAmount_reimbursedAmount: 'Възстановена сума',

        //reimbursedAmounts_basicDataForm
        reimbursedAmounts_basicDataForm_status: 'Статус',
        reimbursedAmounts_basicDataForm_regNumber: 'Номер',
        reimbursedAmounts_basicDataForm_creationType: 'Начин на въвеждане',
        reimbursedAmounts_basicDataForm_isDeletedNote: 'Причина за анулиране',
        reimbursedAmounts_basicDataForm_programme: 'Програма',
        reimbursedAmounts_basicDataForm_contract: 'Договор',
        reimbursedAmounts_basicDataForm_contractName: 'Наименование',
        reimbursedAmounts_basicDataForm_contractRegNumber: 'Рег. номер',
        reimbursedAmounts_basicDataForm_beneficiary: 'Бенефициент',
        reimbursedAmounts_basicDataForm_uinType: 'Булстат/ЕИК/ЕГН',
        reimbursedAmounts_basicDataForm_uin: 'Номер',
        reimbursedAmounts_basicDataForm_name: 'Наименование',
        reimbursedAmounts_basicDataForm_contractDebt: 'Дълг',
        reimbursedAmounts_basicDataForm_payments: 'Искания за плащане',
        reimbursedAmounts_basicDataForm_paymentVersionNum: 'Номер',
        reimbursedAmounts_basicDataForm_requestedAmount: 'Поискана сума БФП',
        reimbursedAmounts_basicDataForm_approvedTotalAmount: 'Одобрена сума БФП',
        reimbursedAmounts_basicDataForm_checkedDate: 'Дата на верификация',
        reimbursedAmounts_basicDataForm_certReportNum: 'Номер на доклад по сертификация',

        //reimbursedAmounts_basicData
        reimbursedAmounts_basicData_title: 'Преглед на основни данни',
        reimbursedAmounts_basicData_del: 'Изтриване',
        reimbursedAmounts_basicData_draft: 'Чернова',
        reimbursedAmounts_basicData_draftConfirm:
          'Сигурни ли сте че искате да върнете записа в чернова?',
        reimbursedAmounts_basicData_enter: 'Въведена',
        reimbursedAmounts_basicData_enterConfirm: 'Сигурни ли сте че искате да въведете записа?',
        reimbursedAmounts_basicData_remove: 'Анулиране',
        reimbursedAmounts_basicData_removeNote: 'Причина за анулиране',
        reimbursedAmounts_basicData_removeConfirm: 'Сигурни ли сте че искате да анулирате записа?',

        //reimbursedAmounts_reimbursedAmountForm
        reimbursedAmounts_reimbursedAmountForm_type: 'Вид',
        reimbursedAmounts_reimbursedAmountForm_reimbursementDate: 'Дата на възстановяване',
        reimbursedAmounts_reimbursedAmountForm_reimbursements: 'Начин на възстановяване',
        reimbursedAmounts_reimbursedAmountForm_principalBfp: 'Главница',
        reimbursedAmounts_reimbursedAmountForm_interesBfp: 'Лихва',

        reimbursedAmounts_reimbursedAmountForm_bfpTotalAmount: 'Общо',
        reimbursedAmounts_reimbursedAmountForm_comment: 'Коментар',
        reimbursedAmounts_reimbursedAmountForm_programmePriority:
          'Разпоредител с бюджетни средства',

        reimbursedAmounts_reimbursedAmountForm_paymentIds: 'Искане за плащане',

        //reimbursedAmounts_editReimbursedAmount
        reimbursedAmounts_editReimbursedAmount_title: 'Преглед на възстановена сума',
        reimbursedAmounts_editReimbursedAmount_edit: 'Редакция',
        reimbursedAmounts_editReimbursedAmount_save: 'Запис',
        reimbursedAmounts_editReimbursedAmount_cancel: 'Отказ',

        //contractReimbursedAmounts_chooseContractModal
        contractReimbursedAmounts_chooseContractModal_title: 'Избор на договор за БФП',
        contractReimbursedAmounts_chooseContractModal_cancel: 'Отказ',
        contractReimbursedAmounts_chooseContractModal_programme: 'Програма',
        contractReimbursedAmounts_chooseContractModal_contractNumber: '№ договор',
        contractReimbursedAmounts_chooseContractModal_search: 'Търси',
        contractReimbursedAmounts_chooseContractModal_choose: 'Избери',
        contractReimbursedAmounts_chooseContractModal_procedure: 'Бюджет',
        contractReimbursedAmounts_chooseContractModal_regNumber: 'Номер',
        contractReimbursedAmounts_chooseContractModal_contractDate: 'Дата на сключване',
        contractReimbursedAmounts_chooseContractModal_name: 'Наименование',
        contractReimbursedAmounts_chooseContractModal_executionStatus: 'Статус на изпълнение',
        contractReimbursedAmounts_chooseContractModal_company: 'Бенефициент',
        contractReimbursedAmounts_chooseContractModal_companyKidCode: 'КО по КИД 2008',

        //contractReimbursedAmounts_chooseContractModal
        contractReimbursedAmounts_chooseDebtModal_title: 'Избор на дълг към договор',
        contractReimbursedAmounts_chooseDebtModal_choose: 'Избери',
        contractReimbursedAmounts_chooseDebtModal_cancel: 'Отказ',
        contractReimbursedAmounts_chooseDebtModal_debt: 'Дълг',

        //contractReimbursedAmounts_amountsSearch
        contractReimbursedAmounts_amountsSearch_contract: 'Договор',
        contractReimbursedAmounts_amountsSearch_type: 'Вид',
        contractReimbursedAmounts_amountsSearch_search: 'Търси',
        contractReimbursedAmounts_amountsSearch_new: 'Нова сума',
        contractReimbursedAmounts_amountsSearch_excelExport: 'Експорт',
        contractReimbursedAmounts_amountsSearch_contractRegNumber: '№ договор',
        contractReimbursedAmounts_amountsSearch_programme: 'Програма',
        contractReimbursedAmounts_amountsSearch_regNumber: 'Номер',
        contractReimbursedAmounts_amountsSearch_status: 'Статус',
        contractReimbursedAmounts_amountsSearch_reimbursement: 'Начин на възстановяване',
        contractReimbursedAmounts_amountsSearch_reimbursementDate: 'Дата на плащане',
        contractReimbursedAmounts_amountsSearch_principalEuAmount: 'Главница-Финансиране от ЕС',
        contractReimbursedAmounts_amountsSearch_principalBgAmount: 'Главница-Финансиране от НФ',
        contractReimbursedAmounts_amountsSearch_principalTotalAmount: 'Главница-Общо',
        contractReimbursedAmounts_amountsSearch_interestEuAmount: 'Лихва-Финансиране от ЕС',
        contractReimbursedAmounts_amountsSearch_interestBgAmount: 'Лихва-Финансиране от НФ',
        contractReimbursedAmounts_amountsSearch_interestTotalAmount: 'Лихва-Общо',

        //contractReimbursedAmounts_newStep1
        contractReimbursedAmounts_newStep1_title:
          'Създаване на възстановена сума по договор (стъпка 1/2)',
        contractReimbursedAmounts_newStep1_next: 'Напред',
        contractReimbursedAmounts_newStep1_cancel: 'Отказ',
        contractReimbursedAmounts_newStep1_programme: 'Основна организация',
        contractReimbursedAmounts_newStep1_contractRegNumber: '№ договор',
        contractReimbursedAmounts_newStep1_contractNumberInvaid: 'Невалиден номер на договор',
        contractReimbursedAmounts_newStep1_chooseContract: 'Търси',

        //contractReimbursedAmounts_newStep2
        contractReimbursedAmounts_newStep2_title:
          'Създаване на възстановена сума по договор (стъпка 2/2)',
        contractReimbursedAmounts_newStep2_save: 'Запис',
        contractReimbursedAmounts_newStep2_cancel: 'Отказ',
        contractReimbursedAmounts_newStep2_reimbursementDate: 'Дата на възстановяване',
        contractReimbursedAmounts_newStep2_type: 'Вид',
        contractReimbursedAmounts_newStep2_reimbursement: 'Начин на възстановяване',
        contractReimbursedAmounts_newStep2_contractData: 'Договор',
        contractReimbursedAmounts_newStep2_beneficiary: 'Бенефициент',
        contractReimbursedAmounts_newStep2_programmePriority: 'Разпоредител с бюджетни средства',

        //contractReimbursedAmounts_viewReimbursedAmount
        contractReimbursedAmounts_viewReimbursedAmount_info:
          'Статус: {{status}}, Договор: {{contractNum}}',
        contractReimbursedAmounts_viewReimbursedAmount_view: 'Основни данни',
        contractReimbursedAmounts_viewReimbursedAmount_reimbursedAmount: 'Възстановена сума',

        //contractReimbursedAmounts_basicDataForm
        contractReimbursedAmounts_basicDataForm_status: 'Статус',
        contractReimbursedAmounts_basicDataForm_regNumber: 'Номер',
        contractReimbursedAmounts_basicDataForm_isDeletedNote: 'Причина за анулиране',
        contractReimbursedAmounts_basicDataForm_programme: 'Основна организация',
        contractReimbursedAmounts_basicDataForm_contract: 'Договор',
        contractReimbursedAmounts_basicDataForm_contractName: 'Наименование',
        contractReimbursedAmounts_basicDataForm_contractRegNumber: 'Рег. номер',
        contractReimbursedAmounts_basicDataForm_beneficiary: 'Бенефициент',
        contractReimbursedAmounts_basicDataForm_uinType: 'Булстат/ЕИК/ЕГН',
        contractReimbursedAmounts_basicDataForm_uin: 'Номер',
        contractReimbursedAmounts_basicDataForm_name: 'Наименование',

        //contractReimbursedAmounts_basicData
        contractReimbursedAmounts_basicData_title: 'Преглед на основни данни',
        contractReimbursedAmounts_basicData_attachToDebt: 'Свържи с дълг',
        contractReimbursedAmounts_basicData_del: 'Изтриване',
        contractReimbursedAmounts_basicData_draft: 'Чернова',
        contractReimbursedAmounts_basicData_draftConfirm:
          'Сигурни ли сте че искате да върнете записа в чернова?',
        contractReimbursedAmounts_basicData_enter: 'Въведена',
        contractReimbursedAmounts_basicData_enterConfirm:
          'Сигурни ли сте че искате да въведете записа?',
        contractReimbursedAmounts_basicData_remove: 'Анулиране',
        contractReimbursedAmounts_basicData_removeNote: 'Причина за анулиране',
        contractReimbursedAmounts_basicData_removeConfirm:
          'Сигурни ли сте че искате да анулирате записа?',

        //fiReimbursedAmounts_fiReimbursedAmountForm
        fiReimbursedAmounts_fiReimbursedAmountForm_type: 'Суми, възстановени на ФИ',
        fiReimbursedAmounts_fiReimbursedAmountForm_reimbursementDate: 'Дата на възстановяване',
        fiReimbursedAmounts_fiReimbursedAmountForm_reimbursements: 'Начин на възстановяване',
        fiReimbursedAmounts_fiReimbursedAmountForm_principalBfp: 'Главница',
        fiReimbursedAmounts_fiReimbursedAmountForm_interesBfp: 'Лихва',
        fiReimbursedAmounts_fiReimbursedAmountForm_bfpEuAmount: 'Финансиране от ЕС',
        fiReimbursedAmounts_fiReimbursedAmountForm_bfpBgAmount: 'Финансиране от НФ',
        fiReimbursedAmounts_fiReimbursedAmountForm_bfpTotalAmount: 'Общо',
        fiReimbursedAmounts_fiReimbursedAmountForm_comment: 'Коментар',
        fiReimbursedAmounts_fiReimbursedAmountForm_programmePriority:
          'Разпоредител с бюджетни средства',

        //fiReimbursedAmounts_amountsSearch
        fiReimbursedAmounts_amountsSearch_contract: 'Договор',
        fiReimbursedAmounts_amountsSearch_type: 'Суми, възстановени на ФИ',
        fiReimbursedAmounts_amountsSearch_search: 'Търси',
        fiReimbursedAmounts_amountsSearch_new: 'Нова сума',
        fiReimbursedAmounts_amountsSearch_contractRegNumber: '№ договор',
        fiReimbursedAmounts_amountsSearch_programme: 'Програма',
        fiReimbursedAmounts_amountsSearch_regNumber: 'Номер',
        fiReimbursedAmounts_amountsSearch_status: 'Статус',
        fiReimbursedAmounts_amountsSearch_reimbursement: 'Начин на възстановяване',
        fiReimbursedAmounts_amountsSearch_reimbursementDate: 'Дата на плащане',

        //fiReimbursedAmounts_newStep1
        fiReimbursedAmounts_newStep1_title: 'Създаване на възстановена сума по ФИ (стъпка 1/2)',
        fiReimbursedAmounts_newStep1_next: 'Напред',
        fiReimbursedAmounts_newStep1_cancel: 'Отказ',
        fiReimbursedAmounts_newStep1_programme: 'Програма',
        fiReimbursedAmounts_newStep1_contractRegNumber: '№ договор',
        fiReimbursedAmounts_newStep1_contractNumberInvaid: 'Невалиден номер на договор',
        fiReimbursedAmounts_newStep1_chooseContract: 'Търси',

        //fiReimbursedAmounts_newStep2
        fiReimbursedAmounts_newStep2_title: 'Създаване на възстановена сума по ФИ (стъпка 2/2)',
        fiReimbursedAmounts_newStep2_save: 'Запис',
        fiReimbursedAmounts_newStep2_cancel: 'Отказ',
        fiReimbursedAmounts_newStep2_reimbursementDate: 'Дата на възстановяване',
        fiReimbursedAmounts_newStep2_type: 'Суми, възстановени на ФИ',
        fiReimbursedAmounts_newStep2_reimbursement: 'Начин на възстановяване',
        fiReimbursedAmounts_newStep2_contractData: 'Договор',
        fiReimbursedAmounts_newStep2_beneficiary: 'Бенефициент',
        fiReimbursedAmounts_newStep2_programmePriority: 'Разпоредител с бюджетни средства',

        //fiReimbursedAmounts_viewReimbursedAmount
        fiReimbursedAmounts_viewReimbursedAmount_info:
          'Статус: {{status}}, Договор: {{contractNum}}',
        fiReimbursedAmounts_viewReimbursedAmount_view: 'Основни данни',
        fiReimbursedAmounts_viewReimbursedAmount_reimbursedAmount: 'Възстановена сума',

        //fiReimbursedAmounts_basicDataForm
        fiReimbursedAmounts_basicDataForm_status: 'Статус',
        fiReimbursedAmounts_basicDataForm_regNumber: 'Номер',
        fiReimbursedAmounts_basicDataForm_isDeletedNote: 'Причина за анулиране',
        fiReimbursedAmounts_basicDataForm_programme: 'Програма',
        fiReimbursedAmounts_basicDataForm_contract: 'Договор',
        fiReimbursedAmounts_basicDataForm_contractName: 'Наименование',
        fiReimbursedAmounts_basicDataForm_contractRegNumber: 'Рег. номер',
        fiReimbursedAmounts_basicDataForm_beneficiary: 'Бенефициент',
        fiReimbursedAmounts_basicDataForm_uinType: 'Булстат/ЕИК/ЕГН',
        fiReimbursedAmounts_basicDataForm_uin: 'Номер',
        fiReimbursedAmounts_basicDataForm_name: 'Наименование',

        //fiReimbursedAmounts_basicData
        fiReimbursedAmounts_basicData_title: 'Преглед на основни данни',
        fiReimbursedAmounts_basicData_del: 'Изтриване',
        fiReimbursedAmounts_basicData_draft: 'Чернова',
        fiReimbursedAmounts_basicData_draftConfirm:
          'Сигурни ли сте че искате да върнете записа в чернова?',
        fiReimbursedAmounts_basicData_enter: 'Въведена',
        fiReimbursedAmounts_basicData_enterConfirm: 'Сигурни ли сте че искате да въведете записа?',
        fiReimbursedAmounts_basicData_remove: 'Анулиране',
        fiReimbursedAmounts_basicData_removeNote: 'Причина за анулиране',
        fiReimbursedAmounts_basicData_removeConfirm:
          'Сигурни ли сте че искате да анулирате записа?',

        //euReimbursedAmounts_amountsSearch
        euReimbursedAmounts_amountsSearch_status: 'Статус',
        euReimbursedAmounts_amountsSearch_search: 'Търси',
        euReimbursedAmounts_amountsSearch_new: 'Нова сума',
        euReimbursedAmounts_amountsSearch_programme: 'Програма',

        euReimbursedAmounts_amountsSearch_paymentType: 'Тип на плащането',
        euReimbursedAmounts_amountsSearch_date: 'Дата',
        euReimbursedAmounts_amountsSearch_euTranche: 'Преведен транш от ЕК',

        //euReimbursedAmounts_newAmount
        euReimbursedAmounts_newAmount_title: 'Създаване на нова възстановена от ЕК сума',
        euReimbursedAmounts_newAmount_save: 'Запис',
        euReimbursedAmounts_newAmount_cancel: 'Отказ',
        euReimbursedAmounts_newAmount_programme: 'Програма',

        //euReimbursedAmounts_viewAmount
        euReimbursedAmounts_viewAmount_info: 'Програма: {{programmeCode}}, Статус: {{status}}',
        euReimbursedAmounts_viewAmount_view: 'Възстановена сума',
        euReimbursedAmounts_viewAmount_certReports: 'Доклади по сертификация',

        //euReimbursedAmounts_amountDataForm
        euReimbursedAmounts_amountDataForm_programme: 'Програма',
        euReimbursedAmounts_amountDataForm_status: 'Статус',
        euReimbursedAmounts_amountDataForm_deleteNote: 'Причина за анулиране',

        euReimbursedAmounts_amountDataForm_paymentType: 'Тип на плащането',
        euReimbursedAmounts_amountDataForm_date: 'Дата',
        euReimbursedAmounts_amountDataForm_euTranche: 'Преведен транш от ЕК',
        euReimbursedAmounts_amountDataForm_paymentApp: 'Заявление за плащане',
        euReimbursedAmounts_amountDataForm_paymentAppNum: 'Пореден номер',
        euReimbursedAmounts_amountDataForm_paymentAppSentDate: 'Дата на изпращане',
        euReimbursedAmounts_amountDataForm_paymentAppNumMaxLength:
          'Полето може да е максимум 50 символа.',
        euReimbursedAmounts_amountDataForm_paymentAppDateFrom: 'Обхванат период - от',
        euReimbursedAmounts_amountDataForm_paymentAppDateTo: 'Обхванат период - до',
        euReimbursedAmounts_amountDataForm_certExpensesLv:
          'Обща сума на сертифицираните разходи по ОП за отчетния период в лева',
        euReimbursedAmounts_amountDataForm_certExpensesEuro:
          'Обща сума на сертифицираните разходи по ОП за отчетния период в евро',
        euReimbursedAmounts_amountDataForm_calculate: 'Изчисли',
        euReimbursedAmounts_amountDataForm_euAmount: 'ЕС',
        euReimbursedAmounts_amountDataForm_bgAmount: 'НФ',
        euReimbursedAmounts_amountDataForm_bfpTotalAmount: 'Общо БФП',
        euReimbursedAmounts_amountDataForm_selfAmount: 'Собствено съфинансиране',
        euReimbursedAmounts_amountDataForm_totalAmount: 'Обща сума',
        euReimbursedAmounts_amountDataForm_note: 'Забележка',

        //euReimbursedAmounts_editAmount
        euReimbursedAmounts_editAmount_title: 'Преглед на възстановена от ЕК сума',
        euReimbursedAmounts_editAmount_edit: 'Редакция',
        euReimbursedAmounts_editAmount_save: 'Запис',
        euReimbursedAmounts_editAmount_cancel: 'Отказ',
        euReimbursedAmounts_editAmount_del: 'Изтриване',
        euReimbursedAmounts_editAmount_draft: 'Чернова',
        euReimbursedAmounts_editAmount_draftConfirm:
          'Сигурни ли сте че искате да върнете записа в чернова?',
        euReimbursedAmounts_editAmount_enter: 'Въведена',
        euReimbursedAmounts_editAmount_enterConfirm: 'Сигурни ли сте че искате да въведете записа?',
        euReimbursedAmounts_editAmount_remove: 'Анулиране',
        euReimbursedAmounts_editAmount_removeNote: 'Причина за анулиране',
        euReimbursedAmounts_editAmount_removeConfirm:
          'Сигурни ли сте че искате да анулирате записа?',

        //euReimbursedAmounts_chooseCertRrportsModal
        euReimbursedAmounts_chooseCertRrportsModal_title: 'Избор на доклади по сертификация',
        euReimbursedAmounts_chooseCertRrportsModal_choose: 'Избери',
        euReimbursedAmounts_chooseCertRrportsModal_cancel: 'Отказ',
        euReimbursedAmounts_chooseCertRrportsModal_orderNum: 'Пореден номер',
        euReimbursedAmounts_chooseCertRrportsModal_regDate: 'Дата на регистрация',
        euReimbursedAmounts_chooseCertRrportsModal_dateFrom: 'Период от',
        euReimbursedAmounts_chooseCertRrportsModal_dateTo: 'Период до',
        euReimbursedAmounts_chooseCertRrportsModal_type: 'Тип',

        //euReimbursedAmounts_certReportsSearch
        euReimbursedAmounts_certReportsSearch_choose: 'Избор',
        euReimbursedAmounts_certReportsSearch_orderNum: 'Пореден номер',
        euReimbursedAmounts_certReportsSearch_regDate: 'Дата на регистрация',
        euReimbursedAmounts_certReportsSearch_dateFrom: 'Период от',
        euReimbursedAmounts_certReportsSearch_dateTo: 'Период до',
        euReimbursedAmounts_certReportsSearch_type: 'Тип',

        //certAuthorityCommunications_tabs
        certAuthorityCommunications_tabs_contract: 'Договор',
        certAuthorityCommunications_tabs_communication: 'Комуникация',

        //certAuthorityCommunications_contractsSearch
        certAuthorityCommunications_contractsSearch_search: 'Търси',
        certAuthorityCommunications_contractsSearch_programmePriority:
          'Разпоредител с бюджетни средства',
        certAuthorityCommunications_contractsSearch_procedure: 'Бюджет',
        certAuthorityCommunications_contractsSearch_regNumber: 'Номер',
        certAuthorityCommunications_contractsSearch_contractDate: 'Дата на сключване',
        certAuthorityCommunications_contractsSearch_name: 'Наименование',
        certAuthorityCommunications_contractsSearch_executionStatus: 'Статус на изпълнение',
        certAuthorityCommunications_contractsSearch_company: 'Бенефициент',
        certAuthorityCommunications_contractsSearch_companyKidCode: 'КО по КИД 2008',

        //certAuthorityCommunications_viewContract
        certAuthorityCommunications_viewContract_title: 'Преглед на договор',
        certAuthorityCommunications_viewContract_contractData: 'Общи данни',
        certAuthorityCommunications_viewContract_beneficiary: 'Бенефициент',

        //certAuthorityCommunications_search
        certAuthorityCommunications_search_new: 'Нова комуникация',
        certAuthorityCommunications_search_status: 'Статус',
        certAuthorityCommunications_search_source: 'Изпратено от',
        certAuthorityCommunications_search_regNumber: 'Рег. номер',
        certAuthorityCommunications_search_subject: 'Тема',
        certAuthorityCommunications_search_sendDate: 'Дата на изпращане',
        certAuthorityCommunications_search_readDate: 'Дата на първо отваряне',

        //certAuthorityCommunications_editCommunication
        certAuthorityCommunications_editCommunication_title: 'Преглед на комуникация',
        certAuthorityCommunications_editCommunication_delete: 'Изтриване',
        certAuthorityCommunications_editCommunication_communication: 'Съобщение',

        //auditAuthorityCommunications_tabs
        auditAuthorityCommunications_tabs_contract: 'Договор',
        auditAuthorityCommunications_tabs_communication: 'Комуникация',

        //auditAuthorityCommunications_contractsSearch
        auditAuthorityCommunications_contractsSearch_search: 'Търси',
        auditAuthorityCommunications_contractsSearch_programmePriority:
          'Разпоредител с бюджетни средства',
        auditAuthorityCommunications_contractsSearch_procedure: 'Бюджет',
        auditAuthorityCommunications_contractsSearch_regNumber: 'Номер',
        auditAuthorityCommunications_contractsSearch_contractDate: 'Дата на сключване',
        auditAuthorityCommunications_contractsSearch_name: 'Наименование',
        auditAuthorityCommunications_contractsSearch_executionStatus: 'Статус на изпълнение',
        auditAuthorityCommunications_contractsSearch_company: 'Бенефициент',
        auditAuthorityCommunications_contractsSearch_companyKidCode: 'КО по КИД 2008',

        //auditAuthorityCommunications_viewContract
        auditAuthorityCommunications_viewContract_title: 'Преглед на договор',
        auditAuthorityCommunications_viewContract_contractData: 'Общи данни',
        auditAuthorityCommunications_viewContract_beneficiary: 'Бенефициент',

        //auditAuthorityCommunications_search
        auditAuthorityCommunications_search_new: 'Нова комуникация',
        auditAuthorityCommunications_search_status: 'Статус',
        auditAuthorityCommunications_search_source: 'Изпратено от',
        auditAuthorityCommunications_search_regNumber: 'Рег. номер',
        auditAuthorityCommunications_search_subject: 'Тема',
        auditAuthorityCommunications_search_sendDate: 'Дата на изпращане',
        auditAuthorityCommunications_search_readDate: 'Дата на първо отваряне',

        //auditAuthorityCommunications_editCommunication
        auditAuthorityCommunications_editCommunication_title: 'Преглед на комуникация',
        auditAuthorityCommunications_editCommunication_delete: 'Изтриване',
        auditAuthorityCommunications_editCommunication_communication: 'Съобщение',

        //contractReportCorrections_searchContractReportCorrections
        contractReportCorrections_searchContractReportCorrections_search: 'Търси',
        contractReportCorrections_searchContractReportCorrections_new: 'Нова корекция',
        contractReportCorrections_searchContractReportCorrections_programme: 'Основна организация',
        contractReportCorrections_searchContractReportCorrections_regNumber: 'Номер',
        contractReportCorrections_searchContractReportCorrections_status: 'Статус',
        contractReportCorrections_searchContractReportCorrections_correctedApprovedBfpTotalAmount:
          'Коригирана одобрена сума',

        contractReportCorrections_searchContractReportCorrections_type: 'Вид',
        contractReportCorrections_searchContractReportCorrections_date: 'Дата',

        //contractReportCorrections_new
        contractReportCorrections_new_title: 'Създаване на корекция',
        contractReportCorrections_new_save: 'Запис',
        contractReportCorrections_new_cancel: 'Отказ',
        contractReportCorrections_new_type: 'Вид',
        contractReportCorrections_new_sign: 'Знак',
        contractReportCorrections_new_signPlusNote:
          'Изборът на знак "+" ще доведе до увеличаване на верифицираните суми',
        contractReportCorrections_new_signMinusNote:
          'Изборът на знак "-" ще доведе до намаляване на верифицираните суми',
        contractReportCorrections_new_date: 'Дата',
        contractReportCorrections_new_programme: 'Програма',
        contractReportCorrections_new_procedure: 'Бюджет',
        contractReportCorrections_new_programmePriority: 'Разпоредител с бюджетни средства',
        contractReportCorrections_new_contract: 'Договор',
        contractReportCorrections_new_contractReportPayment: 'Искане за плащане',

        //contractReportCorrections_viewContractReportCorrection
        contractReportCorrections_viewContractReportCorrection_statusInfo:
          'Основна организация: {{programme}}, Статус: {{status}}',
        contractReportCorrections_viewContractReportCorrection_viewTab: 'Основни данни',
        contractReportCorrections_viewContractReportCorrection_correctionTab: 'Корекция',
        contractReportCorrections_viewContractReportCorrection_docsTab: 'Документи',

        //contractReportCorrections_basicDataForm
        contractReportCorrections_basicDataForm_type: 'Вид',
        contractReportCorrections_basicDataForm_status: 'Статус',
        contractReportCorrections_basicDataForm_regNumber: 'Номер',
        contractReportCorrections_basicDataForm_deleteNote: 'Причина за анулиране',
        contractReportCorrections_basicDataForm_programme: 'Основна организация',
        contractReportCorrections_basicDataForm_programmePriority:
          'Разпоредител с бюджетни средства',
        contractReportCorrections_basicDataForm_procedure: 'Бюджет',
        contractReportCorrections_basicDataForm_contract: 'Договор',
        contractReportCorrections_basicDataForm_contractName: 'Наименование',
        contractReportCorrections_basicDataForm_contractRegNumber: 'Рег. номер',
        contractReportCorrections_basicDataForm_beneficiary: 'Бенефициент',
        contractReportCorrections_basicDataForm_uinType: 'Булстат/ЕИК/ЕГН',
        contractReportCorrections_basicDataForm_uin: 'Номер',
        contractReportCorrections_basicDataForm_name: 'Наименование',
        contractReportCorrections_basicDataForm_contractReportPayment: 'Искане за плащане',
        contractReportCorrections_basicDataForm_paymentVersionNum: 'Номер',
        contractReportCorrections_basicDataForm_requestedAmount: 'Поискана сума БФП',
        contractReportCorrections_basicDataForm_paidBfpTotalAmount: 'Сума за плащане БФП',
        contractReportCorrections_basicDataForm_paymentCheckedDate: 'Дата на верификация',
        contractReportCorrections_basicDataForm_checkedDate: 'Дата на проверка',
        contractReportCorrections_basicDataForm_checkedByUser: 'Проверено от',

        //contractReportCorrections_editContractReportCorrection
        contractReportCorrections_editContractReportCorrection_title: 'Преглед на корекция',
        contractReportCorrections_editContractReportCorrection_edit: 'Редакция',
        contractReportCorrections_editContractReportCorrection_save: 'Запис',
        contractReportCorrections_editContractReportCorrection_cancel: 'Отказ',

        //contractReportCorrections_contractReportCorrectionForm
        contractReportCorrections_contractReportCorrectionForm_kind: 'Вид',
        contractReportCorrections_contractReportCorrectionForm_elementNumber: 'Номер на елемента',
        contractReportCorrections_contractReportCorrectionForm_sign: 'Знак',
        contractReportCorrections_contractReportCorrectionForm_signPlusNote:
          'Изборът на знак "+" ще доведе до увеличаване на верифицираните суми',
        contractReportCorrections_contractReportCorrectionForm_signMinusNote:
          'Изборът на знак "-" ще доведе до намаляване на верифицираните суми',
        contractReportCorrections_contractReportCorrectionForm_date: 'Дата',
        contractReportCorrections_contractReportCorrectionForm_description: 'Описание',
        contractReportCorrections_contractReportCorrectionForm_reason: 'Основание',

        contractReportCorrections_contractReportCorrectionForm_totalAmount: 'Общо',
        contractReportCorrections_contractReportCorrectionForm_correctionType: 'Тип на корекцията',
        contractReportCorrections_contractReportCorrectionForm_irregularityId: 'Нередност',
        contractReportCorrections_contractReportCorrectionForm_financialCorrectionId:
          'Финансова корекция',
        contractReportCorrections_contractReportCorrectionForm_flatFinancialCorrectionId:
          'Плоска финансова корекция',
        contractReportCorrections_contractReportCorrectionForm_reportPayment: 'Искане за плащане',
        contractReportCorrections_contractReportCorrectionForm_reportPaymentCheck:
          'Верифицирано искане за плащане',
        contractReportCorrections_contractReportCorrectionForm_document: 'Документ',
        contractReportCorrections_contractReportCorrectionForm_correction: 'Корекция',
        contractReportCorrections_contractReportCorrectionForm_uncertifiedCorrected:
          'Несертифицирана сума',
        contractReportCorrections_contractReportCorrectionForm_certifiedCorrected:
          'Сертифицирана сума',
        contractReportCorrections_contractReportCorrectionForm_certCorrection:
          'Сертифициране на корекция',
        contractReportCorrections_contractReportCorrectionForm_certStatus:
          'Статус на сертифициране',
        contractReportCorrections_contractReportCorrectionForm_certCheckedDate: 'Дата на проверка',
        contractReportCorrections_contractReportCorrectionForm_certCheckedByUser: 'Проверено от',

        //contractReportCorrections_basicData
        contractReportCorrections_basicData_title: 'Преглед на основни данни',
        contractReportCorrections_basicData_del: 'Изтриване',
        contractReportCorrections_basicData_draft: 'Чернова',
        contractReportCorrections_basicData_draftConfirm:
          'Сигурни ли сте че искате да върнете записа в чернова?',
        contractReportCorrections_basicData_enter: 'Въведена',
        contractReportCorrections_basicData_enterConfirm:
          'Сигурни ли сте че искате да въведете записа?',
        contractReportCorrections_basicData_remove: 'Анулиране',
        contractReportCorrections_basicData_removeNote: 'Причина за анулиране',
        contractReportCorrections_basicData_removeConfirm:
          'Сигурни ли сте че искате да анулирате записа?',

        //contractReportCorrections_documentForm
        contractReportCorrections_documentForm_description: 'Описание',
        contractReportCorrections_documentForm_file: 'Файл',

        //contractReportCorrections_docsSearch
        contractReportCorrections_docsSearch_docs: 'Документи към корекция',
        contractReportCorrections_docsSearch_description: 'Описание',
        contractReportCorrections_docsSearch_file: 'Файл',

        //contractReportCorrections_newDoc
        contractReportCorrections_newDoc_title: 'Нов документ към корекция',
        contractReportCorrections_newDoc_save: 'Запис',
        contractReportCorrections_newDoc_cancel: 'Отказ',

        //contractReportCorrections_docEdit
        contractReportCorrections_docEdit_title: 'Преглед на документ към корекция',
        contractReportCorrections_docEdit_edit: 'Редакция',
        contractReportCorrections_docEdit_del: 'Изтриване',
        contractReportCorrections_docEdit_save: 'Запис',
        contractReportCorrections_docEdit_cancel: 'Отказ',

        //contractReportRevalidations_searchContractReportRevalidations
        contractReportRevalidations_searchContractReportRevalidations_search: 'Търси',
        contractReportRevalidations_searchContractReportRevalidations_new: 'Ново препотвърждаване',
        contractReportRevalidations_searchContractReportRevalidations_programme: 'Програма',
        contractReportRevalidations_searchContractReportRevalidations_regNumber: 'Номер',
        contractReportRevalidations_searchContractReportRevalidations_status: 'Статус',
        contractReportRevalidations_searchContractReportRevalidations_type: 'Вид',
        contractReportRevalidations_searchContractReportRevalidations_date: 'Дата',

        //contractReportRevalidations_new
        contractReportRevalidations_new_title: 'Създаване на препотвърждаване',
        contractReportRevalidations_new_save: 'Запис',
        contractReportRevalidations_new_cancel: 'Отказ',
        contractReportRevalidations_new_type: 'Вид',
        contractReportRevalidations_new_sign: 'Знак',
        contractReportRevalidations_new_date: 'Дата',
        contractReportRevalidations_new_programme: 'Програма',
        contractReportRevalidations_new_procedure: 'Бюджет',
        contractReportRevalidations_new_programmePriority: 'Разпоредител с бюджетни средства',
        contractReportRevalidations_new_contract: 'Договор',
        contractReportRevalidations_new_contractReportPayment: 'Искане за плащане',

        //contractReportRevalidations_viewContractReportRevalidation
        contractReportRevalidations_viewContractReportRevalidation_statusInfo:
          'Програма: {{programme}}, Статус: {{status}}',
        contractReportRevalidations_viewContractReportRevalidation_viewTab: 'Основни данни',
        contractReportRevalidations_viewContractReportRevalidation_revalidationTab:
          'Препотвърждаване',
        contractReportRevalidations_viewContractReportRevalidation_docsTab: 'Документи',

        //contractReportRevalidations_basicDataForm
        contractReportRevalidations_basicDataForm_type: 'Вид',
        contractReportRevalidations_basicDataForm_status: 'Статус',
        contractReportRevalidations_basicDataForm_regNumber: 'Номер',
        contractReportRevalidations_basicDataForm_deleteNote: 'Причина за анулиране',
        contractReportRevalidations_basicDataForm_programme: 'Програма',
        contractReportRevalidations_basicDataForm_programmePriority:
          'Разпоредител с бюджетни средства',
        contractReportRevalidations_basicDataForm_procedure: 'Бюджет',
        contractReportRevalidations_basicDataForm_contract: 'Договор',
        contractReportRevalidations_basicDataForm_contractName: 'Наименование',
        contractReportRevalidations_basicDataForm_contractRegNumber: 'Рег. номер',
        contractReportRevalidations_basicDataForm_beneficiary: 'Бенефициент',
        contractReportRevalidations_basicDataForm_uinType: 'Булстат/ЕИК/ЕГН',
        contractReportRevalidations_basicDataForm_uin: 'Номер',
        contractReportRevalidations_basicDataForm_name: 'Наименование',
        contractReportRevalidations_basicDataForm_contractReportPayment: 'Искане за плащане',
        contractReportRevalidations_basicDataForm_paymentVersionNum: 'Номер',
        contractReportRevalidations_basicDataForm_requestedAmount: 'Поискана сума БФП',
        contractReportRevalidations_basicDataForm_paidBfpTotalAmount: 'Сума за плащане БФП',
        contractReportRevalidations_basicDataForm_paymentCheckedDate: 'Дата на верификация',
        contractReportRevalidations_basicDataForm_checkedDate: 'Дата на проверка',
        contractReportRevalidations_basicDataForm_checkedByUser: 'Проверено от',

        //contractReportRevalidations_editContractReportRevalidation
        contractReportRevalidations_editContractReportRevalidation_title:
          'Преглед на препотвърждаване',
        contractReportRevalidations_editContractReportRevalidation_edit: 'Редакция',
        contractReportRevalidations_editContractReportRevalidation_save: 'Запис',
        contractReportRevalidations_editContractReportRevalidation_cancel: 'Отказ',

        //contractReportRevalidations_contractReportRevalidationForm
        contractReportRevalidations_contractReportRevalidationForm_sign: 'Знак',
        contractReportRevalidations_contractReportRevalidationForm_date: 'Дата',
        contractReportRevalidations_contractReportRevalidationForm_description: 'Описание',
        contractReportRevalidations_contractReportRevalidationForm_reason: 'Основание',

        contractReportRevalidations_contractReportRevalidationForm_euAmount: 'БФП - ЕС',
        contractReportRevalidations_contractReportRevalidationForm_bgAmount: 'БФП - НФ',
        contractReportRevalidations_contractReportRevalidationForm_crossAmount:
          'Кръстосано съфинансиране',
        contractReportRevalidations_contractReportRevalidationForm_bfpTotalAmount: 'Общо БФП',
        contractReportRevalidations_contractReportRevalidationForm_selfAmount:
          'Собствено съфинанс.',
        contractReportRevalidations_contractReportRevalidationForm_totalAmount: 'Общо',
        contractReportRevalidations_contractReportRevalidationForm_reportPayment:
          'Искане за плащане',
        contractReportRevalidations_contractReportRevalidationForm_reportPaymentCheck:
          'Верифицирано искане за плащане',
        contractReportRevalidations_contractReportRevalidationForm_document: 'Документ',
        contractReportRevalidations_contractReportRevalidationForm_revalidation: 'Препотвърждаване',
        contractReportRevalidations_contractReportRevalidationForm_uncertifiedRevalidated:
          'Несертифицирана сума',
        contractReportRevalidations_contractReportRevalidationForm_certifiedRevalidated:
          'Сертифицирана сума',
        contractReportRevalidations_contractReportRevalidationForm_certRevalidation:
          'Сертифициране на препотвърждаване',
        contractReportRevalidations_contractReportRevalidationForm_certStatus:
          'Статус на сертифициране',
        contractReportRevalidations_contractReportRevalidationForm_certCheckedDate:
          'Дата на проверка',
        contractReportRevalidations_contractReportRevalidationForm_certCheckedByUser:
          'Проверено от',

        //contractReportRevalidations_basicData
        contractReportRevalidations_basicData_title: 'Преглед на основни данни',
        contractReportRevalidations_basicData_del: 'Изтриване',
        contractReportRevalidations_basicData_draft: 'Чернова',
        contractReportRevalidations_basicData_draftConfirm:
          'Сигурни ли сте че искате да върнете записа в чернова?',
        contractReportRevalidations_basicData_enter: 'Въведена',
        contractReportRevalidations_basicData_enterConfirm:
          'Сигурни ли сте че искате да въведете записа?',
        contractReportRevalidations_basicData_remove: 'Анулиране',
        contractReportRevalidations_basicData_removeNote: 'Причина за анулиране',
        contractReportRevalidations_basicData_removeConfirm:
          'Сигурни ли сте че искате да анулирате записа?',

        //contractReportRevalidations_documentForm
        contractReportRevalidations_documentForm_description: 'Описание',
        contractReportRevalidations_documentForm_file: 'Файл',

        //contractReportRevalidations_docsSearch
        contractReportRevalidations_docsSearch_docs: 'Документи към препотвърждаване',
        contractReportRevalidations_docsSearch_description: 'Описание',
        contractReportRevalidations_docsSearch_file: 'Файл',

        //contractReportRevalidations_newDoc
        contractReportRevalidations_newDoc_title: 'Нов документ към препотвърждаване',
        contractReportRevalidations_newDoc_save: 'Запис',
        contractReportRevalidations_newDoc_cancel: 'Отказ',

        //contractReportRevalidations_docEdit
        contractReportRevalidations_docEdit_title: 'Преглед на документ към препотвърждаване',
        contractReportRevalidations_docEdit_edit: 'Редакция',
        contractReportRevalidations_docEdit_del: 'Изтриване',
        contractReportRevalidations_docEdit_save: 'Запис',
        contractReportRevalidations_docEdit_cancel: 'Отказ',

        //contractReportCertCorrections_searchContractReportCertCorrections
        contractReportCertCorrections_searchContractReportCertCorrections_search: 'Търси',
        contractReportCertCorrections_searchContractReportCertCorrections_new: 'Ново изравняване',
        contractReportCertCorrections_searchContractReportCertCorrections_programme: 'Програма',
        contractReportCertCorrections_searchContractReportCertCorrections_regNumber: 'Номер',
        contractReportCertCorrections_searchContractReportCertCorrections_status: 'Статус',
        contractReportCertCorrections_searchContractReportCertCorrections_type: 'Вид',
        contractReportCertCorrections_searchContractReportCertCorrections_date: 'Дата',

        //contractReportCertCorrections_new
        contractReportCertCorrections_new_title: 'Създаване на изравняване',
        contractReportCertCorrections_new_save: 'Запис',
        contractReportCertCorrections_new_cancel: 'Отказ',
        contractReportCertCorrections_new_type: 'Вид',
        contractReportCertCorrections_new_sign: 'Знак',
        contractReportCertCorrections_new_date: 'Дата',
        contractReportCertCorrections_new_programme: 'Програма',
        contractReportCertCorrections_new_procedure: 'Бюджет',
        contractReportCertCorrections_new_programmePriority: 'Разпоредител с бюджетни средства',
        contractReportCertCorrections_new_contract: 'Договор',
        contractReportCertCorrections_new_contractReportPayment: 'Искане за плащане',

        //contractReportCertCorrections_viewContractReportCertCorrection
        contractReportCertCorrections_viewContractReportCertCorrection_statusInfo:
          'Програма: {{programme}}, Статус: {{status}}',
        contractReportCertCorrections_viewContractReportCertCorrection_viewTab: 'Основни данни',
        contractReportCertCorrections_viewContractReportCertCorrection_correctionTab: 'Изравняване',
        contractReportCertCorrections_viewContractReportCertCorrection_docsTab: 'Документи',

        //contractReportCertCorrections_basicDataForm
        contractReportCertCorrections_basicDataForm_type: 'Вид',
        contractReportCertCorrections_basicDataForm_status: 'Статус',
        contractReportCertCorrections_basicDataForm_regNumber: 'Номер',
        contractReportCertCorrections_basicDataForm_deleteNote: 'Причина за анулиране',
        contractReportCertCorrections_basicDataForm_programme: 'Програма',
        contractReportCertCorrections_basicDataForm_programmePriority:
          'Разпоредител с бюджетни средства',
        contractReportCertCorrections_basicDataForm_procedure: 'Бюджет',
        contractReportCertCorrections_basicDataForm_contract: 'Договор',
        contractReportCertCorrections_basicDataForm_contractName: 'Наименование',
        contractReportCertCorrections_basicDataForm_contractRegNumber: 'Рег. номер',
        contractReportCertCorrections_basicDataForm_beneficiary: 'Бенефициент',
        contractReportCertCorrections_basicDataForm_uinType: 'Булстат/ЕИК/ЕГН',
        contractReportCertCorrections_basicDataForm_uin: 'Номер',
        contractReportCertCorrections_basicDataForm_name: 'Наименование',
        contractReportCertCorrections_basicDataForm_contractReportPayment: 'Искане за плащане',
        contractReportCertCorrections_basicDataForm_paymentVersionNum: 'Номер',
        contractReportCertCorrections_basicDataForm_requestedAmount: 'Поискана сума БФП',
        contractReportCertCorrections_basicDataForm_paidBfpTotalAmount: 'Сума за плащане БФП',
        contractReportCertCorrections_basicDataForm_paymentCheckedDate: 'Дата на верификация',
        contractReportCertCorrections_basicDataForm_checkedDate: 'Дата на проверка',
        contractReportCertCorrections_basicDataForm_checkedByUser: 'Проверено от',

        //contractReportCertCorrections_editContractReportCertCorrection
        contractReportCertCorrections_editContractReportCertCorrection_title:
          'Преглед на изравняване',
        contractReportCertCorrections_editContractReportCertCorrection_edit: 'Редакция',
        contractReportCertCorrections_editContractReportCertCorrection_save: 'Запис',
        contractReportCertCorrections_editContractReportCertCorrection_cancel: 'Отказ',

        //contractReportCertCorrections_contractReportCertCorrectionForm
        contractReportCertCorrections_contractReportCertCorrectionForm_sign: 'Знак',
        contractReportCertCorrections_contractReportCertCorrectionForm_date: 'Дата',
        contractReportCertCorrections_contractReportCertCorrectionForm_description: 'Описание',
        contractReportCertCorrections_contractReportCertCorrectionForm_reason: 'Основание',

        contractReportCertCorrections_contractReportCertCorrectionForm_euAmount: 'БФП - ЕС',
        contractReportCertCorrections_contractReportCertCorrectionForm_bgAmount: 'БФП - НФ',
        contractReportCertCorrections_contractReportCertCorrectionForm_crossAmount:
          'Кръстосано съфинансиране',
        contractReportCertCorrections_contractReportCertCorrectionForm_bfpTotalAmount: 'Общо БФП',
        contractReportCertCorrections_contractReportCertCorrectionForm_selfAmount:
          'Собствено съфинанс.',
        contractReportCertCorrections_contractReportCertCorrectionForm_totalAmount: 'Общо',

        contractReportCertCorrections_contractReportCertCorrectionForm_reportPayment:
          'Искане за плащане',
        contractReportCertCorrections_contractReportCertCorrectionForm_reportPaymentCheck:
          'Верифицирано искане за плащане',
        contractReportCertCorrections_contractReportCertCorrectionForm_document: 'Документ',
        contractReportCertCorrections_contractReportCertCorrectionForm_correction: 'Изравняване',

        //contractReportCertCorrections_basicData
        contractReportCertCorrections_basicData_title: 'Преглед на основни данни',
        contractReportCertCorrections_basicData_del: 'Изтриване',
        contractReportCertCorrections_basicData_draft: 'Чернова',
        contractReportCertCorrections_basicData_draftConfirm:
          'Сигурни ли сте че искате да върнете записа в чернова?',
        contractReportCertCorrections_basicData_enter: 'Въведена',
        contractReportCertCorrections_basicData_enterConfirm:
          'Сигурни ли сте че искате да въведете записа?',
        contractReportCertCorrections_basicData_remove: 'Анулиране',
        contractReportCertCorrections_basicData_removeNote: 'Причина за анулиране',
        contractReportCertCorrections_basicData_removeConfirm:
          'Сигурни ли сте че искате да анулирате записа?',

        //contractReportCertCorrections_documentForm
        contractReportCertCorrections_documentForm_description: 'Описание',
        contractReportCertCorrections_documentForm_file: 'Файл',

        //contractReportCertCorrections_docsSearch
        contractReportCertCorrections_docsSearch_docs: 'Документи към корекция',
        contractReportCertCorrections_docsSearch_description: 'Описание',
        contractReportCertCorrections_docsSearch_file: 'Файл',

        //contractReportCertCorrections_newDoc
        contractReportCertCorrections_newDoc_title: 'Нов документ към корекция',
        contractReportCertCorrections_newDoc_save: 'Запис',
        contractReportCertCorrections_newDoc_cancel: 'Отказ',

        //contractReportCertCorrections_docEdit
        contractReportCertCorrections_docEdit_title: 'Преглед на документ към корекция',
        contractReportCertCorrections_docEdit_edit: 'Редакция',
        contractReportCertCorrections_docEdit_del: 'Изтриване',
        contractReportCertCorrections_docEdit_save: 'Запис',
        contractReportCertCorrections_docEdit_cancel: 'Отказ',

        //contractReportFinancialRevalidations_tabs
        contractReportFinancialRevalidations_tabs_contract: 'Договор',
        contractReportFinancialRevalidations_tabs_report: 'Пакет',
        contractReportFinancialRevalidations_tabs_data: 'Основни данни',
        contractReportFinancialRevalidations_tabs_csds: 'Верифицирани РОД',
        contractReportFinancialRevalidations_tabs_revalidatedCsds: 'Препотвърдени верифицирани РОД',

        //contractReportFinancialRevalidations_viewContractReportFinancialRevalidation
        contractReportFinancialRevalidations_viewContractReportFinancialRevalidation_title:
          'Договор: {{contractName}}',

        //contractReportFinancialRevalidations_search
        contractReportFinancialRevalidations_search_new: 'Ново препотвърждаване',
        contractReportFinancialRevalidations_search_search: 'Търси',
        contractReportFinancialRevalidations_search_contractRegNum: 'Номер на договор',
        contractReportFinancialRevalidations_search_fromDate: 'От дата',
        contractReportFinancialRevalidations_search_toDate: 'До дата',
        contractReportFinancialRevalidations_search_contractName: 'Договор',
        contractReportFinancialRevalidations_search_procedureName: 'Бюджет',
        contractReportFinancialRevalidations_search_reportOrderNum: 'Номер на пакет',
        contractReportFinancialRevalidations_search_status: 'Статус',
        contractReportFinancialRevalidations_search_orderNum: 'Пореден номер',
        contractReportFinancialRevalidations_search_createDate: 'Дата на създаване',
        contractReportFinancialRevalidations_search_notes: 'Бележки',

        //contractReportFinancialRevalidations_newContractReportFinancialRevalidation
        contractReportFinancialRevalidations_newContractReportFinancialRevalidation_title:
          'Нова препотвърждаване',
        contractReportFinancialRevalidations_newContractReportFinancialRevalidation_save: 'Запис',
        contractReportFinancialRevalidations_newContractReportFinancialRevalidation_cancel: 'Отказ',
        contractReportFinancialRevalidations_newContractReportFinancialRevalidation_procedureId:
          'Бюджет',
        contractReportFinancialRevalidations_newContractReportFinancialRevalidation_contractNumber:
          'Номер на договор',
        contractReportFinancialRevalidations_newContractReportFinancialRevalidation_contractReportNumber:
          'Номер на пакет',
        contractReportFinancialRevalidations_newContractReportFinancialRevalidation_chooseContractReport:
          'Търси',
        contractReportFinancialRevalidations_newContractReportFinancialRevalidation_contractNumberInvalid:
          'Невалиден номер на договор',
        contractReportFinancialRevalidations_newContractReportFinancialRevalidation_contractReportNumberInvalid:
          'Невалиден номер на пакет',
        contractReportFinancialRevalidations_newContractReportFinancialRevalidation_notes:
          'Бележки',

        //contractReportFinancialRevalidations_chooseContractReportModal
        contractReportFinancialRevalidations_chooseContractReportModal_title:
          'Избор на пакет отчетни документи',
        contractReportFinancialRevalidations_chooseContractReportModal_cancel: 'Отказ',
        contractReportFinancialRevalidations_chooseContractReportModal_search: 'Търси',
        contractReportFinancialRevalidations_chooseContractReportModal_choose: 'Избери',
        contractReportFinancialRevalidations_chooseContractReportModal_contractRegNum:
          'Номер на договор',
        contractReportFinancialRevalidations_chooseContractReportModal_fromDate: 'От дата',
        contractReportFinancialRevalidations_chooseContractReportModal_toDate: 'До дата',
        contractReportFinancialRevalidations_chooseContractReportModal_contractName: 'Договор',
        contractReportFinancialRevalidations_chooseContractReportModal_procedureName: 'Бюджет',
        contractReportFinancialRevalidations_chooseContractReportModal_orderNum: 'Пореден номер',
        contractReportFinancialRevalidations_chooseContractReportModal_status: 'Статус',
        contractReportFinancialRevalidations_chooseContractReportModal_source: 'Въведен от',
        contractReportFinancialRevalidations_chooseContractReportModal_reportType: 'Тип',
        contractReportFinancialRevalidations_chooseContractReportModal_regDate:
          'Дата на регистрация',
        contractReportFinancialRevalidations_chooseContractReportModal_procedure: 'Бюджет',
        contractReportFinancialRevalidations_chooseContractReportModal_contractNumber:
          'Номер на договор',
        contractReportFinancialRevalidations_chooseContractReportModal_contractReportNum:
          'Номер на пакет',

        //contractReportFinancialRevalidations_viewContract
        contractReportFinancialRevalidations_viewContract_title: 'Преглед на договор',
        contractReportFinancialRevalidations_viewContract_contractData: 'Общи данни',
        contractReportFinancialRevalidations_viewContract_beneficiary: 'Бенефициент',

        //contractReportFinancialRevalidations_viewContractReport
        contractReportFinancialRevalidations_viewContractReport_title:
          'Преглед на пакет отчетни документи',

        //contractReportFinancialRevalidations_editContractReportFinancialRevalidation_title
        contractReportFinancialRevalidations_editContractReportFinancialRevalidation_title:
          'Редакция на препотвърждаване',
        contractReportFinancialRevalidations_editContractReportFinancialRevalidation_ended:
          'Приключен',
        contractReportFinancialRevalidations_editContractReportFinancialRevalidation_endedReason:
          "Сигурни ли сте, че искате да промените статуса на препотвърждаването на 'Приключен'",
        contractReportFinancialRevalidations_editContractReportFinancialRevalidation_draft:
          'Чернова',
        contractReportFinancialRevalidations_editContractReportFinancialRevalidation_draftReason:
          "Сигурни ли сте, че искате да промените статуса на препотвърждаването на 'Чернова'",
        contractReportFinancialRevalidations_editContractReportFinancialRevalidation_edit:
          'Редакция',
        contractReportFinancialRevalidations_editContractReportFinancialRevalidation_save: 'Запис',
        contractReportFinancialRevalidations_editContractReportFinancialRevalidation_cancel:
          'Отказ',
        contractReportFinancialRevalidations_editContractReportFinancialRevalidation_delete:
          'Изтриване',

        //contractReportFinancialRevalidations_contractReportFinancialRevalidationForm
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_reportPayment:
          'Искане за плащане',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_reportFinancial:
          'Финансов отчет',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_reportFinancialRevalidation:
          'Препотвърждаване',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_orderNum:
          'Номер',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_status:
          'Статус',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_document:
          'Документ',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_ended:
          'Приключен',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_notes:
          'Бележки',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_file: 'Файл',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_revalidationDate:
          'Дата на препотвърждаване',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_checkedDate:
          'Дата на проверка',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationForm_checkedByUser:
          'Проверено от',

        //contractReportFinancialRevalidations_viewContractReportFinancialRevalidationReport_title
        contractReportFinancialRevalidations_viewContractReportFinancialRevalidationReport_title:
          'Преглед на пакет отчетни документи',

        //contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_csd:
          'Разходооправдателен документ',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_budgetDetail:
          'Ред от бюджета',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_contractActivity:
          'Дейност',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_csdAmount:
          'Сума на РОД',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_approvedAmount:
          'Одобрена сума',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_euAmount:
          'БФП - ЕС',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_bgAmount:
          'БФП - НФ',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_selfAmount:
          'Собствено съфинансиране',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_totalAmount:
          'Обща сума',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_from:
          'от',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_status:
          'Статус',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_approval:
          'Съгласие',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_confirmCorrect:
          'Сигурни ли сте, че искате да препотвърдите този верифициран РОД ? Това ще премести записа в ' +
          "таб 'Препотвърдени верифицирани РОД'",
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_company:
          'Бенефициент/Партньор/Изпълнител',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsSearch_search:
          'Търси',

        //contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsView
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCSDsView_title:
          'Преглед на разходооправдателен документ',

        //contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_csd:
          'Разходооправдателен документ',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_budgetDetail:
          'Ред от бюджета',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_contractActivity:
          'Дейност',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_approvedAmount:
          'Одобрена сума',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_revalidatedAmount:
          'Препотвърдена сума',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_euAmount:
          'БФП - ЕС',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_bgAmount:
          'БФП - НФ',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_selfAmount:
          'Собствено съфинансиране',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_totalAmount:
          'Обща сума',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_from:
          'от',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_status:
          'Статус',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_approval:
          'Съгласие',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_sign:
          'Знак',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_company:
          'Бенефициент/Партньор/Изпълнител',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsSearch_search:
          'Търси',

        //contractReportFinancialRevalidations_correctionCSDBudgetItemForm
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_correction:
          'Препотвърждаване на разходооправдателен документ',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_sign: 'Знак',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_status: 'Статус',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_notes: 'Бележки',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_euAmount: 'БФП - ЕС',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_bgAmount: 'БФП - НФ',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_bfpTotalAmount: 'Общо БФП',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_selfAmount:
          'Собствено съфинанс.',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_totalAmount: 'Обща сума',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_checkedByUser:
          'Проверено от',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_checkedDate:
          'Дата на проверка',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_revalidatedAmount:
          'Препотвърдена сума на разходооправдателния документ за конкретния бюджетен ред и дейност',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_uncertifiedRevalidated:
          'Несертифицирана сума',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_certifiedRevalidated:
          'Сертифицирана сума',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_certRevalidation:
          'Сертифициране на препотвърждаване',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_certStatus:
          'Статус на сертифициране',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_certCheckedDate:
          'Дата на проверка',
        contractReportFinancialRevalidations_correctionCSDBudgetItemForm_certCheckedByUser:
          'Проверено от',

        //contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsEdit
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsEdit_editTitle:
          'Редакция на препотвърден разходооправдателен документ',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsEdit_viewTitle:
          'Преглед на препотвърден разходооправдателен документ',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsEdit_save:
          'Запис',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsEdit_edit:
          'Редакция',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsEdit_cancel:
          'Отказ',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsEdit_ended:
          'Приключен',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsEdit_endedConfirm:
          "Сигурни ли сте, че искате да смените статуса на препотвърждаването на разходооправдателния документ на 'Приключен'?",
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsEdit_draft:
          'Чернова',
        contractReportFinancialRevalidations_contractReportFinancialRevalidationsCorrectedCSDsEdit_draftConfirm:
          "Сигурни ли сте, че искате да смените статуса на препотвърждаването на разходооправдателния документ на 'Чернова'?",

        //contractReportFinancialCertCorrections_tabs
        contractReportFinancialCertCorrections_tabs_contract: 'Договор',
        contractReportFinancialCertCorrections_tabs_report: 'Пакет',
        contractReportFinancialCertCorrections_tabs_data: 'Основни данни',
        contractReportFinancialCertCorrections_tabs_csds: 'Верифицирани РОД',
        contractReportFinancialCertCorrections_tabs_correctedCsds: 'Изравнени сертифицирани РОД',

        //contractReportFinancialCertCorrections_viewContractReportFinancialCertCorrection
        contractReportFinancialCertCorrections_viewContractReportFinancialCertCorrection_title:
          'Договор: {{contractName}}',

        //contractReportFinancialCertCorrections_search
        contractReportFinancialCertCorrections_search_new: 'Ново изравняване',
        contractReportFinancialCertCorrections_search_search: 'Търси',
        contractReportFinancialCertCorrections_search_contractRegNum: 'Номер на договор',
        contractReportFinancialCertCorrections_search_fromDate: 'От дата',
        contractReportFinancialCertCorrections_search_toDate: 'До дата',
        contractReportFinancialCertCorrections_search_contractName: 'Договор',
        contractReportFinancialCertCorrections_search_procedureName: 'Бюджет',
        contractReportFinancialCertCorrections_search_reportOrderNum: 'Номер на пакет',
        contractReportFinancialCertCorrections_search_status: 'Статус',
        contractReportFinancialCertCorrections_search_orderNum: 'Пореден номер',
        contractReportFinancialCertCorrections_search_createDate: 'Дата на създаване',
        contractReportFinancialCertCorrections_search_notes: 'Бележки',

        //contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection
        contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection_title:
          'Ново изравняване',
        contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection_save:
          'Запис',
        contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection_cancel:
          'Отказ',
        contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection_procedureId:
          'Бюджет',
        contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection_contractNumber:
          'Номер на договор',
        contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection_contractReportNumber:
          'Номер на пакет',
        contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection_chooseContractReport:
          'Търси',
        contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection_contractNumberInvalid:
          'Невалиден номер на договор',
        contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection_contractReportNumberInvalid:
          'Невалиден номер на пакет',
        contractReportFinancialCertCorrections_newContractReportFinancialCertCorrection_notes:
          'Бележки',

        //contractReportFinancialCertCorrections_chooseContractReportModal
        contractReportFinancialCertCorrections_chooseContractReportModal_title:
          'Избор на пакет отчетни документи',
        contractReportFinancialCertCorrections_chooseContractReportModal_cancel: 'Отказ',
        contractReportFinancialCertCorrections_chooseContractReportModal_search: 'Търси',
        contractReportFinancialCertCorrections_chooseContractReportModal_choose: 'Избери',
        contractReportFinancialCertCorrections_chooseContractReportModal_contractRegNum:
          'Номер на договор',
        contractReportFinancialCertCorrections_chooseContractReportModal_fromDate: 'От дата',
        contractReportFinancialCertCorrections_chooseContractReportModal_toDate: 'До дата',
        contractReportFinancialCertCorrections_chooseContractReportModal_contractName: 'Договор',
        contractReportFinancialCertCorrections_chooseContractReportModal_procedureName: 'Бюджет',
        contractReportFinancialCertCorrections_chooseContractReportModal_orderNum: 'Пореден номер',
        contractReportFinancialCertCorrections_chooseContractReportModal_status: 'Статус',
        contractReportFinancialCertCorrections_chooseContractReportModal_source: 'Въведен от',
        contractReportFinancialCertCorrections_chooseContractReportModal_reportType: 'Тип',
        contractReportFinancialCertCorrections_chooseContractReportModal_regDate:
          'Дата на регистрация',
        contractReportFinancialCertCorrections_chooseContractReportModal_procedure: 'Бюджет',
        contractReportFinancialCertCorrections_chooseContractReportModal_contractNumber:
          'Номер на договор',
        contractReportFinancialCertCorrections_chooseContractReportModal_contractReportNum:
          'Номер на пакет',

        //contractReportFinancialCertCorrections_viewContract
        contractReportFinancialCertCorrections_viewContract_title: 'Преглед на договор',
        contractReportFinancialCertCorrections_viewContract_contractData: 'Общи данни',
        contractReportFinancialCertCorrections_viewContract_beneficiary: 'Бенефициент',

        //contractReportFinancialCertCorrections_viewContractReport
        contractReportFinancialCertCorrections_viewContractReport_title:
          'Преглед на пакет отчетни документи',

        //contractReportFinancialCertCorrections_editContractReportFinancialCertCorrection_title
        contractReportFinancialCertCorrections_editContractReportFinancialCertCorrection_title:
          'Редакция на изравняване',
        contractReportFinancialCertCorrections_editContractReportFinancialCertCorrection_ended:
          'Приключен',
        contractReportFinancialCertCorrections_editContractReportFinancialCertCorrection_endedReason:
          "Сигурни ли сте, че искате да промените статуса на изравняването на 'Приключен'",
        contractReportFinancialCertCorrections_editContractReportFinancialCertCorrection_draft:
          'Чернова',
        contractReportFinancialCertCorrections_editContractReportFinancialCertCorrection_draftReason:
          "Сигурни ли сте, че искате да промените статуса на изравняването на 'Чернова'",
        contractReportFinancialCertCorrections_editContractReportFinancialCertCorrection_edit:
          'Редакция',
        contractReportFinancialCertCorrections_editContractReportFinancialCertCorrection_save:
          'Запис',
        contractReportFinancialCertCorrections_editContractReportFinancialCertCorrection_cancel:
          'Отказ',
        contractReportFinancialCertCorrections_editContractReportFinancialCertCorrection_delete:
          'Изтриване',

        //contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_reportPayment:
          'Искане за плащане',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_reportFinancial:
          'Финансов отчет',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_reportFinancialCertCorrection:
          'Изравняване',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_orderNum:
          'Номер',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_status:
          'Статус',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_document:
          'Документ',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_ended:
          'Приключен',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_notes:
          'Бележки',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_file:
          'Файл',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_correctionDate:
          'Дата на изравняването',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_checkedDate:
          'Дата на проверка',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionForm_checkedByUser:
          'Проверено от',

        //contractReportFinancialCertCorrections_viewContractReportFinancialCertCorrectionReport_title
        contractReportFinancialCertCorrections_viewContractReportFinancialCertCorrectionReport_title:
          'Преглед на пакет отчетни документи',

        //contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_csd:
          'Разходооправдателен документ',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_budgetDetail:
          'Ред от бюджета',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_contractActivity:
          'Дейност',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_csdAmount:
          'Сума на РОД',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_approvedAmount:
          'Одобрена сума',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_euAmount:
          'БФП - ЕС',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_bgAmount:
          'БФП - НФ',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_selfAmount:
          'Собствено съфинансиране',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_totalAmount:
          'Обща сума',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_from:
          'от',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_status:
          'Статус',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_approval:
          'Съгласие',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_confirmCorrect:
          'Сигурни ли сте, че искате да изравните този верифициран РОД ? Това ще премести записа в ' +
          "таб 'Изравнени сертифицирани РОД'",
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_company:
          'Бенефициент/Партньор/Изпълнител',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsSearch_search:
          'Търси',

        //contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsView
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCSDsView_title:
          'Преглед на разходооправдателен документ',

        //contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_csd:
          'Разходооправдателен документ',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_budgetDetail:
          'Ред от бюджета',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_contractActivity:
          'Дейност',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_certifiedAmount:
          'Сертифицирана сума',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_correctedApprovedAmount:
          'Корекция на сертифицирана сума',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_euAmount:
          'БФП - ЕС',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_bgAmount:
          'БФП - НФ',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_selfAmount:
          'Собствено съфинансиране',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_totalAmount:
          'Обща сума',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_from:
          'от',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_status:
          'Статус',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_approval:
          'Съгласие',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_sign:
          'Знак',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_company:
          'Бенефициент/Партньор/Изпълнител',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsSearch_search:
          'Търси',

        //contractReportFinancialCertCorrections_correctionCSDBudgetItemForm
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_correction:
          'Изравняване на разходооправдателен документ',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_sign: 'Знак',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_status: 'Статус',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_notes: 'Бележки',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_euAmount: 'БФП - ЕС',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_bgAmount: 'БФП - НФ',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_bfpTotalAmount:
          'Общо БФП',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_selfAmount:
          'Собствено съфинанс.',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_totalAmount: 'Обща сума',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_checkedByUser:
          'Проверено от',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_checkedDate:
          'Дата на проверка',
        contractReportFinancialCertCorrections_correctionCSDBudgetItemForm_certifiedAmount:
          'Изравнена сертифицирана сума на разходооправдателния документ за конкретния бюджетен ред и дейност',

        //contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsEdit
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsEdit_editTitle:
          'Редакция на изравнен разходооправдателен документ',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsEdit_viewTitle:
          'Преглед на изравнен разходооправдателен документ',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsEdit_save:
          'Запис',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsEdit_edit:
          'Редакция',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsEdit_cancel:
          'Отказ',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsEdit_ended:
          'Приключен',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsEdit_endedConfirm:
          "Сигурни ли сте, че искате да смените статуса на изравняването на разходооправдателния документ на 'Приключен'?",
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsEdit_draft:
          'Чернова',
        contractReportFinancialCertCorrections_contractReportFinancialCertCorrectionsCorrectedCSDsEdit_draftConfirm:
          "Сигурни ли сте, че искате да смените статуса на изравняването на разходооправдателния документ на 'Чернова'?",

        //monitoring_anex3Report
        monitoring_anex3Report_contract: 'Договор',
        monitoring_anex3Report_excelExport: 'Експорт',

        //monitoring_advancePaymentsReport
        monitoring_advancePaymentsReport_programme: 'Основна организация',
        monitoring_advancePaymentsReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_advancePaymentsReport_procedure: 'Бюджет',
        monitoring_advancePaymentsReport_fromDate: 'Начална дата',
        monitoring_advancePaymentsReport_toDate: 'Крайна дата',
        monitoring_advancePaymentsReport_search: 'Търси',
        monitoring_advancePaymentsReport_excelExport: 'Експорт',
        monitoring_advancePaymentsReport_noResults: 'Няма намерени резултати',
        monitoring_advancePaymentsReport_contractRegNum: 'Номер на договор',
        monitoring_advancePaymentsReport_beneficiaryName: 'Бенефициент',
        monitoring_advancePaymentsReport_verifiedValue:
          'Стойност на верифицирани авансови плащания',
        monitoring_advancePaymentsReport_verifiedCosts:
          'Стойност на верифицирани разходи, покриващи авансовите плащания',
        monitoring_advancePaymentsReport_verifiedNonCoveredValue:
          'Стойност на непокритите верифицирани авансови плащания',

        //monitoring_doubleFundingReport
        monitoring_doubleFundingReport_uin: 'Булстат',
        monitoring_doubleFundingReport_search: 'Търси',
        monitoring_doubleFundingReport_excelExport: 'Експорт',
        monitoring_doubleFundingReport_beneficiary: 'Бенефициент',
        monitoring_doubleFundingReport_partner: 'Партньор',
        monitoring_doubleFundingReport_name: 'Име',
        monitoring_doubleFundingReport_contractNum: 'Номер на ДБФП',
        monitoring_doubleFundingReport_contractTotalAmount: 'Обща стойност на ДБФП',
        monitoring_doubleFundingReport_contractBfp: 'БФП на ДБФП',
        monitoring_doubleFundingReport_noResults: 'Няма намерени резултати',

        //monitoring_physicalExecutionReport
        monitoring_physicalExecutionReport_programme: 'Основна организация',
        monitoring_physicalExecutionReport_project: 'Проект',
        monitoring_physicalExecutionReport_spd: 'СПД',
        monitoring_physicalExecutionReport_table: 'Таблица',
        monitoring_physicalExecutionReport_search: 'Търси',
        monitoring_physicalExecutionReport_export: 'Експорт',
        monitoring_physicalExecutionReport_noResults: 'Няма намерени резултати',
        monitoring_physicalExecutionReport_table1:
          'Показатели за резултатите за ЕФРР и за Кохезионния фонд (по ' +
          'разпоредител с бюджетни средства и конкретна цел); прилага се също за приоритетната ос за техническа помощ',
        monitoring_physicalExecutionReport_table2a: 'Общи показатели за резултатите за ЕСФ',
        monitoring_physicalExecutionReport_table2b:
          'Показатели за резултатите на ИМЗ по разпоредител с бюджетни средства или ' +
          'по част от разпоредител с бюджетни средства (член 19, параграф 3, приложения I и II към Регламента за ЕСФ)',
        monitoring_physicalExecutionReport_table2c:
          'Специфични за програмата показатели за резултатите за ЕСФ',
        monitoring_physicalExecutionReport_table3a:
          'Общите и специфичните за програмата показатели за изпълнението ' +
          'за ЕФРР и Кохезионния фонд',
        monitoring_physicalExecutionReport_table3b:
          'За някои общи  показатели за предоставяната от ЕФРР подкрепа по ' +
          'цел „Инвестиции за растеж и работни места“, свързани с производствените инвестиции - брой ' +
          'предприятия, подкрепени от мрежата за многостранна подкрепа за същите предприятия на ' +
          'оперативната програма',
        monitoring_physicalExecutionReport_table4a: 'Общи показатели за изпълнение за ЕСФ',
        monitoring_physicalExecutionReport_table4b:
          'Общи специфични за програмата показатели за изпълнение за ЕСФ',
        monitoring_physicalExecutionReport_table5:
          'Информация относно етапните цели и целевите стойности, ' +
          'определени в рамката на изпълнението',
        monitoring_physicalExecutionReport_table12: 'Големи проекти',
        monitoring_physicalExecutionReport_table13: 'Съвместни планове за действие (СПД)',
        monitoring_physicalExecutionReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_physicalExecutionReport_investmentPriority: 'Инвестиционен приоритет',
        monitoring_physicalExecutionReport_specificTarget: 'Специфична цел',

        monitoring_physicalExecutionReport_code: 'Идентификация',
        monitoring_physicalExecutionReport_indicator: 'Показател',
        monitoring_physicalExecutionReport_indicatorKind: 'Вид показател',
        monitoring_physicalExecutionReport_commonIndicator:
          'Общ показател за изпълнението, използван като ' +
          'основа за определяне на целеви стойности',
        monitoring_physicalExecutionReport_indicatorMeasure: 'Мерна единица',
        monitoring_physicalExecutionReport_baseAndTargetMeasure:
          'Мерна единица за базовата и целевата стойност',
        monitoring_physicalExecutionReport_targetMeasure: 'Мерна единица за целевата стойност',
        monitoring_physicalExecutionReport_regionCategory: 'Категория региони',
        monitoring_physicalExecutionReport_companyCount:
          'Брой предприятия, подкрепени от мрежата за многостранна подкрепа на ОП',
        monitoring_physicalExecutionReport_base: 'Базова стойност',
        monitoring_physicalExecutionReport_baseYear: 'Базова година',
        monitoring_physicalExecutionRepor_milestoneTarget: 'Етапна цел (2018 г.)',
        monitoring_physicalExecutionRepor_finalTarget: 'Крайна цел (2023 г.)',
        monitoring_physicalExecutionReport_target: 'Целева стойност (2023 г.)',
        monitoring_physicalExecutionReport_yearly: 'Годишна стойност',
        monitoring_physicalExecutionReport_men: 'Мъже',
        monitoring_physicalExecutionReport_women: 'Жени',
        monitoring_physicalExecutionReport_total: 'Общо',
        monitoring_physicalExecutionReport_2014: '2014',
        monitoring_physicalExecutionReport_2015: '2015',
        monitoring_physicalExecutionReport_2016: '2016',
        monitoring_physicalExecutionReport_2017: '2017',
        monitoring_physicalExecutionReport_2018: '2018',
        monitoring_physicalExecutionReport_2019: '2019',
        monitoring_physicalExecutionReport_2020: '2020',
        monitoring_physicalExecutionReport_2021: '2021',
        monitoring_physicalExecutionReport_2022: '2022',
        monitoring_physicalExecutionReport_2023: '2023',
        monitoring_physicalExecutionReport_cumulative: 'Кумулативна стойност',
        monitoring_physicalExecutionReport_achievementsProportion: 'Съотношение на постиженията',
        monitoring_physicalExecutionReport_cciNumber: 'Номер по CCI',

        monitoring_physicalExecutionReport_totalInvestments: 'Общ размер на инвестициите',
        monitoring_physicalExecutionReport_totalAmount: 'Общ размер на допустимите разходи',
        monitoring_physicalExecutionReport_plannedNotification:
          'Планирана нотификация/дата на представяне',
        monitoring_physicalExecutionReport_approvementDate:
          'Дата на мълчаливо съгласие/одобрение от Комисията',
        monitoring_physicalExecutionReport_plannedStart: 'Планирано започване',
        monitoring_physicalExecutionReport_plannedEndDate: 'Планирана дата на приключване',
        monitoring_physicalExecutionReport_piPriority: 'Приоритет/ос/инвестиционни приоритети',
        monitoring_physicalExecutionReport_financialStage:
          'Настоящ етап на реализация финансов напредък',
        monitoring_physicalExecutionReport_physicalStage:
          'Настоящ етап на реализация — физически напредък',
        monitoring_physicalExecutionReport_mainEndProducts: 'Основни крайни продукти',
        monitoring_physicalExecutionReport_firstContractDate:
          'Дата на подписване на първия договор за извършване на строителни работи',
        monitoring_physicalExecutionReport_remarks: 'Забележки',
        monitoring_physicalExecutionReport_spdName: 'Наименование на СПД',
        monitoring_physicalExecutionReport_spdStage: 'Етап на изпълнение на СПД',
        monitoring_physicalExecutionReport_publicSupportAmount: 'Общ размер на публичната подкрепа',
        monitoring_physicalExecutionReport_spdProgrammeContribution: 'Принос на ОП към СПД',
        monitoring_physicalExecutionReport_spdType: 'Вид на СПД',
        monitoring_physicalExecutionReport_plannedPresentation:
          'Планирано представяне на Комисията',
        monitoring_physicalExecutionReport_presentation: 'Представяне на Комисията',
        monitoring_physicalExecutionReport_plannedExecutionStart:
          'Планирано започване на изпълнението',
        monitoring_physicalExecutionReport_executionStart: 'Започване на изпълнението',
        monitoring_physicalExecutionReport_plannedEnd: 'Планирано приключване',
        monitoring_physicalExecutionReport_ending: 'Приключване',
        monitoring_physicalExecutionReport_mainEndProductsResults:
          'Основни крайни продукти и резултати',
        monitoring_physicalExecutionReport_totalCertifiedAmount:
          'Обща размер на допустимите разходи, сертифицирани пред Комисията',

        //monitoring_financialExecutionReport
        monitoring_financialExecutionReport_programme: 'Основна организация',
        monitoring_financialExecutionReport_date: 'Дата',
        monitoring_financialExecutionReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_financialExecutionReport_year: 'Година',
        monitoring_financialExecutionReport_table: 'Таблица',
        monitoring_financialExecutionReport_search: 'Търси',
        monitoring_financialExecutionReport_export: 'Експорт',
        monitoring_financialExecutionReport_noResults: 'Няма намерени резултати',
        monitoring_financialExecutionReport_table1:
          'Финансова информация на ниво разпоредител с бюджетни средства и програма',
        monitoring_financialExecutionReport_table2:
          'Разбивка на кумулативните финансови данни по категории на интервенция',
        monitoring_financialExecutionReport_table3:
          'Разчетни данни за сумите, за които държавата членка очаква да подаде заявления за междинно плащане за текущата финансова година и следващата финансова година',
        monitoring_financialExecutionReport_table8: 'Използване на кръстосано финансиране',
        monitoring_financialExecutionReport_table9:
          'Разходи за операции извън програмния район (ЕФРР и Кохезионния фонд по целта „Инвестиции за растеж и работни места“)',
        monitoring_financialExecutionReport_table10: 'Разходи, извършени извън Съюза (ЕСФ)',
        monitoring_financialExecutionReport_tableIMZ:
          'Разпределение на средства по ИМЗ за младите хора извън допустимите региони от ниво 2 по NUTS (член 16 от Регламент (ЕС) № 1304/2013)',
        monitoring_financialExecutionReport_imzAmount:
          'Размерът на подкрепата от ЕС по линия на ИМЗ (специално разпределени средства за ИМЗ и съответната подкрепа от ЕСФ), предвиден за отпускане на млади хора извън допустимите региони от ниво 2 по NUTS (в евро), както е посочено в раздел 2.A.6.1 на оперативната програма',
        monitoring_financialExecutionReport_imzOperationsAmount:
          'Размерът на подкрепата от ЕС по линия на ИМЗ (специално разпределени средства за ИМЗ и съответната подкрепа от ЕСФ), отпусната за операции в подкрепа на млади хора извън допустимите региони от ниво 2 по NUTS (в евро)',
        monitoring_financialExecutionReport_contractedImzAmount:
          'Допустими разходи, направени при операции в подкрепа на млади хора извън допустимите региони (в евро)',
        monitoring_financialExecutionReport_imzSelfAmount:
          'Съответстваща подкрепа от ЕС за допустими разходи, направени за операции в подкрепа на млади хора извън допустимите региони, като резултат от прилагането на процента на съфинансиране по приоритетната ос (в евро)',
        monitoring_financialExecutionReport_esfAmount:
          'Размерът на разходите, които се предвижда да бъдат направени извън Съюза по тематични цели 8 и 10 въз основа на избраните операции (в евро)',
        monitoring_financialExecutionReport_esfAmountPercent:
          'Дял на общия размер на отпуснатите средства (от Съюза и като национален принос) за програмата по ЕСФ или частта по ЕСФ от програма, финансирана от няколко фонда (в проценти)',
        monitoring_financialExecutionReport_esfReportedAmount:
          'Допустимите разходи, направени извън Съюза, декларирани от бенефициента пред управляващия орган (в евро)',
        monitoring_financialExecutionReport_operationsAmount:
          'Размерът на подкрепата, предвидена да бъде използвана за операции извън програмния район, въз основа на избраните операции (в евро)',
        monitoring_financialExecutionReport_reportedOperationsAmount:
          'Допустимите разходи, извършени при операции извън програмния район, декларирани от бенефициента пред управляващия орган (в евро)',
        monitoring_financialExecutionReport_crossAmount:
          'Размерът на подкрепата от ЕС, която се предвижда да бъде използвана за кръстосано финансиране въз основа на избраните операции (в евро)',
        monitoring_financialExecutionReport_budgetBfpPercent:
          'Дял на общия размер на отпуснатите от ЕС финансови средства по приоритетната ос (в проценти)',
        monitoring_financialExecutionReport_reportedCrossAmount:
          'Допустимите разходи, използвани в рамките на кръстосаното финансиране, декларирани от бенефициента пред управляващия орган (в евро)',
        monitoring_financialExecutionReport_budgetTotalPercent:
          'Дял на общия размер на финансовите средства по приоритетната ос (в проценти)',

        monitoring_financialExecutionReport_regionCategory: 'Категория на региона',
        monitoring_financialExecutionReport_budgetBfpAmount:
          'Финансови средства, отпуснати по приоритетната ос',
        monitoring_financialExecutionReport_data:
          'Кумулативни данни за финансовия напредък по оперативната програма',
        monitoring_financialExecutionReport_contractedTotalAmount:
          'Общ размер на допустимите разходи за операциите, избрани за подкрепа (в евро)',
        monitoring_financialExecutionReport_contractedPercent:
          'Дял от общия размер на отпуснатите средства, покрит с избраните операции (в процентно изражение)',
        monitoring_financialExecutionReport_contractedBfpAmount:
          'Допустими публични разходи за операциите, избрани за подкрепа (в евро)',
        monitoring_financialExecutionReport_reportedTotalAmount:
          'Общ размер на допустимите разходи, декларирани от бенефициентите пред управляващия орган',
        monitoring_financialExecutionReport_reportedPercent:
          'Дял от общия размер на отпуснатите средства, покрит с допустимите разходи, декларирани от бенефициентите',
        monitoring_financialExecutionReport_contractsCount: 'Брой на избраните операции',
        monitoring_financialExecutionReport_interventionCategories:
          'Категоризация съобразно измеренията',
        monitoring_financialExecutionReport_interventionField: 'Област на интервенция',
        monitoring_financialExecutionReport_formOfFinance: 'Форма на финансиране',
        monitoring_financialExecutionReport_territorialDimension: 'Териториално измерение',
        monitoring_financialExecutionReport_territorialDeliveryMechanism:
          'Териториален механизъм за изпълнение',
        monitoring_financialExecutionReport_thematicObjective:
          'Измерение, свързано с тематичната цел',
        monitoring_financialExecutionReport_esfSecondaryTheme: 'Вторична тема ЕСФ',
        monitoring_financialExecutionReport_economicDimension: 'Икономическо измерение',
        monitoring_financialExecutionReport_nutsName: 'Измерение, свързано с местоположението',
        monitoring_financialExecutionReport_financeData: 'Финансови данни',
        monitoring_financialExecutionReport_currYearEuAmounts:
          'Принос на Съюза - текуща финансова година',
        monitoring_financialExecutionReport_nextYearEuAmounts:
          'Принос на Съюза - следваща финансова година',
        monitoring_financialExecutionReport_januaryToOctober: 'януари — октомври',
        monitoring_financialExecutionReport_novemberToDecember: 'ноември — декември',
        monitoring_financialExecutionReport_januaryToDecember: 'януари — декември',

        //monitoring_projectsReport
        monitoring_projectsReport_programme: 'Основна организация',
        monitoring_projectsReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_projectsReport_procedure: 'Бюджет',
        monitoring_projectsReport_fromDate: 'Дата на регистрация от',
        monitoring_projectsReport_toDate: 'Дата на регистрация до',
        monitoring_projectsReport_nutsLevel: 'Ниво',
        monitoring_projectsReport_countryId: 'Държава',
        monitoring_projectsReport_nuts1Id: 'NUTS ниво 1',
        monitoring_projectsReport_nuts2Id: 'NUTS ниво 2',
        monitoring_projectsReport_districtId: 'Област',
        monitoring_projectsReport_municipalityId: 'Община',
        monitoring_projectsReport_settlementId: 'Населено място',
        monitoring_projectsReport_protectedZoneId: 'Защитена зона',
        monitoring_projectsReport_currency: 'Валута',
        monitoring_projectsReport_search: 'Търси',
        monitoring_projectsReport_excelExport: 'Експорт',
        monitoring_projectsReport_regNumber: 'Рег. номер',
        monitoring_projectsReport_name: 'Наименование',
        monitoring_projectsReport_regDate: 'Дата и час на регистрация',
        monitoring_projectsReport_recieveDate: 'Дата и час на получаване',
        monitoring_projectsReport_recieveType: 'Начин на получаване',
        monitoring_projectsReport_companyUin: 'ЕИК',
        monitoring_projectsReport_companyName: 'Име на организацията',
        monitoring_projectsReport_companyType: 'Тип на организацията',
        monitoring_projectsReport_companyLegalType: 'Вид на организацията',
        monitoring_projectsReport_companyKidCode: 'Код по КИД 2008 на организацията',
        monitoring_projectsReport_companyAddress: 'Адрес на организацията',
        monitoring_projectsReport_companyCorrespondenceAddress: 'Адрес за кореспонденция',
        monitoring_projectsReport_companyEmail: 'Е-mail за контакт',
        monitoring_projectsReport_companySizeType: 'Категория на предприятие',
        monitoring_projectsReport_projectDuration: 'Продължителност на проекта',
        monitoring_projectsReport_projectPlace: 'Място на изпълнение на проекта',
        monitoring_projectsReport_projectKidCode: 'Код по КИД 2008 на проекта',
        monitoring_projectsReport_initialTotalBfpAmount:
          'Общ размер на БФП (лв.) на подаденото проектно предложение (първа версия на проектното предложение)',
        monitoring_projectsReport_initialCoFinancingAmount:
          'Общ размер на съфинансиране (лв.) на подаденото проектно предложение (първа версия на проектното предложение)',
        monitoring_projectsReport_actualTotalBfpAmount:
          'Размер на Одобрената БФП (последна версия на проектното предложение)',
        monitoring_projectsReport_adminAdmiss: 'Оценка ОАСД',
        monitoring_projectsReport_techFinances: 'Оценка ТФО',
        monitoring_projectsReport_complex: 'Комплексна оценка',
        monitoring_projectsReport_standingStatus: 'Статус на проекта от оценката',
        monitoring_projectsReport_noResults: 'Няма намерени резултати',

        //monitoring_contractsReport
        monitoring_contractsReport_programme: 'Основна организация',
        monitoring_contractsReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_contractsReport_procedure: 'Бюджет',
        monitoring_contractsReport_currency: 'Валута',
        monitoring_contractsReport_fromDate: 'Начална дата',
        monitoring_contractsReport_toDate: 'Към дата',

        monitoring_contractsReport_nutsLevel: 'Място на изпълнение',
        monitoring_contractsReport_countryId: 'Държава',
        monitoring_contractsReport_nuts1Id: 'NUTS ниво 1',
        monitoring_contractsReport_nuts2Id: 'NUTS ниво 2',
        monitoring_contractsReport_districtId: 'Област',
        monitoring_contractsReport_municipalityId: 'Община',
        monitoring_contractsReport_settlementId: 'Населено място',
        monitoring_contractsReport_protectedZoneId: 'Защитена зона',
        monitoring_contractsReport_search: 'Търси',
        monitoring_contractsReport_excelExport: 'Експорт',
        monitoring_contractsReport_contract: 'Договор',
        monitoring_contractsReport_company: 'Бенефициент',
        monitoring_contractsReport_initialContractedAmounts:
          'Договорени средства - първа версия на договора',
        monitoring_contractsReport_actualContractedAmounts:
          'Договорени средства - последна версия на договора',
        monitoring_contractsReport_reportedAmounts: 'Отчетени средства',
        monitoring_contractsReport_approvedAmounts: 'Верифицирани средства',
        monitoring_contractsReport_unapprovedAmounts: 'Неверифицирани средства',
        monitoring_contractsReport_unapprovedByCorrectionAmounts:
          'Неверифицирани средства по финансови корекции ',
        monitoring_contractsReport_paidAdvanceAmounts: 'Реално изплатени суми - авансови плащания',
        monitoring_contractsReport_paidIntermediateAmounts:
          'Реално изплатени суми - междинни плащания',
        monitoring_contractsReport_paidFinalAmounts: 'Реално изплатени суми - окончателни плащания',
        monitoring_contractsReport_reimbursedPrincipalAmounts: 'Възстановени суми - главница ',
        monitoring_contractsReport_reimbursedInterestAmounts: 'Възстановени суми - лихви ',
        monitoring_contractsReport_regNumber: 'Рег. номер',
        monitoring_contractsReport_name: 'Наименование',
        monitoring_contractsReport_companyUin: 'ЕИК',
        monitoring_contractsReport_companyName: 'Име',
        monitoring_contractsReport_companyType: 'Тип',
        monitoring_contractsReport_companyLegalType: 'Вид',
        monitoring_contractsReport_companyKidCode: 'Код по КИД 2008 на организацията',
        monitoring_contractsReport_companyAddress: 'Адрес на организацията',
        monitoring_contractsReport_companyCorrespondenceAddress: 'Адрес за кореспонденция',
        monitoring_contractsReport_companyEmail: 'Е-mail за контакт',
        monitoring_contractsReport_companySizeType: 'Категория на предприятие',
        monitoring_contractsReport_projectDuration: 'Продължителност на проекта',

        monitoring_contractsReport_projectKidCode: 'Код по КИД 2008 на проекта',
        monitoring_contractsReport_initialContractDate: 'Дата на сключване на основния договор',
        monitoring_contractsReport_actualContractDate:
          'Дата на сключване на последния анекс по договора',
        monitoring_contractsReport_initialStartDate: 'Първоначална начална дата на договора',
        monitoring_contractsReport_initialCompletionDate: 'Първоначална крайна дата на договора',
        monitoring_contractsReport_actualStartDate: 'Актуална начална дата на договора',
        monitoring_contractsReport_actualCompletionDate: 'Актуална крайна дата на договора',
        monitoring_contractsReport_contractTerminationDate:
          'Дата на приключване/ прекратяване на договора',
        monitoring_contractsReport_contractExecutionStatus: 'Статус на договора',
        monitoring_contractsReport_noResults: 'Няма намерени резултати',
        monitoring_contractsReport_clippedResults:
          'Показани са част от резултатите. За всички резултати използвайте бутона Експорт',

        //monitoring_contractReportsReport
        monitoring_contractReportsReport_programme: 'Основна организация',
        monitoring_contractReportsReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_contractReportsReport_procedure: 'Бюджет',
        monitoring_contractReportsReport_contract: 'Договор',
        monitoring_contractReportsReport_toDate: 'Към дата',
        monitoring_contractReportsReport_reportType: 'Тип',
        monitoring_contractReportsReport_search: 'Търси',
        monitoring_contractReportsReport_excelExport: 'Експорт',
        monitoring_contractReportsReport_reportedAmounts: 'Отчетени средства',
        monitoring_contractReportsReport_verifiedAmounts: 'Верифицирани средства',
        monitoring_contractReportsReport_verifiedAdvancePaymentAmounts:
          'Верифицирани средства, покриващи  авансово плащане по чл. 131 от Регл. EC 1303/2013',

        monitoring_contractReportsReport_paidAmounts:
          'Реално изплатени суми (без възстановени суми)',
        monitoring_contractReportsReport_regNumber: 'Рег. номер на отчет',
        monitoring_contractReportsReport_contractRegNumber: 'Рег. номер на договор',
        monitoring_contractReportsReport_contractExecutionStatus: 'Статус на договор',
        monitoring_contractReportsReport_contractName: 'Име на договор',
        monitoring_contractReportsReport_companyUin: 'ЕИК',
        monitoring_contractReportsReport_companyName: 'Име на организацията',
        monitoring_contractReportsReport_dateFrom: 'Начална дата',
        monitoring_contractReportsReport_dateTo: 'Крайна дата',

        monitoring_contractReportsReport_reportStatus: 'Статус на отчет',
        monitoring_contractReportsReport_submitDate: 'Дата на представяне',
        monitoring_contractReportsReport_requestedAmount: 'Стойност на исканите средства',
        monitoring_contractReportsReport_noResults: 'Няма намерени резултати',

        //monitoring_indicatorsReport
        monitoring_indicatorsReport_programme: 'Основна организация',
        monitoring_indicatorsReport_procedure: 'Бюджет',
        monitoring_indicatorsReport_excelExport: 'Експорт',
        monitoring_indicatorsReport_contractRegNum: 'Номер на договор от СУНИ',
        monitoring_indicatorsReport_contractName: 'Наименование на проекта',
        monitoring_indicatorsReport_nutsFullPathName: 'Място/места на изпълнение',
        monitoring_indicatorsReport_contractExecutionStatus: 'Статус на договора',
        monitoring_indicatorsReport_contractEndTerminationDate: 'Дата на приключване/прекратяване',
        monitoring_indicatorsReport_companyName: 'Бенефициент',
        monitoring_indicatorsReport_companyUin: 'ЕИК',
        monitoring_indicatorsReport_companyType: 'Тип на организацията',
        monitoring_indicatorsReport_companyLegalType: 'Вид на организацията',
        monitoring_indicatorsReport_companySizeType: 'Категория на предприятие',
        monitoring_indicatorsReport_name: 'Име на индикатора',
        monitoring_indicatorsReport_type: 'Тип на индикатора',
        monitoring_indicatorsReport_kind: 'Вид на индикатора',
        monitoring_indicatorsReport_measure: 'Мерна единица',
        monitoring_indicatorsReport_baseTotalValue: 'Базова стойност',
        monitoring_indicatorsReport_targetTotalValue: 'Целева стойност',
        monitoring_indicatorsReport_reportedTotalValue: 'Отчетена стойност',
        monitoring_indicatorsReport_approvedTotalValue: 'Верифицирана стойност',
        monitoring_indicatorsReport_noResults: 'Няма намерени резултати',

        //monitoring_budgetLevelsReport
        monitoring_budgetLevelsReport_programme: 'Основна организация',
        monitoring_budgetLevelsReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_budgetLevelsReport_procedure: 'Бюджет',
        monitoring_budgetLevelsReport_fromDate: 'Начална дата',
        monitoring_budgetLevelsReport_toDate: 'Крайна дата',
        monitoring_budgetLevelsReport_nutsLevel: 'Ниво',
        monitoring_budgetLevelsReport_countryId: 'Държава',
        monitoring_budgetLevelsReport_nuts1Id: 'NUTS ниво 1',
        monitoring_budgetLevelsReport_nuts2Id: 'NUTS ниво 2',
        monitoring_budgetLevelsReport_districtId: 'Област',
        monitoring_budgetLevelsReport_municipalityId: 'Община',
        monitoring_budgetLevelsReport_settlementId: 'Населено място',
        monitoring_budgetLevelsReport_protectedZoneId: 'Защитена зона',
        monitoring_budgetLevelsReport_budgetLevels: 'Ниво на бюджетни пера',
        monitoring_budgetLevelsReport_currency: 'Валута',
        monitoring_budgetLevelsReport_search: 'Търси',

        monitoring_budgetLevelsReport_noResults: 'Няма намерени резултати',
        monitoring_budgetLevelsReport_budgetLevel: 'Бюджетно перо',
        monitoring_budgetLevelsReport_budgetLevel1Name: 'Първо ниво от бюджета',
        monitoring_budgetLevelsReport_budgetLevel2Name: 'Второ ниво от бюджета',
        monitoring_budgetLevelsReport_contractedTotal: 'Договорени средства от текущия договор',
        monitoring_budgetLevelsReport_reportedTotal: 'Отчетени от бенефициента средства',
        monitoring_budgetLevelsReport_approvedTotal: 'Верифицирани средства',

        //monitoring_financialCorrectionsReport
        monitoring_financialCorrectionsReport_programme: 'Основна организация',
        monitoring_financialCorrectionsReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_financialCorrectionsReport_procedure: 'Бюджет',
        monitoring_financialCorrectionsReport_fromDate: 'Начална дата',
        monitoring_financialCorrectionsReport_toDate: 'Крайна дата',
        monitoring_financialCorrectionsReport_currency: 'Валута',
        monitoring_financialCorrectionsReport_search: 'Търси',

        monitoring_financialCorrectionsReport_noResults: 'Няма намерени резултати',
        monitoring_financialCorrectionsReport_contractRegNum: 'Рег. номер на договор',
        monitoring_financialCorrectionsReport_companyUin: 'ЕИК на бенефициента',
        monitoring_financialCorrectionsReport_companyName: 'Име на организацията',
        monitoring_financialCorrectionsReport_companyType: 'Тип на организацията',
        monitoring_financialCorrectionsReport_companyLegalType: 'Вид на организацията',
        monitoring_financialCorrectionsReport_correctionDate: 'Дата на налагане на ФК',
        monitoring_financialCorrectionsReport_correctionNum: 'Пореден номер на ФК',
        monitoring_financialCorrectionsReport_contractContractName:
          'Договор с изпълнител (ако е приложимо)',
        monitoring_financialCorrectionsReport_contractContractCompanyName: 'Име на Изпълнител',
        monitoring_financialCorrectionsReport_contractContractUin: 'ЕИК на Изпълнител',
        monitoring_financialCorrectionsReport_initialContractContractPercent:
          'Процент от Договор с изпълнител-първоначална ФК (ако е приложимо)',
        monitoring_financialCorrectionsReport_initialAmountTotal:
          'Абсолютна стойност на наложената първоначална финансова корекция',
        monitoring_financialCorrectionsReport_initialReason:
          'Основание за налагане на ФК - първоначална ФК',
        monitoring_financialCorrectionsReport_initialViolations:
          'Други констатирани нарушения - първоначална ФК',
        monitoring_financialCorrectionsReport_initialViolationFoundBy:
          'Орган/ институция установила нарушението за ФК - първоначална ФК',
        monitoring_financialCorrectionsReport_initialBearer:
          'Следва да се понесе от - първоначална ФК',
        monitoring_financialCorrectionsReport_currentContractContractPercent:
          'Процент от Договор с изпълнител - текуща версия на ФК (ако е приложимо)',
        monitoring_financialCorrectionsReport_currentAmountTotal:
          'Абсолютна стойност на текуща версия на наложената финансова корекция',
        monitoring_financialCorrectionsReport_amendmentReason: 'Причина за изменението',
        monitoring_financialCorrectionsReport_currentReason:
          'Основание за налагане на ФК - текуща версия на ФК',
        monitoring_financialCorrectionsReport_currentViolations:
          'Други констатирани нарушения - текуща версия на ФК',
        monitoring_financialCorrectionsReport_currentViolationFoundBy:
          'Орган/ институция установила нарушението за ФК - текуща версия на ФК',
        monitoring_financialCorrectionsReport_currentBearer:
          'Следва да се понесе от - текуща версия на ФК',
        monitoring_financialCorrectionsReport_irregularity: 'Нередност (ако е приложимо)',

        monitoring_financialCorrectionsReport_corretionAmountTotal:
          'Стойност на извършените финансови корекции',
        monitoring_financialCorrectionsReport_contractReportPayments:
          'Списък с искания за плащане, в които е включена ФК',

        //monitoring_concludedContractsReport
        monitoring_concludedContractsReport_programme: 'Основна организация',
        monitoring_concludedContractsReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_concludedContractsReport_procedure: 'Бюджет',
        monitoring_concludedContractsReport_fromDate: 'Начална дата',
        monitoring_concludedContractsReport_toDate: 'Крайна дата',
        monitoring_concludedContractsReport_currency: 'Валута',
        monitoring_concludedContractsReport_uin: 'ЕИК',
        monitoring_concludedContractsReport_search: 'Търси',

        monitoring_concludedContractsReport_noResults: 'Няма намерени резултати',
        monitoring_concludedContractsReport_contractContractRegNum: 'Номер на договор с изпълнител',
        monitoring_concludedContractsReport_companyName: 'Име на изпълнител',
        monitoring_concludedContractsReport_companyUin: 'ЕИК на изпълнител',
        monitoring_concludedContractsReport_contractDate:
          'Дата на сключване на Договора с изпълнителя',
        monitoring_concludedContractsReport_contractRegNum: 'Номер от СУНИ на договор за БФП',
        monitoring_concludedContractsReport_contractCompanyName: 'Име на бенефициента',
        monitoring_concludedContractsReport_contractCompanyUin: 'ЕИК на бенефициента',
        monitoring_concludedContractsReport_contractCompanyType: 'Тип на организацията',
        monitoring_concludedContractsReport_contractCompanyLegalType: 'Вид на организацията',
        monitoring_concludedContractsReport_contractContractName:
          'Предмет на бюджета за избор на изпълнител',
        monitoring_concludedContractsReport_errandArea: 'Обект на бюджета',
        monitoring_concludedContractsReport_errandLegalAct: 'Приложим нормативен акт',
        monitoring_concludedContractsReport_errandType: 'Тип на бюджета',
        monitoring_concludedContractsReport_maPreliminaryControl:
          'Бюджета е преминала през предварителен контрол от организация',
        monitoring_concludedContractsReport_ppaPreliminaryControl:
          'Бюджета е преминала през предварителен контрол от АОП',
        monitoring_concludedContractsReport_totalFundedValue: 'Обща сума на договора',
        monitoring_concludedContractsReport_subcontractors: 'Подизпълнители',
        monitoring_concludedContractsReport_unionMembers: 'Членове на обединение',
        monitoring_concludedContractsReport_reportedTotalAmount:
          'Обща сума на отчетените разходи по договора',
        monitoring_concludedContractsReport_approvedTotalAmount:
          'Обща сума на верифицираните разходи по договора',
        monitoring_concludedContractsReport_financialCorrectionTotalAmount:
          'Абсолютна стойност на наложената финансова корекция (текуща)',

        //monitoring_beneficiaryProjectsContractsReport
        monitoring_beneficiaryProjectsContractsReport_programme: 'Основна организация',
        monitoring_beneficiaryProjectsContractsReport_programmePriority:
          'Разпоредител с бюджетни средства',
        monitoring_beneficiaryProjectsContractsReport_procedure: 'Бюджет',
        monitoring_beneficiaryProjectsContractsReport_fromDate: 'Начална дата',
        monitoring_beneficiaryProjectsContractsReport_toDate: 'Крайна дата',
        monitoring_beneficiaryProjectsContractsReport_currency: 'Валута',
        monitoring_beneficiaryProjectsContractsReport_companyType: 'Тип на организацията',
        monitoring_beneficiaryProjectsContractsReport_companyLegalType: 'Вид на организацията',
        monitoring_beneficiaryProjectsContractsReport_search: 'Търси',
        monitoring_beneficiaryProjectsContractsReport_excelExport: 'Експорт',
        monitoring_beneficiaryProjectsContractsReport_noResults: 'Няма намерени резултати',
        monitoring_beneficiaryProjectsContractsReport_companyName: 'Име на бенефициент',
        monitoring_beneficiaryProjectsContractsReport_companyUin: 'ЕИК на бенефициент',
        monitoring_beneficiaryProjectsContractsReport_projectsCount:
          'Брой подадени проектни предложения',
        monitoring_beneficiaryProjectsContractsReport_projectsTotalAmount:
          'Стойност на подадените проектни предложения',
        monitoring_beneficiaryProjectsContractsReport_contractsCount: 'Брой сключени договори',
        monitoring_beneficiaryProjectsContractsReport_contractsTotalAmount:
          'Стойност на проектите по сключените договори',
        monitoring_beneficiaryProjectsContractsReport_actuallyPaidTotalAmount:
          'Стойност нa реално изплатените суми',
        monitoring_beneficiaryProjectsContractsReport_irregularitySignalsCount:
          'Подадени сигнали за нередности срещу бенефициента',
        monitoring_beneficiaryProjectsContractsReport_irregularitySignalsActiveCount:
          'Активни сигнали',
        monitoring_beneficiaryProjectsContractsReport_irregularitiesCount:
          'Брой регистрирани нередности',
        monitoring_beneficiaryProjectsContractsReport_financialCorrectionTotalAmount:
          'Абсолютна стойност на наложената финансова корекция',
        monitoring_beneficiaryProjectsContractsReport_correctionsTotalAmount:
          'Стойност на извършените финансови корекции',

        //monitoring_evaluationsReport
        monitoring_evaluationsReport_programme: 'Основна организация',
        monitoring_evaluationsReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_evaluationsReport_procedure: 'Бюджет',
        monitoring_evaluationsReport_search: 'Търси',

        monitoring_evaluationsReport_noResults: 'Няма намерени резултати',
        monitoring_evaluationsReport_projectRegNum: 'Номер от СУНИ на ПП',
        monitoring_evaluationsReport_company: 'Кандидат',
        monitoring_evaluationsReport_companyUin: 'ЕИК на кандидат',
        monitoring_evaluationsReport_initialProjectTotalAmount: 'Стойност на подаденото ПП',
        monitoring_evaluationsReport_actualProjectTotalAmount: 'Стойност на одобреното ПП',
        monitoring_evaluationsReport_committeeStartDate: 'Дата на сформиране на комисия',
        monitoring_evaluationsReport_committeeEndDate: 'Дата на приключване на комисия',
        monitoring_evaluationsReport_communicationsDuration:
          'Продължителност на комуникация с кандидата (дни)',
        monitoring_evaluationsReport_communicationsCount: 'Брой комуникации с кандидата',

        //monitoring_contractReportPaymentsReport
        monitoring_contractReportPaymentsReport_programme: 'Основна организация',
        monitoring_contractReportPaymentsReport_programmePriority:
          'Разпоредител с бюджетни средства',
        monitoring_contractReportPaymentsReport_procedure: 'Бюджет',
        monitoring_contractReportPaymentsReport_fromDate: 'Начална дата',
        monitoring_contractReportPaymentsReport_toDate: 'Крайна дата',
        monitoring_contractReportPaymentsReport_currency: 'Валута',
        monitoring_contractReportPaymentsReport_search: 'Търси',

        monitoring_contractReportPaymentsReport_noResults: 'Няма намерени резултати',
        monitoring_contractReportPaymentsReport_contractRegNum: 'Номер на договор от СУНИ',
        monitoring_contractReportPaymentsReport_reportNum: 'Пореден номер на ПОД',
        monitoring_contractReportPaymentsReport_paymentNum: 'Пореден номер на ИП',
        monitoring_contractReportPaymentsReport_companyUin: 'Булстат',
        monitoring_contractReportPaymentsReport_companyName: 'Име на Бенефициент',
        monitoring_contractReportPaymentsReport_regDate: 'Дата на регистрация',
        monitoring_contractReportPaymentsReport_paymentType: 'Тип на ИП',
        monitoring_contractReportPaymentsReport_paymentStatus: 'Статус',
        monitoring_contractReportPaymentsReport_paymentTotalAmount: 'Обща стойност на ИП',
        monitoring_contractReportPaymentsReport_paymentApprovedAmount:
          'Верифицирана от организация сума',
        monitoring_contractReportPaymentsReport_paymentPaidAmount: 'Сума за плащане',
        monitoring_contractReportPaymentsReport_paymentCheckDate: 'Дата на крайна проверка',
        monitoring_contractReportPaymentsReport_paymentActuallyPaidAmount: 'Изплатена сума',
        monitoring_contractReportPaymentsReport_paymentActuallyPaidDate: 'Дата на плащане',
        monitoring_contractReportPaymentsReport_paymentReimbursedAmount: 'Възстановени суми',
        monitoring_contractReportPaymentsReport_paymentReimbursementDate: 'Дата на възстановяване',

        //monitoring_programmeSummary
        monitoring_programmeSummary_programme: 'Основна организация',
        monitoring_programmeSummary_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_programmeSummary_procedure: 'Бюджет',
        monitoring_programmeSummary_groupingLevel: 'Групиране по',
        monitoring_programmeSummary_fromDate: 'Начална дата',
        monitoring_programmeSummary_toDate: 'Крайна дата',
        monitoring_programmeSummary_currency: 'Валута',
        monitoring_programmeSummary_nutsLevel: 'Ниво',
        monitoring_programmeSummary_countryId: 'Държава',
        monitoring_programmeSummary_nuts1Id: 'NUTS ниво 1',
        monitoring_programmeSummary_nuts2Id: 'NUTS ниво 2',
        monitoring_programmeSummary_districtId: 'Област',
        monitoring_programmeSummary_municipalityId: 'Община',
        monitoring_programmeSummary_settlementId: 'Населено място',
        monitoring_programmeSummary_protectedZoneId: 'Защитена зона',
        monitoring_programmeSummary_search: 'Търси',

        monitoring_programmeSummary_noResults: 'Няма намерени резултати',
        monitoring_programmeSummary_contractRegNum: 'Договор',
        monitoring_programmeSummary_programmeBudgetBfpTotalAmount: 'Бюджет на основна организация',
        monitoring_programmeSummary_contractedTotalAmount: 'Договорени средства',
        monitoring_programmeSummary_reportedTotalAmount: 'Отчетени средства',
        monitoring_programmeSummary_actuallyPaidTotalAmount: 'Реално изплатени суми',
        monitoring_programmeSummary_approvedTotalAmount: 'Верифицирани разходи',

        //monitoring_irregularties
        monitoring_irregularties_programme: 'Основна организация',
        monitoring_irregularties_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_irregularties_procedure: 'Бюджет',
        monitoring_irregularties_fromDate: 'Начална дата',
        monitoring_irregularties_toDate: 'Крайна дата',
        monitoring_irregularties_search: 'Търси',

        monitoring_irregularties_noResults: 'Няма намерени резултати',
        monitoring_irregularties_beneficiaryName: 'Бенефициент',
        monitoring_irregularties_beneficiaryUin: 'ЕИК',
        monitoring_irregularties_beneficiaryType: 'Тип на бенефициента',
        monitoring_irregularties_beneficiaryLegalType: 'Вид на бенефициента',
        monitoring_irregularties_beneficiarySeatAddress: 'Адрес на бенефициента',
        monitoring_irregularties_beneficiaryCorrespondenceAddress: 'Адрес за кореспонденция',
        monitoring_irregularties_contractRegNum: 'Номер на договор за БФП ИСУН',
        monitoring_irregularties_project: 'Наименование на проекта',
        monitoring_irregularties_irregularitySignal: 'Сигнал за нередност',
        monitoring_irregularties_irregularitySignalRegDate: 'Дата на регистриране на сигнала',
        monitoring_irregularties_status: 'Статус',
        monitoring_irregularties_irregularityRegNum: 'Номер на Нередност',
        monitoring_irregularties_irregularityRegDate: 'Дата на нередността',
        monitoring_irregularties_irregularityValue: 'Стойност на нередността',
        monitoring_irregularties_financialCorrections: 'Финансови корекции',

        //monitoring_pin
        monitoring_pin_programme: 'Основна организация',
        monitoring_pin_fromDate: 'Начална дата',
        monitoring_pin_toDate: 'Крайна дата',
        monitoring_pin_uin: 'ЕГН/ЛНЧ',
        monitoring_pin_search: 'Търси',

        monitoring_pin_noResults: 'Няма намерени резултати',
        monitoring_pin_name: 'Име',
        monitoring_pin_date: 'Дата',
        monitoring_pin_hours: 'Отработени часове',
        monitoring_pin_contractRegNum: 'Номер на договор от СУНИ',

        //monitoring_arachne
        monitoring_arachne_programme: 'Основна организация',
        monitoring_arachne_xmlExport: 'Експорт',

        //monitoring_microdataEsfReport
        monitoring_microdataEsfReport_programme: 'Основна организация',
        monitoring_microdataEsfReport_programmePriority: 'Разпоредител с бюджетни средства',
        monitoring_microdataEsfReport_procedure: 'Бюджет',
        monitoring_microdataEsfReport_toDate: 'Към дата',
        monitoring_microdataEsfReport_excelExport: 'Експорт',

        //monitoring_v4Plus4Report
        monitoring_v4Plus4Report_financeSource: 'Фонд',
        monitoring_v4Plus4Report_proceduresEuAmount: 'Процедури за предоставяне на БФП - ЕС',
        monitoring_v4Plus4Report_proceduresBgAmount: 'Процедури за предоставяне на БФП – НФ',
        monitoring_v4Plus4Report_proceduresBfpTotalAmount:
          'Процедури за предоставяне на БФП – Общо',
        monitoring_v4Plus4Report_projectsBfpTotalAmount: 'Проектни предложения - Общо',
        monitoring_v4Plus4Report_contractsEuAmount: 'Сключени договори за БФП - ЕС',
        monitoring_v4Plus4Report_contractsBgAmount: 'Сключени договори за БФП - НФ',
        monitoring_v4Plus4Report_contractsBfpTotalAmount: 'Сключени договори за БФП - Общо',
        monitoring_v4Plus4Report_actuallyPaidEuAmount: 'РИС - ЕС',
        monitoring_v4Plus4Report_actuallyPaidBgAmount: 'РИС - НФ',
        monitoring_v4Plus4Report_actuallyPaidTotalAmount: 'РИС - Общо',
        monitoring_v4Plus4Report_search: 'Търси',
        monitoring_v4Plus4Report_toDate: 'Към дата',
        monitoring_v4Plus4Report_excelExport: 'Експорт',

        //monitoring_expenseTypesReport
        monitoring_expenseTypes_programme: 'Основна организация',
        monitoring_expenseTypes_expenseType: 'Тип на разхода',
        monitoring_expenseTypes_verifiedAmounts: 'Верифицирани разходи',
        monitoring_expenseTypes_certifiedAmounts: 'Сертифицирани разходи',
        monitoring_expenseTypes_search: 'Търси',
        monitoring_expenseTypes_toDate: 'Към дата',
        monitoring_expenseTypes_excelExport: 'Експорт',

        //monitoring_sebra
        monitoring_sebra_programme: 'Оперативна програма',
        monitoring_sebra_procedure: 'Процедура',
        monitoring_sebra_fromDate: 'Начална дата',
        monitoring_sebra_toDate: 'Крайна дата',
        monitoring_sebra_fromNumber: 'От номер',
        monitoring_sebra_toNumber: 'До номер',
        monitoring_sebra_paymentType: 'Вид на плащане',
        monitoring_sebra_sendername: 'Заместване на подател (sendername)',
        monitoring_sebra_acc: 'IBAN или код на БР на наредителя',
        monitoring_sebra_o1: 'Основание за плащане',
        monitoring_sebra_template: 'Свали шаблон',
        monitoring_sebra_chooseFile: 'Зареди ПП от Excel',
        monitoring_sebra_file: 'Файл',
        monitoring_sebra_chooseFilter: 'Избор чрез филтриране',
        monitoring_sebra_xmlExport: 'Експорт',

        //sapFiles_filesSearch
        sapFiles_filesSearch_status: 'Статус',
        sapFiles_filesSearch_type: 'Вид',
        sapFiles_filesSearch_search: 'Търси',
        sapFiles_filesSearch_new: 'Нов файл',
        sapFiles_filesSearch_createdByUser: 'Създал',
        sapFiles_filesSearch_createDate: 'Дата на създаване',

        //sapFiles_newFile
        sapFiles_newFile_title: 'Нов файл от САП',
        sapFiles_newFile_type: 'Вид на файла',
        sapFiles_newFile_save: 'Запис',
        sapFiles_newFile_cancel: 'Отказ',
        sapFiles_newFile_file: 'Файл',
        sapFiles_newFile_fileInvalid: 'Невалиден файл',

        //sapFiles_view
        sapFiles_view_data: 'Файл от САП',
        sapFiles_view_paidAmounts: 'Данни',
        sapFiles_view_distributedLimits: 'Данни',

        //sapFiles_sapFileData
        sapFiles_sapFileData_status: 'Статус',
        sapFiles_sapFileData_createDate: 'Дата на създаване',
        sapFiles_sapFileData_createdByUser: 'Създал',
        sapFiles_sapFileData_type: 'Вид на файла',
        sapFiles_sapFileData_file: 'Xml файл',
        sapFiles_sapFileData_sapDate: 'Дата от САП',
        sapFiles_sapFileData_sapUser: 'Потребител от САП',

        //sapFiles_editSapFile
        sapFiles_editSapFile_title: 'Преглед на файл от САП',
        sapFiles_editSapFile_import: 'Импортирай',
        sapFiles_editSapFile_importConfirm: 'Сигурни ли сте че искате да импортирате данните?',
        sapFiles_editSapFile_delete: 'Изтрий',

        //sapFiles_paidAmounts
        sapFiles_paidAmounts_data: 'Извлечени данни',
        sapFiles_paidAmounts_imported: 'Импортиран',
        sapFiles_paidAmounts_importedDoc: 'Документ',
        sapFiles_paidAmounts_contractSapNum: '№ договор',
        sapFiles_paidAmounts_fund: 'Фонд',

        sapFiles_paidAmounts_contractReportPaymentNum: '№ искане за плащане',
        sapFiles_paidAmounts_contractReportPaymentDate: 'Дата на ИП',
        sapFiles_paidAmounts_paidAmount: 'Платена сума',
        sapFiles_paidAmounts_paymentType: 'Вид плащане',
        sapFiles_paidAmounts_comment: 'Коментар',
        sapFiles_paidAmounts_errors: 'Възникнали грешки',
        sapFiles_paidAmounts_warnings: 'Възникнали предупреждения',

        //sapFiles_distributedLimits
        sapFiles_distributedLimits_data: 'Извлечени данни',
        sapFiles_distributedLimits_imported: 'Импортиран',
        sapFiles_distributedLimits_importedDoc: 'Документ',
        sapFiles_distributedLimits_contractSapNum: '№ договор',

        sapFiles_distributedLimits_contractReportPaymentNum: '№ искане за плащане',
        sapFiles_distributedLimits_contractReportPaymentDate: 'Дата на ИП',
        sapFiles_distributedLimits_paidAmount: 'Платена сума',
        sapFiles_distributedLimits_paymentType: 'Вид плащане',
        sapFiles_distributedLimits_comment: 'Коментар',
        sapFiles_distributedLimits_errors: 'Възникнали грешки',
        sapFiles_distributedLimits_warnings: 'Възникнали предупреждения',

        //sapCertReports_search

        sapCertReports_search_search: 'Търси',
        sapCertReports_search_excelExport: 'Експорт (excel)',
        sapCertReports_search_tsvExport: 'Експорт (tsv)',
        sapCertReports_search_contractNum: 'Номер на договор',
        sapCertReports_search_paymentNum: 'Пореден номер на ИП',
        sapCertReports_search_programmePriorityCode:
          'Разпоредител с бюджетни средства на финансиране',
        sapCertReports_search_financeFund: 'Фонд',

        sapCertReports_search_totalAmount: 'Сума',
        sapCertReports_search_certDate: 'Дата на която е подписан Сертификата към комисията',
        sapCertReports_search_finYear: 'Счетоводна година',
        sapCertReports_search_certNum: 'Пореден номер на одобрения Сертификат',

        //monitorstat_newMonitorstatForm
        monitorstat_newMonitorstatForm_title: 'Зареждане на отчети от мониторстат',
        monitorstat_newMonitorstatForm_cancel: 'Отказ',
        monitorstat_newMonitorstatForm_year: 'Година',
        monitorstat_newMonitorstatForm_load: 'Зареди',
        monitorstat_newMonitorstatForm_confirmExternalLoading:
          'Ще бъдат заредени данни от системата на "Мониторстат". Продължение?',

        //monitorstat_searchMonitorstat
        monitorstat_searchMonitorstat_newBtn: 'Зареждане от "Мониторстат"',
        monitorstat_searchMonitorstat_year: 'Година',
        monitorstat_searchMonitorstat_createDate: 'Дата на създаване',
        monitorstat_searchMonitorstat_surveyName: 'Отчет',

        //monitorstat_viewMonitorstat
        monitorstat_viewMonitorstat_year: 'Година',
        monitorstat_viewMonitorstat_code: 'Код',
        monitorstat_viewMonitorstat_createDate: 'Дата на създаване',
        monitorstat_viewMonitorstat_name: 'Наименование на отчет',
        monitorstat_viewMonitorstat_reportCode: 'Код на справка',
        monitorstat_viewMonitorstat_reportName: 'Наименование на справка',

        //regixInterfaces_view
        regixInterfaces_tabs_validPerson: 'Справка за валидност на физическо лице',
        regixInterfaces_tabs_personalIdentity: 'Справка за лице по документ за самоличност',
        regixInterfaces_tabs_actualState: 'Справка за актуално състояние',
        regixInterfaces_tabs_stateOfPlay: 'Справка по код на БУЛСТАТ',
        regixInterfaces_tabs_npoRegistration: 'Справка за вписано юридическо лице',

        //regixInterfaces_regixInterfacesNpoRegistration
        regixInterfaces_regixInterfacesNpoRegistration_title:
          'Интерграция с RegiX (МП) - Справка за вписано юридическо лице с нестопанска цел',
        regixInterfaces_regixInterfacesNpoRegistration_orgForm: 'Форма',
        regixInterfaces_regixInterfacesNpoRegistration_nationality: 'Националност',

        //regixInterfaces_regixInterfacesPersonIdentity
        regixInterfaces_regixInterfacesPersonIdentity_title:
          'Интерграция с RegiX (МВР) - Справка за лице по документ за самоличност',

        //regixInterfaces_regixInterfacesValidPerson
        regixInterfaces_regixInterfacesValidPerson_title:
          'Интерграция с RegiX (ГРАО-НБД) - Справка за валидност на физическо лице',
        regixInterfaces_regixInterfacesStateOfPlay_title:
          'Интерграция с RegiX (АВ/БУЛСТАТ) - Справка по код на БУЛСТАТ',
        regixInterfaces_regixInterfacesValidPerson_check: 'Провери',
        regixInterfaces_regixInterfacesValidPerson_personId: 'ЕГН',
        regixInterfaces_regixInterfacesValidPerson_firstName: 'Име',
        regixInterfaces_regixInterfacesValidPerson_surName: 'Презиме',
        regixInterfaces_regixInterfacesValidPerson_familyName: 'Фамилия',
        regixInterfaces_regixInterfacesValidPerson_birthDay: 'Дата на раждане',
        regixInterfaces_regixInterfacesValidPerson_idNumber: 'Номер на документ за самоличност',

        //regixInterfaces_regixInterfacesActualState
        regixInterfaces_regixInterfacesActualState_title:
          'Интерграция с RegiX (АВ/ТР) - Справка за актуално състояние(v1)',
        regixInterfaces_regixInterfacesActualState_check: 'Провери',
        regixInterfaces_regixInterfacesActualState_uic: 'ЕИК',
        regixInterfaces_regixInterfacesActualState_company: 'Наименование',
        regixInterfaces_regixInterfacesActualState_transliteration: 'Транслитерация',
        regixInterfaces_regixInterfacesActualState_wayOfManagement: 'Начин на управление',

        //interfaces_other
        interfaces_other_contract: 'Договор',
        interfaces_other_system: 'Информационна система',
        interfaces_other_contract_xmlExport: 'Експорт',

        //projectDossier_view
        projectDossier_view_title: 'Проектно досие',
        projectDossier_view_projectNumber: 'ПП номер',
        projectDossier_view_projectNumberInvaid: 'Невалиден номер на ПП',
        projectDossier_view_chooseProject: 'Търси ПП',
        projectDossier_view_titleText:
          'Номер на ПП: {{projectRegNumber}}, Номер на договор: {{contractRegNumber}}',
        projectDossier_view_next: 'Напред',
        projectDossier_view_contract: 'Договор',

        //projectDossier_tabs
        projectDossier_tabs_project: 'Проектно предложение',
        projectDossier_tabs_documents: 'Документи',
        projectDossier_tabs_contract: 'Договор',
        projectDossier_tabs_paidAmounts: 'Изплатени суми',
        projectDossier_tabs_debts: 'Дългове',
        projectDossier_tabs_reimbursedAmounts: 'Възстановени суми',
        projectDossier_tabs_financialCorrections: 'Финансови корекции',
        projectDossier_tabs_approvedAmounts: 'Верифицирани средства',
        projectDossier_tabs_certifiedAmounts: 'Сертифицирани средства',
        projectDossier_tabs_physicalExecution: 'Физическо изпълнение',
        projectDossier_tabs_spotChecks: 'Проверки на място',
        projectDossier_tabs_irregularitiesAndSignals: 'Нередности и сигнали',
        projectDossier_tabs_audits: 'Одити',

        //projectDossier_modals_chooseProjectModal
        projectDossier_modals_chooseProjectModal_title: 'Избор на ПП',
        projectDossier_modals_chooseProjectModal_cancel: 'Отказ',
        projectDossier_modals_chooseProjectModal_procedure: 'Бюджет',
        projectDossier_modals_chooseProjectModal_regNumber: 'Номер на ПП',
        projectDossier_modals_chooseProjectModal_search: 'Търси',
        projectDossier_modals_chooseProjectModal_choose: 'Избери',
        projectDossier_modals_chooseProjectModal_company: 'Кандидат',
        projectDossier_modals_chooseProjectModal_name: 'Наименование',
        projectDossier_modals_chooseProjectModal_registrationStatus: 'Регистрационен статус',
        projectDossier_modals_chooseProjectModal_projectType: 'Тип',
        projectDossier_modals_chooseProjectModal_regDate: 'Дата на регистрация',

        //projectDossier_viewProject
        projectDossier_viewProject_title: 'Преглед на проектно предложение',
        projectDossier_viewProject_regData: 'Регистрационни данни',
        projectDossier_viewProject_history: 'История на промените',
        projectDossier_viewProject_historyExcelExport: 'Експорт',
        projectDossier_viewProject_cancelations: 'Анулирания',
        projectDossier_viewProject_createFromRegData: 'Зареди от регистрационните данни',
        projectDossier_viewProject_projectStatus: 'Статус',
        projectDossier_viewProject_createDate: 'Дата на създаване',
        projectDossier_viewProject_createNote: 'Бележка',
        projectDossier_viewProject_modifyDate: 'Дата на последна промяна',
        projectDossier_viewProject_file: 'Файл',
        projectDossier_viewProject_signatures: 'Ел. подписи',
        projectDossier_viewProject_communication: 'Комуникация с кандидата',
        projectDossier_viewProject_communicationExcelExport: 'Експорт',
        projectDossier_viewProject_otherCommunication: 'Друга комуникация с кандидата',
        projectDossier_viewProject_communicationSessionNum: 'Номер на сесия',
        projectDossier_viewProject_communicationStatus: 'Статус',
        projectDossier_viewProject_regNumber: 'Рег. номер',
        projectDossier_viewProject_questionDate: 'Дата на изпращане',
        projectDossier_viewProject_questionEndingDate: 'Краен срок за отговор',
        projectDossier_viewProject_answerDate: 'Дата на отговор',
        projectDossier_viewProject_evaluations: 'Обобщени оценки',
        projectDossier_viewProject_evaluationExcelExport: 'Експорт',
        projectDossier_viewProject_type: 'Тип на етап',
        projectDossier_viewProject_calculationType: 'Тип на обобщаването',
        projectDossier_viewProject_pass: 'Преминава',
        projectDossier_viewProject_result: 'Точки',
        projectDossier_viewProject_note: 'Коментар',
        projectDossier_viewProject_isDeleted: 'Анулиран',
        projectDossier_viewProject_isDeletedNote: 'Причина за анулиране',
        projectDossier_viewProject_cancelProject: 'Анулиране',
        projectDossier_viewProject_restoreProject: 'Възстановяване',
        projectDossier_viewProject_cancelMessage: 'Причина за анулиране',
        projectDossier_viewProject_confirmCancel:
          'Сигурни ли сте, че искате да анулирате проектното предложение?',
        projectDossier_viewProject_standings: 'Класиране',
        projectDossier_viewProject_standingExcelExport: 'Експорт',
        projectDossier_viewProject_newStanding: 'Ново класиране',
        projectDossier_viewProject_isPreliminary: 'Предварително',
        projectDossier_viewProject_orderNum: 'Пореден номер',
        projectDossier_viewProject_standingType: 'Тип',
        projectDossier_viewProject_status: 'Статус',
        projectDossier_viewProject_grandAmount: 'Одобрено БФП (лв.)',
        projectDossier_viewProject_notes: 'Бележки',
        projectDossier_viewProject_adminAdmiss: 'Обобщена ОАСД',
        projectDossier_viewProject_techFinance: 'Обобщена ТФО',
        projectDossier_viewProject_complex: 'Обобщена КО',
        projectDossier_viewProject_evalSessionNum: 'Номер на сесия',

        //projectDossier_editProjectVersion
        projectDossier_editProjectVersion_title: 'Редакция на проектно предложение',
        projectDossier_editProjectVersion_back: 'Назад',
        projectDossier_editProjectVersion_edit: 'Редакция',
        projectDossier_editProjectVersion_delete: 'Изтриване',
        projectDossier_editProjectVersion_save: 'Запис',
        projectDossier_editProjectVersion_cancel: 'Отказ',
        projectDossier_editProjectVersion_template: 'Проектно предложение',
        projectDossier_editProjectVersion_file: 'Файл',
        projectDossier_editProjectVersion_signatures: 'Електронни подписи',

        //projectDossier_editEvalSessionProjectCommunication
        projectDossier_editEvalSessionProjectCommunication_title: 'Преглед на въпрос към кандидат',
        projectDossier_editEvalSessionProjectCommunication_back: 'Назад',
        projectDossier_editEvalSessionProjectCommunication_delete: 'Изтриване',
        projectDossier_editEvalSessionProjectCommunication_edit: 'Редакция',
        projectDossier_editEvalSessionProjectCommunication_save: 'Запис',
        projectDossier_editEvalSessionProjectCommunication_cancel: 'Отказ',
        projectDossier_editEvalSessionProjectCommunication_cancelCommunication: 'Анулиране',
        projectDossier_editEvalSessionProjectCommunication_print: 'Принтирай',
        projectDossier_editEvalSessionProjectCommunication_register:
          'Регистриране на отговор с код',
        projectDossier_editEvalSessionProjectCommunication_apply: 'Приеми отговор',
        projectDossier_editEvalSessionProjectCommunication_reject: 'Отхвърли отговор',
        projectDossier_editEvalSessionProjectCommunication_questionTemplate: 'Въпрос',
        projectDossier_editEvalSessionProjectCommunication_answerTemplate: 'Отговор',

        //projectDossier_evalSessionEvaluationEdit
        projectDossier_evalSessionEvaluationEdit_title: 'Преглед на обобщена оценка',
        projectDossier_evalSessionEvaluationEdit_back: 'Назад',

        //projectDossier_evalSessionProjectStandingEdit
        projectDossier_evalSessionProjectStandingEdit_title: 'Преглед на класиране',
        projectDossier_evalSessionProjectStandingEdit_back: 'Назад',

        //projectDossier_documentsSearch
        projectDossier_documentsSearch_docTypes: 'Тип на документа',
        projectDossier_documentsSearch_objDescription: 'Документ',
        projectDossier_documentsSearch_file: 'Файл',
        projectDossier_documentsSearch_fileDescription: 'Описание',

        //projectDossier_viewContract
        projectDossier_viewContract_title: 'Преглед на договор',
        projectDossier_viewContract_contractData: 'Общи данни',
        projectDossier_viewContract_beneficiary: 'Бенефициент',
        projectDossier_viewContract_versions: 'Договор',
        projectDossier_viewContract_newAnnex: 'Изменение',
        projectDossier_viewContract_newChange: 'Промяна',
        projectDossier_viewContract_versionExcelExport: 'Експорт',
        projectDossier_viewContract_versionNum: '№ на версия',
        projectDossier_viewContract_versionType: 'Тип',
        projectDossier_viewContract_versionRegNumber: 'Рег. номер',
        projectDossier_viewContract_versionCreateNote: 'Бележки',
        projectDossier_viewContract_versionTotalBfpAmount: 'БФП',
        projectDossier_viewContract_versionContractDate: 'Дата на сключване',
        projectDossier_viewContract_versionStatus: 'Статус',
        projectDossier_viewContract_procurements:
          'Процедури за избор на изпълнител и сключени договори',
        projectDossier_viewContract_procurementExcelExport: 'Експорт',
        projectDossier_viewContract_procurementSource: 'Източник',
        projectDossier_viewContract_procurementStatus: 'Статус',
        projectDossier_viewContract_procurementCreateDate: 'Дата на създаване',
        projectDossier_viewContract_procurementModifyDate: 'Дата на промяна',
        projectDossier_viewContract_procurementCreateTitle:
          'Новa процедурa за избор на изпълнител и сключени договори',
        projectDossier_viewContract_procurementCreateNote: 'Бележка',
        projectDossier_viewContract_spendingPlans: 'Планове за разходване на средствата',
        projectDossier_viewContract_spendingPlanExcelExport: 'Експорт',
        projectDossier_viewContract_spendingPlanSource: 'Източник',
        projectDossier_viewContract_spendingPlanStatus: 'Статус',
        projectDossier_viewContract_spendingPlanCreateDate: 'Дата на създаване',
        projectDossier_viewContract_spendingPlanModifyDate: 'Дата на промяна',
        projectDossier_viewContract_spendingPlanCreateTitle: 'Нов план за разходване на средствата',
        projectDossier_viewContract_spendingPlanCreateNote: 'Бележка',
        projectDossier_viewContract_offers: 'Оферти',
        projectDossier_viewContract_offerExcelExport: 'Експорт',
        projectDossier_viewContract_offersSubmitDate: 'Дата на подаване',
        projectDossier_viewContract_offersExpectedAmount: 'Прогнозна стойност съгласно обявление',
        projectDossier_viewContract_offersName: 'Предмет на бюджетта',
        projectDossier_viewContract_communications: 'Кореспонденция',
        projectDossier_viewContract_communicationExcelExport: 'Експорт',
        projectDossier_viewContract_orderNum: 'Пореден номер',
        projectDossier_viewContract_status: 'Статус',
        projectDossier_viewContract_source: 'Изпратено от',
        projectDossier_viewContract_regNumber: 'Рег. номер',
        projectDossier_viewContract_subject: 'Тема',
        projectDossier_viewContract_sendDate: 'Дата на изпращане',
        projectDossier_viewContract_readDate: 'Дата на първо отваряне',
        projectDossier_viewContract_newContractTitle: 'Преглед на договор',
        projectDossier_viewContract_annexTitle: 'Преглед на изменение',
        projectDossier_viewContract_changeTitle: 'Преглед на промяна',
        projectDossier_viewContract_versionTemplate: 'Договор',
        projectDossier_viewContract_procurementTitle:
          'Преглед на процедура за избор на изпълнител и сключени договори',
        projectDossier_viewContract_procurementTemplate: 'Процедура за избор',
        projectDossier_viewContract_spendingPlanTitle:
          'Преглед на план за разходване на средствата',
        projectDossier_viewContract_spendingPlanTemplate: 'План за разходване на средствата',
        projectDossier_viewContract_communicationTitle: 'Преглед на кореспонденция',
        projectDossier_viewContract_communicationTemplate: 'Съобщение',

        //projectDossier_viewPaidAmounts
        projectDossier_viewPaidAmounts_title: 'Преглед на изплатени суми',
        projectDossier_viewPaidAmounts_requestedAmounts: 'Поискани суми',
        projectDossier_viewPaidAmounts_requestedAmountExcelExport: 'Експорт',
        projectDossier_viewPaidAmounts_requestedAmount: 'Искана сума',
        projectDossier_viewPaidAmounts_requestedAmountRegNum: 'Номер на договор',
        projectDossier_viewPaidAmounts_requestedAmountContractName: 'Договор',
        projectDossier_viewPaidAmounts_requestedAmountProcedureName: 'Бюджет',
        projectDossier_viewPaidAmounts_requestedAmountStatus: 'Статус',
        projectDossier_viewPaidAmounts_requestedAmountOrderNum: 'Пореден номер',
        projectDossier_viewPaidAmounts_requestedAmountReportType: 'Тип',
        projectDossier_viewPaidAmounts_requestedAmountSource: 'Въведен от',
        projectDossier_viewPaidAmounts_actuallyPaidAmounts: 'Реално изплатени суми',
        projectDossier_viewPaidAmounts_actuallyPaidAmountExcelExport: 'Експорт',
        projectDossier_viewPaidAmounts_actuallyPaidAmountProgramme: 'Програма',
        projectDossier_viewPaidAmounts_actuallyPaidAmountContractRegNumber: '№ договор',
        projectDossier_viewPaidAmounts_actuallyPaidAmountReportPaymentNum: 'Номер на ИП',
        projectDossier_viewPaidAmounts_actuallyPaidAmountPaymentType: 'Тип на ИП',
        projectDossier_viewPaidAmounts_actuallyPaidAmountFinanceSource: 'Фонд',
        projectDossier_viewPaidAmounts_actuallyPaidAmountStatus: 'Статус',
        projectDossier_viewPaidAmounts_actuallyPaidAmountRegNumber: 'Номер',
        projectDossier_viewPaidAmounts_actuallyPaidAmountPaymentReason: 'Основание за плащане',
        projectDossier_viewPaidAmounts_actuallyPaidAmountPaymentDate: 'Дата на плащане',
        projectDossier_viewPaidAmounts_actuallyPaidAmountPaidBfpTotalAmount: 'Сума',

        //projectDossier_viewDebts
        projectDossier_viewDebts_title: 'Преглед дългове',
        projectDossier_viewDebts_contractDebts: 'Дългове към договор',
        projectDossier_viewDebts_contractDebtExcelExport: 'Експорт',
        projectDossier_viewDebts_contractDebtОrderNum: 'Пореден номер',
        projectDossier_viewDebts_contractDebtContractRegNumber: 'Номер на договор',
        projectDossier_viewDebts_contractDebtRegNumber: 'Номер на дълга',
        projectDossier_viewDebts_contractDebtExecutionStatus: 'Актуален статус',
        projectDossier_viewDebts_contractDebtRegDate: 'Дата на регистрация',
        projectDossier_viewDebts_contractDebtModifyDate: 'Дата на последна актуализация',
        projectDossier_viewDebts_contractDebtTotalAmount: 'Главница Общо',
        projectDossier_viewDebts_contractDebtTotalInterestAmount: 'Лихва Общо',
        projectDossier_viewDebts_contractDebtCertReportNumber: '№ ДС',
        projectDossier_viewDebts_correctionDebts: 'Дългове по ФКСП',
        projectDossier_viewDebts_correctionDebtExcelExport: 'Експорт',
        projectDossier_viewDebts_correctionDebtОrderNum: 'Пореден номер',
        projectDossier_viewDebts_correctionDebtContractRegNumber: 'Номер на договор',
        projectDossier_viewDebts_correctionDebtRegNumber: 'Номер на дълга по ФКСП',
        projectDossier_viewDebts_correctionDebtRegDate: 'Дата на регистрация',
        projectDossier_viewDebts_correctionDebtModifyDate: 'Дата на последна актуализация',
        projectDossier_viewDebts_correctionDebtTotalAmount: 'Обща дължина сума ',
        projectDossier_viewDebts_correctionDebtCertTotalAmount: 'Обща сертифицирана сума',
        projectDossier_viewDebts_correctionDebtReimbursedTotapAmount: 'Обща възстановена сума',

        //projectDossier_viewReimbursedAmounts
        projectDossier_viewReimbursedAmounts_title: 'Преглед възстановени суми',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmounts: 'Възстановени суми по дългове',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountExcelExport: 'Експорт',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountProgramme: 'Програма',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountDebtRegNumber: '№ дълг',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountStatus: 'Статус',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountRegNumber: 'Номер',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountType: 'Вид',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountReimbursement:
          'Начин на възстановяване',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountReimbursementDate:
          'Дата на плащане',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountPrincipalEuAmount:
          'Главница-Финансиране от ЕС',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountPrincipalBgAmount:
          'Главница-Финансиране от НФ',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountPrincipalTotalAmount:
          'Главница-Общо',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountInterestEuAmount:
          'Лихва-Финансиране от ЕС',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountInterestBgAmount:
          'Лихва-Финансиране от НФ',
        projectDossier_viewReimbursedAmounts_debtReimbursedAmountInterestTotalAmount: 'Лихва-Общо',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmounts:
          'Възстановени суми по договор',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountExcelExport: 'Експорт',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountProgramme: 'Програма',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountContractRegNumber: '№ договор',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountStatus: 'Статус',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountRegNumber: 'Номер',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountType: 'Вид',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountReimbursement:
          'Начин на възстановяване',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountReimbursementDate:
          'Дата на плащане',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountPrincipalEuAmount:
          'Главница-Финансиране от ЕС',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountPrincipalBgAmount:
          'Главница-Финансиране от НФ',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountPrincipalTotalAmount:
          'Главница-Общо',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountInterestEuAmount:
          'Лихва-Финансиране от ЕС',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountInterestBgAmount:
          'Лихва-Финансиране от НФ',
        projectDossier_viewReimbursedAmounts_contractReimbursedAmountInterestTotalAmount:
          'Лихва-Общо',
        projectDossier_viewReimbursedAmounts_fiReimbursedAmounts: 'Възстановени суми по ФИ',
        projectDossier_viewReimbursedAmounts_fiReimbursedAmountExcelExport: 'Експорт',
        projectDossier_viewReimbursedAmounts_fiReimbursedAmountProgramme: 'Програма',
        projectDossier_viewReimbursedAmounts_fiReimbursedAmountContractRegNumber: '№ договор',
        projectDossier_viewReimbursedAmounts_fiReimbursedAmountStatus: 'Статус',
        projectDossier_viewReimbursedAmounts_fiReimbursedAmountRegNumber: 'Номер',
        projectDossier_viewReimbursedAmounts_fiReimbursedAmountType: 'Суми, възстановени на ФИ',
        projectDossier_viewReimbursedAmounts_fiReimbursedAmountReimbursement:
          'Начин на възстановяване',
        projectDossier_viewReimbursedAmounts_fiReimbursedAmountReimbursementDate: 'Дата на плащане',

        //projectDossier_viewFinancialCorrections
        projectDossier_viewFinancialCorrections_title: 'Преглед финансови корекции',
        projectDossier_viewFinancialCorrections_financialCorrections: 'Финансови корекции',
        projectDossier_viewFinancialCorrections_financialCorrectionExcelExport: 'Експорт',
        projectDossier_viewFinancialCorrections_financialCorrectionOrderNum: 'Пореден номер',
        projectDossier_viewFinancialCorrections_financialCorrectionContractRegNumber:
          'Номер на договор',
        projectDossier_viewFinancialCorrections_financialCorrectionImpositionDate:
          'Дата на налагане',
        projectDossier_viewFinancialCorrections_financialCorrectionContractContract:
          'Договор с изпълнител',
        projectDossier_viewFinancialCorrections_financialCorrectionContractContractNumber: 'Номер',
        projectDossier_viewFinancialCorrections_financialCorrectionContractContractorCompany:
          'Изпълнител',
        projectDossier_viewFinancialCorrections_financialCorrectionImposingReason:
          'Основание за налагане',
        projectDossier_viewFinancialCorrections_financialCorrectionPercent:
          '% на наложената финансова корекция',
        projectDossier_viewFinancialCorrections_financialCorrectionTotalAmount:
          'Стойност на наложената финансова корекция - Общо',
        projectDossier_viewFinancialCorrections_flatFinancialCorrections:
          'Финансови корекции за СП',
        projectDossier_viewFinancialCorrections_flatFinancialCorrectionExcelExport: 'Експорт',
        projectDossier_viewFinancialCorrections_flatFinancialCorrectionOrderNum: 'Пореден номер',
        projectDossier_viewFinancialCorrections_flatFinancialCorrectionContractRegNumber:
          'Номер на договор',
        projectDossier_viewFinancialCorrections_flatFinancialCorrectionName: 'Наименование',
        projectDossier_viewFinancialCorrections_flatFinancialCorrectionLevel: 'Ниво',
        projectDossier_viewFinancialCorrections_flatFinancialCorrectionType: 'Тип',
        projectDossier_viewFinancialCorrections_flatFinancialCorrectionStatus: 'Статус',
        projectDossier_viewFinancialCorrections_flatFinancialCorrectionImpositionDate:
          'Дата на налагане',
        projectDossier_viewFinancialCorrections_flatFinancialCorrectionImpositionNumber:
          'Номер на решението за налагане',

        //projectDossier_viewApprovedAmounts
        projectDossier_viewApprovedAmounts_title: 'Преглед верифицирани средства',
        projectDossier_viewApprovedAmounts_contractReports: 'Пакети отчетни документи',
        projectDossier_viewApprovedAmounts_contractReportExcelExport: 'Експорт',
        projectDossier_viewApprovedAmounts_contractReportApprovedAmount: 'Верифицирана сума',
        projectDossier_viewApprovedAmounts_contractReportContractRegNum: 'Номер на договор',
        projectDossier_viewApprovedAmounts_contractReportContractName: 'Договор',
        projectDossier_viewApprovedAmounts_contractReportProcedureName: 'Бюджет',
        projectDossier_viewApprovedAmounts_contractReportStatus: 'Статус',
        projectDossier_viewApprovedAmounts_contractReportOrderNum: 'Пореден номер',
        projectDossier_viewApprovedAmounts_contractReportReportType: 'Тип',
        projectDossier_viewApprovedAmounts_contractReportSource: 'Въведен от',
        projectDossier_viewApprovedAmounts_contractReportCorrections:
          'Коригиране на верифицирани суми на други нива',
        projectDossier_viewApprovedAmounts_contractReportCorrectionExcelExport: 'Експорт',
        projectDossier_viewApprovedAmounts_contractReportCorrectionApprovedBfpTotalAmount:
          'Коригирана одобрена сума-БФП',
        projectDossier_viewApprovedAmounts_contractReportCorrectionApprovedSelfAmount:
          'Коригирана одобрена сума-СФ',
        projectDossier_viewApprovedAmounts_contractReportCorrectionProgramme: 'Програма',
        projectDossier_viewApprovedAmounts_contractReportCorrectionStatus: 'Статус',
        projectDossier_viewApprovedAmounts_contractReportCorrectionRegNumber: 'Номер',
        projectDossier_viewApprovedAmounts_contractReportCorrectionType: 'Вид',
        projectDossier_viewApprovedAmounts_contractReportCorrectionDate: 'Дата',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrections:
          'Коригиране на верифицирани суми на ниво РОД',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionExcelExport: 'Експорт',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionApprovedBfpTotalAmount:
          'Коригирана одобрена сума-БФП',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionApprovedSelfAmount:
          'Коригирана одобрена сума-СФ',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionContractRegNum:
          'Номер на договор',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionContractName: 'Договор',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionProcedureName: 'Бюджет',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionReportOrderNum:
          'Номер на пакет',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionStatus: 'Статус',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionOrderNum:
          'Пореден номер',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionCreateDate:
          'Дата на създаване',
        projectDossier_viewApprovedAmounts_contractReportFinancialCorrectionNotes: 'Бележки',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSD: 'РОД',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSDExcelExport: 'Експорт',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSDContractRegNum:
          'Номер на договор',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSDcsd:
          'Разходооправдателен документ',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSDReportNum:
          'Номер и дата на пакет',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSDPaymentDate:
          'Номер и дата на ИП',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSDAmount: 'Размер на РОД',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSDApprovedAmount:
          'Одобрен размер на РОД',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSDCorrectedApprovedAmount:
          'Корекция на РОД',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSDBfpAmount: 'БФП',
        projectDossier_viewApprovedAmounts_contractReportFinancialCSDTotalAmount: 'Общо',

        //projectDossier_viewCertifiedAmounts
        projectDossier_viewCertifiedAmounts_title: 'Преглед сертифицирани средства',
        projectDossier_viewCertifiedAmounts_contractReports: 'Пакети отчетни документи',
        projectDossier_viewCertifiedAmounts_contractReportExcelExport: 'Експорт',
        projectDossier_viewCertifiedAmounts_contractReportCertifiedAmount: 'Сертифицирана сума',
        projectDossier_viewCertifiedAmounts_contractReportContractRegNum: 'Номер на договор',
        projectDossier_viewCertifiedAmounts_contractReportContractName: 'Договор',
        projectDossier_viewCertifiedAmounts_contractReportProcedureName: 'Бюджет',
        projectDossier_viewCertifiedAmounts_contractReportStatus: 'Статус',
        projectDossier_viewCertifiedAmounts_contractReportOrderNum: 'Пореден номер',
        projectDossier_viewCertifiedAmounts_contractReportReportType: 'Тип',
        projectDossier_viewCertifiedAmounts_contractReportSource: 'Въведен от',
        projectDossier_viewCertifiedAmounts_contractReportRevalidations:
          'Препотвърждаване на верифицирани суми на други нива',
        projectDossier_viewCertifiedAmounts_contractReportRevalidationExcelExport: 'Експорт',
        projectDossier_viewCertifiedAmounts_contractReportRevalidationBfpTotalAmount:
          'Преп. сума БФП',
        projectDossier_viewCertifiedAmounts_contractReportRevalidationSelfAmount: 'Преп. сума СФ',
        projectDossier_viewCertifiedAmounts_contractReportRevalidationProgramme: 'Програма',
        projectDossier_viewCertifiedAmounts_contractReportRevalidationStatus: 'Статус',
        projectDossier_viewCertifiedAmounts_contractReportRevalidationRegNumber: 'Номер',
        projectDossier_viewCertifiedAmounts_contractReportRevalidationType: 'Вид',
        projectDossier_viewCertifiedAmounts_contractReportRevalidationDate: 'Дата',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidations:
          'Препотвърждаване на верифицирани суми на ниво РОД',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationExcelExport:
          'Експорт',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationTotalRevalidatedBfpTotalAmount:
          'Преп. сума БФП',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationTotalRevalidatedSelfAmount:
          'Преп. сума СФ',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationContractRegNum:
          'Номер на договор',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationContractName:
          'Договор',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationProcedureName:
          'Бюджет',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationReportOrderNum:
          'Номер на пакет',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationStatus: 'Статус',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationOrderNum:
          'Пореден номер',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationCreateDate:
          'Дата на създаване',
        projectDossier_viewCertifiedAmounts_contractReportFinancialRevalidationNotes: 'Бележки',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCorrections:
          'Коригиране на верифицирани суми на ниво РОД',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCorrectionExcelExport: 'Експорт',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCorrectionContractRegNum:
          'Номер на договор',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCorrectionReportOrderNum:
          'Номер на пакет',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCorrectionOrderNum:
          'Пореден номер',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCorrectionCertifiedCorrectedApprovedBfpTotalAmount:
          'Сертифицирана коригирана сума-БФП',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCorrectionCertifiedCorrectedApprovedSelfAmount:
          'Сертифицирана коригирана сума-СФ',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCorrectionCertReportNumber:
          '№ на ДС',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCorrectionNotes: 'Бележки',
        projectDossier_viewCertifiedAmounts_contractReportCorrections:
          'Коригиране на верифицирани суми на други нива',
        projectDossier_viewCertifiedAmounts_contractReportCorrectionExcelExport: 'Експорт',
        projectDossier_viewCertifiedAmounts_contractReportCorrectionContractRegNum:
          'Номер на договор',
        projectDossier_viewCertifiedAmounts_contractReportCorrectionReportOrderNum:
          'Номер на пакет',
        projectDossier_viewCertifiedAmounts_contractReportCorrectionRegNumber: 'Пореден номер',
        projectDossier_viewCertifiedAmounts_contractReportCorrectionCertifiedCorrectedApprovedBfpTotalAmount:
          'Сертифицирана коригирана сума-БФП',
        projectDossier_viewCertifiedAmounts_contractReportCorrectionCertifiedCorrectedApprovedSelfAmount:
          'Сертифицирана коригирана сума-СФ',
        projectDossier_viewCertifiedAmounts_contractReportCorrectionCertReportNumber: '№ на ДС',
        projectDossier_viewCertifiedAmounts_contractReportCorrectionDescription: 'Бележки',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityFinancialCorrections:
          'Корекции(СС) на ниво РОД',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityFinancialCorrectionExcelExport:
          'Експорт',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityFinancialCorrectionContractRegNum:
          'Номер на договор',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityFinancialCorrectionReportOrderNum:
          'Номер на пакет',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityFinancialCorrectionCorrectionOrderNum:
          'Пореден номер',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityFinancialCorrectionCertifiedApprovedBfpTotalAmount:
          'Коригирана сертифицирана сума-БФП',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityFinancialCorrectionCertifiedApprovedSelfAmount:
          'Коригирана сертифицирана сума-СФ',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityFinancialCorrectionAnnualAccountReportOrderNum:
          '№ на ГСО',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityFinancialCorrectionNotes:
          'Бележки',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityCorrections: 'Корекции(СС)',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityCorrectionExcelExport:
          'Експорт',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityCorrectionContractRegNum:
          'Номер на договор',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityCorrectionReportOrderNum:
          'Номер на пакет',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityCorrectionRegNumber:
          'Пореден номер',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityCorrectionCertifiedBfpTotalAmount:
          'Коригирана сертифицирана сума-БФП',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityCorrectionCertifiedSelfAmount:
          'Коригирана сертифицирана сума-СФ',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityCorrectionAnnualAccountReportOrderNum:
          '№ на ГСО',
        projectDossier_viewCertifiedAmounts_contractReportCertAuthorityCorrectionDescription:
          'Бележки',
        projectDossier_viewCertifiedAmounts_contractReportCertCorrections:
          'Изравняване на сертифицирани суми на други нива',
        projectDossier_viewCertifiedAmounts_contractReportCertCorrectionExcelExport: 'Експорт',
        projectDossier_viewCertifiedAmounts_contractReportCertCorrectionBfpTotalAmount: 'Кор. БФП',
        projectDossier_viewCertifiedAmounts_contractReportCertCorrectionSelfAmount: 'Кор. СФ',
        projectDossier_viewCertifiedAmounts_contractReportCertCorrectionProgramme: 'Програма',
        projectDossier_viewCertifiedAmounts_contractReportCertCorrectionRegNumber: 'Номер',
        projectDossier_viewCertifiedAmounts_contractReportCertCorrectionStatus: 'Статус',
        projectDossier_viewCertifiedAmounts_contractReportCertCorrectionType: 'Вид',
        projectDossier_viewCertifiedAmounts_contractReportCertCorrectionDate: 'Дата',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCertCorrections:
          'Изравняване на сертифицирани суми на ниво РОД',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCertCorrectionExcelExport:
          'Експорт',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCertCorrectionContractRegNum:
          'Номер на договор',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCertCorrectionContractName:
          'Договор',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCertCorrectionProcedureName:
          'Бюджет',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCertCorrectionReportOrderNum:
          'Номер на пакет',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCertCorrectionStatus: 'Статус',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCertCorrectionOrderNum:
          'Пореден номер',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCertCorrectionCreateDate:
          'Дата на създаване',
        projectDossier_viewCertifiedAmounts_contractReportFinancialCertCorrectionNotes: 'Бележки',

        //projectDossier_viewPhysicalExecution
        projectDossier_viewPhysicalExecution_title: 'Преглед физическо изпълнение',
        projectDossier_viewPhysicalExecution_activities: 'Дейности по проекта',
        projectDossier_viewPhysicalExecution_activityExcelExport: 'Експорт',
        projectDossier_viewPhysicalExecution_activityContractRegNum: 'Номер на договор',
        projectDossier_viewPhysicalExecution_activityName: 'Дейност',
        projectDossier_viewPhysicalExecution_activityStatusDesc: 'Статус',
        projectDossier_viewPhysicalExecution_activityStartDate: 'Актуална начална дата',
        projectDossier_viewPhysicalExecution_activityEndDate: 'Актуална крайна дата',
        projectDossier_viewPhysicalExecution_activityAmount: 'Договорена стойност',
        projectDossier_viewPhysicalExecution_activityTotalAmount: 'Отчетена стойност',
        projectDossier_viewPhysicalExecution_indicators: 'Индикатори',
        projectDossier_viewPhysicalExecution_indicatorExcelExport: 'Експорт',
        projectDossier_viewPhysicalExecution_indicatorContractRegNum: 'Номер на договор',
        projectDossier_viewPhysicalExecution_indicatorName: 'Индикатор',
        projectDossier_viewPhysicalExecution_indicatorMeasureName: 'Мерна единица',
        projectDossier_viewPhysicalExecution_indicatorBaseTotal: 'Базова стойност',
        projectDossier_viewPhysicalExecution_indicatorTargetTotal: 'Целева стойност',
        projectDossier_viewPhysicalExecution_indicatorCumulativeAmount: 'Отчетена',
        projectDossier_viewPhysicalExecution_indicatorApprovedCumulativeAmount: 'Одобрена',
        projectDossier_viewPhysicalExecution_indicatorCorrectedApprovedCumulativeAmountTotal:
          'Коригирана одобрена стойност',

        //projectDossier_contractReportsSearch
        projectDossier_contractReportsSearch_contractRegNum: 'Номер на договор',
        projectDossier_contractReportsSearch_contractName: 'Договор',
        projectDossier_contractReportsSearch_procedureName: 'Бюджет',
        projectDossier_contractReportsSearch_orderNum: 'Пореден номер',
        projectDossier_contractReportsSearch_status: 'Статус',
        projectDossier_contractReportsSearch_source: 'Въведен от',
        projectDossier_contractReportsSearch_reportType: 'Тип',

        //projectDossier_viewSpotChecks
        projectDossier_viewSpotChecks_title: 'Преглед проверки на място',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotChecks:
          'Проверки на място – вътрешна среда',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckExcelExport: 'Експорт',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckProgrammeName: 'Програма',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckContractRegNum:
          'Номер на договор',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckRegNumber: '№ проверка',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckStatus: 'Статус',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckDateFrom: 'Период от',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckDateTo: 'Период до',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckType: 'Вид',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckAscertainments: 'Констатации',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckRecommendations: 'Препоръки',
        projectDossier_viewSpotChecks_internalЕnvironmentSpotCheckRecommendationExecutionStatuses:
          'Статус на препоръките',
        projectDossier_viewSpotChecks_technicalReportSpotChecks:
          'Проверки на място – технически отчет',
        projectDossier_viewSpotChecks_technicalReportSpotCheckExcelExport: 'Експорт',
        projectDossier_viewSpotChecks_technicalReportSpotCheckVersionNum: 'Номер на ТО',
        projectDossier_viewSpotChecks_technicalReportSpotCheckNumber: 'Номер',
        projectDossier_viewSpotChecks_technicalReportSpotCheckSubject:
          'Проверяван изпълнител/обект',
        projectDossier_viewSpotChecks_technicalReportSpotCheckStartDate:
          'Начална дата на изпълнение',
        projectDossier_viewSpotChecks_technicalReportSpotCheckEndDate: 'Крайна дата на изпълнение',
        projectDossier_viewSpotChecks_technicalReportSpotCheckPreview: 'Преглед',

        //projectDossier_viewIrregularitiesAndSignals
        projectDossier_viewIrregularitiesAndSignals_title: 'Преглед нередности и сигнали',
        projectDossier_viewIrregularitiesAndSignals_irregularities: 'Нередности',
        projectDossier_viewIrregularitiesAndSignals_irregularitieExcelExport: 'Експорт',
        projectDossier_viewIrregularitiesAndSignals_irregularitieSignalNum: '№ сигнал',
        projectDossier_viewIrregularitiesAndSignals_irregularitieProgramme: 'Програма',
        projectDossier_viewIrregularitiesAndSignals_irregularitieContractRegNumber: '№ договор',
        projectDossier_viewIrregularitiesAndSignals_irregularitieRegNumber: 'Национален номер',
        projectDossier_viewIrregularitiesAndSignals_irregularitieCompany: 'Бенефициент',
        projectDossier_viewIrregularitiesAndSignals_irregularitySignals: 'Сигнали за нередности',
        projectDossier_viewIrregularitiesAndSignals_irregularitySignalExcelExport: 'Експорт',
        projectDossier_viewIrregularitiesAndSignals_irregularitySignalProgramme: 'Програма',
        projectDossier_viewIrregularitiesAndSignals_irregularitySignalContractRegNumber:
          '№ договор/ПП',
        projectDossier_viewIrregularitiesAndSignals_irregularitySignalStatus: 'Статус',
        projectDossier_viewIrregularitiesAndSignals_irregularitySignalIsIrregularityFound:
          'Установена нередност',

        //projectDossier_viewAudits
        projectDossier_viewAudits_title: 'Преглед одити',
        projectDossier_viewAudits_internalЕnvironmentAudits: 'Одити – вътрешна среда',
        projectDossier_viewAudits_internalЕnvironmentAuditExcelExport: 'Експорт',
        projectDossier_viewAudits_internalЕnvironmentAuditProgrammeName: 'Програма',
        projectDossier_viewAudits_internalЕnvironmentAuditContractRegNum: 'Номер на договор',
        projectDossier_viewAudits_internalЕnvironmentAuditAuditInstitution:
          'Институция, извършваща одита',
        projectDossier_viewAudits_internalЕnvironmentAuditType: 'Тип',
        projectDossier_viewAudits_internalЕnvironmentAuditKind: 'Вид',
        projectDossier_viewAudits_internalЕnvironmentAuditLevel: 'Ниво',
        projectDossier_viewAudits_internalЕnvironmentAuditAscertainmentOrderNum: 'Констатации',
        projectDossier_viewAudits_internalЕnvironmentAuditIsRecommendationsFulfilled:
          'Изпълнени ли са препоръките',
        projectDossier_viewAudits_internalЕnvironmentAuditIsFinancial: 'Финансово изражение',
        projectDossier_viewAudits_technicalReportAudits: 'Одити – технически отчет',
        projectDossier_viewAudits_technicalReportAuditExcelExport: 'Експорт',
        projectDossier_viewAudits_technicalReportAuditVersionNum: 'Номер на ТО',
        projectDossier_viewAudits_technicalReportAuditInstitutionName: 'Институция',
        projectDossier_viewAudits_technicalReportAuditTypeDesc: 'Тип',
        projectDossier_viewAudits_technicalReportAuditKindDesc: 'Вид',
        projectDossier_viewAudits_technicalReportAuditFinalReportNumber:
          'Номер на окончателния доклад',
        projectDossier_viewAudits_technicalReportAuditFinalReportDate:
          'Дата на окончателния доклад',
        projectDossier_viewAudits_technicalReportAuditPreview: 'Преглед',

        //projectDossier_offersEdit
        projectDossier_offersEdit_title: 'Преглед на оферта към процедура за избор на изпълнител',
        projectDossier_offersEdit_template: 'Оферта',

        //prognoses_search
        prognoses_search_year: 'Година',
        prognoses_search_month: 'Месец',

        prognoses_search_search: 'Търси',
        prognoses_search_new: 'Нова прогноза',
        prognoses_search_programme: 'Програма',
        prognoses_search_programmePriority: 'Разпоредител с бюджетни средства',
        prognoses_search_procedure: 'Бюджет',
        prognoses_search_status: 'Статус',
        prognoses_search_contractedAmount: 'Прогноза за договаряне',
        prognoses_search_paymentAmount: 'Прогноза за плащане',
        prognoses_search_advancePaymentAmount: 'Прогноза за авансово плащане',
        prognoses_search_advanceVerPaymentAmount:
          'Прогноза за авансово плащане, подлежащо на верификация',
        prognoses_search_intermediatePaymentAmount: 'Прогноза за междинни плащания',
        prognoses_search_finalPaymentAmount: 'Прогноза за окончателни плащания',
        prognoses_search_approvedAmount: 'Прогноза за верифициране',
        prognoses_search_certifiedAmount: 'Прогноза за сертифициране',

        //prognoses_dataForm
        prognoses_dataForm_programme: 'Програма',
        prognoses_dataForm_programmePriority: 'Разпоредител с бюджетни средства',
        prognoses_dataForm_procedure: 'Бюджет',
        prognoses_dataForm_status: 'Статус',
        prognoses_dataForm_deleteNote: 'Причина за анулиране',
        prognoses_dataForm_createDate: 'Дата на създаване',
        prognoses_dataForm_modifyDate: 'Дата на последна промяна',
        prognoses_dataForm_year: 'Година',
        prognoses_dataForm_month: 'Месец',

        prognoses_dataForm_contracted: 'Прогноза за договарян',
        prognoses_dataForm_payment: 'Прогноза за плащане',
        prognoses_dataForm_advancePayment: 'Прогноза за авансово плащане',
        prognoses_dataForm_advanceVerPayment:
          'Прогноза за авансово плащане, подлежащо на верификация',
        prognoses_dataForm_intermediatePayment: 'Прогноза за междинни плащания',
        prognoses_dataForm_finalPayment: 'Прогноза за окончателни плащания',
        prognoses_dataForm_approved: 'Прогноза за верифициране',
        prognoses_dataForm_certified: 'Прогноза за сертифициране',
        prognoses_dataForm_euAmount: 'Финансиране от ЕС',
        prognoses_dataForm_bgAmount: 'Финансиране от НФ',
        prognoses_dataForm_bfpAmount: 'БФП',

        //prognoses_new
        prognoses_new_programmePrognosisTitle: 'Нова прогноза на ниво програма',
        prognoses_new_programmePriorityPrognosisTitle:
          'Нова прогноза на ниво разпоредител с бюджетни средства',
        prognoses_new_procedurePrognosisTitle: 'Нова прогноза на ниво бюджет',
        prognoses_new_save: 'Запис',
        prognoses_new_cancel: 'Отказ',
        prognoses_new_programme: 'Програма',
        prognoses_new_programmePriority: 'Разпоредител с бюджетни средства',
        prognoses_new_procedure: 'Бюджет',
        prognoses_new_year: 'Година',
        prognoses_new_month: 'Месец',

        //prognoses_edit
        prognoses_edit_programmePrognosisTitle: 'Преглед на прогноза на ниво програма',
        prognoses_edit_programmePriorityPrognosisTitle:
          'Преглед на прогноза на ниво разпоредител с бюджетни средства',
        prognoses_edit_procedurePrognosisTitle: 'Преглед на прогноза на ниво бюджет',
        prognoses_edit_edit: 'Редакция',
        prognoses_edit_draft: 'Чернова',
        prognoses_edit_draftConfirm: 'Сигурни ли сте че искате да върнете записа в чернова?',
        prognoses_edit_enter: 'Въведена',
        prognoses_edit_enterConfirm: 'Сигурни ли сте че искате да въведете записа?',
        prognoses_edit_del: 'Изтриване',
        prognoses_edit_remove: 'Анулиране',
        prognoses_edit_removeNote: 'Причина за анулиране',
        prognoses_edit_removeConfirm: 'Сигурни ли сте че искате да анулирате записа?',
        prognoses_edit_save: 'Запис',
        prognoses_edit_cancel: 'Отказ',

        //prognoses_yearlyReport
        prognoses_yearlyReport_programme: 'Програма',
        prognoses_yearlyReport_years: 'Години',
        prognoses_yearlyReport_search: 'Търси',
        prognoses_yearlyReport_excelExport: 'Експорт',
        prognoses_yearlyReport_advancePayments: 'аванси',
        prognoses_yearlyReport_advanceVerPayments: 'аванси, подлежащи на верификация',
        prognoses_yearlyReport_intermediatePayments: 'междинни',
        prognoses_yearlyReport_finalPayments: 'окончателни',

        //prognoses_monthlyReport
        prognoses_monthlyReport_programme: 'Програма',
        prognoses_monthlyReport_year: 'Година',
        prognoses_monthlyReport_months: 'Месеци',
        prognoses_monthlyReport_search: 'Търси',
        prognoses_monthlyReport_excelExport: 'Експорт',
        prognoses_monthlyReport_advancePayments: 'аванси',
        prognoses_monthlyReport_advanceVerPayments: 'аванси, подлежащи на верификация',
        prognoses_monthlyReport_intermediatePayments: 'междинни',
        prognoses_monthlyReport_finalPayments: 'окончателни',

        //prognoses_programmePriorityReport
        prognoses_programmePriorityReport_programme: 'Програма',
        prognoses_programmePriorityReport_programmePriority: 'Разпоредител с бюджетни средства',
        prognoses_programmePriorityReport_search: 'Търси',
        prognoses_programmePriorityReport_excelExport: 'Експорт',
        prognoses_programmePriorityReport_prognosedContracted: 'Прогноза за договаряне',
        prognoses_programmePriorityReport_contracted: 'Договаряне',
        prognoses_programmePriorityReport_prognosedApproved: 'Прогноза за верифициране',
        prognoses_programmePriorityReport_approved: 'Верифицирани',
        prognoses_programmePriorityReport_prognosedCertified: 'Прогноза за сертификация',
        prognoses_programmePriorityReport_certified: 'Сертифицирани',

        //prognoses_programmeReport
        prognoses_programmeReport_programme: 'Програма',
        prognoses_programmeReport_search: 'Търси',
        prognoses_programmeReport_excelExport: 'Експорт',
        prognoses_programmeReport_prognosedContracted: 'Прогноза за договаряне',
        prognoses_programmeReport_contracted: 'Договаряне',
        prognoses_programmeReport_prognosedApproved: 'Прогноза за верифициране',
        prognoses_programmeReport_approved: 'Верифицирани',
        prognoses_programmeReport_prognosedCertified: 'Прогноза за сертификация',
        prognoses_programmeReport_certified: 'Сертифицирани',

        //prognoses_summaryReport
        prognoses_summaryReport_excelExport: 'Експорт',
        prognoses_summaryReport_procedure: 'Бюджет',
        prognoses_summaryReport_budgetTotal: 'Общ бюджет за бюджетта',
        prognoses_summaryReport_approvedProjectsBudget: 'Общ бюджет на одобрените предложения',
        prognoses_summaryReport_contractsBudget: 'Общ бюджет на сключените договори',
        prognoses_summaryReport_prognosedContractedAmounts: 'Прогноза за договаряне',
        prognoses_summaryReport_paymentAmounts: 'Обща сума на исканията за плащане',
        prognoses_summaryReport_approvedAmounts: 'Верифицирани – изпълнение',
        prognoses_summaryReport_paidAmounts: 'Обща сума на реално изплатените суми',
        prognoses_summaryReport_prognosedApprovedAmounts: 'Прогноза за верифициране ',
        prognoses_summaryReport_certifiedAmounts: 'Сертифицирани – изпълнение',
        prognoses_summaryReport_prognosedCertifiedAmounts: 'Прогноза за сертификация',

        //defaultErrorTexts
        defaultErrorTexts_required: 'Задължително поле',
        defaultErrorTexts_pattern: 'Невалиден формат',
        defaultErrorTexts_minlength: 'Нарушена минимална дължина на полето',
        defaultErrorTexts_maxlength: 'Нарушена максимална дължина на полето',
        defaultErrorTexts_min: 'Нарушена минимална стойност на полето',
        defaultErrorTexts_max: 'Нарушена максимална стойност на полето',
        defaultErrorTexts_unique: 'Полето не е уникално',
        defaultErrorTexts_positive: 'Стойността на полето трябва да е по-голяма от 0',
        defaultErrorTexts_eumisMaxBlobSize: 'Размерът на файла надхвърля максималната големина',

        //errorTexts
        errorTexts_notUniqueUin: 'Въведеният Булстат/ЕИК/ЕГН вече съществува в системата',
        errorTexts_notValidUin: 'Въведеният Булстат/ЕИК/ЕГН не е валиден',
        errorTexts_notUniquePersonalUin: 'Въведеното ЕГН вече съществува в системата',
        errorTexts_notValidPersonalUin: 'Въведеното ЕГН не е валидно',
        errorTexts_permissionTemplateNameExist: 'Съществува шаблон за група с такова име',
        errorTexts_usereRequestNameExist: 'Съществува заявка за права с такова име',

        states: {
          'root.users': 'Потребителски профили',
          'root.users.new': 'Нов потребителски профил',
          'root.users.view.edit': 'Регистрационни данни',
          'root.users.view.permissions': 'Права',
          'root.users.view.requests': 'История на промените',

          'root.pTemplates': 'Шаблони за групи',
          'root.pTemplates.new': 'Нов шаблон за група',
          'root.pTemplates.edit': 'Редакция на шаблон за група',

          'root.userTypes': 'Групи потребители',
          'root.userTypes.new': 'Нова група потребители',
          'root.userTypes.edit': 'Редакция на група потребители',

          'root.requestPackages': 'Пакети за актуализация',
          'root.requestPackages.new': 'Нов пакет',
          'root.requestPackages.view': 'Данни за пакет',
          'root.requestPackages.view.users': 'Потребители',

          'root.internalActionLogs': 'Лог на действията (Вътрешна система)',
          'root.portalActionLogs': 'Лог на действията (Портал)',
          'root.loginActionLogs': 'Лог на действията (Неуспешен вход)',
          'root.procedureActionLogs': 'Лог на действията (Бюджети)',
          'root.procedureActionLogs.view': 'Преглед',

          'root.map': 'Основна структура',
          'root.map.programmes.new': 'Нова Основна организация',
          'root.map.programmes.view': 'Основна организация',

          'root.map.programmes.view.budgets': 'Финансиране по години',
          'root.map.programmes.view.budgets.new': 'Ново финансиране за година',
          'root.map.programmes.view.budgets.edit': 'Редакция на финансиране за година',

          'root.map.programmes.view.documents': 'Документи',
          'root.map.programmes.view.documents.new': 'Нов документ',
          'root.map.programmes.view.documents.edit': 'Редакция на документ',

          'root.map.programmes.view.programmePriorities': 'Разпоредители с бюджетни средства',
          'root.map.programmes.view.programmePriorities.new':
            'Нов разпоредител с бюджетни средства',

          'root.map.programmes.view.directions': 'Направления',
          'root.map.programmes.view.directions.new': 'Ново направление',
          'root.map.programmes.view.directions.edit': 'Редакция на направление',

          'root.map.programmes.view.declarations.new': 'Създаване на декларация',
          'root.map.programmes.view.declarations.edit': 'Редакция на декларация',

          'root.map.programmes.view.declarations.edit.items.new': 'Създаване на нов ред',
          'root.map.programmes.view.declarations.edit.items.edit': 'Редакция на ред',

          'root.map.ppriorities.view': 'Разпоредител с бюджетни средства',

          'root.map.ppriorities.view.indicators': 'Индикатори',
          'root.map.ppriorities.view.indicators.new': 'Нов индикатор',
          'root.map.ppriorities.view.indicators.edit': 'Редакция на индикатор',
          'root.map.ppriorities.view.indicators.attach': 'Присъединяване на индикатор',

          'root.map.ppriorities.view.directions': 'Направления',
          'root.map.ppriorities.view.directions.new': 'Ново направление',
          'root.map.ppriorities.view.directions.edit': 'Редакция на направление',

          'root.map.ppriorities.view.budgets': 'Финансиране',
          'root.map.ppriorities.view.budgets.new': 'Ново финансиране',
          'root.map.ppriorities.view.budgets.edit': 'Редакция на финансиране',

          'root.map.ppriorities.view.documents': 'Документи',
          'root.map.ppriorities.view.documents.new': 'Нов документ',
          'root.map.ppriorities.view.documents.edit': 'Редакция на документ',

          'root.map.ppriorities.view.ipriorities': 'Инвестиционни приоритети',
          'root.map.ppriorities.view.ipriorities.new': 'Нов инвестиционен приоритет',

          'root.map.measures': 'Мерни единици',
          'root.map.measures.new': 'Нова мерна единица',
          'root.map.measures.edit': 'Редакция на мерна единица',

          'root.map.indicators': 'Индикатори',
          'root.map.indicators.edit': 'Редакция на индикатор',

          'root.map.indicatorTypes': 'Видове индикатори',
          'root.map.indicatorTypes.edit': 'Редакция на вид индикатор',
          'root.map.indicatorTypes.new': 'Нов вид индикатор',

          'root.map.expenseTypes': 'Типове разходи',
          'root.map.expenseTypes.new': 'Нов тип разход',
          'root.map.expenseTypes.edit': 'Редакция на тип разход',

          'root.map.allowances': 'Надбавки',
          'root.map.allowances.new': 'Нова надбавка',
          'root.map.allowances.edit': 'Редакция на надбавка',

          'root.map.basicInterestRates': 'Осн. лихвени проценти',
          'root.map.basicInterestRates.new': 'Нов процент',
          'root.map.basicInterestRates.edit': 'Редакция на процент',

          'root.map.interestSchemes': 'Схеми за олихвяване',
          'root.map.interestSchemes.new': 'Нова схема',
          'root.map.interestSchemes.edit': 'Редакция на схема',

          'root.map.checkBlankTopics': 'Теми за формуляр за провеждане на проверки на място',
          'root.map.checkBlankTopics.new': 'Нова тема',
          'root.map.checkBlankTopics.edit': 'Редакция на тема',

          'root.map.declarations': 'Декларации',
          'root.map.declarations.new': 'Нова декларация',
          'root.map.declarations.edit': 'Редакция на декларация',

          'root.map.directions': 'Направления',
          'root.map.directions.view.edit': 'Редакция на направление',
          'root.map.directions.view.subDirections': 'Поднаправления',

          'root.procedures': 'Бюджети',
          'root.procedures.search': 'Търсене',
          'root.procedures.new': 'Нов бюджет',
          'root.procedures.view': 'Преглед',

          'root.procedures.view.sections': 'Приложими секции',
          'root.procedures.view.directions': 'Направления',
          'root.procedures.view.indicators': 'Индикатори',
          'root.procedures.view.indicators.new': 'Нов индивидуален индикатор',
          'root.procedures.view.indicators.edit': 'Редакция на индикатор',
          'root.procedures.view.indicators.attach': 'Присъединяване на индикатор',

          'root.procedures.view.procedureShares': 'Източници на финансиране',
          'root.procedures.view.procedureShares.new': 'Нов източник на финансиране',
          'root.procedures.view.procedureShares.edit': 'Редакция на източник на финансиране',

          'root.procedures.view.procedureTimeLimits': 'Срокове',
          'root.procedures.view.procedureTimeLimits.new': 'Нов срок',
          'root.procedures.view.procedureTimeLimits.edit': 'Редакция на срок',

          'root.procedures.view.ProcedureExpenseBudgets': 'Бюджет',

          'root.procedures.view.procedureSpecFields': 'Допълнителни полета',
          'root.procedures.view.procedureSpecFields.new': 'Ново допълнителнo поле',
          'root.procedures.view.procedureSpecFields.edit': 'Редакция на допълнително поле',

          'root.procedures.view.allDocs': 'Документи',
          'root.procedures.view.allDocs.docs.new': 'Нов документ',
          'root.procedures.view.allDocs.docs.edit': 'Редакция на документ',
          'root.procedures.view.allDocs.appGuidelines.new': 'Нова насока',
          'root.procedures.view.allDocs.appGuidelines.edit': 'Редакция на кандидатстване',
          'root.procedures.view.allDocs.appDocs.new': 'Нов документ за подаване',
          'root.procedures.view.allDocs.appDocs.edit': 'Редакция на документ за подаване',
          'root.procedures.view.allDocs.evalTables.new': 'Нова оценителна таблица',
          'root.procedures.view.allDocs.evalTables.edit': 'Редакция на оценителна таблица',
          'root.procedures.view.allDocs.questions.new': 'Нов въпрос/отговор',
          'root.procedures.view.allDocs.questions.edit': 'Редакция на въпрос/отговор',
          'root.procedures.view.allDocs.declarations.new': 'Добавяне на декларация',
          'root.procedures.view.allDocs.declarations.edit': 'Преглед на декларация',

          'root.procedureMassCommunications': 'Обща кореспонденция',
          'root.procedureMassCommunications.new': 'Нова кореспонденция',
          'root.procedureMassCommunications.view': 'Преглед',

          'root.companies': 'Организации',
          'root.companies.new': 'Нова организация',
          'root.companies.edit': 'Редакция на организация',

          'root.registrations': 'Профили за кандидатстване',
          'root.registrations.view': 'Преглед на профил за кандидатстване',

          'root.contractAccessCodes': 'Кодове за достъп към договор',
          'root.contractAccessCodes.view': 'Преглед на код за достъп към договор',

          'root.procurements': 'Централизирани обществени поръчки',
          'root.procurements.search': 'Търсене',
          'root.procurements.view.edit': 'Основни данни',
          'root.procurements.view.documents': 'Документи',
          'root.procurements.view.documents.edit': 'Редакция',
          'root.procurements.view.documents.new': 'Нов документ',
          'root.procurements.view.differentiatedPosition': 'Обособени позиции',
          'root.procurements.view.differentiatedPosition.edit': 'Редакция',
          'root.procurements.view.differentiatedPosition.new': 'Нова обособена позиция',

          'root.projects': 'Проектни предложения',
          'root.projects.newStep1a': 'Регистриране (1/3)',
          'root.projects.newStep1b': 'Регистриране (1/3)',
          'root.projects.newStep2': 'Регистриране (2/3)',
          'root.projects.newStep3': 'Регистриране (3/3)',
          'root.projects.view': 'Преглед',
          'root.projects.view.communications': 'Комуникация с организация',
          'root.projects.view.communications.edit': 'Редакция на комуникация с организация',
          'root.projects.view.communications.edit.answers.edit': 'Редакция на отговор',

          'root.projectCommunications': 'Комуникация с организация',
          'root.projectCommunications.edit': 'Преглед на комуникация с организация',
          'root.projectCommunications.edit.answers.edit': 'Преглед на отговор',

          'root.projectMassManagingAuthorityCommunications': 'Обща комуникация',
          'root.projectMassManagingAuthorityCommunications.new': 'Нова комуникация',
          'root.projectMassManagingAuthorityCommunications.view': 'Преглед на комуникация',

          'root.userOrganizations': 'Организации',
          'root.userOrganizations.new': 'Нова организация',
          'root.userOrganizations.edit': 'Редакция на организация',

          'root.evalSessions': 'Оценителни сесии',
          'root.evalSessions.search': 'Търсене',
          'root.evalSessions.new': 'Нова оценителна сесия',
          'root.evalSessions.view': 'Преглед',

          'root.evalSessions.view.users': 'Членове',
          'root.evalSessions.view.users.new': 'Нов член',
          'root.evalSessions.view.users.edit': 'Редакция на член',

          'root.evalSessions.view.projects': 'Проектни предложения',
          'root.evalSessions.view.projects.view': 'Преглед на проектно предложение',
          'root.evalSessions.view.projects.view.versions.new': 'Нова версия',
          'root.evalSessions.view.projects.view.versions.edit': 'Редакция на версия',
          'root.evalSessions.view.projects.view.communications.edit':
            'Преглед на въпрос към кандидат',
          'root.evalSessions.view.projects.view.standings.new': 'Ново класиране',
          'root.evalSessions.view.projects.view.standings.edit': 'Преглед на класиране',
          'root.evalSessions.view.projects.evaluations.new': 'Обобщаване на оценка',
          'root.evalSessions.view.projects.evaluations.edit': 'Преглед на обобщена оценка',

          'root.evalSessions.view.sheets': 'Оценителни листове',
          'root.evalSessions.view.sheets.new': 'Нов оценителен лист',
          'root.evalSessions.view.sheets.edit': 'Преглед на оценителен лист',

          'root.evalSessions.view.standpoints': 'Становища',
          'root.evalSessions.view.standpoints.new': 'Ново становище',
          'root.evalSessions.view.standpoints.edit': 'Преглед на становище',

          'root.evalSessions.view.communication': 'Комуникация',

          'root.evalSessions.view.distributions': 'Разпределения',
          'root.evalSessions.view.distributions.new': 'Ново разпределение',
          'root.evalSessions.view.distributions.edit': 'Преглед на разпределение',

          'root.evalSessions.view.standings': 'Класиране',
          'root.evalSessions.view.standings.new': 'Ново класиране',
          'root.evalSessions.view.standings.edit': 'Преглед на класиране',

          'root.evalSessions.my.view': 'Преглед',

          'root.evalSessions.my.view.sheets': 'Моите оценителни листове',
          'root.evalSessions.my.view.sheets.edit': 'Преглед на оценителен лист',
          'root.evalSessions.my.view.sheets.edit.project': 'Проектно предложение',

          'root.evalSessions.my.view.standpoints': 'Моите становища',
          'root.evalSessions.my.view.standpoints.edit': 'Преглед на становище',
          'root.evalSessions.my.view.standpoints.edit.project': 'Проектно предложение',

          'root.evalSessions.view.allDocs': 'Документи',
          'root.evalSessions.view.allDocs.docs.new': 'Нов документ',
          'root.evalSessions.view.allDocs.docs.edit': 'Редакция на документ',
          'root.evalSessions.view.allDocs.reports.new': 'Нов документ',
          'root.evalSessions.view.allDocs.reports.edit': 'Преглед на документ',

          'root.news': 'Новини',
          'root.news.new': 'Нова новина',
          'root.news.edit': 'Редакция на новина',

          'root.allNews': 'Всички новини',
          'root.allNews.view': 'Преглед на новина',

          'root.newsFeed.view': 'Преглед на новина',

          'root.guidances': 'Ръководства',
          'root.guidances.new': 'Ново ръководство',
          'root.guidances.edit': 'Редакция на ръководство',

          'root.messages.new': 'Ново съобщение',
          'root.messages.inbox': 'Входящи съобщения',
          'root.messages.inbox.view': 'Преглед на съобщение',
          'root.messages.draft': 'Чернови съобщения',
          'root.messages.draft.edit': 'Редакция на съобщение',
          'root.messages.sent': 'Изпратени съобщения',
          'root.messages.sent.view': 'Преглед на съобщение',
          'root.messages.archive': 'Архивирани съобщения',
          'root.messages.archive.view': 'Преглед на съобщение',
          'root.contractRegistrations': 'Профили за достъп към договор',
          'root.contractRegistrations.new': 'Нов профил за достъп към договор',
          'root.contractRegistrations.view': 'Преглед на профил за достъп към договор',

          'root.notificationSettings': 'Настройки за нотификация',
          'root.notificationSettings.view': 'Настройка за нотификация',
          'root.userNotifications': 'Нотификации за потребител',
          'root.userNotifications.view': 'Нотификация',

          'root.contractCommunications': 'Кореспонденция',
          'root.contractCommunications.edit': 'Преглед',

          'root.contracts': 'Договори',
          'root.contracts.newStep1': 'Създаване (1/2)',
          'root.contracts.newStep2': 'Създаване (2/2)',
          'root.contracts.view': 'Преглед',
          'root.contracts.editNew': 'Редакция',

          'root.contracts.view.amendments': 'Промени и изменения',
          'root.contracts.view.amendments.versions.new': 'Нов договор',
          'root.contracts.view.amendments.versions.edit': 'Преглед на договор',
          'root.contracts.view.amendments.procurements.new': 'Нова процедура за избор',
          'root.contracts.view.amendments.procurements.edit': 'Преглед на процедура за избор',

          'root.contracts.view.registrations': 'Профили за достъп',
          'root.contracts.view.registrations.new': 'Нов профил',
          'root.contracts.view.registrations.attach': 'Присъединяване на профил',
          'root.contracts.view.registrations.edit': 'Преглед на профил',

          'root.contracts.view.accesscodes': 'Кодове за достъп',
          'root.contracts.view.accesscodes.edit': 'Преглед на код за достъп',

          'root.contracts.view.communications': 'Кореспонденция',
          'root.contracts.view.communications.edit': 'Преглед на кореспонденция',

          'root.contractReports': 'Пакети отчетни документи',
          'root.contractReports.newStep1': 'Създаване на пакет (1/2)',
          'root.contractReports.newStep2': 'Създаване на пакет (2/2)',
          'root.contractReports.view': 'Преглед',
          'root.contractReports.view.data': 'Основни данни',
          'root.contractReports.view.documents': 'Документи',
          'root.contractReports.view.documents.editTechnical': 'Преглед на технически отчет',
          'root.contractReports.view.documents.editFinancial': 'Преглед на финансов отчет',
          'root.contractReports.view.documents.editPayment': 'Преглед на искане за плащане',
          'root.contractReports.view.documents.editMicro': 'Преглед на микроданни',

          'root.contractReportChecks': 'Мониторинг на пакети отчетни документи',
          'root.contractReportChecks.view': 'Преглед',
          'root.contractReportChecks.view.data': 'Основни данни',
          'root.contractReportChecks.view.documents': 'Документи',
          'root.contractReportChecks.view.documents.editTechnical': 'Преглед на технически отчет',
          'root.contractReportChecks.view.documents.editFinancial': 'Преглед на финансов отчет',
          'root.contractReportChecks.view.documents.editPayment': 'Преглед на искане за плащане',
          'root.contractReportChecks.view.documents.editMicro': 'Преглед на микроданни',
          'root.contractReportChecks.view.checks': 'Проверки',
          'root.contractReportChecks.view.checks.editTechnical':
            'Преглед на проверка на технически отчет',
          'root.contractReportChecks.view.checks.editFinancial':
            'Преглед на проверка на финансов отчет',
          'root.contractReportChecks.view.checks.editMicro': 'Преглед на проверка на метаданни',

          'root.contractReportChecks.view.csds': 'Верифицирани РОД',
          'root.contractReportChecks.view.csds.edit': 'Преглед на верифициран РОД',
          'root.contractReportChecks.view.paymentChecks': 'Верифицирано ИП',
          'root.contractReportChecks.view.paymentChecks.edit':
            'Преглед на верифицирано искане за плащане',
          'root.contractReportChecks.view.attachedDocs': 'Свързани документи',
          'root.contractReportChecks.view.attachedDocs.viewFinCor':
            'Преглед на корекция на верифицирана сума',
          'root.contractReportChecks.view.paymentRequests': 'Искания за плащания до момента',
          'root.contractReportChecks.view.indicators': 'Верифицирани Индикатори',
          'root.contractReportChecks.view.indicators.edit': 'Преглед на верифициран индикатор',
          'root.contractReportChecks.view.advPaymentAmounts': 'Верифициранo АП',
          'root.contractReportChecks.view.advPaymentAmounts.edit': 'Преглед на верифицирано АП',
          'root.contractReportChecks.view.advNVPaymentAmounts': 'АП',
          'root.contractReportChecks.view.advNVPaymentAmounts.edit': 'Преглед на АП',
          'root.contractReportChecks.view.sap': 'Данни за САП',

          'root.contractReportFinancialCorrections': 'Коригиране на верифицирани суми на ниво РОД',
          'root.contractReportFinancialCorrections.new': 'Нова корекция',
          'root.contractReportFinancialCorrections.view': 'Преглед',
          'root.contractReportFinancialCorrections.view.report': 'Пакет',
          'root.contractReportFinancialCorrections.view.data': 'Основни данни',
          'root.contractReportFinancialCorrections.view.csds': 'Верифицирани РОД',
          'root.contractReportFinancialCorrections.view.csds.view': 'Преглед',
          'root.contractReportFinancialCorrections.view.correctedCsds':
            'Коригирани верифицирани РОД',
          'root.contractReportFinancialCorrections.view.correctedCsds.edit': 'Преглед',

          'root.contractReportTechnicalCorrections': 'Коригиране на верифицирани индикатори',
          'root.contractReportTechnicalCorrections.new': 'Нова корекция',
          'root.contractReportTechnicalCorrections.view': 'Преглед',
          'root.contractReportTechnicalCorrections.view.report': 'Пакет',
          'root.contractReportTechnicalCorrections.view.data': 'Основни данни',
          'root.contractReportTechnicalCorrections.view.indicators': 'Верифицирани индикатори',
          'root.contractReportTechnicalCorrections.view.indicators.view': 'Преглед',
          'root.contractReportTechnicalCorrections.view.correctedIndicators':
            'Коригирани верифицирани индикатори',
          'root.contractReportTechnicalCorrections.view.correctedIndicators.edit': 'Преглед',

          'root.flatFinancialCorrections': 'Финансови корекции за системни пропуски',
          'root.flatFinancialCorrections.new': 'Нова корекция',
          'root.flatFinancialCorrections.view': 'Преглед',
          'root.flatFinancialCorrections.view.items': 'Обхват',
          'root.flatFinancialCorrections.view.programmeItem': 'Обхват',

          'root.financialCorrections': 'Финансови корекции',
          'root.financialCorrections.newStep1': 'Създаване (1/2)',
          'root.financialCorrections.newStep2': 'Създаване (2/2)',
          'root.financialCorrections.view': 'Преглед',
          'root.financialCorrections.view.versions': 'Корекция',
          'root.financialCorrections.view.versions.edit': 'Редакция',
          'root.financialCorrections.view.attachedDocs': 'Свързани документи',

          'root.contractDebts': 'Дългове към договор',
          'root.contractDebts.newStep1': 'Създаване (1/2)',
          'root.contractDebts.newStep2': 'Създаване (2/2)',
          'root.contractDebts.view': 'Преглед на дълг към договор',
          'root.contractDebts.view.versions': 'Версии',
          'root.contractDebts.view.versions.edit': 'Редакция',
          'root.contractDebtsReport': 'Книга на длъжниците',

          'root.correctionDebts': 'Дългове по ФКСП',
          'root.correctionDebts.newStep1': 'Създаване (1/2)',
          'root.correctionDebts.newStep2': 'Създаване (2/2)',
          'root.correctionDebts.view': 'Преглед на дълг по ФКСП',
          'root.correctionDebts.view.versions': 'Версии',
          'root.correctionDebts.view.versions.edit': 'Редакция',
          'root.correctionDebtsReport': 'Книга на длъжниците по ФКСП',

          'root.actuallyPaidAmounts': 'Реално изплатени суми',
          'root.actuallyPaidAmounts.newStep1': 'Създаване (1/2)',
          'root.actuallyPaidAmounts.newStep2': 'Създаване (2/2)',
          'root.actuallyPaidAmounts.view': 'Преглед на изплатена сума',
          'root.actuallyPaidAmounts.view.paidAmount': 'Изплатена сума',
          'root.actuallyPaidAmounts.view.documents': 'Документи',
          'root.actuallyPaidAmounts.view.documents.new': 'Нов документ',
          'root.actuallyPaidAmounts.view.documents.edit': 'Редакция на документ',

          'root.debtReimbursedAmounts': 'Възстановени суми по дългове',
          'root.debtReimbursedAmounts.new': 'Създаване',
          'root.debtReimbursedAmounts.view': 'Преглед на възстановена сума',
          'root.debtReimbursedAmounts.view.amount': 'Възстановена сума',

          'root.contractReimbursedAmounts': 'Възстановени суми по договор',
          'root.contractReimbursedAmounts.newStep1': 'Създаване (1/2)',
          'root.contractReimbursedAmounts.newStep2': 'Създаване (2/2)',
          'root.contractReimbursedAmounts.view': 'Преглед на възстановена сума',
          'root.contractReimbursedAmounts.view.amount': 'Възстановена сума',

          'root.fiReimbursedAmounts': 'Възстановени суми по ФИ',
          'root.fiReimbursedAmounts.newStep1': 'Създаване (1/2)',
          'root.fiReimbursedAmounts.newStep2': 'Създаване (2/2)',
          'root.fiReimbursedAmounts.view': 'Преглед на възстановена сума',
          'root.fiReimbursedAmounts.view.amount': 'Възстановена сума',

          'root.certAuthorityChecks': 'Проверки – СО',
          'root.certAuthorityChecks.new': 'Създаване',
          'root.certAuthorityChecks.view': 'Преглед на проверка',
          'root.certAuthorityChecks.view.items': 'Обхват на проверката',
          'root.certAuthorityChecks.view.ascertainments': 'Констатации',
          'root.certAuthorityChecks.view.ascertainments.new': 'Нова констатация',
          'root.certAuthorityChecks.view.ascertainments.edit': 'Преглед на констатация',
          'root.certAuthorityChecks.view.documents': 'Документи',
          'root.certAuthorityChecks.view.documents.new': 'Нов документ',
          'root.certAuthorityChecks.view.documents.edit': 'Преглед на документ',

          'root.euReimbursedAmounts': 'Възстановени от ЕК суми',
          'root.euReimbursedAmounts.new': 'Създаване',
          'root.euReimbursedAmounts.view': 'Преглед на сума',
          'root.euReimbursedAmounts.view.certReports': 'Доклади по сертификация',

          'root.contractReportCertAuthorityFinancialCorrections': 'Корекции(СС) на ниво РОД',
          'root.contractReportCertAuthorityFinancialCorrections.new': 'Нова корекция',
          'root.contractReportCertAuthorityFinancialCorrections.view': 'Преглед',
          'root.contractReportCertAuthorityFinancialCorrections.view.report': 'Пакет',
          'root.contractReportCertAuthorityFinancialCorrections.view.data': 'Основни данни',
          'root.contractReportCertAuthorityFinancialCorrections.view.csds': 'Верифицирани РОД',
          'root.contractReportCertAuthorityFinancialCorrections.view.csds.view': 'Преглед',
          'root.contractReportCertAuthorityFinancialCorrections.view.correctedCsds':
            'Коригирани сертифицирани РОД',
          'root.contractReportCertAuthorityFinancialCorrections.view.correctedCsds.edit': 'Преглед',

          'root.contractReportCertAuthorityCorrections': 'Корекции(СС)',
          'root.contractReportCertAuthorityCorrections.new': 'Създаване',
          'root.contractReportCertAuthorityCorrections.view': 'Преглед на корекция',
          'root.contractReportCertAuthorityCorrections.view.docs': 'Документи',
          'root.contractReportCertAuthorityCorrections.view.docs.new': 'Нов документ',
          'root.contractReportCertAuthorityCorrections.view.docs.edit': 'Редакция на документ',

          'root.contractReportRevalidationCertAuthorityFinancialCorrections':
            'Корекции(СС) на препотвърдени суми на ниво РОД',
          'root.contractReportRevalidationCertAuthorityFinancialCorrections.new': 'Нова корекция',
          'root.contractReportRevalidationCertAuthorityFinancialCorrections.view': 'Преглед',
          'root.contractReportRevalidationCertAuthorityFinancialCorrections.view.report': 'Пакет',
          'root.contractReportRevalidationCertAuthorityFinancialCorrections.view.data':
            'Основни данни',
          'root.contractReportRevalidationCertAuthorityFinancialCorrections.view.csds':
            'Препотвърдени РОД',
          'root.contractReportRevalidationCertAuthorityFinancialCorrections.view.csds.view':
            'Преглед',
          'root.contractReportRevalidationCertAuthorityFinancialCorrections.view.correctedCsds':
            'Коригирани сертифицирани препотвърдени РОД',
          'root.contractReportRevalidationCertAuthorityFinancialCorrections.view.correctedCsds.edit':
            'Преглед',

          'root.contractReportRevalidationCertAuthorityCorrections':
            'Корекции(СС) на препотвърдени суми',
          'root.contractReportRevalidationCertAuthorityCorrections.new': 'Създаване',
          'root.contractReportRevalidationCertAuthorityCorrections.view': 'Преглед на корекция',
          'root.contractReportRevalidationCertAuthorityCorrections.view.docs': 'Документи',
          'root.contractReportRevalidationCertAuthorityCorrections.view.docs.new': 'Нов документ',
          'root.contractReportRevalidationCertAuthorityCorrections.view.docs.edit':
            'Редакция на документ',

          'root.certAuthorityCommunications': 'Комуникация сертифициращ орган',
          'root.certAuthorityCommunications.view': 'Преглед',
          'root.certAuthorityCommunications.view.communication': 'Комуникация',
          'root.certAuthorityCommunications.view.communication.edit': 'Преглед на комуникация',

          'root.auditAuthorityCommunications': 'Комуникация одитен орган',
          'root.auditAuthorityCommunications.view': 'Преглед',
          'root.auditAuthorityCommunications.view.communication': 'Комуникация',
          'root.auditAuthorityCommunications.view.communication.edit': 'Преглед на комуникация',

          'root.contractReportCorrections': 'Коригиране на верифицирани суми на други нива',
          'root.contractReportCorrections.new': 'Създаване',
          'root.contractReportCorrections.view': 'Преглед на корекция',
          'root.contractReportCorrections.view.docs': 'Документи',
          'root.contractReportCorrections.view.docs.new': 'Нов документ',
          'root.contractReportCorrections.view.docs.edit': 'Редакция на документ',

          'root.contractReportRevalidations': 'Препотвърждаване на верифицирани суми на други нива',
          'root.contractReportRevalidations.new': 'Създаване',
          'root.contractReportRevalidations.view': 'Преглед на препотвърждаване',
          'root.contractReportRevalidations.view.docs': 'Документи',
          'root.contractReportRevalidations.view.docs.new': 'Нов документ',
          'root.contractReportRevalidations.view.docs.edit': 'Редакция на документ',

          'root.contractReportCertCorrections': 'Изравняване на сертифицирани суми на други нива',
          'root.contractReportCertCorrections.new': 'Създаване',
          'root.contractReportCertCorrections.view': 'Преглед на изравняване',
          'root.contractReportCertCorrections.view.docs': 'Документи',
          'root.contractReportCertCorrections.view.docs.new': 'Нов документ',
          'root.contractReportCertCorrections.view.docs.edit': 'Редакция на документ',

          'root.contractReportFinancialRevalidations':
            'Препотвърждаване на верифицирани суми на ниво РОД',
          'root.contractReportFinancialRevalidations.new': 'Нова корекция',
          'root.contractReportFinancialRevalidations.view': 'Преглед',
          'root.contractReportFinancialRevalidations.view.report': 'Пакет',
          'root.contractReportFinancialRevalidations.view.data': 'Основни данни',
          'root.contractReportFinancialRevalidations.view.csds': 'Верифицирани РОД',
          'root.contractReportFinancialRevalidations.view.csds.view': 'Преглед',
          'root.contractReportFinancialRevalidations.view.revalidatedCsds':
            'Препотвърдени верифицирани РОД',
          'root.contractReportFinancialRevalidations.view.revalidatedCsds.edit': 'Преглед',

          'root.contractReportFinancialCertCorrections':
            'Изравняване на сертифицирани суми на ниво РОД',
          'root.contractReportFinancialCertCorrections.new': 'Ново изравняване',
          'root.contractReportFinancialCertCorrections.view': 'Преглед',
          'root.contractReportFinancialCertCorrections.view.report': 'Пакет',
          'root.contractReportFinancialCertCorrections.view.data': 'Основни данни',
          'root.contractReportFinancialCertCorrections.view.csds': 'Верифицирани РОД',
          'root.contractReportFinancialCertCorrections.view.csds.view': 'Преглед',
          'root.contractReportFinancialCertCorrections.view.correctedCsds':
            'Коригирани сертифицирани РОД',
          'root.contractReportFinancialCertCorrections.view.correctedCsds.edit': 'Преглед',

          'root.monitoringAdvancePayments': 'Справка Аванси (чл.131)',
          'root.monitoringAnex3Report': 'Справка Анекс 3',
          'root.monitoringDoubleFunding': 'Справка Двойно финансиране',
          'root.monitoringPhysicalExecution': 'Справка Физическо изпълнение',
          'root.monitoringFinancialExecution': 'Справка Финансово изпълнение',
          'root.monitoringProjects': 'Справка Проектни предложения',
          'root.monitoringContracts': 'Справка Договори',
          'root.monitoringIndicators': 'Справка Индикатори',
          'root.monitoringContractReports': 'Справка Отчети',
          'root.monitoringBudgetLevels': 'Справка за бюджетни пера',
          'root.monitoringFinancialCorrections': 'Справка Финансови корекции',
          'root.monitoringConcludedContracts': 'Справка Сключени договори',
          'root.monitoringBeneficiaryProjectsContracts':
            'Справка за проекти и договори на бенефициенти',
          'root.monitoringEvaluations': 'Справка Оценка',
          'root.monitoringContractReportPayments': 'Справка по ИП',
          'root.monitoringIrregularities': 'Справка Нередности',
          'root.monitoringPin': 'Справка по ЕГН',
          'root.monitoringArachne': 'Инструмент Arachne',
          'root.monitoringV4Plus4': 'Справка V4+4',
          'root.monitoringExpenseTypes': 'Справка Типове разходи',
          'root.monitoringSebra': 'Данни за СЕБРА',

          'root.sapFiles': 'Файлове от САП',
          'root.sapFiles.new': 'Нов файл',
          'root.sapFiles.view': 'Преглед',
          'root.sapFiles.view.paidAmounts': 'Данни',

          'root.sapCertReports': 'Експорт на данни за САП',

          'root.iacs': 'Заявки от ИСАК',
          'root.iacs.view': 'Преглед',

          'root.interfaces': 'Експорт на данни за други ИС',

          'root.projectDossier': 'Проектно досие',
          'root.projectDossier.view': 'Преглед',
          'root.projectDossier.view.project.versions.edit': 'Версия на ПП',
          'root.projectDossier.view.project.communications.edit': 'Комуникация към ПП',
          'root.projectDossier.view.project.evaluations.edit': 'Обобщена оценка на ПП',
          'root.projectDossier.view.project.standings.edit': 'Класиране на ПП',
          'root.projectDossier.view.documents': 'Документи',
          'root.projectDossier.view.contract': 'Договор',
          'root.projectDossier.view.contract.versions.edit': 'Преглед на версия',
          'root.projectDossier.view.contract.procurements.edit':
            'Преглед на процедура за избор на изпълнител',
          'root.projectDossier.view.contract.spendingPlans.edit': 'Преглед на план за разходване',
          'root.projectDossier.view.contract.communications.edit': 'Преглед на кореспонденция',
          'root.projectDossier.view.paidAmounts': 'Изплатени суми',
          'root.projectDossier.view.debts': 'Дългове',
          'root.projectDossier.view.reimbursedAmounts': 'Възстановени суми',
          'root.projectDossier.view.financialCorrections': 'Финансови корекции',
          'root.projectDossier.view.flatFinancialCorrections': 'Финансови корекции за СП',
          'root.projectDossier.view.approvedAmounts': 'Верифицирани средства',
          'root.projectDossier.view.certifiedAmounts': 'Сертифицирани средства',

          'root.programmePrognoses': 'Прогнози на ниво програма',
          'root.programmePrognoses.new': 'Нова прогноза',
          'root.programmePrognoses.view': 'Преглед на прогноза',

          'root.programmePriorityPrognoses': 'Прогнози на ниво разпоредител с бюджетни средства',
          'root.programmePriorityPrognoses.new': 'Нова прогноза',
          'root.programmePriorityPrognoses.view': 'Преглед на прогноза',

          'root.procedurePrognoses': 'Прогнози на ниво бюджет',
          'root.procedurePrognoses.new': 'Нова прогноза',
          'root.procedurePrognoses.view': 'Преглед на прогноза',

          'root.yearlyPrognosesReport': 'Справка „Годишни прогнози“',
          'root.monthlyPrognosesReport': 'Справка „Месечни прогнози“',
          'root.programmePriorityPrognosesReport':
            'Справка „ЛОТАР по разпоредител с бюджетни средства“',
          'root.programmePrognosesReport': 'Справка „ЛОТАР – ОП“',
          'root.prognosesSummaryReport': 'Справка „ЛОТАР – обобщена“',

          'root.userProfile.view': 'Преглед на потребителски профил',
          'root.userProfile.view.regData': 'Регистрационни данни',
          'root.userProfile.view.permissions': 'Права',
          'root.userProfile.view.requests': 'История на промените',
          'root.userProfile.view.declarations': 'Декларации'
        }
      });
    }
  ]);

export default BgModule.name;
