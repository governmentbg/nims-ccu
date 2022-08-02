import _ from 'lodash';

function EvalSessionProjectVersionEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProjectVersion,
  projectVersion
) {
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.projectVersion = projectVersion;
  $scope.editMode = null;
  $scope.versionId = $stateParams.vid;

  $scope.back = function() {
    return $state.go('root.evalSessions.view.projects.view', {
      id: $stateParams.id,
      ind: $stateParams.ind
    });
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProjectVersionForm.$validate().then(function() {
      if ($scope.editProjectVersionForm.$valid) {
        return ProjectVersion.update(
          {
            id: $stateParams.ind,
            vid: $stateParams.vid,
            evalSessionId: $stateParams.id
          },
          $scope.projectVersion
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProjectVersion',
      action: 'remove',
      params: {
        id: $stateParams.ind,
        vid: $stateParams.vid,
        evalSessionId: $stateParams.id,
        version: $scope.projectVersion.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.projects.view', $stateParams, {
          reload: true
        });
      }
    });
  };

  $scope.versionUpdated = function() {
    return $state.partialReload();
  };
}

EvalSessionProjectVersionEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProjectVersion',
  'projectVersion'
];

EvalSessionProjectVersionEditCtrl.$resolve = {
  projectVersion: [
    'ProjectVersion',
    'ProjectFile',
    '$stateParams',
    function(ProjectVersion, ProjectFile, $stateParams) {
      return ProjectVersion.get({
        id: $stateParams.ind,
        vid: $stateParams.vid
      }).$promise.then(function(version) {
        if (version.projectFile) {
          version.projectFile.url = ProjectFile.getUrl({
            id: $stateParams.ind,
            projectFileId: version.projectFile.id
          });

          _(version.projectFileSignatures).forEach(function(pfs) {
            pfs.url = ProjectFile.getUrl({
              id: $stateParams.ind,
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

export { EvalSessionProjectVersionEditCtrl };
