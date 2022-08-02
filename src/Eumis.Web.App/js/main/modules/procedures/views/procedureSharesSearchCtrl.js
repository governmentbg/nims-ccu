function ProcedureSharesSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scMessage,
  scConfirm,
  scModal,
  procedureShares
) {
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.procedureShares = procedureShares;
  $scope.procedureId = $stateParams.id;
}

ProcedureSharesSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scMessage',
  'scConfirm',
  'scModal',
  'procedureShares'
];

ProcedureSharesSearchCtrl.$resolve = {
  procedureShares: [
    '$stateParams',
    'ProcedureShare',
    function($stateParams, ProcedureShare) {
      return ProcedureShare.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureSharesSearchCtrl };
