function ProgrammeDirectionsSearchCtrl($scope, $stateParams, directions) {
  $scope.programmePriorityStatus = $scope.info.status;
  $scope.directions = directions;

  $scope.programmeId = $stateParams.id;
}

ProgrammeDirectionsSearchCtrl.$inject = ['$scope', '$stateParams', 'directions'];

ProgrammeDirectionsSearchCtrl.$resolve = {
  directions: [
    '$stateParams',
    'ProgrammeDirection',
    function($stateParams, ProgrammeDirection) {
      return ProgrammeDirection.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProgrammeDirectionsSearchCtrl };
