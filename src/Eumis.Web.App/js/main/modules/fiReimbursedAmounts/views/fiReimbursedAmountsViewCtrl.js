function FIReimbursedAmountsViewCtrl($scope, $interpolate, l10n, reimbursedAmountInfo) {
  $scope.infoText = $interpolate(l10n.get('fiReimbursedAmounts_viewReimbursedAmount_info'))({
    status: reimbursedAmountInfo.statusDescr,
    contractNum: reimbursedAmountInfo.contractRegNumber
  });

  $scope.reimbursedAmountInfo = reimbursedAmountInfo;
}

FIReimbursedAmountsViewCtrl.$inject = ['$scope', '$interpolate', 'l10n', 'reimbursedAmountInfo'];

FIReimbursedAmountsViewCtrl.$resolve = {
  reimbursedAmountInfo: [
    'FIReimbursedAmount',
    '$stateParams',
    function(FIReimbursedAmount, $stateParams) {
      return FIReimbursedAmount.getInfo({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { FIReimbursedAmountsViewCtrl };
