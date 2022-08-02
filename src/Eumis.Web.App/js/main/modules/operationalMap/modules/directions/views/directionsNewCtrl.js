function DirectionsNewCtrl($scope, $state, scConfirm, direction) {
  $scope.direction = direction;

  $scope.save = function() {
    return $scope.newDirectionForm.$validate().then(function() {
      if ($scope.newDirectionForm.$valid) {
        return scConfirm({
          resource: 'Direction',
          action: 'save',
          data: $scope.direction
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.map.directions.view.edit', {
              id: result.result.directionId
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.directions.search');
  };
}

DirectionsNewCtrl.$inject = ['$scope', '$state', 'scConfirm', 'direction'];

DirectionsNewCtrl.$resolve = {
  direction: [
    'Direction',
    function(Direction) {
      return Direction.newDirection().$promise;
    }
  ]
};

export { DirectionsNewCtrl };
