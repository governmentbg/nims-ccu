export const EvalSessionResultFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/results/:ind',
      {},
      {
        newEvalSessionResult: {
          method: 'GET',
          url: 'api/evalSessions/:id/results/new'
        },
        getAdminAdmissProjects: {
          method: 'GET',
          url: 'api/evalSessions/:id/results/:ind/adminAdmissProjects',
          isArray: true
        },
        getPreliminaryProjects: {
          method: 'GET',
          url: 'api/evalSessions/:id/results/:ind/preliminaryProjects',
          isArray: true
        },
        getStandingProjects: {
          method: 'GET',
          url: 'api/evalSessions/:id/results/:ind/standingProjects',
          isArray: true
        },
        canCreate: {
          method: 'POST',
          url: 'api/evalSessions/:id/results/canCreate'
        },
        canPublish: {
          method: 'POST',
          url: 'api/evalSessions/:id/results/:ind/canPublish'
        },
        publish: {
          method: 'POST',
          url: 'api/evalSessions/:id/results/:ind/publish'
        },
        loadProjects: {
          method: 'POST',
          url: 'api/evalSessions/:id/results/:ind/loadProjects'
        },
        clearProjects: {
          method: 'POST',
          url: 'api/evalSessions/:id/results/:ind/clearProjects'
        },
        canLoadProjects: {
          method: 'POST',
          url: 'api/evalSessions/:id/results/:ind/canLoadProjects'
        },
        cancel: {
          method: 'POST',
          url: 'api/evalSessions/:id/results/:ind/cancel'
        }
      }
    );
  }
];
