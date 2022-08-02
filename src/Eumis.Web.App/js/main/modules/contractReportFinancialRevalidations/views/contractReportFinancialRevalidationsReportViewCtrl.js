function ContractReportFinancialRevalidationsReportViewCtrl($scope, contractReport) {
  $scope.contractReport = contractReport;
}

ContractReportFinancialRevalidationsReportViewCtrl.$inject = ['$scope', 'contractReport'];

ContractReportFinancialRevalidationsReportViewCtrl.$resolve = {
  contractReport: [
    'ContractReport',
    'contractReportFinancialRevalidationInfo',
    function(ContractReport, contractReportFinancialRevalidationInfo) {
      return ContractReport.get({
        id: contractReportFinancialRevalidationInfo.contractReportId
      }).$promise;
    }
  ]
};

export { ContractReportFinancialRevalidationsReportViewCtrl };
