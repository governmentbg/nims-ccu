function ProgrammeBudgetsSearchCtrl($scope, $stateParams, programmeBudgets) {
  $scope.programmeBudgets = programmeBudgets;
}

ProgrammeBudgetsSearchCtrl.$inject = ['$scope', '$stateParams', 'programmeBudgets'];

ProgrammeBudgetsSearchCtrl.$resolve = {
  programmeBudgets: [
    '$stateParams',
    'ProgrammeBudget',
    function($stateParams, ProgrammeBudget) {
      return ProgrammeBudget.query({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProgrammeBudgetsSearchCtrl };
