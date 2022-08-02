import _ from 'lodash';

function PhysicalExecutionReportCtrl($scope, $timeout, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    investmentPriorityId: null,
    table: null,
    projectNumber: null,
    spd: null
  };

  $scope.selectedTable = null;
  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return $scope.physicalExecutionForm.$validate().then(function() {
      if ($scope.physicalExecutionForm.$valid) {
        if ($scope.filters.table === 'table12' || $scope.filters.table === 'table13') {
          $scope.selectedTable = $scope.filters.table;
          return $timeout(function() {
            return [];
          }, 500).then(function(result) {
            $scope.report = result;
            $scope.displayResult = true;
          });
        } else {
          return Monitoring.getPhysicalExecutionReport($scope.filters).$promise.then(function(
            result
          ) {
            $scope.selectedTable = $scope.filters.table;
            $scope.report = result;
            $scope.displayResult = true;

            var query = _.map($scope.filters, function(v, k) {
              return encodeURIComponent(k) + '=' + encodeURIComponent(v);
            }).join('&');

            $scope.exportUrl = 'api/monitoringReports/physicalExecution/export?' + query;
          });
        }
      }
    });
  };
}

PhysicalExecutionReportCtrl.$inject = ['$scope', '$timeout', 'Monitoring'];

export { PhysicalExecutionReportCtrl };
