function PPriorityDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProgrammePriorityDocument,
  document
) {
  $scope.document = document;

  $scope.save = function() {
    return $scope.newProgrammePriorityDocumentForm.$validate().then(function() {
      if ($scope.newProgrammePriorityDocumentForm.$valid) {
        return ProgrammePriorityDocument.save(
          { id: $stateParams.id },
          $scope.document
        ).$promise.then(function() {
          return $state.go('root.map.ppriorities.view.documents.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.ppriorities.view.documents.search');
  };
}

PPriorityDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProgrammePriorityDocument',
  'document'
];

PPriorityDocumentsNewCtrl.$resolve = {
  document: [
    'ProgrammePriorityDocument',
    '$stateParams',
    function(ProgrammePriorityDocument, $stateParams) {
      return ProgrammePriorityDocument.newDocument({ id: $stateParams.id }).$promise;
    }
  ]
};

export { PPriorityDocumentsNewCtrl };
