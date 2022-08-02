import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { UserNotificationFactory } from './resources/userNotification';
import userNotificationsSearchTemplateUrl from './views/userNotificationsSearch.html';
import { UserNotificationsSearchCtrl } from './views/userNotificationsSearchCtrl';
import viewUserNotificationTemplateUrl from './modals/viewNotificationModal.html';
import { ViewNotificationModalCtrl } from './modals/viewNotificationModalCtrl';

const UserNotificationModule = angular
  .module('main.notifications.userNotifications', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
      .modal('viewNotificationModal'                                   , viewUserNotificationTemplateUrl                              , ViewNotificationModalCtrl                                         , 'xlg')
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.userNotifications'                                  , '/userNotifications?notificationEventId&isRead'                                                                                                                                             ])
    .state(['root.userNotifications.search'                           , ''                  ,       ['@root'                          , userNotificationsSearchTemplateUrl                                , UserNotificationsSearchCtrl                            ]])
    }
  ]);

export default UserNotificationModule.name;
UserNotificationModule.factory('UserNotification', UserNotificationFactory);
