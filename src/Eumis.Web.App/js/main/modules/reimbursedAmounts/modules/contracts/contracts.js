import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractReimbursedAmountBasicDataTemplateUrl from './forms/contractReimbursedAmountBasicData.html';
import chooseReimbursedAmountContractModalTemplateUrl from './modals/chooseReimbursedAmountContractModal.html';
import { ChooseReimbursedAmountContractModalCtrl } from './modals/chooseReimbursedAmountContractModalCtrl';
import chooseReimbursedAmountDebtModalTemplateUrl from './modals/chooseReimbursedAmountDebtModal.html';
import { ChooseReimbursedAmountDebtModalCtrl } from './modals/chooseReimbursedAmountDebtModalCtrl';
import { ContractReimbursedAmountFactory } from './resources/contractReimbursedAmount';
import contractReimbursedAmountsBasicViewTemplateUrl from './views/contractReimbursedAmountsBasicView.html';
import { ContractReimbursedAmountsBasicViewCtrl } from './views/contractReimbursedAmountsBasicViewCtrl';
import contractReimbursedAmountsEditTemplateUrl from './views/contractReimbursedAmountsEdit.html';
import { ContractReimbursedAmountsEditCtrl } from './views/contractReimbursedAmountsEditCtrl';
import contractReimbursedAmountsNewStep1TemplateUrl from './views/contractReimbursedAmountsNewStep1.html';
import { ContractReimbursedAmountsNewStep1Ctrl } from './views/contractReimbursedAmountsNewStep1Ctrl';
import contractReimbursedAmountsNewStep2TemplateUrl from './views/contractReimbursedAmountsNewStep2.html';
import { ContractReimbursedAmountsNewStep2Ctrl } from './views/contractReimbursedAmountsNewStep2Ctrl';
import contractReimbursedAmountsSearchTemplateUrl from './views/contractReimbursedAmountsSearch.html';
import { ContractReimbursedAmountsSearchCtrl } from './views/contractReimbursedAmountsSearchCtrl';
import contractReimbursedAmountsViewTemplateUrl from './views/contractReimbursedAmountsView.html';
import { ContractReimbursedAmountsViewCtrl } from './views/contractReimbursedAmountsViewCtrl';

const ReimbursedAmountsContractsModule = angular
  .module('main.reimbursedAmounts.contracts', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractReimbursedAmountBasicData',
        templateUrl: contractReimbursedAmountBasicDataTemplateUrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('chooseReimbursedAmountsContractModal'   , chooseReimbursedAmountContractModalTemplateUrl, ChooseReimbursedAmountContractModalCtrl   , 'xlg')
    .modal('chooseReimbursedAmountsDebtModal'       , chooseReimbursedAmountDebtModalTemplateUrl    , ChooseReimbursedAmountDebtModalCtrl       , 'sm');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractReimbursedAmounts'                          , '/contractReimbursedAmounts?contractId&type'                                                                                                                                                                        ])
    .state(['root.contractReimbursedAmounts.search'                   , ''                 ,       ['@root'                               , contractReimbursedAmountsSearchTemplateUrl           , ContractReimbursedAmountsSearchCtrl             ]])
    .state(['root.contractReimbursedAmounts.newStep1'                 , '/newStep1'        ,       ['@root'                               , contractReimbursedAmountsNewStep1TemplateUrl         , ContractReimbursedAmountsNewStep1Ctrl           ]])
    .state(['root.contractReimbursedAmounts.newStep2'                 , '/newStep2?cNum'   ,       ['@root'                               , contractReimbursedAmountsNewStep2TemplateUrl         , ContractReimbursedAmountsNewStep2Ctrl           ]])
    .state(['root.contractReimbursedAmounts.view'                     , '/:id?rf'          , true, ['@root'                               , contractReimbursedAmountsViewTemplateUrl             , ContractReimbursedAmountsViewCtrl               ]])
    .state(['root.contractReimbursedAmounts.view.basicData'           , ''                 ,       ['@root.contractReimbursedAmounts.view', contractReimbursedAmountsBasicViewTemplateUrl        , ContractReimbursedAmountsBasicViewCtrl          ]])
    .state(['root.contractReimbursedAmounts.view.amount'              , '/amount'          ,       ['@root.contractReimbursedAmounts.view', contractReimbursedAmountsEditTemplateUrl             , ContractReimbursedAmountsEditCtrl               ]]);
    }
  ]);

export default ReimbursedAmountsContractsModule.name;
ReimbursedAmountsContractsModule.factory(
  'ContractReimbursedAmount',
  ContractReimbursedAmountFactory
);
