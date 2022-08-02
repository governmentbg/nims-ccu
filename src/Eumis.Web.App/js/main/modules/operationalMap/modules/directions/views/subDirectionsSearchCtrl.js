function SubDirectionsSearchCtrl($scope, $stateParams, subDirections) {
  $scope.subDirections = subDirections;
}

SubDirectionsSearchCtrl.$inject = ['$scope', '$stateParams', 'subDirections'];

SubDirectionsSearchCtrl.$resolve = {
  subDirections: [
    'SubDirection',
    '$stateParams',
    function(SubDirection, $stateParams) {
      return SubDirection.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { SubDirectionsSearchCtrl };
