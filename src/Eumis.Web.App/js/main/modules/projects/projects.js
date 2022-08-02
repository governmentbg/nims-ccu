import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import projectRegistrationDataTemplateUrl from './forms/projectRegistrationData.html';
import { ProjectRegistrationDataCtrl } from './forms/projectRegistrationDataCtrl';
import projectRegistrationCommunicationDataTemplateUrl from './forms/projectRegistrationCommunicationData.html';
import { ProjectRegistrationCommunicationDataCtrl } from './forms/projectRegistrationCommunicationDataCtrl';
import projectCommunicationAnswerDataTemplateUrl from './forms/projectCommunicationAnswerData.html';
import projectVersionTemplateUrl from './forms/projectVersion.html';
import chooseCompanyModalTemplateUrl from './modals/chooseCompanyModal.html';
import { ChooseCompanyModalCtrl } from './modals/chooseCompanyModalCtrl';
import { ProjectFactory } from './resources/project';
import { ProjectCommunicationFileFactory } from './resources/projectCommunicationFile';
import { ProjectFileFactory } from './resources/projectFile';
import { ProjectVersionFactory } from './resources/projectVersion';
import { ProjectRegistrationCommunicationFactory } from './resources/projectRegistrationCommunication';
import { ProjectRegistrationCommunicationAnswerFactory } from './resources/projectRegistrationCommunicationAnswer';
import { ProjectMonitorstatRequestFactory } from './resources/projectMonitorstatRequest';
import projectRegistrationsViewTemplateUrl from './views/projectRegistrationsView.html';
import { ProjectRegistrationsViewCtrl } from './views/projectRegistrationsViewCtrl';
import projectRegistrationsEditTemplateUrl from './views/projectRegistrationsEdit.html';
import { ProjectRegistrationsEditCtrl } from './views/projectRegistrationsEditCtrl';
import projectRegistrationsNewStep1aTemplateUrl from './views/projectRegistrationsNewStep1a.html';
import { ProjectRegistrationsNewStep1aCtrl } from './views/projectRegistrationsNewStep1aCtrl';
import projectRegistrationsNewStep1bTemplateUrl from './views/projectRegistrationsNewStep1b.html';
import { ProjectRegistrationsNewStep1bCtrl } from './views/projectRegistrationsNewStep1bCtrl';
import projectRegistrationsNewStep2TemplateUrl from './views/projectRegistrationsNewStep2.html';
import { ProjectRegistrationsNewStep2Ctrl } from './views/projectRegistrationsNewStep2Ctrl';
import projectRegistrationsNewStep3TemplateUrl from './views/projectRegistrationsNewStep3.html';
import { ProjectRegistrationsNewStep3Ctrl } from './views/projectRegistrationsNewStep3Ctrl';
import projectRegistrationsSearchTemplateUrl from './views/projectRegistrationsSearch.html';
import { ProjectRegistrationsSearchCtrl } from './views/projectRegistrationsSearchCtrl';
import projectRegistrationCommunicationsEditTemplateUrl from './views/projectRegistrationCommunicationsEdit.html';
import { ProjectRegistrationCommunicationsEditCtrl } from './views/projectRegistrationCommunicationsEditCtrl';
import projectRegistrationCommunicationsSearchTemplateUrl from './views/projectRegistrationCommunicationsSearch.html';
import { ProjectRegistrationCommunicationsSearchCtrl } from './views/projectRegistrationCommunicationsSearchCtrl';
import projectRegistrationCommunicationAnswersEditTemplateUrl from './views/projectRegistrationCommunicationAnswersEdit.html';
import { ProjectRegistrationCommunicationAnswersEditCtrl } from './views/projectRegistrationCommunicationAnswersEditCtrl';

const ProjectsModule = angular
  .module('main.projects', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisProjectRegistrationData',
        templateUrl: projectRegistrationDataTemplateUrl,
        controller: ProjectRegistrationDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProjectVersion',
        templateUrl: projectVersionTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisProjectRegistrationCommunicationData',
        templateUrl: projectRegistrationCommunicationDataTemplateUrl,
        controller: ProjectRegistrationCommunicationDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProjectCommunicationAnswerData',
        templateUrl: projectCommunicationAnswerDataTemplateUrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseCompanyModal'                     , chooseCompanyModalTemplateUrl                                   , ChooseCompanyModalCtrl                     , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.projects'                                           , '/projects?programmePriorityId&procedureId&fromDate&toDate'                                                                                                                                                       ])
    .state(['root.projects.search'                                    , ''                              ,       ['@root'                           , projectRegistrationsSearchTemplateUrl                              , ProjectRegistrationsSearchCtrl                       ]])
    .state(['root.projects.newStep1a'                                 , '/newStep1a'                    ,       ['@root'                           , projectRegistrationsNewStep1aTemplateUrl                           , ProjectRegistrationsNewStep1aCtrl                    ]])
    .state(['root.projects.newStep1b'                                 , '/newStep1b'                    ,       ['@root'                           , projectRegistrationsNewStep1bTemplateUrl                           , ProjectRegistrationsNewStep1bCtrl                    ]])
    .state(['root.projects.newStep2'                                  , '/newStep2?pId&uinType&uin&code',       ['@root'                           , projectRegistrationsNewStep2TemplateUrl                            , ProjectRegistrationsNewStep2Ctrl                     ]])
    .state(['root.projects.newStep3'                                  , '/newStep3?pId&cId&xmlId'       ,       ['@root'                           , projectRegistrationsNewStep3TemplateUrl                            , ProjectRegistrationsNewStep3Ctrl                     ]])

    .state(['root.projects.view'                                      , '/:id?rf'                       , true, ['@root'                           , projectRegistrationsViewTemplateUrl                                , ProjectRegistrationsViewCtrl                         ]])
    .state(['root.projects.view.edit'                                 , ''                              ,       ['@root.projects.view'             , projectRegistrationsEditTemplateUrl                                , ProjectRegistrationsEditCtrl                         ]])

    .state(['root.projects.view.communications'                       , '/communications'                                                                                                                                                                                                 ])
    .state(['root.projects.view.communications.search'                , ''                              ,       ['@root.projects.view'             , projectRegistrationCommunicationsSearchTemplateUrl                 , ProjectRegistrationCommunicationsSearchCtrl          ]])
    .state(['root.projects.view.communications.edit'                  , '/:ind'                         ,       ['@root.projects.view'             , projectRegistrationCommunicationsEditTemplateUrl                   , ProjectRegistrationCommunicationsEditCtrl            ]])

    .state(['root.projects.view.communications.edit.answers'          , '/answers'                                                                                                                                                                                                        ])
    .state(['root.projects.view.communications.edit.answers.edit'     , '/:aid'                         ,       ['@root.projects.view'             , projectRegistrationCommunicationAnswersEditTemplateUrl             , ProjectRegistrationCommunicationAnswersEditCtrl      ]]);
    }
  ]);

export default ProjectsModule.name;
ProjectsModule.factory('Project', ProjectFactory);
ProjectsModule.factory('ProjectCommunicationFile', ProjectCommunicationFileFactory);
ProjectsModule.factory('ProjectFile', ProjectFileFactory);
ProjectsModule.factory('ProjectVersion', ProjectVersionFactory);
ProjectsModule.factory('ProjectRegistrationCommunication', ProjectRegistrationCommunicationFactory);
ProjectsModule.factory(
  'ProjectRegistrationCommunicationAnswer',
  ProjectRegistrationCommunicationAnswerFactory
);
ProjectsModule.factory('ProjectMonitorstatRequest', ProjectMonitorstatRequestFactory);
