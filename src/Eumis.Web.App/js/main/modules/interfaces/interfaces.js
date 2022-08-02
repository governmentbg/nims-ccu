import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import interfacesSearchTemplateUrl from './views/interfacesSearch.html';
import { InterfacesSearchCtrl } from './views/interfacesSearchCtrl';

const InterfacesModule = angular
  .module('main.interfaces', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.interfaces'                                                      , '/interfaces'                                                                                                                                                                                                                                                             ])
    .state(['root.interfaces.search'                                               , ''                  ,       ['@root'                                            , interfacesSearchTemplateUrl                                                                     , InterfacesSearchCtrl                                          ]]);
    }
  ]);

export default InterfacesModule.name;
