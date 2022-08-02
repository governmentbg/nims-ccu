function EvalSessionEvaluationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scConfirm,
  EvalSessionEvaluation,
  evalSessionEvaluation
) {
  $scope.evalSessionEvaluation = evalSessionEvaluation;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';

  $scope.back = function() {
    return $state.go('root.evalSessions.view.projects.view', {
      id: $stateParams.id,
      ind: $scope.evalSessionEvaluation.projectId
    });
  };

  $scope.deleteEvaluation = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_evalSessionEvaluationEdit_deleteConfirm',
      noteLabel: 'evalSessions_evalSessionEvaluationEdit_deleteMessage',
      resource: 'EvalSessionEvaluation',
      validationAction: 'canCancel',
      action: 'cancelEvaluation',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        projectId: $scope.evalSessionEvaluation.projectId,
        version: $scope.evalSessionEvaluation.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

EvalSessionEvaluationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scConfirm',
  'EvalSessionEvaluation',
  'evalSessionEvaluation'
];

EvalSessionEvaluationsEditCtrl.$resolve = {
  evalSessionEvaluation: [
    '$state',
    '$stateParams',
    'EvalSessionEvaluation',
    function($state, $stateParams, EvalSessionEvaluation) {
      return EvalSessionEvaluation.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { EvalSessionEvaluationsEditCtrl };
