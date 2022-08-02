function ContractAdditionalDocumentsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  actuallyPaidAmounts,
  contractDebts,
  reimbursedAmounts,
  financialCorrections,
  flatFinancialCorrections
) {
  $scope.contractId = $stateParams.id;
  $scope.actuallyPaidAmounts = actuallyPaidAmounts;
  $scope.contractDebts = contractDebts;
  $scope.reimbursedAmounts = reimbursedAmounts;
  $scope.financialCorrections = financialCorrections;
  $scope.flatFinancialCorrections = flatFinancialCorrections;
}

ContractAdditionalDocumentsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'actuallyPaidAmounts',
  'contractDebts',
  'reimbursedAmounts',
  'financialCorrections',
  'flatFinancialCorrections'
];

ContractAdditionalDocumentsSearchCtrl.$resolve = {
  actuallyPaidAmounts: [
    '$stateParams',
    'Contract',
    function($stateParams, Contract) {
      return Contract.getActuallyPaidAmounts({
        id: $stateParams.id
      }).$promise;
    }
  ],
  contractDebts: [
    '$stateParams',
    'Contract',
    function($stateParams, Contract) {
      return Contract.getContractDebts({
        id: $stateParams.id
      }).$promise;
    }
  ],
  reimbursedAmounts: [
    '$stateParams',
    'Contract',
    function($stateParams, Contract) {
      return Contract.getReimbursedAmounts({
        id: $stateParams.id
      }).$promise;
    }
  ],
  financialCorrections: [
    '$stateParams',
    'Contract',
    function($stateParams, Contract) {
      return Contract.getFinancialCorrections({
        id: $stateParams.id
      }).$promise;
    }
  ],
  flatFinancialCorrections: [
    '$stateParams',
    'Contract',
    function($stateParams, Contract) {
      return Contract.getFlatFinancialCorrections({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractAdditionalDocumentsSearchCtrl };
