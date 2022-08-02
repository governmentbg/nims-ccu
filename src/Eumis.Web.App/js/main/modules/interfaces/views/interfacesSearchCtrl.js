function InterfacesSearchCtrl($scope) {
  $scope.filters = {
    contractId: null,
    informationSystem: null
  };
}

InterfacesSearchCtrl.$inject = ['$scope'];

export { InterfacesSearchCtrl };
