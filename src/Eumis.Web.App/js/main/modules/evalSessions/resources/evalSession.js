export const EvalSessionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id',
      {},
      {
        newEvalSession: {
          method: 'GET',
          url: 'api/evalSessions/new'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/evalSessions/:id/changeStatusToDraft'
        },
        changeStatusToActive: {
          method: 'POST',
          url: 'api/evalSessions/:id/changeStatusToActive'
        },
        changeStatusToEnded: {
          method: 'POST',
          url: 'api/evalSessions/:id/changeStatusToEnded'
        },
        changeStatusToEndedByLAG: {
          method: 'POST',
          url: 'api/evalSessions/:id/changeStatusToEndedByLAG'
        },
        changeStatusToCanceled: {
          method: 'POST',
          url: 'api/evalSessions/:id/changeStatusToCanceled'
        },
        canChangeStatusToDraft: {
          method: 'POST',
          url: 'api/evalSessions/:id/canChangeStatusToDraft'
        },
        canChangeStatusToActive: {
          method: 'POST',
          url: 'api/evalSessions/:id/canChangeStatusToActive'
        },
        canChangeStatusToEnded: {
          method: 'POST',
          url: 'api/evalSessions/:id/canChangeStatusToEnded'
        },
        canChangeStatusToEndedByLAG: {
          method: 'POST',
          url: 'api/evalSessions/:id/canChangeStatusToEndedByLAG'
        },
        getInfo: {
          method: 'GET',
          url: 'api/evalSessions/:id/info'
        },
        getProjects: {
          method: 'GET',
          url: 'api/evalSessions/:id/getProjects',
          isArray: true
        }
      }
    );
  }
];
