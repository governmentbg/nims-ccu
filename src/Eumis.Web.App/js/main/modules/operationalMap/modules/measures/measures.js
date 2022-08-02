import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import measureTemplateUrl from './forms/measure.html';
import { MeasureFactory } from './resources/measure';
import measuresEditTemplateUrl from './views/measuresEdit.html';
import { MeasuresEditCtrl } from './views/measuresEditCtrl';
import measuresNewTemplateUrl from './views/measuresNew.html';
import { MeasuresNewCtrl } from './views/measuresNewCtrl';
import measuresSearchTemplateUrl from './views/measuresSearch.html';
import { MeasuresSearchCtrl } from './views/measuresSearchCtrl';

const OperationalMapMeasuresModule = angular
  .module('main.operationalMap.measures', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisMeasure',
        templateUrl: measureTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map.measures'                                       , '/measures'                                                                                                                                                                                                ])
    .state(['root.map.measures.search'                                , ''                 ,       ['@root'                           , measuresSearchTemplateUrl                                    , MeasuresSearchCtrl                     ]])
    .state(['root.map.measures.new'                                   , '/new?rf'          ,       ['@root'                           , measuresNewTemplateUrl                                       , MeasuresNewCtrl                        ]])
    .state(['root.map.measures.edit'                                  , '/:id?rf'          ,       ['@root'                           , measuresEditTemplateUrl                                      , MeasuresEditCtrl                       ]]);
    }
  ]);

export default OperationalMapMeasuresModule.name;
OperationalMapMeasuresModule.factory('Measure', MeasureFactory);
