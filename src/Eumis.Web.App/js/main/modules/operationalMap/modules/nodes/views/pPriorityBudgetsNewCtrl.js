function PPriorityBudgetsNewCtrl($scope, $state, $stateParams, ProgrammePriorityBudget, newBudget) {
  $scope.newBudget = newBudget;

  $scope.save = function() {
    return $scope.newPPriorityBudgetForm.$validate().then(function() {
      if ($scope.newPPriorityBudgetForm.$valid) {
        return ProgrammePriorityBudget.save(
          { id: $stateParams.id },
          $scope.newBudget
        ).$promise.then(function() {
          return $state.go('root.map.ppriorities.view.budgets.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.ppriorities.view.budgets.search');
  };
}

PPriorityBudgetsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProgrammePriorityBudget',
  'newBudget'
];

PPriorityBudgetsNewCtrl.$resolve = {
  newBudget: [
    'ProgrammePriorityBudget',
    '$stateParams',
    function(ProgrammePriorityBudget, $stateParams) {
      return ProgrammePriorityBudget.newBudget({ id: $stateParams.id }).$promise;
    }
  ]
};

export { PPriorityBudgetsNewCtrl };
