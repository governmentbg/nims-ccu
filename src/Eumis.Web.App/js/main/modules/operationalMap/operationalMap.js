import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import documentTemplateUrl from './forms/document.html';
import { DocumentCtrl } from './forms/documentCtrl';
import indicatorDataTemplateUrl from './forms/indicatorData.html';
import { IndicatorDataCtrl } from './forms/indicatorDataCtrl';
import indicatorReportedAmountDataTemplateUrl from './forms/indicatorReportedAmountData.html';
import { IndicatorReportedAmountDataCtrl } from './forms/indicatorReportedAmountDataCtrl';
import operationalMapBudgetTemplateUrl from './forms/operationalMapBudget.html';
import { OperationalMapBudgetCtrl } from './forms/operationalMapBudgetCtrl';
import operationalMapBudgetMiniTemplateUrl from './forms/operationalMapBudgetMini.html';
import operationalMapBudgetNextThreeTemplateUrl from './forms/operationalMapBudgetNextThree.html';
import procedureIndicatorTemplateUrl from './forms/procedureIndicator.html';
import OperationalMapAllowancesModule from './modules/allowances/allowances';
import OperationalMapBasicInterestRatesModule from './modules/basicInterestRates/basicInterestRates';
import OperationalMapCheckBlankTopicsModule from './modules/checkBlankTopics/checkBlankTopics';
import OperationalMapExpenseTypesModule from './modules/expenseTypes/expenseTypes';
import OperationalMapIndicatorsModule from './modules/indicators/indicators';
import OperationalMapInterestSchemesModule from './modules/interestSchemes/interestSchemes';
import OperationalMapMeasuresModule from './modules/measures/measures';
import OperationalMapNodesModule from './modules/nodes/nodes';
import OperationalDeclarationsModule from './modules/declarations/declarations';
import operationalMapTemplateUrl from './views/operationalMap.html';
import { OperationalMapCtrl } from './views/operationalMapCtrl';
import OperationalMapDirectionsModule from './modules/directions/direction';

const OperationalMapModule = angular
  .module('main.operationalMap', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule,

    //submodules
    OperationalMapAllowancesModule,
    OperationalMapBasicInterestRatesModule,
    OperationalMapCheckBlankTopicsModule,
    OperationalMapExpenseTypesModule,
    OperationalMapIndicatorsModule,
    OperationalMapInterestSchemesModule,
    OperationalMapNodesModule,
    OperationalMapMeasuresModule,
    OperationalDeclarationsModule,
    OperationalMapDirectionsModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisOperationalMapBudget',
        templateUrl: operationalMapBudgetTemplateUrl,
        controller: OperationalMapBudgetCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisIndicatorData',
        templateUrl: indicatorDataTemplateUrl,
        controller: IndicatorDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisIndicatorReportedAmountData',
        templateUrl: indicatorReportedAmountDataTemplateUrl,
        controller: IndicatorReportedAmountDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureIndicator',
        templateUrl: procedureIndicatorTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisDocument',
        templateUrl: documentTemplateUrl,
        controller: DocumentCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisOperationalMapBudgetMini',
        templateUrl: operationalMapBudgetMiniTemplateUrl,
        controller: OperationalMapBudgetCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisOperationalMapBudgetNextThree',
        templateUrl: operationalMapBudgetNextThreeTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map'                                                , '/map'                                                                                                                                                                         ])
    .state(['root.map.tree'                                           , ''                 ,       ['@root'                           , operationalMapTemplateUrl                                             , OperationalMapCtrl                     ]]);
    }
  ]);

export default OperationalMapModule.name;
