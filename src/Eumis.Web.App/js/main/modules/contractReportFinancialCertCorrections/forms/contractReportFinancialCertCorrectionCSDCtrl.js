import angular from 'angular';

function ContractReportFinancialCertCorrectionCSDCtrl($scope, scModal, moneyOperation) {
  $scope.calculate = function(type) {
    var modalInstance = scModal.open('bfpCalculatorModal', {
      contractId: $scope.model.contractReportFinancialCSDBudgetItem.contractId,
      contractBudgetLevel3AmountId:
        $scope.model.contractReportFinancialCSDBudgetItem.contractBudgetLevel3AmountId
    });

    modalInstance.result.then(function(result) {
      if (type === 'certified') {
        $scope.model.certifiedEuAmount = result.euAmount;
        $scope.model.certifiedBgAmount = result.bgAmount;
      }
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.$watch(
    '[model.certifiedEuAmount, model.certifiedBgAmount]',
    function() {
      $scope.model.certifiedBfpTotalAmount = moneyOperation.addAmounts(
        $scope.model.certifiedEuAmount,
        $scope.model.certifiedBgAmount
      );
    },
    true
  );
  $scope.$watch(
    '[model.certifiedBfpTotalAmount, model.certifiedSelfAmount]',
    function() {
      $scope.model.certifiedTotalAmount = moneyOperation.addAmounts(
        $scope.model.certifiedBfpTotalAmount,
        $scope.model.certifiedSelfAmount
      );
    },
    true
  );
}

ContractReportFinancialCertCorrectionCSDCtrl.$inject = ['$scope', 'scModal', 'moneyOperation'];

export { ContractReportFinancialCertCorrectionCSDCtrl };
