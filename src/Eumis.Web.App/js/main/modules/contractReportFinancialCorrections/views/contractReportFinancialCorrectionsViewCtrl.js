function ContractReportFinancialCorrectionsViewCtrl(
  $scope,
  l10n,
  $interpolate,
  contractInfo,
  contractReportFinancialCorrectionInfo
) {
  $scope.contractInfo = contractInfo;
  $scope.contractReportFinancialCorrectionInfo = contractReportFinancialCorrectionInfo;
  $scope.titleText = $interpolate(
    l10n.get('contractReportFinancialCorrections_' + 'viewContractReportFinancialCorrection_title')
  )({
    contractName: contractInfo.name
  });
}

ContractReportFinancialCorrectionsViewCtrl.$inject = [
  '$scope',
  'l10n',
  '$interpolate',
  'contractInfo',
  'contractReportFinancialCorrectionInfo'
];

ContractReportFinancialCorrectionsViewCtrl.$resolve = {
  contractInfo: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getInfoForCorrection({ id: $stateParams.id }).$promise;
    }
  ],
  contractReportFinancialCorrectionInfo: [
    'ContractReportFinancialCorrection',
    '$stateParams',
    function(ContractReportFinancialCorrection, $stateParams) {
      return ContractReportFinancialCorrection.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractReportFinancialCorrectionsViewCtrl };
