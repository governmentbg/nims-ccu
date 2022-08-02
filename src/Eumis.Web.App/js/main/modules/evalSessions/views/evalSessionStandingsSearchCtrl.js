function EvalSessionStandingsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  EvalSessionStanding,
  evalSessionStandings
) {
  $scope.evalSessionId = $stateParams.id;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.evalSessionStandings = evalSessionStandings;

  $scope.create = function(type) {
    return scConfirm({
      resource: 'EvalSessionStanding',
      validationAction: 'canCreate',
      params: {
        id: $scope.evalSessionId,
        type: type
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.standings.new', {
          t: type
        });
      }
    });
  };
}

EvalSessionStandingsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'EvalSessionStanding',
  'evalSessionStandings'
];

EvalSessionStandingsSearchCtrl.$resolve = {
  evalSessionStandings: [
    '$stateParams',
    'EvalSessionStanding',
    function($stateParams, EvalSessionStanding) {
      return EvalSessionStanding.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { EvalSessionStandingsSearchCtrl };
