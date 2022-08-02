import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
//import spotCheckPlaceTemplateUrl from './forms/spotCheckPlace.html';
//import { SpotCheckPlaceCtrl } from './forms/spotCheckPlaceCtrl';
//import spotCheckTargetTemplateUrl from './forms/spotCheckTarget.html';
import userNotificationModule from './modules/userNotifications/userNotification';
import notificationSettingModule from './modules/notificationSettings/notificationSettings';

const NotificationsModule = angular.module('main.notifications', [
  UiRouterModule,
  UiBootstrapModule,
  ScaffoldingModule,

  //submodules
  userNotificationModule,
  notificationSettingModule
]);

export default NotificationsModule.name;
