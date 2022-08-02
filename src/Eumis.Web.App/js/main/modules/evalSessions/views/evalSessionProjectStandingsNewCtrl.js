function EvalSessionProjectStandingsNewCtrl(
  $scope,
  $state,
  $stateParams,
  EvalSessionProjectStanding,
  evalSessionProjectStanding
) {
  $scope.evalSessionProjectStanding = evalSessionProjectStanding;

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.projects.view', {
      id: $stateParams.id,
      ind: $stateParams.ind
    });
  };

  $scope.save = function() {
    return $scope.evalSessionProjectStandingNewForm.$validate().then(function() {
      if ($scope.evalSessionProjectStandingNewForm.$valid) {
        return EvalSessionProjectStanding.save(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.evalSessionProjectStanding
        ).$promise.then(function() {
          return $state.go('root.evalSessions.view.projects.view', {
            id: $stateParams.id,
            ind: $stateParams.ind
          });
        });
      }
    });
  };
}

EvalSessionProjectStandingsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'EvalSessionProjectStanding',
  'evalSessionProjectStanding'
];

EvalSessionProjectStandingsNewCtrl.$resolve = {
  evalSessionProjectStanding: [
    '$stateParams',
    'EvalSessionProjectStanding',
    function($stateParams, EvalSessionProjectStanding) {
      return EvalSessionProjectStanding.newEvalSessionProjectStanding({
        id: $stateParams.id,
        ind: $stateParams.ind,
        isPreliminary: $stateParams.p
      }).$promise;
    }
  ]
};

export { EvalSessionProjectStandingsNewCtrl };
