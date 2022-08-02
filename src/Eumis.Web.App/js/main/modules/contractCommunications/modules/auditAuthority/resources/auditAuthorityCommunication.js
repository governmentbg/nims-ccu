export const AuditAuthorityCommunicationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id/auditAuthorityCommunications/:ind',
      {},
      {
        getContracts: {
          method: 'GET',
          url: 'api/auditAuthorityCommunicationContracts',
          isArray: true
        }
      }
    );
  }
];
