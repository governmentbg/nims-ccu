function ContractReportChecksContractViewCtrl($scope, contract) {
  $scope.contract = contract;
}

ContractReportChecksContractViewCtrl.$inject = ['$scope', 'contract'];

ContractReportChecksContractViewCtrl.$resolve = {
  contract: [
    'Contract',
    'contractInfo',
    function(Contract, contractInfo) {
      return Contract.getData({ id: contractInfo.contractId }).$promise;
    }
  ]
};

export { ContractReportChecksContractViewCtrl };
