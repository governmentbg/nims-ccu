function ProgrammeApplicationDocumentCtrl($scope, scFormParams) {
  $scope.isNew = scFormParams.isNew;
}

ProgrammeApplicationDocumentCtrl.$inject = ['$scope', 'scFormParams'];

export { ProgrammeApplicationDocumentCtrl };
