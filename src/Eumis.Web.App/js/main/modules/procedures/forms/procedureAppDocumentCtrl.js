function ProcedureAppDocumentCtrl($scope, scFormParams) {
  $scope.isProgrammeIntroduced = scFormParams.isProgrammeIntroduced;

  $scope.setDocumentAttributes = function(document) {
    if (document) {
      $scope.model.name = document.name;
      $scope.model.isSignatureRequired = document.isSignatureRequired;
      $scope.model.extension = document.extension;
    } else {
      $scope.model.name = null;
      $scope.model.isSignatureRequired = null;
      $scope.model.extension = null;
    }
  };
}

ProcedureAppDocumentCtrl.$inject = ['$scope', 'scFormParams'];

export { ProcedureAppDocumentCtrl };
