import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { AdminAuthorityCommunicationFactory } from './resources/adminAuthorityCommunication';
import contractCommunicationsEditTemplateUrl from './views/contractCommunicationsEdit.html';
import { ContractCommunicationsEditCtrl } from './views/contractCommunicationsEditCtrl';
import contractCommunicationsSearchTemplateUrl from './views/contractCommunicationsSearch.html';
import { ContractCommunicationsSearchCtrl } from './views/contractCommunicationsSearchCtrl';

const ContractCommunicationsAdminAuthorityModule = angular
  .module('main.contractCommunications.adminAuthority', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.contractCommunications'                            , '/contractCommunications?programmeId&programmePriorityId&procedureId&fromDate&toDate&source'                                                                        ])
    .state(['root.contractCommunications.search'                     , ''                  ,       ['@root'                            , contractCommunicationsSearchTemplateUrl    , ContractCommunicationsSearchCtrl         ]])
    .state(['root.contractCommunications.edit'                       , '/:id'              ,       ['@root'                            , contractCommunicationsEditTemplateUrl      , ContractCommunicationsEditCtrl           ]])
    }
  ]);

export default ContractCommunicationsAdminAuthorityModule.name;
ContractCommunicationsAdminAuthorityModule.factory(
  'AdminAuthorityCommunication',
  AdminAuthorityCommunicationFactory
);
