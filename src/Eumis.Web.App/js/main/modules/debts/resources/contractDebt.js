export const ContractDebtFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractDebts/:id',
      {},
      {
        canCreate: {
          method: 'POST',
          url: 'api/contractDebts/canCreate'
        },
        newContractDebt: {
          method: 'GET',
          url: 'api/contractDebts/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contractDebts/:id/info'
        },
        getContracts: {
          method: 'GET',
          url: 'api/contractDebtContracts',
          isArray: true
        },
        cancel: {
          method: 'POST',
          url: 'api/contractDebts/:id/cancel'
        },
        getReport: {
          method: 'GET',
          url: 'api/contractDebts/report',
          isArray: true
        }
      }
    );
  }
];
