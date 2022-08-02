function EuReimbursedAmountsViewCtrl($scope, $interpolate, l10n, amountInfo) {
  $scope.infoText = $interpolate(l10n.get('euReimbursedAmounts_viewAmount_info'))({
    programmeCode: amountInfo.programmeCode,
    status: amountInfo.statusDescr
  });

  $scope.amountInfo = amountInfo;
}

EuReimbursedAmountsViewCtrl.$inject = ['$scope', '$interpolate', 'l10n', 'amountInfo'];

EuReimbursedAmountsViewCtrl.$resolve = {
  amountInfo: [
    'EuReimbursedAmount',
    '$stateParams',
    function(EuReimbursedAmount, $stateParams) {
      return EuReimbursedAmount.getInfo({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { EuReimbursedAmountsViewCtrl };
