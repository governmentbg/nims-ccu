function MeasuresSearchCtrl($scope, $stateParams, measures) {
  $scope.measures = measures;
}

MeasuresSearchCtrl.$inject = ['$scope', '$stateParams', 'measures'];

MeasuresSearchCtrl.$resolve = {
  measures: [
    'Measure',
    function(Measure) {
      return Measure.query().$promise;
    }
  ]
};

export { MeasuresSearchCtrl };
