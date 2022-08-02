function FinancialExecutionReportCtrl($scope, $timeout, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    year: null,
    table: null
  };

  $scope.selectedTable = null;
  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return $scope.financialExecutionForm.$validate().then(function() {
      if ($scope.financialExecutionForm.$valid) {
        var promise;

        switch ($scope.filters.table) {
          case 'table1':
            promise = Monitoring.getFinancialExecutionTable1Report({
              programmeId: $scope.filters.programmeId,
              programmePriorityId: $scope.filters.programmePriorityId,
              date: $scope.filters.date
            }).$promise;
            $scope.exportUrl =
              'api/monitoringReports/financialExecution/exportTable1?' +
              'programmeId=' +
              $scope.filters.programmeId +
              '&date=' +
              $scope.filters.date +
              '&programmePriorityId=' +
              $scope.filters.programmePriorityId;
            break;
          case 'table2':
            promise = Monitoring.getFinancialExecutionTable2Report({
              programmeId: $scope.filters.programmeId,
              programmePriorityId: $scope.filters.programmePriorityId,
              date: $scope.filters.date
            }).$promise;
            $scope.exportUrl =
              'api/monitoringReports/financialExecution/exportTable2?' +
              'programmeId=' +
              $scope.filters.programmeId +
              '&date=' +
              $scope.filters.date +
              '&programmePriorityId=' +
              $scope.filters.programmePriorityId;
            break;
          case 'table3':
            promise = Monitoring.getFinancialExecutionTable3Report({
              programmeId: $scope.filters.programmeId,
              year: $scope.filters.year
            }).$promise;
            $scope.exportUrl =
              'api/monitoringReports/financialExecution/exportTable3?' +
              'programmeId=' +
              $scope.filters.programmeId +
              '&year=' +
              $scope.filters.year;
            break;
          default:
            promise = $timeout(function() {
              return [];
            }, 500);
        }

        $scope.displayResult = false;
        $scope.selectedTable = $scope.filters.table;

        return promise.then(function(result) {
          $scope.report = result;
          $scope.displayResult = true;
        });
      }
    });
  };
}

FinancialExecutionReportCtrl.$inject = ['$scope', '$timeout', 'Monitoring'];

export { FinancialExecutionReportCtrl };
