export const AdminAuthorityCommunicationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/adminAuthorityCommunications/:id',
      {},
      {
        getContracts: {
          method: 'GET',
          url: 'api/contracts/adminAuthorityCommunications',
          isArray: true
        }
      }
    );
  }
];
