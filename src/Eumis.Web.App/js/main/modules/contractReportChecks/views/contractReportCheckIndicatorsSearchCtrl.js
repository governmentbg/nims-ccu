function ContractReportCheckIndicatorsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  l10n,
  scModal,
  contractReportIndicators
) {
  $scope.contractReportIndicators = contractReportIndicators;
  $scope.contractReportStatus = $scope.contractReportInfo.status;

  $scope.contractReportIndicatorsExportUrl = $interpolate(
    'api/contractReports/{{contractReportId}}/indicators/excelExport?'
  )({
    contractReportId: $stateParams.id
  });
}

ContractReportCheckIndicatorsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'l10n',
  'scModal',
  'contractReportIndicators'
];

ContractReportCheckIndicatorsSearchCtrl.$resolve = {
  contractReportIndicators: [
    '$stateParams',
    'ContractReportIndicator',
    function($stateParams, ContractReportIndicator) {
      return ContractReportIndicator.query($stateParams).$promise;
    }
  ]
};

export { ContractReportCheckIndicatorsSearchCtrl };
