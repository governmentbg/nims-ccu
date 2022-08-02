function AllowanceRateCtrl($q, $scope, AllowanceRate) {
  $scope.isCorrectDate = function(date) {
    if (!date || $scope.model.allowanceRateId) {
      return $q.resolve();
    }
    return AllowanceRate.isCorrectDate(
      {
        id: $scope.model.allowanceId
      },
      JSON.stringify(date)
    ).$promise.then(function(result) {
      return result ? $q.resolve() : $q.reject();
    });
  };
}

AllowanceRateCtrl.$inject = ['$q', '$scope', 'AllowanceRate'];

export { AllowanceRateCtrl };
