function EvalSessionStandpointsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  evalSessionStandpoint
) {
  $scope.evalSessionId = $stateParams.id;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.evalSessionStandpoint = evalSessionStandpoint;

  $scope.cancelStandpoint = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_editEvalSessionStandpoint_cancelConfirm',
      noteLabel: 'evalSessions_editEvalSessionStandpoint_cancelMessage',
      resource: 'EvalSessionStandpoint',
      action: 'cancelStandpoint',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionStandpoint.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

EvalSessionStandpointsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'evalSessionStandpoint'
];

EvalSessionStandpointsEditCtrl.$resolve = {
  evalSessionStandpoint: [
    '$stateParams',
    'EvalSessionStandpoint',
    function($stateParams, EvalSessionStandpoint) {
      return EvalSessionStandpoint.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { EvalSessionStandpointsEditCtrl };
