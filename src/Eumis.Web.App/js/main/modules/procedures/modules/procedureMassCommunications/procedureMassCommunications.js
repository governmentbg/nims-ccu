import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import procedureMassCommunicationTemplateUrl from './forms/procedureMassCommunication.html';

import chooseRecipientsModalTemplateUrl from './modals/chooseRecipientsModal.html';
import { ChooseRecipientsModalCtrl } from './modals/chooseRecipientsModalCtrl';
import { ProcedureMassCommunicationFactory } from './resources/procedureMassCommunication';
import { ProcedureMassCommunicationDocumentFactory } from './resources/procedureMassCommunicationDocument';
import { ProcedureMassCommunicationFileFactory } from './resources/procedureMassCommunicationFile';
import { ProcedureMassCommunicationRecipientFactory } from './resources/procedureMassCommunicationRecipient';
import procedureMassCommunicationsSearchTemplateUrl from './views/procedureMassCommunicationsSearch.html';
import { ProcedureMassCommunicationsSearchCtrl } from './views/procedureMassCommunicationsSearchCtrl';
import procedureMassCommunicationsNewTemplateUrl from './views/procedureMassCommunicationsNew.html';
import { ProcedureMassCommunicationsNewCtrl } from './views/procedureMassCommunicationsNewCtrl';
import procedureMassCommunicationsViewTemplateUrl from './views/procedureMassCommunicationsView.html';
import { ProcedureMassCommunicationsViewCtrl } from './views/procedureMassCommunicationsViewCtrl';
import procedureMassCommunicationsEditTemplateUrl from './views/procedureMassCommunicationsEdit.html';
import { ProcedureMassCommunicationsEditCtrl } from './views/procedureMassCommunicationsEditCtrl';
import procedureMassCommunicationDocumentsSearchTemplateUrl from './views/procedureMassCommunicationDocumentsSearch.html';
import { ProcedureMassCommunicationDocumentsSearchCtrl } from './views/procedureMassCommunicationDocumentsSearchCtrl';
import procedureMassCommunicationDocumentsNewTemplateUrl from './views/procedureMassCommunicationDocumentsNew.html';
import { ProcedureMassCommunicationDocumentsNewCtrl } from './views/procedureMassCommunicationDocumentsNewCtrl';
import procedureMassCommunicationDocumentsEditTemplateUrl from './views/procedureMassCommunicationDocumentsEdit.html';
import { ProcedureMassCommunicationDocumentsEditCtrl } from './views/procedureMassCommunicationDocumentsEditCtrl';
import procedureMassCommunicationRecipientsSearchTemplateUrl from './views/procedureMassCommunicationRecipientsSearch.html';
import { ProcedureMassCommunicationRecipientsSearchCtrl } from './views/procedureMassCommunicationRecipientsSearchCtrl';

const ProcedureMassCommunicationsModule = angular
  .module('main.procedures.massCommunication', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisMassCommunication',
        templateUrl: procedureMassCommunicationTemplateUrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
      .modal('chooseRecipientsModal'                                  , chooseRecipientsModalTemplateUrl                                  , ChooseRecipientsModalCtrl                                 , 'xlg')
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.procedureMassCommunications'                                             , '/procedureMassCommunications'                                                                                                                                                                                                         ])
    .state(['root.procedureMassCommunications.search'                                      , ''                              ,       ['@root'                                   , procedureMassCommunicationsSearchTemplateUrl                                        , ProcedureMassCommunicationsSearchCtrl                       ]])
    .state(['root.procedureMassCommunications.new'                                         , '/new'                          ,       ['@root'                                   , procedureMassCommunicationsNewTemplateUrl                                           , ProcedureMassCommunicationsNewCtrl                          ]])
    .state(['root.procedureMassCommunications.view'                                        , '/:id?rf'                       , true, ['@root'                                   , procedureMassCommunicationsViewTemplateUrl                                          , ProcedureMassCommunicationsViewCtrl                         ]])
    .state(['root.procedureMassCommunications.view.edit'                                   , ''                              ,       ['@root.procedureMassCommunications.view'  , procedureMassCommunicationsEditTemplateUrl                                          , ProcedureMassCommunicationsEditCtrl                         ]]) 
    .state(['root.procedureMassCommunications.view.documents'                              , '/documents'                                                                                                                                                                                                                           ])
    .state(['root.procedureMassCommunications.view.documents.search'                       , ''                              ,       ['@root.procedureMassCommunications.view'  , procedureMassCommunicationDocumentsSearchTemplateUrl                                , ProcedureMassCommunicationDocumentsSearchCtrl               ]])
    .state(['root.procedureMassCommunications.view.documents.new'                          , '/new'                          ,       ['@root.procedureMassCommunications.view'  , procedureMassCommunicationDocumentsNewTemplateUrl                                   , ProcedureMassCommunicationDocumentsNewCtrl                  ]])
    .state(['root.procedureMassCommunications.view.documents.edit'                         ,'/:ind'                          ,       ['@root.procedureMassCommunications.view'  , procedureMassCommunicationDocumentsEditTemplateUrl                                  , ProcedureMassCommunicationDocumentsEditCtrl                 ]])
    .state(['root.procedureMassCommunications.view.recipients'                             , '/recipients'          ,                                                                                                                                                                                                               ])
    .state(['root.procedureMassCommunications.view.recipients.search'                      , ''                              ,       ['@root.procedureMassCommunications.view'  , procedureMassCommunicationRecipientsSearchTemplateUrl                                , ProcedureMassCommunicationRecipientsSearchCtrl             ]])
    }
  ]);

export default ProcedureMassCommunicationsModule.name;
ProcedureMassCommunicationsModule.factory(
  'ProcedureMassCommunication',
  ProcedureMassCommunicationFactory
);
ProcedureMassCommunicationsModule.factory(
  'ProcedureMassCommunicationDocument',
  ProcedureMassCommunicationDocumentFactory
);
ProcedureMassCommunicationsModule.factory(
  'ProcedureMassCommunicationFile',
  ProcedureMassCommunicationFileFactory
);
ProcedureMassCommunicationsModule.factory(
  'ProcedureMassCommunicationRecipient',
  ProcedureMassCommunicationRecipientFactory
);
