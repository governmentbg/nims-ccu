function ActuallyPaidAmountsNewStep2Ctrl($scope, $state, ActuallyPaidAmount, contract) {
  $scope.contract = contract;

  $scope.save = function() {
    return $scope.paidAmountsNewStep2Form.$validate().then(function() {
      if ($scope.paidAmountsNewStep2Form.$valid) {
        return ActuallyPaidAmount.save(
          {
            contractId: contract.contractId,
            programmePriorityId: $scope.programmePriorityId,
            contractReportPaymentId: $scope.contractReportPaymentId,
            paymentReason: $scope.paymentReason
          },
          {}
        ).$promise.then(function(result) {
          return $state.go('root.actuallyPaidAmounts.view.basicData', {
            id: result.paidAmountId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.actuallyPaidAmounts.search');
  };
}

ActuallyPaidAmountsNewStep2Ctrl.$inject = ['$scope', '$state', 'ActuallyPaidAmount', 'contract'];

ActuallyPaidAmountsNewStep2Ctrl.$resolve = {
  contract: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getActuallyPaidAmountByRegNum({
        contractNum: $stateParams.cNum
      }).$promise;
    }
  ]
};

export { ActuallyPaidAmountsNewStep2Ctrl };
