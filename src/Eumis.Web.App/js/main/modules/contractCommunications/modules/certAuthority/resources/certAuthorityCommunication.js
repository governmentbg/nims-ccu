export const CertAuthorityCommunicationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id/certAuthorityCommunications/:ind',
      {},
      {
        getContracts: {
          method: 'GET',
          url: 'api/certAuthorityCommunicationContracts',
          isArray: true
        }
      }
    );
  }
];
