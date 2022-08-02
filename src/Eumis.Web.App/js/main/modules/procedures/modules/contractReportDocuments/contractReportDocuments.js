import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { ProcedureTechnicalReportDocumentFactory } from './resources/procedureTechnicalReportDocument';
import { ProcedureFinancialReportDocumentFactory } from './resources/procedureFinancialReportDocument';
import { ProcedureAdvancePaymentDocumentFactory } from './resources/procedureAdvancePaymentDocument';
import { ProcedureIntermediatePaymentDocumentFactory } from './resources/procedureIntermediatePaymentDocument';
import { ProcedureFinalPaymentDocumentFactory } from './resources/procedureFinalPaymentDocument';
import { ProcedureProcurementDocumentFactory } from './resources/procedureProcurementDocument';
import procedureContractReportDocumentTemplateUrl from './forms/procedureContractReportDocument.html';
import { ProcedureContractReportDocumentCtrl } from './forms/procedureContractReportDocumentCtrl.js';
import procedureReportDocumentsSearchTemplateUrl from './views/procedureReportDocumentsSearch.html';
import { ProcedureReportDocumentsSearchCtrl } from './views/procedureReportDocumentsSearchCtrl';
import procedureTechnicalReportDocumentsNewTemplateUrl from './views/procedureTechnicalReportDocumentsNew.html';
import { ProcedureTechnicalReportDocumentsNewCtrl } from './views/procedureTechnicalReportDocumentsNewCtrl';
import procedureTechnicalReportDocumentsEditTemplateUrl from './views/procedureTechnicalReportDocumentsEdit.html';
import { ProcedureTechnicalReportDocumentsEditCtrl } from './views/procedureTechnicalReportDocumentsEditCtrl';
import procedureFinancialReportDocumentsNewTemplateUrl from './views/procedureFinancialReportDocumentsNew.html';
import { ProcedureFinancialReportDocumentsNewCtrl } from './views/procedureFinancialReportDocumentsNewCtrl';
import procedureFinancialReportDocumentsEditTemplateUrl from './views/procedureFinancialReportDocumentsEdit.html';
import { ProcedureFinancialReportDocumentsEditCtrl } from './views/procedureFinancialReportDocumentsEditCtrl';
import procedureAdvancePaymentDocumentsNewTemplateUrl from './views/procedureAdvancePaymentDocumentsNew.html';
import { ProcedureAdvancePaymentDocumentsNewCtrl } from './views/procedureAdvancePaymentDocumentsNewCtrl';
import procedureAdvancePaymentDocumentsEditTemplateUrl from './views/procedureAdvancePaymentDocumentsEdit.html';
import { ProcedureAdvancePaymentDocumentsEditCtrl } from './views/procedureAdvancePaymentDocumentsEditCtrl';
import procedureIntermediatePaymentDocumentsNewTemplateUrl from './views/procedureIntermediatePaymentDocumentsNew.html';
import { ProcedureIntermediatePaymentDocumentsNewCtrl } from './views/procedureIntermediatePaymentDocumentsNewCtrl';
import procedureIntermediatePaymentDocumentsEditTemplateUrl from './views/procedureIntermediatePaymentDocumentsEdit.html';
import { ProcedureIntermediatePaymentDocumentsEditCtrl } from './views/procedureIntermediatePaymentDocumentsEditCtrl';
import procedureFinalPaymentDocumentsNewTemplateUrl from './views/procedureFinalPaymentDocumentsNew.html';
import { ProcedureFinalPaymentDocumentsNewCtrl } from './views/procedureFinalPaymentDocumentsNewCtrl';
import procedureFinalPaymentDocumentsEditTemplateUrl from './views/procedureFinalPaymentDocumentsEdit.html';
import { ProcedureFinalPaymentDocumentsEditCtrl } from './views/procedureFinalPaymentDocumentsEditCtrl';
import procedureProcurementDocumentsNewTemplateUrl from './views/procedureProcurementDocumentsNew.html';
import { ProcedureProcurementDocumentsNewCtrl } from './views/procedureProcurementDocumentsNewCtrl';
import procedureProcurementDocumentsEditTemplateUrl from './views/procedureProcurementDocumentsEdit.html';
import { ProcedureProcurementDocumentsEditCtrl } from './views/procedureProcurementDocumentsEditCtrl';

const ContractReportDocumentsModule = angular
  .module('main.contractReportDocuments', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisProcedureContractReportDocument',
        templateUrl: procedureContractReportDocumentTemplateUrl,
        controller: ProcedureContractReportDocumentCtrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.procedures.view.reportDocs'                             , '/reportDocs'                                                                                                                                                                                              ])
    .state(['root.procedures.view.reportDocs.search'                      , ''                 ,       ['@root.procedures.view'           , procedureReportDocumentsSearchTemplateUrl                                  , ProcedureReportDocumentsSearchCtrl             ]])
    .state(['root.procedures.view.reportDocs.technicals'                  , '/technicalDocs'                                                                                                                                                                                           ])
    .state(['root.procedures.view.reportDocs.technicals.new'              , '/new'             ,       ['@root.procedures.view'           , procedureTechnicalReportDocumentsNewTemplateUrl                            , ProcedureTechnicalReportDocumentsNewCtrl       ]])
    .state(['root.procedures.view.reportDocs.technicals.edit'             , '/:ind'            ,       ['@root.procedures.view'           , procedureTechnicalReportDocumentsEditTemplateUrl                           , ProcedureTechnicalReportDocumentsEditCtrl      ]])
    .state(['root.procedures.view.reportDocs.financials'                  , '/financialDocs'                                                                                                                                                                                           ])
    .state(['root.procedures.view.reportDocs.financials.new'              , '/new'             ,       ['@root.procedures.view'           , procedureFinancialReportDocumentsNewTemplateUrl                            , ProcedureFinancialReportDocumentsNewCtrl       ]])
    .state(['root.procedures.view.reportDocs.financials.edit'             , '/:ind'            ,       ['@root.procedures.view'           , procedureFinancialReportDocumentsEditTemplateUrl                           , ProcedureFinancialReportDocumentsEditCtrl      ]])
    .state(['root.procedures.view.reportDocs.advancePayments'             , '/advancePaymentDocs'                                                                                                                                                                                      ])
    .state(['root.procedures.view.reportDocs.advancePayments.new'         , '/new'             ,       ['@root.procedures.view'           , procedureAdvancePaymentDocumentsNewTemplateUrl                             , ProcedureAdvancePaymentDocumentsNewCtrl        ]])
    .state(['root.procedures.view.reportDocs.advancePayments.edit'        , '/:ind'            ,       ['@root.procedures.view'           , procedureAdvancePaymentDocumentsEditTemplateUrl                            , ProcedureAdvancePaymentDocumentsEditCtrl       ]])
    .state(['root.procedures.view.reportDocs.intermediatePayments'        , '/intermediatePaymentDocs'                                                                                                                                                                                 ])
    .state(['root.procedures.view.reportDocs.intermediatePayments.new'    , '/new'             ,       ['@root.procedures.view'           , procedureIntermediatePaymentDocumentsNewTemplateUrl                        , ProcedureIntermediatePaymentDocumentsNewCtrl   ]])
    .state(['root.procedures.view.reportDocs.intermediatePayments.edit'   , '/:ind'            ,       ['@root.procedures.view'           , procedureIntermediatePaymentDocumentsEditTemplateUrl                       , ProcedureIntermediatePaymentDocumentsEditCtrl  ]])
    .state(['root.procedures.view.reportDocs.finalPayments'               , '/finalPaymentDocs'                                                                                                                                                                                        ])
    .state(['root.procedures.view.reportDocs.finalPayments.new'           , '/new'             ,       ['@root.procedures.view'           , procedureFinalPaymentDocumentsNewTemplateUrl                               , ProcedureFinalPaymentDocumentsNewCtrl          ]])
    .state(['root.procedures.view.reportDocs.finalPayments.edit'          , '/:ind'            ,       ['@root.procedures.view'           , procedureFinalPaymentDocumentsEditTemplateUrl                              , ProcedureFinalPaymentDocumentsEditCtrl         ]])
    .state(['root.procedures.view.reportDocs.procurements'                , '/procurements'                                                                                                                                                                                            ])
    .state(['root.procedures.view.reportDocs.procurements.new'            , '/new'             ,       ['@root.procedures.view'           , procedureProcurementDocumentsNewTemplateUrl                                , ProcedureProcurementDocumentsNewCtrl           ]])
    .state(['root.procedures.view.reportDocs.procurements.edit'           , '/:ind'            ,       ['@root.procedures.view'           , procedureProcurementDocumentsEditTemplateUrl                               , ProcedureProcurementDocumentsEditCtrl          ]]);
    }
  ]);

export default ContractReportDocumentsModule.name;
ContractReportDocumentsModule.factory(
  'ProcedureTechnicalReportDocument',
  ProcedureTechnicalReportDocumentFactory
);
ContractReportDocumentsModule.factory(
  'ProcedureFinancialReportDocument',
  ProcedureFinancialReportDocumentFactory
);
ContractReportDocumentsModule.factory(
  'ProcedureAdvancePaymentDocument',
  ProcedureAdvancePaymentDocumentFactory
);
ContractReportDocumentsModule.factory(
  'ProcedureIntermediatePaymentDocument',
  ProcedureIntermediatePaymentDocumentFactory
);
ContractReportDocumentsModule.factory(
  'ProcedureFinalPaymentDocument',
  ProcedureFinalPaymentDocumentFactory
);
ContractReportDocumentsModule.factory(
  'ProcedureProcurementDocument',
  ProcedureProcurementDocumentFactory
);
