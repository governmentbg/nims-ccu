function ProcedureActionLogsViewCtrl($scope, $state, actionLog) {
  $scope.actionLog = actionLog;

  $scope.cancel = function() {
    return $state.go('root.procedureActionLogs.search');
  };
}

ProcedureActionLogsViewCtrl.$inject = ['$scope', '$state', 'actionLog'];

ProcedureActionLogsViewCtrl.$resolve = {
  actionLog: [
    'ActionLog',
    '$stateParams',
    function(ActionLog, $stateParams) {
      return ActionLog.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureActionLogsViewCtrl };
