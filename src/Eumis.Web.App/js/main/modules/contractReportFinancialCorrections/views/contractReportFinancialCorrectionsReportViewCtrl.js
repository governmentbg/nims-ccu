function ContractReportFinancialCorrectionsReportViewCtrl($scope, contractReport) {
  $scope.contractReport = contractReport;
}

ContractReportFinancialCorrectionsReportViewCtrl.$inject = ['$scope', 'contractReport'];

ContractReportFinancialCorrectionsReportViewCtrl.$resolve = {
  contractReport: [
    'ContractReport',
    'contractReportFinancialCorrectionInfo',
    function(ContractReport, contractReportFinancialCorrectionInfo) {
      return ContractReport.get({
        id: contractReportFinancialCorrectionInfo.contractReportId
      }).$promise;
    }
  ]
};

export { ContractReportFinancialCorrectionsReportViewCtrl };
