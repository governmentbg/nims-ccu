import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import directionFormTemplateUrl from './forms/direction.html';
import subDirectionFormTemplateUrl from './forms/subDirection.html';
import { DirectionFactory } from './resources/direction';
import { SubDirectionFactory } from './resources/subDirection';
import directionsSearchTemplateUrl from './views/directionsSearch.html';
import { DirectionsSearchCtrl } from './views/directionsSearchCtrl';
import directionsViewTemplateUrl from './views/directionsView.html';
import { DirectionsViewCtrl } from './views/directionsViewCtrl';
import directionsEditTemplateUrl from './views/directionsEdit.html';
import { DirectionsEditCtrl } from './views/directionsEditCtrl';
import directionsNewTemplateUrl from './views/directionsNew.html';
import { DirectionsNewCtrl } from './views/directionsNewCtrl';
import subDirectionsSearchTemplateUrl from './views/subDirectionsSearch.html';
import { SubDirectionsSearchCtrl } from './views/subDirectionsSearchCtrl';
import subDirectionsNewTemplateUrl from './views/subDirectionsNew.html';
import { SubDirectionsNewCtrl } from './views/subDirectionsNewCtrl';
import subDirectionsEditTemplateUrl from './views/subDirectionsEdit.html';
import { SubDirectionsEditCtrl } from './views/subDirectionsEditCtrl';

const OperationalMapDirectionsModule = angular
  .module('main.operationalMap.directions', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisDirectionForm',
        templateUrl: directionFormTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisSubDirectionForm',
        templateUrl: subDirectionFormTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map.directions'                                     , '/directions'                                                                                                                                                ])
    .state(['root.map.directions.search'                              , ''                 ,       ['@root'                           , directionsSearchTemplateUrl                 , DirectionsSearchCtrl                           ]])
    .state(['root.map.directions.new'                                 , '/new'             ,       ['@root'                           , directionsNewTemplateUrl                    , DirectionsNewCtrl                              ]])
    
    .state(['root.map.directions.view'                                , '/:id?rf'          , true, ['@root'                           , directionsViewTemplateUrl                   , DirectionsViewCtrl                             ]])
    .state(['root.map.directions.view.edit'                           , ''                 ,       ['@root.map.directions.view'       , directionsEditTemplateUrl                   , DirectionsEditCtrl                             ]])
    
    .state(['root.map.directions.view.subDirections'                  , '/subDirections'                                                                                                                                             ])
    .state(['root.map.directions.view.subDirections.search'           , ''                 ,       ['@root.map.directions.view'       , subDirectionsSearchTemplateUrl              , SubDirectionsSearchCtrl                        ]])
    .state(['root.map.directions.view.subDirections.new'              , '/new'             ,       ['@root.map.directions.view'       , subDirectionsNewTemplateUrl                 , SubDirectionsNewCtrl                           ]])
    .state(['root.map.directions.view.subDirections.edit'             , '/:ind'            ,       ['@root.map.directions.view'       , subDirectionsEditTemplateUrl                , SubDirectionsEditCtrl                          ]])
    }
  ]);

export default OperationalMapDirectionsModule.name;
OperationalMapDirectionsModule.factory('Direction', DirectionFactory);
OperationalMapDirectionsModule.factory('SubDirection', SubDirectionFactory);
