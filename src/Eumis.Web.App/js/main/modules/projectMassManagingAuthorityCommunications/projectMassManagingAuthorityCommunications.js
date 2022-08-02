import * as angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import * as UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import projectMassManagingAuthorityCommunicationTemplateUrl from './forms/projectMassManagingAuthorityCommunication.html';
import { ProjectMassManagingAuthorityCommunicationDataCtrl } from './forms/projectMassManagingAuthorityCommunicationCtrl';

import chooseProjectMassMACommunicationRecipientsModalTemplateUrl from './modals/chooseProjectMassMACommunicationRecipientsModal.html';
import { ChooseProjectMassMACommunicationRecipientsModalCtrl } from './modals/chooseProjectMassMACommunicationRecipientsModalCtrl';
import { ProjectMassManagingAuthorityCommunicationFactory } from './resources/projectMassManagingAuthorityCommunication';
import { ProjectMassManagingAuthorityCommunicationDocumentFactory } from './resources/projectMassManagingAuthorityCommunicationDocument';
import { ProjectMassManagingAuthorityCommunicationFileFactory } from './resources/projectMassManagingAuthorityCommunicationFile';
import { ProjectMassManagingAuthorityCommunicationRecipientFactory } from './resources/projectMassManagingAuthorityCommunicationRecipient';
import projectMassManagingAuthorityCommunicationsSearchTemplateUrl from './views/projectMassManagingAuthorityCommunicationsSearch.html';
import { ProjectMassManagingAuthorityCommunicationsSearchCtrl } from './views/projectMassManagingAuthorityCommunicationsSearchCtrl';
import projectMassManagingAuthorityCommunicationsNewTemplateUrl from './views/projectMassManagingAuthorityCommunicationsNew.html';
import { ProjectMassManagingAuthorityCommunicationsNewCtrl } from './views/projectMassManagingAuthorityCommunicationsNewCtrl';
import projectMassManagingAuthorityCommunicationsViewTemplateUrl from './views/projectMassManagingAuthorityCommunicationsView.html';
import { ProjectMassManagingAuthorityCommunicationsViewCtrl } from './views/projectMassManagingAuthorityCommunicationsViewCtrl';
import projectMassManagingAuthorityCommunicationsEditTemplateUrl from './views/projectMassManagingAuthorityCommunicationsEdit.html';
import { ProjectMassManagingAuthorityCommunicationsEditCtrl } from './views/projectMassManagingAuthorityCommunicationsEditCtrl';
import projectMassManagingAuthorityCommunicationDocumentsSearchTemplateUrl from './views/projectMassManagingAuthorityCommunicationDocumentsSearch.html';
import { ProjectMassManagingAuthorityCommunicationDocumentsSearchCtrl } from './views/projectMassManagingAuthorityCommunicationDocumentsSearchCtrl';
import projectMassManagingAuthorityCommunicationDocumentsNewTemplateUrl from './views/projectMassManagingAuthorityCommunicationDocumentsNew.html';
import { ProjectMassManagingAuthorityCommunicationDocumentsNewCtrl } from './views/projectMassManagingAuthorityCommunicationDocumentsNewCtrl';
import projectMassManagingAuthorityCommunicationDocumentsEditTemplateUrl from './views/projectMassManagingAuthorityCommunicationDocumentsEdit.html';
import { ProjectMassManagingAuthorityCommunicationDocumentsEditCtrl } from './views/projectMassManagingAuthorityCommunicationDocumentsEditCtrl';
import projectMassManagingAuthorityCommunicationRecipientsSearchTemplateUrl from './views/projectMassManagingAuthorityCommunicationRecipientsSearch.html';
import { ProjectMassManagingAuthorityCommunicationRecipientsSearchCtrl } from './views/projectMassManagingAuthorityCommunicationRecipientsSearchCtrl';

