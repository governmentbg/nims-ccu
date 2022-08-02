export const ProcedureMonitorstatEconomicActivityFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/monitorstatEconomicActivities/:ind',
      {},
      {
        newProcedureMonitorstatEconomicActivity: {
          method: 'GET',
          url: 'api/procedures/:id/monitorstatEconomicActivities/new'
        },
        canCreate: {
          method: 'POST',
          url: 'api/procedures/:id/monitorstatEconomicActivities/canCreate'
        }
      }
    );
  }
];
