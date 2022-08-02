import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import evalSessionDataTemplateUrl from './forms/evalSessionData.html';
import evalSessionDistributionTemplateUrl from './forms/evalSessionDistribution.html';
import { EvalSessionDistributionCtrl } from './forms/evalSessionDistributionCtrl';
import evalSessionEvaluationTemplateUrl from './forms/evalSessionEvaluation.html';
import { EvalSessionEvaluationCtrl } from './forms/evalSessionEvaluationCtrl';
import evalSessionProjectStandingTemplateUrl from './forms/evalSessionProjectStanding.html';
import { EvalSessionProjectStandingCtrl } from './forms/evalSessionProjectStandingCtrl';
import evalSessionReportTemplateUrl from './forms/evalSessionReport.html';
import { EvalSessionReportCtrl } from './forms/evalSessionReportCtrl';
import evalSessionSheetTemplateUrl from './forms/evalSessionSheet.html';
import { EvalSessionSheetCtrl } from './forms/evalSessionSheetCtrl';
import evalSessionStandingTemplateUrl from './forms/evalSessionStanding.html';
import { EvalSessionStandingCtrl } from './forms/evalSessionStandingCtrl';
import evalSessionStandpointTemplateUrl from './forms/evalSessionStandpoint.html';
import { EvalSessionStandpointCtrl } from './forms/evalSessionStandpointCtrl';
import evalSessionUserTemplateUrl from './forms/evalSessionUser.html';
import { EvalSessionUserCtrl } from './forms/evalSessionUserCtrl';
import projectCommunicationTemplateUrl from './forms/projectCommunication.html';
import { ProjectCommunicationCtrl } from './forms/projectCommunicationCtrl';
import projectMonitorstatRequestTemplateUrl from './forms/projectMonitorstatRequest.html';
import { ProjectMonitorstatRequestCtrl } from './forms/projectMonitorstatRequestCtrl';
import chooseAndEvaluateProjectModalTemplateUrl from './modals/chooseAndEvaluateProjectModal.html';
import { ChooseAndEvaluateProjectModalCtrl } from './modals/chooseAndEvaluateProjectModalCtrl';
import chooseProjectsModalTemplateUrl from './modals/chooseProjectsModal.html';
import { ChooseProjectsModalCtrl } from './modals/chooseProjectsModalCtrl';
import registerAnswerModalTemplateUrl from './modals/registerAnswerModal.html';
import { RegisterAnswerModalCtrl } from './modals/registerAnswerModalCtrl';
import executeAutomaticProjectEvaluationsModalTemplateUrl from './modals/executeAutomaticProjectEvaluationsModal.html';
import { ExecuteAutomaticProjectEvaluationsModalCtrl } from './modals/executeAutomaticProjectEvaluationsModalCtrl';
import chooseMonitorstatRequestCompaniesModalTemplateUrl from './modals/chooseMonitorstatRequestCompaniesModal.html';
import { ChooseMonitorstatRequestCompaniesModalCtrl } from './modals/chooseMonitorstatRequestCompaniesModalCtrl';
import chooseProjectsForAutomaticMonitorstatRequestModalTemplateUrl from './modals/chooseProjectsForAutomaticMonitorstatRequestModal.html';
import { ChooseProjectsForAutomaticMonitorstatRequestModalCtrl } from './modals/chooseProjectsForAutomaticMonitorstatRequestModalCtrl';
import { EvalSessionFactory } from './resources/evalSession';
import { EvalSessionAutomaticProjectMonitorstatRequestFactory } from './resources/evalSessionAutomaticProjectMonitorstatRequest';
import { EvalSessionDistributionFactory } from './resources/evalSessionDistribution';
import { EvalSessionDocumentFactory } from './resources/evalSessionDocument';
import { EvalSessionEvaluationFactory } from './resources/evalSessionEvaluation';
import { EvalSessionFileFactory } from './resources/evalSessionFile';
import { EvalSessionProjectFactory } from './resources/evalSessionProject';
import { EvalSessionProjectStandingFactory } from './resources/evalSessionProjectStanding';
import { EvalSessionReportFactory } from './resources/evalSessionReport';
import { EvalSessionSheetFactory } from './resources/evalSessionSheet';
import { EvalSessionStandingFactory } from './resources/evalSessionStanding';
import { EvalSessionStandpointFactory } from './resources/evalSessionStandpoint';
import { EvalSessionUserFactory } from './resources/evalSessionUser';
import { MyEvalSessionFactory } from './resources/myEvalSession';
import { MyEvalSessionSheetFactory } from './resources/myEvalSessionSheet';
import { MyEvalSessionStandpointFactory } from './resources/myEvalSessionStandpoint';
import { ProjectCommunicationFactory } from './resources/projectCommunication';
import { ProjectCommunicationAnswerFactory } from './resources/projectCommunicationAnswer';
import { EvalSessionResultFactory } from './resources/evalSessionResult';
import { EvalSessionStandingRearrangeFactory } from './resources/evalSessionStandingRearrange';
import { ProjectMonitorstatRequestFileFactory } from './resources/projectMonitorstatRequestFile';
import { ProjectMonitorstatResponseFileFactory } from './resources/projectMonitorstatResponseFile';
import { EvalSessionAutomaticProjectEvaluationFactory } from './resources/evalSessionAutomaticProjectEvaluation';
import evalSessionAllDocsSearchTemplateUrl from './views/evalSessionAllDocsSearch.html';
import { EvalSessionAllDocsSearchCtrl } from './views/evalSessionAllDocsSearchCtrl';
import evalSessionCommunicationsSearchTemplateUrl from './views/evalSessionCommunicationsSearch.html';
import { EvalSessionCommunicationsSearchCtrl } from './views/evalSessionCommunicationsSearchCtrl';
import evalSessionDistributionsEditTemplateUrl from './views/evalSessionDistributionsEdit.html';
import { EvalSessionDistributionsEditCtrl } from './views/evalSessionDistributionsEditCtrl';
import evalSessionDistributionsNewTemplateUrl from './views/evalSessionDistributionsNew.html';
import { EvalSessionDistributionsNewCtrl } from './views/evalSessionDistributionsNewCtrl';
import evalSessionDistributionsSearchTemplateUrl from './views/evalSessionDistributionsSearch.html';
import { EvalSessionDistributionsSearchCtrl } from './views/evalSessionDistributionsSearchCtrl';
import evalSessionDocumentsEditTemplateUrl from './views/evalSessionDocumentsEdit.html';
import { EvalSessionDocumentsEditCtrl } from './views/evalSessionDocumentsEditCtrl';
import evalSessionDocumentsNewTemplateUrl from './views/evalSessionDocumentsNew.html';
import { EvalSessionDocumentsNewCtrl } from './views/evalSessionDocumentsNewCtrl';
import evalSessionEvaluationsEditTemplateUrl from './views/evalSessionEvaluationsEdit.html';
import { EvalSessionEvaluationsEditCtrl } from './views/evalSessionEvaluationsEditCtrl';
import evalSessionEvaluationsNewTemplateUrl from './views/evalSessionEvaluationsNew.html';
import { EvalSessionEvaluationsNewCtrl } from './views/evalSessionEvaluationsNewCtrl';
import evalSessionProjectCommunicationEditTemplateUrl from './views/evalSessionProjectCommunicationEdit.html';
import { EvalSessionProjectCommunicationEditCtrl } from './views/evalSessionProjectCommunicationEditCtrl';
import evalSessionProjectCommunicationAnswerEditTemplateUrl from './views/evalSessionProjectCommunicationAnswerEdit.html';
import { EvalSessionProjectCommunicationAnswerEditCtrl } from './views/evalSessionProjectCommunicationAnswerEditCtrl';
import evalSessionProjectsSearchTemplateUrl from './views/evalSessionProjectsSearch.html';
import { EvalSessionProjectsSearchCtrl } from './views/evalSessionProjectsSearchCtrl';
import evalSessionProjectStandingsEditTemplateUrl from './views/evalSessionProjectStandingsEdit.html';
import { EvalSessionProjectStandingsEditCtrl } from './views/evalSessionProjectStandingsEditCtrl';
import evalSessionProjectStandingsNewTemplateUrl from './views/evalSessionProjectStandingsNew.html';
import { EvalSessionProjectStandingsNewCtrl } from './views/evalSessionProjectStandingsNewCtrl';
import evalSessionProjectsViewTemplateUrl from './views/evalSessionProjectsView.html';
import { EvalSessionProjectsViewCtrl } from './views/evalSessionProjectsViewCtrl';
import evalSessionProjectVersionEditTemplateUrl from './views/evalSessionProjectVersionEdit.html';
import { EvalSessionProjectVersionEditCtrl } from './views/evalSessionProjectVersionEditCtrl';
import evalSessionProjectVersionNewTemplateUrl from './views/evalSessionProjectVersionNew.html';
import { EvalSessionProjectVersionNewCtrl } from './views/evalSessionProjectVersionNewCtrl';
import evalSessionReportsEditTemplateUrl from './views/evalSessionReportsEdit.html';
import { EvalSessionReportsEditCtrl } from './views/evalSessionReportsEditCtrl';
import evalSessionReportsNewTemplateUrl from './views/evalSessionReportsNew.html';
import { EvalSessionReportsNewCtrl } from './views/evalSessionReportsNewCtrl';
import evalSessionsEditTemplateUrl from './views/evalSessionsEdit.html';
import { EvalSessionsEditCtrl } from './views/evalSessionsEditCtrl';
import evalSessionSheetsEditTemplateUrl from './views/evalSessionSheetsEdit.html';
import { EvalSessionSheetsEditCtrl } from './views/evalSessionSheetsEditCtrl';
import evalSessionSheetsNewTemplateUrl from './views/evalSessionSheetsNew.html';
import { EvalSessionSheetsNewCtrl } from './views/evalSessionSheetsNewCtrl';
import evalSessionSheetsSearchTemplateUrl from './views/evalSessionSheetsSearch.html';
import { EvalSessionSheetsSearchCtrl } from './views/evalSessionSheetsSearchCtrl';
import evalSessionsNewTemplateUrl from './views/evalSessionsNew.html';
import { EvalSessionsNewCtrl } from './views/evalSessionsNewCtrl';
import evalSessionsSearchTemplateUrl from './views/evalSessionsSearch.html';
import { EvalSessionsSearchCtrl } from './views/evalSessionsSearchCtrl';
import evalSessionStandingsEditTemplateUrl from './views/evalSessionStandingsEdit.html';
import { EvalSessionStandingsEditCtrl } from './views/evalSessionStandingsEditCtrl';
import evalSessionStandingsRearrangeTemplateUrl from './views/evalSessionStandingsRearrange.html';
import { EvalSessionStandingsRearrangeCtrl } from './views/evalSessionStandingsRearrangeCtrl';
import evalSessionStandingsNewTemplateUrl from './views/evalSessionStandingsNew.html';
import { EvalSessionStandingsNewCtrl } from './views/evalSessionStandingsNewCtrl';
import evalSessionStandingsSearchTemplateUrl from './views/evalSessionStandingsSearch.html';
import { EvalSessionStandingsSearchCtrl } from './views/evalSessionStandingsSearchCtrl';
import evalSessionStandpointsEditTemplateUrl from './views/evalSessionStandpointsEdit.html';
import { EvalSessionStandpointsEditCtrl } from './views/evalSessionStandpointsEditCtrl';
import evalSessionStandpointsNewTemplateUrl from './views/evalSessionStandpointsNew.html';
import { EvalSessionStandpointsNewCtrl } from './views/evalSessionStandpointsNewCtrl';
import evalSessionStandpointsSearchTemplateUrl from './views/evalSessionStandpointsSearch.html';
import { EvalSessionStandpointsSearchCtrl } from './views/evalSessionStandpointsSearchCtrl';
import evalSessionsViewTemplateUrl from './views/evalSessionsView.html';
import { EvalSessionsViewCtrl } from './views/evalSessionsViewCtrl';
import evalSessionUsersEditTemplateUrl from './views/evalSessionUsersEdit.html';
import { EvalSessionUsersEditCtrl } from './views/evalSessionUsersEditCtrl';
import evalSessionUsersNewTemplateUrl from './views/evalSessionUsersNew.html';
import { EvalSessionUsersNewCtrl } from './views/evalSessionUsersNewCtrl';
import evalSessionUsersSearchTemplateUrl from './views/evalSessionUsersSearch.html';
import { EvalSessionUsersSearchCtrl } from './views/evalSessionUsersSearchCtrl';
import myEvalSessionsEditTemplateUrl from './views/myEvalSessionsEdit.html';
import { MyEvalSessionsEditCtrl } from './views/myEvalSessionsEditCtrl';
import myEvalSessionSheetsEditTemplateUrl from './views/myEvalSessionSheetsEdit.html';
import { MyEvalSessionSheetsEditCtrl } from './views/myEvalSessionSheetsEditCtrl';
import myEvalSessionSheetsProjectTemplateUrl from './views/myEvalSessionSheetsProject.html';
import { MyEvalSessionSheetsProjectCtrl } from './views/myEvalSessionSheetsProjectCtrl';
import myEvalSessionSheetsProjectCommunicationEditTemplateUrl from './views/myEvalSessionSheetsProjectCommunicationEdit.html';
import { MyEvalSessionSheetsProjectCommunicationEditCtrl } from './views/myEvalSessionSheetsProjectCommunicationEditCtrl';
import myEvalSessionSheetsSearchTemplateUrl from './views/myEvalSessionSheetsSearch.html';
import { MyEvalSessionSheetsSearchCtrl } from './views/myEvalSessionSheetsSearchCtrl';
import myEvalSessionStandpointsEditTemplateUrl from './views/myEvalSessionStandpointsEdit.html';
import { MyEvalSessionStandpointsEditCtrl } from './views/myEvalSessionStandpointsEditCtrl';
import myEvalSessionStandpointsProjectTemplateUrl from './views/myEvalSessionStandpointsProject.html';
import { MyEvalSessionStandpointsProjectCtrl } from './views/myEvalSessionStandpointsProjectCtrl';
import myEvalSessionStandpointsProjectCommunicationEditTemplateUrl from './views/myEvalSessionStandpointsProjectCommunicationEdit.html';
import { MyEvalSessionStandpointsProjectCommunicationEditCtrl } from './views/myEvalSessionStandpointsProjectCommunicationEditCtrl';
import myEvalSessionStandpointsSearchTemplateUrl from './views/myEvalSessionStandpointsSearch.html';
import { MyEvalSessionStandpointsSearchCtrl } from './views/myEvalSessionStandpointsSearchCtrl';
import myEvalSessionsViewTemplateUrl from './views/myEvalSessionsView.html';
import { MyEvalSessionsViewCtrl } from './views/myEvalSessionsViewCtrl';
import evalSessionResultSearchTemplateUrl from './views/evalSessionResultSearch.html';
import { EvalSessionResultsSearchCtrl } from './views/evalSessionResultSearchCtrl';
import evalSessionResultsNewTemplateUrl from './views/evalSessionResultNew.html';
import { EvalSessionResultsNewCtrl } from './views/evalSessionResultNewCtrl';
import evalSessionResultsEditTemplateUrl from './views/evalSessionResultEdit.html';
import { EvalSessionResultsEditCtrl } from './views/evalSessionResultEditCtrl';
import evalSessionProjectMonitorstatNewTemplateUrl from './views/evalSessionProjectMonitorstatNew.html';
import { EvalSessionProjectMonitorstatNewCtrl } from './views/evalSessionProjectMonitorstatNewCtrl';
import evalSessionProjectMonitorstatEditTemplateUrl from './views/evalSessionProjectMonitorstatEdit.html';
import { EvalSessionProjectMonitorstatEditCtrl } from './views/evalSessionProjectMonitorstatEditCtrl';

