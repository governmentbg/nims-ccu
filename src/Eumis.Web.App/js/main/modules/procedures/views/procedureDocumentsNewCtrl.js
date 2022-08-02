function ProcedureDocumentsNewCtrl($scope, $state, $stateParams, ProcedureDocument, document) {
  $scope.document = document;

  $scope.save = function() {
    return $scope.newProcedureDocumentForm.$validate().then(function() {
      if ($scope.newProcedureDocumentForm.$valid) {
        return ProcedureDocument.save({ id: $stateParams.id }, $scope.document).$promise.then(
          function() {
            return $state.go('root.procedures.view.allDocs.search');
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.allDocs.search');
  };
}

ProcedureDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureDocument',
  'document'
];

ProcedureDocumentsNewCtrl.$resolve = {
  document: [
    'ProcedureDocument',
    '$stateParams',
    function(ProcedureDocument, $stateParams) {
      return ProcedureDocument.newDocument({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureDocumentsNewCtrl };
