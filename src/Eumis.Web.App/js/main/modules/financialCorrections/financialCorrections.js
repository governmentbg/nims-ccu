import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import financialCorrectionTemplateUrl from './forms/financialCorrection.html';
import financialCorrectionVersionTemplateUrl from './forms/financialCorrectionVersion.html';
import { FinancialCorrectionVersionCtrl } from './forms/financialCorrectionVersionCtrl';
import chooseFinancialCorrectionContractModalTemplateUrl from './modals/chooseFinancialCorrectionContractModal.html';
import { ChooseFinancialCorrectionContractModalCtrl } from './modals/chooseFinancialCorrectionContractModalCtrl';
import { FinancialCorrectionFactory } from './resources/financialCorrection';
import { FinancialCorrectionVersionFactory } from './resources/financialCorrectionVersion';
import { FinancialCorrectionVersionFileFactory } from './resources/financialCorrectionVersionFile';
import financialCorrectionAttachedDocsTemplateUrl from './views/financialCorrectionAttachedDocs.html';
import { FinancialCorrectionAttachedDocsCtrl } from './views/financialCorrectionAttachedDocsCtrl';
import financialCorrectionsEditTemplateUrl from './views/financialCorrectionsEdit.html';
import { FinancialCorrectionsEditCtrl } from './views/financialCorrectionsEditCtrl';
import financialCorrectionsNewStep1TemplateUrl from './views/financialCorrectionsNewStep1.html';
import { FinancialCorrectionsNewStep1Ctrl } from './views/financialCorrectionsNewStep1Ctrl';
import financialCorrectionsNewStep2TemplateUrl from './views/financialCorrectionsNewStep2.html';
import { FinancialCorrectionsNewStep2Ctrl } from './views/financialCorrectionsNewStep2Ctrl';
import financialCorrectionsSearchTemplateUrl from './views/financialCorrectionsSearch.html';
import { FinancialCorrectionsSearchCtrl } from './views/financialCorrectionsSearchCtrl';
import financialCorrectionsViewTemplateUrl from './views/financialCorrectionsView.html';
import { FinancialCorrectionsViewCtrl } from './views/financialCorrectionsViewCtrl';
import financialCorrectionVersionsEditTemplateUrl from './views/financialCorrectionVersionsEdit.html';
import { FinancialCorrectionVersionsEditCtrl } from './views/financialCorrectionVersionsEditCtrl';
import financialCorrectionVersionsSearchTemplateUrl from './views/financialCorrectionVersionsSearch.html';
import { FinancialCorrectionVersionsSearchCtrl } from './views/financialCorrectionVersionsSearchCtrl';

const FinancialCorrectionsModule = angular
  .module('main.financialCorrections', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisFinancialCorrection',
        templateUrl: financialCorrectionTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisFinancialCorrectionVersion',
        templateUrl: financialCorrectionVersionTemplateUrl,
        controller: FinancialCorrectionVersionCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseFinancialCorrectionContractModal' , chooseFinancialCorrectionContractModalTemplateUrl   , ChooseFinancialCorrectionContractModalCtrl , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.financialCorrections'                               , '/financialCorrections'                                                                                                                                                                                             ])
    .state(['root.financialCorrections.search'                        , ''                 ,       ['@root'                           , financialCorrectionsSearchTemplateUrl                           , FinancialCorrectionsSearchCtrl                  ]])
    .state(['root.financialCorrections.newStep1'                      , '/newStep1'        ,       ['@root'                           , financialCorrectionsNewStep1TemplateUrl                         , FinancialCorrectionsNewStep1Ctrl                ]])
    .state(['root.financialCorrections.newStep2'                      , '/newStep2?cNum'   ,       ['@root'                           , financialCorrectionsNewStep2TemplateUrl                         , FinancialCorrectionsNewStep2Ctrl                ]])
    .state(['root.financialCorrections.view'                          , '/:id?rf'          , true, ['@root'                           , financialCorrectionsViewTemplateUrl                             , FinancialCorrectionsViewCtrl                    ]])
    .state(['root.financialCorrections.view.edit'                     , ''                 ,       ['@root.financialCorrections.view' , financialCorrectionsEditTemplateUrl                             , FinancialCorrectionsEditCtrl                    ]])
    .state(['root.financialCorrections.view.versions'                 , '/versions'                                                                                                                                                                                                         ])
    .state(['root.financialCorrections.view.versions.search'          , ''                 ,       ['@root.financialCorrections.view' , financialCorrectionVersionsSearchTemplateUrl                    , FinancialCorrectionVersionsSearchCtrl           ]])
    .state(['root.financialCorrections.view.versions.edit'            , '/:ind'            ,       ['@root.financialCorrections.view' , financialCorrectionVersionsEditTemplateUrl                      , FinancialCorrectionVersionsEditCtrl             ]])
    .state(['root.financialCorrections.view.attachedDocs'             , '/attachedDocs'    ,       ['@root.financialCorrections.view' , financialCorrectionAttachedDocsTemplateUrl                      , FinancialCorrectionAttachedDocsCtrl             ]]);
    }
  ]);

export default FinancialCorrectionsModule.name;
FinancialCorrectionsModule.factory('FinancialCorrection', FinancialCorrectionFactory);
FinancialCorrectionsModule.factory('FinancialCorrectionVersion', FinancialCorrectionVersionFactory);
FinancialCorrectionsModule.factory(
  'FinancialCorrectionVersionFile',
  FinancialCorrectionVersionFileFactory
);
