function DirectionsSearchCtrl($scope, $stateParams, directions) {
  $scope.directions = directions;
}

DirectionsSearchCtrl.$inject = ['$scope', '$stateParams', 'directions'];

DirectionsSearchCtrl.$resolve = {
  directions: [
    'Direction',
    function(Direction) {
      return Direction.query().$promise;
    }
  ]
};

export { DirectionsSearchCtrl };
