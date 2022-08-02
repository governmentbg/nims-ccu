function FIReimbursedAmountsBasicViewCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  reimbursedAmountData
) {
  $scope.reimbursedAmountData = reimbursedAmountData;

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'fiReimbursedAmounts_basicData_draftConfirm',
      resource: 'FIReimbursedAmount',
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
      confirmMessage: 'fiReimbursedAmounts_basicData_enterConfirm',
      resource: 'FIReimbursedAmount',
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
      confirmMessage: 'fiReimbursedAmounts_basicData_removeConfirm',
      noteLabel: 'fiReimbursedAmounts_basicData_removeNote',
      resource: 'FIReimbursedAmount',
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
      resource: 'FIReimbursedAmount',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.reimbursedAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.fiReimbursedAmounts.search');
      }
    });
  };
}

FIReimbursedAmountsBasicViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'reimbursedAmountData'
];

FIReimbursedAmountsBasicViewCtrl.$resolve = {
  reimbursedAmountData: [
    'FIReimbursedAmount',
    '$stateParams',
    function(FIReimbursedAmount, $stateParams) {
      return FIReimbursedAmount.getBasicData({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { FIReimbursedAmountsBasicViewCtrl };
