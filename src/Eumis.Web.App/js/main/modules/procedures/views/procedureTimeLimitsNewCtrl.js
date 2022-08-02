function ProcedureTimeLimitsNewCtrl(
  $scope,
  $state,
  $stateParams,
  Procedure,
  ProcedureTimeLimit,
  newProcedureTimeLimit
) {
  $scope.newProcedureTimeLimit = newProcedureTimeLimit;

  $scope.save = function() {
    return $scope.newProcedureTimeLimitForm.$validate().then(function() {
      if ($scope.newProcedureTimeLimitForm.$valid) {
        return ProcedureTimeLimit.save(
          { id: $stateParams.id },
          $scope.newProcedureTimeLimit
        ).$promise.then(function() {
          return $state.go('root.procedures.view.procedureTimeLimits.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.procedureTimeLimits.search');
  };
}

ProcedureTimeLimitsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'Procedure',
  'ProcedureTimeLimit',
  'newProcedureTimeLimit'
];

ProcedureTimeLimitsNewCtrl.$resolve = {
  newProcedureTimeLimit: [
    '$stateParams',
    'ProcedureTimeLimit',
    function($stateParams, ProcedureTimeLimit) {
      return ProcedureTimeLimit.newProcedureTimeLimit({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureTimeLimitsNewCtrl };
