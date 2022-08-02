export const EvalSessionProjectStandingFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/projects/:ind/standings/:sid',
      {},
      {
        newEvalSessionProjectStanding: {
          method: 'GET',
          url: 'api/evalSessions/:id/projects/:ind/standings/new'
        },
        isOrderNumUnique: {
          method: 'GET',
          url: 'api/evalSessions/:id/projects/:ind/standings/isOrderNumUnique',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        canCreate: {
          method: 'POST',
          url: 'api/evalSessions/:id/projects/:ind/standings/canCreate'
        },
        cancel: {
          method: 'POST',
          url: 'api/evalSessions/:id/projects/:ind/standings/:sid/cancel'
        }
      }
    );
  }
];
