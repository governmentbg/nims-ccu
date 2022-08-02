function BudgetValidationRuleModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ProcedureShareExpenseBudget,
  budgetValidationRule
) {
  $scope.editMode = !!scModalParams.procedureBudgetValidationRuleId;
  $scope.budgetValidationRule = budgetValidationRule;

  $scope.conditionError = null;
  $scope.ruleError = null;

  $scope.save = function() {
    $scope.conditionError = null;
    $scope.ruleError = null;
    return $scope.validationRuleForm.$validate().then(function() {
      var addOrUpdatePromise;
      if ($scope.validationRuleForm.$valid) {
        if ($scope.editMode) {
          addOrUpdatePromise = ProcedureShareExpenseBudget.editValidationRule(
            {
              id: scModalParams.procedureId,
              ind: scModalParams.procedureBudgetValidationRuleId
            },
            $scope.budgetValidationRule
          ).$promise;
        } else {
          addOrUpdatePromise = ProcedureShareExpenseBudget.addValidationRule(
            {
              id: scModalParams.procedureId
            },
            $scope.budgetValidationRule
          ).$promise;
        }

        return addOrUpdatePromise.then(function(result) {
          if (result.conditionError || result.ruleError) {
            $scope.conditionError = result.conditionError;
            $scope.ruleError = result.ruleError;
            return;
          } else {
            return $uibModalInstance.close();
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

BudgetValidationRuleModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ProcedureShareExpenseBudget',
  'budgetValidationRule'
];

BudgetValidationRuleModalCtrl.$resolve = {
  budgetValidationRule: [
    'scModalParams',
    'ProcedureShareExpenseBudget',
    function(scModalParams, ProcedureShareExpenseBudget) {
      if (!!scModalParams.procedureBudgetValidationRuleId) {
        return ProcedureShareExpenseBudget.getValidationRule({
          id: scModalParams.procedureId,
          ind: scModalParams.procedureBudgetValidationRuleId
        }).$promise;
      } else {
        return ProcedureShareExpenseBudget.newValidationRule({
          id: scModalParams.procedureId,
          programmeId: scModalParams.programmeId
        }).$promise;
      }
    }
  ]
};

export { BudgetValidationRuleModalCtrl };
