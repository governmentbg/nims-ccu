import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractReportFinancialCorrectionTemplateUrl from './forms/contractReportFinancialCorrection.html';
import contractReportFinancialCorrectionCSDTemplateUrl from './forms/contractReportFinancialCorrectionCSD.html';
import { ContractReportFinancialCorrectionCSDCtrl } from './forms/contractReportFinancialCorrectionCSDCtrl';
import chooseCRFCContractReportModalTemplateUrl from './modals/chooseCRFCContractReportModal.html';
import { ChooseCRFCContractReportModalCtrl } from './modals/chooseCRFCContractReportModalCtrl';
import correctionCSDBudgetItemModalTemplateUrl from './modals/correctionCSDBudgetItemModal.html';
import { CorrectionCSDBudgetItemModalCtrl } from './modals/correctionCSDBudgetItemModalCtrl';
import { ContractReportFinancialCorrectionFactory } from './resources/contractReportFinancialCorrection';
import { ContractReportFinancialCorrectionCSDFactory } from './resources/contractReportFinancialCorrectionCSD';
import { ContractReportFinancialCorrectionFileFactory } from './resources/contractReportFinancialCorrectionFile';
import contractReportFinancialCorrectionsContractViewTemplateUrl from './views/contractReportFinancialCorrectionsContractView.html';
import { ContractReportFinancialCorrectionsContractViewCtrl } from './views/contractReportFinancialCorrectionsContractViewCtrl';
import contractReportFinancialCorrectionsCorrectedCSDsEditTemplateUrl from './views/contractReportFinancialCorrectionsCorrectedCSDsEdit.html';
import { ContractReportFinancialCorrectionsCorrectedCSDsEditCtrl } from './views/contractReportFinancialCorrectionsCorrectedCSDsEditCtrl';
import contractReportFinancialCorrectionsCorrectedCSDsSearchTemplateUrl from './views/contractReportFinancialCorrectionsCorrectedCSDsSearch.html';
import { ContractReportFinancialCorrectionsCorrectedCSDsSearchCtrl } from './views/contractReportFinancialCorrectionsCorrectedCSDsSearchCtrl';
import contractReportFinancialCorrectionsCSDsSearchTemplateUrl from './views/contractReportFinancialCorrectionsCSDsSearch.html';
import { ContractReportFinancialCorrectionsCSDsSearchCtrl } from './views/contractReportFinancialCorrectionsCSDsSearchCtrl';
import contractReportFinancialCorrectionsCSDsViewTemplateUrl from './views/contractReportFinancialCorrectionsCSDsView.html';
import { ContractReportFinancialCorrectionsCSDsViewCtrl } from './views/contractReportFinancialCorrectionsCSDsViewCtrl';
import contractReportFinancialCorrectionsEditTemplateUrl from './views/contractReportFinancialCorrectionsEdit.html';
import { ContractReportFinancialCorrectionsEditCtrl } from './views/contractReportFinancialCorrectionsEditCtrl';
import contractReportFinancialCorrectionsNewTemplateUrl from './views/contractReportFinancialCorrectionsNew.html';
import { ContractReportFinancialCorrectionsNewCtrl } from './views/contractReportFinancialCorrectionsNewCtrl';
import contractReportFinancialCorrectionsReportViewTemplateUrl from './views/contractReportFinancialCorrectionsReportView.html';
import { ContractReportFinancialCorrectionsReportViewCtrl } from './views/contractReportFinancialCorrectionsReportViewCtrl';
import contractReportFinancialCorrectionsSearchTemplateUrl from './views/contractReportFinancialCorrectionsSearch.html';
import { ContractReportFinancialCorrectionsSearchCtrl } from './views/contractReportFinancialCorrectionsSearchCtrl';
import contractReportFinancialCorrectionsViewTemplateUrl from './views/contractReportFinancialCorrectionsView.html';
import { ContractReportFinancialCorrectionsViewCtrl } from './views/contractReportFinancialCorrectionsViewCtrl';

