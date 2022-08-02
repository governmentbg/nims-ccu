export const EvalSessionDistributionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/distributions/:ind',
      {},
      {
        newEvalSessionDistribution: {
          method: 'GET',
          url: 'api/evalSessions/:id/distributions/new'
        },
        refuse: {
          method: 'POST',
          url: 'api/evalSessions/:id/distributions/:ind/refuse'
        },
        canCreate: {
          method: 'POST',
          url: 'api/evalSessions/:id/distributions/canCreate'
        },
        canRefuse: {
          method: 'POST',
          url: 'api/evalSessions/:id/distributions/:ind/canRefuse'
        }
      }
    );
  }
];
