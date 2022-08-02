import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import permissionsTemplateUrl from './forms/permissions.html';
import { PermissionsCtrl } from './forms/permissionsCtrl';
import requestPackageTemplateUrl from './forms/requestPackage.html';
import { RequestPackageCtrl } from './forms/requestPackageCtrl';
import userDataTemplateUrl from './forms/userData.html';
import { UserDataCtrl } from './forms/userDataCtrl';
import userOrganizationTemplateUrl from './forms/userOrganization.html';
import userTypeTemplateUrl from './forms/userType.html';
import { UserTypeCtrl } from './forms/userTypeCtrl';
import changePasswordModalTemplateUrl from './modals/changePasswordModal.html';
import { ChangePasswordModalCtrl } from './modals/changePasswordModalCtrl';
import chooseUsersModalTemplateUrl from './modals/chooseUsersModal.html';
import { ChooseUsersModalCtrl } from './modals/chooseUsersModalCtrl';
import createAndChooseUserModalTemplateUrl from './modals/createAndChooseUserModal.html';
import { CreateAndChooseUserModalCtrl } from './modals/createAndChooseUserModalCtrl';
import notificationSettingsModalTemplateUrl from './modals/notificationSettingsModal.html';
import { NotificationSettingsModalCtrl } from './modals/notificationSettingsModalCtrl';
import permissionRequestModalTemplateUrl from './modals/permissionRequestModal.html';
import { PermissionRequestModalCtrl } from './modals/permissionRequestModalCtrl';
import permissionTemplateModalTemplateUrl from './modals/permissionTemplateModal.html';
import { PermissionTemplateModalCtrl } from './modals/permissionTemplateModalCtrl';
import regDataRequestModalTemplateUrl from './modals/regDataRequestModal.html';
import { RegDataRequestModalCtrl } from './modals/regDataRequestModalCtrl';
import { PermissionTemplateFactory } from './resources/permissionTemplate';
import { RequestPackageFactory } from './resources/requestPackage';
import { RequestPackageFileFactory } from './resources/requestPackageFile';
import { RequestPackageUserFactory } from './resources/requestPackageUser';
import { PermissionRequestFactory } from './resources/requestPackageUserPermissionRequest';
import { RegDataRequestFactory } from './resources/requestPackageUserRegDataRequest';
import { UserFactory } from './resources/user';
import { UserOrganizationFactory } from './resources/userOrganization';
import { UserDeclarationFactory } from './resources/userDeclaration';
import { UserNotificationSettingFactory } from './resources/userNotificationSetting';
import { UserPermissionFactory } from './resources/userPermission';
import { UserRequestFactory } from './resources/userRequest';
import { UserTypeFactory } from './resources/userType';
import pTemplatesEditTemplateUrl from './views/pTemplatesEdit.html';
import { PTemplatesEditCtrl } from './views/pTemplatesEditCtrl';
import pTemplatesNewTemplateUrl from './views/pTemplatesNew.html';
import { PTemplatesNewCtrl } from './views/pTemplatesNewCtrl';
import pTemplatesSearchTemplateUrl from './views/pTemplatesSearch.html';
import { PTemplatesSearchCtrl } from './views/pTemplatesSearchCtrl';
import requestPackagesEditTemplateUrl from './views/requestPackagesEdit.html';
import { RequestPackagesEditCtrl } from './views/requestPackagesEditCtrl';
import requestPackagesNewTemplateUrl from './views/requestPackagesNew.html';
import { RequestPackagesNewCtrl } from './views/requestPackagesNewCtrl';
import requestPackagesSearchTemplateUrl from './views/requestPackagesSearch.html';
import { RequestPackagesSearchCtrl } from './views/requestPackagesSearchCtrl';
import requestPackagesViewTemplateUrl from './views/requestPackagesView.html';
import { RequestPackagesViewCtrl } from './views/requestPackagesViewCtrl';
import requestPackageUsersTemplateUrl from './views/requestPackageUsers.html';
import { RequestPackageUsersCtrl } from './views/requestPackageUsersCtrl';
import userOrganizationsEditTemplateUrl from './views/userOrganizationsEdit.html';
import { UserOrganizationsEditCtrl } from './views/userOrganizationsEditCtrl';
import userOrganizationsNewTemplateUrl from './views/userOrganizationsNew.html';
import { UserOrganizationsNewCtrl } from './views/userOrganizationsNewCtrl';
import userOrganizationsSearchTemplateUrl from './views/userOrganizationsSearch.html';
import { UserOrganizationsSearchCtrl } from './views/userOrganizationsSearchCtrl';
import userPermissionsTemplateUrl from './views/userPermissions.html';
import { UserPermissionsCtrl } from './views/userPermissionsCtrl';
import userRequestsSearchTemplateUrl from './views/userRequestsSearch.html';
import { UserRequestsSearchCtrl } from './views/userRequestsSearchCtrl';
import userDeclarationsSearchTemplateUrl from './views/userDeclarationsSearch.html';
import { UserDeclarationsSearchCtrl } from './views/userDeclarationsSearchCtrl';
import userNotificationSettingsSearchTemplateUrl from './views/userNotificationSettingsSearch.html';
import { UserNotificationSettingsSearchCtrl } from './views/userNotificationSettingsSearchCtrl';
import usersEditTemplateUrl from './views/usersEdit.html';
import { UsersEditCtrl } from './views/usersEditCtrl';
import usersNewTemplateUrl from './views/usersNew.html';
import { UsersNewCtrl } from './views/usersNewCtrl';
import usersSearchTemplateUrl from './views/usersSearch.html';
import { UsersSearchCtrl } from './views/usersSearchCtrl';
import usersViewTemplateUrl from './views/usersView.html';
import { UsersViewCtrl } from './views/usersViewCtrl';
import userTypesEditTemplateUrl from './views/userTypesEdit.html';
import { UserTypesEditCtrl } from './views/userTypesEditCtrl';
import userTypesNewTemplateUrl from './views/userTypesNew.html';
import { UserTypesNewCtrl } from './views/userTypesNewCtrl';
import userTypesSearchTemplateUrl from './views/userTypesSearch.html';
import { UserTypesSearchCtrl } from './views/userTypesSearchCtrl';

const UsersModule = angular
  .module('main.users', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisUserData',
        templateUrl: userDataTemplateUrl,
        controller: UserDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisPermissions',
        templateUrl: permissionsTemplateUrl,
        controller: PermissionsCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisUserType',
        templateUrl: userTypeTemplateUrl,
        controller: UserTypeCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisRequestPackage',
        templateUrl: requestPackageTemplateUrl,
        controller: RequestPackageCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisUserOrganization',
        templateUrl: userOrganizationTemplateUrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('changePasswordModal'                    , changePasswordModalTemplateUrl                                     , ChangePasswordModalCtrl                    , 'xsm')
    .modal('notificationSettingsModal'              , notificationSettingsModalTemplateUrl                               , NotificationSettingsModalCtrl              , 'xlg')
    .modal('permissionTemplateModal'                , permissionTemplateModalTemplateUrl                                 , PermissionTemplateModalCtrl                       )
    .modal('chooseUsersModal'                       , chooseUsersModalTemplateUrl                                        , ChooseUsersModalCtrl                       , 'xlg')
    .modal('createAndChooseUserModal'               , createAndChooseUserModalTemplateUrl                                , CreateAndChooseUserModalCtrl               , 'xlg')
    .modal('regDataRequestModal'                    , regDataRequestModalTemplateUrl                                     , RegDataRequestModalCtrl                    , 'xlg')
    .modal('permissionRequestModal'                 , permissionRequestModalTemplateUrl                                  , PermissionRequestModalCtrl                 , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.users'                                              , '/users?username&fullname&userOrganizationId&active&deleted&locked&hasAcceptedGDPRDeclaration'                                                                                                                     ])
    .state(['root.users.search'                                       , ''                 ,       ['@root'                           , usersSearchTemplateUrl                                                         , UsersSearchCtrl                        ]])
    .state(['root.users.new'                                          , '/new'             ,       ['@root'                           , usersNewTemplateUrl                                                            , UsersNewCtrl                           ]])
    .state(['root.users.view'                                         , '/:id?rf'          , true, ['@root'                           , usersViewTemplateUrl                                                           , UsersViewCtrl                          ]])
    .state(['root.users.view.edit'                                    , ''                 ,       ['@root.users.view'                , usersEditTemplateUrl                                                           , UsersEditCtrl                          ]])
    .state(['root.users.view.permissions'                             , '/permissions'     ,       ['@root.users.view'                , userPermissionsTemplateUrl                                                     , UserPermissionsCtrl                    ]])
    .state(['root.users.view.requests'                                , '/requests'                                                                                                                                                                                                ])
    .state(['root.users.view.requests.search'                         , ''                 ,       ['@root.users.view'                , userRequestsSearchTemplateUrl                                                  , UserRequestsSearchCtrl                 ]])
    .state(['root.users.view.declarations'                            , '/declarations'                                                                                                                                                                                            ])
    .state(['root.users.view.declarations.search'                     , ''                 ,       ['@root.users.view'                , userDeclarationsSearchTemplateUrl                                              , UserDeclarationsSearchCtrl             ]])
    .state(['root.users.view.notificationSettings'                    , '/notificationSettings'                                                                                                                                                                                           ])
    .state(['root.users.view.notificationSettings.search'             , ''                 ,       ['@root.users.view'                , userNotificationSettingsSearchTemplateUrl                                      , UserNotificationSettingsSearchCtrl     ]])

    .state(['root.pTemplates'                                         , '/pTemplates'                                                                                                                                                                                              ])
    .state(['root.pTemplates.search'                                  , ''                 ,       ['@root'                           , pTemplatesSearchTemplateUrl                                                    , PTemplatesSearchCtrl                   ]])
    .state(['root.pTemplates.new'                                     , '/new'             ,       ['@root'                           , pTemplatesNewTemplateUrl                                                       , PTemplatesNewCtrl                      ]])
    .state(['root.pTemplates.edit'                                    , '/:id?rf'          ,       ['@root'                           , pTemplatesEditTemplateUrl                                                      , PTemplatesEditCtrl                     ]])

    .state(['root.userTypes'                                          , '/userTypes'                                                                                                                                                                                               ])
    .state(['root.userTypes.search'                                   , ''                 ,       ['@root'                           , userTypesSearchTemplateUrl                                                     , UserTypesSearchCtrl                    ]])
    .state(['root.userTypes.new'                                      , '/new'             ,       ['@root'                           , userTypesNewTemplateUrl                                                        , UserTypesNewCtrl                       ]])
    .state(['root.userTypes.edit'                                     , '/:id?rf'          ,       ['@root'                           , userTypesEditTemplateUrl                                                       , UserTypesEditCtrl                      ]])

    .state(['root.requestPackages'                                    , '/requestPackages?dateFrom&dateTo&typeId&userOrganizationId&statusId'                                                                                                                                      ])
    .state(['root.requestPackages.search'                             , ''                 ,       ['@root'                           , requestPackagesSearchTemplateUrl                                               , RequestPackagesSearchCtrl              ]])
    .state(['root.requestPackages.new'                                , '/new?d'           ,       ['@root'                           , requestPackagesNewTemplateUrl                                                  , RequestPackagesNewCtrl                 ]])
    .state(['root.requestPackages.view'                               , '/:id?rf'          , true, ['@root'                           , requestPackagesViewTemplateUrl                                                 , RequestPackagesViewCtrl                ]])
    .state(['root.requestPackages.view.edit'                          , ''                 ,       ['@root.requestPackages.view'      , requestPackagesEditTemplateUrl                                                 , RequestPackagesEditCtrl                ]])
    .state(['root.requestPackages.view.users'                         , '/users'           ,       ['@root.requestPackages.view'      , requestPackageUsersTemplateUrl                                                 , RequestPackageUsersCtrl                ]])

    .state(['root.userOrganizations'                                  , '/userOrganizations'                                                                                                                                                                                       ])
    .state(['root.userOrganizations.search'                           , ''                 ,       ['@root'                           , userOrganizationsSearchTemplateUrl                                             , UserOrganizationsSearchCtrl            ]])
    .state(['root.userOrganizations.new'                              , '/new'             ,       ['@root'                           , userOrganizationsNewTemplateUrl                                                , UserOrganizationsNewCtrl               ]])
    .state(['root.userOrganizations.edit'                             , '/:id?rf'          ,       ['@root'                           , userOrganizationsEditTemplateUrl                                               , UserOrganizationsEditCtrl              ]]);
    }
  ]);

export default UsersModule.name;
UsersModule.factory('PermissionTemplate', PermissionTemplateFactory);
UsersModule.factory('RequestPackage', RequestPackageFactory);
UsersModule.factory('RequestPackageFile', RequestPackageFileFactory);
UsersModule.factory('RequestPackageUser', RequestPackageUserFactory);
UsersModule.factory('PermissionRequest', PermissionRequestFactory);
UsersModule.factory('RegDataRequest', RegDataRequestFactory);
UsersModule.factory('User', UserFactory);
UsersModule.factory('UserOrganization', UserOrganizationFactory);
UsersModule.factory('UserDeclaration', UserDeclarationFactory);
UsersModule.factory('UserNotificationSetting', UserNotificationSettingFactory);
UsersModule.factory('UserPermission', UserPermissionFactory);
UsersModule.factory('UserRequest', UserRequestFactory);
UsersModule.factory('UserType', UserTypeFactory);
