function ContractReportCorrectionDataCtrl($scope, scFormParams) {
  $scope.approvedEdit = scFormParams.approvedEdit;
  $scope.certEdit = scFormParams.certEdit;

  $scope.showPaymentData =
    $scope.model.type === 'paymentVerified' || $scope.model.type === 'advanceCovered';

  $scope.correctionTypeChanged = function() {
    $scope.model.irregularityId = null;
    $scope.model.financialCorrectionId = null;
    $scope.model.flatFinancialCorrectionId = null;
  };
}

ContractReportCorrectionDataCtrl.$inject = ['$scope', 'scFormParams', 'scModal', 'moneyOperation'];

export { ContractReportCorrectionDataCtrl };
