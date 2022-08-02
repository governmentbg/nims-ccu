function ContractReportsNewStep2Ctrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReport,
  newContractReport
) {
  $scope.newContractReport = newContractReport;
  $scope.contractId = newContractReport.contractId;

  $scope.save = function() {
    return $scope.newContractReportForm.$validate().then(function() {
      if ($scope.newContractReportForm.$valid) {
        return scConfirm({
          resource: 'ContractReport',
          validationAction: 'canCreate',
          params: {
            contractId: $scope.newContractReport.contractId
          }
        }).then(function(result) {
          if (result.executed) {
            return ContractReport.save({}, $scope.newContractReport).$promise.then(function(
              result
            ) {
              return $state.go('root.contractReports.view.contract', {
                id: result.contractReportId
              });
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReports.search');
  };
}

ContractReportsNewStep2Ctrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReport',
  'newContractReport'
];

ContractReportsNewStep2Ctrl.$resolve = {
  newContractReport: [
    'ContractReport',
    '$stateParams',
    function(ContractReport, $stateParams) {
      return ContractReport.newContractReport({
        contractNum: $stateParams.cNum
      }).$promise;
    }
  ]
};

export { ContractReportsNewStep2Ctrl };
