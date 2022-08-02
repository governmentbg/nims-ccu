import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import chooseProjectDossierProjectModalTemplateUrl from './modals/chooseProjectDossierProjectModal.html';
import { ChooseProjectDossierProjectModalCtrl } from './modals/chooseProjectDossierProjectModalCtrl';
import { ProjectDossierFactory } from './resources/projectDossier';
import { ProjectDossierContractFactory } from './resources/projectDossierContract';
import { ProjectDossierDcoumentFactory } from './resources/projectDossierDocument';
import { ProjectDossierFileFactory } from './resources/projectDossierFile';
import projectDossierTemplateUrl from './views/projectDossier.html';
import projectDossierContractApprovedAmountsViewTemplateUrl from './views/projectDossierContractApprovedAmountsView.html';
import { ProjectDossierContractApprovedAmountsViewCtrl } from './views/projectDossierContractApprovedAmountsViewCtrl';
import projectDossierContractAuditsViewTemplateUrl from './views/projectDossierContractAuditsView.html';
import { ProjectDossierContractAuditsViewCtrl } from './views/projectDossierContractAuditsViewCtrl';
import projectDossierContractCertifiedAmountsViewTemplateUrl from './views/projectDossierContractCertifiedAmountsView.html';
import { ProjectDossierContractCertifiedAmountsViewCtrl } from './views/projectDossierContractCertifiedAmountsViewCtrl';
import projectDossierContractPhysicalExecutionViewTemplateUrl from './views/projectDossierContractPhysicalExecutionView.html';
import { ProjectDossierContractPhysicalExecutionViewCtrl } from './views/projectDossierContractPhysicalExecutionViewCtrl';
import projectDossierContractCommunicationsEditTemplateUrl from './views/projectDossierContractCommunicationsEdit.html';
import { ProjectDossierContractCommunicationsEditCtrl } from './views/projectDossierContractCommunicationsEditCtrl';
import projectDossierContractDebtsViewTemplateUrl from './views/projectDossierContractDebtsView.html';
import { ProjectDossierContractDebtsViewCtrl } from './views/projectDossierContractDebtsViewCtrl';
import projectDossierContractFinancialCorrectionViewTemplateUrl from './views/projectDossierContractFinancialCorrectionView.html';
import { ProjectDossierContractFinancialCorrectionViewCtrl } from './views/projectDossierContractFinancialCorrectionViewCtrl';
import projectDossierContractIrregularitiesAndSignalsViewTemplateUrl from './views/projectDossierContractIrregularitiesAndSignalsView.html';
import { ProjectDossierContractIrregularitiesAndSignalsViewCtrl } from './views/projectDossierContractIrregularitiesAndSignalsViewCtrl';
import projectDossierContractOfferViewTemplateUrl from './views/projectDossierContractOfferView.html';
import { ProjectDossierContractOfferViewCtrl } from './views/projectDossierContractOfferViewCtrl';
import projectDossierContractPaidAmountsViewTemplateUrl from './views/projectDossierContractPaidAmountsView.html';
import { ProjectDossierContractPaidAmountsViewCtrl } from './views/projectDossierContractPaidAmountsViewCtrl';
import projectDossierContractProcurementsEditTemplateUrl from './views/projectDossierContractProcurementsEdit.html';
import { ProjectDossierContractProcurementsEditCtrl } from './views/projectDossierContractProcurementsEditCtrl';
import projectDossierContractReimbursedAmountsViewTemplateUrl from './views/projectDossierContractReimbursedAmountsView.html';
import { ProjectDossierContractReimbursedAmountsViewCtrl } from './views/projectDossierContractReimbursedAmountsViewCtrl';
import projectDossierContractSpendingPlansEditTemplateUrl from './views/projectDossierContractSpendingPlansEdit.html';
import { ProjectDossierContractSpendingPlansEditCtrl } from './views/projectDossierContractSpendingPlansEditCtrl';
import projectDossierContractSpotChecksViewTemplateUrl from './views/projectDossierContractSpotChecksView.html';
import { ProjectDossierContractSpotChecksViewCtrl } from './views/projectDossierContractSpotChecksViewCtrl';
import projectDossierContractVersionsEditTemplateUrl from './views/projectDossierContractVersionsEdit.html';
import { ProjectDossierContractVersionsEditCtrl } from './views/projectDossierContractVersionsEditCtrl';
import projectDossierContractViewTemplateUrl from './views/projectDossierContractView.html';
import { ProjectDossierContractViewCtrl } from './views/projectDossierContractViewCtrl';
import { ProjectDossierCtrl } from './views/projectDossierCtrl';
import projectDossierDocumentsSearchTemplateUrl from './views/projectDossierDocumentsSearch.html';
import { ProjectDossierDocumentsSearchCtrl } from './views/projectDossierDocumentsSearchCtrl';
import projectDossierProjectCommunicationEditTemplateUrl from './views/projectDossierProjectCommunicationEdit.html';
import { ProjectDossierProjectCommunicationEditCtrl } from './views/projectDossierProjectCommunicationEditCtrl';
import projectDossierProjectEvaluationsEditTemplateUrl from './views/projectDossierProjectEvaluationsEdit.html';
import { ProjectDossierProjectEvaluationsEditCtrl } from './views/projectDossierProjectEvaluationsEditCtrl';
import projectDossierProjectStandingsEditTemplateUrl from './views/projectDossierProjectStandingsEdit.html';
import { ProjectDossierProjectStandingsEditCtrl } from './views/projectDossierProjectStandingsEditCtrl';
import projectDossierProjectVersionEditTemplateUrl from './views/projectDossierProjectVersionEdit.html';
import { ProjectDossierProjectVersionEditCtrl } from './views/projectDossierProjectVersionEditCtrl';
import projectDossierProjectViewTemplateUrl from './views/projectDossierProjectView.html';
import { ProjectDossierProjectViewCtrl } from './views/projectDossierProjectViewCtrl';
import projectDossierViewTemplateUrl from './views/projectDossierView.html';
import { ProjectDossierViewCtrl } from './views/projectDossierViewCtrl';

