export const ProcedureLocationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/locations/:ind',
      {},
      {
        newProcedureLocation: {
          method: 'GET',
          url: 'api/procedures/:id/locations/new'
        },
        getLocations: {
          method: 'GET',
          url: 'api/procedures/:id/locations',
          isArray: true
        }
      }
    );
  }
];
