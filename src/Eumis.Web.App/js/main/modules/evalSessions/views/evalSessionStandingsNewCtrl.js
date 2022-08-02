function EvalSessionStandingsNewCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scMessage,
  EvalSessionStanding,
  newEvalSessionStanding
) {
  $scope.newEvalSessionStanding = newEvalSessionStanding;

  $scope.save = function() {
    return $scope.newEvalSessionStandingForm.$validate().then(function() {
      if ($scope.newEvalSessionStandingForm.$valid) {
        return EvalSessionStanding.save(
          { id: $stateParams.id },
          $scope.newEvalSessionStanding
        ).$promise.then(function(newEvalSessionStanding) {
          return $state.go('root.evalSessions.view.standings.edit', {
            ind: newEvalSessionStanding.evalSessionStandingId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.standings.search');
  };
}

EvalSessionStandingsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scMessage',
  'EvalSessionStanding',
  'newEvalSessionStanding'
];

EvalSessionStandingsNewCtrl.$resolve = {
  newEvalSessionStanding: [
    '$stateParams',
    'EvalSessionStanding',
    function($stateParams, EvalSessionStanding) {
      return EvalSessionStanding.newEvalSessionStanding({
        id: $stateParams.id,
        type: $stateParams.t
      }).$promise;
    }
  ]
};

export { EvalSessionStandingsNewCtrl };
