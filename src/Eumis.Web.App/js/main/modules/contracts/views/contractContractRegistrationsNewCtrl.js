import angular from 'angular';

function ContractContractRegistrationsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ContractContractRegistration,
  contractContractReg,
  uinValidation,
  scModal
) {
  $scope.contractContractReg = contractContractReg;

  $scope.save = function() {
    return $scope.newForm.$validate().then(function() {
      if ($scope.newForm.$valid) {
        if (
          $scope.contractContractReg.contractRegistration.uinType === 'foreignNumber' &&
          !uinValidation.uinValid(
            $scope.contractContractReg.contractRegistration.uin,
            $scope.contractContractReg.contractRegistration.uinType
          )
        ) {
          var modalInstance = scModal.open('invalidForeignNumberWarningModal', {
            contractRegistration: $scope.contractContractReg
          });

          modalInstance.result.then(function() {
            return $state.partialReload();
          }, angular.noop);

          return modalInstance.opened;
        } else {
          return ContractContractRegistration.createNewRegistration(
            { id: $stateParams.id },
            $scope.contractContractReg
          ).$promise.then(function() {
            return $state.go('root.contracts.view.registrations.search');
          });
        }
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.view.registrations.search');
  };
}

ContractContractRegistrationsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractContractRegistration',
  'contractContractReg',
  'uinValidation',
  'scModal'
];

ContractContractRegistrationsNewCtrl.$resolve = {
  contractContractReg: [
    'ContractContractRegistration',
    '$stateParams',
    function(ContractContractRegistration, $stateParams) {
      return ContractContractRegistration.newRegistration({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractContractRegistrationsNewCtrl };
