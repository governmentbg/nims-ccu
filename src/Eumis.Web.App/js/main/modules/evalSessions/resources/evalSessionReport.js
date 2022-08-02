export const EvalSessionReportFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/reports/:ind',
      {},
      {
        newEvalSessionReport: {
          method: 'GET',
          url: 'api/evalSessions/:id/reports/new'
        },
        cancelReport: {
          method: 'POST',
          url: 'api/evalSessions/:id/reports/:ind/cancel'
        },
        canCreate: {
          method: 'POST',
          url: 'api/evalSessions/:id/reports/canCreate'
        },
        canCancel: {
          method: 'POST',
          url: 'api/evalSessions/:id/reports/:ind/canCancel'
        }
      }
    );
  }
];
