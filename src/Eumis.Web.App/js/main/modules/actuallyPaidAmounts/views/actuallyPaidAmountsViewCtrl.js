function ActuallyPaidAmountsViewCtrl($scope, $interpolate, l10n, paidAmountInfo) {
  var infoTexts = [
    $interpolate(l10n.get('actuallyPaidAmounts_viewPaidAmount_statusInfo'))({
      status: paidAmountInfo.statusDescr
    })
  ];

  if (paidAmountInfo.contractNum) {
    infoTexts.push(
      $interpolate(l10n.get('actuallyPaidAmounts_viewPaidAmount_contractInfo'))({
        contractNum: paidAmountInfo.contractNum
      })
    );
  }

  $scope.infoText = infoTexts.join(', ');
  $scope.paidAmountInfo = paidAmountInfo;
}

ActuallyPaidAmountsViewCtrl.$inject = ['$scope', '$interpolate', 'l10n', 'paidAmountInfo'];

ActuallyPaidAmountsViewCtrl.$resolve = {
  paidAmountInfo: [
    'ActuallyPaidAmount',
    '$stateParams',
    function(ActuallyPaidAmount, $stateParams) {
      return ActuallyPaidAmount.getInfo({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ActuallyPaidAmountsViewCtrl };
