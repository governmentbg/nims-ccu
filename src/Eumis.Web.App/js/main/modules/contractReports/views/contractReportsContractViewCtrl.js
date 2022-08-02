function ContractReportsContractViewCtrl($scope, contract) {
  $scope.contract = contract;
}

ContractReportsContractViewCtrl.$inject = ['$scope', 'contract'];

ContractReportsContractViewCtrl.$resolve = {
  contract: [
    'Contract',
    'contractInfo',
    function(Contract, contractInfo) {
      return Contract.getData({ id: contractInfo.contractId }).$promise;
    }
  ]
};

export { ContractReportsContractViewCtrl };
