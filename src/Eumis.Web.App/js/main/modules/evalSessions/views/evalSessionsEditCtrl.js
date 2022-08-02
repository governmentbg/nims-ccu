function EvalSessionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scConfirm,
  $interpolate,
  EvalSession,
  evalSession
) {
  $scope.evalSession = evalSession;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editEvalSessionForm.$validate().then(function() {
      if ($scope.editEvalSessionForm.$valid) {
        return EvalSession.update({ id: $stateParams.id }, $scope.evalSession).$promise.then(
          function() {
            return $state.partialReload();
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.changeStatus = function(evalSessionStatus) {
    var validationAction = null,
      confirmMsg = $interpolate(l10n.get('evalSessions_editEvalSession_changeStatusConfirm'))({
        status: l10n.get('evalSessions_editEvalSessions_' + evalSessionStatus)
      });
    if (evalSessionStatus === 'active') {
      validationAction = 'canChangeStatusToActive';
    } else if (evalSessionStatus === 'ended') {
      validationAction = 'canChangeStatusToEnded';
    } else if (evalSessionStatus === 'endedByLAG') {
      validationAction = 'canChangeStatusToEndedByLAG';
    } else if (evalSessionStatus === 'draft') {
      validationAction = 'canChangeStatusToDraft';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      resource: 'EvalSession',
      validationAction: validationAction,
      action:
        'changeStatusTo' + evalSessionStatus.charAt(0).toUpperCase() + evalSessionStatus.slice(1),
      params: {
        id: $stateParams.id,
        version: $scope.evalSession.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

EvalSessionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scConfirm',
  '$interpolate',
  'EvalSession',
  'evalSession'
];

EvalSessionsEditCtrl.$resolve = {
  evalSession: [
    'EvalSession',
    '$stateParams',
    function(EvalSession, $stateParams) {
      return EvalSession.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { EvalSessionsEditCtrl };
