import _ from 'lodash';

function EvalSessionsSearchCtrl($scope, $state, $stateParams, evalSessions) {
  $scope.filters = {
    procedureId: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.evalSessions = evalSessions;

  $scope.search = function() {
    return $state.go('root.evalSessions.search', {
      procedureId: $scope.filters.procedureId
    });
  };
}

EvalSessionsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'evalSessions'];

EvalSessionsSearchCtrl.$resolve = {
  evalSessions: [
    '$stateParams',
    'EvalSession',
    function($stateParams, EvalSession) {
      return EvalSession.query($stateParams).$promise;
    }
  ]
};

export { EvalSessionsSearchCtrl };
