function EuReimbursedAmountsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  EuReimbursedAmount,
  euReimbursedAmount
) {
  $scope.editMode = null;
  $scope.euReimbursedAmount = euReimbursedAmount;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editAmountData.$validate().then(function() {
      if ($scope.editAmountData.$valid) {
        return EuReimbursedAmount.update(
          {
            id: $stateParams.id
          },
          $scope.euReimbursedAmount
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'euReimbursedAmounts_editAmount_draftConfirm',
      resource: 'EuReimbursedAmount',
      action: 'setToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.euReimbursedAmount.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.enter = function() {
    return scConfirm({
      confirmMessage: 'euReimbursedAmounts_editAmount_enterConfirm',
      resource: 'EuReimbursedAmount',
      validationAction: 'canEnter',
      action: 'enter',
      params: {
        id: $stateParams.id,
        version: $scope.euReimbursedAmount.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.remove = function() {
    return scConfirm({
      confirmMessage: 'euReimbursedAmounts_editAmount_removeConfirm',
      noteLabel: 'euReimbursedAmounts_editAmount_removeNote',
      resource: 'EuReimbursedAmount',
      action: 'setToRemoved',
      params: {
        id: $stateParams.id,
        version: $scope.euReimbursedAmount.version
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
      resource: 'EuReimbursedAmount',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.euReimbursedAmount.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.euReimbursedAmounts.search');
      }
    });
  };
}

EuReimbursedAmountsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'EuReimbursedAmount',
  'euReimbursedAmount'
];

EuReimbursedAmountsEditCtrl.$resolve = {
  euReimbursedAmount: [
    'EuReimbursedAmount',
    '$stateParams',
    function(EuReimbursedAmount, $stateParams) {
      return EuReimbursedAmount.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { EuReimbursedAmountsEditCtrl };
