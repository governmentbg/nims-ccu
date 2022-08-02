export const ActionLogFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/actionLogs/:id',
      {},
      {
        getInternal: {
          method: 'GET',
          url: 'api/actionLogs/internal',
          isArray: true
        },
        getPortal: {
          method: 'GET',
          url: 'api/actionLogs/portal',
          isArray: true
        },
        getUnsuccessfulLogin: {
          method: 'GET',
          url: 'api/actionLogs/unsuccessfulLogin',
          isArray: true
        }
      }
    );
  }
];
