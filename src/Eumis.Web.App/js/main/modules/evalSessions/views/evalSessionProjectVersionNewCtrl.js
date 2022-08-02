function EvalSessionProjectVersionNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProjectVersion,
  newProjectVersion
) {
  $scope.newProjectVersion = newProjectVersion;

  $scope.save = function() {
    return $scope.newProjectVersionForm.$validate().then(function() {
      if ($scope.newProjectVersionForm.$valid) {
        return ProjectVersion.save(
          {
            evalSessionId: $stateParams.id,
            id: $stateParams.ind
          },
          $scope.newProjectVersion
        ).$promise.then(function(result) {
          return $state.go(
            'root.evalSessions.view.projects.view.versions.edit',
            {
              id: $stateParams.id,
              ind: $stateParams.ind,
              vid: result.versionId
            },
            {
              reload: true
            }
          );
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.projects.view');
  };
}

EvalSessionProjectVersionNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProjectVersion',
  'newProjectVersion'
];

EvalSessionProjectVersionNewCtrl.$resolve = {
  newProjectVersion: [
    'ProjectVersion',
    '$stateParams',
    function(ProjectVersion, $stateParams) {
      return ProjectVersion.newProjectVersion({
        id: $stateParams.ind
      }).$promise;
    }
  ]
};

export { EvalSessionProjectVersionNewCtrl };
