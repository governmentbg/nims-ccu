import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import programmeDataTemplateUrl from './forms/programmeData.html';
import { ProgrammeDataCtrl } from './forms/programmeDataCtrl';
import programmeInstitutionTemplateUrl from './forms/programmeInstitution.html';
import { ProgrammeInstitutionCtrl } from './forms/programmeInstitutionCtrl';
import programmePriorityBudgetTemplateUrl from './forms/programmePriorityBudget.html';
import { ProgrammePriorityBudgetCtrl } from './forms/programmePriorityBudgetCtrl';
import programmePriorityDataTemplateUrl from './forms/programmePriorityData.html';
import { ProgrammePriorityDataCtrl } from './forms/programmePriorityDataCtrl';
import programmeApplicationDocumentTemplateUrl from './forms/programmeApplicationDocument.html';
import { ProgrammeApplicationDocumentCtrl } from './forms/programmeApplicationDocumentCtrl';
import programmeDirectionTemplateUrl from './forms/programmeDirection.html';
import programmeDeclarationTemplateUrl from './forms/programmeDeclaration.html';
import programmeDeclarationItemTemplateUrl from './forms/programmeDeclarationItem.html';
import { ProgrammeDeclarationCtrl } from './forms/programmeDeclarationCtrl';
import { ProgrammeFactory } from './resources/programme';
import { ProgrammeBudgetFactory } from './resources/programmeBudget';
import { ProgrammeDocumentFactory } from './resources/programmeDocument';
import { ProgrammeApplicationDocumentFactory } from './resources/programmeApplicationDocument';
import { ProgrammeFileFactory } from './resources/programmeFile';
import { ProgrammePriorityFactory } from './resources/programmePriority';
import { ProgrammePriorityBudgetFactory } from './resources/programmePriorityBudget';
import { ProgrammePriorityDocumentFactory } from './resources/programmePriorityDocument';
import { ProgrammePriorityFileFactory } from './resources/programmePriorityFile';
import { ProgrammeDirectionFactory } from './resources/programmeDirection';
import { ProgrammePriorityDirectionFactory } from './resources/programmePriorityDirection';
import { ProgrammeDeclarationFactory } from './resources/programmeDeclaration';
import { ProgrammeDeclarationItemFactory } from './resources/programmeDeclarationItem';
import pPrioritiesEditTemplateUrl from './views/pPrioritiesEdit.html';
import { PPrioritiesEditCtrl } from './views/pPrioritiesEditCtrl';
import pPrioritiesNewTemplateUrl from './views/pPrioritiesNew.html';
import { PPrioritiesNewCtrl } from './views/pPrioritiesNewCtrl';
import pPrioritiesViewTemplateUrl from './views/pPrioritiesView.html';
import { PPrioritiesViewCtrl } from './views/pPrioritiesViewCtrl';
import programmePriorityDirectionsEditTemplateUrl from './views/pPriorityDirectionsEdit.html';
import { ProgrammePriorityDirectionsEditCtrl } from './views/pPriorityDirectionsEditCtrl';
import programmePriorityDirectionsNewTemplateUrl from './views/pPriorityDirectionsNew.html';
import { ProgrammePriorityDirectionsNewCtrl } from './views/pPriorityDirectionsNewCtrl';
import programmePriorityDirectionsSearchTemplateUrl from './views/pPriorityDirectionsSearch.html';
import { ProgrammePriorityDirectionsSearchCtrl } from './views/pPriorityDirectionsSearchCtrl';
import pPriorityBudgetsEditTemplateUrl from './views/pPriorityBudgetsEdit.html';
import { PPriorityBudgetsEditCtrl } from './views/pPriorityBudgetsEditCtrl';
import pPriorityBudgetsNewTemplateUrl from './views/pPriorityBudgetsNew.html';
import { PPriorityBudgetsNewCtrl } from './views/pPriorityBudgetsNewCtrl';
import pPriorityBudgetsSearchTemplateUrl from './views/pPriorityBudgetsSearch.html';
import { PPriorityBudgetsSearchCtrl } from './views/pPriorityBudgetsSearchCtrl';
import pPriorityDocumentsEditTemplateUrl from './views/pPriorityDocumentsEdit.html';
import { PPriorityDocumentsEditCtrl } from './views/pPriorityDocumentsEditCtrl';
import pPriorityDocumentsNewTemplateUrl from './views/pPriorityDocumentsNew.html';
import { PPriorityDocumentsNewCtrl } from './views/pPriorityDocumentsNewCtrl';
import pPriorityDocumentsSearchTemplateUrl from './views/pPriorityDocumentsSearch.html';
import { PPriorityDocumentsSearchCtrl } from './views/pPriorityDocumentsSearchCtrl';
import programmeDirectionsEditTemplateUrl from './views/programmeDirectionsEdit.html';
import { ProgrammeDirectionsEditCtrl } from './views/programmeDirectionsEditCtrl';
import programmeDirectionsNewTemplateUrl from './views/programmeDirectionsNew.html';
import { ProgrammeDirectionsNewCtrl } from './views/programmeDirectionsNewCtrl';
import programmeDirectionsSearchTemplateUrl from './views/programmeDirectionsSearch.html';
import { ProgrammeDirectionsSearchCtrl } from './views/programmeDirectionsSearchCtrl';
import programmeBudgetsSearchTemplateUrl from './views/programmeBudgetsSearch.html';
import { ProgrammeBudgetsSearchCtrl } from './views/programmeBudgetsSearchCtrl';
import programmeDocumentsEditTemplateUrl from './views/programmeDocumentsEdit.html';
import { ProgrammeDocumentsEditCtrl } from './views/programmeDocumentsEditCtrl';
import programmeDocumentsNewTemplateUrl from './views/programmeDocumentsNew.html';
import { ProgrammeDocumentsNewCtrl } from './views/programmeDocumentsNewCtrl';
import programmeApplicationDocumentsEditTemplateUrl from './views/programmeApplicationDocumentsEdit.html';
import { ProgrammeApplicationDocumentsEditCtrl } from './views/programmeApplicationDocumentsEditCtrl';
import programmeApplicationDocumentsNewTemplateUrl from './views/programmeApplicationDocumentsNew.html';
import { ProgrammeApplicationDocumentsNewCtrl } from './views/programmeApplicationDocumentsNewCtrl';
import programmeApplicationDocumentsLoadTemplateUrl from './views/programmeApplicationDocumentsLoad.html';
import { ProgrammeApplicationDocumentsLoadCtrl } from './views/programmeApplicationDocumentsLoadCtrl';
import programmeDocumentsSearchTemplateUrl from './views/programmeDocumentsSearch.html';
import { ProgrammeDocumentsSearchCtrl } from './views/programmeDocumentsSearchCtrl';
import programmeInterventionCategoriesSearchTemplateUrl from './views/programmeInterventionCategoriesSearch.html';
import { PInterventionCategoriesSearchCtrl } from './views/programmeInterventionCategoriesSearchCtrl';
import programmeProgrammePrioritiesSearchTemplateUrl from './views/programmeProgrammePrioritiesSearch.html';
import { ProgrammeProgrammePrioritiesSearchCtrl } from './views/programmeProgrammePrioritiesSearchCtrl';
import programmeDeclarationsEditTemplateUrl from './views/programmeDeclarationsEdit.html';
import { ProgrammeDeclarationsEditCtrl } from './views/programmeDeclarationsEditCtrl';
import programmeDeclarationsNewTemplateUrl from './views/programmeDeclarationsNew.html';
import { ProgrammeDeclarationsNewCtrl } from './views/programmeDeclarationsNewCtrl';
import programmeDeclarationItemsEditTemplateUrl from './views/programmeDeclarationItemsEdit.html';
import { ProgrammeDeclarationItemsEditCtrl } from './views/programmeDeclarationItemsEditCtrl';
import programmeDeclarationItemsNewTemplateUrl from './views/programmeDeclarationItemsNew.html';
import { ProgrammeDeclarationItemsNewCtrl } from './views/programmeDeclarationItemsNewCtrl';
import programmesEditTemplateUrl from './views/programmesEdit.html';
import { ProgrammesEditCtrl } from './views/programmesEditCtrl';
import programmesNewTemplateUrl from './views/programmesNew.html';
import { ProgrammesNewCtrl } from './views/programmesNewCtrl';
import programmesViewTemplateUrl from './views/programmesView.html';
import { ProgrammesViewCtrl } from './views/programmesViewCtrl';
import loadProgrammeDeclarationItemsModalTemplateUrl from './modals/loadProgrammeDeclarationItemsModal.html';
import { LoadProgrammeDeclarationItemsModalCtrl } from './modals/loadProgrammeDeclarationItemsModalCtrl';

