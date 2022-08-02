function EvalSessionUserCtrl($scope, scFormParams) {
  $scope.isNew = scFormParams.isNew;
}

EvalSessionUserCtrl.$inject = ['$scope', 'scFormParams'];

export { EvalSessionUserCtrl };
