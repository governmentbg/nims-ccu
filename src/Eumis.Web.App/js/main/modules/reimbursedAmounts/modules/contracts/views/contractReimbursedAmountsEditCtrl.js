function ContractReimbursedAmountsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ContractReimbursedAmount,
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
        return ContractReimbursedAmount.update(
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

ContractReimbursedAmountsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractReimbursedAmount',
  'reimbursedAmount'
];

ContractReimbursedAmountsEditCtrl.$resolve = {
  reimbursedAmount: [
    'ContractReimbursedAmount',
    '$stateParams',
    function(ContractReimbursedAmount, $stateParams) {
      return ContractReimbursedAmount.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReimbursedAmountsEditCtrl };
