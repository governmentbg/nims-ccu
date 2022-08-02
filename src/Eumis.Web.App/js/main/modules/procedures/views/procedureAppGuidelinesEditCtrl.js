function ProcedureAppGuidelinesEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureAppGuideline,
  appGuideline,
  scConfirm
) {
  $scope.procedureId = $stateParams.id;
  $scope.appGuideline = appGuideline;
  $scope.editMode = null;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcedureAppGuidelinesForm.$validate().then(function() {
      if ($scope.editProcedureAppGuidelinesForm.$valid) {
        return ProcedureAppGuideline.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.appGuideline
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deleteAppGuideline = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureAppGuideline',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.appGuideline.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.allDocs.search');
      }
    });
  };
}

ProcedureAppGuidelinesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureAppGuideline',
  'appGuideline',
  'scConfirm'
];

ProcedureAppGuidelinesEditCtrl.$resolve = {
  appGuideline: [
    'ProcedureAppGuideline',
    '$stateParams',
    function(ProcedureAppGuideline, $stateParams) {
      return ProcedureAppGuideline.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ProcedureAppGuidelinesEditCtrl };
