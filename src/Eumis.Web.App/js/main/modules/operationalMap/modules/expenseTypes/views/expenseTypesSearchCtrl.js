function ExpenseTypesSearchCtrl($scope, expenseTypes) {
  $scope.expenseTypes = expenseTypes;
}

ExpenseTypesSearchCtrl.$inject = ['$scope', 'expenseTypes'];

ExpenseTypesSearchCtrl.$resolve = {
  expenseTypes: [
    'ExpenseType',
    function(ExpenseType) {
      return ExpenseType.query().$promise;
    }
  ]
};

export { ExpenseTypesSearchCtrl };
