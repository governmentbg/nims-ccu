function ContractUsersSearchCtrl(
  $scope,
  $state,
  $stateParams,
  ContractUser,
  contractUsers,
  scConfirm
) {
  $scope.contractId = $stateParams.id;
  $scope.contractUsers = contractUsers;

  $scope.deleteContractUser = function(item) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractUser',
      action: 'remove',
      params: {
        id: item.contractUserId,
        version: item.version
      }
    }).then(function() {
      return $state.reload();
    });
  };
}

ContractUsersSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractUser',
  'contractUsers',
  'scConfirm'
];

ContractUsersSearchCtrl.$resolve = {
  contractUsers: [
    '$stateParams',
    'ContractUser',
    function($stateParams, ContractUser) {
      return ContractUser.query({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractUsersSearchCtrl };
