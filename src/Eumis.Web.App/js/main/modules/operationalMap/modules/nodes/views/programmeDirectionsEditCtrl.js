function ProgrammeDirectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProgrammeDirection,
  programmeDirection,
  scConfirm
) {
  $scope.programmeDirection = programmeDirection;
  $scope.editMode = null;
  $scope.programmeStatus = $scope.info.status;
  $scope.programmeId = $stateParams.id;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProgrammeDirectionForm.$validate().then(function() {
      if ($scope.editProgrammeDirectionForm.$valid) {
        return scConfirm({
          validationAction: 'canUpdate',
          resource: 'ProgrammeDirection',
          action: 'update',
          params: {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          data: $scope.programmeDirection
        }).then(function(result) {
          if (result.executed) {
            return $state.partialReload();
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.delete = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProgrammeDirection',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.programmeDirection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.programmes.view.directions.search');
      }
    });
  };
}

ProgrammeDirectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProgrammeDirection',
  'programmeDirection',
  'scConfirm',
  'scModal'
];

ProgrammeDirectionsEditCtrl.$resolve = {
  programmeDirection: [
    'ProgrammeDirection',
    '$stateParams',
    function(ProgrammeDirection, $stateParams) {
      return ProgrammeDirection.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProgrammeDirectionsEditCtrl };
