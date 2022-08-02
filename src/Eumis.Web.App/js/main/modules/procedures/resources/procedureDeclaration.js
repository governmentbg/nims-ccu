export const ProcedureDeclarationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/declarations/:ind',
      {},
      {
        newProcedureDeclaration: {
          method: 'GET',
          url: 'api/procedures/:id/declarations/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/declarations/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/declarations/:ind/activate'
        }
      }
    );
  }
];
