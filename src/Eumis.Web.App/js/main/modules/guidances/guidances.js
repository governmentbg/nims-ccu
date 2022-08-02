import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import guidanceDataTemplateUrl from './forms/guidanceData.html';
import { GuidanceFactory } from './resources/guidance';
import { GuidanceFileFactory } from './resources/guidanceFile';
import { GuidanceNavFileFactory } from './resources/guidanceNavFile';
import guidancesEditTemplateUrl from './views/guidancesEdit.html';
import { GuidancesEditCtrl } from './views/guidancesEditCtrl';
import guidancesNewTemplateUrl from './views/guidancesNew.html';
import { GuidancesNewCtrl } from './views/guidancesNewCtrl';
import guidancesSearchTemplateUrl from './views/guidancesSearch.html';
import { GuidancesSearchCtrl } from './views/guidancesSearchCtrl';

const GuidancesModule = angular
  .module('main.guidances', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisGuidanceData',
        templateUrl: guidanceDataTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.guidances'                                          , '/guidances'                                                                                                                                                                                               ])
    .state(['root.guidances.search'                                   , ''                 ,       ['@root'                           , guidancesSearchTemplateUrl                                                 , GuidancesSearchCtrl                    ]])
    .state(['root.guidances.new'                                      , '/new'             ,       ['@root'                           , guidancesNewTemplateUrl                                                    , GuidancesNewCtrl                       ]])
    .state(['root.guidances.edit'                                     , '/:id?rf'          ,       ['@root'                           , guidancesEditTemplateUrl                                                   , GuidancesEditCtrl                      ]]);
    }
  ]);

export default GuidancesModule.name;
GuidancesModule.factory('Guidance', GuidanceFactory);
GuidancesModule.factory('GuidanceFile', GuidanceFileFactory);
GuidancesModule.factory('GuidanceNavFile', GuidanceNavFileFactory);
