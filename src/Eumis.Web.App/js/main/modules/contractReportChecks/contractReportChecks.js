import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractReportAdvanceNVPaymentAmountTemplateUrl from './forms/contractReportAdvanceNVPaymentAmount.html';
import { ContractReportAdvanceNVPaymentAmountCtrl } from './forms/contractReportAdvanceNVPaymentAmountCtrl';
import contractReportAdvancePaymentAmountTemplateUrl from './forms/contractReportAdvancePaymentAmount.html';
import { ContractReportAdvancePaymentAmountCtrl } from './forms/contractReportAdvancePaymentAmountCtrl';
import contractReportFinancialCheckTemplateUrl from './forms/contractReportFinancialCheck.html';
import contractReportFinancialCSDBudgetItemTemplateUrl from './forms/contractReportFinancialCSDBudgetItem.html';
import { ContractReportFinancialCSDBudgetItemCtrl } from './forms/contractReportFinancialCSDBudgetItemCtrl';
import contractReportIndicatorTemplateUrl from './forms/contractReportIndicator.html';
import { ContractReportIndicatorCtrl } from './forms/contractReportIndicatorCtrl';
import contractReportPaymentCheckTemplateUrl from './forms/contractReportPaymentCheck.html';
import { ContractReportPaymentCheckCtrl } from './forms/contractReportPaymentCheckCtrl';
import contractReportTechnicalCheckTemplateUrl from './forms/contractReportTechnicalCheck.html';
import chooseFinancialCorrectionModalTemplateUrl from './modals/chooseFinancialCorrectionModal.html';
import { ChooseFinancialCorrectionModalCtrl } from './modals/chooseFinancialCorrectionModalCtrl';
import { ContractReportAdvanceNVPaymentAmountFactory } from './resources/contractReportAdvanceNVPaymentAmount';
import { ContractReportAdvancePaymentAmountFactory } from './resources/contractReportAdvancePaymentAmount';
import { ContractReportAttachedFinancialCorrectionFactory } from './resources/contractReportAttachedFinancialCorrection';
import { ContractReportCheckFileFactory } from './resources/contractReportCheckFile';
import { ContractReportFinancialCheckFactory } from './resources/contractReportFinancialCheck';
import { ContractReportFinancialCSDBudgetItemFactory } from './resources/contractReportFinancialCSDBudgetItem';
import { ContractReportFinancialCSDFileFactory } from './resources/contractReportFinancialCSDFile';
import { ContractReportIndicatorFactory } from './resources/contractReportIndicator';
import { ContractReportPaymentCheckFactory } from './resources/contractReportPaymentCheck';
import { ContractReportPaymentRequestFactory } from './resources/contractReportPaymentRequest';
import { ContractReportTechnicalCheckFactory } from './resources/contractReportTechnicalCheck';
import contractReportAttachedDocFinancialCorrectionTemplateUrl from './views/contractReportAttachedDocFinancialCorrection.html';
import { ContractReportAttachedDocFinancialCorrectionCtrl } from './views/contractReportAttachedDocFinancialCorrectionCtrl';
import contractReportAttachedDocsTemplateUrl from './views/contractReportAttachedDocs.html';
import { ContractReportAttachedDocsCtrl } from './views/contractReportAttachedDocsCtrl';
import contractReportCheckIndicatorsEditTemplateUrl from './views/contractReportCheckIndicatorsEdit.html';
import { ContractReportCheckIndicatorsEditCtrl } from './views/contractReportCheckIndicatorsEditCtrl';
import contractReportCheckIndicatorsSearchTemplateUrl from './views/contractReportCheckIndicatorsSearch.html';
import { ContractReportCheckIndicatorsSearchCtrl } from './views/contractReportCheckIndicatorsSearchCtrl';
import contractReportsAdvanceNVPaymentAmountsEditTemplateUrl from './views/contractReportsAdvanceNVPaymentAmountsEdit.html';
import { ContractReportsAdvanceNVPaymentAmountsEditCtrl } from './views/contractReportsAdvanceNVPaymentAmountsEditCtrl';
import contractReportsAdvanceNVPaymentAmountsSearchTemplateUrl from './views/contractReportsAdvanceNVPaymentAmountsSearch.html';
import { ContractReportsAdvanceNVPaymentAmountsSearchCtrl } from './views/contractReportsAdvanceNVPaymentAmountsSearchCtrl';
import contractReportsAdvancePaymentAmountsEditTemplateUrl from './views/contractReportsAdvancePaymentAmountsEdit.html';
import { ContractReportsAdvancePaymentAmountsEditCtrl } from './views/contractReportsAdvancePaymentAmountsEditCtrl';
import contractReportsAdvancePaymentAmountsSearchTemplateUrl from './views/contractReportsAdvancePaymentAmountsSearch.html';
import { ContractReportsAdvancePaymentAmountsSearchCtrl } from './views/contractReportsAdvancePaymentAmountsSearchCtrl';
import contractReportsChecksTemplateUrl from './views/contractReportsChecks.html';
import { ContractReportChecksCtrl } from './views/contractReportsChecksCtrl';
import contractReportsChecksEditFinancialTemplateUrl from './views/contractReportsChecksEditFinancial.html';
import { ContractReportChecksEditFinancialCtrl } from './views/contractReportsChecksEditFinancialCtrl';
import contractReportsChecksEditPaymentTemplateUrl from './views/contractReportsChecksEditPayment.html';
import { ContractReportChecksEditPaymentCtrl } from './views/contractReportsChecksEditPaymentCtrl';
import contractReportsChecksEditTechnicalTemplateUrl from './views/contractReportsChecksEditTechnical.html';
import { ContractReportChecksEditTechnicalCtrl } from './views/contractReportsChecksEditTechnicalCtrl';
import contractReportsContractViewTemplateUrl from './views/contractReportsContractView.html';
import { ContractReportChecksContractViewCtrl } from './views/contractReportsContractViewCtrl';
import contractReportsDocumentsTemplateUrl from './views/contractReportsDocuments.html';
import { ContractReportChecksDocumentsCtrl } from './views/contractReportsDocumentsCtrl';
import contractReportsDocumentsEditFinancialTemplateUrl from './views/contractReportsDocumentsEditFinancial.html';
import { ContractReportChecksDocumentsEditFinancialCtrl } from './views/contractReportsDocumentsEditFinancialCtrl';
import contractReportsDocumentsEditPaymentTemplateUrl from './views/contractReportsDocumentsEditPayment.html';
import { ContractReportChecksDocumentsEditPaymentCtrl } from './views/contractReportsDocumentsEditPaymentCtrl';
import contractReportsDocumentsEditTechnicalTemplateUrl from './views/contractReportsDocumentsEditTechnical.html';
import { ContractReportChecksDocumentsEditTechnicalCtrl } from './views/contractReportsDocumentsEditTechnicalCtrl';
import contractReportsEditTemplateUrl from './views/contractReportsEdit.html';
import { ContractReportChecksReportsEditCtrl } from './views/contractReportsEditCtrl';
import contractReportsFinancialCSDBudgetItemsEditTemplateUrl from './views/contractReportsFinancialCSDBudgetItemsEdit.html';
import { ContractReportsFinancialCSDBudgetItemsEditCtrl } from './views/contractReportsFinancialCSDBudgetItemsEditCtrl';
import contractReportsFinancialCSDBudgetItemsSearchTemplateUrl from './views/contractReportsFinancialCSDBudgetItemsSearch.html';
import { ContractReportsFinancialCSDBudgetItemsSearchCtrl } from './views/contractReportsFinancialCSDBudgetItemsSearchCtrl';
import contractReportsPaymentChecksTemplateUrl from './views/contractReportsPaymentChecks.html';
import { ContractReportPaymentChecksCtrl } from './views/contractReportsPaymentChecksCtrl';
import contractReportsPaymentRequestsTemplateUrl from './views/contractReportsPaymentRequests.html';
import { ContractReportsPaymentRequestsCtrl } from './views/contractReportsPaymentRequestsCtrl';
import contractReportsSearchTemplateUrl from './views/contractReportsSearch.html';
import { ContractReportChecksReportsSearchCtrl } from './views/contractReportsSearchCtrl';
import contractReportsViewTemplateUrl from './views/contractReportsView.html';
import { ContractReportChecksViewCtrl } from './views/contractReportsViewCtrl';

