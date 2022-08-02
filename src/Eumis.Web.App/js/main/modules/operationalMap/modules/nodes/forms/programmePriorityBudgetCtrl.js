function ProgrammePriorityBudgetCtrl($scope) {
  $scope.budgetActivityChanged = function(budget) {
    if (!budget.isActive) {
      budget.budgetAmount.euAmount = null;
      budget.budgetAmount.bgAmount = null;
      budget.reservedBudgetAmount.euAmount = null;
      budget.reservedBudgetAmount.bgAmount = null;
      budget.nextThreeAmount.amountWithAdvances = null;
      budget.nextThreeAmount.amountWithoutAdvances = null;
    }
  };
}

ProgrammePriorityBudgetCtrl.$inject = ['$scope'];

export { ProgrammePriorityBudgetCtrl };
