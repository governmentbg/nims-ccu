import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import ServiceContractsModule from './modules/serviceContracts/serviceContracts';
import contractContractAccessCodeTemplateUrl from './forms/contractContractAccessCode.html';
import { ContractContractAccessCodeCtrl } from './forms/contractContractAccessCodeCtrl';
import contractContractRegistrationTemplateUrl from './forms/contractContractRegistration.html';
import { ContractContractRegistrationCtrl } from './forms/contractContractRegistrationCtrl';
import contractBeneficiaryTemplateUrl from './forms/contractBeneficiary.html';
import formsContractDataTemplateUrl from './forms/contractData.html';
import contractOfferTemplateUrl from './forms/contractOffer.html';
import contractProcurementTemplateUrl from './forms/contractProcurement.html';
import contractProjectTemplateUrl from './forms/contractProject.html';
import contractSpendingPlanTemplateUrl from './forms/contractSpendingPlan.html';
import contractContractCommunicationTemplateUrl from './forms/contractContractCommunication.html';
import contractVersionTemplateUrl from './forms/contractVersion.html';
import { ContractVersionCtrl } from './forms/contractVersionCtrl';
import chooseContractRegistrationModalTemplateUrl from './modals/chooseContractRegistrationModal.html';
import { ChooseContractRegistrationModalCtrl } from './modals/chooseContractRegistrationModalCtrl';
import invalidForeignNumberWarningModalTemplateUrl from './modals/invalidForeignNumberWarningModal.html';
import { InvalidForeignNumberWarningModalCtrl } from './modals/invalidForeignNumberWarningModalCtrl';
import chooseProjectModalTemplateUrl from './modals/chooseProjectModal.html';
import { ChooseProjectModalCtrl } from './modals/chooseProjectModalCtrl';
import { ContractFactory } from './resources/contract';
import { ContractContractAccessCodeFactory } from './resources/contractContractAccessCode';
import { ContractContractRegistrationFactory } from './resources/contractContractRegistration';
import { ContractContractRegistrationFileFactory } from './resources/contractContractRegistrationFile';
import { ContractFileFactory } from './resources/contractFile';
import { ContractGrantDocumentFactory } from './resources/contractGrantDocument';
import { ContractOffersFactory } from './resources/contractOffers';
import { ContractProcurementFactory } from './resources/contractProcurement';
import { ContractProcurementDocumentFactory } from './resources/contractProcurementDocument';
import { ContractSpendingPlanFactory } from './resources/contractSpendingPlan';
import { ContractVersionFactory } from './resources/contractVersion';
import { ContractCommunicationFactory } from './resources/contractCommunication';
import { ContractUserFactory } from './resources/contractUser';
import contractContractAccessCodesEditTemplateUrl from './views/contractContractAccessCodesEdit.html';
import { ContractContractAccessCodesEditCtrl } from './views/contractContractAccessCodesEditCtrl';
import contractContractAccessCodesSearchTemplateUrl from './views/contractContractAccessCodesSearch.html';
import { ContractContractAccessCodesSearchCtrl } from './views/contractContractAccessCodesSearchCtrl';
import contractContractCommunicationsEditTemplateUrl from './views/contractContractCommunicationsEdit.html';
import { ContractContractCommunicationsEditCtrl } from './views/contractContractCommunicationsEditCtrl';
import contractContractCommunicationsSearchTemplateUrl from './views/contractContractCommunicationsSearch.html';
import { ContractContractCommunicationsSearchCtrl } from './views/contractContractCommunicationsSearchCtrl';
import contractAdditionalDocumentsSearchTemplateUrl from './views/contractAdditionalDocumentsSearch.html';
import { ContractAdditionalDocumentsSearchCtrl } from './views/contractAdditionalDocumentsSearchCtrl';
import contractAmendmentsSearchTemplateUrl from './views/contractAmendmentsSearch.html';
import { ContractAmendmentsSearchCtrl } from './views/contractAmendmentsSearchCtrl';
import contractContractRegistrationsAttachTemplateUrl from './views/contractContractRegistrationsAttach.html';
import { ContractContractRegistrationsAttachCtrl } from './views/contractContractRegistrationsAttachCtrl';
import contractContractRegistrationsEditTemplateUrl from './views/contractContractRegistrationsEdit.html';
import { ContractContractRegistrationsEditCtrl } from './views/contractContractRegistrationsEditCtrl';
import contractContractRegistrationsNewTemplateUrl from './views/contractContractRegistrationsNew.html';
import { ContractContractRegistrationsNewCtrl } from './views/contractContractRegistrationsNewCtrl';
import contractContractRegistrationsSearchTemplateUrl from './views/contractContractRegistrationsSearch.html';
import { ContractContractRegistrationsSearchCtrl } from './views/contractContractRegistrationsSearchCtrl';
import viewsContractDataTemplateUrl from './views/contractData.html';
import { ContractDataCtrl } from './views/contractDataCtrl';
import contractDocumentsGrantEditTemplateUrl from './views/contractDocumentsGrantEdit.html';
import { ContractDocumentsGrantEditCtrl } from './views/contractDocumentsGrantEditCtrl';
import contractDocumentsGrantNewTemplateUrl from './views/contractDocumentsGrantNew.html';
import { ContractDocumentsGrantNewCtrl } from './views/contractDocumentsGrantNewCtrl';
import contractDocumentsProcurementEditTemplateUrl from './views/contractDocumentsProcurementEdit.html';
import { ContractDocumentsProcurementEditCtrl } from './views/contractDocumentsProcurementEditCtrl';
import contractDocumentsProcurementNewTemplateUrl from './views/contractDocumentsProcurementNew.html';
import { ContractDocumentsProcurementNewCtrl } from './views/contractDocumentsProcurementNewCtrl';
import contractDocumentsSearchTemplateUrl from './views/contractDocumentsSearch.html';
import { ContractDocumentsSearchCtrl } from './views/contractDocumentsSearchCtrl';
import contractOffersViewTemplateUrl from './views/contractOffersView.html';
import { ContractOffersViewCtrl } from './views/contractOffersViewCtrl';
import contractProcurementsEditTemplateUrl from './views/contractProcurementsEdit.html';
import { ContractProcurementsEditCtrl } from './views/contractProcurementsEditCtrl';
import contractProcurementsNewTemplateUrl from './views/contractProcurementsNew.html';
import { ContractProcurementsNewCtrl } from './views/contractProcurementsNewCtrl';
import contractsEditNewTemplateUrl from './views/contractsEditNew.html';
import { ContractsEditNewCtrl } from './views/contractsEditNewCtrl';
import contractsNewStep1TemplateUrl from './views/contractsNewStep1.html';
import { ContractsNewStep1Ctrl } from './views/contractsNewStep1Ctrl';
import contractsNewStep2TemplateUrl from './views/contractsNewStep2.html';
import { ContractsNewStep2Ctrl } from './views/contractsNewStep2Ctrl';
import contractSpendingPlansEditTemplateUrl from './views/contractSpendingPlansEdit.html';
import { ContractSpendingPlansEditCtrl } from './views/contractSpendingPlansEditCtrl';
import contractSpendingPlansNewTemplateUrl from './views/contractSpendingPlansNew.html';
import { ContractSpendingPlansNewCtrl } from './views/contractSpendingPlansNewCtrl';
import contractsSearchTemplateUrl from './views/contractsSearch.html';
import { ContractsSearchCtrl } from './views/contractsSearchCtrl';
import contractsViewTemplateUrl from './views/contractsView.html';
import { ContractsViewCtrl } from './views/contractsViewCtrl';
import contractVersionsEditTemplateUrl from './views/contractVersionsEdit.html';
import { ContractVersionsEditCtrl } from './views/contractVersionsEditCtrl';
import contractVersionsSAPTemplateUrl from './views/contractVersionsSAP.html';
import { ContractVersionsSAPCtrl } from './views/contractVersionsSAPCtrl';
import contractVersionsNewTemplateUrl from './views/contractVersionsNew.html';
import { ContractVersionsNewCtrl } from './views/contractVersionsNewCtrl';
import contractUsersSearchTemplateUrl from './views/contractUsersSearch.html';
import { ContractUsersSearchCtrl } from './views/contractUsersSearchCtrl';
import contractUsersNewTemplateUrl from './views/contractUsersNew.html';
import { ContractUsersNewCtrl } from './views/contractUsersNewCtrl';
import ContractUsersEditTemplateUrl from './views/contractUsersEdit.html';
import { ContractUsersEditCtrl } from './views/contractUsersEditCtrl';

