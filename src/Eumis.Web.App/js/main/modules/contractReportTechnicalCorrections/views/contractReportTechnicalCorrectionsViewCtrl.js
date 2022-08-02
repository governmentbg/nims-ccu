function ContractReportTechnicalCorrectionsViewCtrl(
  $scope,
  l10n,
  $interpolate,
  contractInfo,
  contractReportTechnicalCorrectionInfo
) {
  $scope.contractInfo = contractInfo;
  $scope.contractReportTechnicalCorrectionInfo = contractReportTechnicalCorrectionInfo;
  $scope.titleText = $interpolate(
    l10n.get('contractReportTechnicalCorrections_' + 'viewContractReportTechnicalCorrection_title')
  )({
    contractName: contractInfo.name
  });
}

ContractReportTechnicalCorrectionsViewCtrl.$inject = [
  '$scope',
  'l10n',
  '$interpolate',
  'contractInfo',
  'contractReportTechnicalCorrectionInfo'
];

ContractReportTechnicalCorrectionsViewCtrl.$resolve = {
  contractInfo: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getInfoForTechnicalCorrection({ id: $stateParams.id }).$promise;
    }
  ],
  contractReportTechnicalCorrectionInfo: [
    'ContractReportTechnicalCorrection',
    '$stateParams',
    function(ContractReportTechnicalCorrection, $stateParams) {
      return ContractReportTechnicalCorrection.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractReportTechnicalCorrectionsViewCtrl };
