function ProgrammeProgrammePrioritiesSearchCtrl($scope, $stateParams, programmePriorities) {
  $scope.programmeId = $stateParams.id;
  $scope.programmeStatus = $scope.info.status;

  $scope.programmePriorities = programmePriorities;
}

ProgrammeProgrammePrioritiesSearchCtrl.$inject = ['$scope', '$stateParams', 'programmePriorities'];

ProgrammeProgrammePrioritiesSearchCtrl.$resolve = {
  programmePriorities: [
    '$stateParams',
    'ProgrammePriority',
    function($stateParams, ProgrammePriority) {
      return ProgrammePriority.getForProgramme({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProgrammeProgrammePrioritiesSearchCtrl };
