import _ from 'lodash';

function ProjectDossierProjectViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  ProjectFile,
  ProjectCommunicationFile,
  projectVersions,
  projectCommunications,
  projectEvaulations,
  projectEvalSessionProjects,
  projectStandings
) {
  $scope.projectId = $stateParams.id;

  $scope.projectRegistration = $scope.projectRegData;
  $scope.projectEvaulations = projectEvaulations;
  $scope.projectEvalSessionProjects = projectEvalSessionProjects;
  $scope.projectStandings = projectStandings;

  $scope.projectVersionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/projectVersions/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.projectCommunicationsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/projectCommunications/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.projectEvaulationsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/projectEvaulations/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.projectStandingsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/projectStandings/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.projectVersions = _.map(projectVersions, function(item) {
    if (item.projectFile) {
      item.projectFile.url = ProjectFile.getUrl({
        id: $stateParams.id,
        projectFileId: item.projectFile.id
      });

      _(item.projectFileSignatures).forEach(function(pfs) {
        pfs.url = ProjectFile.getUrl({
          id: $stateParams.id,
          projectFileId: item.projectFile.id,
          projectFileSignatureId: pfs.id
        });
      });
    }
    return item;
  });

  $scope.projectCommunications = _.map(projectCommunications, function(item) {
    if (item.projectCommunicationFile) {
      item.projectCommunicationFile.url = ProjectCommunicationFile.getUrl({
        id: $stateParams.id,
        ind: item.communicationId,
        projectCommunicationFileId: item.projectCommunicationFile.id
      });

      _(item.projectCommunicationFileSignatures).forEach(function(pcfs) {
        pcfs.url = ProjectCommunicationFile.getUrl({
          id: $stateParams.id,
          ind: item.communicationId,
          projectCommunicationFileId: item.projectCommunicationFile.id,
          projectCommunicationFileSignatureId: pcfs.id
        });
      });
    }
    return item;
  });
}

ProjectDossierProjectViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'ProjectFile',
  'ProjectCommunicationFile',
  'projectVersions',
  'projectCommunications',
  'projectEvaulations',
  'projectEvalSessionProjects',
  'projectStandings'
];

ProjectDossierProjectViewCtrl.$resolve = {
  projectVersions: [
    'ProjectVersion',
    '$stateParams',
    function(ProjectVersion, $stateParams) {
      return ProjectVersion.query({ id: $stateParams.id }).$promise;
    }
  ],
  projectCommunications: [
    'ProjectCommunication',
    '$stateParams',
    function(ProjectCommunication, $stateParams) {
      return ProjectCommunication.query({ id: $stateParams.id }).$promise;
    }
  ],
  projectEvaulations: [
    'ProjectDossier',
    '$stateParams',
    function(ProjectDossier, $stateParams) {
      return ProjectDossier.getProjectEvalSessionEvaluations({
        id: $stateParams.id
      }).$promise;
    }
  ],
  projectEvalSessionProjects: [
    'ProjectDossier',
    '$stateParams',
    function(ProjectDossier, $stateParams) {
      return ProjectDossier.getProjectEvalSessionProjects({
        id: $stateParams.id
      }).$promise;
    }
  ],
  projectStandings: [
    'ProjectDossier',
    '$stateParams',
    function(ProjectDossier, $stateParams) {
      return ProjectDossier.getProjectEvalSessionStandings({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProjectDossierProjectViewCtrl };
