export const ProgrammePriorityDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmePriorities/:id/documents/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/programmePriorities/:id/documents/new'
        }
      }
    );
  }
];
