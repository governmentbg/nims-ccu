import _ from 'lodash';
function ChooseAndEvaluateProjectModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  projects,
  EvalSessionEvaluation,
  scMessage
) {
  $scope.projects = projects;
  $scope.evaluationType = scModalParams.evaluationType;
  $scope.showBulkEvalButton = true;

  $scope.choose = function(project) {
    return EvalSessionEvaluation.canEvaluateProject({
      id: scModalParams.evalSessionId,
      projectId: project.projectId,
      evalTableType: scModalParams.evaluationType
    }).$promise.then(data => {
      if (data.errors.length === 0) {
        $uibModalInstance.close({
          evaluation: {
            evalSessionId: scModalParams.evalSessionId,
            projectId: project.projectId,
            evaluationType: scModalParams.evaluationType
          }
        });
      } else {
        _.map(data.errors, scMessage);
      }
    });
  };

  $scope.evaluate = function() {
    $scope.showBulkEvalButton = !$scope.showBulkEvalButton;
    return EvalSessionEvaluation.bulkEvaluation(
      {
        id: scModalParams.evalSessionId,
        evalTableType: $scope.evaluationType,
        version: scModalParams.version
      },
      {}
    ).$promise.then(data => {
      scMessage('evalSessions_chooseAndEvaluateProjectModal_proceedMessage');
      $scope.projects = data;
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.close({});
  };
}

ChooseAndEvaluateProjectModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'projects',
  'EvalSessionEvaluation',
  'scMessage'
];

ChooseAndEvaluateProjectModalCtrl.$resolve = {
  projects: [
    'EvalSessionEvaluation',
    'scModalParams',
    function(EvalSessionEvaluation, scModalParams) {
      return EvalSessionEvaluation.getEvaluativeProjects({
        id: scModalParams.evalSessionId,
        evalTableType: scModalParams.evaluationType
      }).$promise;
    }
  ]
};

export { ChooseAndEvaluateProjectModalCtrl };
