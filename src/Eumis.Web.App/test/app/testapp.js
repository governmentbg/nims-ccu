import angular from 'angular';
import AngularBootstrapNavTreeModule from 'angular-bootstrap-nav-tree';
import AngularMomentModule from 'angular-moment';
import NgResourceModule from 'angular-resource';
import NgAnimateModule from 'angular-animate';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import UiSortableModule from 'angular-ui-sortable/src/sortable';
import BootModule from 'js/boot';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import AngularL10NModule from 'l10n-angular/build/l10n-with-tools';
import form1TemplateUrl from './forms/form1.html';
import { Form1Ctrl } from './forms/form1Ctrl';
import { TestFileFactory } from './resources/testFile';
import TestappBgModule from './testapp_bg';
import filledTemplateUrl from './views/filled.html';
import { FilledCtrl } from './views/filledCtrl';
import pageTemplateUrl from './views/page.html';
import { PageCtrl } from './views/pageCtrl';
import readonlyTemplateUrl from './views/readonly.html';
import { ReadonlyCtrl } from './views/readonlyCtrl';
import regularTemplateUrl from './views/regular.html';
import { RegularCtrl } from './views/regularCtrl';
import usersDatatableTemplateUrl from './views/usersDatatable.html';
import { UsersDatatableCtrl } from './views/usersDatatableCtrl';
import ChartJsModule from 'angular-chart.js';

const TestappModule = angular
  .module('testapp', [
    NgResourceModule,
    NgAnimateModule,
    UiRouterModule,
    UiBootstrapModule,
    UiSortableModule,
    AngularMomentModule,
    AngularL10NModule,
    ChartJsModule,
    AngularBootstrapNavTreeModule,
    BootModule,
    ScaffoldingModule,
    TestappBgModule
  ])
  .config([
    '$urlRouterProvider',
    '$locationProvider',
    function($urlRouterProvider, $locationProvider) {
      $locationProvider.html5Mode(false);
      $urlRouterProvider.otherwise('/testpage/regular');
    }
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'testPageForm1',
        controller: Form1Ctrl,
        templateUrl: form1TemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['testpage'                , '/testpage'      ,  ['@'        , pageTemplateUrl           , PageCtrl          ]])
    .state(['testpage.regular'        , '/regular'       ,  ['@testpage', regularTemplateUrl        , RegularCtrl       ]])
    .state(['testpage.filled'         , '/filled'        ,  ['@testpage', filledTemplateUrl         , FilledCtrl        ]])
    .state(['testpage.readonly'       , '/readonly'      ,  ['@testpage', readonlyTemplateUrl       , ReadonlyCtrl      ]])
    .state(['testpage.usersDatatable' , '/users'         ,  ['@testpage', usersDatatableTemplateUrl , UsersDatatableCtrl]]);
    }
  ])
  .constant('eumisConstants', {
    emailRegex: /^[a-z0-9!#$%&'*+/=?^_`{|}~.-]+@[a-z0-9-]+(\.[a-z0-9-]+)*$/i
  })
  .factory('accessToken', function() {
    return {
      get: function() {
        return 'testapptoken';
      }
    };
  });

export default TestappModule.name;
TestappModule.factory('TestFile', TestFileFactory);
