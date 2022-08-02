export const ContractReportFinancialCSDBudgetItemFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/financialCSDBudgetItems/:ind',
      {},
      {
        changeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReports/:id/financialCSDBudgetItems/:ind/changeStatusToEnded'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/financialCSDBudgetItems/:ind/changeStatusToDraft'
        },
        techCheck: {
          method: 'POST',
          url: 'api/contractReports/:id/financialCSDBudgetItems/:ind/techCheck'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url: 'api/contractReports/:id/financialCSDBudgetItems/:ind/canChangeStatusToEnded'
        },
        canChangeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/financialCSDBudgetItems/:ind/canChangeStatusToDraft'
        },
        getReportFinancialCorrectionContractReportFinancialCSDBudgetItem: {
          method: 'GET',
          url: 'api/contractReportFinancialCorrections/:id/financialCSDBudgetItems/:ind'
        },
        getReportFinancialRevalidationContractReportFinancialCSDBudgetItem: {
          method: 'GET',
          url: 'api/contractReportFinancialRevalidations/:id/financialCSDBudgetItems/:ind'
        },
        getReportFinancialCertCorrectionContractReportFinancialCSDBudgetItem: {
          method: 'GET',
          url: 'api/contractReportFinancialCertCorrections/:id/financialCSDBudgetItems/:ind'
        },
        getReportCerAuthorityFinancialCorrectionContractReportFinancialCSDBudgetItem: {
          method: 'GET',
          url:
            'api/contractReportCertAuthorityFinancialCorrections/:id/financialCSDBudgetItems/:ind'
        },
        getReportRevalidationCerAuthorityFinancialCorrectionContractReportFinancialCSDBudgetItem: {
          method: 'GET',
          url:
            'api/contractReportRevalidationCertAuthorityFinancialCorrections/:id/financialCSDBudgetItems/:ind'
        },
        getCertReportContractReportAttachedFinancialCSDBudgetItems: {
          method: 'GET',
          url: 'api/certReports/:id/payments/:ind/attachedFinancialCSDBudgetItems',
          isArray: true
        },
        getCertReportContractReportUnattachedFinancialCSDBudgetItems: {
          method: 'GET',
          url: 'api/certReports/:id/payments/:ind/unattachedFinancialCSDBudgetItems',
          isArray: true
        },
        getCertReportContractReportFinancialCSDBudgetItem: {
          method: 'GET',
          url: 'api/certReports/:id/payments/:ind/financialCSDBudgetItems/:index'
        },
        certUpdate: {
          method: 'PUT',
          url: 'api/certReports/:id/payments/:ind/financialCSDBudgetItems/:index/certUpdate'
        },
        changeCertStatusToEnded: {
          method: 'POST',
          url:
            'api/certReports/:id/payments/:ind/financialCSDBudgetItems/:index/' +
            'changeCertStatusToEnded'
        },
        changeCertStatusToDraft: {
          method: 'POST',
          url:
            'api/certReports/:id/payments/:ind/financialCSDBudgetItems/:index/' +
            'changeCertStatusToDraft'
        },
        canChangeCertStatusToEnded: {
          method: 'POST',
          url:
            'api/certReports/:id/payments/:ind/financialCSDBudgetItems/:index/' +
            'canChangeCertStatusToEnded'
        }
      }
    );
  }
];
