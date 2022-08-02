function ContractReportsReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null,
    contractId: null,
    toDate: null,
    reportType: null,
    reportStatus: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return Monitoring.getContractReportsReport({
      programmeId: $scope.filters.programmeId,
      programmePriorityId: $scope.filters.programmePriorityId,
      procedureId: $scope.filters.procedureId,
      contractId: $scope.filters.contractId,
      toDate: $scope.filters.toDate,
      reportType: $scope.filters.reportType,
      reportStatus: $scope.filters.reportStatus
    }).$promise.then(function(result) {
      $scope.contractReports = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/contractReports/export?' +
        'programmeId=' +
        $scope.filters.programmeId +
        '&programmePriorityId=' +
        $scope.filters.programmePriorityId +
        '&procedureId=' +
        $scope.filters.procedureId +
        '&contractId=' +
        $scope.filters.contractId +
        '&toDate=' +
        $scope.filters.toDate +
        '&reportType=' +
        $scope.filters.reportType +
        '&reportStatus=' +
        $scope.filters.reportStatus;
    });
  };
}

ContractReportsReportCtrl.$inject = ['$scope', 'Monitoring'];

export { ContractReportsReportCtrl };
