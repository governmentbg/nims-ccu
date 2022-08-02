function ProcedureLocationNewCtrl($scope, $state, $stateParams, ProcedureLocation, location) {
  $scope.procedureId = $stateParams.id;
  $scope.location = location;

  $scope.save = function() {
    return $scope.newProcedureLocationForm.$validate().then(function() {
      if ($scope.newProcedureLocationForm.$valid) {
        return ProcedureLocation.save(
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
}

ProcedureLocationNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureLocation',
  'location'
];

ProcedureLocationNewCtrl.$resolve = {
  location: [
    'ProcedureLocation',
    '$stateParams',
    function(ProcedureLocation, $stateParams) {
      return ProcedureLocation.newProcedureLocation({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureLocationNewCtrl };
