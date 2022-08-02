function RegixInterfacesValidPersonCtrl($scope, $state, Regix) {
  $scope.check = function() {
    return $scope.regixInterfacesValidPersonForm.$validate().then(function() {
      if ($scope.regixInterfacesValidPersonForm.$valid) {
        return Regix.personValid({}, $scope.request).$promise.then(function(data) {
          $scope.hasResponse = true;
          $scope.model = data;
          return $state.partialReload();
        });
      }
    });
  };
}

RegixInterfacesValidPersonCtrl.$inject = ['$scope', '$state', 'Regix'];

export { RegixInterfacesValidPersonCtrl };
