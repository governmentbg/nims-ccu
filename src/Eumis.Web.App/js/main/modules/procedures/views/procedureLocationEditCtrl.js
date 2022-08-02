function ProcedureLocationEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProcedureLocation,
  location
) {
  $scope.procedureId = $stateParams.id;
  $scope.location = location;

  $scope.save = function() {
    return $scope.editProcedureLocationForm.$validate().then(function() {
      if ($scope.editProcedureLocationForm.$valid) {
        return ProcedureLocation.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.location
        ).$promise.then(function() {
          return $state.go('root.procedures.view.edit');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.edit');
  };

  $scope.deleteLocation = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureLocation',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.location.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.edit');
      }
    });
  };
}

ProcedureLocationEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProcedureLocation',
  'location'
];

ProcedureLocationEditCtrl.$resolve = {
  location: [
    'ProcedureLocation',
    '$stateParams',
    function(ProcedureLocation, $stateParams) {
      return ProcedureLocation.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProcedureLocationEditCtrl };
