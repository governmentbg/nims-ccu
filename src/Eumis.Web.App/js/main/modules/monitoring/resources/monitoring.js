export const MonitoringFactory = [
  '$resource',
  function($resource) {
    return {
      getAdvancePaymentsReport: $resource('api/monitoringReports/advancePayments').query,
      getDoubleFundingReport: $resource('api/monitoringReports/doubleFunding').query,
      getPhysicalExecutionReport: $resource('api/monitoringReports/physicalExecution').query,
      getProjectsReport: $resource('api/monitoringReports/projects').query,
      getContractsReport: $resource('api/monitoringReports/contracts').get,
      geIndicatorsReport: $resource('api/monitoringReports/indicators').query,
      getContractReportsReport: $resource('api/monitoringReports/contractReports').query,
      getFinancialExecutionTable1Report: $resource(
        'api/monitoringReports/financialExecution/table1'
      ).query,
      getFinancialExecutionTable2Report: $resource(
        'api/monitoringReports/financialExecution/table2'
      ).query,
      getFinancialExecutionTable3Report: $resource(
        'api/monitoringReports/financialExecution/table3'
      ).query,
      getBudgetLevelsReport: $resource('api/monitoringReports/budgetLevels').query,
      getFinancialCorrectionsReport: $resource('api/monitoringReports/financialCorrections').query,
      getConcludedContractsReport: $resource('api/monitoringReports/concludedContracts').query,
      getBeneficiaryProjectsContractsReport: $resource(
        'api/monitoringReports/beneficiaryProjectsContracts'
      ).query,
      getEvaluationsReport: $resource('api/monitoringReports/evaluations').query,
      getContractReportPaymentsReport: $resource('api/monitoringReports/contractReportPayments')
        .query,
      getProgrammeSummaryReport: $resource('api/monitoringReports/programmeSummary').query,
      getIrregularitiesReport: $resource('api/monitoringReports/irregularities').query,
      getPinReport: $resource('api/monitoringReports/pin').query,
      getV4Plus4Report: $resource('api/monitoringReports/v4Plus4').query,
      getExpenseTypesReport: $resource('api/monitoringReports/expenseTypes').query
    };
  }
];
