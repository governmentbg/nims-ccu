function ContractDebtsSearchCtrl($scope, $stateParams, contractDebts) {
  $scope.contractDebts = contractDebts;
}

ContractDebtsSearchCtrl.$inject = ['$scope', '$stateParams', 'contractDebts'];

ContractDebtsSearchCtrl.$resolve = {
  contractDebts: [
    'ContractDebt',
    function(ContractDebt) {
      return ContractDebt.query().$promise;
    }
  ]
};

export { ContractDebtsSearchCtrl };
