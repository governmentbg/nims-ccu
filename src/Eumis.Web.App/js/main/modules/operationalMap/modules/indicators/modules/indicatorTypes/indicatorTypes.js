import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import indicatorTypeDataTemplateUrl from './forms/indicatorTypeData.html';
import { IndicatorTypeFactory } from './resources/indicatorType';
import indicatorTypesSearchTemplateUrl from './views/indicatorTypesSearch.html';
import { IndicatorTypesSearchCtrl } from './views/indicatorTypesSearchCtrl';
import indicatorTypesNewTemplateUrl from './views/indicatorTypesNew.html';
import { IndicatorTypesNewCtrl } from './views/indicatorTypesNewCtrl';
import indicatorTypesEditTemplateUrl from './views/indicatorTypesEdit.html';
import { IndicatorTypesEditCtrl } from './views/indicatorTypesEditCtrl';

const OperationalMapIndicatorTypesModule = angular
  .module('main.operationalMap.indicatorTypes', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisIndicatorType',
        templateUrl: indicatorTypeDataTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map.indicatorTypes'                                     , '/indicatorTypes'                                                                                                                                                   ])
    .state(['root.map.indicatorTypes.search'                              , ''                 ,       ['@root'                           , indicatorTypesSearchTemplateUrl                            , IndicatorTypesSearchCtrl               ]])
    .state(['root.map.indicatorTypes.new'                                 , '/new'             ,       ['@root'                           , indicatorTypesNewTemplateUrl                               , IndicatorTypesNewCtrl                  ]])
    .state(['root.map.indicatorTypes.edit'                                , '/:id?rf'          ,       ['@root'                           , indicatorTypesEditTemplateUrl                              , IndicatorTypesEditCtrl                 ]])
    }
  ]);

export default OperationalMapIndicatorTypesModule.name;
OperationalMapIndicatorTypesModule.factory('IndicatorType', IndicatorTypeFactory);
