function EvalSessionDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  EvalSessionDocument,
  newEvalSessionDocument
) {
  $scope.newEvalSessionDocument = newEvalSessionDocument;

  $scope.save = function() {
    return $scope.newEvalSessionDocumentForm.$validate().then(function() {
      if ($scope.newEvalSessionDocumentForm.$valid) {
        return EvalSessionDocument.save(
          { id: $stateParams.id },
          $scope.newEvalSessionDocument
        ).$promise.then(function() {
          return $state.go('root.evalSessions.view.allDocs.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.allDocs.search');
  };
}

EvalSessionDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'EvalSessionDocument',
  'newEvalSessionDocument'
];

EvalSessionDocumentsNewCtrl.$resolve = {
  newEvalSessionDocument: [
    '$stateParams',
    'EvalSessionDocument',
    function($stateParams, EvalSessionDocument) {
      return EvalSessionDocument.newEvalSessionDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { EvalSessionDocumentsNewCtrl };
