export const SubDirectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/directions/:id/subDirections/:ind',
      {},
      {
        newSubDirection: {
          method: 'GET',
          url: 'api/directions/:id/subDirections/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/directions/:id/subDirections/:ind/canDelete',
          isArray: true
        }
      }
    );
  }
];