const ContractReportChecksModule = angular
  .module('main.contractReportChecks', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractReportFinancialCheck',
        templateUrl: contractReportFinancialCheckTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportTechnicalCheck',
        templateUrl: contractReportTechnicalCheckTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportPaymentCheck',
        templateUrl: contractReportPaymentCheckTemplateUrl,
        controller: ContractReportPaymentCheckCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportFinancialCostSupportingDocumentBudgetItem',
        templateUrl: contractReportFinancialCSDBudgetItemTemplateUrl,
        controller: ContractReportFinancialCSDBudgetItemCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportIndicator',
        templateUrl: contractReportIndicatorTemplateUrl,
        controller: ContractReportIndicatorCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportAdvancePaymentAmount',
        templateUrl: contractReportAdvancePaymentAmountTemplateUrl,
        controller: ContractReportAdvancePaymentAmountCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportAdvanceNVPaymentAmount',
        templateUrl: contractReportAdvanceNVPaymentAmountTemplateUrl,
        controller: ContractReportAdvanceNVPaymentAmountCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseFinancialCorrection'              , chooseFinancialCorrectionModalTemplateUrl           , ChooseFinancialCorrectionModalCtrl         , 'xlg')
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractReportChecks'                               , '/contractReportChecks?contractRegNum&fromDate&toDate'                                                                                                                                                              ])
    .state(['root.contractReportChecks.search'                        , ''                  ,       ['@root'                          , contractReportsSearchTemplateUrl                                , ContractReportChecksReportsSearchCtrl           ]])
    .state(['root.contractReportChecks.view'                          , '/:id?rf'           , true, ['@root'                          , contractReportsViewTemplateUrl                                  , ContractReportChecksViewCtrl                    ]])
    .state(['root.contractReportChecks.view.contract'                 , ''                  ,       ['@root.contractReportChecks.view', contractReportsContractViewTemplateUrl                          , ContractReportChecksContractViewCtrl            ]])
    .state(['root.contractReportChecks.view.data'                     , '/report'           ,       ['@root.contractReportChecks.view', contractReportsEditTemplateUrl                                  , ContractReportChecksReportsEditCtrl             ]])

    .state(['root.contractReportChecks.view.documents'                , '/documents'                                                                                                                                                                                                        ])
    .state(['root.contractReportChecks.view.documents.search'         , ''                  ,       ['@root.contractReportChecks.view', contractReportsDocumentsTemplateUrl                             , ContractReportChecksDocumentsCtrl               ]])
    .state(['root.contractReportChecks.view.documents.editTechnical'  , '/technical/:ind'   ,       ['@root.contractReportChecks.view', contractReportsDocumentsEditTechnicalTemplateUrl                , ContractReportChecksDocumentsEditTechnicalCtrl  ]])
    .state(['root.contractReportChecks.view.documents.editFinancial'  , '/financial/:ind'   ,       ['@root.contractReportChecks.view', contractReportsDocumentsEditFinancialTemplateUrl                , ContractReportChecksDocumentsEditFinancialCtrl  ]])
    .state(['root.contractReportChecks.view.documents.editPayment'    , '/payment/:ind'     ,       ['@root.contractReportChecks.view', contractReportsDocumentsEditPaymentTemplateUrl                  , ContractReportChecksDocumentsEditPaymentCtrl    ]])
    
    .state(['root.contractReportChecks.view.checks'                   , '/checks'                                                                                                                                                                                                           ])
    .state(['root.contractReportChecks.view.checks.search'            , ''                  ,       ['@root.contractReportChecks.view', contractReportsChecksTemplateUrl                                , ContractReportChecksCtrl                        ]])
    .state(['root.contractReportChecks.view.checks.editTechnical'     , '/technical/:ind'   ,       ['@root.contractReportChecks.view', contractReportsChecksEditTechnicalTemplateUrl                   , ContractReportChecksEditTechnicalCtrl           ]])
    .state(['root.contractReportChecks.view.checks.editFinancial'     , '/financial/:ind'   ,       ['@root.contractReportChecks.view', contractReportsChecksEditFinancialTemplateUrl                   , ContractReportChecksEditFinancialCtrl           ]])
    
    .state(['root.contractReportChecks.view.csds'                     , '/csds?csd&company'                                                                                                                                                                                                 ])
    .state(['root.contractReportChecks.view.csds.search'              , ''                  ,       ['@root.contractReportChecks.view', contractReportsFinancialCSDBudgetItemsSearchTemplateUrl         , ContractReportsFinancialCSDBudgetItemsSearchCtrl]])
    .state(['root.contractReportChecks.view.csds.edit'                , '/:ind'             ,       ['@root.contractReportChecks.view', contractReportsFinancialCSDBudgetItemsEditTemplateUrl           , ContractReportsFinancialCSDBudgetItemsEditCtrl  ]])

    .state(['root.contractReportChecks.view.paymentChecks'            , '/paymentChecks'                                                                                                                                                                                                    ])
    .state(['root.contractReportChecks.view.paymentChecks.search'     , ''                  ,       ['@root.contractReportChecks.view', contractReportsPaymentChecksTemplateUrl                         , ContractReportPaymentChecksCtrl                 ]])
    .state(['root.contractReportChecks.view.paymentChecks.edit'       , '/:ind'             ,       ['@root.contractReportChecks.view', contractReportsChecksEditPaymentTemplateUrl                     , ContractReportChecksEditPaymentCtrl             ]])

    .state(['root.contractReportChecks.view.attachedDocs'             , '/attachedDocuments'                                                                                                                                                                                                ])
    .state(['root.contractReportChecks.view.attachedDocs.search'      , ''                  ,       ['@root.contractReportChecks.view', contractReportAttachedDocsTemplateUrl                           , ContractReportAttachedDocsCtrl                  ]])
    .state(['root.contractReportChecks.view.attachedDocs.viewFinCor'  , '/financialCor/:ind',       ['@root.contractReportChecks.view', contractReportAttachedDocFinancialCorrectionTemplateUrl         , ContractReportAttachedDocFinancialCorrectionCtrl]])

    .state(['root.contractReportChecks.view.paymentRequests'          , '/paymentRequests'                                                                                                                                                                                                  ])
    .state(['root.contractReportChecks.view.paymentRequests.view'     , ''                  ,       ['@root.contractReportChecks.view', contractReportsPaymentRequestsTemplateUrl                       , ContractReportsPaymentRequestsCtrl              ]])

    .state(['root.contractReportChecks.view.indicators'               , '/indicators'                                                                                                                                                                                                       ])
    .state(['root.contractReportChecks.view.indicators.search'        , ''                  ,       ['@root.contractReportChecks.view', contractReportCheckIndicatorsSearchTemplateUrl                  , ContractReportCheckIndicatorsSearchCtrl         ]])
    .state(['root.contractReportChecks.view.indicators.edit'          , '/:ind'             ,       ['@root.contractReportChecks.view', contractReportCheckIndicatorsEditTemplateUrl                    , ContractReportCheckIndicatorsEditCtrl           ]])

    .state(['root.contractReportChecks.view.advPaymentAmounts'        , '/advancePaymentAmounts'                                                                                                                                                                                            ])
    .state(['root.contractReportChecks.view.advPaymentAmounts.search' , ''                  ,       ['@root.contractReportChecks.view', contractReportsAdvancePaymentAmountsSearchTemplateUrl           , ContractReportsAdvancePaymentAmountsSearchCtrl  ]])
    .state(['root.contractReportChecks.view.advPaymentAmounts.edit'   , '/:ind'             ,       ['@root.contractReportChecks.view', contractReportsAdvancePaymentAmountsEditTemplateUrl             , ContractReportsAdvancePaymentAmountsEditCtrl    ]])

    .state(['root.contractReportChecks.view.advNVPaymentAmounts'      , '/advanceNVPaymentAmounts'                                                                                                                                                                                          ])
    .state(['root.contractReportChecks.view.advNVPaymentAmounts.search', ''                 ,       ['@root.contractReportChecks.view', contractReportsAdvanceNVPaymentAmountsSearchTemplateUrl         , ContractReportsAdvanceNVPaymentAmountsSearchCtrl]])
    .state(['root.contractReportChecks.view.advNVPaymentAmounts.edit' , '/:ind'             ,       ['@root.contractReportChecks.view', contractReportsAdvanceNVPaymentAmountsEditTemplateUrl           , ContractReportsAdvanceNVPaymentAmountsEditCtrl  ]]);
    }
  ]);

