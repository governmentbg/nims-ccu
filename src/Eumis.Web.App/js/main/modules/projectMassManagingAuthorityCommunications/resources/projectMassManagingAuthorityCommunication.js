export const ProjectMassManagingAuthorityCommunicationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projectMassManagingAuthorityCommunications/:id',
      {},
      {
        newProjectMassManagingAuthorityCommunication: {
          method: 'GET',
          url: 'api/projectMassManagingAuthorityCommunications/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/projectMassManagingAuthorityCommunications/:id/info'
        },
        canSend: {
          method: 'POST',
          url: 'api/projectMassManagingAuthorityCommunications/:id/canSend'
        },
        send: {
          method: 'POST',
          url: 'api/projectMassManagingAuthorityCommunications/:id/send'
        }
      }
    );
  }
];
