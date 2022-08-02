function DoubleFundingReportCtrl($scope, Monitoring) {
  $scope.filters = {
    uin: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return $scope.doubleFundingSearchForm.$validate().then(function() {
      if ($scope.doubleFundingSearchForm.$valid) {
        return Monitoring.getDoubleFundingReport({
          uin: $scope.filters.uin
        }).$promise.then(function(result) {
          $scope.contracts = result;
          $scope.displayResult = true;
          $scope.exportUrl =
            'api/monitoringReports/doubleFunding/export?' + 'uin=' + $scope.filters.uin;
        });
      }
    });
  };
}

DoubleFundingReportCtrl.$inject = ['$scope', 'Monitoring'];

export { DoubleFundingReportCtrl };
