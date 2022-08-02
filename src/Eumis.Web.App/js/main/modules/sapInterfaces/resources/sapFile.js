export const SapFileFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/sapFiles/:id',
      {},
      {
        getInfo: {
          method: 'GET',
          url: 'api/sapFiles/:id/info'
        },
        getPaidAmounts: {
          method: 'GET',
          url: 'api/sapFiles/:id/paidAmounts',
          isArray: true
        },
        getDistributedLimits: {
          method: 'GET',
          url: 'api/sapFiles/:id/distributedLimits',
          isArray: true
        },
        importSapFile: {
          method: 'POST',
          url: 'api/sapFiles/:id/import'
        }
      }
    );
  }
];
