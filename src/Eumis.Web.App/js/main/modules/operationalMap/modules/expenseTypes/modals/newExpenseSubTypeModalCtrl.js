function NewExpenseSubTypeModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ExpenseSubType,
  newExpenseSubType
) {
  $scope.newExpenseSubType = newExpenseSubType;

  $scope.save = function() {
    return $scope.newSubTypeForm.$validate().then(function() {
      if ($scope.newSubTypeForm.$valid) {
        return ExpenseSubType.save(
          {
            id: scModalParams.expenseTypeId
          },
          $scope.newExpenseSubType
        ).$promise.then(function() {
          return $uibModalInstance.close();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

NewExpenseSubTypeModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ExpenseSubType',
  'newExpenseSubType'
];

NewExpenseSubTypeModalCtrl.$resolve = {
  newExpenseSubType: [
    'scModalParams',
    'ExpenseSubType',
    function(scModalParams, ExpenseSubType) {
      return ExpenseSubType.newExpenseSubType({
        id: scModalParams.expenseTypeId
      }).$promise;
    }
  ]
};

export { NewExpenseSubTypeModalCtrl };
