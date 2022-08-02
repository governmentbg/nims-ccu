function ContractDebtInterestsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ContractDebtInterest,
  newContractDebtInterest
) {
  $scope.newContractDebtInterest = newContractDebtInterest;

  $scope.save = function() {
    return $scope.newContractDebtInterestForm.$validate().then(function() {
      if ($scope.newContractDebtInterestForm.$valid) {
        return ContractDebtInterest.save(
          {
            id: $stateParams.id
          },
          $scope.newContractDebtInterest
        ).$promise.then(function(result) {
          return $state.go('root.contractDebts.view.interests.edit', {
            ind: result.contractDebtInterestId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractDebts.view.interests.search');
  };
}

ContractDebtInterestsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractDebtInterest',
  'newContractDebtInterest'
];

ContractDebtInterestsNewCtrl.$resolve = {
  newContractDebtInterest: [
    '$stateParams',
    'ContractDebtInterest',
    function($stateParams, ContractDebtInterest) {
      return ContractDebtInterest.newContractDebtInterest({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractDebtInterestsNewCtrl };
