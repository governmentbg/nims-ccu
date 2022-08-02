import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractRegistrationTemplateUrl from './forms/contractRegistration.html';
import { ContractRegistrationFactory } from './resources/contractRegistration';
import contractRegistrationsEditTemplateUrl from './views/contractRegistrationsEdit.html';
import { ContractRegistrationsEditCtrl } from './views/contractRegistrationsEditCtrl';
import contractRegistrationsSearchTemplateUrl from './views/contractRegistrationsSearch.html';
import { ContractRegistrationsSearchCtrl } from './views/contractRegistrationsSearchCtrl';

const ContractRegistrationsModule = angular
  .module('main.contractRegistrations', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractRegistration',
        templateUrl: contractRegistrationTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractRegistrations'                             , '/contractRegistrations'                                                                                                                                                                                    ])
    .state(['root.contractRegistrations.search'                      , ''                  ,       ['@root'                           , contractRegistrationsSearchTemplateUrl                         , ContractRegistrationsSearchCtrl        ]])
    .state(['root.contractRegistrations.view'                        , '/:id'              ,       ['@root'                           , contractRegistrationsEditTemplateUrl                           , ContractRegistrationsEditCtrl          ]]);
    }
  ]);

export default ContractRegistrationsModule.name;
ContractRegistrationsModule.factory('ContractRegistration', ContractRegistrationFactory);
