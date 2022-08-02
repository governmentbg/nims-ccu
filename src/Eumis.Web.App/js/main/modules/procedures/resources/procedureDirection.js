export const ProcedureDirectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/directions/:ind',
      {},
      {
        getDirections: {
          method: 'GET',
          url: 'api/procedures/:id/directions/unattachedDirections',
          isArray: true
        }
      }
    );
  }
];
