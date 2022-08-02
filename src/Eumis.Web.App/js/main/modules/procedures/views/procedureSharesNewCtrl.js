function ProcedureSharesNewCtrl(
  $scope,
  $state,
  $stateParams,
  Procedure,
  ProcedureShare,
  newProcedureShare
) {
  $scope.newProcedureShare = newProcedureShare;

  $scope.save = function() {
    return $scope.newProcedureShareForm.$validate().then(function() {
      if ($scope.newProcedureShareForm.$valid) {
        return ProcedureShare.save({ id: $stateParams.id }, $scope.newProcedureShare).$promise.then(
          function() {
            return $state.go('root.procedures.view.procedureShares.search');
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.procedureShares.search', {
      id: $stateParams.id
    });
  };
}

ProcedureSharesNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'Procedure',
  'ProcedureShare',
  'newProcedureShare'
];

ProcedureSharesNewCtrl.$resolve = {
  newProcedureShare: [
    '$stateParams',
    'ProcedureShare',
    function($stateParams, ProcedureShare) {
      return ProcedureShare.newProcedureShare({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureSharesNewCtrl };
