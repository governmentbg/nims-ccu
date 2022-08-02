function ContractDebtInterestsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractDebtInterest,
  contractDebtInterest
) {
  $scope.editMode = null;
  $scope.contractDebtInterest = contractDebtInterest;
  $scope.contractDebtIsDeleted = $scope.contractDebtInfo.status === 'removed';

  $scope.save = function() {
    return $scope.editContractDebtInterestForm.$validate().then(function() {
      if ($scope.editContractDebtInterestForm.$valid) {
        return ContractDebtInterest.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.contractDebtInterest
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractDebtInterest',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractDebtInterest.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractDebts.view.interests.search');
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

ContractDebtInterestsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractDebtInterest',
  'contractDebtInterest'
];

ContractDebtInterestsEditCtrl.$resolve = {
  contractDebtInterest: [
    'ContractDebtInterest',
    '$stateParams',
    function(ContractDebtInterest, $stateParams) {
      return ContractDebtInterest.get($stateParams).$promise;
    }
  ]
};

export { ContractDebtInterestsEditCtrl };
