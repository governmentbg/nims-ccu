function ContractContractRegistrationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractContractRegistration,
  contractContractReg
) {
  $scope.contractContractReg = contractContractReg;
  $scope.contractId = $stateParams.id;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ContractContractRegistration',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractContractReg.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.activate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmActivate',
      resource: 'ContractContractRegistration',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractContractReg.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.back = function() {
    return $state.go('root.contracts.view.registrations.search');
  };

  $scope.save = function() {
    return $scope.editRegistrationForm.$validate().then(function() {
      if ($scope.editRegistrationForm.$valid) {
        return ContractContractRegistration.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind,
            version: $scope.contractContractReg.version
          },
          $scope.contractContractReg
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

ContractContractRegistrationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractContractRegistration',
  'contractContractReg'
];

ContractContractRegistrationsEditCtrl.$resolve = {
  contractContractReg: [
    'ContractContractRegistration',
    '$stateParams',
    function(ContractContractRegistration, $stateParams) {
      return ContractContractRegistration.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractContractRegistrationsEditCtrl };
