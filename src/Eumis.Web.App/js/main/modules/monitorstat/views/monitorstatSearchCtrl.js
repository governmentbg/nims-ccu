function MonitorstatSearchCtrl($scope, $state, surveys) {
  $scope.surveys = surveys;
}

MonitorstatSearchCtrl.$inject = ['$scope', '$state', 'surveys'];

MonitorstatSearchCtrl.$resolve = {
  surveys: [
    '$stateParams',
    'Monitorstat',
    function($stateParams, Monitorstat) {
      return Monitorstat.query($stateParams).$promise;
    }
  ]
};

export { MonitorstatSearchCtrl };
