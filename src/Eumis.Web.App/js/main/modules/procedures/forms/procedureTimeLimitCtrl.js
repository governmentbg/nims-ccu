function ProcedureTimeLimitCtrl($q, $scope, scFormParams, ProcedureTimeLimit) {
  $scope.isValidEndTime = function(endTime) {
    if (!$scope.model.endDate || !endTime) {
      return $q.resolve();
    }

    return ProcedureTimeLimit.isValidEndTime({
      id: $scope.model.procedureId,
      endDate: $scope.model.endDate,
      endTime: endTime,
      procedureTimeLimitId: $scope.model.procedureTimeLimitId
    }).$promise.then(function(isValid) {
      return isValid ? $q.resolve() : $q.reject();
    });
  };
}

ProcedureTimeLimitCtrl.$inject = ['$q', '$scope', 'scFormParams', 'ProcedureTimeLimit'];

export { ProcedureTimeLimitCtrl };
