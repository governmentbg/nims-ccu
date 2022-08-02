function EvalSessionDistributionsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  EvalSessionDistribution,
  evalSessionDistributions
) {
  $scope.evalSessionId = $stateParams.id;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.evalSessionDistributions = evalSessionDistributions;

  $scope.create = function(evalTableType) {
    return scConfirm({
      resource: 'EvalSessionDistribution',
      validationAction: 'canCreate',
      params: {
        id: $scope.evalSessionId,
        evalTableType: evalTableType
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.distributions.new', {
          t: evalTableType
        });
      }
    });
  };
}

EvalSessionDistributionsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'EvalSessionDistribution',
  'evalSessionDistributions'
];

EvalSessionDistributionsSearchCtrl.$resolve = {
  evalSessionDistributions: [
    '$stateParams',
    'EvalSessionDistribution',
    function($stateParams, EvalSessionDistribution) {
      return EvalSessionDistribution.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { EvalSessionDistributionsSearchCtrl };