const OperationalMapNodesModule = angular
  .module('main.operationalMap.nodes', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisProgrammeData',
        templateUrl: programmeDataTemplateUrl,
        controller: ProgrammeDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProgrammeInstitution',
        templateUrl: programmeInstitutionTemplateUrl,
        controller: ProgrammeInstitutionCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProgrammePriorityData',
        templateUrl: programmePriorityDataTemplateUrl,
        controller: ProgrammePriorityDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProgrammePriorityBudget',
        templateUrl: programmePriorityBudgetTemplateUrl,
        controller: ProgrammePriorityBudgetCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProgrammeApplicationDocument',
        templateUrl: programmeApplicationDocumentTemplateUrl,
        controller: ProgrammeApplicationDocumentCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProgrammeDirection',
        templateUrl: programmeDirectionTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisProgrammeDeclaration',
        templateUrl: programmeDeclarationTemplateUrl,
        controller: ProgrammeDeclarationCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProgrammeDeclarationItem',
        templateUrl: programmeDeclarationItemTemplateUrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
        .modal('loadProgrammeDeclarationItemsModal', loadProgrammeDeclarationItemsModalTemplateUrl, LoadProgrammeDeclarationItemsModalCtrl, 'md')
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map.programmes'                                     , '/programmes'                                                                                                                                                                                              ])
    .state(['root.map.programmes.new'                                 , '/new'             ,       ['@root'                           , programmesNewTemplateUrl                                   , ProgrammesNewCtrl                      ]])
    .state(['root.map.programmes.view'                                , '/:id?rf'          , true, ['@root'                           , programmesViewTemplateUrl                                  , ProgrammesViewCtrl                     ]])
    .state(['root.map.programmes.view.edit'                           , ''                 ,       ['@root.map.programmes.view'       , programmesEditTemplateUrl                                  , ProgrammesEditCtrl                     ]])

    .state(['root.map.programmes.view.budgets'                        , '/budgets'                                                                                                                                                                                                 ])
    .state(['root.map.programmes.view.budgets.search'                 , ''                 ,       ['@root.map.programmes.view'       , programmeBudgetsSearchTemplateUrl                          , ProgrammeBudgetsSearchCtrl             ]])

    .state(['root.map.programmes.view.icategories'                    , '/icategories'                                                                                                                                                                                             ])
    .state(['root.map.programmes.view.icategories.search'             , ''                 ,       ['@root.map.programmes.view'       , programmeInterventionCategoriesSearchTemplateUrl           , PInterventionCategoriesSearchCtrl      ]])

    .state(['root.map.programmes.view.documents'                      , '/documents'                                                                                                                                                                                               ])
    .state(['root.map.programmes.view.documents.search'               , ''                 ,       ['@root.map.programmes.view'       , programmeDocumentsSearchTemplateUrl                        , ProgrammeDocumentsSearchCtrl           ]])
    .state(['root.map.programmes.view.documents.new'                  , '/new'             ,       ['@root.map.programmes.view'       , programmeDocumentsNewTemplateUrl                           , ProgrammeDocumentsNewCtrl              ]])
    .state(['root.map.programmes.view.documents.edit'                 , '/:ind'            ,       ['@root.map.programmes.view'       , programmeDocumentsEditTemplateUrl                          , ProgrammeDocumentsEditCtrl             ]])

    .state(['root.map.programmes.view.applicationDocuments'           , '/appDocuments'                                                                                                                                                                                            ])
    .state(['root.map.programmes.view.applicationDocuments.new'       , '/new'             ,       ['@root.map.programmes.view'       , programmeApplicationDocumentsNewTemplateUrl                , ProgrammeApplicationDocumentsNewCtrl   ]])
    .state(['root.map.programmes.view.applicationDocuments.load'      , '/load'             ,      ['@root.map.programmes.view'       , programmeApplicationDocumentsLoadTemplateUrl               , ProgrammeApplicationDocumentsLoadCtrl  ]])
    .state(['root.map.programmes.view.applicationDocuments.edit'      , '/:ind'            ,       ['@root.map.programmes.view'       , programmeApplicationDocumentsEditTemplateUrl               , ProgrammeApplicationDocumentsEditCtrl  ]])

    .state(['root.map.programmes.view.declarations'                   , '/declarations'                                                                                                                                                                                            ])
    .state(['root.map.programmes.view.declarations.new'               , '/new'             ,       ['@root.map.programmes.view'       , programmeDeclarationsNewTemplateUrl                        , ProgrammeDeclarationsNewCtrl           ]])
    .state(['root.map.programmes.view.declarations.edit'              , '/:ind'            ,       ['@root.map.programmes.view'       , programmeDeclarationsEditTemplateUrl                       , ProgrammeDeclarationsEditCtrl          ]])
    .state(['root.map.programmes.view.declarations.edit.items'        , '/items'                                                                                                                                                                                                   ])
    .state(['root.map.programmes.view.declarations.edit.items.new'    , '/new'             ,       ['@root.map.programmes.view'       , programmeDeclarationItemsNewTemplateUrl                     , ProgrammeDeclarationItemsNewCtrl      ]])
    .state(['root.map.programmes.view.declarations.edit.items.edit'   , '/:did'            ,       ['@root.map.programmes.view'       , programmeDeclarationItemsEditTemplateUrl                    , ProgrammeDeclarationItemsEditCtrl     ]])

    .state(['root.map.programmes.view.programmePriorities'            , '/programmePriorities'                                                                                                                                                                                     ])
    .state(['root.map.programmes.view.programmePriorities.search'     , ''                 ,       ['@root.map.programmes.view'       , programmeProgrammePrioritiesSearchTemplateUrl              , ProgrammeProgrammePrioritiesSearchCtrl ]])
    .state(['root.map.programmes.view.programmePriorities.new'        , '/new'             ,       ['@root.map.programmes.view'       , pPrioritiesNewTemplateUrl                                  , PPrioritiesNewCtrl                     ]])

    .state(['root.map.programmes.view.directions'                    , '/directions'                                                                                                                                                                                              ])
    .state(['root.map.programmes.view.directions.search'             , ''                 ,       ['@root.map.programmes.view'        , programmeDirectionsSearchTemplateUrl                       , ProgrammeDirectionsSearchCtrl          ]])
    .state(['root.map.programmes.view.directions.new'                , '/new?type'        ,       ['@root.map.programmes.view'        , programmeDirectionsNewTemplateUrl                          , ProgrammeDirectionsNewCtrl             ]])
    .state(['root.map.programmes.view.directions.edit'               , '/:ind?type'       ,       ['@root.map.programmes.view'        , programmeDirectionsEditTemplateUrl                         , ProgrammeDirectionsEditCtrl            ]])

    .state(['root.map.ppriorities'                                    , '/ppriorities'                                                                                                                                                                                             ])
    .state(['root.map.ppriorities.view'                               , '/:id?rf'          , true, ['@root'                           , pPrioritiesViewTemplateUrl                        , PPrioritiesViewCtrl                    ]])
    .state(['root.map.ppriorities.view.edit'                          , ''                 ,       ['@root.map.ppriorities.view'      , pPrioritiesEditTemplateUrl                        , PPrioritiesEditCtrl                    ]])

    .state(['root.map.ppriorities.view.directions'                    , '/directions'                                                                                                                                                                                              ])
    .state(['root.map.ppriorities.view.directions.search'             , ''                 ,       ['@root.map.ppriorities.view'      , programmePriorityDirectionsSearchTemplateUrl      , ProgrammePriorityDirectionsSearchCtrl  ]])
    .state(['root.map.ppriorities.view.directions.new'                , '/new'             ,       ['@root.map.ppriorities.view'      , programmePriorityDirectionsNewTemplateUrl         , ProgrammePriorityDirectionsNewCtrl     ]])
    .state(['root.map.ppriorities.view.directions.edit'               , '/:ind'            ,       ['@root.map.ppriorities.view'      , programmePriorityDirectionsEditTemplateUrl        , ProgrammePriorityDirectionsEditCtrl    ]])

    .state(['root.map.ppriorities.view.budgets'                       , '/budgets'                                                                                                                                                                                                 ])
    .state(['root.map.ppriorities.view.budgets.search'                , ''                 ,       ['@root.map.ppriorities.view'      , pPriorityBudgetsSearchTemplateUrl                 , PPriorityBudgetsSearchCtrl             ]])
    .state(['root.map.ppriorities.view.budgets.new'                   , '/new'             ,       ['@root.map.ppriorities.view'      , pPriorityBudgetsNewTemplateUrl                    , PPriorityBudgetsNewCtrl                ]])
    .state(['root.map.ppriorities.view.budgets.edit'                  , '/:ind'            ,       ['@root.map.ppriorities.view'      , pPriorityBudgetsEditTemplateUrl                   , PPriorityBudgetsEditCtrl               ]])

    .state(['root.map.ppriorities.view.documents'                     , '/documents'                                                                                                                                                                                               ])
    .state(['root.map.ppriorities.view.documents.search'              , ''                 ,       ['@root.map.ppriorities.view'      , pPriorityDocumentsSearchTemplateUrl               , PPriorityDocumentsSearchCtrl           ]])
    .state(['root.map.ppriorities.view.documents.new'                 , '/new'             ,       ['@root.map.ppriorities.view'      , pPriorityDocumentsNewTemplateUrl                  , PPriorityDocumentsNewCtrl              ]])
    .state(['root.map.ppriorities.view.documents.edit'                , '/:ind'            ,       ['@root.map.ppriorities.view'      , pPriorityDocumentsEditTemplateUrl                 , PPriorityDocumentsEditCtrl             ]])
    }
  ]);

export default OperationalMapNodesModule.name;
OperationalMapNodesModule.factory('Programme', ProgrammeFactory);
OperationalMapNodesModule.factory('ProgrammeBudget', ProgrammeBudgetFactory);
OperationalMapNodesModule.factory('ProgrammeDocument', ProgrammeDocumentFactory);
OperationalMapNodesModule.factory(
  'ProgrammeApplicationDocument',
  ProgrammeApplicationDocumentFactory
);
OperationalMapNodesModule.factory('ProgrammeFile', ProgrammeFileFactory);
OperationalMapNodesModule.factory('ProgrammePriority', ProgrammePriorityFactory);
OperationalMapNodesModule.factory('ProgrammePriorityBudget', ProgrammePriorityBudgetFactory);
OperationalMapNodesModule.factory('ProgrammePriorityDocument', ProgrammePriorityDocumentFactory);
OperationalMapNodesModule.factory('ProgrammePriorityFile', ProgrammePriorityFileFactory);

OperationalMapNodesModule.factory('ProgrammeDirection', ProgrammeDirectionFactory);
OperationalMapNodesModule.factory('ProgrammePriorityDirection', ProgrammePriorityDirectionFactory);
OperationalMapNodesModule.factory('ProgrammeDeclaration', ProgrammeDeclarationFactory);
OperationalMapNodesModule.factory('ProgrammeDeclarationItem', ProgrammeDeclarationItemFactory);
