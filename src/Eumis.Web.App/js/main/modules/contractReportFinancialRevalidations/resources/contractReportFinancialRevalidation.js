export const ContractReportFinancialRevalidationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportFinancialRevalidations/:id',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReportFinancialRevalidations/:id/changeStatusToEnded'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReportFinancialRevalidations/:id/canChangeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReportFinancialRevalidations/:id/changeStatusToDraft'
        },
        canChangeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReportFinancialRevalidations/:id/canChangeStatusToDraft'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReportFinancialRevalidations/canCreate'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contractReportFinancialRevalidations/:id/info'
        },
        canDelete: {
          method: 'POST',
          url: 'api/contractReportFinancialRevalidations/:id/canDelete'
        },
        getFinancialCSDBudgetItems: {
          method: 'GET',
          url: 'api/contractReportFinancialRevalidations/:id/financialCSDBudgetItems',
          isArray: true
        },
        getContractReports: {
          method: 'GET',
          url: 'api/contractReportFinancialRevalidations/contractReports',
          isArray: true
        },
        getCertReportFinancialRevalidationsContractReportFinancialRevalidation: {
          method: 'GET',
          url: 'api/certReports/:id/financialRevalidations/:ind/financialRevalidation'
        }
      }
    );
  }
];
