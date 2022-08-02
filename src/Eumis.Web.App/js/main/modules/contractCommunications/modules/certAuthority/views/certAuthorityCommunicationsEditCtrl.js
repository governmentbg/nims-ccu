function CertAuthorityCommunicationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractCommunication
) {
  $scope.contractCommunication = contractCommunication;
  $scope.communicationId = $stateParams.ind;

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'CertAuthorityCommunication',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.certAuthorityCommunications.view.communication.search');
      }
    });
  };

  $scope.communicationUpdated = function() {
    return $state.partialReload();
  };
}

CertAuthorityCommunicationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractCommunication'
];

CertAuthorityCommunicationsEditCtrl.$resolve = {
  contractCommunication: [
    'CertAuthorityCommunication',
    '$stateParams',
    function(CertAuthorityCommunication, $stateParams) {
      return CertAuthorityCommunication.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { CertAuthorityCommunicationsEditCtrl };
