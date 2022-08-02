function ProgrammeDeclarationCtrl($scope, scFormParams) {
  $scope.isNew = scFormParams.isNew;
}

ProgrammeDeclarationCtrl.$inject = ['$scope', 'scFormParams'];

export { ProgrammeDeclarationCtrl };