const ProjectDossierModule = angular
  .module('main.projectDossier', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseProjectDossierProjectModal'       , chooseProjectDossierProjectModalTemplateUrl               , ChooseProjectDossierProjectModalCtrl       , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.projectDossier'                                                  , '/projectDossier?contractId',['@root'                                           , projectDossierTemplateUrl                                                                   , ProjectDossierCtrl                                            ]])
    .state(['root.projectDossier.view'                                             , '/:id?rf'           , true, ['@root'                                            , projectDossierViewTemplateUrl                                                               , ProjectDossierViewCtrl                                        ]])
    .state(['root.projectDossier.view.project'                                     , ''                  ,       ['@root.projectDossier.view'                        , projectDossierProjectViewTemplateUrl                                                        , ProjectDossierProjectViewCtrl                                 ]])
    .state(['root.projectDossier.view.project.versions'                            , '/versions'                                                                                                                                                                                                                                                               ])
    .state(['root.projectDossier.view.project.versions.edit'                       , '/:vid'             ,       ['@root.projectDossier.view'                        , projectDossierProjectVersionEditTemplateUrl                                                 , ProjectDossierProjectVersionEditCtrl                          ]])
    .state(['root.projectDossier.view.project.communications'                      , '/communications'                                                                                                                                                                                                                                                         ])
    .state(['root.projectDossier.view.project.communications.edit'                 , '/:mid'             ,       ['@root.projectDossier.view'                        , projectDossierProjectCommunicationEditTemplateUrl                                           , ProjectDossierProjectCommunicationEditCtrl                    ]])
    .state(['root.projectDossier.view.project.evaluations'                         , '/evaluations'                                                                                                                                                                                                                                                            ])
    .state(['root.projectDossier.view.project.evaluations.edit'                    , '/:eid'             ,       ['@root.projectDossier.view'                        , projectDossierProjectEvaluationsEditTemplateUrl                                             , ProjectDossierProjectEvaluationsEditCtrl                      ]])
    .state(['root.projectDossier.view.project.standings'                           , '/standings'                                                                                                                                                                                                                                                              ])
    .state(['root.projectDossier.view.project.standings.edit'                      , '/:sid'             ,       ['@root.projectDossier.view'                        , projectDossierProjectStandingsEditTemplateUrl                                               , ProjectDossierProjectStandingsEditCtrl                        ]])
    .state(['root.projectDossier.view.contract'                                    , '/contract'         ,       ['@root.projectDossier.view'                        , projectDossierContractViewTemplateUrl                                                       , ProjectDossierContractViewCtrl                                ]])
    .state(['root.projectDossier.view.contract.versions'                           , '/versions'                                                                                                                                                                                                                                                               ])
    .state(['root.projectDossier.view.contract.versions.edit'                      , '/:vid'             ,       ['@root.projectDossier.view'                        , projectDossierContractVersionsEditTemplateUrl                                               , ProjectDossierContractVersionsEditCtrl                        ]])
    .state(['root.projectDossier.view.contract.procurements'                       , '/procurements'                                                                                                                                                                                                                                                           ])
    .state(['root.projectDossier.view.contract.procurements.edit'                  , '/:pid'             ,       ['@root.projectDossier.view'                        , projectDossierContractProcurementsEditTemplateUrl                                           , ProjectDossierContractProcurementsEditCtrl                    ]])
    .state(['root.projectDossier.view.contract.spendingPlans'                      , '/spendingPlans'                                                                                                                                                                                                                                                          ])
    .state(['root.projectDossier.view.contract.spendingPlans.edit'                 , '/:spid'            ,       ['@root.projectDossier.view'                        , projectDossierContractSpendingPlansEditTemplateUrl                                          , ProjectDossierContractSpendingPlansEditCtrl                   ]])
    .state(['root.projectDossier.view.contract.offers'                             , '/offers'                                                                                                                                                                                                                                                                 ])
    .state(['root.projectDossier.view.contract.offers.edit'                        , '/:oid'             ,       ['@root.projectDossier.view'                        , projectDossierContractOfferViewTemplateUrl                                                  , ProjectDossierContractOfferViewCtrl                           ]])
    .state(['root.projectDossier.view.contract.communications'                     , '/communications'                                                                                                                                                                                                                                                         ])
    .state(['root.projectDossier.view.contract.communications.edit'                , '/:cid'             ,       ['@root.projectDossier.view'                        , projectDossierContractCommunicationsEditTemplateUrl                                         , ProjectDossierContractCommunicationsEditCtrl                  ]])
    .state(['root.projectDossier.view.paidAmounts'                                 , '/paidAmounts'      ,       ['@root.projectDossier.view'                        , projectDossierContractPaidAmountsViewTemplateUrl                                            , ProjectDossierContractPaidAmountsViewCtrl                     ]])
    .state(['root.projectDossier.view.debts'                                       , '/debts'            ,       ['@root.projectDossier.view'                        , projectDossierContractDebtsViewTemplateUrl                                                  , ProjectDossierContractDebtsViewCtrl                           ]])
    .state(['root.projectDossier.view.reimbursedAmounts'                           , '/reimbursedAmounts',       ['@root.projectDossier.view'                        , projectDossierContractReimbursedAmountsViewTemplateUrl                                      , ProjectDossierContractReimbursedAmountsViewCtrl               ]])
    .state(['root.projectDossier.view.financialCorrections'                        , '/financialCorrections',    ['@root.projectDossier.view'                        , projectDossierContractFinancialCorrectionViewTemplateUrl                                    , ProjectDossierContractFinancialCorrectionViewCtrl             ]])
    .state(['root.projectDossier.view.approvedAmounts'                             , '/approvedAmounts'  ,       ['@root.projectDossier.view'                        , projectDossierContractApprovedAmountsViewTemplateUrl                                        , ProjectDossierContractApprovedAmountsViewCtrl                 ]])
    .state(['root.projectDossier.view.certifiedAmounts'                            , '/certifiedAmounts' ,       ['@root.projectDossier.view'                        , projectDossierContractCertifiedAmountsViewTemplateUrl                                       , ProjectDossierContractCertifiedAmountsViewCtrl                ]])
    .state(['root.projectDossier.view.physicalExecution'                           , '/physicalExecution',       ['@root.projectDossier.view'                        , projectDossierContractPhysicalExecutionViewTemplateUrl                                      , ProjectDossierContractPhysicalExecutionViewCtrl               ]])
    .state(['root.projectDossier.view.spotChecks'                                  , '/spotChecks'       ,       ['@root.projectDossier.view'                        , projectDossierContractSpotChecksViewTemplateUrl                                             , ProjectDossierContractSpotChecksViewCtrl                      ]])
    .state(['root.projectDossier.view.irregularitiesAndSignals'                    , '/irregularitiesAndSignals',['@root.projectDossier.view'                        , projectDossierContractIrregularitiesAndSignalsViewTemplateUrl                               , ProjectDossierContractIrregularitiesAndSignalsViewCtrl        ]])
    .state(['root.projectDossier.view.audits'                                      , '/audits'           ,       ['@root.projectDossier.view'                        , projectDossierContractAuditsViewTemplateUrl                                                 , ProjectDossierContractAuditsViewCtrl                          ]])
    .state(['root.projectDossier.view.documents'                                   , '/documents?docTypes&objDescription&fileDescription'                                                                                                                                                                                                                      ])
    .state(['root.projectDossier.view.documents.search'                            , ''                  ,       ['@root.projectDossier.view'                        , projectDossierDocumentsSearchTemplateUrl                                                    , ProjectDossierDocumentsSearchCtrl                             ]]);
    }
  ]);

export default ProjectDossierModule.name;
ProjectDossierModule.factory('ProjectDossier', ProjectDossierFactory);
ProjectDossierModule.factory('ProjectDossierContract', ProjectDossierContractFactory);
ProjectDossierModule.factory('ProjectDossierDcoument', ProjectDossierDcoumentFactory);
ProjectDossierModule.factory('ProjectDossierFile', ProjectDossierFileFactory);
