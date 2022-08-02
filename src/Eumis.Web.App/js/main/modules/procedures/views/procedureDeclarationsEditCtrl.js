function ProcedureDeclarationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  procedureDeclaration,
  ProcedureDeclaration,
  scConfirm
) {
  $scope.procedureDeclaration = procedureDeclaration;

  $scope.procedureId = $stateParams.id;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.status = procedureDeclaration.status;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcedureDeclarationForm.$validate().then(function() {
      if ($scope.editProcedureDeclarationForm.$valid) {
        return ProcedureDeclaration.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.procedureDeclaration
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureDeclaration',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureDeclaration.version
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
      resource: 'ProcedureDeclaration',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureDeclaration.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.delete = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureDeclaration',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureDeclaration.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.allDocs.search');
      }
    });
  };
}

ProcedureDeclarationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'procedureDeclaration',
  'ProcedureDeclaration',
  'scConfirm'
];

ProcedureDeclarationsEditCtrl.$resolve = {
  procedureDeclaration: [
    'ProcedureDeclaration',
    '$stateParams',
    function(ProcedureDeclaration, $stateParams) {
      return ProcedureDeclaration.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ProcedureDeclarationsEditCtrl };
