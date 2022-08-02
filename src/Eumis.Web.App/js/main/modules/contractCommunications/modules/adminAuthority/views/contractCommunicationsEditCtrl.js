function ContractCommunicationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  AdminAuthorityCommunication,
  contractCommunication
) {
  $scope.contractCommunication = contractCommunication;
  $scope.communicationId = $stateParams.ind;
}

ContractCommunicationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'AdminAuthorityCommunication',
  'contractCommunication'
];

ContractCommunicationsEditCtrl.$resolve = {
  contractCommunication: [
    'AdminAuthorityCommunication',
    '$stateParams',
    function(AdminAuthorityCommunication, $stateParams) {
      return AdminAuthorityCommunication.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractCommunicationsEditCtrl };
