function ValidationErrorsModalCtrl($scope, $uibModalInstance, scModalParams) {
  $scope.errors = scModalParams.errors;

  $scope.cancel = function() {
    return $uibModalInstance.close();
  };
}

ValidationErrorsModalCtrl.$inject = ['$scope', '$uibModalInstance', 'scModalParams'];

export { ValidationErrorsModalCtrl };
