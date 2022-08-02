function FIReimbursedAmountsNewStep2Ctrl(
  $scope,
  $state,
  scModal,
  FIReimbursedAmount,
  newReimbursedAmount
) {
  $scope.newReimbursedAmount = newReimbursedAmount;

  $scope.save = function() {
    return $scope.reimbursedAmountsNewStep2Form.$validate().then(function() {
      if ($scope.reimbursedAmountsNewStep2Form.$valid) {
        return FIReimbursedAmount.save(newReimbursedAmount).$promise.then(function(result) {
          return $state.go('root.fiReimbursedAmounts.view.basicData', {
            id: result.reimbursedAmountId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.fiReimbursedAmounts.search');
  };
}

FIReimbursedAmountsNewStep2Ctrl.$inject = [
  '$scope',
  '$state',
  'scModal',
  'FIReimbursedAmount',
  'newReimbursedAmount'
];

FIReimbursedAmountsNewStep2Ctrl.$resolve = {
  newReimbursedAmount: [
    'FIReimbursedAmount',
    '$stateParams',
    function(FIReimbursedAmount, $stateParams) {
      return FIReimbursedAmount.newReimbursedAmount({
        contractNum: $stateParams.cNum
      }).$promise;
    }
  ]
};

export { FIReimbursedAmountsNewStep2Ctrl };
