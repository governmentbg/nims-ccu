function EvaluationsReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return Monitoring.getEvaluationsReport({
      programmeId: $scope.filters.programmeId,
      programmePriorityId: $scope.filters.programmePriorityId,
      procedureId: $scope.filters.procedureId
    }).$promise.then(function(result) {
      $scope.evaluations = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/evaluations/export?' +
        'programmeId=' +
        $scope.filters.programmeId +
        '&programmePriorityId=' +
        $scope.filters.programmePriorityId +
        '&procedureId=' +
        $scope.filters.procedureId;
    });
  };
}

EvaluationsReportCtrl.$inject = ['$scope', 'Monitoring'];

export { EvaluationsReportCtrl };
