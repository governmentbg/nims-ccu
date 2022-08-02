function InterestRateCtrl($q, $scope, InterestRate) {
  $scope.isCorrectDate = function(date) {
    if (!date || $scope.model.interestRateId) {
      return $q.resolve();
    }
    return InterestRate.isCorrectDate(
      {
        id: $scope.model.basicInterestRateId
      },
      JSON.stringify(date)
    ).$promise.then(function(result) {
      return result ? $q.resolve() : $q.reject();
    });
  };
}

InterestRateCtrl.$inject = ['$q', '$scope', 'InterestRate'];

export { InterestRateCtrl };
