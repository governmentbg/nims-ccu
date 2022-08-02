function FinancialCorrectionVersionCtrl(
  $scope,
  scModal,
  moneyOperation,
  FinancialCorrectionVersion
) {
  $scope.calculate = function() {
    if (!$scope.model.percent) {
      return;
    }

    return FinancialCorrectionVersion.calculate(
      {
        id: $scope.model.financialCorrectionId,
        ind: $scope.model.financialCorrectionVersionId
      },
      $scope.model
    ).$promise.then(function(result) {
      $scope.model.totalAmount = result.totalAmount;
    });
  };
}

FinancialCorrectionVersionCtrl.$inject = [
  '$scope',
  'scModal',
  'moneyOperation',
  'FinancialCorrectionVersion'
];

export { FinancialCorrectionVersionCtrl };
