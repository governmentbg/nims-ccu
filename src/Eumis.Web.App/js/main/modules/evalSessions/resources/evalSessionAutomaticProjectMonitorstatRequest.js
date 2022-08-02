export const EvalSessionAutomaticProjectMonitorstatRequestFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/evalSessions/:id/automaticProjectMonitorstatRequests',
      {},
      {
        getProjects: {
          method: 'GET',
          url: 'api/evalSessions/:id/automaticProjectMonitorstatRequests/projects',
          isArray: true
        },
        loadProjectsFromFile: {
          method: 'GET',
          url: 'api/evalSessions/:id/automaticProjectMonitorstatRequests/loadProjectsFromFile'
        },
        sendAutomaticRequests: {
          method: 'POST',
          url: 'api/evalSessions/:id/automaticProjectMonitorstatRequests/sendAutomaticRequests'
        }
      }
    );
  }
];
