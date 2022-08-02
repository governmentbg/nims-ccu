export const ProjectMassManagingAuthorityCommunicationFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/projectMassManagingAuthorityCommunications/:id/files/:fileKey');
  }
];
