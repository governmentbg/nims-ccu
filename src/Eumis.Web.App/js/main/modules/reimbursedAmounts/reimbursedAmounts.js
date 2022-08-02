import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import reimbursedAmountDataTemplateUrl from './forms/reimbursedAmountData.html';
import { ReimbursedAmountDataCtrl } from './forms/reimbursedAmountDataCtrl';
import ReimbursedAmountsContractsModule from './modules/contracts/contracts';
import ReimbursedAmountsDebtsModule from './modules/debts/debts';

const ReimbursedAmountsModule = angular
  .module('main.reimbursedAmounts', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule,

    //submodules
    ReimbursedAmountsContractsModule,
    ReimbursedAmountsDebtsModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisReimbursedAmountData',
        templateUrl: reimbursedAmountDataTemplateUrl,
        controller: ReimbursedAmountDataCtrl
      });
    }
  ]);

export default ReimbursedAmountsModule.name;
