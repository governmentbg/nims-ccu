import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { ProcedurePrognosisFactory } from './resources/procedurePrognosis';
import procedurePrognosesEditTemplateUrl from './views/procedurePrognosesEdit.html';
import { ProcedurePrognosesEditCtrl } from './views/procedurePrognosesEditCtrl';
import procedurePrognosesNewTemplateUrl from './views/procedurePrognosesNew.html';
import { ProcedurePrognosesNewCtrl } from './views/procedurePrognosesNewCtrl';
import procedurePrognosesSearchTemplateUrl from './views/procedurePrognosesSearch.html';
import { ProcedurePrognosesSearchCtrl } from './views/procedurePrognosesSearchCtrl';

const PrognosesProcedureModule = angular
  .module('main.prognoses.procedure', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.procedurePrognoses'                                              , '/procedurePrognoses?year&month'                                                                                                                                                                                                                            ])
    .state(['root.procedurePrognoses.search'                                       , ''                  ,       ['@root'                                            , procedurePrognosesSearchTemplateUrl                                           , ProcedurePrognosesSearchCtrl                                  ]])
    .state(['root.procedurePrognoses.new'                                          , '/new'              ,       ['@root'                                            , procedurePrognosesNewTemplateUrl                                              , ProcedurePrognosesNewCtrl                                     ]])
    .state(['root.procedurePrognoses.view'                                         , '/:id?rf'           ,       ['@root'                                            , procedurePrognosesEditTemplateUrl                                             , ProcedurePrognosesEditCtrl                                    ]]);
    }
  ]);

export default PrognosesProcedureModule.name;
PrognosesProcedureModule.factory('ProcedurePrognosis', ProcedurePrognosisFactory);
