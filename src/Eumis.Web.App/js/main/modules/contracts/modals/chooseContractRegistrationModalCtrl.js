function ChooseContractRegistrationModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ContractRegistration,
  contractRegs
) {
  $scope.contractRegs = contractRegs;

  $scope.filters = {
    email: null,
    uin: null,
    firstName: null,
    lastName: null,
    phone: null
  };

  $scope.search = function() {
    return ContractRegistration.query({
      email: $scope.filters.email,
      uin: $scope.filters.uin,
      firstName: $scope.filters.firstName,
      lastName: $scope.filters.lastName,
      phone: $scope.filters.phone,
      contractId: scModalParams.contractId
    }).$promise.then(function(result) {
      $scope.contractRegs = result;
    });
  };

  $scope.choose = function(contractRegistration) {
    return $uibModalInstance.close(contractRegistration);
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseContractRegistrationModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ContractRegistration',
  'contractRegs'
];

ChooseContractRegistrationModalCtrl.$resolve = {
  contractRegs: [
    'scModalParams',
    'ContractRegistration',
    function(scModalParams, ContractRegistration) {
      return ContractRegistration.query({
        contractId: scModalParams.contractId
      }).$promise;
    }
  ]
};

export { ChooseContractRegistrationModalCtrl };
