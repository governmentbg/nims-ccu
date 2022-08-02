function ContractReportPaymentsReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null,
    fromDate: null,
    toDate: null,
    currency: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return Monitoring.getContractReportPaymentsReport({
      programmeId: $scope.filters.programmeId,
      programmePriorityId: $scope.filters.programmePriorityId,
      procedureId: $scope.filters.procedureId,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate,
      currency: $scope.filters.currency
    }).$promise.then(function(result) {
      $scope.contractReportPayments = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/contractReportPayments/export?' +
        'programmeId=' +
        $scope.filters.programmeId +
        '&programmePriorityId=' +
        $scope.filters.programmePriorityId +
        '&procedureId=' +
        $scope.filters.procedureId +
        '&fromDate=' +
        $scope.filters.fromDate +
        '&toDate=' +
        $scope.filters.toDate +
        '&currency=' +
        $scope.filters.currency;
    });
  };
}

ContractReportPaymentsReportCtrl.$inject = ['$scope', 'Monitoring'];

export { ContractReportPaymentsReportCtrl };
