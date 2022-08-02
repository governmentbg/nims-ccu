function RegistrationCtrl($scope, eumisConstants) {
  $scope.emailRegex = eumisConstants.emailRegex;
}

RegistrationCtrl.$inject = ['$scope', 'eumisConstants'];

export { RegistrationCtrl };
