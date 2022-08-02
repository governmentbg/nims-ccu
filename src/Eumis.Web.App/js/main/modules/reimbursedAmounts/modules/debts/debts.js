import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import debtReimbursedAmountBasicDataTemplateUrl from './forms/debtReimbursedAmountBasicData.html';
import { DebtReimbursedAmountFactory } from './resources/debtReimbursedAmount';
import debtReimbursedAmountsBasicViewTemplateUrl from './views/debtReimbursedAmountsBasicView.html';
import { DebtReimbursedAmountsBasicViewCtrl } from './views/debtReimbursedAmountsBasicViewCtrl';
import debtReimbursedAmountsEditTemplateUrl from './views/debtReimbursedAmountsEdit.html';
import { DebtReimbursedAmountsEditCtrl } from './views/debtReimbursedAmountsEditCtrl';
import debtReimbursedAmountsNewTemplateUrl from './views/debtReimbursedAmountsNew.html';
import { DebtReimbursedAmountsNewCtrl } from './views/debtReimbursedAmountsNewCtrl';
import debtReimbursedAmountsSearchTemplateUrl from './views/debtReimbursedAmountsSearch.html';
import { DebtReimbursedAmountsSearchCtrl } from './views/debtReimbursedAmountsSearchCtrl';
import debtReimbursedAmountsViewTemplateUrl from './views/debtReimbursedAmountsView.html';
import { DebtReimbursedAmountsViewCtrl } from './views/debtReimbursedAmountsViewCtrl';

const ReimbursedAmountsDebtsModule = angular
  .module('main.reimbursedAmounts.debts', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisDebtReimbursedAmountBasicData',
        templateUrl: debtReimbursedAmountBasicDataTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.debtReimbursedAmounts'                              , '/debtReimbursedAmounts?contractId&type'                                                                                                                                                                            ])
    .state(['root.debtReimbursedAmounts.search'                       , ''                 ,       ['@root'                           , debtReimbursedAmountsSearchTemplateUrl                       , DebtReimbursedAmountsSearchCtrl                 ]])
    .state(['root.debtReimbursedAmounts.new'                          , '/new'             ,       ['@root'                           , debtReimbursedAmountsNewTemplateUrl                          , DebtReimbursedAmountsNewCtrl                    ]])
    .state(['root.debtReimbursedAmounts.view'                         , '/:id?rf'          , true, ['@root'                           , debtReimbursedAmountsViewTemplateUrl                         , DebtReimbursedAmountsViewCtrl                   ]])
    .state(['root.debtReimbursedAmounts.view.basicData'               , ''                 ,       ['@root.debtReimbursedAmounts.view', debtReimbursedAmountsBasicViewTemplateUrl                    , DebtReimbursedAmountsBasicViewCtrl              ]])
    .state(['root.debtReimbursedAmounts.view.amount'                  , '/amount'          ,       ['@root.debtReimbursedAmounts.view', debtReimbursedAmountsEditTemplateUrl                         , DebtReimbursedAmountsEditCtrl                   ]]);
    }
  ]);

export default ReimbursedAmountsDebtsModule.name;
ReimbursedAmountsDebtsModule.factory('DebtReimbursedAmount', DebtReimbursedAmountFactory);
