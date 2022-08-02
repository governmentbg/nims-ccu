function V4Plus4ReportCtrl($scope, Monitoring) {
  $scope.filters = {
    toDate: null,
    financeSource: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return Monitoring.getV4Plus4Report({
      toDate: $scope.filters.toDate,
      financeSource: $scope.filters.financeSource
    }).$promise.then(function(result) {
      $scope.results = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/v4Plus4/export?' +
        '&toDate=' +
        $scope.filters.toDate +
        '&financeSource=' +
        $scope.filters.financeSource;
    });
  };
}

V4Plus4ReportCtrl.$inject = ['$scope', 'Monitoring'];

export { V4Plus4ReportCtrl };
