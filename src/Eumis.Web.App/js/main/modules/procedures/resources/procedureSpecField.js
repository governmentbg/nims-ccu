export const ProcedureSpecFieldFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/specFields/:ind',
      {},
      {
        newProcedureSpecField: {
          method: 'GET',
          url: 'api/procedures/:id/specFields/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/specFields/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/specFields/:ind/activate'
        }
      }
    );
  }
];
