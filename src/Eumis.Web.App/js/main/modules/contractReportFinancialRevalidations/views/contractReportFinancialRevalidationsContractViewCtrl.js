function ContractReportFinancialRevalidationsContractViewCtrl($scope, contract) {
  $scope.contract = contract;
}

ContractReportFinancialRevalidationsContractViewCtrl.$inject = ['$scope', 'contract'];

ContractReportFinancialRevalidationsContractViewCtrl.$resolve = {
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

export { ContractReportFinancialRevalidationsContractViewCtrl };
