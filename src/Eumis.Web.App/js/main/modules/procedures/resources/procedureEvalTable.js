export const ProcedureEvalTableFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/evalTables/:ind',
      {},
      {
        newEvalTable: {
          method: 'GET',
          url: 'api/procedures/:id/evalTables/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/evalTables/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/evalTables/:ind/activate'
        }
      }
    );
  }
];
