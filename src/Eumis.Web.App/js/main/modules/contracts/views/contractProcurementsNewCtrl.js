function ContractProcurementsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ContractProcurement,
  newContractProcurement
) {
  $scope.newContractProcurement = newContractProcurement;

  $scope.save = function() {
    return $scope.newContractProcurementForm.$validate().then(function() {
      if ($scope.newContractProcurementForm.$valid) {
        return ContractProcurement.save(
          {
            id: $stateParams.id
          },
          $scope.newContractProcurement
        ).$promise.then(function(result) {
          return $state.go(
            'root.contracts.view.amendments.procurements.edit',
            {
              id: result.contractId,
              pid: result.contractProcurementId
            },
            {
              reload: true
            }
          );
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.view.amendments.search');
  };
}

ContractProcurementsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractProcurement',
  'newContractProcurement'
];

ContractProcurementsNewCtrl.$resolve = {
  newContractProcurement: [
    'ContractProcurement',
    '$stateParams',
    function(ContractProcurement, $stateParams) {
      return ContractProcurement.newContractProcurement({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractProcurementsNewCtrl };
