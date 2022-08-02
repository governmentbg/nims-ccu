function ProcedureDirectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureDirection,
  procedureDirection,
  scConfirm
) {
  $scope.procedureDirection = procedureDirection;
  $scope.editMode = null;
  $scope.procedureId = $stateParams.id;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcedureDirectionForm.$validate().then(function() {
      if ($scope.editProcedureDirectionForm.$valid) {
        return scConfirm({
          resource: 'ProcedureDirection',
          action: 'update',
          params: {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          data: $scope.procedureDirection
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

  $scope.delete = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureDirection',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureDirection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.directions.search');
      }
    });
  };
}

ProcedureDirectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureDirection',
  'procedureDirection',
  'scConfirm',
  'scModal'
];

ProcedureDirectionsEditCtrl.$resolve = {
  procedureDirection: [
    'ProcedureDirection',
    '$stateParams',
    function(ProcedureDirection, $stateParams) {
      return ProcedureDirection.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProcedureDirectionsEditCtrl };
