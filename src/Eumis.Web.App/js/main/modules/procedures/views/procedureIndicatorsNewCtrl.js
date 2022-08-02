function ProcedureIndicatorsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureIndicator,
  procedureIndicator
) {
  $scope.procedureIndicator = procedureIndicator;

  $scope.save = function() {
    return $scope.newForm.$validate().then(function() {
      if ($scope.newForm.$valid) {
        return ProcedureIndicator.createNewIndicator(
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

ProcedureIndicatorsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureIndicator',
  'procedureIndicator'
];

ProcedureIndicatorsNewCtrl.$resolve = {
  procedureIndicator: [
    'ProcedureIndicator',
    '$stateParams',
    function(ProcedureIndicator, $stateParams) {
      return ProcedureIndicator.newIndicator({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureIndicatorsNewCtrl };
