function ChooseActuallyPaidAmountContractReportPaymentModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  contractReportPayments
) {
  $scope.contractReportPayments = contractReportPayments;

  $scope.choose = function(contract) {
    return $uibModalInstance.close(contract);
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

ChooseActuallyPaidAmountContractReportPaymentModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'contracts'
];

ChooseActuallyPaidAmountContractReportPaymentModalCtrl.$resolve = {
  contracts: [
    'ActuallyPaidAmount',
    'scModalParams',
    function(ActuallyPaidAmount, scModalParams) {
      return ActuallyPaidAmount.getContractReportPayments(scModalParams).$promise;
    }
  ]
};

export { ChooseActuallyPaidAmountContractReportPaymentModalCtrl };
