function ExpenseTypesNewCtrl($scope, $state, ExpenseType, newExpenseType) {
  $scope.newExpenseType = newExpenseType;

  $scope.save = function() {
    return $scope.newExpenseTypeForm.$validate().then(function() {
      if ($scope.newExpenseTypeForm.$valid) {
        return ExpenseType.save($scope.newExpenseType).$promise.then(function() {
          return $state.go('root.map.expenseTypes.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.expenseTypes.search');
  };
}

ExpenseTypesNewCtrl.$inject = ['$scope', '$state', 'ExpenseType', 'newExpenseType'];

ExpenseTypesNewCtrl.$resolve = {
  newExpenseType: [
    'ExpenseType',
    function(ExpenseType) {
      return ExpenseType.newExpenseType().$promise;
    }
  ]
};

export { ExpenseTypesNewCtrl };
