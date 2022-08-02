function ContractReportFinancialCorrectionsContractViewCtrl($scope, contract) {
  $scope.contract = contract;
}

ContractReportFinancialCorrectionsContractViewCtrl.$inject = ['$scope', 'contract'];

ContractReportFinancialCorrectionsContractViewCtrl.$resolve = {
  contract: [
    'Contract',
    'contractInfo',
    function(Contract, contractInfo) {
      return Contract.getData({
        id: contractInfo.contractId
      }).$promise;
    }
  ]
};

export { ContractReportFinancialCorrectionsContractViewCtrl };
