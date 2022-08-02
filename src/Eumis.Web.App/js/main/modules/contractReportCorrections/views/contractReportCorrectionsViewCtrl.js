function ContractReportCorrectionsViewCtrl(
  $scope,
  $interpolate,
  l10n,
  contractReportCorrectionInfo
) {
  $scope.infoText = $interpolate(
    l10n.get('contractReportCorrections_viewContractReportCorrection_statusInfo')
  )({
    status: contractReportCorrectionInfo.statusDescr,
    programme: contractReportCorrectionInfo.programmeCode
  });

  $scope.contractReportCorrectionInfo = contractReportCorrectionInfo;
}

ContractReportCorrectionsViewCtrl.$inject = [
  '$scope',
  '$interpolate',
  'l10n',
  'contractReportCorrectionInfo'
];

ContractReportCorrectionsViewCtrl.$resolve = {
  contractReportCorrectionInfo: [
    'ContractReportCorrection',
    '$stateParams',
    function(ContractReportCorrection, $stateParams) {
      return ContractReportCorrection.getInfo({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportCorrectionsViewCtrl };
