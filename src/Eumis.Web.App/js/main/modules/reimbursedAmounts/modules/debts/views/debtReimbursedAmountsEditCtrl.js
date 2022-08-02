function DebtReimbursedAmountsEditCtrl(
  $scope,
  $state,
  $stateParams,
  DebtReimbursedAmount,
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
        return DebtReimbursedAmount.update(
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

DebtReimbursedAmountsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'DebtReimbursedAmount',
  'reimbursedAmount'
];

DebtReimbursedAmountsEditCtrl.$resolve = {
  reimbursedAmount: [
    'DebtReimbursedAmount',
    '$stateParams',
    function(DebtReimbursedAmount, $stateParams) {
      return DebtReimbursedAmount.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { DebtReimbursedAmountsEditCtrl };
