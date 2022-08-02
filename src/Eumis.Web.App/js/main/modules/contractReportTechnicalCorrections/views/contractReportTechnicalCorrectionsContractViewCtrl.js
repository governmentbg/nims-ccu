function ContractReportTechnicalCorrectionsContractViewCtrl($scope, contract) {
  $scope.contract = contract;
}

ContractReportTechnicalCorrectionsContractViewCtrl.$inject = ['$scope', 'contract'];

ContractReportTechnicalCorrectionsContractViewCtrl.$resolve = {
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

export { ContractReportTechnicalCorrectionsContractViewCtrl };
