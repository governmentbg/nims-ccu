export const FlatFinancialCorrectionProgrammeItemFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/flatFinancialCorrections/:id/programmeItems/:ind',
      {},
      {
        calculate: {
          method: 'POST',
          url: 'api/flatFinancialCorrections/:id/programmeItems/:ind/calculate'
        }
      }
    );
  }
];