const ProjectMassManagingAuthorityCommunicationsModule = angular
  .module('main.projectMassManagingAuthorityCommunications', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisProjectMassManagingAuthorityCommunication',
        templateUrl: projectMassManagingAuthorityCommunicationTemplateUrl,
        controller: ProjectMassManagingAuthorityCommunicationDataCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
      .modal('chooseProjectMassMACommunicationRecipientsModal'                , chooseProjectMassMACommunicationRecipientsModalTemplateUrl                       , ChooseProjectMassMACommunicationRecipientsModalCtrl                         , 'xlg')
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.projectMassManagingAuthorityCommunications'                                             , '/projectMassManagingAuthorityCommunications'                                                                                                                                                                                                                                                   ])
    .state(['root.projectMassManagingAuthorityCommunications.search'                                      , ''                              ,       ['@root'                                                  , projectMassManagingAuthorityCommunicationsSearchTemplateUrl                                        , ProjectMassManagingAuthorityCommunicationsSearchCtrl                       ]])
    .state(['root.projectMassManagingAuthorityCommunications.new'                                         , '/new'                          ,       ['@root'                                                  , projectMassManagingAuthorityCommunicationsNewTemplateUrl                                           , ProjectMassManagingAuthorityCommunicationsNewCtrl                          ]])
    .state(['root.projectMassManagingAuthorityCommunications.view'                                        , '/:id?rf'                       , true, ['@root'                                                  , projectMassManagingAuthorityCommunicationsViewTemplateUrl                                          , ProjectMassManagingAuthorityCommunicationsViewCtrl                         ]])
    .state(['root.projectMassManagingAuthorityCommunications.view.edit'                                   , ''                              ,       ['@root.projectMassManagingAuthorityCommunications.view'  , projectMassManagingAuthorityCommunicationsEditTemplateUrl                                          , ProjectMassManagingAuthorityCommunicationsEditCtrl                         ]])
    .state(['root.projectMassManagingAuthorityCommunications.view.documents'                              , '/documents'                                                                                                                                                                                                                                                                                    ])
    .state(['root.projectMassManagingAuthorityCommunications.view.documents.search'                       , ''                              ,       ['@root.projectMassManagingAuthorityCommunications.view'  , projectMassManagingAuthorityCommunicationDocumentsSearchTemplateUrl                                , ProjectMassManagingAuthorityCommunicationDocumentsSearchCtrl               ]])
    .state(['root.projectMassManagingAuthorityCommunications.view.documents.new'                          , '/new'                          ,       ['@root.projectMassManagingAuthorityCommunications.view'  , projectMassManagingAuthorityCommunicationDocumentsNewTemplateUrl                                   , ProjectMassManagingAuthorityCommunicationDocumentsNewCtrl                  ]])
    .state(['root.projectMassManagingAuthorityCommunications.view.documents.edit'                         ,'/:ind'                          ,       ['@root.projectMassManagingAuthorityCommunications.view'  , projectMassManagingAuthorityCommunicationDocumentsEditTemplateUrl                                  , ProjectMassManagingAuthorityCommunicationDocumentsEditCtrl                 ]])
    .state(['root.projectMassManagingAuthorityCommunications.view.recipients'                             , '/recipients'                                                                                                                                                                                                                                                                                   ])
    .state(['root.projectMassManagingAuthorityCommunications.view.recipients.search'                      , ''                              ,       ['@root.projectMassManagingAuthorityCommunications.view'  , projectMassManagingAuthorityCommunicationRecipientsSearchTemplateUrl                               , ProjectMassManagingAuthorityCommunicationRecipientsSearchCtrl              ]])
    }
  ]);

export default ProjectMassManagingAuthorityCommunicationsModule.name;
ProjectMassManagingAuthorityCommunicationsModule.factory(
  'ProjectMassManagingAuthorityCommunication',
  ProjectMassManagingAuthorityCommunicationFactory
);
ProjectMassManagingAuthorityCommunicationsModule.factory(
  'ProjectMassManagingAuthorityCommunicationDocument',
  ProjectMassManagingAuthorityCommunicationDocumentFactory
);
ProjectMassManagingAuthorityCommunicationsModule.factory(
  'ProjectMassManagingAuthorityCommunicationFile',
  ProjectMassManagingAuthorityCommunicationFileFactory
);
ProjectMassManagingAuthorityCommunicationsModule.factory(
  'ProjectMassManagingAuthorityCommunicationRecipient',
  ProjectMassManagingAuthorityCommunicationRecipientFactory
);
