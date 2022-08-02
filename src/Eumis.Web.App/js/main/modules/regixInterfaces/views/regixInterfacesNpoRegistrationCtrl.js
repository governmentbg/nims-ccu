function RegixInterfaceNpoRegistrationCtrl($scope, $state, Regix) {
  $scope.check = function() {
    return $scope.regixInterfacesNpoRegistrationForm.$validate().then(function() {
      if ($scope.regixInterfacesNpoRegistrationForm.$valid) {
        return Regix.npoRegistration({}, $scope.request).$promise.then(function(data) {
          $scope.hasResponse = true;
          $scope.model = data;
          return $state.partialReload();
        });
      }
    });
  };
}

RegixInterfaceNpoRegistrationCtrl.$inject = ['$scope', '$state', 'Regix'];

export { RegixInterfaceNpoRegistrationCtrl };
