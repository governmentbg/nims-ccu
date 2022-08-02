function ProcedureMonitorstatEconomicActivitiesNewCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProcedureMonitorstatEconomicActivity,
  procedureMonitorstatEconomicActivity
) {
  $scope.procedureId = $stateParams.id;
  $scope.economicActivity = procedureMonitorstatEconomicActivity;

  $scope.save = function() {
    return $scope.newProcedureMonitorsatEconomicActivityForm.$validate().then(function() {
      if ($scope.newProcedureMonitorsatEconomicActivityForm.$valid) {
        return scConfirm({
          resource: 'ProcedureMonitorstatEconomicActivity',
          validationAction: 'canCreate',
          action: 'save',
          params: {
            id: $stateParams.id
          },
          data: $scope.economicActivity
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.procedures.view.monitorstat.search');
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.monitorstat.search');
  };
}

ProcedureMonitorstatEconomicActivitiesNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProcedureMonitorstatEconomicActivity',
  'procedureMonitorstatEconomicActivity'
];

ProcedureMonitorstatEconomicActivitiesNewCtrl.$resolve = {
  procedureMonitorstatEconomicActivity: [
    'ProcedureMonitorstatEconomicActivity',
    '$stateParams',
    function(ProcedureMonitorstatEconomicActivity, $stateParams) {
      return ProcedureMonitorstatEconomicActivity.newProcedureMonitorstatEconomicActivity({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureMonitorstatEconomicActivitiesNewCtrl };
