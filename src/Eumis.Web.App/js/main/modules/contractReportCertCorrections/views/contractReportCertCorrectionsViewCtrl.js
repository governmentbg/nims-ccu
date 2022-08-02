function ContractReportCertCorrectionsViewCtrl(
  $scope,
  $interpolate,
  l10n,
  contractReportCertCorrectionInfo
) {
  $scope.infoText = $interpolate(
    l10n.get('contractReportCertCorrections_' + 'viewContractReportCertCorrection_statusInfo')
  )({
    status: contractReportCertCorrectionInfo.statusDescr,
    programme: contractReportCertCorrectionInfo.programmeCode
  });

  $scope.contractReportCertCorrectionInfo = contractReportCertCorrectionInfo;
}

ContractReportCertCorrectionsViewCtrl.$inject = [
  '$scope',
  '$interpolate',
  'l10n',
  'contractReportCertCorrectionInfo'
];

ContractReportCertCorrectionsViewCtrl.$resolve = {
  contractReportCertCorrectionInfo: [
    'ContractReportCertCorrection',
    '$stateParams',
    function(ContractReportCertCorrection, $stateParams) {
      return ContractReportCertCorrection.getInfo({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportCertCorrectionsViewCtrl };
