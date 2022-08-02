function MeasuresEditCtrl($scope, $state, $stateParams, scConfirm, Measure, measure) {
  $scope.editMode = null;
  $scope.measure = measure;

  $scope.save = function() {
    return $scope.editMeasureForm.$validate().then(function() {
      if ($scope.editMeasureForm.$valid) {
        return Measure.update({ id: $stateParams.id }, $scope.measure).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'Measure',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.measure.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.measures.search');
      }
    });
  };
}

MeasuresEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'scConfirm', 'Measure', 'measure'];

MeasuresEditCtrl.$resolve = {
  measure: [
    'Measure',
    '$stateParams',
    function(Measure, $stateParams) {
      return Measure.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { MeasuresEditCtrl };
