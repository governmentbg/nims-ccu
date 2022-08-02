function EvalSessionResultsNewCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scMessage,
  EvalSessionResult,
  newEvalSessionResult
) {
  $scope.newEvalSessionResult = newEvalSessionResult;

  $scope.save = function() {
    return $scope.newEvalSessionResultForm.$validate().then(function() {
      if ($scope.newEvalSessionResultForm.$valid) {
        return EvalSessionResult.save(
          { id: $stateParams.id },
          $scope.newEvalSessionResult
        ).$promise.then(function(newEvalSessionResult) {
          return $state.go('root.evalSessions.view.result.edit', {
            ind: newEvalSessionResult.evalSessionResultId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.result.search');
  };
}

EvalSessionResultsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scMessage',
  'EvalSessionResult',
  'newEvalSessionResult'
];

EvalSessionResultsNewCtrl.$resolve = {
  newEvalSessionResult: [
    '$stateParams',
    'EvalSessionResult',
    '$state',
    function($stateParams, EvalSessionResult, $state) {
      $state.mggg = 123;
      return EvalSessionResult.newEvalSessionResult({
        id: $stateParams.id,
        type: $stateParams.type
      }).$promise;
    }
  ]
};

export { EvalSessionResultsNewCtrl };
