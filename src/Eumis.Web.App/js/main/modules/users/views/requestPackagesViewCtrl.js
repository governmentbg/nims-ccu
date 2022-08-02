function RequestPackagesViewCtrl($scope, info) {
  $scope.info = info;
}

RequestPackagesViewCtrl.$inject = ['$scope', 'info'];

RequestPackagesViewCtrl.$resolve = {
  info: [
    'RequestPackage',
    '$stateParams',
    function(RequestPackage, $stateParams) {
      return RequestPackage.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { RequestPackagesViewCtrl };
