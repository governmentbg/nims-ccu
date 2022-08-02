function MeasuresNewCtrl($scope, $state, Measure, newMeasure) {
  $scope.newMeasure = newMeasure;

  $scope.save = function() {
    return $scope.newMeasureForm.$validate().then(function() {
      if ($scope.newMeasureForm.$valid) {
        return Measure.save($scope.newMeasure).$promise.then(function() {
          return $state.go('root.map.measures.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.measures.search');
  };
}

MeasuresNewCtrl.$inject = ['$scope', '$state', 'Measure', 'newMeasure'];

MeasuresNewCtrl.$resolve = {
  newMeasure: [
    'Measure',
    function(Measure) {
      return Measure.newMeasure().$promise;
    }
  ]
};

export { MeasuresNewCtrl };
