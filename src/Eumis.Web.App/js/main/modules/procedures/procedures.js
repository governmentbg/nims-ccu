import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import ContractReportDocumentsModule from './modules/contractReportDocuments/contractReportDocuments.js';
import ProcedureMassCommunicationsModule from './modules/procedureMassCommunications/procedureMassCommunications';
import procedureAppDocumentTemplateUrl from './forms/procedureAppDocument.html';
import { ProcedureAppDocumentCtrl } from './forms/procedureAppDocumentCtrl';
import procedureDataTemplateUrl from './forms/procedureData.html';
import { ProcedureDataCtrl } from './forms/procedureDataCtrl';
import procedureEvalTableTemplateUrl from './forms/procedureEvalTable.html';
import { ProcedureEvalTableCtrl } from './forms/procedureEvalTableCtrl';
import procedureQuestionTemplateUrl from './forms/procedureQuestion.html';
import procedureShareDataTemplateUrl from './forms/procedureShareData.html';
import { ProcedureShareDataCtrl } from './forms/procedureShareDataCtrl';
import procedureSpecFieldDataTemplateUrl from './forms/procedureSpecFieldData.html';
import procedureTimeLimitTemplateUrl from './forms/procedureTimeLimit.html';
import { ProcedureTimeLimitCtrl } from './forms/procedureTimeLimitCtrl';
import procedureLocationTemplateUrl from './forms/procedureLocation.html';
import { ProcedureLocationCtrl } from './forms/procedureLocationCtrl';
import procedureMonitorstatRequestTemplateUrl from './forms/procedureMonitorstatRequest.html';
import procedureDirectionTemplateUrl from './forms/procedureDirection.html';
import procedureDeclarationTemplateUrl from './forms/procedureDeclaration.html';
import { ProcedureDeclarationCtrl } from './forms/procedureDeclarationCtrl';
import budgetLevel1ModalTemplateUrl from './modals/budgetLevel1Modal.html';
import { BudgetLevel1ModalCtrl } from './modals/budgetLevel1ModalCtrl';
import budgetLevel2ModalTemplateUrl from './modals/budgetLevel2Modal.html';
import { BudgetLevel2ModalCtrl } from './modals/budgetLevel2ModalCtrl';
import budgetLevel3ModalTemplateUrl from './modals/budgetLevel3Modal.html';
import { BudgetLevel3ModalCtrl } from './modals/budgetLevel3ModalCtrl';
import budgetValidationRuleModalTemplateUrl from './modals/budgetValidationRuleModal.html';
import { BudgetValidationRuleModalCtrl } from './modals/budgetValidationRuleModalCtrl';
import chooseProcedureModalTemplateUrl from './modals/chooseProcedureModal.html';
import { ChooseProcedureModalCtrl } from './modals/chooseProcedureModalCtrl';
import chooseDirectionModalTemplateUrl from './modals/chooseDirectionModal.html';
import { ChooseDirectionModalCtrl } from './modals/chooseDirectionModalCtrl';
import chooseMonitorstatReportsModalTemplateUrl from './modals/chooseMonitorstatReportsModal.html';
import { ChooseMonitorstatReportsModalCtrl } from './modals/chooseMonitorstatReportsModalCtrl';
import { ProcedureFactory } from './resources/procedure';
import { ProcedureAppDocumentFactory } from './resources/procedureApplicationDocument';
import { ProcedureAppGuidelineFactory } from './resources/procedureApplicationGuideline';
import { ProcedureDocumentFactory } from './resources/procedureDocument';
import { ProcedureEvalTableFactory } from './resources/procedureEvalTable';
import { ProcedureFileFactory } from './resources/procedureFile';
import { ProcedureIndicatorFactory } from './resources/procedureIndicator';
import { ProcedureQuestionFactory } from './resources/procedureQuestion';
import { ProcedureShareFactory } from './resources/procedureShare';
import { ProcedureLocationFactory } from './resources/procedureLocation';
import { ProcedureShareExpenseBudgetFactory } from './resources/procedureShareExpenseBudget';
import { ProcedureSpecFieldFactory } from './resources/procedureSpecField';
import { ProcedureTimeLimitFactory } from './resources/procedureTimeLimit';
import { ProcedureApplicationSectionFactory } from './resources/procedureApplicationSection';
import { ProcedureDirectionFactory } from './resources/procedureDirection';
import { ProcedureDeclarationFactory } from './resources/procedureDeclaration';
import procedureAllDocumentsSearchTemplateUrl from './views/procedureAllDocumentsSearch.html';
import { ProcedureAllDocumentsSearchCtrl } from './views/procedureAllDocumentsSearchCtrl';
import procedureAppDocumentsEditTemplateUrl from './views/procedureAppDocumentsEdit.html';
import { ProcedureAppDocumentsEditCtrl } from './views/procedureAppDocumentsEditCtrl';
import procedureAppDocumentsNewTemplateUrl from './views/procedureAppDocumentsNew.html';
import { ProcedureAppDocumentsNewCtrl } from './views/procedureAppDocumentsNewCtrl';
import procedureAppGuidelinesEditTemplateUrl from './views/procedureAppGuidelinesEdit.html';
import { ProcedureAppGuidelinesEditCtrl } from './views/procedureAppGuidelinesEditCtrl';
import procedureAppGuidelinesNewTemplateUrl from './views/procedureAppGuidelinesNew.html';
import { ProcedureAppGuidelinesNewCtrl } from './views/procedureAppGuidelinesNewCtrl';
import procedureDocumentsEditTemplateUrl from './views/procedureDocumentsEdit.html';
import { ProcedureDocumentsEditCtrl } from './views/procedureDocumentsEditCtrl';
import procedureDocumentsNewTemplateUrl from './views/procedureDocumentsNew.html';
import { ProcedureDocumentsNewCtrl } from './views/procedureDocumentsNewCtrl';
import procedureEvalTablesEditTemplateUrl from './views/procedureEvalTablesEdit.html';
import { ProcedureEvalTablesEditCtrl } from './views/procedureEvalTablesEditCtrl';
import procedureEvalTablesNewTemplateUrl from './views/procedureEvalTablesNew.html';
import { ProcedureEvalTablesNewCtrl } from './views/procedureEvalTablesNewCtrl';
import procedureExpenseBudgetsViewTemplateUrl from './views/procedureExpenseBudgetsView.html';
import { ProcedureExpenseBudgetsViewCtrl } from './views/procedureExpenseBudgetsViewCtrl';
import procedureIndicatorsAttachTemplateUrl from './views/procedureIndicatorsAttach.html';
import { ProcedureIndicatorsAttachCtrl } from './views/procedureIndicatorsAttachCtrl';
import procedureIndicatorsEditTemplateUrl from './views/procedureIndicatorsEdit.html';
import { ProcedureIndicatorsEditCtrl } from './views/procedureIndicatorsEditCtrl';
import procedureIndicatorsNewTemplateUrl from './views/procedureIndicatorsNew.html';
import { ProcedureIndicatorsNewCtrl } from './views/procedureIndicatorsNewCtrl';
import procedureIndicatorsSearchTemplateUrl from './views/procedureIndicatorsSearch.html';
import { ProcedureIndicatorsSearchCtrl } from './views/procedureIndicatorsSearchCtrl';
import procedureQuestionsEditTemplateUrl from './views/procedureQuestionsEdit.html';
import { ProcedureQuestionsEditCtrl } from './views/procedureQuestionsEditCtrl';
import procedureQuestionsNewTemplateUrl from './views/procedureQuestionsNew.html';
import { ProcedureQuestionsNewCtrl } from './views/procedureQuestionsNewCtrl';
import procedureDeclarationsEditTemplateUrl from './views/procedureDeclarationsEdit.html';
import { ProcedureDeclarationsEditCtrl } from './views/procedureDeclarationsEditCtrl';
import procedureDeclarationsNewTemplateUrl from './views/procedureDeclarationsNew.html';
import { ProcedureDeclarationsNewCtrl } from './views/procedureDeclarationsNewCtrl';
import proceduresEditTemplateUrl from './views/proceduresEdit.html';
import { ProceduresEditCtrl } from './views/proceduresEditCtrl';
import procedureSharesEditTemplateUrl from './views/procedureSharesEdit.html';
import { ProcedureSharesEditCtrl } from './views/procedureSharesEditCtrl';
import procedureSharesNewTemplateUrl from './views/procedureSharesNew.html';
import { ProcedureSharesNewCtrl } from './views/procedureSharesNewCtrl';
import procedureSharesSearchTemplateUrl from './views/procedureSharesSearch.html';
import { ProcedureSharesSearchCtrl } from './views/procedureSharesSearchCtrl';
import proceduresMapTemplateUrl from './views/proceduresMap.html';
import { ProceduresMapCtrl } from './views/proceduresMapCtrl';
import proceduresNewTemplateUrl from './views/proceduresNew.html';
import { ProceduresNewCtrl } from './views/proceduresNewCtrl';
import procedureSpecFieldsEditTemplateUrl from './views/procedureSpecFieldsEdit.html';
import { ProcedureSpecFieldsEditCtrl } from './views/procedureSpecFieldsEditCtrl';
import procedureSpecFieldsNewTemplateUrl from './views/procedureSpecFieldsNew.html';
import { ProcedureSpecFieldsNewCtrl } from './views/procedureSpecFieldsNewCtrl';
import procedureSpecFieldsSearchTemplateUrl from './views/procedureSpecFieldsSearch.html';
import { ProcedureSpecFieldsSearchCtrl } from './views/procedureSpecFieldsSearchCtrl';
import proceduresSearchTemplateUrl from './views/proceduresSearch.html';
import { ProceduresSearchCtrl } from './views/proceduresSearchCtrl';
import proceduresViewTemplateUrl from './views/proceduresView.html';
import { ProceduresViewCtrl } from './views/proceduresViewCtrl';
import procedureTimeLimitsEditTemplateUrl from './views/procedureTimeLimitsEdit.html';
import { ProcedureTimeLimitsEditCtrl } from './views/procedureTimeLimitsEditCtrl';
import procedureTimeLimitsNewTemplateUrl from './views/procedureTimeLimitsNew.html';
import { ProcedureTimeLimitsNewCtrl } from './views/procedureTimeLimitsNewCtrl';
import procedureTimeLimitsSearchTemplateUrl from './views/procedureTimeLimitsSearch.html';
import { ProcedureTimeLimitsSearchCtrl } from './views/procedureTimeLimitsSearchCtrl';
import procedureLocationNewTemplateUrl from './views/procedureLocationNew.html';
import { ProcedureLocationNewCtrl } from './views/procedureLocationNewCtrl';
import procedureLocationEditTemplateUrl from './views/procedureLocationEdit.html';
import { ProcedureLocationEditCtrl } from './views/procedureLocationEditCtrl';
import procedureAppSectionSearchTemplateUrl from './views/procedureAppSectionsSearch.html';
import { ProcedureAppSectionSearchCtrl } from './views/procedureAppSectionsSearchCtrl';
import procedureDirectionsSearchTemplateUrl from './views/procedureDirectionsSearch.html';
import { ProcedureDirectionsSearchCtrl } from './views/procedureDirectionsSearchCtrl';
import procedureDirectionsEditTemplateUrl from './views/procedureDirectionsEdit.html';
import { ProcedureDirectionsEditCtrl } from './views/procedureDirectionsEditCtrl';
import procedureMonitorstatSearchTemplateUrl from './views/procedureMonitorstatSearch.html';
import { ProcedureMonitorstatSearchCtrl } from './views/procedureMonitorstatSearchCtrl';
import procedureMonitorstatNewTemplateUrl from './views/procedureMonitorstatRequestNew.html';
import { ProcedureMonitorstatRequestNewCtrl } from './views/procedureMonitorstatRequestNewCtrl';
import procedureMonitorstatEditTemplateUrl from './views/procedureMonitorstatRequestEdit.html';
import { ProcedureMonitorstatRequestEditCtrl } from './views/procedureMonitorstatRequestEditCtrl';
import procedureMonitorstatEconomicActivitiesNewTemplateUrl from './views/procedureMonitorstatEconomicActivitiesNew.html';
import { ProcedureMonitorstatEconomicActivitiesNewCtrl } from './views/procedureMonitorstatEconomicActivitiesNewCtrl';
import { ProcedureMonitorstatDocumentFactory } from './resources/procedureMonitorstatDocument';
import { ProcedureMonitorstatRequestFactory } from './resources/procedureMonitorstatRequest';
import { ProcedureMonitorstatEconomicActivityFactory } from './resources/procedureMonitorstatEconomicActivity';

