function ProcedureMonitorstatRequestEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProcedureMonitorstatRequest,
  monitorstatRequest
) {
  $scope.procedureId = $stateParams.id;
  $scope.request = monitorstatRequest;
  $scope.editMode = undefined;

  $scope.edit = function() {
    $scope.editMode = !$scope.editMode;
  };

  $scope.save = function() {
    return $scope.editProcedureMonitorsatRequestForm.$validate().then(function() {
      if ($scope.editProcedureMonitorsatRequestForm.$valid) {
        return ProcedureMonitorstatRequest.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.request
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.sendRequest = function() {
    return scConfirm({
      confirmMessage: 'procedures_editProcedureMonitorstatRequest_sendMessage',
      resource: 'ProcedureMonitorstatRequest',
      validationAction: 'canSendMonitorstatRequest',
      action: 'sendMonitorstatRequest',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.request.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProcedureMonitorstatRequestEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProcedureMonitorstatRequest',
  'monitorstatRequest'
];

ProcedureMonitorstatRequestEditCtrl.$resolve = {
  monitorstatRequest: [
    'ProcedureMonitorstatRequest',
    '$stateParams',
    function(ProcedureMonitorstatRequest, $stateParams) {
      return ProcedureMonitorstatRequest.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProcedureMonitorstatRequestEditCtrl };
