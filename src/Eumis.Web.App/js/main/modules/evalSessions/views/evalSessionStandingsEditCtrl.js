function EvalSessionStandingsEditCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scConfirm,
  EvalSessionStanding,
  evalSessionStanding
) {
  $scope.evalSessionStanding = evalSessionStanding;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';

  $scope.refuseStanding = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_editEvalSessionStanding_refuseConfirm',
      noteLabel: 'evalSessions_editEvalSessionStanding_refuseMessage',
      resource: 'EvalSessionStanding',
      validationAction: 'canRefuse',
      action: 'refuse',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionStanding.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
  $scope.rearrangeStanding = function() {
    return $state.go('root.evalSessions.view.standings.rearrange', {
      id: $stateParams.id,
      ind: evalSessionStanding.evalSessionStandingId
    });
  };
}

EvalSessionStandingsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scConfirm',
  'EvalSessionStanding',
  'evalSessionStanding'
];

EvalSessionStandingsEditCtrl.$resolve = {
  evalSessionStanding: [
    '$stateParams',
    'EvalSessionStanding',
    function($stateParams, EvalSessionStanding) {
      return EvalSessionStanding.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { EvalSessionStandingsEditCtrl };
