export const ProjectDossierFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/projectDossier/:id/files/:fileKey');
  }
];
