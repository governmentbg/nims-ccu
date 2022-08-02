function ContractReportPaymentChecksCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractReportPaymentChecks
) {
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportSource = $scope.contractReportInfo.source;
  $scope.contractReportPaymentChecks = contractReportPaymentChecks;

  $scope.newCheck = function() {
    return scConfirm({
      resource: 'ContractReportPaymentCheck',
      validationAction: 'canCreate',
      action: 'save',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportChecks.view.paymentChecks.edit', {
          id: $stateParams.id,
          ind: result.result.contractReportPaymentCheckId
        });
      }
    });
  };
}

ContractReportPaymentChecksCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractReportPaymentChecks'
];

ContractReportPaymentChecksCtrl.$resolve = {
  contractReportPaymentChecks: [
    '$stateParams',
    'ContractReportPaymentCheck',
    function($stateParams, ContractReportPaymentCheck) {
      return ContractReportPaymentCheck.query($stateParams).$promise;
    }
  ]
};

export { ContractReportPaymentChecksCtrl };
