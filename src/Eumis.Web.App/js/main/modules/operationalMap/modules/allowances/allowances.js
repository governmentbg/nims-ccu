import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import allowanceTemplateUrl from './forms/allowance.html';
import allowanceRateTemplateUrl from './forms/allowanceRate.html';
import { AllowanceRateCtrl } from './forms/allowanceRateCtrl';
import editAllowanceRateModalTemplateUrl from './modals/editAllowanceRateModal.html';
import { EditAllowanceRateModalCtrl } from './modals/editAllowanceRateModalCtrl';
import newAllowanceRateModalTemplateUrl from './modals/newAllowanceRateModal.html';
import { NewAllowanceRateModalCtrl } from './modals/newAllowanceRateModalCtrl';
import { AllowanceFactory } from './resources/allowance';
import { AllowanceRateFactory } from './resources/allowanceRate';
import allowancesEditTemplateUrl from './views/allowancesEdit.html';
import { AllowancesEditCtrl } from './views/allowancesEditCtrl';
import allowancesNewTemplateUrl from './views/allowancesNew.html';
import { AllowancesNewCtrl } from './views/allowancesNewCtrl';
import allowancesSearchTemplateUrl from './views/allowancesSearch.html';
import { AllowancesSearchCtrl } from './views/allowancesSearchCtrl';

const OperationalMapAllowancesModule = angular
  .module('main.operationalMap.allowances', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisAllowanceData',
        templateUrl: allowanceTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisAllowanceRate',
        templateUrl: allowanceRateTemplateUrl,
        controller: AllowanceRateCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('newAllowanceRateModal'                  , newAllowanceRateModalTemplateUrl               , NewAllowanceRateModalCtrl                  , 'md' )
    .modal('editAllowanceRateModal'                 , editAllowanceRateModalTemplateUrl              , EditAllowanceRateModalCtrl                 , 'md' );
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map.allowances'                                     , '/allowances'                                                                                                                                                                                              ])
    .state(['root.map.allowances.search'                              , ''                 ,       ['@root'                           , allowancesSearchTemplateUrl                                , AllowancesSearchCtrl                   ]])
    .state(['root.map.allowances.new'                                 , '/new?rf'          ,       ['@root'                           , allowancesNewTemplateUrl                                   , AllowancesNewCtrl                      ]])
    .state(['root.map.allowances.edit'                                , '/:id?rf'          ,       ['@root'                           , allowancesEditTemplateUrl                                  , AllowancesEditCtrl                     ]]);
    }
  ]);

export default OperationalMapAllowancesModule.name;
OperationalMapAllowancesModule.factory('Allowance', AllowanceFactory);
OperationalMapAllowancesModule.factory('AllowanceRate', AllowanceRateFactory);
