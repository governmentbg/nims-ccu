export const ProjectFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/projects/:id/versionFiles');
  }
];
