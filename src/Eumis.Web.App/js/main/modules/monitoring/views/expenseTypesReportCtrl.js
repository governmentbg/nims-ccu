function ExpenseTypesReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    toDate: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return Monitoring.getExpenseTypesReport({
      programmeId: $scope.filters.programmeId,
      toDate: $scope.filters.toDate
    }).$promise.then(function(result) {
      $scope.results = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/expenseTypes/export?' + '&programmeId=' + $scope.filters.programmeId;
      '&toDate=' + $scope.filters.toDate;
    });
  };
}

ExpenseTypesReportCtrl.$inject = ['$scope', 'Monitoring'];

export { ExpenseTypesReportCtrl };
