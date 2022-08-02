export const EvalSessionEvaluationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/evaluations/:ind',
      {},
      {
        newEvalSessionEvaluation: {
          method: 'GET',
          url: 'api/evalSessions/:id/evaluations/new'
        },
        cancelEvaluation: {
          method: 'POST',
          url: 'api/evalSessions/:id/evaluations/:ind/cancel'
        },
        getEvaluativeProjects: {
          method: 'GET',
          url: 'api/evalSessions/:id/evaluations/getEvaluativeProjects',
          isArray: true
        },
        canEvaluateProject: {
          method: 'GET',
          url: 'api/evalSessions/:id/evaluations/canEvaluateProject'
        },
        canCancel: {
          method: 'POST',
          url: 'api/evalSessions/:id/evaluations/:ind/canCancel'
        },
        canCreate: {
          method: 'POST',
          url: 'api/evalSessions/:id/evaluations/canCreate'
        },
        bulkEvaluation: {
          method: 'POST',
          url: 'api/evalSessions/:id/evaluations/bulkEvaluation',
          isArray: true
        }
      }
    );
  }
];
