export const DeclarationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/declarations/:id',
      {},
      {
        newDeclaration: {
          method: 'GET',
          url: 'api/declarations/new'
        },
        newFile: {
          method: 'GET',
          url: 'api/declarations/newFile'
        },
        newPublication: {
          method: 'GET',
          url: 'api/declarations/:id/newPublication'
        },
        publish: {
          method: 'POST',
          url: 'api/declarations/:id/publish'
        },
        archive: {
          method: 'POST',
          url: 'api/declarations/:id/archive'
        }
      }
    );
  }
];
