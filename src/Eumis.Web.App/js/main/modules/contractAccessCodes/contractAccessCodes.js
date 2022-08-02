import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractAccessCodeTemplateUrl from './forms/contractAccessCode.html';
import { ContractAccessCodeCtrl } from './forms/contractAccessCodeCtrl';
import { ContractAccessCodeFactory } from './resources/contractAccessCode';
import contractAccessCodesEditTemplateUrl from './views/contractAccessCodesEdit.html';
import { ContractAccessCodesEditCtrl } from './views/contractAccessCodesEditCtrl';
import contractAccessCodesSearchTemplateUrl from './views/contractAccessCodesSearch.html';
import { ContractAccessCodesSearchCtrl } from './views/contractAccessCodesSearchCtrl';

const ContractAccessCodesModule = angular
  .module('main.conrtactAccessCodes', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractAccessCode',
        templateUrl: contractAccessCodeTemplateUrl,
        controller: ContractAccessCodeCtrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractAccessCodes'                                      , '/contractAccessCodes'                                                                                                                                                                                           ])
    .state(['root.contractAccessCodes.search'                               , ''                 ,       ['@root'                           , contractAccessCodesSearchTemplateUrl                                         , ContractAccessCodesSearchCtrl                ]])
    .state(['root.contractAccessCodes.view'                                 , '/:id'             ,       ['@root'                           , contractAccessCodesEditTemplateUrl                                           , ContractAccessCodesEditCtrl                  ]]);
    }
  ]);

export default ContractAccessCodesModule.name;
ContractAccessCodesModule.factory('ContractAccessCode', ContractAccessCodeFactory);
