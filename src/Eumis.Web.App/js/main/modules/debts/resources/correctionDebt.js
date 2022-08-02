export const CorrectionDebtFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/correctionDebts/:id',
      {},
      {
        newCorrectionDebt: {
          method: 'GET',
          url: 'api/correctionDebts/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/correctionDebts/:id/info'
        },
        getContracts: {
          method: 'GET',
          url: 'api/correctionDebtContracts',
          isArray: true
        },
        cancel: {
          method: 'POST',
          url: 'api/correctionDebts/:id/cancel'
        },
        getReport: {
          method: 'GET',
          url: 'api/correctionDebts/report',
          isArray: true
        }
      }
    );
  }
];
