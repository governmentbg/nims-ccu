function BudgetLevel3ModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ProcedureShareExpenseBudget,
  budgetLevel3
) {
  $scope.editMode = !!scModalParams.procedureBudgetLevel3Id;
  $scope.budgetLevel3 = budgetLevel3;

  $scope.save = function() {
    return $scope.level3Form.$validate().then(function() {
      if ($scope.level3Form.$valid) {
        if ($scope.editMode) {
          return ProcedureShareExpenseBudget.editLevel3(
            {
              id: scModalParams.procedureId,
              ind: scModalParams.procedureBudgetLevel3Id
            },
            $scope.budgetLevel3
          ).$promise.then(function() {
            return $uibModalInstance.close();
          });
        } else {
          return ProcedureShareExpenseBudget.addLevel3(
            {
              id: scModalParams.procedureId
            },
            $scope.budgetLevel3
          ).$promise.then(function() {
            return $uibModalInstance.close();
          });
        }
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

BudgetLevel3ModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ProcedureShareExpenseBudget',
  'budgetLevel3'
];

BudgetLevel3ModalCtrl.$resolve = {
  budgetLevel3: [
    'scModalParams',
    'ProcedureShareExpenseBudget',
    function(scModalParams, ProcedureShareExpenseBudget) {
      if (!!scModalParams.procedureBudgetLevel3Id) {
        return ProcedureShareExpenseBudget.getLevel3({
          id: scModalParams.procedureId,
          ind: scModalParams.procedureBudgetLevel3Id
        }).$promise;
      } else {
        return ProcedureShareExpenseBudget.newLevel3({
          id: scModalParams.procedureId,
          programmeId: scModalParams.programmeId,
          procedureBudgetLevel2Id: scModalParams.procedureBudgetLevel2Id
        }).$promise;
      }
    }
  ]
};

export { BudgetLevel3ModalCtrl };
