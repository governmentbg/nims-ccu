function AdvancePaymentsReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null,
    fromDate: null,
    toDate: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return Monitoring.getAdvancePaymentsReport({
      programmeId: $scope.filters.programmeId,
      programmePriorityId: $scope.filters.programmePriorityId,
      procedureId: $scope.filters.procedureId,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate
    }).$promise.then(function(result) {
      $scope.advancePayments = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/advancePayments/export?' +
        'programmeId=' +
        $scope.filters.programmeId +
        '&programmePriorityId=' +
        $scope.filters.programmePriorityId +
        '&procedureId=' +
        $scope.filters.procedureId +
        '&fromDate=' +
        $scope.filters.fromDate +
        '&toDate=' +
        $scope.filters.toDate;
    });
  };
}

AdvancePaymentsReportCtrl.$inject = ['$scope', 'Monitoring'];

export { AdvancePaymentsReportCtrl };
