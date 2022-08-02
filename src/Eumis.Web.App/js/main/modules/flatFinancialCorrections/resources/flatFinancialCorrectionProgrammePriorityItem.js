export const FlatFinancialCorrectionProgrammePriorityItemFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/flatFinancialCorrections/:id/programmePriorityItems/:ind',
      {},
      {
        getProgrammePriorities: {
          method: 'GET',
          url: 'api/flatFinancialCorrections/:id/programmePriorityItems/programmePriorities',
          isArray: true
        },
        calculate: {
          method: 'POST',
          url: 'api/flatFinancialCorrections/:id/programmePriorityItems/:ind/calculate'
        }
      }
    );
  }
];
