function EvalSessionStandpointCtrl($scope, scFormParams) {
  $scope.viewProjectState = scFormParams.viewProjectState;
}

EvalSessionStandpointCtrl.$inject = ['$scope', 'scFormParams'];

export { EvalSessionStandpointCtrl };
