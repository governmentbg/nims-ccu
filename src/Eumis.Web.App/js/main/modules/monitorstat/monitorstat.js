import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { MonitorstatFactory } from './resources/monitorstat';
import monitorstatSearchTemplateUrl from './views/monitorstatSearch.html';
import { MonitorstatSearchCtrl } from './views/monitorstatSearchCtrl';
import monitorstatNewTemplateUrl from './views/monitorstatNew.html';
import { MonitorstatNewCtrl } from './views/monitorstatNewCtrl';
import monitorstatViewTemplateUrl from './views/monitorstatView.html';
import { MonitorstatViewCtrl } from './views/monitorstatViewCtrl';

const MonitorstatInterfacesModule = angular
  .module('main.monitorstatInterfaces', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
      .state(['root.monitorstat'                                                        , '/monitorstat'                                                                                                                                                                                                                                 ])
      .state(['root.monitorstat.search'                                                 , ''                  ,       ['@root'                                            , monitorstatSearchTemplateUrl                                                                    , MonitorstatSearchCtrl                                     ]])
      .state(['root.monitorstat.new'                                                    , '/new'              ,       ['@root'                                            , monitorstatNewTemplateUrl                                                                       , MonitorstatNewCtrl                                        ]])
      .state(['root.monitorstat.view'                                                   , '/:id?rf'           ,       ['@root'                                            , monitorstatViewTemplateUrl                                                                      , MonitorstatViewCtrl                                       ]])
    }
  ]);

export default MonitorstatInterfacesModule.name;
MonitorstatInterfacesModule.factory('Monitorstat', MonitorstatFactory);
