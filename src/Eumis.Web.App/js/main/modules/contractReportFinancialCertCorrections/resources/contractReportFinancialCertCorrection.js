export const ContractReportFinancialCertCorrectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportFinancialCertCorrections/:id',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReportFinancialCertCorrections/:id/changeStatusToEnded'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReportFinancialCertCorrections/:id/canChangeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReportFinancialCertCorrections/:id/changeStatusToDraft'
        },
        canChangeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReportFinancialCertCorrections/:id/canChangeStatusToDraft'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReportFinancialCertCorrections/canCreate'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contractReportFinancialCertCorrections/:id/info'
        },
        canDelete: {
          method: 'POST',
          url: 'api/contractReportFinancialCertCorrections/:id/canDelete'
        },
        getFinancialCSDBudgetItems: {
          method: 'GET',
          url: 'api/contractReportFinancialCertCorrections/:id/financialCSDBudgetItems',
          isArray: true
        },
        getContractReports: {
          method: 'GET',
          url: 'api/contractReportFinancialCertCorrections/contractReports',
          isArray: true
        },
        getCertReportFinancialCertCorrectionsContractReportFinancialCertCorrection: {
          method: 'GET',
          url: 'api/certReports/:id/financialCertCorrections/:ind/financialCertCorrection'
        }
      }
    );
  }
];
