import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import declarationDataTemplateUrl from './forms/declarationData.html';
import { DeclarationDataCtrl } from './forms/declarationDataCtrl';
import publishDeclarationModalTemplateUrl from './modals/publishDeclarationModal.html';
import { PublishDeclarationModalCtrl } from './modals/publishDeclarationModalCtrl';
import { DeclarationFactory } from './resources/declaration';
import { DeclarationFileFactory } from './resources/declarationFile';
import declarationsEditTemplateUrl from './views/declarationsEdit.html';
import { DeclarationsEditCtrl } from './views/declarationsEditCtrl';
import declarationsNewTemplateUrl from './views/declarationsNew.html';
import { DeclarationsNewCtrl } from './views/declarationsNewCtrl';
import declarationsSearchTemplateUrl from './views/declarationsSearch.html';
import { DeclarationsSearchCtrl } from './views/declarationsSearchCtrl';

const OperationalMapDeclarationsModule = angular
  .module('main.operationalMap.declarations', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisDeclarationData',
        templateUrl: declarationDataTemplateUrl,
        controller: DeclarationDataCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
        .modal('publishDeclarationModal'                , publishDeclarationModalTemplateUrl                                  , PublishDeclarationModalCtrl                , 'sm');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
        .state(['root.map.declarations'                                   , '/declarations?activationDate&status'                                                                                                                                                                      ])
        .state(['root.map.declarations.search'                            , ''                 ,       ['@root'                           , declarationsSearchTemplateUrl                                                   , DeclarationsSearchCtrl                 ]])
        .state(['root.map.declarations.new'                               , '/new'             ,       ['@root'                           , declarationsNewTemplateUrl                                                      , DeclarationsNewCtrl                    ]])
        .state(['root.map.declarations.edit'                              , '/:id?rf'          ,       ['@root'                           , declarationsEditTemplateUrl                                                     , DeclarationsEditCtrl                   ]]);
    }
  ]);

export default OperationalMapDeclarationsModule.name;
OperationalMapDeclarationsModule.factory('Declaration', DeclarationFactory);
OperationalMapDeclarationsModule.factory('DeclarationFile', DeclarationFileFactory);