export default ContractReportChecksModule.name;
ContractReportChecksModule.factory(
  'ContractReportAdvanceNVPaymentAmount',
  ContractReportAdvanceNVPaymentAmountFactory
);
ContractReportChecksModule.factory(
  'ContractReportAdvancePaymentAmount',
  ContractReportAdvancePaymentAmountFactory
);
ContractReportChecksModule.factory(
  'ContractReportAttachedFinancialCorrection',
  ContractReportAttachedFinancialCorrectionFactory
);
ContractReportChecksModule.factory('ContractReportCheckFile', ContractReportCheckFileFactory);
ContractReportChecksModule.factory(
  'ContractReportFinancialCheck',
  ContractReportFinancialCheckFactory
);
ContractReportChecksModule.factory(
  'ContractReportFinancialCSDBudgetItem',
  ContractReportFinancialCSDBudgetItemFactory
);
ContractReportChecksModule.factory(
  'ContractReportFinancialCSDFile',
  ContractReportFinancialCSDFileFactory
);
ContractReportChecksModule.factory('ContractReportIndicator', ContractReportIndicatorFactory);
ContractReportChecksModule.factory('ContractReportPaymentCheck', ContractReportPaymentCheckFactory);
ContractReportChecksModule.factory(
  'ContractReportPaymentRequest',
  ContractReportPaymentRequestFactory
);
ContractReportChecksModule.factory(
  'ContractReportTechnicalCheck',
  ContractReportTechnicalCheckFactory
);
