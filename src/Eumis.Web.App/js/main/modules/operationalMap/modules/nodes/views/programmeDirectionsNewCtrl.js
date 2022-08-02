function ProgrammeDirectionsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProgrammeDirection,
  programmeDirection,
  scConfirm
) {
  $scope.programmeDirection = programmeDirection;

  $scope.save = function() {
    return $scope.newProgrammeDirectionForm.$validate().then(function() {
      if ($scope.newProgrammeDirectionForm.$valid) {
        return scConfirm({
          validationAction: 'canCreate',
          resource: 'ProgrammeDirection',
          action: 'save',
          params: {
            id: $stateParams.id
          },
          data: $scope.programmeDirection
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.map.programmes.view.directions.search');
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.programmes.view.directions.search');
  };
}

ProgrammeDirectionsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProgrammeDirection',
  'programmeDirection',
  'scConfirm'
];

ProgrammeDirectionsNewCtrl.$resolve = {
  programmeDirection: [
    'ProgrammeDirection',
    '$stateParams',
    function(ProgrammeDirection, $stateParams) {
      return ProgrammeDirection.newDirection({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProgrammeDirectionsNewCtrl };
