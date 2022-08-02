export const ProjectDossierDcoumentFactory = [
  '$resource',
  function($resource) {
    return $resource('api/projectDossier/:id/documents');
  }
];
