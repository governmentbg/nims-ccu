import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractReportCorrectionBasicDataTemplateUrl from './forms/contractReportCorrectionBasicData.html';
import contractReportCorrectionDataTemplateUrl from './forms/contractReportCorrectionData.html';
import { ContractReportCorrectionDataCtrl } from './forms/contractReportCorrectionDataCtrl';
import contractReportCorrectionDocumentTemplateUrl from './forms/contractReportCorrectionDocument.html';
import { ContractReportCorrectionFactory } from './resources/contractReportCorrection';
import { ContractReportCorrectionDocumentFactory } from './resources/contractReportCorrectionDocument';
import { ContractReportCorrectionFileFactory } from './resources/contractReportCorrectionFile';
import contractReportCorrectionDocumentsEditTemplateUrl from './views/contractReportCorrectionDocumentsEdit.html';
import { ContractReportCorrectionDocumentsEditCtrl } from './views/contractReportCorrectionDocumentsEditCtrl';
import contractReportCorrectionDocumentsNewTemplateUrl from './views/contractReportCorrectionDocumentsNew.html';
import { ContractReportCorrectionDocumentsNewCtrl } from './views/contractReportCorrectionDocumentsNewCtrl';
import contractReportCorrectionDocumentsSearchTemplateUrl from './views/contractReportCorrectionDocumentsSearch.html';
import { ContractReportCorrectionDocumentsSearchCtrl } from './views/contractReportCorrectionDocumentsSearchCtrl';
import contractReportCorrectionsBasicViewTemplateUrl from './views/contractReportCorrectionsBasicView.html';
import { ContractReportCorrectionsBasicViewCtrl } from './views/contractReportCorrectionsBasicViewCtrl';
import contractReportCorrectionsEditTemplateUrl from './views/contractReportCorrectionsEdit.html';
import { ContractReportCorrectionsEditCtrl } from './views/contractReportCorrectionsEditCtrl';
import contractReportCorrectionsNewTemplateUrl from './views/contractReportCorrectionsNew.html';
import { ContractReportCorrectionsNewCtrl } from './views/contractReportCorrectionsNewCtrl';
import contractReportCorrectionsSearchTemplateUrl from './views/contractReportCorrectionsSearch.html';
import { ContractReportCorrectionsSearchCtrl } from './views/contractReportCorrectionsSearchCtrl';
import contractReportCorrectionsViewTemplateUrl from './views/contractReportCorrectionsView.html';
import { ContractReportCorrectionsViewCtrl } from './views/contractReportCorrectionsViewCtrl';

const ContractReportCorrectionsModule = angular
  .module('main.contractReportCorrections', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractReportCorrectionBasicData',
        templateUrl: contractReportCorrectionBasicDataTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportCorrectionData',
        templateUrl: contractReportCorrectionDataTemplateUrl,
        controller: ContractReportCorrectionDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportCorrectionDocument',
        templateUrl: contractReportCorrectionDocumentTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractReportCorrections'                             , '/contractReportCorrections?programmeId&type&status'                                                                                                                                                                          ])
    .state(['root.contractReportCorrections.search'                      , ''                ,       ['@root'                                  , contractReportCorrectionsSearchTemplateUrl                       , ContractReportCorrectionsSearchCtrl              ]])
    .state(['root.contractReportCorrections.new'                         , '/new'            ,       ['@root'                                  , contractReportCorrectionsNewTemplateUrl                          , ContractReportCorrectionsNewCtrl                 ]])
    .state(['root.contractReportCorrections.view'                        , '/:id?rf'         , true, ['@root'                                  , contractReportCorrectionsViewTemplateUrl                         , ContractReportCorrectionsViewCtrl                ]])
    .state(['root.contractReportCorrections.view.basicData'              , ''                ,       ['@root.contractReportCorrections.view'   , contractReportCorrectionsBasicViewTemplateUrl                    , ContractReportCorrectionsBasicViewCtrl           ]])
    .state(['root.contractReportCorrections.view.correction'             , '/correction'     ,       ['@root.contractReportCorrections.view'   , contractReportCorrectionsEditTemplateUrl                         , ContractReportCorrectionsEditCtrl                ]])
    .state(['root.contractReportCorrections.view.docs'                   , '/docs'                                                                                                                                                                                                                       ])
    .state(['root.contractReportCorrections.view.docs.search'            , ''                ,       ['@root.contractReportCorrections.view'   , contractReportCorrectionDocumentsSearchTemplateUrl               , ContractReportCorrectionDocumentsSearchCtrl      ]])
    .state(['root.contractReportCorrections.view.docs.new'               , '/new'            ,       ['@root.contractReportCorrections.view'   , contractReportCorrectionDocumentsNewTemplateUrl                  , ContractReportCorrectionDocumentsNewCtrl         ]])
    .state(['root.contractReportCorrections.view.docs.edit'              , '/:ind'           ,       ['@root.contractReportCorrections.view'   , contractReportCorrectionDocumentsEditTemplateUrl                 , ContractReportCorrectionDocumentsEditCtrl        ]])
    }
  ]);

export default ContractReportCorrectionsModule.name;
ContractReportCorrectionsModule.factory(
  'ContractReportCorrection',
  ContractReportCorrectionFactory
);
ContractReportCorrectionsModule.factory(
  'ContractReportCorrectionDocument',
  ContractReportCorrectionDocumentFactory
);
ContractReportCorrectionsModule.factory(
  'ContractReportCorrectionFile',
  ContractReportCorrectionFileFactory
);
