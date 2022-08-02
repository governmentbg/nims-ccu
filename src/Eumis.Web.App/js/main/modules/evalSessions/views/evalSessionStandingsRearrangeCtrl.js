function EvalSessionStandingsRearrangeCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scConfirm,
  evalSessionStanding,
  EvalSessionStandingRearrange
) {
  $scope.evalSessionStanding = evalSessionStanding;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.standings.edit', {
      id: $stateParams.id,
      ind: $stateParams.ind
    });
  };

  $scope.moveUp = function(rowData) {
    return EvalSessionStandingRearrange.moveUp(
      {
        id: $stateParams.id,
        ind: $stateParams.ind,
        idx: rowData.projectId,
        version: $scope.evalSessionStanding.version
      },
      {}
    ).$promise.then(() => $state.partialReload());
  };

  $scope.moveDown = function(rowData) {
    return EvalSessionStandingRearrange.moveDown(
      {
        id: $stateParams.id,
        ind: $stateParams.ind,
        idx: rowData.projectId,
        version: $scope.evalSessionStanding.version
      },
      {}
    ).$promise.then(() => $state.partialReload());
  };

  $scope.apply = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_rearrangeEvalSessionStanding_applyConfirm',
      resource: 'EvalSessionStandingRearrange',
      validationAction: 'canApply',
      action: 'apply',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionStanding.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.standings.edit', {
          id: $stateParams.id,
          ind: $stateParams.ind
        });
      }
    });
  };
}

EvalSessionStandingsRearrangeCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scConfirm',
  'evalSessionStanding',
  'EvalSessionStandingRearrange'
];

EvalSessionStandingsRearrangeCtrl.$resolve = {
  evalSessionStanding: [
    '$stateParams',
    'EvalSessionStandingRearrange',
    function($stateParams, EvalSessionStandingRearrange) {
      return EvalSessionStandingRearrange.get({ id: $stateParams.id, ind: $stateParams.ind })
        .$promise;
    }
  ]
};

export { EvalSessionStandingsRearrangeCtrl };
