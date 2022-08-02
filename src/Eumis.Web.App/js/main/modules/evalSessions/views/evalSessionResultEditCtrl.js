function EvalSessionResultsEditCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  $interpolate,
  scConfirm,
  EvalSessionResult,
  evalSessionResult
) {
  $scope.showLoadbtn = false;
  $scope.showClearbtn = false;
  $scope.yesText = l10n.get('common_texts_yes');
  $scope.noText = l10n.get('common_texts_no');

  $scope.isSessionEndedByLAG = $scope.evalSessionInfo.evalSessionStatusName === 'endedByLAG';

  $scope.evalSessionResult = evalSessionResult;
  if (evalSessionResult.type === 'adminAdmiss') {
    $scope.adminAdmissProjects = EvalSessionResult.getAdminAdmissProjects({
      id: $stateParams.id,
      ind: $stateParams.ind
    });
    $scope.adminAdmissProjects.$promise.then(result => {
      if (result.$resolved) {
        $scope.showLoadbtn = !$scope.adminAdmissProjects.length;
        $scope.showClearbtn = !$scope.showLoadbtn;
      }
    });
  }

  if (evalSessionResult.type === 'preliminary') {
    $scope.preliminaryProjects = EvalSessionResult.getPreliminaryProjects({
      id: $stateParams.id,
      ind: $stateParams.ind
    });
    $scope.preliminaryProjects.$promise.then(result => {
      if (result.$resolved) {
        $scope.showLoadbtn = !$scope.preliminaryProjects.length;
        $scope.showClearbtn = !$scope.showLoadbtn;
      }
    });
  }

  if (evalSessionResult.type === 'standing') {
    $scope.standingProjects = EvalSessionResult.getStandingProjects({
      id: $stateParams.id,
      ind: $stateParams.ind
    });
    $scope.standingProjects.$promise.then(result => {
      if (result.$resolved) {
        $scope.showLoadbtn = !$scope.standingProjects.length;
        $scope.showClearbtn = !$scope.showLoadbtn;
      }
    });
  }

  $scope.doAction = function(action) {
    var confirmMessage = undefined;
    var validationAction = undefined;
    var noteLabel = undefined;
    if (action === 'publish' || action === 'cancel') {
      confirmMessage = `evalSessions_editEvalSessionResult_${action}Confirm`;
      if (action === 'cancel') {
        noteLabel = 'evalSessions_editEvalSessionResult_cancelMessage';
      }
    }

    if (action === 'publish' || action === 'loadProjects') {
      validationAction = 'can' + action.charAt(0).toUpperCase() + action.slice(1);
    }

    return scConfirm({
      confirmMessage: confirmMessage,
      resource: 'EvalSessionResult',
      validationAction: validationAction,
      action: action,
      noteLabel: noteLabel,
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionResult.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

EvalSessionResultsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  '$interpolate',
  'scConfirm',
  'EvalSessionResult',
  'evalSessionResult'
];

EvalSessionResultsEditCtrl.$resolve = {
  evalSessionResult: [
    '$stateParams',
    'EvalSessionResult',
    function($stateParams, EvalSessionResult) {
      return EvalSessionResult.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { EvalSessionResultsEditCtrl };
