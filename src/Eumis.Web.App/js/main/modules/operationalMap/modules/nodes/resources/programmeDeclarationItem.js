export const ProgrammeDeclarationItemFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmes/:id/declarations/:ind/items/:did',
      {},
      {
        newDeclarationItem: {
          method: 'GET',
          url: 'api/programmes/:id/declarations/:ind/items/new'
        },
        canAdd: {
          method: 'POST',
          url: 'api/programmes/:id/declarations/:ind/items/canAdd'
        },
        canDelete: {
          method: 'POST',
          url: 'api/programmes/:id/declarations/:ind/items/:did/canDelete'
        },
        canUpdate: {
          method: 'POST',
          url: 'api/programmes/:id/declarations/:ind/items/:did/canUpdate'
        },
        canActivate: {
          method: 'POST',
          url: 'api/programmes/:id/declarations/:ind/items/:did/canActivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/programmes/:id/declarations/:ind/items/:did/activate'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/programmes/:id/declarations/:ind/items/:did/deactivate'
        },
        loadItems: {
          method: 'POST',
          url: 'api/programmes/:id/declarations/:ind/items/loadItems'
        }
      }
    );
  }
];