const ContractReportFinancialCorrectionsModule = angular
  .module('main.contractReportFinancialCorrections', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractReportFinancialCorrection',
        templateUrl: contractReportFinancialCorrectionTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportFinancialCorrectionCostSupportingDocument',
        templateUrl: contractReportFinancialCorrectionCSDTemplateUrl,
        controller: ContractReportFinancialCorrectionCSDCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('correctionCSDBudgetItemModal'           , correctionCSDBudgetItemModalTemplateUrl      , CorrectionCSDBudgetItemModalCtrl    , 'xlg')
    .modal('chooseCRFCContractReportModal'          , chooseCRFCContractReportModalTemplateUrl     , ChooseCRFCContractReportModalCtrl   , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractReportFinancialCorrections'                               , '/contractReportFinancialCorrections?contractRegNum&fromDate&toDate'                                                                                                                                                                                      ])
    .state(['root.contractReportFinancialCorrections.search'                        , ''                  ,       ['@root'                                        , contractReportFinancialCorrectionsSearchTemplateUrl             , ContractReportFinancialCorrectionsSearchCtrl              ]])
    .state(['root.contractReportFinancialCorrections.new'                           , '/new'              ,       ['@root'                                        , contractReportFinancialCorrectionsNewTemplateUrl                , ContractReportFinancialCorrectionsNewCtrl                 ]])
    .state(['root.contractReportFinancialCorrections.view'                          , '/:id?rf'           , true, ['@root'                                        , contractReportFinancialCorrectionsViewTemplateUrl               , ContractReportFinancialCorrectionsViewCtrl                ]])
    .state(['root.contractReportFinancialCorrections.view.contract'                 , ''                  ,       ['@root.contractReportFinancialCorrections.view', contractReportFinancialCorrectionsContractViewTemplateUrl       , ContractReportFinancialCorrectionsContractViewCtrl        ]])
    .state(['root.contractReportFinancialCorrections.view.report'                   , '/report'           ,       ['@root.contractReportFinancialCorrections.view', contractReportFinancialCorrectionsReportViewTemplateUrl         , ContractReportFinancialCorrectionsReportViewCtrl          ]])
    .state(['root.contractReportFinancialCorrections.view.data'                     , '/correction'       ,       ['@root.contractReportFinancialCorrections.view', contractReportFinancialCorrectionsEditTemplateUrl               , ContractReportFinancialCorrectionsEditCtrl                ]])

    .state(['root.contractReportFinancialCorrections.view.csds'                     , '/csds?csd&company'                                                                                                                                                                                                                                       ])
    .state(['root.contractReportFinancialCorrections.view.csds.search'              , ''                  ,       ['@root.contractReportFinancialCorrections.view', contractReportFinancialCorrectionsCSDsSearchTemplateUrl         , ContractReportFinancialCorrectionsCSDsSearchCtrl          ]])
    .state(['root.contractReportFinancialCorrections.view.csds.view'                , '/:ind'             ,       ['@root.contractReportFinancialCorrections.view', contractReportFinancialCorrectionsCSDsViewTemplateUrl           , ContractReportFinancialCorrectionsCSDsViewCtrl            ]])

    .state(['root.contractReportFinancialCorrections.view.correctedCsds'            , '/correctedCsds?csd&company'                                                                                                                                                                                                                              ])
    .state(['root.contractReportFinancialCorrections.view.correctedCsds.search'     , ''                  ,       ['@root.contractReportFinancialCorrections.view', contractReportFinancialCorrectionsCorrectedCSDsSearchTemplateUrl, ContractReportFinancialCorrectionsCorrectedCSDsSearchCtrl ]])
    .state(['root.contractReportFinancialCorrections.view.correctedCsds.edit'       , '/:ind'             ,       ['@root.contractReportFinancialCorrections.view', contractReportFinancialCorrectionsCorrectedCSDsEditTemplateUrl  , ContractReportFinancialCorrectionsCorrectedCSDsEditCtrl   ]])
    }
  ]);

export default ContractReportFinancialCorrectionsModule.name;
ContractReportFinancialCorrectionsModule.factory(
  'ContractReportFinancialCorrection',
  ContractReportFinancialCorrectionFactory
);
ContractReportFinancialCorrectionsModule.factory(
  'ContractReportFinancialCorrectionCSD',
  ContractReportFinancialCorrectionCSDFactory
);
ContractReportFinancialCorrectionsModule.factory(
  'ContractReportFinancialCorrectionFile',
  ContractReportFinancialCorrectionFileFactory
);
