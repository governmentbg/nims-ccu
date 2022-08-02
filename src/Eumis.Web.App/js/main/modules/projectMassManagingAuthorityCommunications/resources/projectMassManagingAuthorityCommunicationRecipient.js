export const ProjectMassManagingAuthorityCommunicationRecipientFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projectMassManagingAuthorityCommunications/:id/recipients/:ind',
      {},
      {
        getUnattachedRecipients: {
          method: 'GET',
          url: 'api/projectMassManagingAuthorityCommunications/:id/recipients/unattachedProjects',
          isArray: true
        },
        loadRecipientsFromFile: {
          method: 'GET',
          url:
            'api/projectMassManagingAuthorityCommunications/:id/recipients/loadRecipientsFromFile'
        }
      }
    );
  }
];
