function ProgrammePriorityDirectionsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProgrammePriorityDirection,
  programmePriorityDirection,
  scConfirm
) {
  $scope.programmePriorityDirection = programmePriorityDirection;

  $scope.save = function() {
    return $scope.newPPriorityDirectionForm.$validate().then(function() {
      if ($scope.newPPriorityDirectionForm.$valid) {
        return scConfirm({
          validationAction: 'canCreate',
          resource: 'ProgrammePriorityDirection',
          action: 'save',
          params: {
            id: $stateParams.id
          },
          data: $scope.programmePriorityDirection
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.map.ppriorities.view.directions.search');
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.ppriorities.view.directions.search');
  };
}

ProgrammePriorityDirectionsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProgrammePriorityDirection',
  'programmePriorityDirection',
  'scConfirm'
];

ProgrammePriorityDirectionsNewCtrl.$resolve = {
  programmePriorityDirection: [
    'ProgrammePriorityDirection',
    '$stateParams',
    function(ProgrammePriorityDirection, $stateParams) {
      return ProgrammePriorityDirection.newDirection({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProgrammePriorityDirectionsNewCtrl };
