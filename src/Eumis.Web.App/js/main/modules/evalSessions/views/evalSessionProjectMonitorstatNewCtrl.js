function EvalSessionProjectMonitorstatNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProjectMonitorstatRequest,
  projectMonitorstatRequest
) {
  $scope.projectId = $stateParams.ind;
  $scope.projectMonitorstatRequest = projectMonitorstatRequest;

  $scope.save = function() {
    return $scope.newEvalSessionProjectMonitorstatForm.$validate().then(function() {
      if ($scope.newEvalSessionProjectMonitorstatForm.$valid) {
        return ProjectMonitorstatRequest.save(
          { id: $stateParams.ind },
          $scope.projectMonitorstatRequest
        ).$promise.then(function(data) {
          return $state.go('root.evalSessions.view.projects.view.monitorstat.edit', {
            rid: data.projectMonitorstatRequestId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.projects.view');
  };
}

EvalSessionProjectMonitorstatNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProjectMonitorstatRequest',
  'projectMonitorstatRequest'
];

EvalSessionProjectMonitorstatNewCtrl.$resolve = {
  projectMonitorstatRequest: [
    'ProjectMonitorstatRequest',
    '$stateParams',
    function(ProjectMonitorstatRequest, $stateParams) {
      return ProjectMonitorstatRequest.newMonitorstatRequest({
        id: $stateParams.ind
      }).$promise;
    }
  ]
};

export { EvalSessionProjectMonitorstatNewCtrl };
