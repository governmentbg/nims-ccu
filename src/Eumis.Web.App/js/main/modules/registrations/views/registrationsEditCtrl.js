function RegistrationsEditCtrl($scope, registration) {
  $scope.registration = registration;
}

RegistrationsEditCtrl.$inject = ['$scope', 'registration'];

RegistrationsEditCtrl.$resolve = {
  registration: [
    'Registration',
    '$stateParams',
    function(Registration, $stateParams) {
      return Registration.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { RegistrationsEditCtrl };
