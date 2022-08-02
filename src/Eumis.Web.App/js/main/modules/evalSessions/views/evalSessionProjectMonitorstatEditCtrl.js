import _ from 'lodash';

function EvalSessionProjectMonitorstatEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProjectMonitorstatRequest,
  ProjectMonitorstatResponseFile,
  evalSessionProjectMonitorstat
) {
  $scope.procedureId = $stateParams.id;
  $scope.evalSessionProjectMonitorstat = evalSessionProjectMonitorstat;

  $scope.responses = _.map(evalSessionProjectMonitorstat.responses, function(item) {
    item.fileUrl = ProjectMonitorstatResponseFile.getUrl({
      id: $stateParams.ind,
      fileKey: item.fileKey
    });
    return item;
  });

  $scope.editMode = undefined;

  $scope.edit = function() {
    $scope.editMode = !$scope.editMode;
  };

  $scope.save = function() {
    return $scope.editEvalSessionProjectMonitorstatForm.$validate().then(function() {
      if ($scope.editEvalSessionProjectMonitorstatForm.$valid) {
        return ProjectMonitorstatRequest.update(
          { id: $stateParams.ind, ind: $stateParams.rid },
          $scope.evalSessionProjectMonitorstat
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.send = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_projectMonitorstatEdit_sendMessage',
      resource: 'ProjectMonitorstatRequest',
      validationAction: 'canSendMonitorstatRequest',
      action: 'sendMonitorstatRequest',
      params: {
        id: $stateParams.ind,
        ind: $stateParams.rid,
        version: $scope.evalSessionProjectMonitorstat.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.back = function() {
    return $state.go('root.evalSessions.view.projects.view');
  };
}

EvalSessionProjectMonitorstatEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProjectMonitorstatRequest',
  'ProjectMonitorstatResponseFile',
  'evalSessionProjectMonitorstat'
];

EvalSessionProjectMonitorstatEditCtrl.$resolve = {
  evalSessionProjectMonitorstat: [
    'ProjectMonitorstatRequest',
    '$stateParams',
    function(ProjectMonitorstatRequest, $stateParams) {
      return ProjectMonitorstatRequest.get({
        id: $stateParams.ind,
        ind: $stateParams.rid
      }).$promise;
    }
  ]
};

export { EvalSessionProjectMonitorstatEditCtrl };
