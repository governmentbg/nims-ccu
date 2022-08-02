function ContractReportFinancialCertCorrectionsReportViewCtrl($scope, contractReport) {
  $scope.contractReport = contractReport;
}

ContractReportFinancialCertCorrectionsReportViewCtrl.$inject = ['$scope', 'contractReport'];

ContractReportFinancialCertCorrectionsReportViewCtrl.$resolve = {
  contractReport: [
    'ContractReport',
    'contractReportFinancialCertCorrectionInfo',
    function(ContractReport, contractReportFinancialCertCorrectionInfo) {
      return ContractReport.get({
        id: contractReportFinancialCertCorrectionInfo.contractReportId
      }).$promise;
    }
  ]
};

export { ContractReportFinancialCertCorrectionsReportViewCtrl };
