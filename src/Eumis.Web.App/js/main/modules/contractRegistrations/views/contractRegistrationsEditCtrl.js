function ContractRegistrationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ContractRegistration,
  contractRegistration
) {
  $scope.contractRegistration = contractRegistration;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editContractRegistrationForm.$validate().then(function() {
      if ($scope.editContractRegistrationForm.$valid) {
        return ContractRegistration.update(
          {
            id: $stateParams.id
          },
          $scope.contractRegistration
        ).$promise.then(function() {
          return $state.reload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.reload();
  };
}

ContractRegistrationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractRegistration',
  'contractRegistration'
];

ContractRegistrationsEditCtrl.$resolve = {
  contractRegistration: [
    'ContractRegistration',
    '$stateParams',
    function(ContractRegistration, $stateParams) {
      return ContractRegistration.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractRegistrationsEditCtrl };
