export const ContractDebtInterestFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractDebts/:id/interests/:ind',
      {},
      {
        newContractDebtInterest: {
          method: 'GET',
          url: 'api/contractDebts/:id/interests/new'
        },
        calculate: {
          method: 'POST',
          url: 'api/contractDebts/:id/interests/:ind/calculate'
        }
      }
    );
  }
];
