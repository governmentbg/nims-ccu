export const FlatFinancialCorrectionProcedureItemFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/flatFinancialCorrections/:id/procedureItems/:ind',
      {},
      {
        getProcedures: {
          method: 'GET',
          url: 'api/flatFinancialCorrections/:id/procedureItems/procedures',
          isArray: true
        },
        calculate: {
          method: 'POST',
          url: 'api/flatFinancialCorrections/:id/procedureItems/:ind/calculate'
        }
      }
    );
  }
];
