function ContractUsersEditCtrl(
  $scope,
  $state,
  $stateParams,
  ContractUser,
  contractUser,
  scConfirm
) {
  $scope.contractUser = contractUser;

  $scope.cancel = function() {
    return $state.go('root.contracts.view.users.search');
  };

  $scope.deleteUser = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractUser',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractUser.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contracts.view.users.search');
      }
    });
  };
}

ContractUsersEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractUser',
  'contractUser',
  'scConfirm'
];

ContractUsersEditCtrl.$resolve = {
  contractUser: [
    '$stateParams',
    'ContractUser',
    function($stateParams, ContractUser) {
      return ContractUser.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ContractUsersEditCtrl };
