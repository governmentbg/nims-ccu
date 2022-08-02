function RegixInterfacesActualStateCtrl($scope, $state, Regix) {
  $scope.check = function() {
    return $scope.regixInterfacesActualStateForm.$validate().then(function() {
      if ($scope.regixInterfacesActualStateForm.$valid) {
        return Regix.actualState({}, $scope.request).$promise.then(function(data) {
          $scope.hasResponse = true;
          $scope.model = data;
          return $state.partialReload();
        });
      }
    });
  };
}

RegixInterfacesActualStateCtrl.$inject = ['$scope', '$state', 'Regix'];

export { RegixInterfacesActualStateCtrl };
