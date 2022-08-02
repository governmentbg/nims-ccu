function ProgrammePriorityDataCtrl($scope, scFormParams) {
  $scope.isNew = scFormParams.isNew;
}

ProgrammePriorityDataCtrl.$inject = ['$scope', 'scFormParams'];

export { ProgrammePriorityDataCtrl };
