function ContractReportFinancialCertCorrectionsCSDsViewCtrl(
  $scope,
  $state,
  $stateParams,
  CSDBudgetItem
) {
  $scope.CSDBudgetItem = CSDBudgetItem;
}

ContractReportFinancialCertCorrectionsCSDsViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'CSDBudgetItem'
];

ContractReportFinancialCertCorrectionsCSDsViewCtrl.$resolve = {
  CSDBudgetItem: [
    '$stateParams',
    'ContractReportFinancialCSDBudgetItem',
    function($stateParams, ContractReportFinancialCSDBudgetItem) {
      return ContractReportFinancialCSDBudgetItem.getReportFinancialCertCorrectionContractReportFinancialCSDBudgetItem(
        {
          id: $stateParams.id,
          ind: $stateParams.ind
        }
      ).$promise;
    }
  ]
};

export { ContractReportFinancialCertCorrectionsCSDsViewCtrl };
