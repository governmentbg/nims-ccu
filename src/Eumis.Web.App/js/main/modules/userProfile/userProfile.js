import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';

import profileViewTemplateUrl from './views/profileView.html';
import { ProfileViewCtrl } from './views/profileViewCtrl';

import userDataTemplateUrl from './views/userData.html';
import { UserDataCtrl } from './views/userDataCtrl';

import userPermissionsTemplateUrl from './views/userPermissions.html';
import { UserPermissionsCtrl } from './views/userPermissionsCtrl';

import userRequestsSearchTemplateUrl from './views/userRequestsSearch.html';
import { UserRequestsSearchCtrl } from './views/userRequestsSearchCtrl';

import userDeclarationsSearchTemplateUrl from './views/userDeclarationsSearch.html';
import { UserDeclarationsSearchCtrl } from './views/userDeclarationsSearchCtrl';

import { UserProfileFactory } from './resources/userProfile';

const UserProfileModule = angular
  .module('main.userProfile', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
  .state(['root.userProfile'                                        , '/userProfile'                                                                                                                                                                           ])
  .state(['root.userProfile.view'                                   , ''                 , true, ['@root'                           , profileViewTemplateUrl                                                         , ProfileViewCtrl                        ]])
  .state(['root.userProfile.view.regData'                           , ''                 ,       ['@root.userProfile.view'          , userDataTemplateUrl                                                            , UserDataCtrl                           ]])
  .state(['root.userProfile.view.permissions'                       , '/permissions'     ,       ['@root.userProfile.view'          , userPermissionsTemplateUrl                                                     , UserPermissionsCtrl                    ]])
  .state(['root.userProfile.view.requests'                          , '/requests'        ,       ['@root.userProfile.view'          , userRequestsSearchTemplateUrl                                                  , UserRequestsSearchCtrl                 ]])
  .state(['root.userProfile.view.declarations'                      , '/declarations'    ,       ['@root.userProfile.view'          , userDeclarationsSearchTemplateUrl                                              , UserDeclarationsSearchCtrl             ]])
    }
  ]);

export default UserProfileModule.name;

UserProfileModule.factory('UserProfile', UserProfileFactory);
