import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import basicInterestRateTemplateUrl from './forms/basicInterestRate.html';
import interestRateTemplateUrl from './forms/interestRate.html';
import { InterestRateCtrl } from './forms/interestRateCtrl';
import editInterestRateModalTemplateUrl from './modals/editInterestRateModal.html';
import { EditInterestRateModalCtrl } from './modals/editInterestRateModalCtrl';
import newInterestRateModalTemplateUrl from './modals/newInterestRateModal.html';
import { NewInterestRateModalCtrl } from './modals/newInterestRateModalCtrl';
import { BasicInterestRateFactory } from './resources/basicInterestRate';
import { InterestRateFactory } from './resources/interestRate';
import basicInterestRatesEditTemplateUrl from './views/basicInterestRatesEdit.html';
import { BasicInterestRatesEditCtrl } from './views/basicInterestRatesEditCtrl';
import basicInterestRatesNewTemplateUrl from './views/basicInterestRatesNew.html';
import { BasicInterestRatesNewCtrl } from './views/basicInterestRatesNewCtrl';
import basicInterestRatesSearchTemplateUrl from './views/basicInterestRatesSearch.html';
import { BasicInterestRatesSearchCtrl } from './views/basicInterestRatesSearchCtrl';

const OperationalMapBasicInterestRatesModule = angular
  .module('main.operationalMap.basicInterestRates', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisBasicInterestRateData',
        templateUrl: basicInterestRateTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisInterestRate',
        templateUrl: interestRateTemplateUrl,
        controller: InterestRateCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('newInterestRateModal'                   , newInterestRateModalTemplateUrl        , NewInterestRateModalCtrl                   , 'md' )
    .modal('editInterestRateModal'                  , editInterestRateModalTemplateUrl       , EditInterestRateModalCtrl                  , 'md' );
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map.basicInterestRates'                             , '/basicInterestRates'                                                                                                                                                                                      ])
    .state(['root.map.basicInterestRates.search'                      , ''                 ,       ['@root'                           , basicInterestRatesSearchTemplateUrl                , BasicInterestRatesSearchCtrl           ]])
    .state(['root.map.basicInterestRates.new'                         , '/new?rf'          ,       ['@root'                           , basicInterestRatesNewTemplateUrl                   , BasicInterestRatesNewCtrl              ]])
    .state(['root.map.basicInterestRates.edit'                        , '/:id?rf'          ,       ['@root'                           , basicInterestRatesEditTemplateUrl                  , BasicInterestRatesEditCtrl             ]]);
    }
  ]);

export default OperationalMapBasicInterestRatesModule.name;
OperationalMapBasicInterestRatesModule.factory('BasicInterestRate', BasicInterestRateFactory);
OperationalMapBasicInterestRatesModule.factory('InterestRate', InterestRateFactory);
