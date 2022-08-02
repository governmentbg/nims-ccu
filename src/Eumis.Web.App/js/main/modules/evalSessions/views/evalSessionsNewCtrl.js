function EvalSessionsNewCtrl($scope, $state, $stateParams, EvalSession, newEvalSession) {
  $scope.newEvalSession = newEvalSession;

  $scope.save = function() {
    return $scope.newEvalSessionForm.$validate().then(function() {
      if ($scope.newEvalSessionForm.$valid) {
        return EvalSession.save($scope.newEvalSession).$promise.then(function(result) {
          return $state.go('root.evalSessions.view.edit', {
            id: result.evalSessionId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.search');
  };
}

EvalSessionsNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'EvalSession', 'newEvalSession'];

EvalSessionsNewCtrl.$resolve = {
  newEvalSession: [
    '$stateParams',
    'EvalSession',
    function($stateParams, EvalSession) {
      return EvalSession.newEvalSession().$promise;
    }
  ]
};

export { EvalSessionsNewCtrl };
