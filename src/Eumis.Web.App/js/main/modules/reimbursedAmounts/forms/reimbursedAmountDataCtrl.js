function ReimbursedAmountDataCtrl($scope, scModal, scFormParams) {
  $scope.hasPayments = scFormParams.hasPayments;
}

ReimbursedAmountDataCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

export { ReimbursedAmountDataCtrl };
