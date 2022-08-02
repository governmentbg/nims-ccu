function ProceduresEditCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  l10n,
  $interpolate,
  Procedure,
  procedure,
  procedureLocations
) {
  $scope.procedure = procedure;
  $scope.procedureLocations = procedureLocations;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcedureDataForm.$validate().then(function() {
      if ($scope.editProcedureDataForm.$valid) {
        return Procedure.update({ id: $stateParams.id }, $scope.procedure).$promise.then(
          function() {
            return $state.partialReload();
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.changeStatus = function(procedureStatus) {
    var validationAction = null,
      confirmMsg = $interpolate(l10n.get('procedures_editProcedureData_changeStatusConfirm'))({
        status: l10n.get('procedures_editProcedureData_' + procedureStatus)
      });
    if (procedureStatus === 'entered') {
      validationAction = 'canChangeStatusToEntered';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      resource: 'Procedure',
      validationAction: validationAction,
      action: 'changeStatusTo' + procedureStatus.charAt(0).toUpperCase() + procedureStatus.slice(1),
      params: { id: $scope.procedure.procedureId, version: $scope.procedure.version }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProceduresEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'l10n',
  '$interpolate',
  'Procedure',
  'procedure',
  'procedureLocations'
];

ProceduresEditCtrl.$resolve = {
  procedure: [
    'Procedure',
    '$stateParams',
    function(Procedure, $stateParams) {
      return Procedure.get({ id: $stateParams.id }).$promise;
    }
  ],
  procedureLocations: [
    '$stateParams',
    'ProcedureLocation',
    function($stateParams, ProcedureLocation) {
      return ProcedureLocation.getLocations({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProceduresEditCtrl };
