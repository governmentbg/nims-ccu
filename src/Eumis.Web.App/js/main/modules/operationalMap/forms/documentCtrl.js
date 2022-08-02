function DocumentCtrl($scope, $state, scFormParams) {
  $scope.urlTemplate = scFormParams.urlTemplate;
  $scope.urlParamsId = scFormParams.urlParamsId;
  $scope.isAttachedFileReadOnly = scFormParams.isAttachedFileReadOnly;
}

DocumentCtrl.$inject = ['$scope', '$state', 'scFormParams'];

export { DocumentCtrl };
