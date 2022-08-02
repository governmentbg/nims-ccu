function ContractReportFinancialCertCorrectionsViewCtrl(
  $scope,
  l10n,
  $interpolate,
  contractInfo,
  contractReportFinancialCertCorrectionInfo
) {
  $scope.contractInfo = contractInfo;
  $scope.contractReportFinancialCertCorrectionInfo = contractReportFinancialCertCorrectionInfo;
  $scope.titleText = $interpolate(
    l10n.get(
      'contractReportFinancialCertCorrections_' + 'viewContractReportFinancialCertCorrection_title'
    )
  )({
    contractName: contractInfo.name
  });
}

ContractReportFinancialCertCorrectionsViewCtrl.$inject = [
  '$scope',
  'l10n',
  '$interpolate',
  'contractInfo',
  'contractReportFinancialCertCorrectionInfo'
];

ContractReportFinancialCertCorrectionsViewCtrl.$resolve = {
  contractInfo: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getInfoForCertCorrection({ id: $stateParams.id }).$promise;
    }
  ],
  contractReportFinancialCertCorrectionInfo: [
    'ContractReportFinancialCertCorrection',
    '$stateParams',
    function(ContractReportFinancialCertCorrection, $stateParams) {
      return ContractReportFinancialCertCorrection.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractReportFinancialCertCorrectionsViewCtrl };
