export const ProgrammeDeclarationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmes/:id/declarations/:ind',
      {},
      {
        newDeclaration: {
          method: 'GET',
          url: 'api/programmes/:id/declarations/new'
        },
        canAdd: {
          method: 'POST',
          url: 'api/programmes/:id/declarations/canAdd'
        },
        canDelete: {
          method: 'POST',
          url: 'api/programmes/:id/declarations/:ind/canDelete'
        },
        activate: {
          method: 'PUT',
          url: 'api/programmes/:id/declarations/:ind/activate'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/programmes/:id/declarations/:ind/deactivate'
        },
        getRelatedProcedures: {
          method: 'GET',
          url: 'api/programmes/:id/declarations/:ind/relatedProcedures',
          isArray: true
        }
      }
    );
  }
];
