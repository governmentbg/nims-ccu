function NewAllowanceRateModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  AllowanceRate,
  newAllowanceRate
) {
  $scope.newAllowanceRate = newAllowanceRate;

  $scope.save = function() {
    return $scope.newAllowanceRateForm.$validate().then(function() {
      if ($scope.newAllowanceRateForm.$valid) {
        return AllowanceRate.save(
          {
            id: scModalParams.allowanceId
          },
          $scope.newAllowanceRate
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

NewAllowanceRateModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'AllowanceRate',
  'newAllowanceRate'
];

NewAllowanceRateModalCtrl.$resolve = {
  newAllowanceRate: [
    'scModalParams',
    'AllowanceRate',
    function(scModalParams, AllowanceRate) {
      return AllowanceRate.newAllowanceRate({
        id: scModalParams.allowanceId
      }).$promise;
    }
  ]
};

export { NewAllowanceRateModalCtrl };
