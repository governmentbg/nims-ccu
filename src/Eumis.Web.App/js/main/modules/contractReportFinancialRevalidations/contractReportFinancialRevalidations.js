import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractReportFinancialRevalidationTemplateUrl from './forms/contractReportFinancialRevalidation.html';
import contractReportFinancialRevalidationCSDTemplateUrl from './forms/contractReportFinancialRevalidationCSD.html';
import { ContractReportFinancialRevalidationCSDCtrl } from './forms/contractReportFinancialRevalidationCSDCtrl';
import chooseCRFRContractReportModalTemplateUrl from './modals/chooseCRFRContractReportModal.html';
import { ChooseCRFRContractReportModalCtrl } from './modals/chooseCRFRContractReportModalCtrl';
import { ContractReportFinancialRevalidationFactory } from './resources/contractReportFinancialRevalidation';
import { ContractReportFinancialRevalidationCSDFactory } from './resources/contractReportFinancialRevalidationCSD';
import { ContractReportFinancialRevalidationFileFactory } from './resources/contractReportFinancialRevalidationFile';
import contractReportFinancialRevalidationsContractViewTemplateUrl from './views/contractReportFinancialRevalidationsContractView.html';
import { ContractReportFinancialRevalidationsContractViewCtrl } from './views/contractReportFinancialRevalidationsContractViewCtrl';
import contractReportFinancialRevalidationsCSDsSearchTemplateUrl from './views/contractReportFinancialRevalidationsCSDsSearch.html';
import { ContractReportFinancialRevalidationsCSDsSearchCtrl } from './views/contractReportFinancialRevalidationsCSDsSearchCtrl';
import contractReportFinancialRevalidationsCSDsViewTemplateUrl from './views/contractReportFinancialRevalidationsCSDsView.html';
import { ContractReportFinancialRevalidationsCSDsViewCtrl } from './views/contractReportFinancialRevalidationsCSDsViewCtrl';
import contractReportFinancialRevalidationsEditTemplateUrl from './views/contractReportFinancialRevalidationsEdit.html';
import { ContractReportFinancialRevalidationsEditCtrl } from './views/contractReportFinancialRevalidationsEditCtrl';
import contractReportFinancialRevalidationsNewTemplateUrl from './views/contractReportFinancialRevalidationsNew.html';
import { ContractReportFinancialRevalidationsNewCtrl } from './views/contractReportFinancialRevalidationsNewCtrl';
import contractReportFinancialRevalidationsReportViewTemplateUrl from './views/contractReportFinancialRevalidationsReportView.html';
import { ContractReportFinancialRevalidationsReportViewCtrl } from './views/contractReportFinancialRevalidationsReportViewCtrl';
import contractReportFinancialRevalidationsRevalidatedCSDsEditTemplateUrl from './views/contractReportFinancialRevalidationsRevalidatedCSDsEdit.html';
import { ContractReportFinancialRevalidationsRevalidatedCSDsEditCtrl } from './views/contractReportFinancialRevalidationsRevalidatedCSDsEditCtrl';
import contractReportFinancialRevalidationsRevalidatedCSDsSearchTemplateUrl from './views/contractReportFinancialRevalidationsRevalidatedCSDsSearch.html';
import { ContractReportFinancialRevalidationsRevalidatedCSDsSearchCtrl } from './views/contractReportFinancialRevalidationsRevalidatedCSDsSearchCtrl';
import contractReportFinancialRevalidationsSearchTemplateUrl from './views/contractReportFinancialRevalidationsSearch.html';
import { ContractReportFinancialRevalidationsSearchCtrl } from './views/contractReportFinancialRevalidationsSearchCtrl';
import contractReportFinancialRevalidationsViewTemplateUrl from './views/contractReportFinancialRevalidationsView.html';
import { ContractReportFinancialRevalidationsViewCtrl } from './views/contractReportFinancialRevalidationsViewCtrl';

