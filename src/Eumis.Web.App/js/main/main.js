// polyfills
import 'autofill-event/src/autofill-event';

// external modules
import angular from 'angular';
import AngularBootstrapNavTreeModule from 'angular-bootstrap-nav-tree';
import AngularL10NModule from 'l10n-angular/build/l10n-with-tools';
import AngularMomentModule from 'angular-moment';
import ChartJsModule from 'angular-chart.js';
import NgAnimateModule from 'angular-animate';
import NgResourceModule from 'angular-resource';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import UiSortableModule from 'angular-ui-sortable/src/sortable';

// root modules
import AuthorizerModule from 'js/authorizer';
import BootModule from 'js/boot';
import ScaffoldingModule from 'js/scaffolding/scaffolding';

// submodules
import ActionLogsModule from 'js/main/modules/actionLogs/actionLogs';
import ActuallyPaidAmountsModule from 'js/main/modules/actuallyPaidAmounts/actuallyPaidAmounts';
import CompaniesModule from 'js/main/modules/companies/companies';
import ContractCommunicationsModule from 'js/main/modules/contractCommunications/contractCommunications';
import ContractRegistrationsModule from 'js/main/modules/contractRegistrations/contractRegistrations';
import ContractAccessCodesModule from 'js/main/modules/contractAccessCodes/contractAccessCodes';
import ContractReportCertCorrectionsModule from 'js/main/modules/contractReportCertCorrections/contractReportCertCorrections';
import ContractReportChecksModule from 'js/main/modules/contractReportChecks/contractReportChecks';
import ContractReportCorrectionsModule from 'js/main/modules/contractReportCorrections/contractReportCorrections';
import ContractReportFinancialCertCorrectionsModule from 'js/main/modules/contractReportFinancialCertCorrections/contractReportFinancialCertCorrections';
import ContractReportFinancialCorrectionsModule from 'js/main/modules/contractReportFinancialCorrections/contractReportFinancialCorrections';
import ContractReportFinancialRevalidationsModule from 'js/main/modules/contractReportFinancialRevalidations/contractReportFinancialRevalidations';
import ContractReportRevalidationsModule from 'js/main/modules/contractReportRevalidations/contractReportRevalidations';
import ContractReportTechnicalCorrectionsModule from 'js/main/modules/contractReportTechnicalCorrections/contractReportTechnicalCorrections';
import ContractReportsModule from 'js/main/modules/contractReports/contractReports';
import ContractsModule from 'js/main/modules/contracts/contracts';
import DebtsModule from 'js/main/modules/debts/debts';
import EuReimbursedAmountsModule from 'js/main/modules/euReimbursedAmounts/euReimbursedAmounts';
import EvalSessionsModule from 'js/main/modules/evalSessions/evalSessions';
import FinancialCorrectionsModule from 'js/main/modules/financialCorrections/financialCorrections';
import FiReimbursedAmountsModule from 'js/main/modules/fiReimbursedAmounts/fiReimbursedAmounts';
import FlatFinancialCorrectionsModule from 'js/main/modules/flatFinancialCorrections/flatFinancialCorrections';
import GuidancesModule from 'js/main/modules/guidances/guidances';
import InterfacesModule from 'js/main/modules/interfaces/interfaces';
import MessagesModule from 'js/main/modules/messages/messages';
import MonitoringModule from 'js/main/modules/monitoring/monitoring';
import NewsModule from 'js/main/modules/news/news';
import NewsFeedModule from 'js/main/modules/newsFeed/newsFeed';
import NotificationsModule from 'js/main/modules/notifications/notifications';
import OperationalMapModule from 'js/main/modules/operationalMap/operationalMap';
import ProceduresModule from 'js/main/modules/procedures/procedures';
import PrognosesModule from 'js/main/modules/prognoses/prognoses';
import ProjectDossierModule from 'js/main/modules/projectDossier/projectDossier';
import ProjectsModule from 'js/main/modules/projects/projects';
import ProjectManagingAuthorityCommunicationsModule from 'js/main/modules/projectManagingAuthorityCommunications/projectManagingAuthorityCommunications';
import ProjectMassManagingAuthorityCommunicationsModule from 'js/main/modules/projectMassManagingAuthorityCommunications/projectMassManagingAuthorityCommunications';
import RegistrationsModule from 'js/main/modules/registrations/registrations';
import ReimbursedAmountsModule from 'js/main/modules/reimbursedAmounts/reimbursedAmounts';
import SapInterfacesModule from 'js/main/modules/sapInterfaces/sapInterfaces';
import RegixInterfacesModule from 'js/main/modules/regixInterfaces/regixInterfaces';
import UsersModule from 'js/main/modules/users/users';
import UserProfileModule from 'js/main/modules/userProfile/userProfile';
import ProcurementModule from './modules/procurements/procurements';
import MonitorstatInterfacesModule from './modules/monitorstat/monitorstat';
import { eumisPercentDirective } from './directives/percent/percentDirective';
import { eumisStructuredDocumentDirective } from './directives/structuredDocument/structuredDocumentDirective';
import { eumisStructuredParentChildDocumentDirective } from './directives/structuredParentChildDocument/structuredParentChildDocumentDirective';
import { joinFilter } from './filters/joinFilter';
import { moneyFilter } from './filters/moneyFilter';
import { trimFilter } from './filters/trimFilter';
import bfpCalculatorModalTemplateUrl from './modals/bfpCalculatorModal.html';
import { BfpCalculatorModalCtrl } from './modals/bfpCalculatorModalCtrl';
import messageNoteModalTemplateUrl from './modals/messageNoteModal.html';
import { MessageNoteModalCtrl } from './modals/messageNoteModalCtrl';
import portalIntegrationModalTemplateUrl from './modals/portalIntegrationModal.html';
import { PortalIntegrationModalCtrl } from './modals/portalIntegrationModalCtrl';
import validationErrorsModalTemplateUrl from './modals/validationErrorsModal.html';
import { ValidationErrorsModalCtrl } from './modals/validationErrorsModalCtrl';
import { BfpCalculatorFactory } from './resources/bfpCalculator';
import { scConfirmService } from './services/confirmService';
import { csdNameCreatorService } from './services/csdNameCreator';
import { moneyConversionService, exchangeRatesConstant } from './services/moneyConversion';
import { moneyOperationService } from './services/moneyOperation';
import { romanizerService } from './services/romanizer';
import {
  structuredDocumentService,
  structuredDocumentConfigConstant
} from './services/structuredDocument';
import { uinValidationService } from './services/uinValidation';
import rootTemplateUrl from './views/root.html';
import { RootCtrl, rootViewFactory } from './views/rootCtrl';
import { inspect } from 'util';

const MainModule = angular
  .module('main', [
    // external modules
    NgAnimateModule,
    NgResourceModule,
    UiRouterModule,
    UiBootstrapModule,
    UiSortableModule,
    AngularMomentModule,
    AngularL10NModule,
    ChartJsModule,
    AngularBootstrapNavTreeModule,

    // root modules
    BootModule,
    AuthorizerModule,
    ScaffoldingModule,

    // submodules
    ActionLogsModule,
    ActuallyPaidAmountsModule,
    CompaniesModule,
    ContractCommunicationsModule,
    ContractRegistrationsModule,
    ContractAccessCodesModule,
    ContractReportCertCorrectionsModule,
    ContractReportChecksModule,
    ContractReportCorrectionsModule,
    ContractReportFinancialCertCorrectionsModule,
    ContractReportFinancialCorrectionsModule,
    ContractReportFinancialRevalidationsModule,
    ContractReportRevalidationsModule,
    ContractReportTechnicalCorrectionsModule,
    ContractReportsModule,
    ContractsModule,
    DebtsModule,
    EuReimbursedAmountsModule,
    EvalSessionsModule,
    FinancialCorrectionsModule,
    FiReimbursedAmountsModule,
    FlatFinancialCorrectionsModule,
    GuidancesModule,
    InterfacesModule,
    MessagesModule,
    MonitoringModule,
    NewsModule,
    NewsFeedModule,
    NotificationsModule,
    OperationalMapModule,
    ProceduresModule,
    PrognosesModule,
    ProjectDossierModule,
    ProjectsModule,
    ProjectManagingAuthorityCommunicationsModule,
    ProjectMassManagingAuthorityCommunicationsModule,
    RegistrationsModule,
    ReimbursedAmountsModule,
    SapInterfacesModule,
    RegixInterfacesModule,
    UsersModule,
    UserProfileModule,
    ProcurementModule,
    MonitorstatInterfacesModule
  ])
  .config([
    '$urlRouterProvider',
    '$locationProvider',
    function($urlRouterProvider, $locationProvider) {
      $locationProvider.html5Mode(false);
      $locationProvider.hashPrefix('');
      $urlRouterProvider.otherwise('/');
    }
  ])
  .config([
    '$provide',
    function($provide) {
      $provide.decorator('$exceptionHandler', [
        '$delegate',
        '$injector',
        '$window',
        function($delegate, $injector, $window) {
          var $rootScope, l10n, scMessage, l10nMessage, modalMessage;
          return function(exception) {
            $delegate(exception);

            // skip alerts for angular 1.6 'Possibly unhandled rejection' errors
            if (
              typeof exception === 'string' &&
              exception.indexOf('Possibly unhandled rejection') !== -1
            ) {
              return;
            }

            // skip alerts for cancelled transitions
            // e.g. the user navigated somewhere else, before the current navigation has completed
            if (exception instanceof Error && exception.message === 'transition superseded') {
              return;
            }

            // skip aborted requests (most likely aborted due to 'transition superseded')
            if (exception && exception.xhrStatus === 'abort') {
              return;
            }

            try {
              $rootScope = $rootScope || $injector.get('$rootScope');
              l10n = l10n || $injector.get('l10n');
              scMessage = scMessage || $injector.get('scMessage');

              switch (exception.status) {
                case 403: //forbidden
                  l10nMessage = 'forbiddenErrorMessage';
                  break;
                case 404: //not found
                  l10nMessage = 'objectNotFoundErrorMessage';
                  break;
                case 409: //conflict
                  l10nMessage = 'updateConcurrencyErrorMessage';
                  break;
                case 401: //unauthorized
                  modalMessage = 'sessionExpiredMessage';
                  break;
                case 503: //service unavailable
                  modalMessage = 'serviceUnavailableMessage';
                  break;
                default:
                  l10nMessage = 'unknownErrorMessage';

                  // do not log network errors and timeouts in Google Analytics
                  if (
                    exception &&
                    (exception.xhrStatus === 'error' || exception.xhrStatus === 'timeout')
                  ) {
                    break;
                  }

                  var trackingMessage;
                  if (exception instanceof Error) {
                    trackingMessage = `${exception.name}\n${exception.message}\n${exception.stack}`;
                  } else {
                    trackingMessage = inspect(exception);
                  }

                  $window.ga('send', 'exception', {
                    exDescription: trackingMessage
                  });

                  break;
              }

              if (modalMessage) {
                return scMessage(modalMessage).then(function(result) {
                  if (result === 'OK') {
                    window.location.reload();
                  }
                });
              } else {
                $rootScope.$broadcast('alert', l10n.get(l10nMessage), 'danger');
              }
            } catch (e) {
              //swallow all exception so that we don't end up in an infinite loop
            }
          };
        }
      ]);
    }
  ])
  .factory('accessToken', function() {
    return {
      get: function() {
        var match = /(^|;\s?)authCookie=(.+?)($|;)/.exec(window.document.cookie),
          accessToken = match && match[2];

        return accessToken;
      }
    };
  })
  .factory('authHttpInterceptor', [
    'accessToken',
    function(accessToken) {
      return {
        request: function(config) {
          var token = accessToken.get();
          if (token) {
            config.headers.Authorization = 'Bearer ' + token;
          }

          return config;
        }
      };
    }
  ])
  .config([
    '$httpProvider',
    function($httpProvider) {
      $httpProvider.defaults.headers.get = {
        'cache-control': 'no-cache, no-store, must-revalidate',
        Pragma: 'no-cache',
        Expires: '0'
      };

      $httpProvider.interceptors.push('authHttpInterceptor');
    }
  ])
  .run([
    '$rootScope',
    'authorizerService',
    '$state',
    function($rootScope, authorizer, $state) {
      var offStateChangeErrorFn, offStateChangeSuccessFn;

      //add the authorizer's canDo to the rootScope so that it is accessible everywhere
      $rootScope.$canDo = authorizer.canDo;

      //listen for the first state change(after login) and if it is unsuccessful, due to
      //lack of permissions, redirect to root.map.tree instead of falling into an endless loop
      //and do nothing
      offStateChangeErrorFn = $rootScope.$on('$stateChangeError', function(
        event,
        toState,
        toParams,
        fromState,
        fromParams,
        error
      ) {
        if (error.status === 403) {
          $state.go('root.map.tree');
        }
      });

      //unsubscribe the listeners
      offStateChangeSuccessFn = $rootScope.$on('$stateChangeSuccess', function() {
        offStateChangeErrorFn();
        offStateChangeSuccessFn();
      });
    }
  ])
  .config([
    '$provide',
    function($provide) {
      $provide.decorator('$resource', [
        '$delegate',
        function($resource) {
          return function(url, paramDefaults, actions) {
            var updateAction = {
              update: { method: 'PUT' }
            };
            actions = angular.extend({}, updateAction, actions);
            return $resource(url, paramDefaults, actions);
          };
        }
      ]);
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      scModalProvider
        //common
        .modal(
          'validationErrorsModal',
          validationErrorsModalTemplateUrl,
          ValidationErrorsModalCtrl,
          'md'
        )
        .modal(
          'portalIntegrationModal',
          portalIntegrationModalTemplateUrl,
          PortalIntegrationModalCtrl,
          'plg'
        )
        .modal('messageNoteModal', messageNoteModalTemplateUrl, MessageNoteModalCtrl, 'sm')
        .modal('bfpCalculatorModal', bfpCalculatorModalTemplateUrl, BfpCalculatorModalCtrl, 'sm');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root'                                                    , null               ,       ['@'                               , rootTemplateUrl                                                                 , RootCtrl                               ]]);
    }
  ])
  .constant('eumisConstants', {
    emailRegex: /^[a-z0-9!#$%&'*+/=?^_`{|}~.-]+@[a-z0-9-]+(\.[a-z0-9-]+)*$/i
  });

export default MainModule.name;
MainModule.directive('eumisPercent', eumisPercentDirective);
MainModule.directive('eumisStructuredDocument', eumisStructuredDocumentDirective);
MainModule.directive(
  'eumisStructuredParentChildDocument',
  eumisStructuredParentChildDocumentDirective
);
MainModule.filter('join', joinFilter);
MainModule.filter('money', moneyFilter);
MainModule.filter('trim', trimFilter);
MainModule.factory('BfpCalculator', BfpCalculatorFactory);
MainModule.factory('rootView', rootViewFactory);
MainModule.service('scConfirm', scConfirmService);
MainModule.service('csdNameCreator', csdNameCreatorService);
MainModule.service('moneyConversion', moneyConversionService);
MainModule.service('moneyOperation', moneyOperationService);
MainModule.service('romanizer', romanizerService);
MainModule.service('structuredDocument', structuredDocumentService);
MainModule.service('uinValidation', uinValidationService);
MainModule.constant('exchangeRates', exchangeRatesConstant);
MainModule.constant('structuredDocumentConfig', structuredDocumentConfigConstant);
