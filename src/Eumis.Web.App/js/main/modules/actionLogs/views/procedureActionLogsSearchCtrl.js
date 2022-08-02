import angular from 'angular';
import _ from 'lodash';

function ProcedureActionLogsSearchCtrl($scope, $state, $stateParams, ActionLog, actionLogs) {
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
    return $state.go(
      'root.procedureActionLogs.search',
      angular.extend({}, $scope.filters, { procedureActionsOnly: true })
    );
  };
}

ProcedureActionLogsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ActionLog',
  'actionLogs'
];

ProcedureActionLogsSearchCtrl.$resolve = {
  actionLogs: [
    '$stateParams',
    'ActionLog',
    function($stateParams, ActionLog) {
      return ActionLog.getInternal(angular.extend({}, $stateParams, { procedureActionsOnly: true }))
        .$promise;
    }
  ]
};

export { ProcedureActionLogsSearchCtrl };
