export const EvalSessionStandingFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/standings/:ind',
      {},
      {
        newEvalSessionStanding: {
          method: 'GET',
          url: 'api/evalSessions/:id/standings/new'
        },
        canRefuse: {
          method: 'POST',
          url: 'api/evalSessions/:id/standings/:ind/canRefuse'
        },
        moveUp: {
          method: 'POST',
          url: 'api/evalSessions/:id/standings/:ind/rearrange/:idx/moveUp'
        },
        refuse: {
          method: 'POST',
          url: 'api/evalSessions/:id/standings/:ind/refuse'
        },
        canCreate: {
          method: 'POST',
          url: 'api/evalSessions/:id/standings/canCreate'
        }
      }
    );
  }
];
