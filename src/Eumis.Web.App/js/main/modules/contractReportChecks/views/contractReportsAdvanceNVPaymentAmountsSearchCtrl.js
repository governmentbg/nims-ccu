function ContractReportsAdvanceNVPaymentAmountsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scModal,
  contractReportAdvanceNVPaymentAmounts
) {
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportAdvanceNVPaymentAmounts = contractReportAdvanceNVPaymentAmounts;
}

ContractReportsAdvanceNVPaymentAmountsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scModal',
  'contractReportAdvanceNVPaymentAmounts'
];

ContractReportsAdvanceNVPaymentAmountsSearchCtrl.$resolve = {
  contractReportAdvanceNVPaymentAmounts: [
    '$stateParams',
    'ContractReportAdvanceNVPaymentAmount',
    function($stateParams, ContractReportAdvanceNVPaymentAmount) {
      return ContractReportAdvanceNVPaymentAmount.query($stateParams).$promise;
    }
  ]
};

export { ContractReportsAdvanceNVPaymentAmountsSearchCtrl };
