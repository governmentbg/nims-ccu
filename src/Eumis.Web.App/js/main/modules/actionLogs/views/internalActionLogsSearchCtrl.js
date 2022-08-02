import _ from 'lodash';

function InternalActionLogsSearchCtrl($scope, $state, $stateParams, ActionLog, actionLogs) {
  $scope.actionLogs = actionLogs;

  $scope.filters = {
    actionId: null,
    aggregateRootId: null,
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
    return $state.go('root.internalActionLogs.search', $scope.filters);
  };
}

InternalActionLogsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ActionLog',
  'actionLogs'
];

InternalActionLogsSearchCtrl.$resolve = {
  actionLogs: [
    '$stateParams',
    'ActionLog',
    function($stateParams, ActionLog) {
      return ActionLog.getInternal($stateParams).$promise;
    }
  ]
};

export { InternalActionLogsSearchCtrl };
