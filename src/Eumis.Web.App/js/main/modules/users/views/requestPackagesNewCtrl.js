function RequestPackagesNewCtrl($scope, $state, $stateParams, RequestPackage, newRequestPackage) {
  $scope.newRequestPackage = newRequestPackage;
  $scope.isDirect = $stateParams.d === 'true';

  $scope.save = function() {
    return $scope.newRequestPackageForm.$validate().then(function() {
      if ($scope.newRequestPackageForm.$valid) {
        return RequestPackage.save($scope.newRequestPackage).$promise.then(function(
          requestPackage
        ) {
          return $state.go('root.requestPackages.view.edit', {
            id: requestPackage.requestPackageId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.requestPackages.search');
  };
}

RequestPackagesNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'RequestPackage',
  'newRequestPackage'
];

RequestPackagesNewCtrl.$resolve = {
  newRequestPackage: [
    'RequestPackage',
    '$stateParams',
    function(RequestPackage, $stateParams) {
      return RequestPackage.newRequestPackage({
        isDirect: $stateParams.d === 'true'
      }).$promise;
    }
  ]
};

export { RequestPackagesNewCtrl };
