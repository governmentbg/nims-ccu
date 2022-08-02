export const ProjectRegistrationCommunicationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projects/:id/managingAuthorityCommunications/:ind',
      {},
      {
        canCreate: {
          method: 'POST',
          url: 'api/projects/:id/managingAuthorityCommunications/canCreate'
        },
        cancelCommunication: {
          method: 'POST',
          url: 'api/projects/:id/managingAuthorityCommunications/:ind/cancel'
        }
      }
    );
  }
];
