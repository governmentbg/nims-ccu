import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import contractCommunicationTemplateUrl from './forms/contractCommunication.html';
import ContractCommunicationsAdminAuthorityModule from './modules/adminAuthority/adminAuthority';
import ContractCommunicationsAuditAuthorityModule from './modules/auditAuthority/auditAuthority';
import ContractCommunicationsCertAuthorityModule from './modules/certAuthority/certAuthority';

const ContractCommunicationsModule = angular
  .module('main.contractCommunications', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule,

    //submodules
    ContractCommunicationsAdminAuthorityModule,
    ContractCommunicationsAuditAuthorityModule,
    ContractCommunicationsCertAuthorityModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisContractCommunication',
        templateUrl: contractCommunicationTemplateUrl
      });
    }
  ]);

export default ContractCommunicationsModule.name;
