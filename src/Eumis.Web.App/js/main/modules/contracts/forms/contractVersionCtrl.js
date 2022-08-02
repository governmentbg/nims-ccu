function ContractVersionCtrl($scope, scFormParams) {
  $scope.isNew = scFormParams.isNew;
  $scope.editMode = null;

  $scope.$parent.$watch('editMode', function(value) {
    $scope.editMode = value;
  });
}

ContractVersionCtrl.$inject = ['$scope', 'scFormParams'];

export { ContractVersionCtrl };
