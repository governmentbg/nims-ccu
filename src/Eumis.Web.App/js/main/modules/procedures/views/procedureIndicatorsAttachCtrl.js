function ProcedureIndicatorsAttachCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureIndicator,
  procedureIndicator
) {
  $scope.procedureIndicator = procedureIndicator;

  $scope.save = function() {
    return $scope.attachForm.$validate().then(function() {
      if ($scope.attachForm.$valid) {
        return ProcedureIndicator.save(
          { id: $stateParams.id },
          $scope.procedureIndicator
        ).$promise.then(function() {
          return $state.go('root.procedures.view.indicators.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.indicators.search');
  };
}

ProcedureIndicatorsAttachCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureIndicator',
  'procedureIndicator'
];

ProcedureIndicatorsAttachCtrl.$resolve = {
  procedureIndicator: [
    'ProcedureIndicator',
    '$stateParams',
    function(ProcedureIndicator, $stateParams) {
      return ProcedureIndicator.newAttachedIndicator({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureIndicatorsAttachCtrl };
