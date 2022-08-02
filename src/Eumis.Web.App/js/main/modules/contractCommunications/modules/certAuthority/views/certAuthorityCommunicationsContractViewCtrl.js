function CertAuthorityCommunicationsContractViewCtrl($scope, contract) {
  $scope.contract = contract;
}

CertAuthorityCommunicationsContractViewCtrl.$inject = ['$scope', 'contract'];

CertAuthorityCommunicationsContractViewCtrl.$resolve = {
  contract: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getData({ id: $stateParams.id }).$promise;
    }
  ]
};

export { CertAuthorityCommunicationsContractViewCtrl };
