import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import serviceContractsNewStep1TemplateUrl from './views/serviceContractsNewStep1.html';
import { ServiceContractsNewStep1Ctrl } from './views/serviceContractsNewStep1Ctrl';
import serviceContractsNewStep2TemplateUrl from './views/serviceContractsNewStep2.html';
import { ServiceContractsNewStep2Ctrl } from './views/serviceContractsNewStep2Ctrl';
import serviceContractsNewStep3TemplateUrl from './views/serviceContractsNewStep3.html';
import { ServiceContractsNewStep3Ctrl } from './views/serviceContractsNewStep3Ctrl';
import { ServiceContractFactory } from './resources/serviceContract';
import serviceContractRegistrationTemplateUrl from './forms/serviceContractRegistration.html';
const ServiceContractsModule = angular
  .module('main.contracts.serviceContracts', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisServiceContractData',
        templateUrl: serviceContractRegistrationTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
      .state(['root.contracts.serviceContracts'                    , '/contracts?programmePriorityId&procedureId'                                                                                                                                                                            ])
      .state(['root.contracts.serviceContracts.newStep1'           , '/newStep1'                              ,       ['@root'                           , serviceContractsNewStep1TemplateUrl                                            , ServiceContractsNewStep1Ctrl                     ]])
      .state(['root.contracts.serviceContracts.newStep2'           , '/newStep2?pId&uinType&uin'              ,       ['@root'                           , serviceContractsNewStep2TemplateUrl                                            , ServiceContractsNewStep2Ctrl                     ]])
      .state(['root.contracts.serviceContracts.newStep3'           , '/newStep3?pId&cId&xmlId'                ,       ['@root'                           , serviceContractsNewStep3TemplateUrl                                            , ServiceContractsNewStep3Ctrl                     ]])
    }
  ]);

ServiceContractFactory;
export default ServiceContractsModule.name;
ServiceContractsModule.factory('ServiceContract', ServiceContractFactory);
