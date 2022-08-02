import _ from 'lodash';

function ProjectDossierProjectVersionEditCtrl($scope, $state, $stateParams, projectVersion) {
  $scope.projectVersion = projectVersion;

  $scope.back = function() {
    return $state.go('root.projectDossier.view.project', {
      id: $stateParams.id
    });
  };
}

ProjectDossierProjectVersionEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'projectVersion'
];

ProjectDossierProjectVersionEditCtrl.$resolve = {
  projectVersion: [
    'ProjectVersion',
    'ProjectFile',
    '$stateParams',
    function(ProjectVersion, ProjectFile, $stateParams) {
      return ProjectVersion.get({
        id: $stateParams.id,
        vid: $stateParams.vid
      }).$promise.then(function(version) {
        if (version.projectFile) {
          version.projectFile.url = ProjectFile.getUrl({
            id: $stateParams.id,
            projectFileId: version.projectFile.id
          });

          _(version.projectFileSignatures).forEach(function(pfs) {
            pfs.url = ProjectFile.getUrl({
              id: $stateParams.id,
              projectFileId: version.projectFile.id,
              projectFileSignatureId: pfs.id
            });
          });
        }

        return version;
      });
    }
  ]
};

export { ProjectDossierProjectVersionEditCtrl };
