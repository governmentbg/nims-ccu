function ProcurementDifferentiatedPostionionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProcurementDifferentiatedPosition,
  differentiatedPosition
) {
  $scope.editMode = null;
  $scope.procurementId = $stateParams.id;
  $scope.differentiatedPosition = differentiatedPosition;
  $scope.status = $scope.info.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcurementPositionForm.$validate().then(function() {
      if ($scope.editProcurementPositionForm.$valid) {
        return ProcurementDifferentiatedPosition.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.differentiatedPosition
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

ProcurementDifferentiatedPostionionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProcurementDifferentiatedPosition',
  'differentiatedPosition'
];

ProcurementDifferentiatedPostionionsEditCtrl.$resolve = {
  differentiatedPosition: [
    'ProcurementDifferentiatedPosition',
    '$stateParams',
    function(ProcurementDifferentiatedPosition, $stateParams) {
      return ProcurementDifferentiatedPosition.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProcurementDifferentiatedPostionionsEditCtrl };
