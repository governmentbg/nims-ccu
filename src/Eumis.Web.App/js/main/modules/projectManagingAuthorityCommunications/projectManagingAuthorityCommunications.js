import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { ProjectManagingAuthorityCommunicationFactory } from './resources/projectManagingAuthorityCommunication';
import { ProjectManagingAuthorityCommunicationAnswerFactory } from './resources/projectManagingAuthorityCommunicationAnswer';
import projectManagingAuthorityCommunicationsEditTemplateUrl from './views/projectManagingAuthorityCommunicationsEdit.html';
import { ProjectManagingAuthorityCommunicationsEditCtrl } from './views/projectManagingAuthorityCommunicationsEditCtrl';
import projectManagingAuthorityCommunicationsSearchTemplateUrl from './views/projectManagingAuthorityCommunicationsSearch.html';
import { ProjectManagingAuthorityCommunicationsSearchCtrl } from './views/projectManagingAuthorityCommunicationsSearchCtrl';
import projectManagingAuthorityCommunicationAnswersEditTemplateUrl from './views/projectManagingAuthorityCommunicationAnswersEdit.html';
import { ProjectManagingAuthorityCommunicationAnswersEditCtrl } from './views/projectManagingAuthorityCommunicationAnswersEditCtrl';

const ProjectManagingAuthorityCommunicationsModule = angular
  .module('main.projectManagingAuthorityCommunications', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.projectCommunications'                                               , '/projectCommunications?programmeId&programmePriorityId&procedureId&fromDate&toDate&source'                                                                                                                                               ])
    .state(['root.projectCommunications.search'                                        , ''                 ,       ['@root'                           , projectManagingAuthorityCommunicationsSearchTemplateUrl                    , ProjectManagingAuthorityCommunicationsSearchCtrl                         ]])
    .state(['root.projectCommunications.edit'                                          , '/:id?rf'          ,       ['@root'                           , projectManagingAuthorityCommunicationsEditTemplateUrl                      , ProjectManagingAuthorityCommunicationsEditCtrl                           ]])
      
    .state(['root.projectCommunications.edit.answers'                                  , '/answers'                                                                                                                                                                                                                                ])
    .state(['root.projectCommunications.edit.answers.edit'                             , '/:ind'            ,       ['@root'                           , projectManagingAuthorityCommunicationAnswersEditTemplateUrl                , ProjectManagingAuthorityCommunicationAnswersEditCtrl                     ]]);
    }
  ]);

export default ProjectManagingAuthorityCommunicationsModule.name;
ProjectManagingAuthorityCommunicationsModule.factory(
  'ProjectManagingAuthorityCommunication',
  ProjectManagingAuthorityCommunicationFactory
);
ProjectManagingAuthorityCommunicationsModule.factory(
  'ProjectManagingAuthorityCommunicationAnswer',
  ProjectManagingAuthorityCommunicationAnswerFactory
);
