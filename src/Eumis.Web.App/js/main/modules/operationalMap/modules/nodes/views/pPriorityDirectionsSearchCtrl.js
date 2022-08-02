function ProgrammePriorityDirectionsSearchCtrl($scope, $stateParams, directions) {
  $scope.programmePriorityStatus = $scope.info.status;
  $scope.directions = directions;

  $scope.programmeId = $stateParams.id;
}

ProgrammePriorityDirectionsSearchCtrl.$inject = ['$scope', '$stateParams', 'directions'];

ProgrammePriorityDirectionsSearchCtrl.$resolve = {
  directions: [
    '$stateParams',
    'ProgrammePriorityDirection',
    function($stateParams, ProgrammePriorityDirection) {
      return ProgrammePriorityDirection.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProgrammePriorityDirectionsSearchCtrl };
