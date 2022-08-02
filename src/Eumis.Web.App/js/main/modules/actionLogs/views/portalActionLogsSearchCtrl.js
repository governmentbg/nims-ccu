import _ from 'lodash';

function PortalActionLogsSearchCtrl($scope, $state, $stateParams, ActionLog, actionLogs) {
  $scope.actionLogs = actionLogs;

  $scope.filters = {
    actionId: null,
    aggregateRootId: null,
    email: null,
    remoteIpAddress: null,
    logDate: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined) {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.portalActionLogs.search', $scope.filters);
  };
}

PortalActionLogsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ActionLog',
  'actionLogs'
];

PortalActionLogsSearchCtrl.$resolve = {
  actionLogs: [
    '$stateParams',
    'ActionLog',
    function($stateParams, ActionLog) {
      return ActionLog.getPortal($stateParams).$promise;
    }
  ]
};

export { PortalActionLogsSearchCtrl };
