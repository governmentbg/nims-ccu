function ProcurementDifferentiatedPositionsSearchCtrl($scope, $stateParams, procurementPositions) {
  $scope.status = $scope.info.status;

  $scope.procurementPositions = procurementPositions;
}

ProcurementDifferentiatedPositionsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'procurementPositions'
];

ProcurementDifferentiatedPositionsSearchCtrl.$resolve = {
  procurementPositions: [
    '$stateParams',
    'ProcurementDifferentiatedPosition',
    function($stateParams, ProcurementDifferentiatedPosition) {
      return ProcurementDifferentiatedPosition.query($stateParams).$promise;
    }
  ]
};

export { ProcurementDifferentiatedPositionsSearchCtrl };