const EvalSessionsModule = angular
  .module('main.evalSessions', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisProjectCommunication',
        templateUrl: projectCommunicationTemplateUrl,
        controller: ProjectCommunicationCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisEvalSessionData',
        templateUrl: evalSessionDataTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisEvalSessionUser',
        templateUrl: evalSessionUserTemplateUrl,
        controller: EvalSessionUserCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisEvalSessionSheet',
        templateUrl: evalSessionSheetTemplateUrl,
        controller: EvalSessionSheetCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisEvalSessionStandpoint',
        templateUrl: evalSessionStandpointTemplateUrl,
        controller: EvalSessionStandpointCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisEvalSessionDistribution',
        templateUrl: evalSessionDistributionTemplateUrl,
        controller: EvalSessionDistributionCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisEvalSessionEvaluation',
        templateUrl: evalSessionEvaluationTemplateUrl,
        controller: EvalSessionEvaluationCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisEvalSessionProjectStanding',
        templateUrl: evalSessionProjectStandingTemplateUrl,
        controller: EvalSessionProjectStandingCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisEvalSessionStanding',
        templateUrl: evalSessionStandingTemplateUrl,
        controller: EvalSessionStandingCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisEvalSessionReport',
        templateUrl: evalSessionReportTemplateUrl,
        controller: EvalSessionReportCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProjectMonitorstatRequest',
        templateUrl: projectMonitorstatRequestTemplateUrl,
        controller: ProjectMonitorstatRequestCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseProjectsModal'                      , chooseProjectsModalTemplateUrl                              , ChooseProjectsModalCtrl                     , 'xlg')
    .modal('chooseAndEvaluateProjectModal'            , chooseAndEvaluateProjectModalTemplateUrl                    , ChooseAndEvaluateProjectModalCtrl           , 'xlg')
    .modal('chooseMonitorstatRequestCompaniesModal'   , chooseMonitorstatRequestCompaniesModalTemplateUrl           , ChooseMonitorstatRequestCompaniesModalCtrl  , 'xlg')
    .modal('registerAnswerModal'                      , registerAnswerModalTemplateUrl                              , RegisterAnswerModalCtrl                     , 'sm' )
    .modal('createAutomaticProjectVersionsModal'      , executeAutomaticProjectEvaluationsModalTemplateUrl          , ExecuteAutomaticProjectEvaluationsModalCtrl , 'md' )
    .modal('chooseProjectsForAutomaticMonitorstatRequestModal'  , chooseProjectsForAutomaticMonitorstatRequestModalTemplateUrl  , ChooseProjectsForAutomaticMonitorstatRequestModalCtrl       , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.evalSessions'                                                      , '/evalSessions?procedureId'                                                                                                                                                                                ])
    .state(['root.evalSessions.search'                                               , ''                 ,       ['@root'                           , evalSessionsSearchTemplateUrl                                           , EvalSessionsSearchCtrl                          ]])
    .state(['root.evalSessions.new'                                                  , '/new'             ,       ['@root'                           , evalSessionsNewTemplateUrl                                              , EvalSessionsNewCtrl                             ]])
    .state(['root.evalSessions.view'                                                 , '/:id?rf'          , true, ['@root'                           , evalSessionsViewTemplateUrl                                             , EvalSessionsViewCtrl                            ]])
    .state(['root.evalSessions.view.edit'                                            , ''                 ,       ['@root.evalSessions.view'         , evalSessionsEditTemplateUrl                                             , EvalSessionsEditCtrl                            ]])

    .state(['root.evalSessions.view.users'                                           , '/users'                                                                                                                                                                                                   ])
    .state(['root.evalSessions.view.users.search'                                    , ''                 ,       ['@root.evalSessions.view'         , evalSessionUsersSearchTemplateUrl                                       , EvalSessionUsersSearchCtrl                      ]])
    .state(['root.evalSessions.view.users.new'                                       , '/new'             ,       ['@root.evalSessions.view'         , evalSessionUsersNewTemplateUrl                                          , EvalSessionUsersNewCtrl                         ]])
    .state(['root.evalSessions.view.users.edit'                                      , '/:ind'            ,       ['@root.evalSessions.view'         , evalSessionUsersEditTemplateUrl                                         , EvalSessionUsersEditCtrl                        ]])

    .state(['root.evalSessions.view.projects'                                        , '/projects'                                                                                                                                                                                                ])
    .state(['root.evalSessions.view.projects.search'                                 , ''                 ,       ['@root.evalSessions.view'         , evalSessionProjectsSearchTemplateUrl                                    , EvalSessionProjectsSearchCtrl                   ]])
    .state(['root.evalSessions.view.projects.view'                                   , '/:ind'            ,       ['@root.evalSessions.view'         , evalSessionProjectsViewTemplateUrl                                      , EvalSessionProjectsViewCtrl                     ]])
    .state(['root.evalSessions.view.projects.view.versions'                          , '/versions'                                                                                                                                                                                                ])
    .state(['root.evalSessions.view.projects.view.versions.new'                      , '/new'             ,       ['@root.evalSessions.view'         , evalSessionProjectVersionNewTemplateUrl                                 , EvalSessionProjectVersionNewCtrl                ]])
    .state(['root.evalSessions.view.projects.view.versions.edit'                     , '/:vid'            ,       ['@root.evalSessions.view'         , evalSessionProjectVersionEditTemplateUrl                                , EvalSessionProjectVersionEditCtrl               ]])

    .state(['root.evalSessions.view.projects.view.communications'                    , '/communications'                                                                                                                                                                                          ])
    .state(['root.evalSessions.view.projects.view.communications.edit'               , '/:mid'            ,       ['@root.evalSessions.view'         , evalSessionProjectCommunicationEditTemplateUrl                          , EvalSessionProjectCommunicationEditCtrl         ]])

    .state(['root.evalSessions.view.projects.view.communications.edit.answers'       , '/answers'                                                                                                                                                                                                 ])
    .state(['root.evalSessions.view.projects.view.communications.edit.answers.edit'  , '/:aid'            ,       ['@root.evalSessions.view'         , evalSessionProjectCommunicationAnswerEditTemplateUrl                    , EvalSessionProjectCommunicationAnswerEditCtrl   ]])

    .state(['root.evalSessions.view.projects.view.standings'                         , '/standings'                                                                                                                                                                                               ])
    .state(['root.evalSessions.view.projects.view.standings.new'                     , '/new?p'           ,       ['@root.evalSessions.view'         , evalSessionProjectStandingsNewTemplateUrl                               , EvalSessionProjectStandingsNewCtrl              ]])
    .state(['root.evalSessions.view.projects.view.standings.edit'                    , '/:sid'            ,       ['@root.evalSessions.view'         , evalSessionProjectStandingsEditTemplateUrl                              , EvalSessionProjectStandingsEditCtrl             ]])

    .state(['root.evalSessions.view.projects.view.monitorstat'                       , '/monitorstat'                                                                                                                                                                                             ])
    .state(['root.evalSessions.view.projects.view.monitorstat.new'                   , '/new'             ,       ['@root.evalSessions.view'         , evalSessionProjectMonitorstatNewTemplateUrl                             , EvalSessionProjectMonitorstatNewCtrl            ]])
    .state(['root.evalSessions.view.projects.view.monitorstat.edit'                  , '/:rid'            ,       ['@root.evalSessions.view'         , evalSessionProjectMonitorstatEditTemplateUrl                            , EvalSessionProjectMonitorstatEditCtrl           ]])

    .state(['root.evalSessions.view.sheets'                                          , '/sheets?project&evalTableType&distribution&assessor&statuses'                                                                                                                                             ])
    .state(['root.evalSessions.view.sheets.search'                                   , ''                 ,       ['@root.evalSessions.view'         , evalSessionSheetsSearchTemplateUrl                                      , EvalSessionSheetsSearchCtrl                     ]])
    .state(['root.evalSessions.view.sheets.new'                                      , '/new?sheetId'     ,       ['@root.evalSessions.view'         , evalSessionSheetsNewTemplateUrl                                         , EvalSessionSheetsNewCtrl                        ]])
    .state(['root.evalSessions.view.sheets.edit'                                     , '/:ind'            ,       ['@root.evalSessions.view'         , evalSessionSheetsEditTemplateUrl                                        , EvalSessionSheetsEditCtrl                       ]])

    .state(['root.evalSessions.view.standpoints'                                     , '/standpoints?project&user&statuses'                                                                                                                                                                       ])
    .state(['root.evalSessions.view.standpoints.search'                              , ''                 ,       ['@root.evalSessions.view'         , evalSessionStandpointsSearchTemplateUrl                                 , EvalSessionStandpointsSearchCtrl                ]])
    .state(['root.evalSessions.view.standpoints.new'                                 , '/new'             ,       ['@root.evalSessions.view'         , evalSessionStandpointsNewTemplateUrl                                    , EvalSessionStandpointsNewCtrl                   ]])
    .state(['root.evalSessions.view.standpoints.edit'                                , '/:ind'            ,       ['@root.evalSessions.view'         , evalSessionStandpointsEditTemplateUrl                                   , EvalSessionStandpointsEditCtrl                  ]])

    .state(['root.evalSessions.view.distributions'                                   , '/distributions'                                                                                                                                                                                           ])
    .state(['root.evalSessions.view.distributions.search'                            , ''                 ,       ['@root.evalSessions.view'         , evalSessionDistributionsSearchTemplateUrl                               , EvalSessionDistributionsSearchCtrl              ]])
    .state(['root.evalSessions.view.distributions.new'                               , '/new?t'           ,       ['@root.evalSessions.view'         , evalSessionDistributionsNewTemplateUrl                                  , EvalSessionDistributionsNewCtrl                 ]])
    .state(['root.evalSessions.view.distributions.edit'                              , '/:ind'            ,       ['@root.evalSessions.view'         , evalSessionDistributionsEditTemplateUrl                                 , EvalSessionDistributionsEditCtrl                ]])

    .state(['root.evalSessions.view.communication'                                   , '/communication?projectId&statusId&questionDateFrom&questionDateTo'                                                                                                                                        ])
    .state(['root.evalSessions.view.communication.search'                            , ''                 ,       ['@root.evalSessions.view'         , evalSessionCommunicationsSearchTemplateUrl                              , EvalSessionCommunicationsSearchCtrl             ]])

    .state(['root.evalSessions.view.allDocs'                                         , '/allDocs'                                                                                                                                                                                                 ])
    .state(['root.evalSessions.view.allDocs.search'                                  , ''                 ,       ['@root.evalSessions.view'         , evalSessionAllDocsSearchTemplateUrl                                     , EvalSessionAllDocsSearchCtrl                    ]])
    .state(['root.evalSessions.view.allDocs.docs'                                    , '/docs'                                                                                                                                                                                                    ])
    .state(['root.evalSessions.view.allDocs.docs.new'                                , '/new'             ,       ['@root.evalSessions.view'         , evalSessionDocumentsNewTemplateUrl                                      , EvalSessionDocumentsNewCtrl                     ]])
    .state(['root.evalSessions.view.allDocs.docs.edit'                               , '/:ind'            ,       ['@root.evalSessions.view'         , evalSessionDocumentsEditTemplateUrl                                     , EvalSessionDocumentsEditCtrl                    ]])
    .state(['root.evalSessions.view.allDocs.reports'                                 , '/reports'                                                                                                                                                                                                 ])
    .state(['root.evalSessions.view.allDocs.reports.new'                             , '/new'             ,       ['@root.evalSessions.view'         , evalSessionReportsNewTemplateUrl                                        , EvalSessionReportsNewCtrl                       ]])
    .state(['root.evalSessions.view.allDocs.reports.edit'                            , '/:ind'            ,       ['@root.evalSessions.view'         , evalSessionReportsEditTemplateUrl                                       , EvalSessionReportsEditCtrl                      ]])

    .state(['root.evalSessions.my'                                                   , '/my'                                                                                                                                                                                                      ])
    .state(['root.evalSessions.my.view'                                              , '/:id?rf'          , true, ['@root'                           , myEvalSessionsViewTemplateUrl                                           , MyEvalSessionsViewCtrl                          ]])
    .state(['root.evalSessions.my.view.edit'                                         , ''                 ,       ['@root.evalSessions.my.view'      , myEvalSessionsEditTemplateUrl                                           , MyEvalSessionsEditCtrl                          ]])

    .state(['root.evalSessions.my.view.sheets'                                       , '/sheets?project&evalTableType&distribution&statuses'                                                                                                                                                      ])
    .state(['root.evalSessions.my.view.sheets.search'                                , ''                 ,       ['@root.evalSessions.my.view'      , myEvalSessionSheetsSearchTemplateUrl                                    , MyEvalSessionSheetsSearchCtrl                   ]])
    .state(['root.evalSessions.my.view.sheets.edit'                                  , '/:ind'            ,       ['@root.evalSessions.my.view'      , myEvalSessionSheetsEditTemplateUrl                                      , MyEvalSessionSheetsEditCtrl                     ]])
    .state(['root.evalSessions.my.view.sheets.edit.project'                          , '/project'         ,       ['@root.evalSessions.my.view'      , myEvalSessionSheetsProjectTemplateUrl                                   , MyEvalSessionSheetsProjectCtrl                  ]])
    .state(['root.evalSessions.my.view.sheets.edit.project.communications'           , '/communications'                                                                                                                                                                                          ])
    .state(['root.evalSessions.my.view.sheets.edit.project.communications.edit'      , '/:mid'            ,       ['@root.evalSessions.my.view'      , myEvalSessionSheetsProjectCommunicationEditTemplateUrl                  , MyEvalSessionSheetsProjectCommunicationEditCtrl ]])

    .state(['root.evalSessions.my.view.standpoints'                                  , '/standpoints?project&statuses'                                                                                                                                                                            ])
    .state(['root.evalSessions.my.view.standpoints.search'                           , ''                 ,       ['@root.evalSessions.my.view'      , myEvalSessionStandpointsSearchTemplateUrl                               , MyEvalSessionStandpointsSearchCtrl              ]])
    .state(['root.evalSessions.my.view.standpoints.edit'                             , '/:ind'            ,       ['@root.evalSessions.my.view'      , myEvalSessionStandpointsEditTemplateUrl                                 , MyEvalSessionStandpointsEditCtrl                ]])
    .state(['root.evalSessions.my.view.standpoints.edit.project'                     , '/project'         ,       ['@root.evalSessions.my.view'      , myEvalSessionStandpointsProjectTemplateUrl                              , MyEvalSessionStandpointsProjectCtrl             ]])
    .state(['root.evalSessions.my.view.standpoints.edit.project.communications'      , '/communications'                                                                                                                                                                                          ])
    .state(['root.evalSessions.my.view.standpoints.edit.project.communications.edit' , '/:mid'            ,       ['@root.evalSessions.my.view'      , myEvalSessionStandpointsProjectCommunicationEditTemplateUrl             , MyEvalSessionStandpointsProjectCommunicationEditCtrl ]])

    .state(['root.evalSessions.view.projects.evaluations'                            , '/evaluations'                                                                                                                                                                                             ])
    .state(['root.evalSessions.view.projects.evaluations.new'                        , '/new?pId&t'       ,       ['@root.evalSessions.view'         , evalSessionEvaluationsNewTemplateUrl                                    , EvalSessionEvaluationsNewCtrl                   ]])
    .state(['root.evalSessions.view.projects.evaluations.edit'                       , '/:ind'            ,       ['@root.evalSessions.view'         , evalSessionEvaluationsEditTemplateUrl                                   , EvalSessionEvaluationsEditCtrl                  ]])

    .state(['root.evalSessions.view.standings'                                       , '/standings'                                                                                                                                                                                               ])
    .state(['root.evalSessions.view.standings.search'                                , ''                 ,       ['@root.evalSessions.view'         , evalSessionStandingsSearchTemplateUrl                                   , EvalSessionStandingsSearchCtrl                  ]])
    .state(['root.evalSessions.view.standings.new'                                   , '/new?t'           ,       ['@root.evalSessions.view'         , evalSessionStandingsNewTemplateUrl                                      , EvalSessionStandingsNewCtrl                     ]])
    .state(['root.evalSessions.view.standings.edit'                                  , '/:ind'            ,       ['@root.evalSessions.view'         , evalSessionStandingsEditTemplateUrl                                     , EvalSessionStandingsEditCtrl                    ]])
    .state(['root.evalSessions.view.standings.rearrange'                             , '/:ind/reorder'    ,       ['@root.evalSessions.view'         , evalSessionStandingsRearrangeTemplateUrl                                , EvalSessionStandingsRearrangeCtrl               ]])
    
    .state(['root.evalSessions.view.result'                                          , '/results?type'                                                                                                                                                                                            ])
    .state(['root.evalSessions.view.result.search'                                   , ''                 ,       ['@root.evalSessions.view'         , evalSessionResultSearchTemplateUrl                                      , EvalSessionResultsSearchCtrl                    ]])
    .state(['root.evalSessions.view.result.new'                                      , '/new'             ,       ['@root.evalSessions.view'         , evalSessionResultsNewTemplateUrl                                        , EvalSessionResultsNewCtrl                       ]])
    .state(['root.evalSessions.view.result.edit'                                     , '/:ind'            ,       ['@root.evalSessions.view'         , evalSessionResultsEditTemplateUrl                                       , EvalSessionResultsEditCtrl                      ]]);
    }
  ]);

export default EvalSessionsModule.name;
EvalSessionsModule.factory('EvalSession', EvalSessionFactory);
EvalSessionsModule.factory('EvalSessionDistribution', EvalSessionDistributionFactory);
EvalSessionsModule.factory('EvalSessionDocument', EvalSessionDocumentFactory);
EvalSessionsModule.factory('EvalSessionEvaluation', EvalSessionEvaluationFactory);
EvalSessionsModule.factory('EvalSessionFile', EvalSessionFileFactory);
EvalSessionsModule.factory('EvalSessionProject', EvalSessionProjectFactory);
EvalSessionsModule.factory('EvalSessionProjectStanding', EvalSessionProjectStandingFactory);
EvalSessionsModule.factory('EvalSessionReport', EvalSessionReportFactory);
EvalSessionsModule.factory('EvalSessionSheet', EvalSessionSheetFactory);
EvalSessionsModule.factory('EvalSessionStanding', EvalSessionStandingFactory);
EvalSessionsModule.factory('EvalSessionStandpoint', EvalSessionStandpointFactory);
EvalSessionsModule.factory('EvalSessionUser', EvalSessionUserFactory);
EvalSessionsModule.factory('MyEvalSession', MyEvalSessionFactory);
EvalSessionsModule.factory('MyEvalSessionSheet', MyEvalSessionSheetFactory);
EvalSessionsModule.factory('MyEvalSessionStandpoint', MyEvalSessionStandpointFactory);
EvalSessionsModule.factory('ProjectCommunication', ProjectCommunicationFactory);
EvalSessionsModule.factory('ProjectCommunicationAnswer', ProjectCommunicationAnswerFactory);
EvalSessionsModule.factory('EvalSessionResult', EvalSessionResultFactory);
EvalSessionsModule.factory('EvalSessionStandingRearrange', EvalSessionStandingRearrangeFactory);
EvalSessionsModule.factory('ProjectMonitorstatRequestFile', ProjectMonitorstatRequestFileFactory);
EvalSessionsModule.factory('ProjectMonitorstatResponseFile', ProjectMonitorstatResponseFileFactory);
EvalSessionsModule.factory(
  'EvalSessionAutomaticProjectEvaluation',
  EvalSessionAutomaticProjectEvaluationFactory
);
EvalSessionsModule.factory(
  'EvalSessionAutomaticProjectMonitorstatRequest',
  EvalSessionAutomaticProjectMonitorstatRequestFactory
);
