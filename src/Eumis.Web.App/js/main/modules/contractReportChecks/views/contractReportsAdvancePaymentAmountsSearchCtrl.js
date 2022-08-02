function ContractReportsAdvancePaymentAmountsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scModal,
  contractReportAdvancePaymentAmounts
) {
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportAdvancePaymentAmounts = contractReportAdvancePaymentAmounts;
}

ContractReportsAdvancePaymentAmountsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scModal',
  'contractReportAdvancePaymentAmounts'
];

ContractReportsAdvancePaymentAmountsSearchCtrl.$resolve = {
  contractReportAdvancePaymentAmounts: [
    '$stateParams',
    'ContractReportAdvancePaymentAmount',
    function($stateParams, ContractReportAdvancePaymentAmount) {
      return ContractReportAdvancePaymentAmount.query($stateParams).$promise;
    }
  ]
};

export { ContractReportsAdvancePaymentAmountsSearchCtrl };
