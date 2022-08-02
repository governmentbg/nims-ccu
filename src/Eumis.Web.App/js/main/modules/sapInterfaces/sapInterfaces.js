import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import sapFileDataTemplateUrl from './forms/sapFileData.html';
import { SapCertReportFactory } from './resources/sapCertReports';
import { SapFileFactory } from './resources/sapFile';
import { SapFileBlobFileFactory } from './resources/sapFileBlobFile';
import sapCertReportsSearchTemplateUrl from './views/sapCertReportsSearch.html';
import { SapCertReportsSearchCtrl } from './views/sapCertReportsSearchCtrl';
import sapFilePaidAmountsSearchTemplateUrl from './views/sapFilePaidAmountsSearch.html';
import { SapFilePaidAmountsSearchCtrl } from './views/sapFilePaidAmountsSearchCtrl';
import sapFileDistributedLimitsSearchTemplateUrl from './views/sapFileDistributedLimitsSearch.html';
import { SapFileDistributedLimitsSearchCtrl } from './views/sapFileDistributedLimitsSearchCtrl';
import sapFilesEditTemplateUrl from './views/sapFilesEdit.html';
import { SapFilesEditCtrl } from './views/sapFilesEditCtrl';
import sapFilesNewTemplateUrl from './views/sapFilesNew.html';
import { SapFilesNewCtrl } from './views/sapFilesNewCtrl';
import sapFilesSearchTemplateUrl from './views/sapFilesSearch.html';
import { SapFilesSearchCtrl } from './views/sapFilesSearchCtrl';
import sapFilesViewTemplateUrl from './views/sapFilesView.html';
import { SapFilesViewCtrl } from './views/sapFilesViewCtrl';

const SapInterfacesModule = angular
  .module('main.sapInterfaces', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisSapFileData',
        templateUrl: sapFileDataTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.sapFiles'                                                        , '/sapFiles?status&type'                                                                                                                                                                                                                                                  ])
    .state(['root.sapFiles.search'                                                 , ''                   ,       ['@root'                                            , sapFilesSearchTemplateUrl                                                                    , SapFilesSearchCtrl                                            ]])
    .state(['root.sapFiles.new'                                                    , '/new'               ,       ['@root'                                            , sapFilesNewTemplateUrl                                                                       , SapFilesNewCtrl                                               ]])
    .state(['root.sapFiles.view'                                                   , '/:id?rf'            , true, ['@root'                                            , sapFilesViewTemplateUrl                                                                      , SapFilesViewCtrl                                              ]])
    .state(['root.sapFiles.view.edit'                                              , ''                   ,       ['@root.sapFiles.view'                              , sapFilesEditTemplateUrl                                                                      , SapFilesEditCtrl                                              ]])
    .state(['root.sapFiles.view.paidAmounts'                                       , '/paidAmounts'       ,       ['@root.sapFiles.view'                              , sapFilePaidAmountsSearchTemplateUrl                                                          , SapFilePaidAmountsSearchCtrl                                  ]])
    .state(['root.sapFiles.view.distributedLimits'                                 , '/distrbituredLimits',       ['@root.sapFiles.view'                              , sapFileDistributedLimitsSearchTemplateUrl                                                    , SapFileDistributedLimitsSearchCtrl                            ]])

    .state(['root.sapCertReports'                                                  , '/sapCertReports?fromDate&toDate&certReportId'                                                                                                                                                                                                                            ])
    .state(['root.sapCertReports.search'                                           , ''                  ,       ['@root'                                            , sapCertReportsSearchTemplateUrl                                                              , SapCertReportsSearchCtrl                                      ]]);
    }
  ]);

export default SapInterfacesModule.name;
SapInterfacesModule.factory('SapCertReport', SapCertReportFactory);
SapInterfacesModule.factory('SapFile', SapFileFactory);
SapInterfacesModule.factory('SapFileBlobFile', SapFileBlobFileFactory);
