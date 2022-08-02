function ContractDebtInterestsSearchCtrl($scope, $state, $stateParams, contractDebtInterests) {
  $scope.contractDebtInterests = contractDebtInterests;
  $scope.contractDebtIsDeleted = $scope.contractDebtInfo.status === 'removed';
  $scope.contractDebtId = $stateParams.id;
}

ContractDebtInterestsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'contractDebtInterests'
];

ContractDebtInterestsSearchCtrl.$resolve = {
  contractDebtInterests: [
    '$stateParams',
    'ContractDebtInterest',
    function($stateParams, ContractDebtInterest) {
      return ContractDebtInterest.query($stateParams).$promise;
    }
  ]
};

export { ContractDebtInterestsSearchCtrl };
