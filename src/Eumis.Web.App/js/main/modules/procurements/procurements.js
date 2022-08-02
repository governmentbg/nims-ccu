import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { ProcurementFactory } from './resources/procurement';
import { ProcurementDocumentFactory } from './resources/procurementDocument';
import { ProcurementFileFactory } from './resources/procurementFile';
import { ProcurementDifferentiatedPositionFactory } from './resources/procurementDifferentiatedPosition';
import procurementDataTemplateUrl from './forms/procurementData.html';
import { ProcurementDataCtrl } from './forms/procurementDataCtrl';
import procurementDifferentiatedPositionTemplateUrl from './forms/procurementDifferentiatedPosition.html';
import procurementsSearchTemplateUrl from './views/procurementsSearch.html';
import { ProcurementsSearchCtrl } from './views/procurementsSearchCtrl';
import procurementsNewTemplateUrl from './views/procurementsNew.html';
import { ProcurementsNewCtrl } from './views/procurementsNewCtrl';
import procurementsViewTemplateUrl from './views/procurementsView.html';
import { ProcurementsViewCtrl } from './views/procurementsViewCtrl';
import procurementsEditTemplateUrl from './views/procurementsEdit.html';
import { ProcurementsEditCtrl } from './views/procurementsEditCtrl';
import procurementDocumentsSearchTemplateUrl from './views/procurementDocumentsSearch.html';
import { ProcurementDocumentsSearchCtrl } from './views/procurementDocumentsSearchCtrl';
import procurementDocumentsNewTemplateUrl from './views/procurementDocumentsNew.html';
import { ProcurementDocumentsNewCtrl } from './views/procurementDocumentsNewCtrl';
import procurementDocumentsEditTemplateUrl from './views/procurementDocumentsEdit.html';
import { ProcurementDocumentsEditCtrl } from './views/procurementDocumentsEditCtrl';
import procurementDifferentiatedPositionSearchTemplateUrl from './views/procurementDifferentiatedPositionSearch.html';
import { ProcurementDifferentiatedPositionsSearchCtrl } from './views/procurementDifferentiatedPositionSearchCtrl';
import procurementDifferentiatedPositionNewTemplateUrl from './views/procurementDifferentiatedPositionNew.html';
import { ProcurementDifferentiatedPositionsNewCtrl } from './views/procurementDifferentiatedPositionNewCtrl';
import procurementDifferentiatedPositionEditTemplateUrl from './views/procurementDifferentiatedPositionEdit.html';
import { ProcurementDifferentiatedPostionionsEditCtrl } from './views/procurementDifferentiatedPositionEditCtrl';

const ProcurementsModule = angular
  .module('main.procurements', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisProcurementData',
        templateUrl: procurementDataTemplateUrl,
        controller: ProcurementDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisDifferentiatedPosition',
        templateUrl: procurementDifferentiatedPositionTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.procurements'                                             , '/procurements'                                                                                                                                                                                            ])
    .state(['root.procurements.search'                                      , ''                    ,       ['@root'                           , procurementsSearchTemplateUrl                                             , ProcurementsSearchCtrl                         ]])
    .state(['root.procurements.new'                                         , '/new'                ,       ['@root'                           , procurementsNewTemplateUrl                                                , ProcurementsNewCtrl                            ]])
    .state(['root.procurements.view'                                        , '/:id?rf'             , true, ['@root'                           , procurementsViewTemplateUrl                                               , ProcurementsViewCtrl                           ]])
    .state(['root.procurements.view.edit'                                   , ''                    ,       ['@root.procurements.view'         , procurementsEditTemplateUrl                                               , ProcurementsEditCtrl                           ]])

    .state(['root.procurements.view.documents'                              , '/documents'                                                                                                                                                                                               ])
    .state(['root.procurements.view.documents.search'                       , ''                    ,       ['@root.procurements.view'         , procurementDocumentsSearchTemplateUrl                                      , ProcurementDocumentsSearchCtrl                ]])
    .state(['root.procurements.view.documents.new'                          , '/new'                ,       ['@root.procurements.view'         , procurementDocumentsNewTemplateUrl                                         , ProcurementDocumentsNewCtrl                   ]])
    .state(['root.procurements.view.documents.edit'                         , '/:ind'               ,       ['@root.procurements.view'         , procurementDocumentsEditTemplateUrl                                        , ProcurementDocumentsEditCtrl                  ]])

    .state(['root.procurements.view.differentiatedPosition'                  , '/positions'                                                                                                                                                                                               ])
    .state(['root.procurements.view.differentiatedPosition.search'           , ''                    ,       ['@root.procurements.view'         , procurementDifferentiatedPositionSearchTemplateUrl                         , ProcurementDifferentiatedPositionsSearchCtrl  ]])
    .state(['root.procurements.view.differentiatedPosition.new'              , '/new'                ,       ['@root.procurements.view'         , procurementDifferentiatedPositionNewTemplateUrl                            , ProcurementDifferentiatedPositionsNewCtrl     ]])
    .state(['root.procurements.view.differentiatedPosition.edit'             , '/:ind'               ,       ['@root.procurements.view'         , procurementDifferentiatedPositionEditTemplateUrl                           , ProcurementDifferentiatedPostionionsEditCtrl  ]])
    }
  ]);

export default ProcurementsModule.name;
ProcurementsModule.factory('Procurement', ProcurementFactory);
ProcurementsModule.factory('ProcurementDocument', ProcurementDocumentFactory);
ProcurementsModule.factory('ProcurementFile', ProcurementFileFactory);
ProcurementsModule.factory(
  'ProcurementDifferentiatedPosition',
  ProcurementDifferentiatedPositionFactory
);
