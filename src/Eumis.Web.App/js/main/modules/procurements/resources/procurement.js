export const ProcurementFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procurements/:id',
      {},
      {
        newProcurement: {
          method: 'GET',
          url: 'api/procurements/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/procurements/:id/info'
        },
        changeStatusToDraft: {
          method: 'PUT',
          url: 'api/procurements/:id/changeStatusToDraft'
        },
        changeStatusToActive: {
          method: 'PUT',
          url: 'api/procurements/:id/changeStatusToActive'
        },
        canChangeStatusToActive: {
          method: 'POST',
          url: 'api/procurements/:id/canChangeStatusToActive'
        },
        changeStatusToCanceled: {
          method: 'PUT',
          url: 'api/procurements/:id/changeStatusToCanceled'
        }
      }
    );
  }
];
