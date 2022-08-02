function ContractReimbursedAmountsNewStep2Ctrl(
  $scope,
  $state,
  scModal,
  ContractReimbursedAmount,
  newReimbursedAmount
) {
  $scope.newReimbursedAmount = newReimbursedAmount;

  $scope.save = function() {
    return $scope.reimbursedAmountsNewStep2Form.$validate().then(function() {
      if ($scope.reimbursedAmountsNewStep2Form.$valid) {
        return ContractReimbursedAmount.save(newReimbursedAmount).$promise.then(function(result) {
          return $state.go('root.contractReimbursedAmounts.view.basicData', {
            id: result.reimbursedAmountId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReimbursedAmounts.search');
  };
}

ContractReimbursedAmountsNewStep2Ctrl.$inject = [
  '$scope',
  '$state',
  'scModal',
  'ContractReimbursedAmount',
  'newReimbursedAmount'
];

ContractReimbursedAmountsNewStep2Ctrl.$resolve = {
  newReimbursedAmount: [
    'ContractReimbursedAmount',
    '$stateParams',
    function(ContractReimbursedAmount, $stateParams) {
      return ContractReimbursedAmount.newReimbursedAmount({
        contractNum: $stateParams.cNum
      }).$promise;
    }
  ]
};

export { ContractReimbursedAmountsNewStep2Ctrl };
