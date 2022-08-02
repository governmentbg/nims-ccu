function ProcedureEvalTablesNewCtrl($scope, $state, $stateParams, ProcedureEvalTable, evalTable) {
  $scope.evalTable = evalTable;

  $scope.save = function() {
    return $scope.newProcedureEvalTableForm.$validate().then(function() {
      if ($scope.newProcedureEvalTableForm.$valid) {
        return ProcedureEvalTable.save({ id: $stateParams.id }, $scope.evalTable).$promise.then(
          function(result) {
            return $state.go('root.procedures.view.allDocs.evalTables.edit', {
              ind: result.procedureEvalTableId
            });
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.allDocs.search');
  };
}

ProcedureEvalTablesNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureEvalTable',
  'evalTable'
];

ProcedureEvalTablesNewCtrl.$resolve = {
  evalTable: [
    'ProcedureEvalTable',
    '$stateParams',
    function(ProcedureEvalTable, $stateParams) {
      return ProcedureEvalTable.newEvalTable({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureEvalTablesNewCtrl };
