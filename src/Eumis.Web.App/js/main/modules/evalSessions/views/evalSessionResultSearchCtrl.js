function EvalSessionResultsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  EvalSessionResult,
  evalSessionResults
) {
  $scope.evalSessionId = $stateParams.id;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.isSessionEndedByLAG = $scope.evalSessionInfo.evalSessionStatusName === 'endedByLAG';
  $scope.evalSessionResults = evalSessionResults;

  $scope.create = function(type) {
    return scConfirm({
      resource: 'EvalSessionResult',
      validationAction: 'canCreate',
      params: {
        id: $scope.evalSessionId,
        type: type
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.result.new', { type: type });
      }
    });
  };
}

EvalSessionResultsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'EvalSessionResult',
  'evalSessionResults'
];

EvalSessionResultsSearchCtrl.$resolve = {
  evalSessionResults: [
    '$stateParams',
    'EvalSessionResult',
    function($stateParams, EvalSessionResult) {
      return EvalSessionResult.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { EvalSessionResultsSearchCtrl };
