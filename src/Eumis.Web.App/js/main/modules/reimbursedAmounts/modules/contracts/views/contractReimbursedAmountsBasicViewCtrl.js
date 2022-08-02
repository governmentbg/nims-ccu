import angular from 'angular';

function ContractReimbursedAmountsBasicViewCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  ContractReimbursedAmount,
  reimbursedAmountData
) {
  $scope.reimbursedAmountData = reimbursedAmountData;

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'contractReimbursedAmounts_basicData_draftConfirm',
      resource: 'ContractReimbursedAmount',
      action: 'setToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.reimbursedAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.enter = function() {
    return scConfirm({
      confirmMessage: 'contractReimbursedAmounts_basicData_enterConfirm',
      resource: 'ContractReimbursedAmount',
      action: 'enterAmount',
      params: {
        id: $stateParams.id,
        version: $scope.reimbursedAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.remove = function() {
    return scConfirm({
      confirmMessage: 'contractReimbursedAmounts_basicData_removeConfirm',
      noteLabel: 'contractReimbursedAmounts_basicData_removeNote',
      resource: 'ContractReimbursedAmount',
      action: 'setToRemoved',
      params: {
        id: $stateParams.id,
        version: $scope.reimbursedAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractReimbursedAmount',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.reimbursedAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReimbursedAmounts.search');
      }
    });
  };

  $scope.attachToDebt = function() {
    var modalInstance = scModal.open('chooseReimbursedAmountsDebtModal', {
      contractId: $scope.reimbursedAmountData.contractId
    });

    modalInstance.result.then(function(result) {
      return ContractReimbursedAmount.attachToContractDebt(
        {
          id: $stateParams.id,
          contractDebtId: result.contractDebtId,
          version: $scope.reimbursedAmountData.version
        },
        {}
      ).$promise.then(function() {
        $state.go('root.debtReimbursedAmounts.view.basicData', { id: $stateParams.id });
      });
    }, angular.noop);

    return modalInstance.opened;
  };
}

ContractReimbursedAmountsBasicViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'ContractReimbursedAmount',
  'reimbursedAmountData'
];

ContractReimbursedAmountsBasicViewCtrl.$resolve = {
  reimbursedAmountData: [
    'ContractReimbursedAmount',
    '$stateParams',
    function(ContractReimbursedAmount, $stateParams) {
      return ContractReimbursedAmount.getBasicData({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReimbursedAmountsBasicViewCtrl };
