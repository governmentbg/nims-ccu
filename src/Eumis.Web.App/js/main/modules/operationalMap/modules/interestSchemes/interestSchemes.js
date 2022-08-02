import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import interestSchemeTemplateUrl from './forms/interestScheme.html';
import { InterestSchemeFactory } from './resources/interestScheme';
import interestSchemesEditTemplateUrl from './views/interestSchemesEdit.html';
import { InterestSchemesEditCtrl } from './views/interestSchemesEditCtrl';
import interestSchemesNewTemplateUrl from './views/interestSchemesNew.html';
import { InterestSchemesNewCtrl } from './views/interestSchemesNewCtrl';
import interestSchemesSearchTemplateUrl from './views/interestSchemesSearch.html';
import { InterestSchemesSearchCtrl } from './views/interestSchemesSearchCtrl';

const OperationalMapInterestSchemesModule = angular
  .module('main.operationalMap.interestSchemes', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisInterestScheme',
        templateUrl: interestSchemeTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map.interestSchemes'                                , '/interestSchemes'                                                                                                                                                                                         ])
    .state(['root.map.interestSchemes.search'                         , ''                 ,       ['@root'                           , interestSchemesSearchTemplateUrl                      , InterestSchemesSearchCtrl              ]])
    .state(['root.map.interestSchemes.new'                            , '/new?rf'          ,       ['@root'                           , interestSchemesNewTemplateUrl                         , InterestSchemesNewCtrl                 ]])
    .state(['root.map.interestSchemes.edit'                           , '/:id?rf'          ,       ['@root'                           , interestSchemesEditTemplateUrl                        , InterestSchemesEditCtrl                ]]);
    }
  ]);

export default OperationalMapInterestSchemesModule.name;
OperationalMapInterestSchemesModule.factory('InterestScheme', InterestSchemeFactory);
