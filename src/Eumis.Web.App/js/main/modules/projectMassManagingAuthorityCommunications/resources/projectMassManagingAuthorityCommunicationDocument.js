export const ProjectMassManagingAuthorityCommunicationDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projectMassManagingAuthorityCommunications/:id/documents/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/projectMassManagingAuthorityCommunications/:id/documents/new'
        }
      }
    );
  }
];