const ProceduresModule = angular
  .module('main.procedures', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule,

    //submodules
    ContractReportDocumentsModule,
    ProcedureMassCommunicationsModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisProcedureData',
        templateUrl: procedureDataTemplateUrl,
        controller: ProcedureDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureShareData',
        templateUrl: procedureShareDataTemplateUrl,
        controller: ProcedureShareDataCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureTimeLimitData',
        templateUrl: procedureTimeLimitTemplateUrl,
        controller: ProcedureTimeLimitCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureSpecFieldData',
        templateUrl: procedureSpecFieldDataTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureEvalTable',
        templateUrl: procedureEvalTableTemplateUrl,
        controller: ProcedureEvalTableCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureQuestion',
        templateUrl: procedureQuestionTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureAppDocument',
        templateUrl: procedureAppDocumentTemplateUrl,
        controller: ProcedureAppDocumentCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureLocation',
        templateUrl: procedureLocationTemplateUrl,
        controller: ProcedureLocationCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureDirection',
        templateUrl: procedureDirectionTemplateUrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureMonitorstatRequest',
        templateUrl: procedureMonitorstatRequestTemplateUrl,
        controller: ProcedureAppDocumentCtrl
      });
      scaffoldingProvider.form({
        name: 'eumisProcedureDeclaration',
        templateUrl: procedureDeclarationTemplateUrl,
        controller: ProcedureDeclarationCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('budgetLevel1Modal'                      , budgetLevel1ModalTemplateUrl                                  , BudgetLevel1ModalCtrl                      , 'md' )
    .modal('budgetLevel2Modal'                      , budgetLevel2ModalTemplateUrl                                  , BudgetLevel2ModalCtrl                      , 'md' )
    .modal('budgetLevel3Modal'                      , budgetLevel3ModalTemplateUrl                                  , BudgetLevel3ModalCtrl                      , 'md' )
    .modal('budgetValidationRuleModal'              , budgetValidationRuleModalTemplateUrl                          , BudgetValidationRuleModalCtrl              , 'md' )
    .modal('chooseProcedureModal'                   , chooseProcedureModalTemplateUrl                               , ChooseProcedureModalCtrl                   , 'md' )
    .modal('chooseMonitorstatReportModal'           , chooseMonitorstatReportsModalTemplateUrl                      , ChooseMonitorstatReportsModalCtrl          , 'xlg')
    .modal('chooseDirectionModal'                   , chooseDirectionModalTemplateUrl                               , ChooseDirectionModalCtrl                   , 'md' )
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.procedures'                                             , '/procedures?programmeId&programmePriorityId'                                                                                                                                                              ])
    .state(['root.procedures.tree'                                        , ''                    ,       ['@root'                           , proceduresMapTemplateUrl                                                  , ProceduresMapCtrl                              ]])
    .state(['root.procedures.search'                                      , '/search'             ,       ['@root'                           , proceduresSearchTemplateUrl                                               , ProceduresSearchCtrl                           ]])
    .state(['root.procedures.new'                                         , '/new'                ,       ['@root'                           , proceduresNewTemplateUrl                                                  , ProceduresNewCtrl                              ]])
    .state(['root.procedures.view'                                        , '/:id?rf'             , true, ['@root'                           , proceduresViewTemplateUrl                                                 , ProceduresViewCtrl                             ]])
    .state(['root.procedures.view.edit'                                   , ''                    ,       ['@root.procedures.view'           , proceduresEditTemplateUrl                                                 , ProceduresEditCtrl                             ]])

    .state(['root.procedures.view.directions'                             , '/directions'                                                                                                                                                                                              ])
    .state(['root.procedures.view.directions.search'                      , ''                    ,       ['@root.procedures.view'           , procedureDirectionsSearchTemplateUrl                                      , ProcedureDirectionsSearchCtrl                  ]])
    .state(['root.procedures.view.directions.edit'                        , '/:ind'               ,       ['@root.procedures.view'           , procedureDirectionsEditTemplateUrl                                        , ProcedureDirectionsEditCtrl                    ]])
    
    .state(['root.procedures.view.sections'                               , '/sections'           ,       ['@root.procedures.view'           , procedureAppSectionSearchTemplateUrl                                      , ProcedureAppSectionSearchCtrl                  ]])
    .state(['root.procedures.view.indicators'                             , '/indicators'                                                                                                                                                                                              ])
    .state(['root.procedures.view.indicators.search'                      , ''                    ,       ['@root.procedures.view'           , procedureIndicatorsSearchTemplateUrl                                      , ProcedureIndicatorsSearchCtrl                  ]])
    .state(['root.procedures.view.indicators.attach'                      , '/attach'             ,       ['@root.procedures.view'           , procedureIndicatorsAttachTemplateUrl                                      , ProcedureIndicatorsAttachCtrl                  ]])
    .state(['root.procedures.view.indicators.new'                         , '/new'                ,       ['@root.procedures.view'           , procedureIndicatorsNewTemplateUrl                                         , ProcedureIndicatorsNewCtrl                     ]])
    .state(['root.procedures.view.indicators.edit'                        , '/:ind'               ,       ['@root.procedures.view'           , procedureIndicatorsEditTemplateUrl                                        , ProcedureIndicatorsEditCtrl                    ]])

    .state(['root.procedures.view.procedureShares'                        , '/shares'                                                                                                                                                                                                  ])
    .state(['root.procedures.view.procedureShares.search'                 , ''                    ,       ['@root.procedures.view'           , procedureSharesSearchTemplateUrl                                          , ProcedureSharesSearchCtrl                      ]])
    .state(['root.procedures.view.procedureShares.new'                    , '/new'                ,       ['@root.procedures.view'           , procedureSharesNewTemplateUrl                                             , ProcedureSharesNewCtrl                         ]])
    .state(['root.procedures.view.procedureShares.edit'                   , '/:ind'               ,       ['@root.procedures.view'           , procedureSharesEditTemplateUrl                                            , ProcedureSharesEditCtrl                        ]])
    
    .state(['root.procedures.view.procedureTimeLimits'                    , '/timeLimits'                                                                                                                                                                                              ])
    .state(['root.procedures.view.procedureTimeLimits.search'             , ''                    ,       ['@root.procedures.view'           , procedureTimeLimitsSearchTemplateUrl                                      , ProcedureTimeLimitsSearchCtrl                  ]])
    .state(['root.procedures.view.procedureTimeLimits.new'                , '/new'                ,       ['@root.procedures.view'           , procedureTimeLimitsNewTemplateUrl                                         , ProcedureTimeLimitsNewCtrl                     ]])
    .state(['root.procedures.view.procedureTimeLimits.edit'               , '/:ind?editable'      ,       ['@root.procedures.view'           , procedureTimeLimitsEditTemplateUrl                                        , ProcedureTimeLimitsEditCtrl                    ]])

    .state(['root.procedures.view.ProcedureExpenseBudgets'                , '/expenseBudgets'                                                                                                                                                                                          ])
    .state(['root.procedures.view.ProcedureExpenseBudgets.view'           , ''                    ,       ['@root.procedures.view'           , procedureExpenseBudgetsViewTemplateUrl                                    , ProcedureExpenseBudgetsViewCtrl                ]])

    .state(['root.procedures.view.procedureSpecFields'                    , '/specFields'                                                                                                                                                                                              ])
    .state(['root.procedures.view.procedureSpecFields.search'             , ''                    ,       ['@root.procedures.view'           , procedureSpecFieldsSearchTemplateUrl                                      , ProcedureSpecFieldsSearchCtrl                  ]])
    .state(['root.procedures.view.procedureSpecFields.new'                , '/new'                ,       ['@root.procedures.view'           , procedureSpecFieldsNewTemplateUrl                                         , ProcedureSpecFieldsNewCtrl                     ]])
    .state(['root.procedures.view.procedureSpecFields.edit'               , '/:ind'               ,       ['@root.procedures.view'           , procedureSpecFieldsEditTemplateUrl                                        , ProcedureSpecFieldsEditCtrl                    ]])

    .state(['root.procedures.view.allDocs'                                , '/allDocs'                                                                                                                                                                                                 ])
    .state(['root.procedures.view.allDocs.search'                         , ''                    ,       ['@root.procedures.view'           , procedureAllDocumentsSearchTemplateUrl                                    , ProcedureAllDocumentsSearchCtrl                ]])
    .state(['root.procedures.view.allDocs.docs'                           , '/documents'                                                                                                                                                                                               ])
    .state(['root.procedures.view.allDocs.docs.new'                       , '/new'                ,       ['@root.procedures.view'           , procedureDocumentsNewTemplateUrl                                          , ProcedureDocumentsNewCtrl                      ]])
    .state(['root.procedures.view.allDocs.docs.edit'                      , '/:ind'               ,       ['@root.procedures.view'           , procedureDocumentsEditTemplateUrl                                         , ProcedureDocumentsEditCtrl                     ]])
    .state(['root.procedures.view.allDocs.appGuidelines'                  , '/appGuidelines'                                                                                                                                                                                           ])
    .state(['root.procedures.view.allDocs.appGuidelines.new'              , '/new'                ,       ['@root.procedures.view'           , procedureAppGuidelinesNewTemplateUrl                                      , ProcedureAppGuidelinesNewCtrl                  ]])
    .state(['root.procedures.view.allDocs.appGuidelines.edit'             , '/:ind'               ,       ['@root.procedures.view'           , procedureAppGuidelinesEditTemplateUrl                                     , ProcedureAppGuidelinesEditCtrl                 ]])
    .state(['root.procedures.view.allDocs.appDocs'                        , '/appDocs'                                                                                                                                                                                                 ])
    .state(['root.procedures.view.allDocs.appDocs.new'                    , '/new'                ,       ['@root.procedures.view'           , procedureAppDocumentsNewTemplateUrl                                       , ProcedureAppDocumentsNewCtrl                   ]])
    .state(['root.procedures.view.allDocs.appDocs.edit'                   , '/:ind'               ,       ['@root.procedures.view'           , procedureAppDocumentsEditTemplateUrl                                      , ProcedureAppDocumentsEditCtrl                  ]])
    .state(['root.procedures.view.allDocs.evalTables'                     , '/evalTables'                                                                                                                                                                                              ])
    .state(['root.procedures.view.allDocs.evalTables.new'                 , '/new'                ,       ['@root.procedures.view'           , procedureEvalTablesNewTemplateUrl                                         , ProcedureEvalTablesNewCtrl                     ]])
    .state(['root.procedures.view.allDocs.evalTables.edit'                , '/:ind'               ,       ['@root.procedures.view'           , procedureEvalTablesEditTemplateUrl                                        , ProcedureEvalTablesEditCtrl                    ]])
    .state(['root.procedures.view.allDocs.questions'                      , '/questions'                                                                                                                                                                                               ])
    .state(['root.procedures.view.allDocs.questions.new'                  , '/new'                ,       ['@root.procedures.view'           , procedureQuestionsNewTemplateUrl                                          , ProcedureQuestionsNewCtrl                      ]])
    .state(['root.procedures.view.allDocs.questions.edit'                 , '/:ind?del'           ,       ['@root.procedures.view'           , procedureQuestionsEditTemplateUrl                                         , ProcedureQuestionsEditCtrl                     ]])
    .state(['root.procedures.view.allDocs.declarations'                   , '/declarations'                                                                                                                                                                                            ])
    .state(['root.procedures.view.allDocs.declarations.new'               , '/new'                ,       ['@root.procedures.view'           , procedureDeclarationsNewTemplateUrl                                       , ProcedureDeclarationsNewCtrl                   ]])
    .state(['root.procedures.view.allDocs.declarations.edit'              , '/:ind'               ,       ['@root.procedures.view'           , procedureDeclarationsEditTemplateUrl                                      , ProcedureDeclarationsEditCtrl                  ]])

    .state(['root.procedures.view.procedureLocation'                      , '/locations'                                                                                                                                                                                               ])
    .state(['root.procedures.view.procedureLocation.new'                  , '/new'                ,       ['@root.procedures.view'           , procedureLocationNewTemplateUrl                                           , ProcedureLocationNewCtrl                       ]])
    .state(['root.procedures.view.procedureLocation.edit'                 , '/:ind'               ,       ['@root.procedures.view'           , procedureLocationEditTemplateUrl                                          , ProcedureLocationEditCtrl                      ]])
    
    .state(['root.procedures.view.monitorstat'                            , '/monitorstat'                                                                                                                                                                                              ])
    .state(['root.procedures.view.monitorstat.search'                     , ''                    ,       ['@root.procedures.view'           , procedureMonitorstatSearchTemplateUrl                                      , ProcedureMonitorstatSearchCtrl                ]])
    .state(['root.procedures.view.monitorstat.new'                        , '/new'                ,       ['@root.procedures.view'           , procedureMonitorstatNewTemplateUrl                                         , ProcedureMonitorstatRequestNewCtrl            ]])
    .state(['root.procedures.view.monitorstat.edit'                       , '/:ind'               ,       ['@root.procedures.view'           , procedureMonitorstatEditTemplateUrl                                        , ProcedureMonitorstatRequestEditCtrl           ]])
    .state(['root.procedures.view.monitorstat.economicActivities'         , '/activities'                                                                                                                                                                                               ])
    .state(['root.procedures.view.monitorstat.economicActivities.new'     , '/new'                ,       ['@root.procedures.view'           , procedureMonitorstatEconomicActivitiesNewTemplateUrl                       , ProcedureMonitorstatEconomicActivitiesNewCtrl ]]);
    }
  ]);

export default ProceduresModule.name;
ProceduresModule.factory('Procedure', ProcedureFactory);
ProceduresModule.factory('ProcedureAppDocument', ProcedureAppDocumentFactory);
ProceduresModule.factory('ProcedureAppGuideline', ProcedureAppGuidelineFactory);
ProceduresModule.factory('ProcedureDocument', ProcedureDocumentFactory);
ProceduresModule.factory('ProcedureEvalTable', ProcedureEvalTableFactory);
ProceduresModule.factory('ProcedureFile', ProcedureFileFactory);
ProceduresModule.factory('ProcedureIndicator', ProcedureIndicatorFactory);
ProceduresModule.factory('ProcedureQuestion', ProcedureQuestionFactory);
ProceduresModule.factory('ProcedureShare', ProcedureShareFactory);
ProceduresModule.factory('ProcedureShareExpenseBudget', ProcedureShareExpenseBudgetFactory);

ProceduresModule.factory('ProcedureSpecField', ProcedureSpecFieldFactory);
ProceduresModule.factory('ProcedureTimeLimit', ProcedureTimeLimitFactory);

ProceduresModule.factory('ProcedureLocation', ProcedureLocationFactory);
ProceduresModule.factory('ProcedureApplicationSection', ProcedureApplicationSectionFactory);
ProceduresModule.factory('ProcedureDirection', ProcedureDirectionFactory);
ProceduresModule.factory('ProcedureMonitorstatDocument', ProcedureMonitorstatDocumentFactory);
ProceduresModule.factory('ProcedureMonitorstatRequest', ProcedureMonitorstatRequestFactory);
ProceduresModule.factory(
  'ProcedureMonitorstatEconomicActivity',
  ProcedureMonitorstatEconomicActivityFactory
);
ProceduresModule.factory('ProcedureDeclaration', ProcedureDeclarationFactory);
