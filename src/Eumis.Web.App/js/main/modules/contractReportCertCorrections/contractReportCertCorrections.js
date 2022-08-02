import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractReportCertCorrectionBasicDataTemplateUrl from './forms/contractReportCertCorrectionBasicData.html';
import contractReportCertCorrectionDataTemplateUrl from './forms/contractReportCertCorrectionData.html';
import { ContractReportCertCorrectionDataCtrl } from './forms/contractReportCertCorrectionDataCtrl';
import contractReportCertCorrectionDocumentTemplateUrl from './forms/contractReportCertCorrectionDocument.html';
import { ContractReportCertCorrectionFactory } from './resources/contractReportCertCorrection';
import { ContractReportCertCorrectionDocumentFactory } from './resources/contractReportCertCorrectionDocument';
import { ContractReportCertCorrectionFileFactory } from './resources/contractReportCertCorrectionFile';
import contractReportCertCorrectionDocumentsEditTemplateUrl from './views/contractReportCertCorrectionDocumentsEdit.html';
import { ContractReportCertCorrectionDocumentsEditCtrl } from './views/contractReportCertCorrectionDocumentsEditCtrl';
import contractReportCertCorrectionDocumentsNewTemplateUrl from './views/contractReportCertCorrectionDocumentsNew.html';
import { ContractReportCertCorrectionDocumentsNewCtrl } from './views/contractReportCertCorrectionDocumentsNewCtrl';
import contractReportCertCorrectionDocumentsSearchTemplateUrl from './views/contractReportCertCorrectionDocumentsSearch.html';
import { ContractReportCertCorrectionDocumentsSearchCtrl } from './views/contractReportCertCorrectionDocumentsSearchCtrl';
import contractReportCertCorrectionsBasicViewTemplateUrl from './views/contractReportCertCorrectionsBasicView.html';
import { ContractReportCertCorrectionsBasicViewCtrl } from './views/contractReportCertCorrectionsBasicViewCtrl';
import contractReportCertCorrectionsEditTemplateUrl from './views/contractReportCertCorrectionsEdit.html';
import { ContractReportCertCorrectionsEditCtrl } from './views/contractReportCertCorrectionsEditCtrl';
import contractReportCertCorrectionsNewTemplateUrl from './views/contractReportCertCorrectionsNew.html';
import { ContractReportCertCorrectionsNewCtrl } from './views/contractReportCertCorrectionsNewCtrl';
import contractReportCertCorrectionsSearchTemplateUrl from './views/contractReportCertCorrectionsSearch.html';
import { ContractReportCertCorrectionsSearchCtrl } from './views/contractReportCertCorrectionsSearchCtrl';
import contractReportCertCorrectionsViewTemplateUrl from './views/contractReportCertCorrectionsView.html';
import { ContractReportCertCorrectionsViewCtrl } from './views/contractReportCertCorrectionsViewCtrl';

const ContractReportCertCorrectionsModule = angular
  .module('main.contractReportCertCorrections', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractReportCertCorrectionBasicData',
        templateUrl: contractReportCertCorrectionBasicDataTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportCertCorrectionData',
        templateUrl: contractReportCertCorrectionDataTemplateUrl,
        controller: ContractReportCertCorrectionDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportCertCorrectionDocument',
        templateUrl: contractReportCertCorrectionDocumentTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractReportCertCorrections'                         , '/contractReportCertCorrections?programmeId&type&status'                                                                                                                                                                       ])
    .state(['root.contractReportCertCorrections.search'                  , ''                ,       ['@root'                                    , contractReportCertCorrectionsSearchTemplateUrl             , ContractReportCertCorrectionsSearchCtrl        ]])
    .state(['root.contractReportCertCorrections.new'                     , '/new'            ,       ['@root'                                    , contractReportCertCorrectionsNewTemplateUrl                , ContractReportCertCorrectionsNewCtrl           ]])
    .state(['root.contractReportCertCorrections.view'                    , '/:id?rf'         , true, ['@root'                                    , contractReportCertCorrectionsViewTemplateUrl               , ContractReportCertCorrectionsViewCtrl          ]])
    .state(['root.contractReportCertCorrections.view.basicData'          , ''                ,       ['@root.contractReportCertCorrections.view' , contractReportCertCorrectionsBasicViewTemplateUrl          , ContractReportCertCorrectionsBasicViewCtrl     ]])
    .state(['root.contractReportCertCorrections.view.correction'         , '/certCorrection' ,       ['@root.contractReportCertCorrections.view' , contractReportCertCorrectionsEditTemplateUrl               , ContractReportCertCorrectionsEditCtrl          ]])
    .state(['root.contractReportCertCorrections.view.docs'               , '/docs'                                                                                                                                                                                                                        ])
    .state(['root.contractReportCertCorrections.view.docs.search'        , ''                ,       ['@root.contractReportCertCorrections.view' , contractReportCertCorrectionDocumentsSearchTemplateUrl     , ContractReportCertCorrectionDocumentsSearchCtrl]])
    .state(['root.contractReportCertCorrections.view.docs.new'           , '/new'            ,       ['@root.contractReportCertCorrections.view' , contractReportCertCorrectionDocumentsNewTemplateUrl        , ContractReportCertCorrectionDocumentsNewCtrl   ]])
    .state(['root.contractReportCertCorrections.view.docs.edit'          , '/:ind'           ,       ['@root.contractReportCertCorrections.view' , contractReportCertCorrectionDocumentsEditTemplateUrl       , ContractReportCertCorrectionDocumentsEditCtrl  ]]);
    }
  ]);

export default ContractReportCertCorrectionsModule.name;
ContractReportCertCorrectionsModule.factory(
  'ContractReportCertCorrection',
  ContractReportCertCorrectionFactory
);
ContractReportCertCorrectionsModule.factory(
  'ContractReportCertCorrectionDocument',
  ContractReportCertCorrectionDocumentFactory
);
ContractReportCertCorrectionsModule.factory(
  'ContractReportCertCorrectionFile',
  ContractReportCertCorrectionFileFactory
);
