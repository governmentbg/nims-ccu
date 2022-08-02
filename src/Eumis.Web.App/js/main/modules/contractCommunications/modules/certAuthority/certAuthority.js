import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { CertAuthorityCommunicationFactory } from './resources/certAuthorityCommunication';
import certAuthorityCommunicationContractsSearchTemplateUrl from './views/certAuthorityCommunicationContractsSearch.html';
import { CertAuthorityCommunicationContractsSearchCtrl } from './views/certAuthorityCommunicationContractsSearchCtrl';
import certAuthorityCommunicationsContractViewTemplateUrl from './views/certAuthorityCommunicationsContractView.html';
import { CertAuthorityCommunicationsContractViewCtrl } from './views/certAuthorityCommunicationsContractViewCtrl';
import certAuthorityCommunicationsEditTemplateUrl from './views/certAuthorityCommunicationsEdit.html';
import { CertAuthorityCommunicationsEditCtrl } from './views/certAuthorityCommunicationsEditCtrl';
import certAuthorityCommunicationsSearchTemplateUrl from './views/certAuthorityCommunicationsSearch.html';
import { CertAuthorityCommunicationsSearchCtrl } from './views/certAuthorityCommunicationsSearchCtrl';
import certAuthorityCommunicationsViewTemplateUrl from './views/certAuthorityCommunicationsView.html';
import { CertAuthorityCommunicationsViewCtrl } from './views/certAuthorityCommunicationsViewCtrl';

const ContractCommunicationsCertAuthorityModule = angular
  .module('main.contractCommunications.certAuthority', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.certAuthorityCommunications'                           , '/certAuthorityCommunications?programmePriorityId&procedureId'                                                                                                                                                                ])
    .state(['root.certAuthorityCommunications.search'                    , ''                ,       ['@root'                                 , certAuthorityCommunicationContractsSearchTemplateUrl  , CertAuthorityCommunicationContractsSearchCtrl  ]])
    .state(['root.certAuthorityCommunications.view'                      , '/:id?rf'         , true, ['@root'                                 , certAuthorityCommunicationsViewTemplateUrl            , CertAuthorityCommunicationsViewCtrl            ]])
    .state(['root.certAuthorityCommunications.view.contract'             , ''                ,       ['@root.certAuthorityCommunications.view', certAuthorityCommunicationsContractViewTemplateUrl    , CertAuthorityCommunicationsContractViewCtrl    ]])
    .state(['root.certAuthorityCommunications.view.communication'        , '/communication'                                                                                                                                                                                                              ])
    .state(['root.certAuthorityCommunications.view.communication.search' , ''                ,       ['@root.certAuthorityCommunications.view', certAuthorityCommunicationsSearchTemplateUrl          , CertAuthorityCommunicationsSearchCtrl          ]])
    .state(['root.certAuthorityCommunications.view.communication.edit'   , '/:ind'           ,       ['@root.certAuthorityCommunications.view', certAuthorityCommunicationsEditTemplateUrl            , CertAuthorityCommunicationsEditCtrl            ]]);
    }
  ]);

export default ContractCommunicationsCertAuthorityModule.name;
ContractCommunicationsCertAuthorityModule.factory(
  'CertAuthorityCommunication',
  CertAuthorityCommunicationFactory
);
