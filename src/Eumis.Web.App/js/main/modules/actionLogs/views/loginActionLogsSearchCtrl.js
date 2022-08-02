import _ from 'lodash';

function LoginActionLogsSearchCtrl($scope, $state, $stateParams, ActionLog, actionLogs) {
  $scope.actionLogs = actionLogs;

  $scope.filters = {
    username: null,
    remoteIpAddress: null,
    logDate: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined) {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.loginActionLogs.search', $scope.filters);
  };
}

LoginActionLogsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'ActionLog', 'actionLogs'];

LoginActionLogsSearchCtrl.$resolve = {
  actionLogs: [
    '$stateParams',
    'ActionLog',
    function($stateParams, ActionLog) {
      return ActionLog.getUnsuccessfulLogin($stateParams).$promise;
    }
  ]
};

export { LoginActionLogsSearchCtrl };
