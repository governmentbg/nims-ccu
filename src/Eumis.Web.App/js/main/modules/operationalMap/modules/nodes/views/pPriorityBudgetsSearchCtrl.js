function PPriorityBudgetsSearchCtrl($scope, $stateParams, pPriorityBudgets) {
  $scope.programmePriorityId = $stateParams.id;
  $scope.programmePriorityStatus = $scope.info.status;
  $scope.pPriorityBudgets = pPriorityBudgets;
}

PPriorityBudgetsSearchCtrl.$inject = ['$scope', '$stateParams', 'pPriorityBudgets'];

PPriorityBudgetsSearchCtrl.$resolve = {
  pPriorityBudgets: [
    'ProgrammePriorityBudget',
    '$stateParams',
    function(ProgrammePriorityBudget, $stateParams) {
      return ProgrammePriorityBudget.query({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { PPriorityBudgetsSearchCtrl };