const ContractReportFinancialRevalidationsModule = angular
  .module('main.contractReportFinancialRevalidations', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractReportFinancialRevalidation',
        templateUrl: contractReportFinancialRevalidationTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractReportFinancialRevalidationCostSupportingDocument',
        templateUrl: contractReportFinancialRevalidationCSDTemplateUrl,
        controller: ContractReportFinancialRevalidationCSDCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
        .modal('chooseCRFRContractReportModal', chooseCRFRContractReportModalTemplateUrl, ChooseCRFRContractReportModalCtrl, 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractReportFinancialRevalidations'                               , '/contractReportFinancialRevalidations?contractRegNum&fromDate&toDate'                                                                                                                                                                                                ])
    .state(['root.contractReportFinancialRevalidations.search'                        , ''                  ,       ['@root'                                          , contractReportFinancialRevalidationsSearchTemplateUrl               , ContractReportFinancialRevalidationsSearchCtrl                ]])
    .state(['root.contractReportFinancialRevalidations.new'                           , '/new'              ,       ['@root'                                          , contractReportFinancialRevalidationsNewTemplateUrl                  , ContractReportFinancialRevalidationsNewCtrl                   ]])
    .state(['root.contractReportFinancialRevalidations.view'                          , '/:id?rf'           , true, ['@root'                                          , contractReportFinancialRevalidationsViewTemplateUrl                 , ContractReportFinancialRevalidationsViewCtrl                  ]])
    .state(['root.contractReportFinancialRevalidations.view.contract'                 , ''                  ,       ['@root.contractReportFinancialRevalidations.view', contractReportFinancialRevalidationsContractViewTemplateUrl         , ContractReportFinancialRevalidationsContractViewCtrl          ]])
    .state(['root.contractReportFinancialRevalidations.view.report'                   , '/report'           ,       ['@root.contractReportFinancialRevalidations.view', contractReportFinancialRevalidationsReportViewTemplateUrl           , ContractReportFinancialRevalidationsReportViewCtrl            ]])
    .state(['root.contractReportFinancialRevalidations.view.data'                     , '/revalidation'     ,       ['@root.contractReportFinancialRevalidations.view', contractReportFinancialRevalidationsEditTemplateUrl                 , ContractReportFinancialRevalidationsEditCtrl                  ]])

    .state(['root.contractReportFinancialRevalidations.view.csds'                     , '/csds?csd&company'                                                                                                                                                                                                                                                   ])
    .state(['root.contractReportFinancialRevalidations.view.csds.search'              , ''                  ,       ['@root.contractReportFinancialRevalidations.view', contractReportFinancialRevalidationsCSDsSearchTemplateUrl           , ContractReportFinancialRevalidationsCSDsSearchCtrl            ]])
    .state(['root.contractReportFinancialRevalidations.view.csds.view'                , '/:ind'             ,       ['@root.contractReportFinancialRevalidations.view', contractReportFinancialRevalidationsCSDsViewTemplateUrl             , ContractReportFinancialRevalidationsCSDsViewCtrl              ]])

    .state(['root.contractReportFinancialRevalidations.view.revalidatedCsds'          , '/revalidatedCsds?csd&company'                                                                                                                                                                                                                                        ])
    .state(['root.contractReportFinancialRevalidations.view.revalidatedCsds.search'   , ''                  ,       ['@root.contractReportFinancialRevalidations.view', contractReportFinancialRevalidationsRevalidatedCSDsSearchTemplateUrl, ContractReportFinancialRevalidationsRevalidatedCSDsSearchCtrl ]])
    .state(['root.contractReportFinancialRevalidations.view.revalidatedCsds.edit'     , '/:ind'             ,       ['@root.contractReportFinancialRevalidations.view', contractReportFinancialRevalidationsRevalidatedCSDsEditTemplateUrl  , ContractReportFinancialRevalidationsRevalidatedCSDsEditCtrl   ]])
    }
  ]);

export default ContractReportFinancialRevalidationsModule.name;
ContractReportFinancialRevalidationsModule.factory(
  'ContractReportFinancialRevalidation',
  ContractReportFinancialRevalidationFactory
);
ContractReportFinancialRevalidationsModule.factory(
  'ContractReportFinancialRevalidationCSD',
  ContractReportFinancialRevalidationCSDFactory
);
ContractReportFinancialRevalidationsModule.factory(
  'ContractReportFinancialRevalidationFile',
  ContractReportFinancialRevalidationFileFactory
);
