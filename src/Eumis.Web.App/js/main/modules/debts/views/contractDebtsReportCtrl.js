function ContractDebtsReportCtrl($scope, ContractDebt) {
  $scope.filters = {
    month: null,
    year: null,
    programmeId: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return $scope.reportSearchForm.$validate().then(function() {
      if ($scope.reportSearchForm.$valid) {
        return ContractDebt.getReport($scope.filters).$promise.then(function(result) {
          $scope.contractDebts = result;
          $scope.displayResult = true;
          $scope.exportUrl =
            'api/contractDebtsExcelExport/report?' +
            'month=' +
            $scope.filters.month +
            '&year=' +
            $scope.filters.year +
            '&programmeId=' +
            $scope.filters.programmeId;
        });
      }
    });
  };
}

ContractDebtsReportCtrl.$inject = ['$scope', 'ContractDebt'];

export { ContractDebtsReportCtrl };
