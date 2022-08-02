function ProcedureShareDataCtrl($scope, scFormParams) {
  $scope.isNewProcedure = scFormParams.isNewProcedure;
  $scope.isNew = scFormParams.isNew;
}

ProcedureShareDataCtrl.$inject = ['$scope', 'scFormParams'];

export { ProcedureShareDataCtrl };
