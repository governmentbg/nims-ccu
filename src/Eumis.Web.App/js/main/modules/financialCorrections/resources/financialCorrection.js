export const FinancialCorrectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/financialCorrections/:id',
      {},
      {
        canCreate: {
          method: 'POST',
          url: 'api/financialCorrections/canCreate'
        },
        newFinancialCorrection: {
          method: 'GET',
          url: 'api/financialCorrections/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/financialCorrections/:id/info'
        },
        getContracts: {
          method: 'GET',
          url: 'api/financialCorrectionContracts',
          isArray: true
        },
        cancel: {
          method: 'POST',
          url: 'api/financialCorrections/:id/cancel'
        },
        changeStatusToEntered: {
          method: 'POST',
          url: 'api/financialCorrections/:id/changeStatusToEntered'
        },
        calculate: {
          method: 'POST',
          url: 'api/financialCorrections/calculate'
        },
        getContractReports: {
          method: 'GET',
          url: 'api/financialCorrections/:id/contractReports',
          isArray: true
        },
        getContractReportCorrections: {
          method: 'GET',
          url: 'api/financialCorrections/:id/contractReportCorrections',
          isArray: true
        },
        getContractDebts: {
          method: 'GET',
          url: 'api/financialCorrections/:id/contractDebts',
          isArray: true
        },
        getCertReports: {
          method: 'GET',
          url: 'api/financialCorrections/:id/certReports',
          isArray: true
        },
        getIrregularities: {
          method: 'GET',
          url: 'api/financialCorrections/:id/irregularities',
          isArray: true
        }
      }
    );
  }
];
