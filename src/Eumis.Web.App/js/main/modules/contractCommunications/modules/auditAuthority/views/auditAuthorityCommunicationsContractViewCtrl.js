function AuditAuthorityCommunicationsContractViewCtrl($scope, contract) {
  $scope.contract = contract;
}

AuditAuthorityCommunicationsContractViewCtrl.$inject = ['$scope', 'contract'];

AuditAuthorityCommunicationsContractViewCtrl.$resolve = {
  contract: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getData({ id: $stateParams.id }).$promise;
    }
  ]
};

export { AuditAuthorityCommunicationsContractViewCtrl };
