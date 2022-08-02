import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import expenseSubTypeTemplateUrl from './forms/expenseSubType.html';
import expenseTypeTemplateUrl from './forms/expenseType.html';
import editExpenseSubTypeModalTemplateUrl from './modals/editExpenseSubTypeModal.html';
import { EditExpenseSubTypeModalCtrl } from './modals/editExpenseSubTypeModalCtrl';
import newExpenseSubTypeModalTemplateUrl from './modals/newExpenseSubTypeModal.html';
import { NewExpenseSubTypeModalCtrl } from './modals/newExpenseSubTypeModalCtrl';
import { ExpenseSubTypeFactory } from './resources/expenseSubType';
import { ExpenseTypeFactory } from './resources/expenseType';
import expenseTypesEditTemplateUrl from './views/expenseTypesEdit.html';
import { ExpenseTypesEditCtrl } from './views/expenseTypesEditCtrl';
import expenseTypesNewTemplateUrl from './views/expenseTypesNew.html';
import { ExpenseTypesNewCtrl } from './views/expenseTypesNewCtrl';
import expenseTypesSearchTemplateUrl from './views/expenseTypesSearch.html';
import { ExpenseTypesSearchCtrl } from './views/expenseTypesSearchCtrl';

const OperationalMapExpenseTypesModule = angular
  .module('main.operationalMap.expenseTypes', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisExpenseSubTypeData',
        templateUrl: expenseSubTypeTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisExpenseTypeData',
        templateUrl: expenseTypeTemplateUrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('newExpenseSubTypeModal'                 , newExpenseSubTypeModalTemplateUrl            , NewExpenseSubTypeModalCtrl                 , 'md' )
    .modal('editExpenseSubTypeModal'                , editExpenseSubTypeModalTemplateUrl           , EditExpenseSubTypeModalCtrl                , 'md' );
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map.expenseTypes'                                   , '/expenseTypes'                                                                                                                                                                                            ])
    .state(['root.map.expenseTypes.search'                            , ''                 ,       ['@root'                           , expenseTypesSearchTemplateUrl                            , ExpenseTypesSearchCtrl                 ]])
    .state(['root.map.expenseTypes.new'                               , '/new?rf'          ,       ['@root'                           , expenseTypesNewTemplateUrl                               , ExpenseTypesNewCtrl                    ]])
    .state(['root.map.expenseTypes.edit'                              , '/:id?rf'          ,       ['@root'                           , expenseTypesEditTemplateUrl                              , ExpenseTypesEditCtrl                   ]]);
    }
  ]);

export default OperationalMapExpenseTypesModule.name;
OperationalMapExpenseTypesModule.factory('ExpenseSubType', ExpenseSubTypeFactory);
OperationalMapExpenseTypesModule.factory('ExpenseType', ExpenseTypeFactory);
