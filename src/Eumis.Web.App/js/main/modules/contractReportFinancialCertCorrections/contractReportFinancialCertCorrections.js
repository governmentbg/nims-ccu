import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractReportFinancialCertCorrectionTemplateUrl from './forms/contractReportFinancialCertCorrection.html';
import contractReportFinancialCertCorrectionCSDTemplateUrl from './forms/contractReportFinancialCertCorrectionCSD.html';
import { ContractReportFinancialCertCorrectionCSDCtrl } from './forms/contractReportFinancialCertCorrectionCSDCtrl';
import chooseCRFCCContractReportModalTemplateUrl from './modals/chooseCRFCCContractReportModal.html';
import { ChooseCRFCCContractReportModalCtrl } from './modals/chooseCRFCCContractReportModalCtrl';
import { ContractReportFinancialCertCorrectionFactory } from './resources/contractReportFinancialCertCorrection';
import { ContractReportFinancialCertCorrectionCSDFactory } from './resources/contractReportFinancialCertCorrectionCSD';
import { ContractReportFinancialCertCorrectionFileFactory } from './resources/contractReportFinancialCertCorrectionFile';
import contractReportFinancialCertCorrectionsContractViewTemplateUrl from './views/contractReportFinancialCertCorrectionsContractView.html';
import { ContractReportFinancialCertCorrectionsContractViewCtrl } from './views/contractReportFinancialCertCorrectionsContractViewCtrl';
import contractReportFinancialCertCorrectionsCorrectedCSDsEditTemplateUrl from './views/contractReportFinancialCertCorrectionsCorrectedCSDsEdit.html';
import { ContractReportFinancialCertCorrectionsCorrectedCSDsEditCtrl } from './views/contractReportFinancialCertCorrectionsCorrectedCSDsEditCtrl';
import contractReportFinancialCertCorrectionsCorrectedCSDsSearchTemplateUrl from './views/contractReportFinancialCertCorrectionsCorrectedCSDsSearch.html';
import { ContractReportFinancialCertCorrectionsCorrectedCSDsSearchCtrl } from './views/contractReportFinancialCertCorrectionsCorrectedCSDsSearchCtrl';
import contractReportFinancialCertCorrectionsCSDsSearchTemplateUrl from './views/contractReportFinancialCertCorrectionsCSDsSearch.html';
import { ContractReportFinancialCertCorrectionsCSDsSearchCtrl } from './views/contractReportFinancialCertCorrectionsCSDsSearchCtrl';
import contractReportFinancialCertCorrectionsCSDsViewTemplateUrl from './views/contractReportFinancialCertCorrectionsCSDsView.html';
import { ContractReportFinancialCertCorrectionsCSDsViewCtrl } from './views/contractReportFinancialCertCorrectionsCSDsViewCtrl';
import contractReportFinancialCertCorrectionsEditTemplateUrl from './views/contractReportFinancialCertCorrectionsEdit.html';
import { ContractReportFinancialCertCorrectionsEditCtrl } from './views/contractReportFinancialCertCorrectionsEditCtrl';
import contractReportFinancialCertCorrectionsNewTemplateUrl from './views/contractReportFinancialCertCorrectionsNew.html';
import { ContractReportFinancialCertCorrectionsNewCtrl } from './views/contractReportFinancialCertCorrectionsNewCtrl';
import contractReportFinancialCertCorrectionsReportViewTemplateUrl from './views/contractReportFinancialCertCorrectionsReportView.html';
import { ContractReportFinancialCertCorrectionsReportViewCtrl } from './views/contractReportFinancialCertCorrectionsReportViewCtrl';
import contractReportFinancialCertCorrectionsSearchTemplateUrl from './views/contractReportFinancialCertCorrectionsSearch.html';
import { ContractReportFinancialCertCorrectionsSearchCtrl } from './views/contractReportFinancialCertCorrectionsSearchCtrl';
import contractReportFinancialCertCorrectionsViewTemplateUrl from './views/contractReportFinancialCertCorrectionsView.html';
import { ContractReportFinancialCertCorrectionsViewCtrl } from './views/contractReportFinancialCertCorrectionsViewCtrl';

const ContractReportFinancialCertCorrectionsModule = angular
  .module('main.contractReportFinancialCertCorrections', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractReportFinancialCertCorrection',
        templateUrl: contractReportFinancialCertCorrectionTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportFinancialCertCorrectionCostSupportingDocument',
        templateUrl: contractReportFinancialCertCorrectionCSDTemplateUrl,
        controller: ContractReportFinancialCertCorrectionCSDCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseCRFCCContractReportModal'         , chooseCRFCCContractReportModalTemplateUrl, ChooseCRFCCContractReportModalCtrl  , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractReportFinancialCertCorrections'                          , '/contractReportFinancialCertCorrections?contractRegNum&fromDate&toDate'                                                                                                                                                                                                  ])
    .state(['root.contractReportFinancialCertCorrections.search'                   , ''                  ,       ['@root'                                            , contractReportFinancialCertCorrectionsSearchTemplateUrl             , ContractReportFinancialCertCorrectionsSearchCtrl              ]])
    .state(['root.contractReportFinancialCertCorrections.new'                      , '/new'              ,       ['@root'                                            , contractReportFinancialCertCorrectionsNewTemplateUrl                , ContractReportFinancialCertCorrectionsNewCtrl                 ]])
    .state(['root.contractReportFinancialCertCorrections.view'                     , '/:id?rf'           , true, ['@root'                                            , contractReportFinancialCertCorrectionsViewTemplateUrl               , ContractReportFinancialCertCorrectionsViewCtrl                ]])
    .state(['root.contractReportFinancialCertCorrections.view.contract'            , ''                  ,       ['@root.contractReportFinancialCertCorrections.view', contractReportFinancialCertCorrectionsContractViewTemplateUrl       , ContractReportFinancialCertCorrectionsContractViewCtrl        ]])
    .state(['root.contractReportFinancialCertCorrections.view.report'              , '/report'           ,       ['@root.contractReportFinancialCertCorrections.view', contractReportFinancialCertCorrectionsReportViewTemplateUrl         , ContractReportFinancialCertCorrectionsReportViewCtrl          ]])
    .state(['root.contractReportFinancialCertCorrections.view.data'                , '/correction'       ,       ['@root.contractReportFinancialCertCorrections.view', contractReportFinancialCertCorrectionsEditTemplateUrl               , ContractReportFinancialCertCorrectionsEditCtrl                ]])
    .state(['root.contractReportFinancialCertCorrections.view.csds'                , '/csds?csd&company'                                                                                                                                                                                                                                                       ])
    .state(['root.contractReportFinancialCertCorrections.view.csds.search'         , ''                  ,       ['@root.contractReportFinancialCertCorrections.view', contractReportFinancialCertCorrectionsCSDsSearchTemplateUrl         , ContractReportFinancialCertCorrectionsCSDsSearchCtrl          ]])
    .state(['root.contractReportFinancialCertCorrections.view.csds.view'           , '/:ind'             ,       ['@root.contractReportFinancialCertCorrections.view', contractReportFinancialCertCorrectionsCSDsViewTemplateUrl           , ContractReportFinancialCertCorrectionsCSDsViewCtrl            ]])
    .state(['root.contractReportFinancialCertCorrections.view.correctedCsds'       , '/correctedCsds?csd&company'                                                                                                                                                                                                                                              ])
    .state(['root.contractReportFinancialCertCorrections.view.correctedCsds.search', ''                  ,       ['@root.contractReportFinancialCertCorrections.view', contractReportFinancialCertCorrectionsCorrectedCSDsSearchTemplateUrl, ContractReportFinancialCertCorrectionsCorrectedCSDsSearchCtrl ]])
    .state(['root.contractReportFinancialCertCorrections.view.correctedCsds.edit'  , '/:ind'             ,       ['@root.contractReportFinancialCertCorrections.view', contractReportFinancialCertCorrectionsCorrectedCSDsEditTemplateUrl  , ContractReportFinancialCertCorrectionsCorrectedCSDsEditCtrl   ]]);
    }
  ]);

export default ContractReportFinancialCertCorrectionsModule.name;
ContractReportFinancialCertCorrectionsModule.factory(
  'ContractReportFinancialCertCorrection',
  ContractReportFinancialCertCorrectionFactory
);
ContractReportFinancialCertCorrectionsModule.factory(
  'ContractReportFinancialCertCorrectionCSD',
  ContractReportFinancialCertCorrectionCSDFactory
);
ContractReportFinancialCertCorrectionsModule.factory(
  'ContractReportFinancialCertCorrectionFile',
  ContractReportFinancialCertCorrectionFileFactory
);
