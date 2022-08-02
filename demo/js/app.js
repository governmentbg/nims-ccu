/*global angular*/
(function (angular) {
  'use strict';
  angular.module('app', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.select2',
    'l10n',
    'l10n-tools',
    'ui.sortable',
    'scaffolding'
  ]).config(['$urlRouterProvider', '$locationProvider',function($urlRouterProvider, $locationProvider) {
      $locationProvider.html5Mode(false);
      $urlRouterProvider.otherwise('/programmes/programme');
  }]).config(['$stateProvider', function ($stateProvider) {
      $stateProvider
      .state('programmes/programme', { url: "/programmes/programme", templateUrl: 'forms/programmes/programme.html' })
      .state('programmes/programmeInstitution', { url: "/programmes/programmeInstitution", templateUrl: 'forms/programmes/programmeInstitution.html' })
      .state('programmes/programmeBudget', { url: "/programmes/programmeBudget", templateUrl: 'forms/programmes/programmeBudget.html' })
      .state('sampleSearch', { url: "/sampleSearch", templateUrl: 'forms/sampleSearch.html' })
      .state('sampleForm', { url: "/sampleForm", templateUrl: 'forms/sampleForm.html' });
  }]);
}(angular));
