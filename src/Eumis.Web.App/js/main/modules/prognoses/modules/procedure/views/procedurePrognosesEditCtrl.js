function ProcedurePrognosesEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProcedurePrognosis,
  procedurePrognosis
) {
  $scope.editMode = null;
  $scope.procedurePrognosis = procedurePrognosis;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcedurePrognosis.$validate().then(function() {
      if ($scope.editProcedurePrognosis.$valid) {
        return ProcedurePrognosis.update(
          {
            id: $stateParams.id
          },
          $scope.procedurePrognosis
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
      resource: 'ProcedurePrognosis',
      action: 'setToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.procedurePrognosis.version
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
      resource: 'ProcedurePrognosis',
      action: 'enter',
      params: {
        id: $stateParams.id,
        version: $scope.procedurePrognosis.version
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
      resource: 'ProcedurePrognosis',
      action: 'setToRemoved',
      params: {
        id: $stateParams.id,
        version: $scope.procedurePrognosis.version
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
      resource: 'ProcedurePrognosis',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.procedurePrognosis.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedurePrognoses.search');
      }
    });
  };
}

ProcedurePrognosesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProcedurePrognosis',
  'procedurePrognosis'
];

ProcedurePrognosesEditCtrl.$resolve = {
  procedurePrognosis: [
    'ProcedurePrognosis',
    '$stateParams',
    function(ProcedurePrognosis, $stateParams) {
      return ProcedurePrognosis.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedurePrognosesEditCtrl };
