function ProcedureEvalTableCtrl($scope, scFormParams) {
  $scope.isNew = scFormParams.isNew;
}

ProcedureEvalTableCtrl.$inject = ['$scope', 'scFormParams'];

export { ProcedureEvalTableCtrl };
