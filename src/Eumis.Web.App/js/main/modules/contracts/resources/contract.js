export const ContractFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id',
      {},
      {
        canCreate: {
          method: 'POST',
          url: 'api/contracts/canCreate'
        },
        canDelete: {
          method: 'POST',
          url: 'api/contracts/:id/canDelete'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contracts/:id/info'
        },
        getData: {
          method: 'GET',
          url: 'api/contracts/:id/data'
        },
        markAsChecked: {
          method: 'POST',
          url: 'api/contracts/:id/markAsChecked'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contracts/:id/changeStatusToDraft'
        },
        isRegNumExisting: {
          method: 'GET',
          url: 'api/contracts/isRegNumExisting',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        getInfoForReport: {
          method: 'GET',
          url: 'api/contractReports/:id/contractInfo'
        },
        getInfoForCorrection: {
          method: 'GET',
          url: 'api/contractReportFinancialCorrections/:id/contractInfo'
        },
        getInfoForTechnicalCorrection: {
          method: 'GET',
          url: 'api/contractReportTechnicalCorrections/:id/contractInfo'
        },
        getInfoForRevalidation: {
          method: 'GET',
          url: 'api/contractReportFinancialRevalidations/:id/contractInfo'
        },
        getInfoForCertCorrection: {
          method: 'GET',
          url: 'api/contractReportFinancialCertCorrections/:id/contractInfo'
        },
        getInfoForCertAuthorityCorrection: {
          method: 'GET',
          url: 'api/contractReportCertAuthorityFinancialCorrections/:id/contractInfo'
        },
        getInfoForRevalidationCertAuthorityCorrection: {
          method: 'GET',
          url: 'api/contractReportRevalidationCertAuthorityFinancialCorrections/:id/contractInfo'
        },
        getCorrectionDebtContractByRegNum: {
          method: 'GET',
          url: 'api/contracts/correctionDebtContractByNumber'
        },
        getActuallyPaidAmountByRegNum: {
          method: 'GET',
          url: 'api/contracts/actuallyPaidAmountByRegNum'
        },
        getProjects: {
          method: 'GET',
          url: 'api/contractProjects',
          isArray: true
        },
        getActuallyPaidAmounts: {
          method: 'GET',
          url: 'api/contracts/:id/actuallyPaidAmounts',
          isArray: true
        },
        getContractDebts: {
          method: 'GET',
          url: 'api/contracts/:id/debts',
          isArray: true
        },
        getReimbursedAmounts: {
          method: 'GET',
          url: 'api/contracts/:id/reimbursedAmounts',
          isArray: true
        },
        getFinancialCorrections: {
          method: 'GET',
          url: 'api/contracts/:id/financialCorrections',
          isArray: true
        },
        getFlatFinancialCorrections: {
          method: 'GET',
          url: 'api/contracts/:id/flatFinancialCorrections',
          isArray: true
        }
      }
    );
  }
];
