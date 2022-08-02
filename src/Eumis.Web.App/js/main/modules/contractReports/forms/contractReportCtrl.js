function ContractReportCtrl($scope, scFormParams) {
  $scope.isNew = scFormParams.isNew;
  $scope.isCheck = scFormParams.isCheck;
  $scope.contractId = scFormParams.contractId;
}

ContractReportCtrl.$inject = ['$scope', 'scFormParams'];

export { ContractReportCtrl };
