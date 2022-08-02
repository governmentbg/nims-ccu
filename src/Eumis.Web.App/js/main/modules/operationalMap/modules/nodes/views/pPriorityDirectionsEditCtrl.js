function ProgrammePriorityDirectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProgrammePriorityDirection,
  programmePriorityDirection,
  scConfirm
) {
  $scope.programmePriorityDirection = programmePriorityDirection;
  $scope.editMode = null;
  $scope.programmeStatus = $scope.info.status;
  $scope.programmePriorityId = $stateParams.id;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editPPriorityDirectionForm.$validate().then(function() {
      if ($scope.editPPriorityDirectionForm.$valid) {
        return scConfirm({
          validationAction: 'canUpdate',
          resource: 'ProgrammePriorityDirection',
          action: 'update',
          params: {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          data: $scope.programmePriorityDirection
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
      resource: 'ProgrammePriorityDirection',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.programmePriorityDirection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.ppriorities.view.directions.search');
      }
    });
  };
}

ProgrammePriorityDirectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProgrammePriorityDirection',
  'programmePriorityDirection',
  'scConfirm',
  'scModal'
];

ProgrammePriorityDirectionsEditCtrl.$resolve = {
  programmePriorityDirection: [
    'ProgrammePriorityDirection',
    '$stateParams',
    function(ProgrammePriorityDirection, $stateParams) {
      return ProgrammePriorityDirection.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProgrammePriorityDirectionsEditCtrl };
