function EditExpenseSubTypeModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ExpenseSubType,
  expenseSubType
) {
  $scope.expenseSubType = expenseSubType;

  $scope.save = function() {
    return $scope.editSubTypeForm.$validate().then(function() {
      if ($scope.editSubTypeForm.$valid) {
        return ExpenseSubType.update(
          {
            id: scModalParams.expenseTypeId,
            ind: scModalParams.expenseSubTypeId
          },
          $scope.expenseSubType
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

EditExpenseSubTypeModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ExpenseSubType',
  'expenseSubType'
];

EditExpenseSubTypeModalCtrl.$resolve = {
  expenseSubType: [
    'scModalParams',
    'ExpenseSubType',
    function(scModalParams, ExpenseSubType) {
      return ExpenseSubType.get({
        id: scModalParams.expenseTypeId,
        ind: scModalParams.expenseSubTypeId
      }).$promise;
    }
  ]
};

export { EditExpenseSubTypeModalCtrl };
