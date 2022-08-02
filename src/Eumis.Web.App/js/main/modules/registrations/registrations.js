import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import registrationTemplateUrl from './forms/registration.html';
import { RegistrationCtrl } from './forms/registrationCtrl';
import { RegistrationFactory } from './resources/registration';
import registrationsEditTemplateUrl from './views/registrationsEdit.html';
import { RegistrationsEditCtrl } from './views/registrationsEditCtrl';
import registrationsSearchTemplateUrl from './views/registrationsSearch.html';
import { RegistrationsSearchCtrl } from './views/registrationsSearchCtrl';

const RegistrationsModule = angular
  .module('main.registrations', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisRegistration',
        templateUrl: registrationTemplateUrl,
        controller: RegistrationCtrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.registrations'                                      , '/registrations'                                                                                                                                                                                           ])
    .state(['root.registrations.search'                               , ''                 ,       ['@root'                           , registrationsSearchTemplateUrl                                         , RegistrationsSearchCtrl                ]])
    .state(['root.registrations.view'                                 , '/:id'             ,       ['@root'                           , registrationsEditTemplateUrl                                           , RegistrationsEditCtrl                  ]]);
    }
  ]);

export default RegistrationsModule.name;
RegistrationsModule.factory('Registration', RegistrationFactory);
