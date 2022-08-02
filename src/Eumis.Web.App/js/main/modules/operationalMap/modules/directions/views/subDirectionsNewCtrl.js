function SubDirectionsNewCtrl($scope, $state, scConfirm, $stateParams, subDirection, SubDirection) {
  $scope.subDirection = subDirection;

  $scope.save = function() {
    return $scope.newSubDirectionForm.$validate().then(function() {
      if ($scope.newSubDirectionForm.$valid) {
        return SubDirection.save(
          { id: $stateParams.id, version: $scope.subDirection.version },
          $scope.subDirection
        ).$promise.then(function(data) {
          return $state.go('root.map.directions.view.subDirections.edit', {
            id: data.directionId,
            ind: data.subDirectionId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.directions.view.subDirections.search');
  };
}

SubDirectionsNewCtrl.$inject = [
  '$scope',
  '$state',
  'scConfirm',
  '$stateParams',
  'subDirection',
  'SubDirection'
];

SubDirectionsNewCtrl.$resolve = {
  subDirection: [
    'SubDirection',
    '$stateParams',
    function(SubDirection, $stateParams) {
      return SubDirection.newSubDirection({ id: $stateParams.id }).$promise;
    }
  ]
};

export { SubDirectionsNewCtrl };
