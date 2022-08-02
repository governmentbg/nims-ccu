function ContractSpendingPlansNewCtrl(
  $scope,
  $state,
  $stateParams,
  ContractSpendingPlan,
  newContractSpendingPlan
) {
  $scope.newContractSpendingPlan = newContractSpendingPlan;

  $scope.save = function() {
    return $scope.newContractSpendingPlanForm.$validate().then(function() {
      if ($scope.newContractSpendingPlanForm.$valid) {
        return ContractSpendingPlan.save(
          {
            id: $stateParams.id
          },
          $scope.newContractSpendingPlan
        ).$promise.then(function(result) {
          return $state.go(
            'root.contracts.view.amendments.spendingPlans.edit',
            {
              id: result.contractId,
              spid: result.contractSpendingPlanId
            },
            {
              reload: true
            }
          );
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.view.amendments.search');
  };
}

ContractSpendingPlansNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractSpendingPlan',
  'newContractSpendingPlan'
];

ContractSpendingPlansNewCtrl.$resolve = {
  newContractSpendingPlan: [
    'ContractSpendingPlan',
    '$stateParams',
    function(ContractSpendingPlan, $stateParams) {
      return ContractSpendingPlan.newContractSpendingPlan({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractSpendingPlansNewCtrl };
