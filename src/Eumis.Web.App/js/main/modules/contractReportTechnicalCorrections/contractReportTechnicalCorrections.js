import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractReportTechnicalCorrectionTemplateUrl from './forms/contractReportTechnicalCorrection.html';
import contractReportTechnicalCorrectionIndicatorTemplateUrl from './forms/contractReportTechnicalCorrectionIndicator.html';
import { ContractReportTechnicalCorrectionIndicatorCtrl } from './forms/contractReportTechnicalCorrectionIndicatorCtrl';
import chooseCRTCContractReportModalTemplateUrl from './modals/chooseCRTCContractReportModal.html';
import { ChooseCRTCContractReportModalCtrl } from './modals/chooseCRTCContractReportModalCtrl';
import { ContractReportTechnicalCorrectionFactory } from './resources/contractReportTechnicalCorrection';
import { ContractReportTechnicalCorrectionIndicatorFactory } from './resources/contractReportTechnicalCorrectionIndicator';
import { ContractReportTechnicalCorrectionFileFactory } from './resources/contractReportTechnicalCorrectionFile';

import contractReportTechnicalCorrectionsContractViewTemplateUrl from './views/contractReportTechnicalCorrectionsContractView.html';
import { ContractReportTechnicalCorrectionsContractViewCtrl } from './views/contractReportTechnicalCorrectionsContractViewCtrl';
import contractReportTechnicalCorrectionsCorrectedIndicatorsEditTemplateUrl from './views/contractReportTechnicalCorrectionsCorrectedIndicatorsEdit.html';
import { ContractReportTechnicalCorrectionsCorrectedIndicatorsEditCtrl } from './views/contractReportTechnicalCorrectionsCorrectedIndicatorsEditCtrl';
import contractReportTechnicalCorrectionsCorrectedIndicatorsSearchTemplateUrl from './views/contractReportTechnicalCorrectionsCorrectedIndicatorsSearch.html';
import { ContractReportTechnicalCorrectionsCorrectedIndicatorsSearchCtrl } from './views/contractReportTechnicalCorrectionsCorrectedIndicatorsSearchCtrl';
import contractReportTechnicalCorrectionsIndicatorsSearchTemplateUrl from './views/contractReportTechnicalCorrectionsIndicatorsSearch.html';
import { ContractReportTechnicalCorrectionsIndicatorsSearchCtrl } from './views/contractReportTechnicalCorrectionsIndicatorsSearchCtrl';
import contractReportTechnicalCorrectionsIndicatorsViewTemplateUrl from './views/contractReportTechnicalCorrectionsIndicatorsView.html';
import { ContractReportTechnicalCorrectionsIndicatorsViewCtrl } from './views/contractReportTechnicalCorrectionsIndicatorsViewCtrl';
import contractReportTechnicalCorrectionsEditTemplateUrl from './views/contractReportTechnicalCorrectionsEdit.html';
import { ContractReportTechnicalCorrectionsEditCtrl } from './views/contractReportTechnicalCorrectionsEditCtrl';
import contractReportTechnicalCorrectionsNewTemplateUrl from './views/contractReportTechnicalCorrectionsNew.html';
import { ContractReportTechnicalCorrectionsNewCtrl } from './views/contractReportTechnicalCorrectionsNewCtrl';
import contractReportTechnicalCorrectionsReportViewTemplateUrl from './views/contractReportTechnicalCorrectionsReportView.html';
import { ContractReportTechnicalCorrectionsReportViewCtrl } from './views/contractReportTechnicalCorrectionsReportViewCtrl';
import contractReportTechnicalCorrectionsSearchTemplateUrl from './views/contractReportTechnicalCorrectionsSearch.html';
import { ContractReportTechnicalCorrectionsSearchCtrl } from './views/contractReportTechnicalCorrectionsSearchCtrl';
import contractReportTechnicalCorrectionsViewTemplateUrl from './views/contractReportTechnicalCorrectionsView.html';
import { ContractReportTechnicalCorrectionsViewCtrl } from './views/contractReportTechnicalCorrectionsViewCtrl';

const ContractReportTechnicalCorrectionsModule = angular
  .module('main.contractReportTechnicalCorrections', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractReportTechnicalCorrection',
        templateUrl: contractReportTechnicalCorrectionTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportTechnicalCorrectionIndicator',
        templateUrl: contractReportTechnicalCorrectionIndicatorTemplateUrl,
        controller: ContractReportTechnicalCorrectionIndicatorCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseCRTCContractReportModal'          , chooseCRTCContractReportModalTemplateUrl     , ChooseCRTCContractReportModalCtrl   , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractReportTechnicalCorrections'                                , '/contractReportTechnicalCorrections?contractRegNum&fromDate&toDate'                                                                                                                                                   ])
    .state(['root.contractReportTechnicalCorrections.search'                         , ''                  ,       ['@root'                                        , contractReportTechnicalCorrectionsSearchTemplateUrl             , ContractReportTechnicalCorrectionsSearchCtrl                          ]])
    .state(['root.contractReportTechnicalCorrections.new'                            , '/new'              ,       ['@root'                                        , contractReportTechnicalCorrectionsNewTemplateUrl                , ContractReportTechnicalCorrectionsNewCtrl                             ]])
    .state(['root.contractReportTechnicalCorrections.view'                           , '/:id?rf'           , true, ['@root'                                        , contractReportTechnicalCorrectionsViewTemplateUrl               , ContractReportTechnicalCorrectionsViewCtrl                            ]])
    .state(['root.contractReportTechnicalCorrections.view.contract'                  , ''                  ,       ['@root.contractReportTechnicalCorrections.view', contractReportTechnicalCorrectionsContractViewTemplateUrl       , ContractReportTechnicalCorrectionsContractViewCtrl                    ]])
    .state(['root.contractReportTechnicalCorrections.view.report'                    , '/report'           ,       ['@root.contractReportTechnicalCorrections.view', contractReportTechnicalCorrectionsReportViewTemplateUrl         , ContractReportTechnicalCorrectionsReportViewCtrl                      ]])
    .state(['root.contractReportTechnicalCorrections.view.data'                      , '/correction'       ,       ['@root.contractReportTechnicalCorrections.view', contractReportTechnicalCorrectionsEditTemplateUrl               , ContractReportTechnicalCorrectionsEditCtrl                            ]])
    .state(['root.contractReportTechnicalCorrections.view.indicators'                , '/indicators'                                                                                                                                                                                                          ])
    .state(['root.contractReportTechnicalCorrections.view.indicators.search'         , ''                  ,       ['@root.contractReportTechnicalCorrections.view', contractReportTechnicalCorrectionsIndicatorsSearchTemplateUrl         , ContractReportTechnicalCorrectionsIndicatorsSearchCtrl          ]])
    .state(['root.contractReportTechnicalCorrections.view.indicators.view'           , '/:ind'             ,       ['@root.contractReportTechnicalCorrections.view', contractReportTechnicalCorrectionsIndicatorsViewTemplateUrl           , ContractReportTechnicalCorrectionsIndicatorsViewCtrl            ]])
    .state(['root.contractReportTechnicalCorrections.view.correctedIndicators'       , '/correctedIndicators'                                                                                                                                                                                                 ])
    .state(['root.contractReportTechnicalCorrections.view.correctedIndicators.search', ''                  ,       ['@root.contractReportTechnicalCorrections.view', contractReportTechnicalCorrectionsCorrectedIndicatorsSearchTemplateUrl, ContractReportTechnicalCorrectionsCorrectedIndicatorsSearchCtrl ]])
    .state(['root.contractReportTechnicalCorrections.view.correctedIndicators.edit'  , '/:ind'             ,       ['@root.contractReportTechnicalCorrections.view', contractReportTechnicalCorrectionsCorrectedIndicatorsEditTemplateUrl  , ContractReportTechnicalCorrectionsCorrectedIndicatorsEditCtrl   ]]);
    }
  ]);

export default ContractReportTechnicalCorrectionsModule.name;
ContractReportTechnicalCorrectionsModule.factory(
  'ContractReportTechnicalCorrection',
  ContractReportTechnicalCorrectionFactory
);
ContractReportTechnicalCorrectionsModule.factory(
  'ContractReportTechnicalCorrectionIndicator',
  ContractReportTechnicalCorrectionIndicatorFactory
);
ContractReportTechnicalCorrectionsModule.factory(
  'ContractReportTechnicalCorrectionFile',
  ContractReportTechnicalCorrectionFileFactory
);
