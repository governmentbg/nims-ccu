function ContractReportFinancialRevalidationsViewCtrl(
  $scope,
  l10n,
  $interpolate,
  contractInfo,
  contractReportFinancialRevalidationInfo
) {
  $scope.contractInfo = contractInfo;
  $scope.contractReportFinancialRevalidationInfo = contractReportFinancialRevalidationInfo;
  $scope.titleText = $interpolate(
    l10n.get(
      'contractReportFinancialRevalidations_' + 'viewContractReportFinancialRevalidation_title'
    )
  )({
    contractName: contractInfo.name
  });
}

ContractReportFinancialRevalidationsViewCtrl.$inject = [
  '$scope',
  'l10n',
  '$interpolate',
  'contractInfo',
  'contractReportFinancialRevalidationInfo'
];

ContractReportFinancialRevalidationsViewCtrl.$resolve = {
  contractInfo: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getInfoForRevalidation({ id: $stateParams.id }).$promise;
    }
  ],
  contractReportFinancialRevalidationInfo: [
    'ContractReportFinancialRevalidation',
    '$stateParams',
    function(ContractReportFinancialRevalidation, $stateParams) {
      return ContractReportFinancialRevalidation.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractReportFinancialRevalidationsViewCtrl };
