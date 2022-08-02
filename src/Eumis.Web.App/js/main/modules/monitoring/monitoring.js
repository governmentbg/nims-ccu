import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { MonitoringFactory } from './resources/monitoring';
import advancePaymentsReportTemplateUrl from './views/advancePaymentsReport.html';
import { AdvancePaymentsReportCtrl } from './views/advancePaymentsReportCtrl';
import anex3ReportTemplateUrl from './views/anex3Report.html';
import { Anex3ReportCtrl } from './views/anex3ReportCtrl';
import arachneReportTemplateUrl from './views/arachneReport.html';
import { ArachneReportCtrl } from './views/arachneReportCtrl';
import beneficiaryProjectsContractsReportTemplateUrl from './views/beneficiaryProjectsContractsReport.html';
import { BeneficiaryProjectsContractsReportCtrl } from './views/beneficiaryProjectsContractsReportCtrl';
import budgetLevelsReportTemplateUrl from './views/budgetLevelsReport.html';
import { BudgetLevelsReportCtrl } from './views/budgetLevelsReportCtrl';
import concludedContractsReportTemplateUrl from './views/concludedContractsReport.html';
import { ConcludedContractsReportCtrl } from './views/concludedContractsReportCtrl';
import contractReportPaymentsReportTemplateUrl from './views/contractReportPaymentsReport.html';
import { ContractReportPaymentsReportCtrl } from './views/contractReportPaymentsReportCtrl';
import contractReportsReportTemplateUrl from './views/contractReportsReport.html';
import { ContractReportsReportCtrl } from './views/contractReportsReportCtrl';
import contractsReportTemplateUrl from './views/contractsReport.html';
import { ContractsReportCtrl } from './views/contractsReportCtrl';
import doubleFundingReportTemplateUrl from './views/doubleFundingReport.html';
import { DoubleFundingReportCtrl } from './views/doubleFundingReportCtrl';
import evaluationsReportTemplateUrl from './views/evaluationsReport.html';
import { EvaluationsReportCtrl } from './views/evaluationsReportCtrl';
import financialCorrectionsReportTemplateUrl from './views/financialCorrectionsReport.html';
import { FinancialCorrectionsReportCtrl } from './views/financialCorrectionsReportCtrl';
import financialExecutionReportTemplateUrl from './views/financialExecutionReport.html';
import { FinancialExecutionReportCtrl } from './views/financialExecutionReportCtrl';
import indicatorsReportTemplateUrl from './views/indicatorsReport.html';
import { IndicatorsReportCtrl } from './views/indicatorsReportCtrl';
import irregularitiesReportTemplateUrl from './views/irregularitiesReport.html';
import { IrregularitiesReportCtrl } from './views/irregularitiesReportCtrl';
import microdataEsfReportTemplateUrl from './views/microdataEsfReport.html';
import { MicrodataEsfReportCtrl } from './views/microdataEsfReportCtrl';
import physicalExecutionReportTemplateUrl from './views/physicalExecutionReport.html';
import { PhysicalExecutionReportCtrl } from './views/physicalExecutionReportCtrl';
import pinReportTemplateUrl from './views/pinReport.html';
import { PinReportCtrl } from './views/pinReportCtrl';
import projectsReportTemplateUrl from './views/projectsReport.html';
import { ProjectsReportCtrl } from './views/projectsReportCtrl';
import v4Plus4ReportTemplateUrl from './views/v4Plus4Report.html';
import { V4Plus4ReportCtrl } from './views/v4Plus4ReportCtrl';
import expenseTypesReportTemplateUrl from './views/expenseTypesReport.html';
import { ExpenseTypesReportCtrl } from './views/expenseTypesReportCtrl';
import sebraReportTemplateUrl from './views/sebraReport.html';
import { SebraReportCtrl } from './views/sebraReportCtrl';

const MonitoringModule = angular
  .module('main.monitoring', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.monitoringAdvancePayments'                                       , '/advancePayments',         ['@root'                                            , advancePaymentsReportTemplateUrl                                                                , AdvancePaymentsReportCtrl                                      ]])
    .state(['root.monitoringAnex3Report'                                           , '/anex3Report'      ,       ['@root'                                            , anex3ReportTemplateUrl                                                                          , Anex3ReportCtrl                                               ]])
    .state(['root.monitoringDoubleFunding'                                         , '/doubleFunding'    ,       ['@root'                                            , doubleFundingReportTemplateUrl                                                                  , DoubleFundingReportCtrl                                       ]])
    .state(['root.monitoringPhysicalExecution'                                     , '/physicalExec'     ,       ['@root'                                            , physicalExecutionReportTemplateUrl                                                              , PhysicalExecutionReportCtrl                                   ]])
    .state(['root.monitoringFinancialExecution'                                    , '/financialExec'    ,       ['@root'                                            , financialExecutionReportTemplateUrl                                                             , FinancialExecutionReportCtrl                                  ]])
    .state(['root.monitoringProjects'                                              , '/monitoringProjects',      ['@root'                                            , projectsReportTemplateUrl                                                                       , ProjectsReportCtrl                                            ]])
    .state(['root.monitoringContracts'                                             , '/monitoringContracts',     ['@root'                                            , contractsReportTemplateUrl                                                                      , ContractsReportCtrl                                           ]])
    .state(['root.monitoringIndicators'                                            , '/monitoringIndicators',    ['@root'                                            , indicatorsReportTemplateUrl                                                                     , IndicatorsReportCtrl                                          ]])
    .state(['root.monitoringContractReports'                                       , '/monitoringReports',       ['@root'                                            , contractReportsReportTemplateUrl                                                                , ContractReportsReportCtrl                                     ]])
    .state(['root.monitoringBudgetLevels'                                          , '/monitoringBudgetLevels',  ['@root'                                            , budgetLevelsReportTemplateUrl                                                                   , BudgetLevelsReportCtrl                                        ]])
    .state(['root.monitoringFinancialCorrections'                                  , '/monitoringFinancialCorrections'         ,['@root'                             , financialCorrectionsReportTemplateUrl                                                           , FinancialCorrectionsReportCtrl                                ]])
    .state(['root.monitoringConcludedContracts'                                    , '/monitoringConcludedContracts'           ,['@root'                             , concludedContractsReportTemplateUrl                                                             , ConcludedContractsReportCtrl                                  ]])
    .state(['root.monitoringBeneficiaryProjectsContracts'                          , '/monitoringBeneficiaryProjectsContracts' ,['@root'                             , beneficiaryProjectsContractsReportTemplateUrl                                                   , BeneficiaryProjectsContractsReportCtrl                        ]])
    .state(['root.monitoringEvaluations'                                           , '/monitoringEvaluations'                  ,['@root'                             , evaluationsReportTemplateUrl                                                                    , EvaluationsReportCtrl                                         ]])
    .state(['root.monitoringContractReportPayments'                                , '/monitoringContractReportPayments'       ,['@root'                             , contractReportPaymentsReportTemplateUrl                                                         , ContractReportPaymentsReportCtrl                              ]])
    .state(['root.monitoringIrregularities'                                        , '/monitoringIrregularities'               ,['@root'                             , irregularitiesReportTemplateUrl                                                                 , IrregularitiesReportCtrl                                      ]])
    .state(['root.monitoringPin'                                                   , '/monitoringPin'                          ,['@root'                             , pinReportTemplateUrl                                                                            , PinReportCtrl                                                 ]])
    .state(['root.monitoringArachne'                                               , '/arachne'                                ,['@root'                             , arachneReportTemplateUrl                                                                        , ArachneReportCtrl                                             ]])
    .state(['root.monitoringMicrodataEsf'                                          , '/monitoringMicrodataEsf'                 ,['@root'                             , microdataEsfReportTemplateUrl                                                                   , MicrodataEsfReportCtrl                                        ]])
    .state(['root.monitoringV4Plus4'                                               , '/V4Plus4'                                ,['@root'                             , v4Plus4ReportTemplateUrl                                                                        , V4Plus4ReportCtrl                                             ]])
    .state(['root.monitoringExpenseTypes'                                          , '/expenseTypes'                           ,['@root'                             , expenseTypesReportTemplateUrl                                                                   , ExpenseTypesReportCtrl                                        ]])
    .state(['root.monitoringSebra'                                                 , '/sebra'                                  ,['@root'                             , sebraReportTemplateUrl                                                                          , SebraReportCtrl                                               ]]);
    }
  ]);

export default MonitoringModule.name;
MonitoringModule.factory('Monitoring', MonitoringFactory);
