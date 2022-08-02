function ProcedureEvalTablesEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProcedureEvalTable,
  evalTable
) {
  $scope.evalTable = evalTable;
  $scope.editMode = null;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureEvalTable',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalTable.version
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
      resource: 'ProcedureEvalTable',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalTable.version
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
    return $scope.editEvalTableForm.$validate().then(function() {
      if ($scope.editEvalTableForm.$valid) {
        return ProcedureEvalTable.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.evalTable
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureEvalTable',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalTable.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.allDocs.search');
      }
    });
  };

  $scope.evalTableUpdated = function() {
    return $state.partialReload();
  };
}

ProcedureEvalTablesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProcedureEvalTable',
  'evalTable'
];

ProcedureEvalTablesEditCtrl.$resolve = {
  evalTable: [
    'ProcedureEvalTable',
    '$stateParams',
    function(ProcedureEvalTable, $stateParams) {
      return ProcedureEvalTable.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ProcedureEvalTablesEditCtrl };
