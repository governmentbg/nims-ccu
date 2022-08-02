function RegixInterfacesStateOfPlayCtrl($scope, $state, Regix) {
  $scope.check = function() {
    return $scope.regixInterfacesStateOfPlayForm.$validate().then(function() {
      if ($scope.regixInterfacesStateOfPlayForm.$valid) {
        return Regix.stateOfPlay({}, $scope.request).$promise.then(function(data) {
          $scope.hasResponse = true;
          $scope.model = data;
          return $state.partialReload();
        });
      }
    });
  };
}

RegixInterfacesStateOfPlayCtrl.$inject = ['$scope', '$state', 'Regix'];

export { RegixInterfacesStateOfPlayCtrl };
