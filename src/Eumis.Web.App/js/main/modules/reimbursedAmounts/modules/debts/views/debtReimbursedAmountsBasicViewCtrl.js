function DebtReimbursedAmountsBasicViewCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  reimbursedAmountData
) {
  $scope.reimbursedAmountData = reimbursedAmountData;

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'reimbursedAmounts_basicData_draftConfirm',
      resource: 'DebtReimbursedAmount',
      validationAction: 'canSetToDraft',
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
      confirmMessage: 'reimbursedAmounts_basicData_enterConfirm',
      resource: 'DebtReimbursedAmount',
      validationAction: 'canEnterAmount',
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
      confirmMessage: 'reimbursedAmounts_basicData_removeConfirm',
      noteLabel: 'reimbursedAmounts_basicData_removeNote',
      resource: 'DebtReimbursedAmount',
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
      resource: 'DebtReimbursedAmount',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.reimbursedAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.debtReimbursedAmounts.search');
      }
    });
  };
}

DebtReimbursedAmountsBasicViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'reimbursedAmountData'
];

DebtReimbursedAmountsBasicViewCtrl.$resolve = {
  reimbursedAmountData: [
    'DebtReimbursedAmount',
    '$stateParams',
    function(DebtReimbursedAmount, $stateParams) {
      return DebtReimbursedAmount.getBasicData({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { DebtReimbursedAmountsBasicViewCtrl };
