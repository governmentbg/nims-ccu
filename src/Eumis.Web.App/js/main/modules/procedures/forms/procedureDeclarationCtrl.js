function ProcedureDeclarationCtrl($scope, scFormParams) {
  $scope.isNew = scFormParams.isNew;
}

ProcedureDeclarationCtrl.$inject = ['$scope', 'scFormParams'];

export { ProcedureDeclarationCtrl };
