function PPrioritiesNewCtrl($scope, $state, scConfirm, programmePriority) {
  $scope.programmePriority = programmePriority;

  $scope.save = function() {
    return $scope.newProgrammePriorityForm.$validate().then(function() {
      if ($scope.newProgrammePriorityForm.$valid) {
        return scConfirm({
          resource: 'ProgrammePriority',
          validationAction: 'canCreate',
          action: 'save',
          data: $scope.programmePriority
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.map.ppriorities.view.edit', {
              id: result.result.programmePriorityId
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.programmes.view.programmePriorities.search');
  };
}

PPrioritiesNewCtrl.$inject = ['$scope', '$state', 'scConfirm', 'programmePriority'];

PPrioritiesNewCtrl.$resolve = {
  programmePriority: [
    'ProgrammePriority',
    '$stateParams',
    function(ProgrammePriority, $stateParams) {
      return ProgrammePriority.newProgrammePriority({
        programmeId: $stateParams.id
      }).$promise;
    }
  ]
};

export { PPrioritiesNewCtrl };
