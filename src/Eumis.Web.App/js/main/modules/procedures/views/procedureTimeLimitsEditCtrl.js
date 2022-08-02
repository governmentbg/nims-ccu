function ProcedureTimeLimitsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProcedureTimeLimit,
  procedureTimeLimit
) {
  $scope.editable = $stateParams.editable === 'true';
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.procedureTimeLimit = procedureTimeLimit;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcedureTimeLimitForm.$validate().then(function() {
      if ($scope.editProcedureTimeLimitForm.$valid) {
        return ProcedureTimeLimit.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.procedureTimeLimit
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureTimeLimit',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: procedureTimeLimit.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.procedureTimeLimits.search');
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.ok = function() {
    return $state.go('root.procedures.view.procedureTimeLimits.search');
  };
}

ProcedureTimeLimitsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProcedureTimeLimit',
  'procedureTimeLimit'
];

ProcedureTimeLimitsEditCtrl.$resolve = {
  procedureTimeLimit: [
    '$stateParams',
    'ProcedureTimeLimit',
    function($stateParams, ProcedureTimeLimit) {
      return ProcedureTimeLimit.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProcedureTimeLimitsEditCtrl };
