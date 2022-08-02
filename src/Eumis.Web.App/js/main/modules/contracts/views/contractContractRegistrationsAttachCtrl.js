import angular from 'angular';

function ContractContractRegistrationsAttachCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  ContractContractRegistration,
  contractContractReg
) {
  $scope.contractContractReg = contractContractReg;
  $scope.contractId = $stateParams.id;
  $scope.contractRegistrationId = null;

  $scope.chooseRegistration = function() {
    var modalInstance = scModal.open('chooseContractRegistrationModal', {
      contractId: $scope.contractId
    });
    modalInstance.result.then(function(result) {
      $scope.contractRegistrationId = result.contractRegistrationId;
    }, angular.noop);
    return modalInstance.opened;
  };

  $scope.save = function() {
    return $scope.attachForm.$validate().then(function() {
      if ($scope.attachForm.$valid) {
        return ContractContractRegistration.save(
          { id: $stateParams.id },
          $scope.contractContractReg
        ).$promise.then(function() {
          return $state.go('root.contracts.view.registrations.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.view.registrations.search');
  };
}

ContractContractRegistrationsAttachCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'ContractContractRegistration',
  'contractContractReg'
];

ContractContractRegistrationsAttachCtrl.$resolve = {
  contractContractReg: [
    'ContractContractRegistration',
    '$stateParams',
    function(ContractContractRegistration, $stateParams) {
      return ContractContractRegistration.newRegistration({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractContractRegistrationsAttachCtrl };
