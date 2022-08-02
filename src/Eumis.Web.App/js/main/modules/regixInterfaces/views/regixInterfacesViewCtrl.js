function RegixInterfacesViewCtrl($scope) {
  $scope.tabList = {
    regixInterfaces_tabs_validPerson: 'root.regix.view.validPerson',
    regixInterfaces_tabs_personalIdentity: 'root.regix.view.personalIdentity',
    regixInterfaces_tabs_actualState: 'root.regix.view.actualState',
    regixInterfaces_tabs_stateOfPlay: 'root.regix.view.stateOfPlay',
    regixInterfaces_tabs_npoRegistration: 'root.regix.view.npoRegistration'
  };
}

RegixInterfacesViewCtrl.$inject = ['$scope'];

export { RegixInterfacesViewCtrl };
