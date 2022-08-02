function ExecuteAutomaticProjectEvaluationsModalCtrl(
  $scope,
  $state,
  $uibModalInstance,
  scConfirm,
  scModalParams,
  l10n
) {
  $scope.errors = [];
  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };

  $scope.ok = function() {
    return $scope.performAutomaticProjectEvaluationsForm.$validate().then(function() {
      if ($scope.performAutomaticProjectEvaluationsForm.$valid) {
        $scope.isReadonly = true;

        return scConfirm({
          confirmMessage: 'evalSessions_performAutomaticProjectEvaluationsModal_confirm',
          resource: 'EvalSessionAutomaticProjectEvaluation',
          validationAction: 'canExecute',
          action: 'save',
          params: {
            id: scModalParams.evalSessionId,
            version: scModalParams.version
          },
          data: $scope.model.file
        }).then(function(result) {
          if (result.executed) {
            $scope.errors = result.result.errors;

            if (result.result.errors.length === 0) {
              $scope.successMessage = l10n.get(
                'evalSessions_performAutomaticProjectEvaluationsModal_automaticEvaluationSuccess'
              );
            }

            return $state.partialReload();
          } else {
            $scope.isReadonly = false;
          }
        });
      }
    });
  };
}

ExecuteAutomaticProjectEvaluationsModalCtrl.$inject = [
  '$scope',
  '$state',
  '$uibModalInstance',
  'scConfirm',
  'scModalParams',
  'l10n'
];

export { ExecuteAutomaticProjectEvaluationsModalCtrl };
