function ContractReportTechnicalCorrectionsIndicatorsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  contractReportIndicators
) {
  $scope.contractReportTechnicalCorrectionId = $stateParams.id;
  $scope.contractReportTechnicalCorrectionStatus =
    $scope.contractReportTechnicalCorrectionInfo.status;
  $scope.contractReportIndicators = contractReportIndicators;

  $scope.correctIndicator = function(contractReportIndicatorId) {
    return scConfirm({
      confirmMessage:
        'contractReportTechnicalCorrections_contractReportTechnicalCorrectionsIndicatorsSearch_confirmCorrect',
      resource: 'ContractReportTechnicalCorrectionIndicator',
      action: 'save',
      params: {
        id: $stateParams.id,
        contractReportIndicatorId: contractReportIndicatorId
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ContractReportTechnicalCorrectionsIndicatorsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'contractReportIndicators'
];

ContractReportTechnicalCorrectionsIndicatorsSearchCtrl.$resolve = {
  contractReportIndicators: [
    '$stateParams',
    'contractReportTechnicalCorrectionInfo',
    'ContractReportTechnicalCorrection',
    function(
      $stateParams,
      contractReportTechnicalCorrectionInfo,
      ContractReportTechnicalCorrection
    ) {
      return ContractReportTechnicalCorrection.getContractReportIndicators({
        id: $stateParams.id,
        contractReportId: contractReportTechnicalCorrectionInfo.contractReportId
      }).$promise;
    }
  ]
};

export { ContractReportTechnicalCorrectionsIndicatorsSearchCtrl };
