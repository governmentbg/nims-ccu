import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import OperationalMapIndicatorTypesModule from './modules/indicatorTypes/indicatorTypes';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { IndicatorFactory } from './resources/indicator';
import indicatorsEditTemplateUrl from './views/indicatorsEdit.html';
import { IndicatorsEditCtrl } from './views/indicatorsEditCtrl';
import indicatorsSearchTemplateUrl from './views/indicatorsSearch.html';
import { IndicatorsSearchCtrl } from './views/indicatorsSearchCtrl';

const OperationalMapIndicatorsModule = angular
  .module('main.operationalMap.indicators', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule,

    // Submodules
    OperationalMapIndicatorTypesModule
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map.indicators'                                     , '/indicators'                                                                                                                                                                                              ])
    .state(['root.map.indicators.search'                              , ''                 ,       ['@root'                           , indicatorsSearchTemplateUrl                                , IndicatorsSearchCtrl                   ]])
    .state(['root.map.indicators.edit'                                , '/:id?rf'          ,       ['@root'                           , indicatorsEditTemplateUrl                                  , IndicatorsEditCtrl                     ]]);
    }
  ]);

export default OperationalMapIndicatorsModule.name;
OperationalMapIndicatorsModule.factory('Indicator', IndicatorFactory);
