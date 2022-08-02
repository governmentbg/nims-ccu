function ContractReportTechnicalCorrectionsCorrectedIndicatorsSearchCtrl(
  $scope,
  $stateParams,
  contractReportTechnicalCorrectionIndicators
) {
  $scope.contractReportTechnicalCorrectionId = $stateParams.id;
  $scope.contractReportTechnicalCorrectionIndicators = contractReportTechnicalCorrectionIndicators;
}

ContractReportTechnicalCorrectionsCorrectedIndicatorsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'contractReportTechnicalCorrectionIndicators'
];

ContractReportTechnicalCorrectionsCorrectedIndicatorsSearchCtrl.$resolve = {
  contractReportTechnicalCorrectionIndicators: [
    '$stateParams',
    'ContractReportTechnicalCorrectionIndicator',
    function($stateParams, ContractReportTechnicalCorrectionIndicator) {
      return ContractReportTechnicalCorrectionIndicator.query($stateParams).$promise;
    }
  ]
};

export { ContractReportTechnicalCorrectionsCorrectedIndicatorsSearchCtrl };
