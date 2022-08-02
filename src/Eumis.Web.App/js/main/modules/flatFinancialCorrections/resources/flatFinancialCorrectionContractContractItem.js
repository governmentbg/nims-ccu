export const FlatFinancialCorrectionContractContractItemFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/flatFinancialCorrections/:id/contractContractItems/:ind',
      {},
      {
        getContractContracts: {
          method: 'GET',
          url: 'api/flatFinancialCorrections/:id/contractContractItems/contractContracts',
          isArray: true
        },
        calculate: {
          method: 'POST',
          url: 'api/flatFinancialCorrections/:id/contractContractItems/:ind/calculate'
        }
      }
    );
  }
];
