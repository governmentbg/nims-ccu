function ContractReportRevalidationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ContractReportRevalidation,
  contractReportRevalidation
) {
  $scope.editMode = null;
  $scope.contractReportRevalidation = contractReportRevalidation;
  $scope.status = $scope.contractReportRevalidationInfo.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editContractReportRevalidationData.$validate().then(function() {
      if ($scope.editContractReportRevalidationData.$valid) {
        return ContractReportRevalidation.update(
          {
            id: $stateParams.id
          },
          $scope.contractReportRevalidation
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

ContractReportRevalidationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractReportRevalidation',
  'contractReportRevalidation'
];

ContractReportRevalidationsEditCtrl.$resolve = {
  contractReportRevalidation: [
    'ContractReportRevalidation',
    '$stateParams',
    function(ContractReportRevalidation, $stateParams) {
      return ContractReportRevalidation.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportRevalidationsEditCtrl };
