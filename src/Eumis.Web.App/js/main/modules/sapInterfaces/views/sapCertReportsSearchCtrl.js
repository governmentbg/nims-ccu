function SapCertReportsSearchCtrl($scope, SapCertReport) {
  $scope.filters = {
    certReportId: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return SapCertReport.query({
      certReportId: $scope.filters.certReportId
    }).$promise.then(function(result) {
      $scope.displayResult = true;

      $scope.sapCertReports = result;

      $scope.exportUrl =
        'api/sapCertReports/export?' + 'certReportId=' + $scope.filters.certReportId;

      $scope.exportTsvUrl =
        'api/sapCertReports/exportTsv?' + 'certReportId=' + $scope.filters.certReportId;
    });
  };
}

SapCertReportsSearchCtrl.$inject = ['$scope', 'SapCertReport'];

export { SapCertReportsSearchCtrl };
