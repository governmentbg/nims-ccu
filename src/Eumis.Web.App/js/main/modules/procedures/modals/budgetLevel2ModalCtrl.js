function BudgetLevel2ModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ProcedureShareExpenseBudget,
  budgetLevel2
) {
  $scope.editMode = !!scModalParams.procedureBudgetLevel2Id;
  $scope.budgetLevel2 = budgetLevel2;
  $scope.previewMode = scModalParams.previewMode;

  $scope.save = function() {
    return $scope.level2Form.$validate().then(function() {
      if ($scope.level2Form.$valid) {
        if ($scope.editMode) {
          return ProcedureShareExpenseBudget.editLevel2(
            {
              id: scModalParams.procedureId,
              ind: scModalParams.procedureBudgetLevel2Id
            },
            $scope.budgetLevel2
          ).$promise.then(function() {
            return $uibModalInstance.close();
          });
        } else {
          return ProcedureShareExpenseBudget.addLevel2(
            {
              id: scModalParams.procedureId
            },
            $scope.budgetLevel2
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

BudgetLevel2ModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ProcedureShareExpenseBudget',
  'budgetLevel2'
];

BudgetLevel2ModalCtrl.$resolve = {
  budgetLevel2: [
    'scModalParams',
    'ProcedureShareExpenseBudget',
    function(scModalParams, ProcedureShareExpenseBudget) {
      if (!!scModalParams.procedureBudgetLevel2Id) {
        return ProcedureShareExpenseBudget.getLevel2({
          id: scModalParams.procedureId,
          ind: scModalParams.procedureBudgetLevel2Id
        }).$promise;
      } else {
        return ProcedureShareExpenseBudget.newLevel2({
          id: scModalParams.procedureId,
          programmeId: scModalParams.programmeId,
          procedureBudgetLevel1Id: scModalParams.procedureBudgetLevel1Id
        }).$promise;
      }
    }
  ]
};

export { BudgetLevel2ModalCtrl };
