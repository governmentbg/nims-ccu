function EvalSessionReportsNewCtrl(
  $scope,
  $state,
  $stateParams,
  EvalSessionReport,
  newEvalSessionReport
) {
  $scope.newEvalSessionReport = newEvalSessionReport;

  $scope.save = function() {
    return $scope.newEvalSessionReportForm.$validate().then(function() {
      if ($scope.newEvalSessionReportForm.$valid) {
        return EvalSessionReport.save(
          {
            id: $stateParams.id
          },
          $scope.newEvalSessionReport
        ).$promise.then(function(result) {
          return $state.go('root.evalSessions.view.allDocs.reports.edit', {
            id: $stateParams.id,
            ind: result.reportId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.allDocs.search');
  };
}

EvalSessionReportsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'EvalSessionReport',
  'newEvalSessionReport'
];

EvalSessionReportsNewCtrl.$resolve = {
  newEvalSessionReport: [
    '$stateParams',
    'EvalSessionReport',
    function($stateParams, EvalSessionReport) {
      return EvalSessionReport.newEvalSessionReport({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { EvalSessionReportsNewCtrl };
