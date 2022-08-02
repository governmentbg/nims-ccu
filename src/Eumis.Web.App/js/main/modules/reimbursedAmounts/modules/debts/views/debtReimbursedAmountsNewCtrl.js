function DebtReimbursedAmountsNewCtrl(
  $scope,
  $state,
  scModal,
  DebtReimbursedAmount,
  newReimbursedAmount
) {
  $scope.newReimbursedAmount = newReimbursedAmount;

  $scope.save = function() {
    return $scope.reimbursedAmountsNewForm.$validate().then(function() {
      if ($scope.reimbursedAmountsNewForm.$valid) {
        return DebtReimbursedAmount.save(newReimbursedAmount).$promise.then(function(result) {
          return $state.go('root.debtReimbursedAmounts.view.basicData', {
            id: result.reimbursedAmountId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.debtReimbursedAmounts.search');
  };

  $scope.debtChanged = function() {
    if ($scope.contractInfo && !$scope.newReimbursedAmount.contractId) {
      $scope.newReimbursedAmount.contractId = $scope.contractInfo.contractId;
    }
  };
}

DebtReimbursedAmountsNewCtrl.$inject = [
  '$scope',
  '$state',
  'scModal',
  'DebtReimbursedAmount',
  'newReimbursedAmount'
];

DebtReimbursedAmountsNewCtrl.$resolve = {
  newReimbursedAmount: [
    'DebtReimbursedAmount',
    function(DebtReimbursedAmount) {
      return DebtReimbursedAmount.newReimbursedAmount().$promise;
    }
  ]
};

export { DebtReimbursedAmountsNewCtrl };
