export const ProjectManagingAuthorityCommunicationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projectManagingAuthorityCommunications/:id',
      {},
      {
        getCommunications: {
          method: 'GET',
          url: 'api/projectManagingAuthorityCommunications',
          isArray: true
        }
      }
    );
  }
];
