function ContractContractRegistrationCtrl(
  $q,
  $scope,
  $stateParams,
  scFormParams,
  eumisConstants,
  uinValidation,
  ContractRegistration
) {
  $scope.isReadonly = scFormParams.isReadonly;
  $scope.hideEmail = scFormParams.hideEmail;
  $scope.emailRegex = eumisConstants.emailRegex;
  $scope.contractId = $stateParams.id;

  $scope.isUnique = function(email) {
    if ($scope.isReadonly || !email) {
      return $q.resolve();
    }
    return ContractRegistration.isUnique({
      email: email
    }).$promise.then(function(result) {
      return result ? $q.resolve() : $q.reject();
    });
  };

  $scope.uinValid = function(uin) {
    if (
      $scope.model.contractRegistration &&
      $scope.model.contractRegistration.uinType &&
      $scope.model.contractRegistration.uinType !== 'foreignNumber'
    ) {
      return uinValidation.uinValid(uin, $scope.model.contractRegistration.uinType);
    } else {
      return true;
    }
  };
}

ContractContractRegistrationCtrl.$inject = [
  '$q',
  '$scope',
  '$stateParams',
  'scFormParams',
  'eumisConstants',
  'uinValidation',
  'ContractRegistration'
];

export { ContractContractRegistrationCtrl };
