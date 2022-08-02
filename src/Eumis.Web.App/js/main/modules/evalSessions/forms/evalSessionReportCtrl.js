function EvalSessionReportCtrl($scope, scFormParams) {
  $scope.isNew = scFormParams.isNew;
}

EvalSessionReportCtrl.$inject = ['$scope', 'scFormParams'];

export { EvalSessionReportCtrl };
