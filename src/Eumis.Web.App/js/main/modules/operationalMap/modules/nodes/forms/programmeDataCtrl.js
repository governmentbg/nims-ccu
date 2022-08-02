function ProgrammeDataCtrl($scope, scFormParams) {
  $scope.isNew = scFormParams.isNew;
}

ProgrammeDataCtrl.$inject = ['$scope', 'scFormParams'];

export { ProgrammeDataCtrl };
