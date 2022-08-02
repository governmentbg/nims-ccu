import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { ActionLogFactory } from './resources/actionLog';
import internalActionLogsSearchTemplateUrl from './views/internalActionLogsSearch.html';
import { InternalActionLogsSearchCtrl } from './views/internalActionLogsSearchCtrl';
import loginActionLogsSearchTemplateUrl from './views/loginActionLogsSearch.html';
import { LoginActionLogsSearchCtrl } from './views/loginActionLogsSearchCtrl';
import portalActionLogsSearchTemplateUrl from './views/portalActionLogsSearch.html';
import { PortalActionLogsSearchCtrl } from './views/portalActionLogsSearchCtrl';
import procedureActionLogsSearchTemplateUrl from './views/procedureActionLogsSearch.html';
import { ProcedureActionLogsSearchCtrl } from './views/procedureActionLogsSearchCtrl';
import procedureActionLogsViewTemplateUrl from './views/procedureActionLogsView.html';
import { ProcedureActionLogsViewCtrl } from './views/procedureActionLogsViewCtrl';

const ActionLogsModule = angular
  .module('main.actionLogs', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.internalActionLogs'                                 , '/internalActionLogs?actionId&aggregateRootId&username&remoteIpAddress&logDate'                                                                                                                            ])
    .state(['root.internalActionLogs.search'                          , ''                 ,       ['@root'                           , internalActionLogsSearchTemplateUrl                                       , InternalActionLogsSearchCtrl           ]])

    .state(['root.procedureActionLogs'                                , '/procedureActionLogs?actionId&aggregateRootId&username&remoteIpAddress&logDate'                                                                                                                           ])
    .state(['root.procedureActionLogs.search'                         , ''                 ,       ['@root'                           , procedureActionLogsSearchTemplateUrl                                      , ProcedureActionLogsSearchCtrl          ]])
    .state(['root.procedureActionLogs.view'                           , '/:id'             ,       ['@root'                           , procedureActionLogsViewTemplateUrl                                        , ProcedureActionLogsViewCtrl            ]])

    .state(['root.portalActionLogs'                                   , '/portalActionLogs?actionId&aggregateRootId&email&remoteIpAddress&logDate'                                                                                                                                 ])
    .state(['root.portalActionLogs.search'                            , ''                 ,       ['@root'                           , portalActionLogsSearchTemplateUrl                                         , PortalActionLogsSearchCtrl             ]])

    .state(['root.loginActionLogs'                                    , '/loginActionLogs?username&remoteIpAddress&logDate'                                                                                                                                                        ])
    .state(['root.loginActionLogs.search'                             , ''                 ,       ['@root'                           , loginActionLogsSearchTemplateUrl                                          , LoginActionLogsSearchCtrl              ]]);
    }
  ]);

export default ActionLogsModule.name;
ActionLogsModule.factory('ActionLog', ActionLogFactory);
