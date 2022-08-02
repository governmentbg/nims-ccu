function CertAuthorityCommunicationsViewCtrl($scope, contractInfo) {
  $scope.contractInfo = contractInfo;
}

CertAuthorityCommunicationsViewCtrl.$inject = ['$scope', 'contractInfo'];

CertAuthorityCommunicationsViewCtrl.$resolve = {
  contractInfo: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { CertAuthorityCommunicationsViewCtrl };
