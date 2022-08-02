export const ProcedureMonitorstatDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/monitorstatDocuments/:ind',
      {},
      {
        getMonitorstatReports: {
          method: 'GET',
          url: 'api/procedures/:id/monitorstatDocuments/getReports',
          isArray: true
        }
      }
    );
  }
];
