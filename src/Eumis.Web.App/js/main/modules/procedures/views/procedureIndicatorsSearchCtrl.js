function ProcedureIndicatorsSearchCtrl($scope, $stateParams, indicators, hasIndicatorsForAttach) {
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.indicators = indicators;
  $scope.hasIndicatorsForAttach = hasIndicatorsForAttach;
  $scope.procedureId = $stateParams.id;
}

ProcedureIndicatorsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'indicators',
  'hasIndicatorsForAttach'
];

ProcedureIndicatorsSearchCtrl.$resolve = {
  indicators: [
    '$stateParams',
    'ProcedureIndicator',
    function($stateParams, ProcedureIndicator) {
      return ProcedureIndicator.query({ id: $stateParams.id }).$promise;
    }
  ],
  hasIndicatorsForAttach: [
    '$stateParams',
    'ProcedureIndicator',
    function($stateParams, ProcedureIndicator) {
      return ProcedureIndicator.hasIndicatorsForAttach({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureIndicatorsSearchCtrl };
