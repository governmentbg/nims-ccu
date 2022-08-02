export const EvalSessionUserFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/users/:ind',
      {},
      {
        newEvalSessionUser: {
          method: 'GET',
          url: 'api/evalSessions/:id/users/new'
        },
        canDelete: {
          method: 'POST',
          url: 'api/evalSessions/:id/users/:ind/canDelete'
        },
        canAdd: {
          method: 'POST',
          url: 'api/evalSessions/:id/users/canAdd'
        },
        activate: {
          method: 'POST',
          url: 'api/evalSessions/:id/users/:ind/activate'
        },
        deactivate: {
          method: 'POST',
          url: 'api/evalSessions/:id/users/:ind/deactivate'
        }
      }
    );
  }
];
