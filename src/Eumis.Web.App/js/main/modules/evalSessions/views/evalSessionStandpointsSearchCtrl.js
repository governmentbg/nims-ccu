import _ from 'lodash';

function EvalSessionStandpointsSearchCtrl($scope, $state, $stateParams, evalSessionStandpoints) {
  $scope.evalSessionId = $stateParams.id;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.evalSessionStandpoints = evalSessionStandpoints;

  $scope.filters = {
    project: null,
    user: null,
    statuses: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.evalSessions.view.standpoints.search', {
      id: $stateParams.id,
      project: $scope.filters.project,
      user: $scope.filters.user,
      statuses: $scope.filters.statuses
    });
  };
}

EvalSessionStandpointsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'evalSessionStandpoints'
];

EvalSessionStandpointsSearchCtrl.$resolve = {
  evalSessionStandpoints: [
    '$stateParams',
    'EvalSessionStandpoint',
    function($stateParams, EvalSessionStandpoint) {
      if ($stateParams.statuses) {
        if (typeof $stateParams.statuses === 'string') {
          $stateParams.statuses = new Array($stateParams.statuses);
        }
      } else {
        $stateParams.statuses = null;
      }
      return EvalSessionStandpoint.query($stateParams).$promise;
    }
  ]
};

export { EvalSessionStandpointsSearchCtrl };
