export const ProgrammePriorityDirectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmePriorities/:id/directions/:ind',
      {},
      {
        newDirection: {
          method: 'GET',
          url: 'api/programmePriorities/:id/directions/new'
        },
        canUpdate: {
          method: 'POST',
          url: 'api/programmePriorities/:id/directions/:ind/canUpdate'
        },
        canCreate: {
          method: 'POST',
          url: 'api/programmePriorities/:id/directions/canCreate'
        }
      }
    );
  }
];
