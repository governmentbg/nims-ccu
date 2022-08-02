function ContractContractAccessCodesEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractContractAccessCode,
  contractAccessCode
) {
  $scope.contractAccessCode = contractAccessCode;
  $scope.contractId = $stateParams.id;

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ContractContractAccessCode',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractAccessCode.version
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
      resource: 'ContractContractAccessCode',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractAccessCode.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.back = function() {
    return $state.go('root.contracts.view.accesscodes.search');
  };
}

ContractContractAccessCodesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractContractAccessCode',
  'contractAccessCode'
];

ContractContractAccessCodesEditCtrl.$resolve = {
  contractAccessCode: [
    'ContractContractAccessCode',
    '$stateParams',
    function(ContractContractAccessCode, $stateParams) {
      return ContractContractAccessCode.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractContractAccessCodesEditCtrl };
