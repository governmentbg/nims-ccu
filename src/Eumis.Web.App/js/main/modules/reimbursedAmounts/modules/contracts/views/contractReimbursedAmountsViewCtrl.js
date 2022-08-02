function ContractReimbursedAmountsViewCtrl($scope, $interpolate, l10n, reimbursedAmountInfo) {
  $scope.infoText = $interpolate(l10n.get('contractReimbursedAmounts_viewReimbursedAmount_info'))({
    status: reimbursedAmountInfo.statusDescr,
    contractNum: reimbursedAmountInfo.contractRegNumber
  });

  $scope.reimbursedAmountInfo = reimbursedAmountInfo;
}

ContractReimbursedAmountsViewCtrl.$inject = [
  '$scope',
  '$interpolate',
  'l10n',
  'reimbursedAmountInfo'
];

ContractReimbursedAmountsViewCtrl.$resolve = {
  reimbursedAmountInfo: [
    'ContractReimbursedAmount',
    '$stateParams',
    function(ContractReimbursedAmount, $stateParams) {
      return ContractReimbursedAmount.getInfo({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReimbursedAmountsViewCtrl };
