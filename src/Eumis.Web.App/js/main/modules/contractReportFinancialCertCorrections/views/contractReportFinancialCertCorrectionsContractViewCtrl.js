function ContractReportFinancialCertCorrectionsContractViewCtrl($scope, contract) {
  $scope.contract = contract;
}

ContractReportFinancialCertCorrectionsContractViewCtrl.$inject = ['$scope', 'contract'];

ContractReportFinancialCertCorrectionsContractViewCtrl.$resolve = {
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

export { ContractReportFinancialCertCorrectionsContractViewCtrl };
