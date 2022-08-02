function ProcedureSpecFieldsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureSpecField,
  procedureSpecField,
  scMessage,
  scConfirm
) {
  $scope.procedureId = $stateParams.id;
  $scope.procedureSpecField = procedureSpecField;
  $scope.editMode = null;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureSpecField',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureSpecField.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.activate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmActivate',
      resource: 'ProcedureSpecField',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureSpecField.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcedureSpecFieldForm.$validate().then(function() {
      if ($scope.editProcedureSpecFieldForm.$valid) {
        return ProcedureSpecField.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.procedureSpecField
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deleteField = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureSpecField',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureSpecField.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.procedureSpecFields.search');
      }
    });
  };
}

ProcedureSpecFieldsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureSpecField',
  'procedureSpecField',
  'scMessage',
  'scConfirm'
];

ProcedureSpecFieldsEditCtrl.$resolve = {
  procedureSpecField: [
    'ProcedureSpecField',
    '$stateParams',
    function(ProcedureSpecField, $stateParams) {
      return ProcedureSpecField.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ProcedureSpecFieldsEditCtrl };
