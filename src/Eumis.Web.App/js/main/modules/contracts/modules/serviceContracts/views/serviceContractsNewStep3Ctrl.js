function ServiceContractsNewStep3Ctrl(
  $scope,
  $state,
  $stateParams,
  ServiceContract,
  contractRegistration
) {
  $scope.contractRegistration = contractRegistration;

  $scope.save = function() {
    return $scope.serviceContractsNewStep3Form.$validate().then(function() {
      if ($scope.serviceContractsNewStep3Form.$valid) {
        return ServiceContract.save($scope.contractRegistration).$promise.then(function(
          contractResult
        ) {
          return $state.go('root.contracts.editNew', { id: contractResult.contractId });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.search');
  };
}

ServiceContractsNewStep3Ctrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ServiceContract',
  'contractRegistration'
];

ServiceContractsNewStep3Ctrl.$resolve = {
  contractRegistration: [
    '$stateParams',
    'ServiceContract',
    function($stateParams, ServiceContract) {
      if ($stateParams.pId && $stateParams.cId) {
        return ServiceContract.newRegistration({
          procedureId: $stateParams.pId,
          companyId: $stateParams.cId
        }).$promise;
      }
    }
  ]
};

export { ServiceContractsNewStep3Ctrl };
