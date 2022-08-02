export const ContractReportFinancialCorrectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportFinancialCorrections/:id',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReportFinancialCorrections/:id/changeStatusToEnded'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReportFinancialCorrections/:id/canChangeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReportFinancialCorrections/:id/changeStatusToDraft'
        },
        canChangeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReportFinancialCorrections/:id/canChangeStatusToDraft'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReportFinancialCorrections/canCreate'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contractReportFinancialCorrections/:id/info'
        },
        canDelete: {
          method: 'POST',
          url: 'api/contractReportFinancialCorrections/:id/canDelete'
        },
        getFinancialCSDBudgetItems: {
          method: 'GET',
          url: 'api/contractReportFinancialCorrections/:id/financialCSDBudgetItems',
          isArray: true
        },
        getContractReports: {
          method: 'GET',
          url: 'api/contractReportFinancialCorrections/contractReports',
          isArray: true
        },
        getCertReportFinancialCorrectionsContractReportFinancialCorrection: {
          method: 'GET',
          url: 'api/certReports/:id/financialCorrections/:ind/financialCorrection'
        },
        getAnnualAccountReportFinancialCorrectionContractReportFinancialCorrection: {
          method: 'GET',
          url: 'api/annualAccountReports/:id/financialCorrections/:ind/financialCorrection'
        }
      }
    );
  }
];
