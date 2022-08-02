function BudgetLevel1ModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ProcedureShareExpenseBudget,
  newBudgetLevel1
) {
  $scope.newBudgetLevel1 = newBudgetLevel1;

  $scope.save = function() {
    return $scope.level1Form.$validate().then(function() {
      if ($scope.level1Form.$valid) {
        return ProcedureShareExpenseBudget.addLevel1(
          {
            id: scModalParams.procedureId
          },
          $scope.newBudgetLevel1
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

BudgetLevel1ModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ProcedureShareExpenseBudget',
  'newBudgetLevel1'
];

BudgetLevel1ModalCtrl.$resolve = {
  newBudgetLevel1: [
    'scModalParams',
    'ProcedureShareExpenseBudget',
    function(scModalParams, ProcedureShareExpenseBudget) {
      return ProcedureShareExpenseBudget.newLevel1({
        id: scModalParams.procedureId,
        programmeId: scModalParams.programmeId
      }).$promise;
    }
  ]
};

export { BudgetLevel1ModalCtrl };
