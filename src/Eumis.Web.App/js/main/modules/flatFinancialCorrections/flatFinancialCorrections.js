import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import flatFinancialCorrectionTemplateUrl from './forms/flatFinancialCorrection.html';
import { FlatFinancialCorrectionCtrl } from './forms/flatFinancialCorrectionCtrl';
import flatFinancialCorrectionInfoTemplateUrl from './forms/flatFinancialCorrectionInfo.html';
import chooseContractContractItemsModalTemplateUrl from './modals/chooseContractContractItemsModal.html';
import { ChooseContractContractItemsModalCtrl } from './modals/chooseContractContractItemsModalCtrl';
import chooseContractItemsModalTemplateUrl from './modals/chooseContractItemsModal.html';
import { ChooseContractItemsModalCtrl } from './modals/chooseContractItemsModalCtrl';
import chooseProcedureItemsModalTemplateUrl from './modals/chooseProcedureItemsModal.html';
import { ChooseProcedureItemsModalCtrl } from './modals/chooseProcedureItemsModalCtrl';
import chooseProgrammePriorityItemsModalTemplateUrl from './modals/chooseProgrammePriorityItemsModal.html';
import { ChooseProgrammePriorityItemsModalCtrl } from './modals/chooseProgrammePriorityItemsModalCtrl';
import editFlatFinancialCorrectionItemModalTemplateUrl from './modals/editFlatFinancialCorrectionItemModal.html';
import { EditFlatFinancialCorrectionItemModalCtrl } from './modals/editFlatFinancialCorrectionItemModalCtrl';
import { FlatFinancialCorrectionFactory } from './resources/flatFinancialCorrection';
import { FlatFinancialCorrectionContractContractItemFactory } from './resources/flatFinancialCorrectionContractContractItem';
import { FlatFinancialCorrectionContractItemFactory } from './resources/flatFinancialCorrectionContractItem';
import { FlatFinancialCorrectionFileFactory } from './resources/flatFinancialCorrectionFile';
import { FlatFinancialCorrectionProcedureItemFactory } from './resources/flatFinancialCorrectionProcedureItem';
import { FlatFinancialCorrectionProgrammeItemFactory } from './resources/flatFinancialCorrectionProgrammeItem';
import { FlatFinancialCorrectionProgrammePriorityItemFactory } from './resources/flatFinancialCorrectionProgrammePriorityItem';
import flatFinancialCorrectionItemsTemplateUrl from './views/flatFinancialCorrectionItems.html';
import { FlatFinancialCorrectionItemsCtrl } from './views/flatFinancialCorrectionItemsCtrl';
import flatFinancialCorrectionProgrammeItemTemplateUrl from './views/flatFinancialCorrectionProgrammeItem.html';
import { FlatFinancialCorrectionProgrammeItemCtrl } from './views/flatFinancialCorrectionProgrammeItemCtrl';
import flatFinancialCorrectionsEditTemplateUrl from './views/flatFinancialCorrectionsEdit.html';
import { FlatFinancialCorrectionsEditCtrl } from './views/flatFinancialCorrectionsEditCtrl';
import flatFinancialCorrectionsNewTemplateUrl from './views/flatFinancialCorrectionsNew.html';
import { FlatFinancialCorrectionsNewCtrl } from './views/flatFinancialCorrectionsNewCtrl';
import flatFinancialCorrectionsSearchTemplateUrl from './views/flatFinancialCorrectionsSearch.html';
import { FlatFinancialCorrectionsSearchCtrl } from './views/flatFinancialCorrectionsSearchCtrl';
import flatFinancialCorrectionsViewTemplateUrl from './views/flatFinancialCorrectionsView.html';
import { FlatFinancialCorrectionsViewCtrl } from './views/flatFinancialCorrectionsViewCtrl';

