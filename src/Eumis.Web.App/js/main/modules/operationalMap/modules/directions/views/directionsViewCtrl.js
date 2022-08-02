function DirectionsViewCtrl($scope, $state, $stateParams, info) {
  $scope.info = info;

  $scope.tabList = {
    directions_tabs_basicData: 'root.map.directions.view.edit',
    directions_tabs_subDirections: 'root.map.directions.view.subDirections'
  };
}

DirectionsViewCtrl.$inject = ['$scope', '$state', '$stateParams', 'info'];

DirectionsViewCtrl.$resolve = {
  info: [
    'Direction',
    '$stateParams',
    function(Direction, $stateParams) {
      return Direction.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { DirectionsViewCtrl };
