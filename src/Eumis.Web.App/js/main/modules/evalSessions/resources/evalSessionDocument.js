export const EvalSessionDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/documents/:ind',
      {},
      {
        newEvalSessionDocument: {
          method: 'GET',
          url: 'api/evalSessions/:id/documents/new'
        },
        cancelDocument: {
          method: 'POST',
          url: 'api/evalSessions/:id/documents/:ind/cancel'
        }
      }
    );
  }
];