const ContractsModule = angular
  .module('main.contracts', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule,
    ServiceContractsModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractData',
        templateUrl: formsContractDataTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractProject',
        templateUrl: contractProjectTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractBeneficiary',
        templateUrl: contractBeneficiaryTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractVersion',
        templateUrl: contractVersionTemplateUrl,
        controller: ContractVersionCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractOffer',
        templateUrl: contractOfferTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractProcurement',
        templateUrl: contractProcurementTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractContractAccessCode',
        templateUrl: contractContractAccessCodeTemplateUrl,
        controller: ContractContractAccessCodeCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractContractRegistration',
        templateUrl: contractContractRegistrationTemplateUrl,
        controller: ContractContractRegistrationCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractSpendingPlan',
        templateUrl: contractSpendingPlanTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisContractContractCommunication',
        templateUrl: contractContractCommunicationTemplateUrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseContractProjectModal'                        , chooseProjectModalTemplateUrl                                  , ChooseProjectModalCtrl                                , 'xlg')
    .modal('chooseContractRegistrationModal'                   , chooseContractRegistrationModalTemplateUrl                     , ChooseContractRegistrationModalCtrl                   , 'xlg')
    .modal('invalidForeignNumberWarningModal'                  , invalidForeignNumberWarningModalTemplateUrl                    , InvalidForeignNumberWarningModalCtrl                  , 'xsm')
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contracts'                                          , '/contracts?programmePriorityId&procedureId'                                                                                                                                                                        ])
    .state(['root.contracts.search'                                   , ''                          ,       ['@root'                           , contractsSearchTemplateUrl                                                 , ContractsSearchCtrl                              ]])
    .state(['root.contracts.newStep1'                                 , '/newStep1'                 ,       ['@root'                           , contractsNewStep1TemplateUrl                                               , ContractsNewStep1Ctrl                            ]])
    .state(['root.contracts.newStep2'                                 , '/newStep2?pNum'            ,       ['@root'                           , contractsNewStep2TemplateUrl                                               , ContractsNewStep2Ctrl                            ]])

    .state(['root.contracts.editNew'                                  , '/new/:id?rf'               ,       ['@root'                           , contractsEditNewTemplateUrl                                                , ContractsEditNewCtrl                             ]])

    .state(['root.contracts.view'                                     , '/:id?rf'                   , true, ['@root'                           , contractsViewTemplateUrl                                                   , ContractsViewCtrl                                ]])
    .state(['root.contracts.view.data'                                , ''                          ,       ['@root.contracts.view'            , viewsContractDataTemplateUrl                                               , ContractDataCtrl                                 ]])

    .state(['root.contracts.view.amendments'                          , '/amendments'                                                                                                                                                                                                       ])
    .state(['root.contracts.view.amendments.search'                   , ''                          ,       ['@root.contracts.view'            , contractAmendmentsSearchTemplateUrl                                        , ContractAmendmentsSearchCtrl                     ]])
    .state(['root.contracts.view.amendments.versions'                 , '/versions'                                                                                                                                                                                                         ])
    .state(['root.contracts.view.amendments.versions.new'             , '/new?t'                    ,       ['@root.contracts.view'            , contractVersionsNewTemplateUrl                                             , ContractVersionsNewCtrl                          ]])
    .state(['root.contracts.view.amendments.versions.edit'            , '/:vid'                     ,       ['@root.contracts.view'            , contractVersionsEditTemplateUrl                                            , ContractVersionsEditCtrl                         ]])
    .state(['root.contracts.view.amendments.versions.edit.sapData'    , '/sapData?status'           ,       ['@root.contracts.view'            , contractVersionsSAPTemplateUrl                                             , ContractVersionsSAPCtrl                          ]])
    .state(['root.contracts.view.amendments.procurements'             , '/procurements'                                                                                                                                                                                                     ])
    .state(['root.contracts.view.amendments.procurements.new'         , '/new'                      ,       ['@root.contracts.view'            , contractProcurementsNewTemplateUrl                                         , ContractProcurementsNewCtrl                      ]])
    .state(['root.contracts.view.amendments.procurements.edit'        , '/:pid'                     ,       ['@root.contracts.view'            , contractProcurementsEditTemplateUrl                                        , ContractProcurementsEditCtrl                     ]])
    .state(['root.contracts.view.amendments.spendingPlans'            , '/spendingPlans'                                                                                                                                                                                                    ])
    .state(['root.contracts.view.amendments.spendingPlans.new'        , '/new'                      ,       ['@root.contracts.view'            , contractSpendingPlansNewTemplateUrl                                         , ContractSpendingPlansNewCtrl                    ]])
    .state(['root.contracts.view.amendments.spendingPlans.edit'       , '/:spid'                    ,       ['@root.contracts.view'            , contractSpendingPlansEditTemplateUrl                                        , ContractSpendingPlansEditCtrl                   ]])
    .state(['root.contracts.view.amendments.offers'                   , '/offers'                                                                                                                                                                                                           ])
    .state(['root.contracts.view.amendments.offers.edit'              , '/:oid'                     ,       ['@root.contracts.view'            , contractOffersViewTemplateUrl                                               , ContractOffersViewCtrl                          ]])

    .state(['root.contracts.view.registrations'                       , '/registrations'                                                                                                                                                                                                    ])
    .state(['root.contracts.view.registrations.search'                , ''                          ,       ['@root.contracts.view'            , contractContractRegistrationsSearchTemplateUrl                             , ContractContractRegistrationsSearchCtrl          ]])
    .state(['root.contracts.view.registrations.new'                   , '/new'                      ,       ['@root.contracts.view'            , contractContractRegistrationsNewTemplateUrl                                , ContractContractRegistrationsNewCtrl             ]])
    .state(['root.contracts.view.registrations.attach'                , '/attach'                   ,       ['@root.contracts.view'            , contractContractRegistrationsAttachTemplateUrl                             , ContractContractRegistrationsAttachCtrl          ]])
    .state(['root.contracts.view.registrations.edit'                  , '/:ind'                     ,       ['@root.contracts.view'            , contractContractRegistrationsEditTemplateUrl                               , ContractContractRegistrationsEditCtrl            ]])

    .state(['root.contracts.view.accesscodes'                         , '/accesscodes'                                                                                                                                                                                                      ])
    .state(['root.contracts.view.accesscodes.search'                  , ''                          ,       ['@root.contracts.view'            , contractContractAccessCodesSearchTemplateUrl                               , ContractContractAccessCodesSearchCtrl            ]])
    .state(['root.contracts.view.accesscodes.edit'                    , '/:ind'                     ,       ['@root.contracts.view'            , contractContractAccessCodesEditTemplateUrl                                 , ContractContractAccessCodesEditCtrl              ]])

    .state(['root.contracts.view.communications'                      , '/communications'                                                                                                                                                                                                   ])
    .state(['root.contracts.view.communications.search'               , ''                          ,       ['@root.contracts.view'            , contractContractCommunicationsSearchTemplateUrl                           , ContractContractCommunicationsSearchCtrl          ]])
    .state(['root.contracts.view.communications.edit'                 , '/:ind'                     ,       ['@root.contracts.view'            , contractContractCommunicationsEditTemplateUrl                             , ContractContractCommunicationsEditCtrl            ]])

    .state(['root.contracts.view.additionalDocuments'                 , '/additionalDocuments'                                                                                                                                                                                              ])
    .state(['root.contracts.view.additionalDocuments.search'          , ''                          ,       ['@root.contracts.view'            , contractAdditionalDocumentsSearchTemplateUrl                               , ContractAdditionalDocumentsSearchCtrl            ]])

    .state(['root.contracts.view.users'                               , '/users'                                                                                                                                                                                                            ])
    .state(['root.contracts.view.users.search'                        , ''                          ,       ['@root.contracts.view'            , contractUsersSearchTemplateUrl                                             , ContractUsersSearchCtrl                          ]])
    .state(['root.contracts.view.users.new'                           , '/new'                      ,       ['@root.contracts.view'            , contractUsersNewTemplateUrl                                                , ContractUsersNewCtrl                             ]])
    .state(['root.contracts.view.users.edit'                          , '/:ind'                     ,       ['@root.contracts.view'            , ContractUsersEditTemplateUrl                                               , ContractUsersEditCtrl                            ]])

    .state(['root.contracts.view.documents'                           , '/documents'                                                                                                                                                                                                        ])
    .state(['root.contracts.view.documents.search'                    , ''                          ,        ['@root.contracts.view'           , contractDocumentsSearchTemplateUrl                                          , ContractDocumentsSearchCtrl                     ]])
    .state(['root.contracts.view.documents.newGrant'                  , '/grant/new'                ,        ['@root.contracts.view'           , contractDocumentsGrantNewTemplateUrl                                        , ContractDocumentsGrantNewCtrl                   ]])
    .state(['root.contracts.view.documents.newProcurement'            , '/procurement/new'          ,        ['@root.contracts.view'           , contractDocumentsProcurementNewTemplateUrl                                  , ContractDocumentsProcurementNewCtrl             ]])
    .state(['root.contracts.view.documents.grantEdit'                 , '/grant/:ind'               ,        ['@root.contracts.view'           , contractDocumentsGrantEditTemplateUrl                                       , ContractDocumentsGrantEditCtrl                  ]])
    .state(['root.contracts.view.documents.procurementEdit'           , '/procurement/:ind'         ,        ['@root.contracts.view'           , contractDocumentsProcurementEditTemplateUrl                                 , ContractDocumentsProcurementEditCtrl            ]])
    }
  ]);

export default ContractsModule.name;
ContractsModule.factory('Contract', ContractFactory);
ContractsModule.factory('ContractContractAccessCode', ContractContractAccessCodeFactory);
ContractsModule.factory('ContractContractRegistration', ContractContractRegistrationFactory);
ContractsModule.factory(
  'ContractContractRegistrationFile',
  ContractContractRegistrationFileFactory
);
ContractsModule.factory('ContractFile', ContractFileFactory);
ContractsModule.factory('ContractGrantDocument', ContractGrantDocumentFactory);
ContractsModule.factory('ContractOffers', ContractOffersFactory);
ContractsModule.factory('ContractProcurement', ContractProcurementFactory);
ContractsModule.factory('ContractProcurementDocument', ContractProcurementDocumentFactory);
ContractsModule.factory('ContractSpendingPlan', ContractSpendingPlanFactory);
ContractsModule.factory('ContractVersion', ContractVersionFactory);
ContractsModule.factory('ContractCommunication', ContractCommunicationFactory);
ContractsModule.factory('ContractUser', ContractUserFactory);
