export const EvalSessionStandingRearrangeFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/standings/:ind/rearrange/:idx',
      {},
      {
        moveUp: {
          method: 'POST',
          url: 'api/evalSessions/:id/standings/:ind/rearrange/:idx/moveUp'
        },
        moveDown: {
          method: 'POST',
          url: 'api/evalSessions/:id/standings/:ind/rearrange/:idx/moveDown'
        },
        apply: {
          method: 'POST',
          url: 'api/evalSessions/:id/standings/:ind/rearrange/apply'
        },
        canApply: {
          method: 'POST',
          url: 'api/evalSessions/:id/standings/:ind/rearrange/canApply'
        }
      }
    );
  }
];
