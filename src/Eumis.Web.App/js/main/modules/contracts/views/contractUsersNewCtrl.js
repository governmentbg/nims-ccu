function ContractUsersNewCtrl($scope, $state, $stateParams, ContractUser, contractUserReg) {
  $scope.contractUserReg = contractUserReg;

  $scope.save = function() {
    return $scope.newForm.$validate().then(function() {
      if ($scope.newForm.$valid) {
        return ContractUser.save({ id: $stateParams.id }, $scope.contractUserReg).$promise.then(
          function() {
            return $state.go('root.contracts.view.users.search');
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.view.users.search');
  };
}

ContractUsersNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractUser',
  'contractUserReg'
];

ContractUsersNewCtrl.$resolve = {
  contractUserReg: [
    'ContractUser',
    '$stateParams',
    function(ContractUser, $stateParams) {
      return ContractUser.newRegistration({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractUsersNewCtrl };
