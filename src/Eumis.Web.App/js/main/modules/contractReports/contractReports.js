import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractReportTemplateUrl from './forms/contractReport.html';
import { ContractReportCtrl } from './forms/contractReportCtrl';
import contractReportFinancialTemplateUrl from './forms/contractReportFinancial.html';
import contractReportPaymentTemplateUrl from './forms/contractReportPayment.html';
import contractReportTechnicalTemplateUrl from './forms/contractReportTechnical.html';
import chooseContractReportContractModalTemplateUrl from './modals/chooseContractReportContractModal.html';
import { ChooseContractReportContractModalCtrl } from './modals/chooseContractReportContractModalCtrl';
import choosePaymentTypeModalTemplateUrl from './modals/choosePaymentTypeModal.html';
import { ChoosePaymentTypeModalCtrl } from './modals/choosePaymentTypeModalCtrl';
import { ContractReportFactory } from './resources/contractReport';
import { ContractReportFinancialFactory } from './resources/contractReportFinancial';
import { ContractReportPaymentFactory } from './resources/contractReportPayment';
import { ContractReportTechnicalFactory } from './resources/contractReportTechnical';
import contractReportsContractViewTemplateUrl from './views/contractReportsContractView.html';
import { ContractReportsContractViewCtrl } from './views/contractReportsContractViewCtrl';
import contractReportsDocumentsTemplateUrl from './views/contractReportsDocuments.html';
import { ContractReportDocumentsCtrl } from './views/contractReportsDocumentsCtrl';
import contractReportsDocumentsEditFinancialTemplateUrl from './views/contractReportsDocumentsEditFinancial.html';
import { ContractReportDocumentsEditFinancialCtrl } from './views/contractReportsDocumentsEditFinancialCtrl';
import contractReportsDocumentsEditPaymentTemplateUrl from './views/contractReportsDocumentsEditPayment.html';
import { ContractReportDocumentsEditPaymentCtrl } from './views/contractReportsDocumentsEditPaymentCtrl';
import contractReportsDocumentsEditTechnicalTemplateUrl from './views/contractReportsDocumentsEditTechnical.html';
import { ContractReportDocumentsEditTechnicalCtrl } from './views/contractReportsDocumentsEditTechnicalCtrl';
import contractReportsEditTemplateUrl from './views/contractReportsEdit.html';
import { ContractReportsEditCtrl } from './views/contractReportsEditCtrl';
import contractReportsNewStep1TemplateUrl from './views/contractReportsNewStep1.html';
import { ContractReportsNewStep1Ctrl } from './views/contractReportsNewStep1Ctrl';
import contractReportsNewStep2TemplateUrl from './views/contractReportsNewStep2.html';
import { ContractReportsNewStep2Ctrl } from './views/contractReportsNewStep2Ctrl';
import contractReportsSearchTemplateUrl from './views/contractReportsSearch.html';
import { ContractReportsSearchCtrl } from './views/contractReportsSearchCtrl';
import contractReportsViewTemplateUrl from './views/contractReportsView.html';
import { ContractReportsViewCtrl } from './views/contractReportsViewCtrl';

const ContractReportsModule = angular
  .module('main.contractReports', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractReport',
        templateUrl: contractReportTemplateUrl,
        controller: ContractReportCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportFinancial',
        templateUrl: contractReportFinancialTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportPayment',
        templateUrl: contractReportPaymentTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportTechnical',
        templateUrl: contractReportTechnicalTemplateUrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseContractReportContractModal'      , chooseContractReportContractModalTemplateUrl             , ChooseContractReportContractModalCtrl      , 'xlg')
    .modal('choosePaymentTypeModal'                 , choosePaymentTypeModalTemplateUrl                        , ChoosePaymentTypeModalCtrl                 , 'sm' );
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractReports'                                    , '/contractReports?contractRegNum'                                                                                                                                                                   ])
    .state(['root.contractReports.search'                             , ''                  ,       ['@root'                          , contractReportsSearchTemplateUrl                                     , ContractReportsSearchCtrl                       ]])
    .state(['root.contractReports.newStep1'                           , '/newStep1'         ,       ['@root'                          , contractReportsNewStep1TemplateUrl                                   , ContractReportsNewStep1Ctrl                     ]])
    .state(['root.contractReports.newStep2'                           , '/newStep2?cNum'    ,       ['@root'                          , contractReportsNewStep2TemplateUrl                                   , ContractReportsNewStep2Ctrl                     ]])
    .state(['root.contractReports.view'                               , '/:id?rf'           , true, ['@root'                          , contractReportsViewTemplateUrl                                       , ContractReportsViewCtrl                         ]])
    .state(['root.contractReports.view.contract'                      , ''                  ,       ['@root.contractReports.view'     , contractReportsContractViewTemplateUrl                               , ContractReportsContractViewCtrl                 ]])
    .state(['root.contractReports.view.data'                          , '/report'           ,       ['@root.contractReports.view'     , contractReportsEditTemplateUrl                                       , ContractReportsEditCtrl                         ]])
    .state(['root.contractReports.view.documents'                     , '/documents'                                                                                                                                                                                                        ])
    .state(['root.contractReports.view.documents.search'              , ''                  ,       ['@root.contractReports.view'     , contractReportsDocumentsTemplateUrl                                  , ContractReportDocumentsCtrl                     ]])
    .state(['root.contractReports.view.documents.editTechnical'       , '/technical/:ind'   ,       ['@root.contractReports.view'     , contractReportsDocumentsEditTechnicalTemplateUrl                     , ContractReportDocumentsEditTechnicalCtrl        ]])
    .state(['root.contractReports.view.documents.editFinancial'       , '/financial/:ind'   ,       ['@root.contractReports.view'     , contractReportsDocumentsEditFinancialTemplateUrl                     , ContractReportDocumentsEditFinancialCtrl        ]])
    .state(['root.contractReports.view.documents.editPayment'         , '/payment/:ind'     ,       ['@root.contractReports.view'     , contractReportsDocumentsEditPaymentTemplateUrl                       , ContractReportDocumentsEditPaymentCtrl          ]])
    }
  ]);

export default ContractReportsModule.name;
ContractReportsModule.factory('ContractReport', ContractReportFactory);
ContractReportsModule.factory('ContractReportFinancial', ContractReportFinancialFactory);
ContractReportsModule.factory('ContractReportPayment', ContractReportPaymentFactory);
ContractReportsModule.factory('ContractReportTechnical', ContractReportTechnicalFactory);
