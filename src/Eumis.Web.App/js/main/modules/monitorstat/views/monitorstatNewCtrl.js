// eslint-disable-next-line no-unused-vars
function MonitorstatNewCtrl($scope, $state, scConfirm, Monitorstat) {
  $scope.load = function() {
    return $scope.newMonitorstatForm.$validate().then(function() {
      if ($scope.newMonitorstatForm.$valid) {
        scConfirm({
          confirmMessage: 'monitorstat_newMonitorstatForm_confirmExternalLoading',
          validationAction: 'canLoadMonitorstatSurveys',
          resource: 'Monitorstat',
          action: 'loadMonitorstatSurveys',
          params: {
            id: $scope.model.year
          }
        }).then(function(result) {
          if (result.executed) {
            $state.go('root.monitorstat.search');
          } else {
            $state.partialReload();
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.monitorstat.search');
  };
}

MonitorstatNewCtrl.$inject = ['$scope', '$state', 'scConfirm', 'Monitorstat'];

export { MonitorstatNewCtrl };
