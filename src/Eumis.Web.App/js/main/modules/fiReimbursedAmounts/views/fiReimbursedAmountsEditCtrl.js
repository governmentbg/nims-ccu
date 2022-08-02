function FIReimbursedAmountsEditCtrl(
  $scope,
  $state,
  $stateParams,
  FIReimbursedAmount,
  reimbursedAmount
) {
  $scope.editMode = null;
  $scope.reimbursedAmount = reimbursedAmount;
  $scope.status = $scope.reimbursedAmountInfo.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editReimbursedAmountData.$validate().then(function() {
      if ($scope.editReimbursedAmountData.$valid) {
        return FIReimbursedAmount.update(
          {
            id: $stateParams.id
          },
          $scope.reimbursedAmount
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

FIReimbursedAmountsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'FIReimbursedAmount',
  'reimbursedAmount'
];

FIReimbursedAmountsEditCtrl.$resolve = {
  reimbursedAmount: [
    'FIReimbursedAmount',
    '$stateParams',
    function(FIReimbursedAmount, $stateParams) {
      return FIReimbursedAmount.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { FIReimbursedAmountsEditCtrl };
