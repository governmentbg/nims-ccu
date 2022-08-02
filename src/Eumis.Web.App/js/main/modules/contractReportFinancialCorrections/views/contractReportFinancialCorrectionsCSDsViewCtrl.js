function ContractReportFinancialCorrectionsCSDsViewCtrl(
  $scope,
  $state,
  $stateParams,
  CSDBudgetItem
) {
  $scope.CSDBudgetItem = CSDBudgetItem;
}

ContractReportFinancialCorrectionsCSDsViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'CSDBudgetItem'
];

ContractReportFinancialCorrectionsCSDsViewCtrl.$resolve = {
  CSDBudgetItem: [
    '$stateParams',
    'ContractReportFinancialCSDBudgetItem',
    function($stateParams, ContractReportFinancialCSDBudgetItem) {
      return ContractReportFinancialCSDBudgetItem.getReportFinancialCorrectionContractReportFinancialCSDBudgetItem(
        {
          id: $stateParams.id,
          ind: $stateParams.ind
        }
      ).$promise;
    }
  ]
};

export { ContractReportFinancialCorrectionsCSDsViewCtrl };
