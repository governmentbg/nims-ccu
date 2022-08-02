function NewInterestRateModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  InterestRate,
  newInterestRate
) {
  $scope.newInterestRate = newInterestRate;

  $scope.save = function() {
    return $scope.newInterestRateForm.$validate().then(function() {
      if ($scope.newInterestRateForm.$valid) {
        return InterestRate.save(
          {
            id: scModalParams.basicInterestRateId
          },
          $scope.newInterestRate
        ).$promise.then(function() {
          return $uibModalInstance.close();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

NewInterestRateModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'InterestRate',
  'newInterestRate'
];

NewInterestRateModalCtrl.$resolve = {
  newInterestRate: [
    'scModalParams',
    'InterestRate',
    function(scModalParams, InterestRate) {
      return InterestRate.newInterestRate({
        id: scModalParams.basicInterestRateId
      }).$promise;
    }
  ]
};

export { NewInterestRateModalCtrl };
