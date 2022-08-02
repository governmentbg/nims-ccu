function DebtReimbursedAmountsViewCtrl($scope, $interpolate, l10n, reimbursedAmountInfo) {
  $scope.infoText = $interpolate(l10n.get('reimbursedAmounts_viewReimbursedAmount_info'))({
    status: reimbursedAmountInfo.statusDescr,
    debtNum: reimbursedAmountInfo.debtRegNumber
  });

  $scope.reimbursedAmountInfo = reimbursedAmountInfo;
}

DebtReimbursedAmountsViewCtrl.$inject = ['$scope', '$interpolate', 'l10n', 'reimbursedAmountInfo'];

DebtReimbursedAmountsViewCtrl.$resolve = {
  reimbursedAmountInfo: [
    'DebtReimbursedAmount',
    '$stateParams',
    function(DebtReimbursedAmount, $stateParams) {
      return DebtReimbursedAmount.getInfo({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { DebtReimbursedAmountsViewCtrl };
