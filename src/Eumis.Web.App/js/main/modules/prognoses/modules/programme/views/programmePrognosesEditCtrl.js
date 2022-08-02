function ProgrammePrognosesEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProgrammePrognosis,
  programmePrognosis
) {
  $scope.editMode = null;
  $scope.programmePrognosis = programmePrognosis;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProgrammePrognosis.$validate().then(function() {
      if ($scope.editProgrammePrognosis.$valid) {
        return ProgrammePrognosis.update(
          {
            id: $stateParams.id
          },
          $scope.programmePrognosis
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
      resource: 'ProgrammePrognosis',
      action: 'setToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.programmePrognosis.version
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
      resource: 'ProgrammePrognosis',
      action: 'enter',
      params: {
        id: $stateParams.id,
        version: $scope.programmePrognosis.version
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
      resource: 'ProgrammePrognosis',
      action: 'setToRemoved',
      params: {
        id: $stateParams.id,
        version: $scope.programmePrognosis.version
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
      resource: 'ProgrammePrognosis',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.programmePrognosis.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.programmePrognoses.search');
      }
    });
  };
}

ProgrammePrognosesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProgrammePrognosis',
  'programmePrognosis'
];

ProgrammePrognosesEditCtrl.$resolve = {
  programmePrognosis: [
    'ProgrammePrognosis',
    '$stateParams',
    function(ProgrammePrognosis, $stateParams) {
      return ProgrammePrognosis.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProgrammePrognosesEditCtrl };
