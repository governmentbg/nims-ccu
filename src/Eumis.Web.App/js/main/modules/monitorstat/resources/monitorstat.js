export const MonitorstatFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/monitorstat/:id',
      {},
      {
        loadMonitorstatSurveys: {
          method: 'POST',
          url: 'api/monitorstat/:id/loadSurveys/'
        },
        canLoadMonitorstatSurveys: {
          method: 'POST',
          url: 'api/monitorstat/:id/canLoadSurveys/'
        }
      }
    );
  }
];
