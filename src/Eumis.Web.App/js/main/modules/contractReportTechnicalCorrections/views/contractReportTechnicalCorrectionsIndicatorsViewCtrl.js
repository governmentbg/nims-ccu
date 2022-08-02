function ContractReportTechnicalCorrectionsIndicatorsViewCtrl(
  $scope,
  $state,
  $stateParams,
  contractReportIndicator
) {
  $scope.contractReportIndicator = contractReportIndicator;
}

ContractReportTechnicalCorrectionsIndicatorsViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'contractReportIndicator'
];

ContractReportTechnicalCorrectionsIndicatorsViewCtrl.$resolve = {
  contractReportIndicator: [
    '$stateParams',
    'ContractReportTechnicalCorrection',
    function($stateParams, ContractReportTechnicalCorrection) {
      return ContractReportTechnicalCorrection.getContractReportIndicator({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportTechnicalCorrectionsIndicatorsViewCtrl };
