function ContractDataCtrl($scope, contract) {
  $scope.contract = contract;
}

ContractDataCtrl.$inject = ['$scope', 'contract'];

ContractDataCtrl.$resolve = {
  contract: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getData({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractDataCtrl };
