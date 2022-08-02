import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractDebtTemplateUrl from './forms/contractDebt.html';
import contractDebtInterestTemplateUrl from './forms/contractDebtInterest.html';
import { ContractDebtInterestCtrl } from './forms/contractDebtInterestCtrl';
import contractDebtVersionTemplateUrl from './forms/contractDebtVersion.html';
import { ContractDebtVersionCtrl } from './forms/contractDebtVersionCtrl';
import correctionDebtTemplateUrl from './forms/correctionDebt.html';
import correctionDebtVersionTemplateUrl from './forms/correctionDebtVersion.html';
import { CorrectionDebtVersionCtrl } from './forms/correctionDebtVersionCtrl';
import chooseContractDebtContractModalTemplateUrl from './modals/chooseContractDebtContractModal.html';
import { ChooseContractDebtContractModalCtrl } from './modals/chooseContractDebtContractModalCtrl';
import chooseCorrectionDebtContractModalTemplateUrl from './modals/chooseCorrectionDebtContractModal.html';
import { ChooseCorrectionDebtContractModalCtrl } from './modals/chooseCorrectionDebtContractModalCtrl';
import { ContractDebtFactory } from './resources/contractDebt';
import { ContractDebtInterestFactory } from './resources/contractDebtInterest';
import { ContractDebtVersionFactory } from './resources/contractDebtVersion';
import { CorrectionDebtFactory } from './resources/correctionDebt';
import { CorrectionDebtVersionFactory } from './resources/correctionDebtVersion';
import contractDebtInterestsEditTemplateUrl from './views/contractDebtInterestsEdit.html';
import { ContractDebtInterestsEditCtrl } from './views/contractDebtInterestsEditCtrl';
import contractDebtInterestsNewTemplateUrl from './views/contractDebtInterestsNew.html';
import { ContractDebtInterestsNewCtrl } from './views/contractDebtInterestsNewCtrl';
import contractDebtInterestsSearchTemplateUrl from './views/contractDebtInterestsSearch.html';
import { ContractDebtInterestsSearchCtrl } from './views/contractDebtInterestsSearchCtrl';
import contractDebtsEditTemplateUrl from './views/contractDebtsEdit.html';
import { ContractDebtsEditCtrl } from './views/contractDebtsEditCtrl';
import contractDebtsNewStep1TemplateUrl from './views/contractDebtsNewStep1.html';
import { ContractDebtsNewStep1Ctrl } from './views/contractDebtsNewStep1Ctrl';
import contractDebtsNewStep2TemplateUrl from './views/contractDebtsNewStep2.html';
import { ContractDebtsNewStep2Ctrl } from './views/contractDebtsNewStep2Ctrl';
import contractDebtsReportTemplateUrl from './views/contractDebtsReport.html';
import { ContractDebtsReportCtrl } from './views/contractDebtsReportCtrl';
import contractDebtsSearchTemplateUrl from './views/contractDebtsSearch.html';
import { ContractDebtsSearchCtrl } from './views/contractDebtsSearchCtrl';
import contractDebtsViewTemplateUrl from './views/contractDebtsView.html';
import { ContractDebtsViewCtrl } from './views/contractDebtsViewCtrl';
import contractDebtVersionsTemplateUrl from './views/contractDebtVersions.html';
import { ContractDebtVersionsCtrl } from './views/contractDebtVersionsCtrl';
import contractDebtVersionsEditTemplateUrl from './views/contractDebtVersionsEdit.html';
import { ContractDebtVersionsEditCtrl } from './views/contractDebtVersionsEditCtrl';
import correctionDebtsEditTemplateUrl from './views/correctionDebtsEdit.html';
import { CorrectionDebtsEditCtrl } from './views/correctionDebtsEditCtrl';
import correctionDebtsNewStep1TemplateUrl from './views/correctionDebtsNewStep1.html';
import { CorrectionDebtsNewStep1Ctrl } from './views/correctionDebtsNewStep1Ctrl';
import correctionDebtsNewStep2TemplateUrl from './views/correctionDebtsNewStep2.html';
import { CorrectionDebtsNewStep2Ctrl } from './views/correctionDebtsNewStep2Ctrl';
import correctionDebtsReportTemplateUrl from './views/correctionDebtsReport.html';
import { CorrectionDebtsReportCtrl } from './views/correctionDebtsReportCtrl';
import correctionDebtsSearchTemplateUrl from './views/correctionDebtsSearch.html';
import { CorrectionDebtsSearchCtrl } from './views/correctionDebtsSearchCtrl';
import correctionDebtsViewTemplateUrl from './views/correctionDebtsView.html';
import { CorrectionDebtsViewCtrl } from './views/correctionDebtsViewCtrl';
import correctionDebtVersionsTemplateUrl from './views/correctionDebtVersions.html';
import { CorrectionDebtVersionsCtrl } from './views/correctionDebtVersionsCtrl';
import correctionDebtVersionsEditTemplateUrl from './views/correctionDebtVersionsEdit.html';
import { CorrectionDebtVersionsEditCtrl } from './views/correctionDebtVersionsEditCtrl';

const DebtsModule = angular
  .module('main.debts', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractDebt',
        templateUrl: contractDebtTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractDebtVersion',
        templateUrl: contractDebtVersionTemplateUrl,
        controller: ContractDebtVersionCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractDebtInterest',
        templateUrl: contractDebtInterestTemplateUrl,
        controller: ContractDebtInterestCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisCorrectionDebt',
        templateUrl: correctionDebtTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisCorrectionDebtVersion',
        templateUrl: correctionDebtVersionTemplateUrl,
        controller: CorrectionDebtVersionCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseContractDebtContractModal'        , chooseContractDebtContractModalTemplateUrl                         , ChooseContractDebtContractModalCtrl        , 'xlg')
    .modal('chooseCorrectionDebtContractModal'      , chooseCorrectionDebtContractModalTemplateUrl                       , ChooseCorrectionDebtContractModalCtrl      , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractDebts'                                      , '/contractDebts'                                                                                                                                                                                                    ])
    .state(['root.contractDebts.search'                               , ''                 ,       ['@root'                           , contractDebtsSearchTemplateUrl                                                 , ContractDebtsSearchCtrl                         ]])
    .state(['root.contractDebts.newStep1'                             , '/newStep1'        ,       ['@root'                           , contractDebtsNewStep1TemplateUrl                                               , ContractDebtsNewStep1Ctrl                       ]])
    .state(['root.contractDebts.newStep2'                             , '/newStep2?cNum'   ,       ['@root'                           , contractDebtsNewStep2TemplateUrl                                               , ContractDebtsNewStep2Ctrl                       ]])
    .state(['root.contractDebts.view'                                 , '/:id?rf'          , true, ['@root'                           , contractDebtsViewTemplateUrl                                                   , ContractDebtsViewCtrl                           ]])
    .state(['root.contractDebts.view.edit'                            , ''                 ,       ['@root.contractDebts.view'        , contractDebtsEditTemplateUrl                                                   , ContractDebtsEditCtrl                           ]])
    .state(['root.contractDebts.view.versions'                        , '/versions'        ,       ['@root.contractDebts.view'        , contractDebtVersionsTemplateUrl                                                , ContractDebtVersionsCtrl                        ]])
    .state(['root.contractDebts.view.versions.edit'                   , '/:ind'            ,       ['@root.contractDebts.view'        , contractDebtVersionsEditTemplateUrl                                            , ContractDebtVersionsEditCtrl                    ]])
    .state(['root.contractDebts.view.interests'                       , '/interests'                                                                                                                                                                                                        ])
    .state(['root.contractDebts.view.interests.search'                , ''                 ,       ['@root.contractDebts.view'        , contractDebtInterestsSearchTemplateUrl                                         , ContractDebtInterestsSearchCtrl                 ]])
    .state(['root.contractDebts.view.interests.new'                   , '/new'             ,       ['@root.contractDebts.view'        , contractDebtInterestsNewTemplateUrl                                            , ContractDebtInterestsNewCtrl                    ]])
    .state(['root.contractDebts.view.interests.edit'                  , '/:ind'            ,       ['@root.contractDebts.view'        , contractDebtInterestsEditTemplateUrl                                           , ContractDebtInterestsEditCtrl                   ]])
    .state(['root.contractDebtsReport'                                , '/contractDebtsReport',    ['@root'                           , contractDebtsReportTemplateUrl                                                 , ContractDebtsReportCtrl                         ]])

    .state(['root.correctionDebts'                                    , '/correctionDebts'                                                                                                                                                                                                  ])
    .state(['root.correctionDebts.search'                             , ''                 ,       ['@root'                           , correctionDebtsSearchTemplateUrl                                               , CorrectionDebtsSearchCtrl                       ]])
    .state(['root.correctionDebts.newStep1'                           , '/newStep1'        ,       ['@root'                           , correctionDebtsNewStep1TemplateUrl                                             , CorrectionDebtsNewStep1Ctrl                     ]])
    .state(['root.correctionDebts.newStep2'                           , '/newStep2?cId'    ,       ['@root'                           , correctionDebtsNewStep2TemplateUrl                                             , CorrectionDebtsNewStep2Ctrl                     ]])
    .state(['root.correctionDebts.view'                               , '/:id?rf'          , true, ['@root'                           , correctionDebtsViewTemplateUrl                                                 , CorrectionDebtsViewCtrl                         ]])
    .state(['root.correctionDebts.view.edit'                          , ''                 ,       ['@root.correctionDebts.view'      , correctionDebtsEditTemplateUrl                                                 , CorrectionDebtsEditCtrl                         ]])
    .state(['root.correctionDebts.view.versions'                      , '/versions'        ,       ['@root.correctionDebts.view'      , correctionDebtVersionsTemplateUrl                                              , CorrectionDebtVersionsCtrl                      ]])
    .state(['root.correctionDebts.view.versions.edit'                 , '/:ind'            ,       ['@root.correctionDebts.view'      , correctionDebtVersionsEditTemplateUrl                                          , CorrectionDebtVersionsEditCtrl                  ]])
    .state(['root.correctionDebtsReport'                              , '/correctionDebtsReport',  ['@root'                           , correctionDebtsReportTemplateUrl                                               , CorrectionDebtsReportCtrl                       ]]);
    }
  ]);

export default DebtsModule.name;
DebtsModule.factory('ContractDebt', ContractDebtFactory);
DebtsModule.factory('ContractDebtInterest', ContractDebtInterestFactory);
DebtsModule.factory('ContractDebtVersion', ContractDebtVersionFactory);
DebtsModule.factory('CorrectionDebt', CorrectionDebtFactory);
DebtsModule.factory('CorrectionDebtVersion', CorrectionDebtVersionFactory);
