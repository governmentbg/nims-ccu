function EvalSessionSheetCtrl($scope, scFormParams) {
  $scope.canViewDistribution = scFormParams.canViewDistribution;
  $scope.viewProjectState = scFormParams.viewProjectState;
}

EvalSessionSheetCtrl.$inject = ['$scope', 'scFormParams'];

export { EvalSessionSheetCtrl };
