function PinReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    fromDate: null,
    toDate: null,
    uin: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return Monitoring.getPinReport({
      programmeId: $scope.filters.programmeId,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate,
      uin: $scope.filters.uin
    }).$promise.then(function(result) {
      $scope.results = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/pin/export?' +
        'programmeId=' +
        $scope.filters.programmeId +
        '&fromDate=' +
        $scope.filters.fromDate +
        '&toDate=' +
        $scope.filters.toDate +
        ($scope.filters.uin ? '&uin=' + $scope.filters.uin : '');
    });
  };
}

PinReportCtrl.$inject = ['$scope', 'Monitoring'];

export { PinReportCtrl };