const FlatFinancialCorrectionsModule = angular
  .module('main.flatFinancialCorrections', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisFlatFinancialCorrection',
        templateUrl: flatFinancialCorrectionTemplateUrl,
        controller: FlatFinancialCorrectionCtrl
      });
      //flatFinancialCorrectionInfo
      scaffoldingProvider.form({
        name: 'eumisFlatFinancialCorrectionInfo',
        templateUrl: flatFinancialCorrectionInfoTemplateUrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('editFlatFinancialCorrectionItemModal'   , editFlatFinancialCorrectionItemModalTemplateUrl , EditFlatFinancialCorrectionItemModalCtrl   , 'md' )
    .modal('chooseProgrammePriorityItemsModal'      , chooseProgrammePriorityItemsModalTemplateUrl    , ChooseProgrammePriorityItemsModalCtrl      , 'xlg')
    .modal('chooseProcedureItemsModal'              , chooseProcedureItemsModalTemplateUrl            , ChooseProcedureItemsModalCtrl              , 'xlg')
    .modal('chooseContractItemsModal'               , chooseContractItemsModalTemplateUrl             , ChooseContractItemsModalCtrl               , 'xlg')
    .modal('chooseContractContractItemsModal'       , chooseContractContractItemsModalTemplateUrl     , ChooseContractContractItemsModalCtrl       , 'xlg');
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.flatFinancialCorrections'                           , '/flatFinancialCorrections'                                                                                                                                                                                         ])
    .state(['root.flatFinancialCorrections.search'                    , ''                 ,       ['@root'                           , flatFinancialCorrectionsSearchTemplateUrl                   , FlatFinancialCorrectionsSearchCtrl              ]])
    .state(['root.flatFinancialCorrections.new'                       , '/new?d'           ,       ['@root'                           , flatFinancialCorrectionsNewTemplateUrl                      , FlatFinancialCorrectionsNewCtrl                 ]])
    .state(['root.flatFinancialCorrections.view'                      , '/:id?rf'          , true, ['@root'                           , flatFinancialCorrectionsViewTemplateUrl                     , FlatFinancialCorrectionsViewCtrl                ]])
    .state(['root.flatFinancialCorrections.view.edit'                 , ''                 ,       ['@root.flatFinancialCorrections.view' , flatFinancialCorrectionsEditTemplateUrl                 , FlatFinancialCorrectionsEditCtrl                ]])
    .state(['root.flatFinancialCorrections.view.items'                , '/items'           ,       ['@root.flatFinancialCorrections.view' , flatFinancialCorrectionItemsTemplateUrl                 , FlatFinancialCorrectionItemsCtrl                ]])
    .state(['root.flatFinancialCorrections.view.programmeItem'        , '/programmeItem'   ,       ['@root.flatFinancialCorrections.view' , flatFinancialCorrectionProgrammeItemTemplateUrl         , FlatFinancialCorrectionProgrammeItemCtrl        ]]);
    }
  ]);

export default FlatFinancialCorrectionsModule.name;
FlatFinancialCorrectionsModule.factory('FlatFinancialCorrection', FlatFinancialCorrectionFactory);
FlatFinancialCorrectionsModule.factory(
  'FlatFinancialCorrectionContractContractItem',
  FlatFinancialCorrectionContractContractItemFactory
);
FlatFinancialCorrectionsModule.factory(
  'FlatFinancialCorrectionContractItem',
  FlatFinancialCorrectionContractItemFactory
);
FlatFinancialCorrectionsModule.factory(
  'FlatFinancialCorrectionFile',
  FlatFinancialCorrectionFileFactory
);
FlatFinancialCorrectionsModule.factory(
  'FlatFinancialCorrectionProcedureItem',
  FlatFinancialCorrectionProcedureItemFactory
);
FlatFinancialCorrectionsModule.factory(
  'FlatFinancialCorrectionProgrammeItem',
  FlatFinancialCorrectionProgrammeItemFactory
);
FlatFinancialCorrectionsModule.factory(
  'FlatFinancialCorrectionProgrammePriorityItem',
  FlatFinancialCorrectionProgrammePriorityItemFactory
);
