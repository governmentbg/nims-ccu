function EvalSessionProjectStandingsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  EvalSessionProjectStanding,
  evalSessionProjectStanding
) {
  $scope.evalSessionProjectStanding = evalSessionProjectStanding;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';

  $scope.back = function() {
    return $state.go('root.evalSessions.view.projects.view', {
      id: $stateParams.id,
      ind: $stateParams.ind
    });
  };

  $scope.cancelStanding = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_evalSessionProjectStandingEdit_deleteConfirm',
      noteLabel: 'evalSessions_evalSessionProjectStandingEdit_deleteMessage',
      resource: 'EvalSessionProjectStanding',
      action: 'cancel',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        sid: $stateParams.sid,
        version: $scope.evalSessionProjectStanding.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

EvalSessionProjectStandingsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'EvalSessionProjectStanding',
  'evalSessionProjectStanding'
];

EvalSessionProjectStandingsEditCtrl.$resolve = {
  evalSessionProjectStanding: [
    '$stateParams',
    'EvalSessionProjectStanding',
    function($stateParams, EvalSessionProjectStanding) {
      return EvalSessionProjectStanding.get({
        id: $stateParams.id,
        ind: $stateParams.ind,
        sid: $stateParams.sid
      }).$promise;
    }
  ]
};

export { EvalSessionProjectStandingsEditCtrl };
