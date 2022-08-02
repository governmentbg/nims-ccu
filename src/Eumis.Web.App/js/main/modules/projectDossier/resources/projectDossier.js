export const ProjectDossierFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projectDossier/:id',
      {},
      {
        getProjects: {
          method: 'GET',
          url: 'api/projectDossierProjects',
          isArray: true
        },
        getProject: {
          method: 'GET',
          url: 'api/projectDossier/:id/project'
        },
        getProjectEvalSessionProjects: {
          method: 'GET',
          url: 'api/projectDossier/:id/evalSessionProjects',
          isArray: true
        },
        getProjectEvalSessionEvaluations: {
          method: 'GET',
          url: 'api/projectDossier/:id/evalSessionEvaluations',
          isArray: true
        },
        getProjectEvalSessionEvaluation: {
          method: 'GET',
          url: 'api/projectDossier/:id/evalSessionEvaluations/:ind'
        },
        getProjectEvalSessionStandings: {
          method: 'GET',
          url: 'api/projectDossier/:id/evalSessionStandings',
          isArray: true
        },
        getProjectEvalSessionStanding: {
          method: 'GET',
          url: 'api/projectDossier/:id/evalSessionStandings/:ind'
        }
      }
    );
  }
];
