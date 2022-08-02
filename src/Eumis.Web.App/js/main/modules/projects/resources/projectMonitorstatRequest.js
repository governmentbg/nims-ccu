export const ProjectMonitorstatRequestFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projects/:id/monitorstatRequests/:ind',
      {},
      {
        newMonitorstatRequest: {
          method: 'GET',
          url: 'api/projects/:id/monitorstatRequests/new'
        },
        canSendMonitorstatRequest: {
          method: 'POST',
          url: 'api/projects/:id/monitorstatRequests/:ind/canSendRequest'
        },
        sendMonitorstatRequest: {
          method: 'POST',
          url: 'api/projects/:id/monitorstatRequests/:ind/sendRequest'
        },
        getMonitorstatRequestsForStandpoint: {
          method: 'GET',
          url: 'api/evalSessionStandpoints/:id/monitorstatRequests',
          isArray: true
        },
        getMonitorstatRequestsForSheet: {
          method: 'GET',
          url: 'api/evalSessionSheets/:id/monitorstatRequests',
          isArray: true
        },
        newMonitorstatMassRequest: {
          method: 'GET',
          url: 'api/projects/:id/monitorstatRequests/newMonitorstatMassRequest'
        },
        canSendMonitorstatMassRequest: {
          method: 'POST',
          url: 'api/projects/:id/monitorstatRequests/:ind/canSendMassRequest'
        },
        sendMonitorstatMassRequest: {
          method: 'POST',
          url: 'api/projects/:id/monitorstatRequests/:ind/sendMassRequest'
        }
      }
    );
  }
];
