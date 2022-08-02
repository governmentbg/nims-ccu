function ContractReportsViewCtrl($scope, l10n, $interpolate, contractInfo, contractReportInfo) {
  $scope.contractInfo = contractInfo;
  $scope.contractReportInfo = contractReportInfo;
  $scope.titleText = $interpolate(l10n.get('contractReports_viewContractReport_title'))({
    contractName: contractInfo.name,
    reportStatus: contractReportInfo.statusDescription
  });
}

ContractReportsViewCtrl.$inject = [
  '$scope',
  'l10n',
  '$interpolate',
  'contractInfo',
  'contractReportInfo'
];

ContractReportsViewCtrl.$resolve = {
  contractInfo: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getInfoForReport({ id: $stateParams.id }).$promise;
    }
  ],
  contractReportInfo: [
    'ContractReport',
    '$stateParams',
    function(ContractReport, $stateParams) {
      return ContractReport.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractReportsViewCtrl };
