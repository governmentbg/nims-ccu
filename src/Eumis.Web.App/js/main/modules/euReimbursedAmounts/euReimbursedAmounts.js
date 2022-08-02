import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import euReimbursedAmountDataTemplateUrl from './forms/euReimbursedAmountData.html';
import { EuReimbursedAmountDataCtrl } from './forms/euReimbursedAmountDataCtrl';
import chooseEuReimbursedAmountCertReportsModalTemplateUrl from './modals/chooseEuReimbursedAmountCertReportsModal.html';
import { ChooseEuReimbursedAmountCertReportsModalCtrl } from './modals/chooseEuReimbursedAmountCertReportsModalCtrl';
import { EuReimbursedAmountFactory } from './resources/euReimbursedAmount';
import { EuReimbursedAmountCertReportFactory } from './resources/euReimbursedAmountCertReport';
import euReimbursedAmountCertReportsTemplateUrl from './views/euReimbursedAmountCertReports.html';
import { EuReimbursedAmountCertReportsCtrl } from './views/euReimbursedAmountCertReportsCtrl';
import euReimbursedAmountsEditTemplateUrl from './views/euReimbursedAmountsEdit.html';
import { EuReimbursedAmountsEditCtrl } from './views/euReimbursedAmountsEditCtrl';
import euReimbursedAmountsNewTemplateUrl from './views/euReimbursedAmountsNew.html';
import { EuReimbursedAmountsNewCtrl } from './views/euReimbursedAmountsNewCtrl';
import euReimbursedAmountsSearchTemplateUrl from './views/euReimbursedAmountsSearch.html';
import { EuReimbursedAmountsSearchCtrl } from './views/euReimbursedAmountsSearchCtrl';
import euReimbursedAmountsViewTemplateUrl from './views/euReimbursedAmountsView.html';
import { EuReimbursedAmountsViewCtrl } from './views/euReimbursedAmountsViewCtrl';

const EuReimbursedAmountsModule = angular
  .module('main.euReimbursedAmounts', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisEuReimbursedAmountData',
        templateUrl: euReimbursedAmountDataTemplateUrl,
        controller: EuReimbursedAmountDataCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseEuReimbursedAmountCertReportsModal', chooseEuReimbursedAmountCertReportsModalTemplateUrl , ChooseEuReimbursedAmountCertReportsModalCtrl,'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.euReimbursedAmounts'                                , '/euReimbursedAmounts?status'                                                                                                                                                                                       ])
    .state(['root.euReimbursedAmounts.search'                         , ''                 ,       ['@root'                          , euReimbursedAmountsSearchTemplateUrl                              , EuReimbursedAmountsSearchCtrl                   ]])
    .state(['root.euReimbursedAmounts.new'                            , '/new'             ,       ['@root'                          , euReimbursedAmountsNewTemplateUrl                                 , EuReimbursedAmountsNewCtrl                      ]])
    .state(['root.euReimbursedAmounts.view'                           , '/:id?rf'          , true, ['@root'                          , euReimbursedAmountsViewTemplateUrl                                , EuReimbursedAmountsViewCtrl                     ]])
    .state(['root.euReimbursedAmounts.view.amount'                    , ''                 ,       ['@root.euReimbursedAmounts.view' , euReimbursedAmountsEditTemplateUrl                                , EuReimbursedAmountsEditCtrl                     ]])
    .state(['root.euReimbursedAmounts.view.certReports'               , '/certReports'     ,       ['@root.euReimbursedAmounts.view' , euReimbursedAmountCertReportsTemplateUrl                          , EuReimbursedAmountCertReportsCtrl               ]]);
    }
  ]);

export default EuReimbursedAmountsModule.name;
EuReimbursedAmountsModule.factory('EuReimbursedAmount', EuReimbursedAmountFactory);
EuReimbursedAmountsModule.factory(
  'EuReimbursedAmountCertReport',
  EuReimbursedAmountCertReportFactory
);
