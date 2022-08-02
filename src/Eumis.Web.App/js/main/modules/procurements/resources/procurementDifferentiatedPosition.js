export const ProcurementDifferentiatedPositionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procurements/:id/differentiatedPositions/:ind',
      {},
      {
        newPosition: {
          method: 'GET',
          url: 'api/procurements/:id/differentiatedPositions/new'
        }
      }
    );
  }
];
