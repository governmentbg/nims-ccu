import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractReportRevalidationBasicDataTemplateUrl from './forms/contractReportRevalidationBasicData.html';
import contractReportRevalidationDataTemplateUrl from './forms/contractReportRevalidationData.html';
import { ContractReportRevalidationDataCtrl } from './forms/contractReportRevalidationDataCtrl';
import contractReportRevalidationDocumentTemplateUrl from './forms/contractReportRevalidationDocument.html';
import { ContractReportRevalidationFactory } from './resources/contractReportRevalidation';
import { ContractReportRevalidationDocumentFactory } from './resources/contractReportRevalidationDocument';
import { ContractReportRevalidationFileFactory } from './resources/contractReportRevalidationFile';
import contractReportRevalidationDocumentsEditTemplateUrl from './views/contractReportRevalidationDocumentsEdit.html';
import { ContractReportRevalidationDocumentsEditCtrl } from './views/contractReportRevalidationDocumentsEditCtrl';
import contractReportRevalidationDocumentsNewTemplateUrl from './views/contractReportRevalidationDocumentsNew.html';
import { ContractReportRevalidationDocumentsNewCtrl } from './views/contractReportRevalidationDocumentsNewCtrl';
import contractReportRevalidationDocumentsSearchTemplateUrl from './views/contractReportRevalidationDocumentsSearch.html';
import { ContractReportRevalidationDocumentsSearchCtrl } from './views/contractReportRevalidationDocumentsSearchCtrl';
import contractReportRevalidationsBasicViewTemplateUrl from './views/contractReportRevalidationsBasicView.html';
import { ContractReportRevalidationsBasicViewCtrl } from './views/contractReportRevalidationsBasicViewCtrl';
import contractReportRevalidationsEditTemplateUrl from './views/contractReportRevalidationsEdit.html';
import { ContractReportRevalidationsEditCtrl } from './views/contractReportRevalidationsEditCtrl';
import contractReportRevalidationsNewTemplateUrl from './views/contractReportRevalidationsNew.html';
import { ContractReportRevalidationsNewCtrl } from './views/contractReportRevalidationsNewCtrl';
import contractReportRevalidationsSearchTemplateUrl from './views/contractReportRevalidationsSearch.html';
import { ContractReportRevalidationsSearchCtrl } from './views/contractReportRevalidationsSearchCtrl';
import contractReportRevalidationsViewTemplateUrl from './views/contractReportRevalidationsView.html';
import { ContractReportRevalidationsViewCtrl } from './views/contractReportRevalidationsViewCtrl';

const ContractReportRevalidationsModule = angular
  .module('main.contractReportRevalidations', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractReportRevalidationBasicData',
        templateUrl: contractReportRevalidationBasicDataTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportRevalidationData',
        templateUrl: contractReportRevalidationDataTemplateUrl,
        controller: ContractReportRevalidationDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportRevalidationDocument',
        templateUrl: contractReportRevalidationDocumentTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractReportRevalidations'                           , '/contractReportRevalidations?programmeId&type&status'                                                                                                                                                                        ])
    .state(['root.contractReportRevalidations.search'                    , ''                ,       ['@root'                                  , contractReportRevalidationsSearchTemplateUrl                   , ContractReportRevalidationsSearchCtrl              ]])
    .state(['root.contractReportRevalidations.new'                       , '/new'            ,       ['@root'                                  , contractReportRevalidationsNewTemplateUrl                      , ContractReportRevalidationsNewCtrl                 ]])
    .state(['root.contractReportRevalidations.view'                      , '/:id?rf'         , true, ['@root'                                  , contractReportRevalidationsViewTemplateUrl                     , ContractReportRevalidationsViewCtrl                ]])
    .state(['root.contractReportRevalidations.view.basicData'            , ''                ,       ['@root.contractReportRevalidations.view' , contractReportRevalidationsBasicViewTemplateUrl                , ContractReportRevalidationsBasicViewCtrl           ]])
    .state(['root.contractReportRevalidations.view.revalidation'         , '/revalidation'   ,       ['@root.contractReportRevalidations.view' , contractReportRevalidationsEditTemplateUrl                     , ContractReportRevalidationsEditCtrl                ]])
    .state(['root.contractReportRevalidations.view.docs'                 , '/docs'                                                                                                                                                                                                                       ])
    .state(['root.contractReportRevalidations.view.docs.search'          , ''                ,       ['@root.contractReportRevalidations.view' , contractReportRevalidationDocumentsSearchTemplateUrl           , ContractReportRevalidationDocumentsSearchCtrl      ]])
    .state(['root.contractReportRevalidations.view.docs.new'             , '/new'            ,       ['@root.contractReportRevalidations.view' , contractReportRevalidationDocumentsNewTemplateUrl              , ContractReportRevalidationDocumentsNewCtrl         ]])
    .state(['root.contractReportRevalidations.view.docs.edit'            , '/:ind'           ,       ['@root.contractReportRevalidations.view' , contractReportRevalidationDocumentsEditTemplateUrl             , ContractReportRevalidationDocumentsEditCtrl        ]])
    }
  ]);

export default ContractReportRevalidationsModule.name;
ContractReportRevalidationsModule.factory(
  'ContractReportRevalidation',
  ContractReportRevalidationFactory
);
ContractReportRevalidationsModule.factory(
  'ContractReportRevalidationDocument',
  ContractReportRevalidationDocumentFactory
);
ContractReportRevalidationsModule.factory(
  'ContractReportRevalidationFile',
  ContractReportRevalidationFileFactory
);
