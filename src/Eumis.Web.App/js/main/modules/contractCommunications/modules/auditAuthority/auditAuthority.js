import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { AuditAuthorityCommunicationFactory } from './resources/auditAuthorityCommunication';
import auditAuthorityCommunicationContractsSearchTemplateUrl from './views/auditAuthorityCommunicationContractsSearch.html';
import { AuditAuthorityCommunicationContractsSearchCtrl } from './views/auditAuthorityCommunicationContractsSearchCtrl';
import auditAuthorityCommunicationsContractViewTemplateUrl from './views/auditAuthorityCommunicationsContractView.html';
import { AuditAuthorityCommunicationsContractViewCtrl } from './views/auditAuthorityCommunicationsContractViewCtrl';
import auditAuthorityCommunicationsEditTemplateUrl from './views/auditAuthorityCommunicationsEdit.html';
import { AuditAuthorityCommunicationsEditCtrl } from './views/auditAuthorityCommunicationsEditCtrl';
import auditAuthorityCommunicationsSearchTemplateUrl from './views/auditAuthorityCommunicationsSearch.html';
import { AuditAuthorityCommunicationsSearchCtrl } from './views/auditAuthorityCommunicationsSearchCtrl';
import auditAuthorityCommunicationsViewTemplateUrl from './views/auditAuthorityCommunicationsView.html';
import { AuditAuthorityCommunicationsViewCtrl } from './views/auditAuthorityCommunicationsViewCtrl';

const ContractCommunicationsAuditAuthorityModule = angular
  .module('main.contractCommunications.auditAuthority', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.auditAuthorityCommunications'                          , '/auditAuthorityCommunications?programmePriorityId&procedureId'                                                                                                                                                               ])
    .state(['root.auditAuthorityCommunications.search'                   , ''                ,       ['@root'                                  , auditAuthorityCommunicationContractsSearchTemplateUrl, AuditAuthorityCommunicationContractsSearchCtrl]])
    .state(['root.auditAuthorityCommunications.view'                     , '/:id?rf'         , true, ['@root'                                  , auditAuthorityCommunicationsViewTemplateUrl          , AuditAuthorityCommunicationsViewCtrl          ]])
    .state(['root.auditAuthorityCommunications.view.contract'            , ''                ,       ['@root.auditAuthorityCommunications.view', auditAuthorityCommunicationsContractViewTemplateUrl  , AuditAuthorityCommunicationsContractViewCtrl  ]])
    .state(['root.auditAuthorityCommunications.view.communication'       , '/communication'                                                                                                                                                                                                              ])
    .state(['root.auditAuthorityCommunications.view.communication.search', ''                ,       ['@root.auditAuthorityCommunications.view', auditAuthorityCommunicationsSearchTemplateUrl        , AuditAuthorityCommunicationsSearchCtrl        ]])
    .state(['root.auditAuthorityCommunications.view.communication.edit'  , '/:ind'           ,       ['@root.auditAuthorityCommunications.view', auditAuthorityCommunicationsEditTemplateUrl          , AuditAuthorityCommunicationsEditCtrl          ]]);
    }
  ]);

export default ContractCommunicationsAuditAuthorityModule.name;
ContractCommunicationsAuditAuthorityModule.factory(
  'AuditAuthorityCommunication',
  AuditAuthorityCommunicationFactory
);
