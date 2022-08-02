function ProcedureIndicatorsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureIndicator,
  procedureIndicator,
  scConfirm
) {
  $scope.procedureIndicator = procedureIndicator;
  $scope.editMode = null;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureIndicator',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureIndicator.version
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
      resource: 'ProcedureIndicator',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureIndicator.version
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
    return $scope.editForm.$validate().then(function() {
      if ($scope.editForm.$valid) {
        return ProcedureIndicator.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.procedureIndicator
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deleteIndicator = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureIndicator',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureIndicator.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.indicators.search');
      }
    });
  };
}

ProcedureIndicatorsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureIndicator',
  'procedureIndicator',
  'scConfirm'
];

ProcedureIndicatorsEditCtrl.$resolve = {
  procedureIndicator: [
    'ProcedureIndicator',
    '$stateParams',
    function(ProcedureIndicator, $stateParams) {
      return ProcedureIndicator.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ProcedureIndicatorsEditCtrl };
