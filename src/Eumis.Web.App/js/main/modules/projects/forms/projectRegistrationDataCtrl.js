function ProjectRegistrationDataCtrl($scope, scFormParams) {
  $scope.hasXml = scFormParams.hasXml;
}

ProjectRegistrationDataCtrl.$inject = ['$scope', 'scFormParams'];

export { ProjectRegistrationDataCtrl };
