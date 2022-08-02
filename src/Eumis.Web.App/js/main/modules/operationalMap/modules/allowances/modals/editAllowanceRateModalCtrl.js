function EditAllowanceRateModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  AllowanceRate,
  allowanceRate
) {
  $scope.allowanceRate = allowanceRate;

  $scope.save = function() {
    return $scope.editAllowanceRate.$validate().then(function() {
      if ($scope.editAllowanceRate.$valid) {
        return AllowanceRate.update(
          {
            id: scModalParams.allowanceId,
            ind: scModalParams.allowanceRateId
          },
          $scope.allowanceRate
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

EditAllowanceRateModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'AllowanceRate',
  'allowanceRate'
];

EditAllowanceRateModalCtrl.$resolve = {
  allowanceRate: [
    'scModalParams',
    'AllowanceRate',
    function(scModalParams, AllowanceRate) {
      return AllowanceRate.get({
        id: scModalParams.allowanceId,
        ind: scModalParams.allowanceRateId
      }).$promise;
    }
  ]
};

export { EditAllowanceRateModalCtrl };
