import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import messageDataTemplateUrl from './forms/messageData.html';
import { MessageDataCtrl } from './forms/messageDataCtrl';
import { MessageFactory } from './resources/message';
import { MessageFileFactory } from './resources/messageFile';
import messagesArchiveTemplateUrl from './views/messagesArchive.html';
import { MessagesArchiveCtrl } from './views/messagesArchiveCtrl';
import messagesDraftTemplateUrl from './views/messagesDraft.html';
import { MessagesDraftCtrl } from './views/messagesDraftCtrl';
import messagesEditTemplateUrl from './views/messagesEdit.html';
import { MessagesEditCtrl } from './views/messagesEditCtrl';
import messagesInboxTemplateUrl from './views/messagesInbox.html';
import { MessagesInboxCtrl } from './views/messagesInboxCtrl';
import messagesIngoingViewTemplateUrl from './views/messagesIngoingView.html';
import { MessagesIngoingViewCtrl } from './views/messagesIngoingViewCtrl';
import messagesNewTemplateUrl from './views/messagesNew.html';
import { MessagesNewCtrl } from './views/messagesNewCtrl';
import messagesSentTemplateUrl from './views/messagesSent.html';
import { MessagesSentCtrl } from './views/messagesSentCtrl';
import messagesSentViewTemplateUrl from './views/messagesSentView.html';
import { MessagesSentViewCtrl } from './views/messagesSentViewCtrl';
import messagesViewTemplateUrl from './views/messagesView.html';
import { MessagesViewCtrl } from './views/messagesViewCtrl';

const MessagesModule = angular
  .module('main.messages', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisMessageData',
        templateUrl: messageDataTemplateUrl,
        controller: MessageDataCtrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.messages'                                           , '/messages?rf'     , true, ['@root'                           , messagesViewTemplateUrl                                                     , MessagesViewCtrl                       ]])
    .state(['root.messages.new'                                       , '/new'             ,       ['@root.messages'                  , messagesNewTemplateUrl                                                      , MessagesNewCtrl                        ]])

    .state(['root.messages.inbox'                                     , '/inbox?pi'        ,       ['@root.messages'                  , messagesInboxTemplateUrl                                                    , MessagesInboxCtrl                      ]])
    .state(['root.messages.inbox.view'                                , '/:id'             ,       ['@root.messages'                  , messagesIngoingViewTemplateUrl                                              , MessagesIngoingViewCtrl                ]])
    .state(['root.messages.draft'                                     , '/draft?pd'        ,       ['@root.messages'                  , messagesDraftTemplateUrl                                                    , MessagesDraftCtrl                      ]])
    .state(['root.messages.draft.edit'                                , '/:id'             ,       ['@root.messages'                  , messagesEditTemplateUrl                                                     , MessagesEditCtrl                       ]])
    .state(['root.messages.sent'                                      , '/sent?ps'         ,       ['@root.messages'                  , messagesSentTemplateUrl                                                     , MessagesSentCtrl                       ]])
    .state(['root.messages.sent.view'                                 , '/:id'             ,       ['@root.messages'                  , messagesSentViewTemplateUrl                                                 , MessagesSentViewCtrl                   ]])
    .state(['root.messages.archive'                                   , '/archive?pa'      ,       ['@root.messages'                  , messagesArchiveTemplateUrl                                                  , MessagesArchiveCtrl                    ]])
    .state(['root.messages.archive.view'                              , '/:id'             ,       ['@root.messages'                  , messagesIngoingViewTemplateUrl                                              , MessagesIngoingViewCtrl                ]]);
    }
  ]);

export default MessagesModule.name;
MessagesModule.factory('Message', MessageFactory);
MessagesModule.factory('MessageFile', MessageFileFactory);
