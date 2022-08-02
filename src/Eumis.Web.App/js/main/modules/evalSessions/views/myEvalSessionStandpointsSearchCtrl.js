import _ from 'lodash';

function MyEvalSessionStandpointsSearchCtrl($scope, $state, $stateParams, evalSessionStandpoints) {
  $scope.evalSessionId = $stateParams.id;
  $scope.evalSessionStandpoints = evalSessionStandpoints;

  $scope.filters = {
    project: null,
    statuses: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.evalSessions.my.view.standpoints.search', {
      id: $stateParams.id,
      project: $scope.filters.project,
      statuses: $scope.filters.statuses
    });
  };
}

MyEvalSessionStandpointsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'evalSessionStandpoints'
];

MyEvalSessionStandpointsSearchCtrl.$resolve = {
  evalSessionStandpoints: [
    '$stateParams',
    'MyEvalSessionStandpoint',
    function($stateParams, MyEvalSessionStandpoint) {
      if ($stateParams.statuses) {
        $stateParams.statuses = $stateParams.statuses.split(',');
      } else {
        $stateParams.statuses = null;
      }
      return MyEvalSessionStandpoint.query($stateParams).$promise;
    }
  ]
};

export { MyEvalSessionStandpointsSearchCtrl };
