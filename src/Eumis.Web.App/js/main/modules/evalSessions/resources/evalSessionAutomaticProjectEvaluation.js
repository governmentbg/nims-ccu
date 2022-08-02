export const EvalSessionAutomaticProjectEvaluationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/automaticProjectEvaluations',
      {},
      {
        canExecute: {
          method: 'POST',
          url: 'api/evalSessions/:id/automaticProjectEvaluations/canExecute'
        }
      }
    );
  }
];
