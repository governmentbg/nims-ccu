function ProcedureMonitorstatRequestNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureMonitorstatRequest,
  monitorstatRequest
) {
  $scope.procedureId = $stateParams.id;
  $scope.request = monitorstatRequest;

  $scope.save = function() {
    return $scope.newProcedureMonitorsatRequestForm.$validate().then(function() {
      if ($scope.newProcedureMonitorsatRequestForm.$valid) {
        return ProcedureMonitorstatRequest.save(
          { id: $stateParams.id },
          $scope.request
        ).$promise.then(function(data) {
          return $state.go('root.procedures.view.monitorstat.edit', {
            ind: data.procedureMonitorstatRequestId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.monitorstat.search');
  };
}

ProcedureMonitorstatRequestNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureMonitorstatRequest',
  'monitorstatRequest'
];

ProcedureMonitorstatRequestNewCtrl.$resolve = {
  monitorstatRequest: [
    'ProcedureMonitorstatRequest',
    '$stateParams',
    function(ProcedureMonitorstatRequest, $stateParams) {
      return ProcedureMonitorstatRequest.newMonitorstatRequest({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureMonitorstatRequestNewCtrl };
