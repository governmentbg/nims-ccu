function MonitorstatViewCtrl($scope, $stateParams, survey) {
  $scope.survey = survey;
  $scope.reports = survey.reports;
}

MonitorstatViewCtrl.$inject = ['$scope', '$stateParams', 'survey'];

MonitorstatViewCtrl.$resolve = {
  survey: [
    'Monitorstat',
    '$stateParams',
    function(Monitorstat, $stateParams) {
      return Monitorstat.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { MonitorstatViewCtrl };
