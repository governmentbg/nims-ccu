function EditInterestRateModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  InterestRate,
  interestRate
) {
  $scope.interestRate = interestRate;

  $scope.save = function() {
    return $scope.editInterestRate.$validate().then(function() {
      if ($scope.editInterestRate.$valid) {
        return InterestRate.update(
          {
            id: scModalParams.basicInterestRateId,
            ind: scModalParams.interestRateId
          },
          $scope.interestRate
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

EditInterestRateModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'InterestRate',
  'interestRate'
];

EditInterestRateModalCtrl.$resolve = {
  interestRate: [
    'scModalParams',
    'InterestRate',
    function(scModalParams, InterestRate) {
      return InterestRate.get({
        id: scModalParams.basicInterestRateId,
        ind: scModalParams.interestRateId
      }).$promise;
    }
  ]
};

export { EditInterestRateModalCtrl };
