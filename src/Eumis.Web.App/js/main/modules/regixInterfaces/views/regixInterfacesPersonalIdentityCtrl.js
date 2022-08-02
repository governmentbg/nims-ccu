function RegixInterfacesPersonalIdentityCtrl($scope, $state, Regix) {
  $scope.check = function() {
    return $scope.regixInterfacesValidPersonalIdentityForm.$validate().then(function() {
      if ($scope.regixInterfacesValidPersonalIdentityForm.$valid) {
        return Regix.personalIdentity({}, $scope.request).$promise.then(function(data) {
          $scope.hasResponse = true;
          $scope.model = data;
          return $state.partialReload();
        });
      }
    });
  };
}

RegixInterfacesPersonalIdentityCtrl.$inject = ['$scope', '$state', 'Regix'];

export { RegixInterfacesPersonalIdentityCtrl };
