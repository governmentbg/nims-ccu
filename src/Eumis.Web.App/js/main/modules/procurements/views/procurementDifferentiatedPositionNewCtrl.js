function ProcurementDifferentiatedPositionsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcurementDifferentiatedPosition,
  differentiatedPosition
) {
  $scope.differentiatedPosition = differentiatedPosition;

  $scope.save = function() {
    return $scope.newProcurementPositionForm.$validate().then(function() {
      if ($scope.newProcurementPositionForm.$valid) {
        return ProcurementDifferentiatedPosition.save(
          {
            id: $stateParams.id
          },
          $scope.differentiatedPosition
        ).$promise.then(function() {
          return $state.go('root.procurements.view.differentiatedPosition.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procurements.view.differentiatedPosition.search');
  };
}

ProcurementDifferentiatedPositionsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcurementDifferentiatedPosition',
  'differentiatedPosition'
];

ProcurementDifferentiatedPositionsNewCtrl.$resolve = {
  differentiatedPosition: [
    '$stateParams',
    'ProcurementDifferentiatedPosition',
    function($stateParams, ProcurementDifferentiatedPosition) {
      return ProcurementDifferentiatedPosition.newPosition({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcurementDifferentiatedPositionsNewCtrl };
