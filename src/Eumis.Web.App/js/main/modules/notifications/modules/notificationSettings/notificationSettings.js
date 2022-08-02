import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import notificationSettingFormTeplateUrl from './forms/notificationSetting.html';
import { NotificationSettingFactory } from './resources/notifcationSetting';
import { NotificationSettingAttachedContractFactory } from './resources/notifcationSettingAttachedContract';
import { NotificationSettingAttachedProcedureFactory } from './resources/notifcationSettingAttachedProcedure';
import { NotificationSettingAttachedProgrammePriorityFactory } from './resources/notifcationSettingAttachedProgrammePriority';
import { NotificationSettingAttachedProgrammeFactory } from './resources/notifcationSettingAttachedProgramme';
import notificationSettingsSearchTemplateUrl from './views/notificationSettingsSearch.html';
import { NotificationSettingsSearchCtrl } from './views/notificationSettingsSearchCtrl';
import notificationSettingsNewTemplateUrl from './views/notificationSettingsNew.html';
import { NotificationSettingsNewCtrl } from './views/notificationSettingsNewCtrl';
import notificationSettingsViewTemplateUrl from './views/notificationSettingsView.html';
import { NotificationSettingsViewCtrl } from './views/notificationSettingsViewCtrl';
import notificationSettingsEditTemplateUrl from './views/notificationSettingsEdit.html';
import { NotificationSettingsEditCtrl } from './views/notificationSettingsEditCtrl';
import notificationSettingAttachedContractsTepmplateUrl from './views/notificationSettingAttachedContracts.html';
import { NotificationSettingAttachedContractsCtrl } from './views/notificationSettingAttachedContractsCtrl';
import chooseNSContractsModalTemplateUrl from './modals/chooseNSContractsModal.html';
import { ChooseNSContractsModalCtrl } from './modals/chooseNSContractsModalCtrl';
import notificationSettingAttachedProceduresTepmplateUrl from './views/notificationSettingAttachedProcedures.html';
import { NotificationSettingAttachedProceduresCtrl } from './views/notificationSettingAttachedProceduresCtrl';
import chooseNSProceduresModalTemplateUrl from './modals/chooseNSProceduresModal.html';
import { ChooseNSProceduresModalCtrl } from './modals/chooseNSProceduresModalCtrl';
import notificationSettingAttachedpPriorityTemplateUrl from './views/notificationSettingAttachedProgrammePriorities.html';
import { NotificationSettingAttachedProgrammePrioritiesCtrl } from './views/notificationSettingAttachedProgrammePrioritiesCtrl';
import chooseNSPPrioritiesModalTemplateUrl from './modals/chooseNSPPrioritiesModal.html';
import { ChooseNSPPrioritiesModalCtrl } from './modals/chooseNSPPrioritiesModalCtrl';
import notificationSettingAttachedProgrammesTemplateUrl from './views/notificationSettingAttachedProgrammes.html';
import { NotificationSettingAttachedProgrammesCtrl } from './views/notificationSettingAttachedProgrammesCtrl';
import chooseNSProgrammesModalTemplateUrl from './modals/chooseNSProgrammesModal.html';
import { ChooseNSProgrammesModalCtrl } from './modals/chooseNSProgrammesModalCtrl';
import chooseUserModalTemplateUrl from './modals/chooseUserModal.html';
import { ChooseUserModalCtrl } from './modals/chooseUserModalCtrl';

const NotificationSettingModule = angular
  .module('main.notifications.userSettings', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisNotificationSetting',
        templateUrl: notificationSettingFormTeplateUrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
      .modal('chooseNSContractsModal'                                   , chooseNSContractsModalTemplateUrl                                   , ChooseNSContractsModalCtrl                                 , 'xlg')
      .modal('chooseNSProceduresModal'                                  , chooseNSProceduresModalTemplateUrl                                  , ChooseNSProceduresModalCtrl                                , 'xlg')
      .modal('chooseNSPPrioritiesModal'                                 , chooseNSPPrioritiesModalTemplateUrl                                 , ChooseNSPPrioritiesModalCtrl                               , 'xlg')
      .modal('chooseNSProgrammesModal'                                  , chooseNSProgrammesModalTemplateUrl                                  , ChooseNSProgrammesModalCtrl                                , 'xlg')
      .modal('chooseUserModal'                                          , chooseUserModalTemplateUrl                                          , ChooseUserModalCtrl                                        , 'md')
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.notificationSettings'                                           , '/notificationSettings'                                                                                                                                 ])
    .state(['root.notificationSettings.search'                                    , ''                              ,       ['@root'                            , notificationSettingsSearchTemplateUrl                         , NotificationSettingsSearchCtrl                        ]])
    .state(['root.notificationSettings.new'                                       , '/new'                          ,       ['@root'                            , notificationSettingsNewTemplateUrl                            , NotificationSettingsNewCtrl                           ]])
    .state(['root.notificationSettings.view'                                      , '/:id?rf'                       , true, ['@root'                            , notificationSettingsViewTemplateUrl                           , NotificationSettingsViewCtrl                          ]])
    .state(['root.notificationSettings.view.edit'                                 , ''                              ,       ['@root.notificationSettings.view'  , notificationSettingsEditTemplateUrl                           , NotificationSettingsEditCtrl                          ]]) 
    .state(['root.notificationSettings.view.attachedContracts'                    , '/attachedContracts'            ,                                                                                                                                                                   ])
    .state(['root.notificationSettings.view.attachedContracts.search'             , ''                              ,       ['@root.notificationSettings.view'  , notificationSettingAttachedContractsTepmplateUrl              , NotificationSettingAttachedContractsCtrl              ]])
    .state(['root.notificationSettings.view.attachedProcedures'                   , '/attachedProcedures'           ,                                                                                                                                                                   ])
    .state(['root.notificationSettings.view.attachedProcedures.search'            , ''                              ,       ['@root.notificationSettings.view'  , notificationSettingAttachedProceduresTepmplateUrl             , NotificationSettingAttachedProceduresCtrl             ]])
    .state(['root.notificationSettings.view.attachedProgrammePriorities'          , '/attachedPriorities'           ,                                                                                                                                                                   ])
    .state(['root.notificationSettings.view.attachedProgrammePriorities.search'   , ''                              ,       ['@root.notificationSettings.view'  , notificationSettingAttachedpPriorityTemplateUrl               , NotificationSettingAttachedProgrammePrioritiesCtrl    ]])
    .state(['root.notificationSettings.view.attachedProgrammes'                   , '/attachedProgrammes'           ,                                                                                                                                                                   ])
    .state(['root.notificationSettings.view.attachedProgrammes.search'            , ''                              ,       ['@root.notificationSettings.view'  , notificationSettingAttachedProgrammesTemplateUrl              , NotificationSettingAttachedProgrammesCtrl             ]])
    }
  ]);

export default NotificationSettingModule.name;
NotificationSettingModule.factory('NotificationSetting', NotificationSettingFactory);
NotificationSettingModule.factory(
  'NotificationSettingAttachedContract',
  NotificationSettingAttachedContractFactory
);
NotificationSettingModule.factory(
  'NotificationSettingAttachedProcedure',
  NotificationSettingAttachedProcedureFactory
);
NotificationSettingModule.factory(
  'NotificationSettingAttachedProgrammePriority',
  NotificationSettingAttachedProgrammePriorityFactory
);
NotificationSettingModule.factory(
  'NotificationSettingAttachedProgramme',
  NotificationSettingAttachedProgrammeFactory
);
