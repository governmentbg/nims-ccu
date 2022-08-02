function EvalSessionSheetsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  EvalSessionSheet,
  evalSessionSheet
) {
  $scope.evalSessionId = $stateParams.id;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.evalSessionSheet = evalSessionSheet;

  $scope.cancelSheet = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_editEvalSessionSheet_cancelSheetConfirm',
      noteLabel: 'evalSessions_editEvalSessionSheet_cancelMessage',
      resource: 'EvalSessionSheet',
      validationAction: 'canCancel',
      action: 'cancelSheet',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionSheet.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.continueSheet = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_editEvalSessionSheet_continueSheetConfirm',
      resource: 'EvalSessionSheet',
      validationAction: 'canContinue',
      action: 'continueSheet',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.sheets.edit', {
          id: result.result.evalSessionId,
          ind: result.result.evalSessionSheetId
        });
      }
    });
  };
}

EvalSessionSheetsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'EvalSessionSheet',
  'evalSessionSheet'
];

EvalSessionSheetsEditCtrl.$resolve = {
  evalSessionSheet: [
    '$stateParams',
    'EvalSessionSheet',
    function($stateParams, EvalSessionSheet) {
      return EvalSessionSheet.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { EvalSessionSheetsEditCtrl };
