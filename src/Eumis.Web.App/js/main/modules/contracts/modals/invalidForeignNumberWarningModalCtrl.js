function InvalidForeignNumberWarningModalCtrl(
  $scope,
  $state,
  $stateParams,
  $uibModalInstance,
  scModalParams,
  ContractContractRegistration
) {
  $scope.ok = function() {
    return ContractContractRegistration.createNewRegistration(
      { id: $stateParams.id },
      scModalParams.contractRegistration
    ).$promise.then(function() {
      $state.go('root.contracts.view.registrations.search');
      return $uibModalInstance.dismiss();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

InvalidForeignNumberWarningModalCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$uibModalInstance',
  'scModalParams',
  'ContractContractRegistration'
];

export { InvalidForeignNumberWarningModalCtrl };
