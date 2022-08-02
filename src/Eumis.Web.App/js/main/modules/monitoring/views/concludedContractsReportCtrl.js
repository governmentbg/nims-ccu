function ConcludedContractsReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null,
    fromDate: null,
    toDate: null,
    currency: null,
    uin: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return Monitoring.getConcludedContractsReport({
      programmeId: $scope.filters.programmeId,
      programmePriorityId: $scope.filters.programmePriorityId,
      procedureId: $scope.filters.procedureId,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate,
      currency: $scope.filters.currency,
      uin: $scope.filters.uin
    }).$promise.then(function(result) {
      $scope.concludedContracts = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/concludedContracts/export?' +
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
        $scope.filters.currency +
        ($scope.filters.uin ? '&uin=' + $scope.filters.uin : '');
    });
  };
}

ConcludedContractsReportCtrl.$inject = ['$scope', 'Monitoring'];

export { ConcludedContractsReportCtrl };
