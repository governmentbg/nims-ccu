import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { ProgrammePriorityPrognosisFactory } from './resources/programmePriorityPrognosis';
import programmePriorityPrognosesEditTemplateUrl from './views/programmePriorityPrognosesEdit.html';
import { ProgrammePriorityPrognosesEditCtrl } from './views/programmePriorityPrognosesEditCtrl';
import programmePriorityPrognosesNewTemplateUrl from './views/programmePriorityPrognosesNew.html';
import { ProgrammePriorityPrognosesNewCtrl } from './views/programmePriorityPrognosesNewCtrl';
import programmePriorityPrognosesSearchTemplateUrl from './views/programmePriorityPrognosesSearch.html';
import { ProgrammePriorityPrognosesSearchCtrl } from './views/programmePriorityPrognosesSearchCtrl';

const PrognosesProgrammePriorityModule = angular
  .module('main.prognoses.programmePriority', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.programmePriorityPrognoses'                                      , '/programmePriorityPrognoses?year&month'                                                                                                                                                                                                                    ])
    .state(['root.programmePriorityPrognoses.search'                               , ''                  ,       ['@root'                                            , programmePriorityPrognosesSearchTemplateUrl                           , ProgrammePriorityPrognosesSearchCtrl                          ]])
    .state(['root.programmePriorityPrognoses.new'                                  , '/new'              ,       ['@root'                                            , programmePriorityPrognosesNewTemplateUrl                              , ProgrammePriorityPrognosesNewCtrl                             ]])
    .state(['root.programmePriorityPrognoses.view'                                 , '/:id?rf'           ,       ['@root'                                            , programmePriorityPrognosesEditTemplateUrl                             , ProgrammePriorityPrognosesEditCtrl                            ]]);
    }
  ]);

export default PrognosesProgrammePriorityModule.name;
PrognosesProgrammePriorityModule.factory(
  'ProgrammePriorityPrognosis',
  ProgrammePriorityPrognosisFactory
);
