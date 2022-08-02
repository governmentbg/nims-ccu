import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import actuallyPaidAmountBasicDataTemplateUrl from './forms/actuallyPaidAmountBasicData.html';
import actuallyPaidAmountDataTemplateUrl from './forms/actuallyPaidAmountData.html';
import { ActuallyPaidAmountDataCtrl } from './forms/actuallyPaidAmountDataCtrl';
import chooseActuallyPaidAmountContractModalTemplateUrl from './modals/chooseActuallyPaidAmountContractModal.html';
import { ChooseActuallyPaidAmountContractModalCtrl } from './modals/chooseActuallyPaidAmountContractModalCtrl';
import chooseActuallyPaidAmountContractReportPaymentModalTemplateUrl from './modals/chooseActuallyPaidAmountContractReportPaymentModal.html';
import { ChooseActuallyPaidAmountContractReportPaymentModalCtrl } from './modals/chooseActuallyPaidAmountContractReportPaymentModalCtrl';
import { ActuallyPaidAmountFactory } from './resources/actuallyPaidAmount';
import { ActuallyPaidAmountDocumentFactory } from './resources/actuallyPaidAmountDocument';
import { PaidAmountFileFactory } from './resources/actuallyPaidAmountFile';
import actuallyPaidAmountsBasicViewTemplateUrl from './views/actuallyPaidAmountsBasicView.html';
import { ActuallyPaidAmountsBasicViewCtrl } from './views/actuallyPaidAmountsBasicViewCtrl';
import actuallyPaidAmountsDocumentsEditTemplateUrl from './views/actuallyPaidAmountsDocumentsEdit.html';
import { ActuallyPaidAmountsDocumentsEditCtrl } from './views/actuallyPaidAmountsDocumentsEditCtrl';
import actuallyPaidAmountsDocumentsNewTemplateUrl from './views/actuallyPaidAmountsDocumentsNew.html';
import { ActuallyPaidAmountsDocumentsNewCtrl } from './views/actuallyPaidAmountsDocumentsNewCtrl';
import actuallyPaidAmountsDocumentsSearchTemplateUrl from './views/actuallyPaidAmountsDocumentsSearch.html';
import { ActuallyPaidAmountsDocumentsSearchCtrl } from './views/actuallyPaidAmountsDocumentsSearchCtrl';
import actuallyPaidAmountsEditTemplateUrl from './views/actuallyPaidAmountsEdit.html';
import { ActuallyPaidAmountsEditCtrl } from './views/actuallyPaidAmountsEditCtrl';
import actuallyPaidAmountsNewStep1TemplateUrl from './views/actuallyPaidAmountsNewStep1.html';
import { ActuallyPaidAmountsNewStep1Ctrl } from './views/actuallyPaidAmountsNewStep1Ctrl';
import actuallyPaidAmountsNewStep2TemplateUrl from './views/actuallyPaidAmountsNewStep2.html';
import { ActuallyPaidAmountsNewStep2Ctrl } from './views/actuallyPaidAmountsNewStep2Ctrl';
import actuallyPaidAmountsSearchTemplateUrl from './views/actuallyPaidAmountsSearch.html';
import { ActuallyPaidAmountsSearchCtrl } from './views/actuallyPaidAmountsSearchCtrl';
import actuallyPaidAmountsViewTemplateUrl from './views/actuallyPaidAmountsView.html';
import { ActuallyPaidAmountsViewCtrl } from './views/actuallyPaidAmountsViewCtrl';

const ActuallyPaidAmountsModule = angular
  .module('main.actuallyPaidAmounts', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisActuallyPaidAmountBasicData',
        templateUrl: actuallyPaidAmountBasicDataTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisActuallyPaidAmountData',
        templateUrl: actuallyPaidAmountDataTemplateUrl,
        controller: ActuallyPaidAmountDataCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseActuallyPaidAmountContractModal'                , chooseActuallyPaidAmountContractModalTemplateUrl                 , ChooseActuallyPaidAmountContractModalCtrl               , 'xlg')
    .modal('chooseActuallyPaidAmountContractReportPaymentModal'   , chooseActuallyPaidAmountContractReportPaymentModalTemplateUrl    , ChooseActuallyPaidAmountContractReportPaymentModalCtrl  , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.actuallyPaidAmounts'                                , '/actuallyPaidAmounts?contractId&paymentReason'                                                                                                                                                                     ])
    .state(['root.actuallyPaidAmounts.search'                         , ''                  ,       ['@root'                          , actuallyPaidAmountsSearchTemplateUrl                             , ActuallyPaidAmountsSearchCtrl                   ]])
    .state(['root.actuallyPaidAmounts.newStep1'                       , '/newStep1'         ,       ['@root'                          , actuallyPaidAmountsNewStep1TemplateUrl                           , ActuallyPaidAmountsNewStep1Ctrl                 ]])
    .state(['root.actuallyPaidAmounts.newStep2'                       , '/newStep2?cNum'    ,       ['@root'                          , actuallyPaidAmountsNewStep2TemplateUrl                           , ActuallyPaidAmountsNewStep2Ctrl                 ]])
    .state(['root.actuallyPaidAmounts.view'                           , '/:id?rf'           , true, ['@root'                          , actuallyPaidAmountsViewTemplateUrl                               , ActuallyPaidAmountsViewCtrl                     ]])
    .state(['root.actuallyPaidAmounts.view.basicData'                 , ''                  ,       ['@root.actuallyPaidAmounts.view' , actuallyPaidAmountsBasicViewTemplateUrl                          , ActuallyPaidAmountsBasicViewCtrl                ]])
    .state(['root.actuallyPaidAmounts.view.paidAmount'                , '/paidAmount'       ,       ['@root.actuallyPaidAmounts.view' , actuallyPaidAmountsEditTemplateUrl                               , ActuallyPaidAmountsEditCtrl                     ]])
    .state(['root.actuallyPaidAmounts.view.documents'                 , '/documents'                                                                                                                                                                                                        ])
    .state(['root.actuallyPaidAmounts.view.documents.search'          , ''                  ,       ['@root.actuallyPaidAmounts.view' , actuallyPaidAmountsDocumentsSearchTemplateUrl                    , ActuallyPaidAmountsDocumentsSearchCtrl          ]])
    .state(['root.actuallyPaidAmounts.view.documents.new'             , '/new'              ,       ['@root.actuallyPaidAmounts.view' , actuallyPaidAmountsDocumentsNewTemplateUrl                       , ActuallyPaidAmountsDocumentsNewCtrl             ]])
    .state(['root.actuallyPaidAmounts.view.documents.edit'            , '/:ind'             ,       ['@root.actuallyPaidAmounts.view' , actuallyPaidAmountsDocumentsEditTemplateUrl                      , ActuallyPaidAmountsDocumentsEditCtrl            ]]);
    }
  ]);

export default ActuallyPaidAmountsModule.name;
ActuallyPaidAmountsModule.factory('ActuallyPaidAmount', ActuallyPaidAmountFactory);
ActuallyPaidAmountsModule.factory('ActuallyPaidAmountDocument', ActuallyPaidAmountDocumentFactory);
ActuallyPaidAmountsModule.factory('PaidAmountFile', PaidAmountFileFactory);
