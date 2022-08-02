import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { RegixFactory } from './resources/regix';
import regixInterfacesNpoRegistrationTemplateUrl from './views/regixInterfacesNpoRegistration.html';
import { RegixInterfaceNpoRegistrationCtrl } from './views/regixInterfacesNpoRegistrationCtrl';
import regixInterfacesStateOfPlayTemplateUrl from './views/regixInterfacesStateOfPlay.html';
import { RegixInterfacesStateOfPlayCtrl } from './views/regixInterfacesStateOfPlayCtrl';
import regixInterfacesPersonalIdentityTemplateUrl from './views/regixInterfacesPersonalIdentity.html';
import { RegixInterfacesPersonalIdentityCtrl } from './views/regixInterfacesPersonalIdentityCtrl';
import regixInterfacesActualStateTemplateUrl from './views/regixInterfacesActualState.html';
import { RegixInterfacesActualStateCtrl } from './views/regixInterfacesActualStateCtrl';
import regixInterfacesValidPersonTemplateUrl from './views/regixInterfacesValidPerson.html';
import { RegixInterfacesValidPersonCtrl } from './views/regixInterfacesValidPersonCtrl';
import regixInterfacesViewTemplateUrl from './views/regixInterfacesView.html';
import { RegixInterfacesViewCtrl } from './views/regixInterfacesViewCtrl';

const RegixInterfacesModule = angular
  .module('main.regixInterfaces', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.regix'                                                        , '/regix'                                                                                                                                                                                                                                         ])
    .state(['root.regix.view'                                                   , ''                   , true, ['@root'                                            , regixInterfacesViewTemplateUrl                                                                      , RegixInterfacesViewCtrl                                 ]])
    .state(['root.regix.view.validPerson'                                       , '/validPerson'       ,       ['@root.regix.view'                                 , regixInterfacesValidPersonTemplateUrl                                                               , RegixInterfacesValidPersonCtrl                          ]])
    .state(['root.regix.view.personalIdentity'                                  , '/personalIdentity'  ,       ['@root.regix.view'                                 , regixInterfacesPersonalIdentityTemplateUrl                                                          , RegixInterfacesPersonalIdentityCtrl                     ]])
    .state(['root.regix.view.actualState'                                       , '/actualState'       ,       ['@root.regix.view'                                 , regixInterfacesActualStateTemplateUrl                                                               , RegixInterfacesActualStateCtrl                          ]])
    .state(['root.regix.view.stateOfPlay'                                       , '/stateOfPlay'       ,       ['@root.regix.view'                                 , regixInterfacesStateOfPlayTemplateUrl                                                               , RegixInterfacesStateOfPlayCtrl                          ]])
    .state(['root.regix.view.npoRegistration'                                   , '/npoRegistration'   ,       ['@root.regix.view'                                 , regixInterfacesNpoRegistrationTemplateUrl                                                           , RegixInterfaceNpoRegistrationCtrl                       ]])
    }
  ]);

export default RegixInterfacesModule.name;
RegixInterfacesModule.factory('Regix', RegixFactory);
