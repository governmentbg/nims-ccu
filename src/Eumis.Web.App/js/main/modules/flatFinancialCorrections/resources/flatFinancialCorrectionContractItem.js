export const FlatFinancialCorrectionContractItemFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/flatFinancialCorrections/:id/contractItems/:ind',
      {},
      {
        getContracts: {
          method: 'GET',
          url: 'api/flatFinancialCorrections/:id/contractItems/contracts',
          isArray: true
        },
        calculate: {
          method: 'POST',
          url: 'api/flatFinancialCorrections/:id/contractItems/:ind/calculate'
        }
      }
    );
  }
];
