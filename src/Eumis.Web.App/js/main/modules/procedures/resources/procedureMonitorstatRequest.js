export const ProcedureMonitorstatRequestFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/monitorstatRequests/:ind',
      {},
      {
        newMonitorstatRequest: {
          method: 'GET',
          url: 'api/procedures/:id/monitorstatRequests/new'
        },
        canSendMonitorstatRequest: {
          method: 'POST',
          url: 'api/procedures/:id/monitorstatRequests/:ind/canSendRequest'
        },
        sendMonitorstatRequest: {
          method: 'POST',
          url: 'api/procedures/:id/monitorstatRequests/:ind/sendRequest'
        }
      }
    );
  }
];
