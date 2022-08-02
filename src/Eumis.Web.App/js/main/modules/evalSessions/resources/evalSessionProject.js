export const EvalSessionProjectFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/projects/:ind',
      {},
      {
        canDelete: {
          method: 'POST',
          url: 'api/evalSessions/:id/projects/:ind/canDelete'
        },
        canCancel: {
          method: 'POST',
          url: 'api/evalSessions/:id/projects/:ind/canCancel'
        },
        cancel: {
          method: 'POST',
          url: 'api/evalSessions/:id/projects/:ind/cancel'
        },
        canRestore: {
          method: 'POST',
          url: 'api/evalSessions/:id/projects/:ind/canRestore'
        },
        restore: {
          method: 'POST',
          url: 'api/evalSessions/:id/projects/:ind/restore'
        }
      }
    );
  }
];
