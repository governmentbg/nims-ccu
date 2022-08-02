function FinancialCorrectionsReportCtrl($scope, Monitoring) {
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

    return Monitoring.getFinancialCorrectionsReport({
      programmeId: $scope.filters.programmeId,
      programmePriorityId: $scope.filters.programmePriorityId,
      procedureId: $scope.filters.procedureId,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate,
      currency: $scope.filters.currency
    }).$promise.then(function(result) {
      $scope.financialCorrections = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/financialCorrections/export?' +
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

FinancialCorrectionsReportCtrl.$inject = ['$scope', 'Monitoring'];

export { FinancialCorrectionsReportCtrl };
