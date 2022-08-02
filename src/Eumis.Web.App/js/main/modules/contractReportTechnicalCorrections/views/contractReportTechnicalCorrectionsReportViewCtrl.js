function ContractReportTechnicalCorrectionsReportViewCtrl($scope, contractReport) {
  $scope.contractReport = contractReport;
}

ContractReportTechnicalCorrectionsReportViewCtrl.$inject = ['$scope', 'contractReport'];

ContractReportTechnicalCorrectionsReportViewCtrl.$resolve = {
  contractReport: [
    'ContractReport',
    'contractReportTechnicalCorrectionInfo',
    function(ContractReport, contractReportTechnicalCorrectionInfo) {
      return ContractReport.get({
        id: contractReportTechnicalCorrectionInfo.contractReportId
      }).$promise;
    }
  ]
};

export { ContractReportTechnicalCorrectionsReportViewCtrl };
