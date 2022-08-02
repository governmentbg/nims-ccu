function AuditAuthorityCommunicationsViewCtrl($scope, contractInfo) {
  $scope.contractInfo = contractInfo;
}

AuditAuthorityCommunicationsViewCtrl.$inject = ['$scope', 'contractInfo'];

AuditAuthorityCommunicationsViewCtrl.$resolve = {
  contractInfo: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { AuditAuthorityCommunicationsViewCtrl };
