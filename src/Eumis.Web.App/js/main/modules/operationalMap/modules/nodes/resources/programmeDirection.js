export const ProgrammeDirectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmes/:id/directions/:ind',
      {},
      {
        newDirection: {
          method: 'GET',
          url: 'api/programmes/:id/directions/new'
        },
        canUpdate: {
          method: 'POST',
          url: 'api/programmes/:id/directions/:ind/canUpdate'
        },
        canCreate: {
          method: 'POST',
          url: 'api/programmes/:id/directions/canCreate'
        }
      }
    );
  }
];
