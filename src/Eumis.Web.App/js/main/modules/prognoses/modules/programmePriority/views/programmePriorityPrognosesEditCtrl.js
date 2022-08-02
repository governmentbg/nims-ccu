function ProgrammePriorityPrognosesEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProgrammePriorityPrognosis,
  programmePriorityPrognosis
) {
  $scope.editMode = null;
  $scope.programmePriorityPrognosis = programmePriorityPrognosis;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProgrammePriorityPrognosis.$validate().then(function() {
      if ($scope.editProgrammePriorityPrognosis.$valid) {
        return ProgrammePriorityPrognosis.update(
          {
            id: $stateParams.id
          },
          $scope.programmePriorityPrognosis
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'prognoses_edit_draftConfirm',
      resource: 'ProgrammePriorityPrognosis',
      action: 'setToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.programmePriorityPrognosis.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.enter = function() {
    return scConfirm({
      confirmMessage: 'prognoses_edit_enterConfirm',
      resource: 'ProgrammePriorityPrognosis',
      action: 'enter',
      params: {
        id: $stateParams.id,
        version: $scope.programmePriorityPrognosis.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.remove = function() {
    return scConfirm({
      confirmMessage: 'prognoses_edit_removeConfirm',
      noteLabel: 'prognoses_edit_removeNote',
      resource: 'ProgrammePriorityPrognosis',
      action: 'setToRemoved',
      params: {
        id: $stateParams.id,
        version: $scope.programmePriorityPrognosis.version
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
      resource: 'ProgrammePriorityPrognosis',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.programmePriorityPrognosis.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.programmePriorityPrognoses.search');
      }
    });
  };
}

ProgrammePriorityPrognosesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProgrammePriorityPrognosis',
  'programmePriorityPrognosis'
];

ProgrammePriorityPrognosesEditCtrl.$resolve = {
  programmePriorityPrognosis: [
    'ProgrammePriorityPrognosis',
    '$stateParams',
    function(ProgrammePriorityPrognosis, $stateParams) {
      return ProgrammePriorityPrognosis.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProgrammePriorityPrognosesEditCtrl };
