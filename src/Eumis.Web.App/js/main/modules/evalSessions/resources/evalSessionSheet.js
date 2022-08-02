export const EvalSessionSheetFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/sheets/:ind',
      {},
      {
        newEvalSessionSheet: {
          method: 'GET',
          url: 'api/evalSessions/:id/sheets/new'
        },
        continueSheet: {
          method: 'POST',
          url: 'api/evalSessions/:id/sheets/:ind/continueSheet'
        },
        cancelSheet: {
          method: 'POST',
          url: 'api/evalSessions/:id/sheets/:ind/cancelSheet'
        },
        canCreate: {
          method: 'POST',
          url: 'api/evalSessions/:id/sheets/canCreate',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        getProjectEvaluation: {
          method: 'GET',
          url: 'api/evalSessions/:id/sheets/getProjectEvaluation'
        },
        canCancel: {
          method: 'POST',
          url: 'api/evalSessions/:id/sheets/:ind/canCancel'
        },
        canContinue: {
          method: 'POST',
          url: 'api/evalSessions/:id/sheets/:ind/canContinue'
        }
      }
    );
  }
];
