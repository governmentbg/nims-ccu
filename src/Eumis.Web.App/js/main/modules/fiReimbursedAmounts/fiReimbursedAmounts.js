import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import fiReimbursedAmountBasicDataTemplateUrl from './forms/fiReimbursedAmountBasicData.html';
import fiReimbursedAmountDataTemplateUrl from './forms/fiReimbursedAmountData.html';
import { FIReimbursedAmountDataCtrl } from './forms/fiReimbursedAmountDataCtrl';
import { FIReimbursedAmountFactory } from './resources/fiReimbursedAmount';
import fiReimbursedAmountsBasicViewTemplateUrl from './views/fiReimbursedAmountsBasicView.html';
import { FIReimbursedAmountsBasicViewCtrl } from './views/fiReimbursedAmountsBasicViewCtrl';
import fiReimbursedAmountsEditTemplateUrl from './views/fiReimbursedAmountsEdit.html';
import { FIReimbursedAmountsEditCtrl } from './views/fiReimbursedAmountsEditCtrl';
import fiReimbursedAmountsNewStep1TemplateUrl from './views/fiReimbursedAmountsNewStep1.html';
import { FIReimbursedAmountsNewStep1Ctrl } from './views/fiReimbursedAmountsNewStep1Ctrl';
import fiReimbursedAmountsNewStep2TemplateUrl from './views/fiReimbursedAmountsNewStep2.html';
import { FIReimbursedAmountsNewStep2Ctrl } from './views/fiReimbursedAmountsNewStep2Ctrl';
import fiReimbursedAmountsSearchTemplateUrl from './views/fiReimbursedAmountsSearch.html';
import { FIReimbursedAmountsSearchCtrl } from './views/fiReimbursedAmountsSearchCtrl';
import fiReimbursedAmountsViewTemplateUrl from './views/fiReimbursedAmountsView.html';
import { FIReimbursedAmountsViewCtrl } from './views/fiReimbursedAmountsViewCtrl';

const FiReimbursedAmountsModule = angular
  .module('main.fiReimbursedAmounts', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisFinancialInstrumentReimbursedAmountBasicData',
        templateUrl: fiReimbursedAmountBasicDataTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisFinancialInstrumentReimbursedAmountData',
        templateUrl: fiReimbursedAmountDataTemplateUrl,
        controller: FIReimbursedAmountDataCtrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.fiReimbursedAmounts'                                , '/fiReimbursedAmounts?contractId&type'                                                                                                                                                                              ])
    .state(['root.fiReimbursedAmounts.search'                         , ''                 ,       ['@root'                          , fiReimbursedAmountsSearchTemplateUrl                              , FIReimbursedAmountsSearchCtrl                   ]])
    .state(['root.fiReimbursedAmounts.newStep1'                       , '/newStep1'        ,       ['@root'                          , fiReimbursedAmountsNewStep1TemplateUrl                            , FIReimbursedAmountsNewStep1Ctrl                 ]])
    .state(['root.fiReimbursedAmounts.newStep2'                       , '/newStep2?cNum'   ,       ['@root'                          , fiReimbursedAmountsNewStep2TemplateUrl                            , FIReimbursedAmountsNewStep2Ctrl                 ]])
    .state(['root.fiReimbursedAmounts.view'                           , '/:id?rf'          , true, ['@root'                          , fiReimbursedAmountsViewTemplateUrl                                , FIReimbursedAmountsViewCtrl                     ]])
    .state(['root.fiReimbursedAmounts.view.basicData'                 , ''                 ,       ['@root.fiReimbursedAmounts.view' , fiReimbursedAmountsBasicViewTemplateUrl                           , FIReimbursedAmountsBasicViewCtrl                ]])
    .state(['root.fiReimbursedAmounts.view.amount'                    , '/amount'          ,       ['@root.fiReimbursedAmounts.view' , fiReimbursedAmountsEditTemplateUrl                                , FIReimbursedAmountsEditCtrl                     ]]);
    }
  ]);

export default FiReimbursedAmountsModule.name;
FiReimbursedAmountsModule.factory('FIReimbursedAmount', FIReimbursedAmountFactory);
