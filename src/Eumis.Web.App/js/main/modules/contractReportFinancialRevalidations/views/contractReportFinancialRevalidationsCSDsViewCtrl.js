function ContractReportFinancialRevalidationsCSDsViewCtrl(
  $scope,
  $state,
  $stateParams,
  CSDBudgetItem
) {
  $scope.CSDBudgetItem = CSDBudgetItem;
}

ContractReportFinancialRevalidationsCSDsViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'CSDBudgetItem'
];

ContractReportFinancialRevalidationsCSDsViewCtrl.$resolve = {
  CSDBudgetItem: [
    '$stateParams',
    'ContractReportFinancialCSDBudgetItem',
    function($stateParams, ContractReportFinancialCSDBudgetItem) {
      return ContractReportFinancialCSDBudgetItem.getReportFinancialRevalidationContractReportFinancialCSDBudgetItem(
        {
          id: $stateParams.id,
          ind: $stateParams.ind
        }
      ).$promise;
    }
  ]
};

export { ContractReportFinancialRevalidationsCSDsViewCtrl };
