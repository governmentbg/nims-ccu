function PPrioritiesEditCtrl($scope, $state, $stateParams, scConfirm, programmePriority) {
  $scope.programmePriority = programmePriority;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProgrammePriorityDataForm.$validate().then(function() {
      if ($scope.editProgrammePriorityDataForm.$valid) {
        return scConfirm({
          resource: 'ProgrammePriority',
          validationAction: 'canUpdate',
          action: 'update',
          params: {
            id: $stateParams.id
          },
          data: $scope.programmePriority
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

  $scope.changeStatusToDraft = function() {
    return scConfirm({
      confirmMessage: 'programmePriorities_editProgrammePriorityData_draftConfirm',
      resource: 'ProgrammePriority',
      action: 'changeStatusToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.programmePriority.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.changeStatusToEntered = function() {
    return scConfirm({
      confirmMessage: 'programmePriorities_editProgrammePriorityData_enterConfirm',
      resource: 'ProgrammePriority',
      validationAction: 'canEnter',
      action: 'changeStatusToEntered',
      params: {
        id: $stateParams.id,
        version: $scope.programmePriority.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProgrammePriority',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.programmePriority.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.tree');
      }
    });
  };
}

PPrioritiesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'programmePriority'
];

PPrioritiesEditCtrl.$resolve = {
  programmePriority: [
    'ProgrammePriority',
    '$stateParams',
    function(ProgrammePriority, $stateParams) {
      return ProgrammePriority.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { PPrioritiesEditCtrl };
