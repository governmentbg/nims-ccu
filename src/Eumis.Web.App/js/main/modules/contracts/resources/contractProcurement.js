export const ContractProcurementFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id/procurements/:pid',
      {},
      {
        newContractProcurement: {
          method: 'GET',
          url: 'api/contracts/:id/procurements/new'
        },
        markAsChecked: {
          method: 'POST',
          url: 'api/contracts/:id/procurements/:pid/markAsChecked'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contracts/:id/procurements/:pid/changeStatusToDraft'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contracts/:id/procurements/canCreate'
        }
      }
    );
  }
];
