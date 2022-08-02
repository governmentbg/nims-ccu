function ContractDebtsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractDebt,
  contractDebt
) {
  $scope.editMode = null;
  $scope.contractDebt = contractDebt;

  $scope.save = function() {
    return $scope.editContractDebtForm.$validate().then(function() {
      if ($scope.editContractDebtForm.$valid) {
        return ContractDebt.update({ id: $stateParams.id }, $scope.contractDebt).$promise.then(
          function() {
            return $state.partialReload();
          }
        );
      }
    });
  };

  $scope.delDebt = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractDebt',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.contractDebt.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractDebts.search');
      }
    });
  };

  $scope.cancelDebt = function() {
    return scConfirm({
      confirmMessage: 'contractDebts_editContractDebt_cancelConfirm',
      noteLabel: 'contractDebts_editContractDebt_cancelMessage',
      resource: 'ContractDebt',
      action: 'cancel',
      params: {
        id: $stateParams.id,
        version: $scope.contractDebt.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };
}

ContractDebtsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractDebt',
  'contractDebt'
];

ContractDebtsEditCtrl.$resolve = {
  contractDebt: [
    'ContractDebt',
    '$stateParams',
    function(ContractDebt, $stateParams) {
      return ContractDebt.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractDebtsEditCtrl };
