import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { ProgrammePrognosisFactory } from './resources/programmePrognosis';
import programmePrognosesEditTemplateUrl from './views/programmePrognosesEdit.html';
import { ProgrammePrognosesEditCtrl } from './views/programmePrognosesEditCtrl';
import programmePrognosesNewTemplateUrl from './views/programmePrognosesNew.html';
import { ProgrammePrognosesNewCtrl } from './views/programmePrognosesNewCtrl';
import programmePrognosesSearchTemplateUrl from './views/programmePrognosesSearch.html';
import { ProgrammePrognosesSearchCtrl } from './views/programmePrognosesSearchCtrl';

const PrognosesProgrammeModule = angular
  .module('main.prognoses.programme', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.programmePrognoses'                                              , '/programmePrognoses?year&month'                                                                                                                                                                                                                            ])
    .state(['root.programmePrognoses.search'                                       , ''                  ,       ['@root'                                            , programmePrognosesSearchTemplateUrl                                           , ProgrammePrognosesSearchCtrl                                  ]])
    .state(['root.programmePrognoses.new'                                          , '/new'              ,       ['@root'                                            , programmePrognosesNewTemplateUrl                                              , ProgrammePrognosesNewCtrl                                     ]])
    .state(['root.programmePrognoses.view'                                         , '/:id?rf'           ,       ['@root'                                            , programmePrognosesEditTemplateUrl                                             , ProgrammePrognosesEditCtrl                                    ]]);
    }
  ]);

export default PrognosesProgrammeModule.name;
PrognosesProgrammeModule.factory('ProgrammePrognosis', ProgrammePrognosisFactory);
