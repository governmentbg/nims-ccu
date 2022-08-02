export const ProjectCommunicationFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/projects/:id/communications/:ind/versionFiles');
  }
];
